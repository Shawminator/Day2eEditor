using System.Diagnostics;
using System.Net.Http.Json;
using System.Reflection;

namespace Day2eEditor
{
    public class UpdateManager
    {
        private readonly string _pluginsDirectory;
        private readonly string _downloadDirectory;
        private readonly string _tempDirectory;
        private readonly string _appDirectory;  // This is the application directory
        private readonly HttpClient _http;
        public string manifestUrl = "https://raw.githubusercontent.com/Shawminator/Day2eEditor/refs/heads/master/Manifest.json";
        private readonly HashSet<string> _compulsoryPlugins = new() { "ProjectsPlugin", "EconomyPlugin"};

        public UpdateManager(string pluginsDirectory = "Plugins", string downloadDirectory = "Downloads", string tempDirectory = "Temp", string appDirectory = "")
        {
            _pluginsDirectory = pluginsDirectory;
            _downloadDirectory = downloadDirectory;
            _tempDirectory = tempDirectory;
            _appDirectory = appDirectory == string.Empty ? Directory.GetCurrentDirectory() : appDirectory; // Default to current directory
            Directory.CreateDirectory(_pluginsDirectory);
            Directory.CreateDirectory(_downloadDirectory);
            Directory.CreateDirectory(_tempDirectory); // Temporary directory for downloading
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
                    // Log or ignore
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

            // Update plugins
            if (manifest.Plugins != null)
            {
                foreach (var plugin in manifest.Plugins)
                {
                    await CheckAndUpdatePluginAsync(plugin);
                }
            }

            Console.WriteLine("Update check complete.");
        }

        private async Task CheckAndUpdateMainAppAsync(AppInfo mainApp)
        {
            // Dynamically create the zip file path based on the version
            string appZipFileName = $"Day2eEditor_v{mainApp.Version}.zip";  // Example naming convention
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

                // Initiate shutdown to replace application files
                Console.WriteLine("Application will now shutdown to apply updates...");
                InitiateShutdown(appZipFilePath, mainApp);
            }
        }

        private void InitiateShutdown(string zipFilePath, AppInfo mainApp)
        {
            // Create a separate process to restart the application after the shutdown
            string updaterPath = Path.Combine(_appDirectory, "Updater.exe");
            int pid = Process.GetCurrentProcess().Id;

            if (!File.Exists(updaterPath))
            {
                Console.WriteLine("Updater.exe not found at: " + updaterPath);
                return;
            }

            var psi = new ProcessStartInfo
            {
                FileName = updaterPath,
                Arguments = $"\"{zipFilePath}\" \" {pid}",
                WorkingDirectory = _appDirectory,
                UseShellExecute = true, // better for launching external .exe files
                CreateNoWindow = true
            };

            try
            {
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to start Updater.exe: " + ex.Message);
            }

            // Shutdown the current application
            Environment.Exit(0);  // Close the current app to allow updates
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
    }
}