using System.IO.Compression;
using System.Diagnostics;

class Updater
{
    static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Invalid arguments.");
            return;
        }

        string zipFilePath = args[0];
        string expectedChecksum = args[1];
        string appDirectory = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

        try
        {
            // Verify checksum (optional)
            if (!ChecksumUtils.VerifyChecksum(File.ReadAllBytes(zipFilePath), expectedChecksum))
            {
                throw new InvalidOperationException("Checksum mismatch. Update failed.");
            }

            // Extract the ZIP file into the application directory
            string extractPath = appDirectory;
            Console.WriteLine("Extracting update...");
            ZipFile.ExtractToDirectory(zipFilePath, extractPath);

            // Restart the application
            string appExePath = Path.Combine(appDirectory, "YourApp.exe");
            Process.Start(appExePath);  // Start the app again

            Console.WriteLine("Update applied successfully. Restarting application.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during update: {ex.Message}");
        }
    }
}

