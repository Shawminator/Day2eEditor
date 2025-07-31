using System.IO.Compression;
using System.Diagnostics;

class Updater
{
    static async Task Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: Updater <zipPath> <expectedChecksum> [mainAppPid]");
            return;
        }

        string zipPath = args[0];
        int? mainAppPid = args.Length >= 2 && int.TryParse(args[1], out var pid) ? pid : null;

        string appDirectory = AppContext.BaseDirectory;

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

        // Extract
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
            Console.WriteLine("Day2eEditor.exe not found.");
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

