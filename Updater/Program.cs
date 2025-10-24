using System.IO.Compression;
using System.Diagnostics;

class Updater
{
    static async Task Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: Updater <zipPath> <expectedChecksum> [mainAppPid] [--restart-only]");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return;
        }

        bool restartOnly = args.Contains("--restart-only");

        string zipPath = args[0];
        int? mainAppPid = args.Length >= 2 && int.TryParse(args[1], out var pid) ? pid : null;

        string baseDir = AppContext.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        string appDirectory = Directory.GetParent(baseDir)?.FullName ?? baseDir;

        if (mainAppPid.HasValue)
        {
            try
            {
                var proc = Process.GetProcessById(mainAppPid.Value);
                Console.WriteLine($"Waiting for MainApp (PID {mainAppPid.Value}) to exit...");
                proc.WaitForExit();
            }
            catch (ArgumentException)
            {
                // Process already exited
            }
        }
        else
        {
            // Fallback: wait for process name
            while (Process.GetProcessesByName("Day2eEditor").Any())
            {
                await Task.Delay(500);
            }
        }

        if (!restartOnly)
        {
            // Extract update
            Console.WriteLine("Extracting update...");
            using (FileStream fs = File.OpenRead(zipPath))
            {
                using (ZipArchive zip = new ZipArchive(fs))
                {
                    ExtractToDirectory(zip, appDirectory, true);
                }
            }
            File.Delete(zipPath);
            Console.WriteLine("Update applied.");
        }
        else
        {
            Console.WriteLine("Restart-only mode, skipping extraction.");
        }

        // Restart main app
        RestartMainApp(appDirectory);
        //Console.WriteLine("Press any key to continue...");
        //Console.ReadKey();
    }

    private static void RestartMainApp(string appDirectory)
    {
        // Restart main app
        string mainAppExe = Path.Combine(appDirectory, "Day2eEditor.exe");
        if (File.Exists(mainAppExe))
        {
            Console.WriteLine("Restarting Day2eEditor...");
            Process.Start(new ProcessStartInfo
            {
                FileName = mainAppExe,
                UseShellExecute = true
            });
        }
        else
        {
            Console.WriteLine($"{mainAppExe} not found.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
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
            {// Assuming Empty for Directory
                Directory.CreateDirectory(Path.GetDirectoryName(completeFileName));
                continue;
            }
            file.ExtractToFile(completeFileName, true);
        }
    }
}

