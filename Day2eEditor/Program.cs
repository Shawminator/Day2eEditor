
using System.Reflection;
using System.Runtime.Loader;

namespace Day2eEditor
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            //Register updatemanager
            var updateManager = new UpdateManager();
            try
            {
                Task.Run(() => updateManager.CheckAndUpdateAsync()).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Update failed:\n{ex.Message}", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            AppServices.Register(updateManager);

           

            // Register fileService
            AppServices.Register(new FileService());

            //Register projectmanager
            var projectManager = new ProjectManager("Projects");
            AppServices.Register(projectManager);
            projectManager.Load();

            string activeProject = projectManager.CurrentProject == null
                ? "Active Project : None Selected"
                : $"Active Project : {projectManager.CurrentProject.ProjectName}";

            Console.WriteLine(activeProject);

            // EconomyManager
            var economyManager = new EconomyManager();
            economyManager.SetExternalFiles();
            AppServices.Register(economyManager);
            if (projectManager.CurrentProject != null)
            {
                economyManager.SetProject(projectManager.CurrentProject);
                if (economyManager.HasErrors)
                {
                    var errorForm = new ErrorDialog("EconomyManager Errors", economyManager.Errors)
                    {
                        StartPosition = FormStartPosition.CenterScreen
                    };
                    errorForm.ShowDialog();
                    Application.Exit(); // Cleanly close WinForms app
                    return;
                }
            }

            Application.Run(new Form1(activeProject));
        }
    }
}