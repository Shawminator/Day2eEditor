using Day2eEditor;
using static System.Net.Mime.MediaTypeNames;

namespace Day2eEditor
{
    public interface IConfigLoader
    {
        void Load();
        bool HasErrors { get; }
        public List<string> Errors { get; }
    }
    public interface IAdvancedConfigLoader : IConfigLoader
    {
        void LoadWithParameters(string vanillaPath, List<string> modPaths);
    }
    public class EconomyManager
    {
        private readonly Dictionary<string, string> _paths = new();
        private readonly ProjectManager _projectManager;

        public bool HasErrors { get; set; }
        public List<string> Errors = new List<string>();

        public economyCoreConfig eonomyCoreConfig { get; set; }
        public cfglimitsdefinitionConfig cfglimitsdefinitionConfig { get; set; }
        public cfglimitsdefinitionuserConfig cfglimitsdefinitionuserConfig { get; set; }
        public cfgenvironmentConfig cfgenvironmentConfig { get; set; }

        public economyConfig economyConfig { get; set; }
        public globalsConfig globalsConfig { get; set; }
        public TypesConfig TypesConfig { get; set; }
        public eventsConfig eventsConfig { get; set; }
        public cfgspawnabletypesConfig cfgspawnabletypesConfig { get; set; }
        public cfgrandompresetsConfig cfgrandompresetsConfig { get; set; }

        public CFGGameplayConfig CFGGameplayConfig { get; set; }
        public cfgeffectareaConfig cfgeffectareaConfig { get; set; }
        public cfgundergroundtriggersConfig cfgundergroundtriggersConfig { get; set; }

        //public cfgplayerspawnpoints cfgplayerspawnpoints { get; set; }
        //public cfgeventspawns cfgeventspawns { get; set; }
        //public cfgeventgroups cfgeventgroups { get; set; }
        //public cfgignorelist cfgignorelist { get; set; }
        //public weatherconfig weatherconfig { get; set; }
        //public mapgroupproto mapgroupproto { get; set; }
        //public mapgrouppos mapgrouppos { get; set; }
        //public BindingList<territoriesConfig> territoriesList;

