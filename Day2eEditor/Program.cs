
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

            var updateManager = new UpdateManager("Plugins", "Downloads");

            try
            {
                await updateManager.CheckAndUpdateAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Update failed:\n{ex.Message}");
            }
            Application.Run(new Form1());
        }
    }
}