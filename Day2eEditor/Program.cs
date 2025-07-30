
namespace Day2eEditor
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var updateManager = new UpdateManager(); // Your own class
            await updateManager.CheckForUpdatesAsync();

            Application.Run(new Form1());
        }
    }
}