using System.Diagnostics;
using System.IO.Compression;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;

namespace Day2eEditor
{
    public class UpdateManager
    {
        private readonly string _mapAddonsManifestPath;
        private readonly string _pluginsDirectory;
        private readonly string _mapAddonsDirectory;
        private readonly string _downloadDirectory;
        private readonly string _tempDirectory;
        private readonly string _appDirectory;
        private readonly HttpClient _http;
        public string manifestUrl = "https://raw.githubusercontent.com/Shawminator/Day2eEditor/refs/heads/master/Manifest.json";
        private readonly HashSet<string> _compulsoryPlugins = new() { "ProjectsPlugin", "EconomyPlugin"};

        public UpdateManager(string pluginsDirectory = "Plugins", string mapAddonsDirectory = "MapAddons", string tempDirectory = "Temp", string appDirectory = "")
        {
            _pluginsDirectory = pluginsDirectory;
            _mapAddonsDirectory = mapAddonsDirectory;
            _mapAddonsManifestPath = Path.Combine(_mapAddonsDirectory, "mapaddons.json");
            _tempDirectory = tempDirectory;
            _appDirectory = appDirectory == string.Empty ? Directory.GetCurrentDirectory() : appDirectory;
            Directory.CreateDirectory(_pluginsDirectory);
            Directory.CreateDirectory(mapAddonsDirectory);
            Directory.CreateDirectory("Data");
            Directory.CreateDirectory(_tempDirectory);
            _http = new HttpClient();
            CleanupMarkedPlugins();
        }
        private void CleanupMarkedPlugins()
        {
            Console.WriteLine("Clean up:");
            foreach (var file in Directory.GetFiles(_pluginsDirectory, "*.dll.delete"))
            {
                try
                {
                    File.Delete(file);
                    Console.WriteLine($"\t{Path.GetFileName(file)} Removed");
                }
                catch
                {
                    
                }
            }
            if (File.Exists("Core.dll.delete"))
            {
                try
                {
                    File.Delete("Core.dll.delete");
                    Console.WriteLine($"\t{Path.GetFileName("Core.dll.delete")} Removed");
                }
                catch
                {
                   
                }
            }
            if (File.Exists("CoreUI.dll.delete"))
            {
                try
                {
                    File.Delete("CoreUI.dll.delete");
                    Console.WriteLine($"\t{Path.GetFileName("CoreUI.dll.delete")} Removed");
                }
                catch
                {
                    
                }
            }
        }
        public async Task CheckAndUpdateAsync()
        {
            Console.WriteLine($"Fetching manifest from {manifestUrl}...");
            Manifest manifest = await _http.GetFromJsonAsync<Manifest>(manifestUrl);
            AppServices.Register(manifest);
            if (manifest == null)
            {
                Console.WriteLine("Failed to fetch or parse manifest.");
                return;
            }

            // Update the main app (ZIP)
            await CheckAndUpdateMainAppAsync(manifest.MainApp);

            // Core DLLs first
            bool coreUpdated = await CheckAndUpdateCoreDllAsync("Core", manifest.Core.Url, manifest.Core.Version, manifest.Core.Checksum);
            bool coreUIUpdated = await CheckAndUpdateCoreDllAsync("CoreUI", manifest.CoreUI.Url, manifest.CoreUI.Version, manifest.CoreUI.Checksum);

            // If either was updated, restart
            if (coreUpdated || coreUIUpdated)
            {
                Console.WriteLine("Core DLL(s) updated — restarting application to apply updates...");
                InitiateShutdown(restartOnly: true);
                return;
            }


            // Update plugins
            if (manifest.Plugins != null)
            {
                foreach (var plugin in manifest.Plugins)
                {
                    await CheckAndUpdatePluginAsync(plugin);
                }
            }
            // Update MapAddons
            if (manifest.MapAddons != null)
            {
                await CheckAndUpdateMapAddonsAsync(manifest.MapAddons);
            }

            Console.WriteLine("Update checks complete.");
        }

        private async Task<bool> CheckAndUpdateCoreDllAsync(
             string name,
             string dllUrl,
             string version,
             string dllChecksum,
             string pdbUrl = null,
             string pdbChecksum = null)
        {
            string dllPath = Path.Combine(_appDirectory, $"{name}.dll");
            bool updated = false;

            bool needsUpdate = true;

            if (File.Exists(dllPath))
            {
                var localVersion = AssemblyName.GetAssemblyName(dllPath).Version;
                var remoteVersion = Version.Parse(version);
                if (localVersion >= remoteVersion)
                {
                    Console.WriteLine($"{name} is up-to-date (v{localVersion})");
                    needsUpdate = false;
                }
                else
                {
                    Console.WriteLine($"{name} update available: v{localVersion} -> v{remoteVersion}");
                }
            }

            if (needsUpdate)
            {
                Console.WriteLine($"Downloading {name}...");
                byte[] data = await _http.GetByteArrayAsync(dllUrl);

                if (!ChecksumUtils.VerifyChecksum(data, dllChecksum))
                    throw new InvalidOperationException($"Checksum verification failed for {name}");

                // Write DLL to temporary file
                string tmpDll = dllPath + ".update";
                await File.WriteAllBytesAsync(tmpDll, data);

                // Rename so when next restart the editor know to delete.
                if (File.Exists(dllPath)) File.Move(dllPath, dllPath + ".delete", true);
                File.Move(tmpDll, dllPath);

                updated = true;
                Console.WriteLine($"{name} updated successfully.");
            }

            return updated;
        }

