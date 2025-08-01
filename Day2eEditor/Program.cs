
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
            //updatemanager
            var updateManager = new UpdateManager("Plugins", "Downloads");

            try
            {
                await updateManager.CheckAndUpdateAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Update failed:\n{ex.Message}");
            }
            //projectmanager
            var projectManager = new ProjectManager("Projects");
            AppServices.Register(projectManager);
            projectManager.Load();
            String Activeproject = null;
            if(projectManager.CurrentProject == null)
            {
                Activeproject = "Active Project : None Selected";
            }
            else
            {
                Activeproject = $"Active Project : {projectManager.CurrentProject.ProjectName}";
            }
            Console.WriteLine(Activeproject);
            Application.Run(new Form1(Activeproject));
        }
    }
}