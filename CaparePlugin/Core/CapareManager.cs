using Day2eEditor;

namespace CaparePlugin
{
    public class CapareManager
    {
        public readonly Dictionary<string, string> _paths = new();
        public string profilePath { get; set; }
        public bool HasErrors { get; set; }
        public List<string> Errors = new List<string>();

        public CapareDynamicMusicConfig CapareDynamicMusicConfig { get; set; }
        public CapareKeyCardConfig CapareKeyCardConfig { get; set; }
        public CapareLootBoxConfig CapareLootBoxConfig { get; set; }
        public CapareLPFrameworkConfig CapareLPFrameworkConfig { get; set; }
        public CapareWorkBenchConfig CapareWorkBenchConfig { get; set; }
        public CapareSalvageConfig CapareSalvageConfig { get; set; }

        public CapareManager() { }
        public void SetCapareStuff()
        {
            profilePath = Path.Combine(AppServices.GetRequired<ProjectManager>().CurrentProject.ProjectRoot, AppServices.GetRequired<ProjectManager>().CurrentProject.ProfileName);

            // Capare_Scripts - $profile:Capare/
            _paths["DynamicMusic"] = Path.Combine(profilePath, "Capare", "Capare_Dynamic_Music", "Capare_Dynamic_Music.json");

            // Capare_Scripts_Shawminator - $profile:Capare_Shawminator/
            _paths["KeyCardSystem"] = Path.Combine(profilePath, "Capare_Shawminator", "Capare_KeyCard_System", "Capare_KeyCard_System.json");
            _paths["LootBox"] = Path.Combine(profilePath, "Capare_Shawminator", "Capare_LootBox", "Capare_LootBox.json");
            _paths["LPFramework"] = Path.Combine(profilePath, "Capare_Shawminator", "Capare_LP_Framework", "Capare_LP_Framework.json");

            // Capare_WorkBench - $profile:Capare_Workbench/
            _paths["WorkBench"] = Path.Combine(profilePath, "Capare_Workbench", "CapareWorkBenchConfig.json");
            _paths["Salvage"] = Path.Combine(profilePath, "Capare_Workbench", "CapareSalvageConfig.json");

            CreateFolders();
            LoadFiles();
        }
        private void CreateFolders()
        {
            Console.WriteLine("\n[Capare Manager] Checking and creating folders associated with the Capare Mods.");

            foreach (var path in _paths.Values)
            {
                var directory = Path.GetDirectoryName(path);
                if (string.IsNullOrWhiteSpace(directory))
                    continue;

                bool existed = Directory.Exists(directory);

                Directory.CreateDirectory(directory);

                if (!existed)
                    Console.WriteLine($"[INFO] Folder Created - {directory}");
            }
        }
        private void LoadFiles()
        {
            Console.WriteLine($"\n[Capare Manager] Loading all files associated with the Capare Mods.");

            CapareDynamicMusicConfig = new CapareDynamicMusicConfig(_paths["DynamicMusic"]);
            LoadConfigWithErrorReport("DynamicMusic", CapareDynamicMusicConfig);

            CapareKeyCardConfig = new CapareKeyCardConfig(_paths["KeyCardSystem"]);
            LoadConfigWithErrorReport("KeyCardSystem", CapareKeyCardConfig);

            CapareLootBoxConfig = new CapareLootBoxConfig(_paths["LootBox"]);
            LoadConfigWithErrorReport("LootBox", CapareLootBoxConfig);

            CapareLPFrameworkConfig = new CapareLPFrameworkConfig(_paths["LPFramework"]);
            LoadConfigWithErrorReport("LPFramework", CapareLPFrameworkConfig);

            CapareWorkBenchConfig = new CapareWorkBenchConfig(_paths["WorkBench"]);
            LoadConfigWithErrorReport("WorkBench", CapareWorkBenchConfig);

            CapareSalvageConfig = new CapareSalvageConfig(_paths["Salvage"]);
            LoadConfigWithErrorReport("Salvage", CapareSalvageConfig);

            // Writes out anything validation fixed on load, but not if a file failed to load,
            // as that would overwrite it with the defaults
            if (!HasErrors)
                Save();
        }
        private void LoadConfigWithErrorReport(string name, IConfigLoader config)
        {
            config.Load();

            if (config.HasErrors)
            {
                HasErrors = true;
                Errors.AddRange(config.Errors.Select(e => $"[{name}] {e}"));
            }
        }
        private IEnumerable<IConfigLoader> GetConfigs()
        {
            return new IConfigLoader[]
            {
                CapareDynamicMusicConfig,
                CapareKeyCardConfig,
                CapareLootBoxConfig,
                CapareLPFrameworkConfig,
                CapareWorkBenchConfig,
                CapareSalvageConfig
            }.Where(x => x != null);
        }
        public IEnumerable<string> Save()
        {
            var savedFiles = new List<string>();
            var projectManager = AppServices.GetRequired<ProjectManager>();
            var uploadTracker = AppServices.GetRequired<UploadTrackerService>();
            foreach (IConfigLoader config in GetConfigs())
            {
                IEnumerable<string> savedfiles = config.Save();
                if (savedfiles.Count() > 0)
                {
                    savedFiles.AddRange(savedfiles);
                    uploadTracker.MarkForUpload(projectManager.CurrentProject.ProjectName, savedfiles);
                }
            }
            return savedFiles;
        }
        public bool needToSave()
        {
            return GetConfigs().Any(x => x.NeedToSave());
        }
    }
}