        private async Task CheckAndUpdateMainAppAsync(AppInfo mainApp)
        {
            // create the zip file path based on the version
            string appZipFileName = $"Day2eEditor.zip";
            string appZipFilePath = Path.Combine(_tempDirectory, appZipFileName);

            bool needsUpdate = true;

            // Get the version of the running application
            Version? currentVersion = Assembly.GetEntryAssembly()?.GetName().Version;
            Version manifestVersion = new Version(mainApp.Version);

            // Compare versions
            if (currentVersion >= manifestVersion)
            {
                Console.WriteLine($"The current version ({currentVersion}) is up-to-date with the latest version ({manifestVersion}).");
                needsUpdate = false; // No need to update if the current version is equal to or greater than the latest version
            }
            else
            {
                Console.WriteLine($"A new version ({manifestVersion}) is available. Current version is {currentVersion}. Update needed.");
            }

            // If the main app needs updating, download and verify it
            if (needsUpdate)
            {
                Console.WriteLine($"Downloading the {appZipFileName}...");
                byte[] data = await _http.GetByteArrayAsync(mainApp.Url);

                if (!ChecksumUtils.VerifyChecksum(data, mainApp.Checksum))
                    throw new InvalidOperationException("Checksum verification failed for the main app.");

                // Save the main app ZIP file to the temporary folder
                await File.WriteAllBytesAsync(appZipFilePath, data);
                Console.WriteLine($"Main app ZIP downloaded and verified.");

                // shutdown to replace application files
                Console.WriteLine("Application will now shutdown to apply updates...");
                InitiateShutdown(appZipFilePath, mainApp);
            }
        }
        private void InitiateShutdown(string zipFilePath = null, AppInfo mainApp = null, bool restartOnly = false)
        {
            string updaterPath = Path.Combine(_appDirectory,"Updater", "Updater.exe");
            int pid = Process.GetCurrentProcess().Id;

            if (!File.Exists(updaterPath))
            {
                Console.WriteLine("Updater.exe not found at: " + updaterPath);
                return;
            }

            string args;
            if (restartOnly)
            {
                args = "--restart-only";
            }
            else
            {
                args = $"\"{zipFilePath}\" \"{pid}\"";
            }

            var psi = new ProcessStartInfo
            {
                FileName = updaterPath,
                Arguments = args,
                WorkingDirectory = _appDirectory,
                UseShellExecute = true,
                CreateNoWindow = false
            };

            try
            {
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to start Updater.exe: " + ex.Message);
            }
            Environment.Exit(0);
        }
        public async Task CheckAndUpdatePluginAsync(PluginInfo plugin)
        {
            string pluginPath = Path.Combine(_pluginsDirectory, $"{plugin.Name}.dll");

            bool isCompulsory = _compulsoryPlugins.Contains(plugin.Name);
            bool pluginExists = File.Exists(pluginPath);

            // Skip optional plugins that don't exist
            if (!isCompulsory && !pluginExists)
            {
                Console.WriteLine($"{plugin.Name} is optional and not present. Skipping.");
                return;
            }

            bool needsUpdate = true;

            // Check if the plugin is already downloaded and up-to-date
            if (pluginExists)
            {
                try
                {
                    var localVersion = AssemblyName.GetAssemblyName(pluginPath).Version;
                    var remoteVersion = Version.Parse(plugin.Version);

                    if (localVersion >= remoteVersion)
                    {
                        Console.WriteLine($"{plugin.Name} is up-to-date (v{localVersion}).");
                        needsUpdate = false;
                    }
                    else
                    {
                        Console.WriteLine($"{plugin.Name} v{localVersion} -> v{remoteVersion} available.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Warning: Could not read version of {plugin.Name}.dll. Will re-download. ({ex.Message})");
                }
            }

            // If the plugin needs updating, download and verify it
            if (needsUpdate)
            {
                Console.WriteLine($"Downloading {plugin.Name}...");
                byte[] data = await _http.GetByteArrayAsync(plugin.Url);

                if (!ChecksumUtils.VerifyChecksum(data, plugin.Checksum))
                    throw new InvalidOperationException($"Checksum verification failed for {plugin.Name}");

                await File.WriteAllBytesAsync(pluginPath, data);
                Console.WriteLine($"{plugin.Name} updated successfully.");
            }
        }
        public async Task DownloadNewPluginAsync(PluginInfo plugin)
        {
            string pluginPath = Path.Combine(_pluginsDirectory, $"{plugin.Name}.dll");

            Console.WriteLine($"Downloading {plugin.Name}...");
            byte[] data = await _http.GetByteArrayAsync(plugin.Url);

            if (!ChecksumUtils.VerifyChecksum(data, plugin.Checksum))
                throw new InvalidOperationException($"Checksum verification failed for {plugin.Name}");

            await File.WriteAllBytesAsync(pluginPath, data);
            Console.WriteLine($"{plugin.Name} Downloaded successfully.");
        }
        private async Task CheckAndUpdateMapAddonsAsync(List<MapAddonInfo> remoteAddons)
        {
            List<MapAddonInfo> localAddons = new();
            Console.WriteLine("[MapAddons] Checking for updates......");
            // If file doesn't exist, just save it and return
            if (!File.Exists(_mapAddonsManifestPath))
            {
                Console.WriteLine("No local mapaddons.json found — creating from manifest.");
                string newJson = JsonSerializer.Serialize(remoteAddons, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(_mapAddonsManifestPath, newJson);
                return;
            }

            // Otherwise, read and compare
            try
            {
                string json = await File.ReadAllTextAsync(_mapAddonsManifestPath);
                localAddons = JsonSerializer.Deserialize<List<MapAddonInfo>>(json) ?? new();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Could not read local mapaddons.json: {ex.Message}");
                localAddons = new();
            }

            foreach (var remoteAddon in remoteAddons)
            {
                string localPngPath = Path.Combine(_mapAddonsDirectory, remoteAddon.MapInfo.MapPng);
                string localXyzPath = Path.Combine(_mapAddonsDirectory, remoteAddon.MapInfo.MapXYZ);

                // Skip if addon is not installed (missing either file)
                if (!File.Exists(localPngPath) && !File.Exists(localXyzPath))
                {
                    continue;
                }

                var localAddon = localAddons.FirstOrDefault(a => a.Name == remoteAddon.Name);

                bool needsUpdate =
                    localAddon == null ||
                    localAddon.Version != remoteAddon.Version ||
                    localAddon.Checksum != remoteAddon.Checksum ||
                    !File.Exists(localPngPath) || !File.Exists(localXyzPath);

                if (needsUpdate)
                {
                    string addonpath = Path.Combine(_tempDirectory, $"{remoteAddon.Name}.zip");
                    Console.WriteLine($"{remoteAddon.Name} needs update (local: {localAddon?.Version ?? "none"}, remote: {remoteAddon.Version})");
                    Console.WriteLine($"Downloading {remoteAddon.Name}...");
                    byte[] data = await _http.GetByteArrayAsync(remoteAddon.Url);

                    if (!ChecksumUtils.VerifyChecksum(data, remoteAddon.Checksum))
                        throw new InvalidOperationException($"Checksum verification failed for {remoteAddon.Name}");

                    await File.WriteAllBytesAsync(addonpath, data);
                    Console.WriteLine("Extracting MapAddon...");
                    using (FileStream fs = File.OpenRead(addonpath))
                    {
                        using (ZipArchive zip = new ZipArchive(fs))
                        {
                            ExtractToDirectory(zip, _mapAddonsDirectory, true);
                        }
                    }
                    File.Delete(addonpath);
                    Console.WriteLine($"{remoteAddon.Name} updated successfully.");
                }
            }

            string updatedJson = JsonSerializer.Serialize(remoteAddons, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_mapAddonsManifestPath, updatedJson);
        }
        public static void ExtractToDirectory(ZipArchive archive, string destinationDirectoryName, bool overwrite)
        {
            if (!overwrite)
            {
                archive.ExtractToDirectory(destinationDirectoryName);
                return;
            }

            DirectoryInfo di = Directory.CreateDirectory(destinationDirectoryName);
            string destinationDirectoryFullPath = di.FullName;

            foreach (ZipArchiveEntry file in archive.Entries)
            {
                if (file.Name == "Updater.exe" || file.Name == "Updater.dll") continue;
                string completeFileName = Path.GetFullPath(Path.Combine(destinationDirectoryFullPath, file.FullName));
                Console.WriteLine("Extracting : " + completeFileName);
                if (!completeFileName.StartsWith(destinationDirectoryFullPath, StringComparison.OrdinalIgnoreCase))
                {
                    throw new IOException("Trying to extract file outside of destination directory");
                }

                if (file.Name == "")
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(completeFileName));
                    continue;
                }
                file.ExtractToFile(completeFileName, true);
            }
        }
        public async Task DownloadMapAddonAsync(MapAddonInfo addon)
        {
            string addonpath = Path.Combine(_tempDirectory, $"{addon.Name}.zip");

            Console.WriteLine($"Downloading {addon.Name}...");
            byte[] data = await _http.GetByteArrayAsync(addon.Url);

            if (!ChecksumUtils.VerifyChecksum(data, addon.Checksum))
                throw new InvalidOperationException($"Checksum verification failed for {addon.Name}");

            await File.WriteAllBytesAsync(addonpath, data);
            Console.WriteLine("Extracting MapAddon...");
            using (FileStream fs = File.OpenRead(addonpath))
            {
                using (ZipArchive zip = new ZipArchive(fs))
                {
                    ExtractToDirectory(zip, _mapAddonsDirectory, true);
                }
            }
            Console.WriteLine($"{addon.Name} Downloaded successfully.");
        }
    }
}