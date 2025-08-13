
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

            //updatemanager
            var updateManager = new UpdateManager();

            try
            {
                Task.Run(() => updateManager.CheckAndUpdateAsync()).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Update failed:\n{ex.Message}");
            }
            AppServices.Register(updateManager);

            // Register fileService
            AppServices.Register(new FileService());

            //projectmanager
            var projectManager = new ProjectManager("Projects");
            AppServices.Register(projectManager);
            projectManager.Load();

            string activeProject = projectManager.CurrentProject == null
                ? "Active Project : None Selected"
                : $"Active Project : {projectManager.CurrentProject.ProjectName}";
            Console.WriteLine(activeProject);
            var economymanager = new EconomyManager();
            AppServices.Register(economymanager);
            if (projectManager.CurrentProject != null)
            {
                economymanager.SetProject(projectManager.CurrentProject);
                if (economymanager.HasErrors)
                {
                    var errorForm = new ErrorDialog("EconomyManager Errors", economymanager.Errors);
                    errorForm.StartPosition = FormStartPosition.CenterScreen;
                    errorForm.ShowDialog();


                    Application.Exit(); // Cleanly close WinForms app
                    return;
                }
                
            }

            Application.Run(new Form1(activeProject));
        }
    }
}