        public EconomyManager() 
        {
            _projectManager = AppServices.GetRequired<ProjectManager>();

            var project = _projectManager.CurrentProject;
            if (project != null)
            {
                SetProject(project);
            }
        }
        public void SetProject(Project project)
        {
            string basePath = Path.Combine(project.ProjectRoot, "mpmissions", project.MpMissionPath);

            _paths["cfgeconomycore"] = Path.Combine(basePath, "cfgeconomycore.xml");
            _paths["cfglimitsdefinition"] = Path.Combine(basePath, "cfglimitsdefinition.xml");
            _paths["cfglimitsdefinitionuser"] = Path.Combine(basePath, "cfglimitsdefinitionuser.xml");
            _paths["cfgenvironment"] = Path.Combine(basePath, "cfgenvironment.xml");
            _paths["CFGGameplay"] = Path.Combine(basePath, "cfggameplay.json");
            _paths["cfgeffectarea"] = Path.Combine(basePath, "cfgeffectarea.json");
            _paths["cfgundergroundtriggers"] = Path.Combine(basePath, "cfgundergroundtriggers.json");

            _paths["VanillaTypes"] = Path.Combine(basePath, "db", "types.xml");
            _paths["VanillaEvents"] = Path.Combine(basePath, "db", "events.xml");
            _paths["VanillaEconomy"] = Path.Combine(basePath, "db", "economy.xml");
            _paths["VanillaGlobals"] = Path.Combine(basePath, "db", "globals.xml");
            _paths["VanillaSpawnableTypes"] = Path.Combine(basePath, "cfgspawnabletypes.xml");
            _paths["VanillaRandomPresets"] = Path.Combine(basePath, "cfgrandompresets.xml");
            LoadFiles(basePath);
        }
        private void LoadConfigWithErrorReport(string name, IConfigLoader config)
        {
            if (config is IAdvancedConfigLoader advanced)
            {
                string vanillaPath = null;
                List<string> modPaths = new();

                var configMap = new Dictionary<string, (string VanillaKey, string ModKey, string ModdedPathKey)>
                    {
                        { "Types", ("VanillaTypes", "types", "types") },
                        { "Events", ("VanillaEvents", "events", "events") },
                        { "Economy", ("VanillaEconomy", "economy", "economy") },
                        { "Globals", ("VanillaGlobals", "globals", "globals") },
                        { "SpawnableTypes", ("VanillaSpawnableTypes", "spawnabletypes", "spawnabletypes") },
                        { "Messages", ("VanillaMessages", "messages", "messages") },
                        { "RandomPresets", ("VanillaRandomPresets", "randompresets", "randompresets") },
                    };

                if (configMap.TryGetValue(name, out var configEntry) &&
                    _paths.TryGetValue(configEntry.VanillaKey, out vanillaPath))
                {
                    modPaths = eonomyCoreConfig?.GetModdedPaths(configEntry.ModdedPathKey) ?? new List<string>();
                }
                else
                {
                    throw new InvalidOperationException($"Unknown advanced config type '{name}' or missing path.");
                }

                advanced.LoadWithParameters(vanillaPath, modPaths);
            }
            else
            {
                config.Load();
            }

            if (config.HasErrors)
            {
                HasErrors = true;
                Errors.AddRange(config.Errors.Select(e => $"[{name}] {e}"));
            }
        }
        private void LoadFiles(string basePath)
        {
            Console.WriteLine($"[Load Economy] Loading all base economy files associated with the current Project.");

            eonomyCoreConfig = new economyCoreConfig(_paths["cfgeconomycore"]);
            LoadConfigWithErrorReport("cfgeconomycore", eonomyCoreConfig);

            cfglimitsdefinitionConfig = new cfglimitsdefinitionConfig(_paths["cfglimitsdefinition"]);
            LoadConfigWithErrorReport("cfglimitsdefinition", cfglimitsdefinitionConfig);

            cfglimitsdefinitionuserConfig = new cfglimitsdefinitionuserConfig(_paths["cfglimitsdefinitionuser"]);
            LoadConfigWithErrorReport("cfglimitsdefinitionuser", cfglimitsdefinitionuserConfig);

            cfgenvironmentConfig = new cfgenvironmentConfig(_paths["cfgenvironment"]);
            LoadConfigWithErrorReport("cfgenvironment", cfgenvironmentConfig);

            CFGGameplayConfig = new CFGGameplayConfig(_paths["CFGGameplay"]);
            LoadConfigWithErrorReport("CFGGameplay", CFGGameplayConfig);

            CFGGameplayConfig = new CFGGameplayConfig(_paths["CFGGameplay"]);
            LoadConfigWithErrorReport("CFGGameplay", CFGGameplayConfig);

            cfgeffectareaConfig = new cfgeffectareaConfig(_paths["cfgeffectarea"]);
            LoadConfigWithErrorReport("cfgeffectarea", cfgeffectareaConfig);

            cfgundergroundtriggersConfig = new cfgundergroundtriggersConfig(_paths["cfgundergroundtriggers"]);
            LoadConfigWithErrorReport("cfgundergroundtriggers", cfgundergroundtriggersConfig);

            Console.WriteLine($"\n**** Starting load of EconomyCore Files Including Vanilla ****");

            economyConfig = new economyConfig();
            LoadConfigWithErrorReport("Economy", economyConfig);

            globalsConfig = new globalsConfig();
            LoadConfigWithErrorReport("Globals", globalsConfig);

            TypesConfig = new TypesConfig();
            LoadConfigWithErrorReport("Types", TypesConfig);

            eventsConfig = new eventsConfig();
            LoadConfigWithErrorReport("Events", eventsConfig);

            cfgspawnabletypesConfig = new cfgspawnabletypesConfig();
            LoadConfigWithErrorReport("SpawnableTypes", cfgspawnabletypesConfig);

            cfgrandompresetsConfig = new cfgrandompresetsConfig();
            LoadConfigWithErrorReport("RandomPresets", cfgrandompresetsConfig);
        }
    }
}
