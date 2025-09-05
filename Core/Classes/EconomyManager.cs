using DayZeLib;
using System.Collections.Generic;

namespace Day2eEditor
{
    public interface IConfigLoader
    {
        bool HasErrors { get; }
        public List<string> Errors { get; }

        void Load();
        IEnumerable<string> Save();
        bool needToSave();
    }
    public interface IAdvancedConfigLoader : IConfigLoader
    {
        void Load();
        void LoadWithParameters(string basePath, string vanillaPath, List<string> modPaths);
        IEnumerable<string> Save();
        public bool needToSave()
        {
            return false;
        }
    }
    public class EconomyManager
    {
        private readonly Dictionary<string, string> _paths = new();
        private readonly ProjectManager _projectManager;

        public string basePath { get; set; }

        public bool HasErrors { get; set; }
        public List<string> Errors = new List<string>();

        public economyCoreConfig eonomyCoreConfig { get; set; }
        public cfglimitsdefinitionConfig cfglimitsdefinitionConfig { get; set; }
        public cfglimitsdefinitionuserConfig cfglimitsdefinitionuserConfig { get; set; }
        public cfgenvironmentConfig cfgenvironmentConfig { get; set; }
        public cfgeventspawnsConfig cfgeventspawnsConfig { get; set; }
        public cfgeventgroupsConfig cfgeventgroupsConfig { get; set; }

        public economyConfig economyConfig { get; set; }
        public globalsConfig globalsConfig { get; set; }
        public TypesConfig TypesConfig { get; set; }
        public eventsConfig eventsConfig { get; set; }
        public cfgspawnabletypesConfig cfgspawnabletypesConfig { get; set; }
        public cfgrandompresetsConfig cfgrandompresetsConfig { get; set; }

        public CFGGameplayConfig CFGGameplayConfig { get; set; }
        public cfgeffectareaConfig cfgeffectareaConfig { get; set; }
        public cfgundergroundtriggersConfig cfgundergroundtriggersConfig { get; set; }
        public cfgplayerspawnpointsConfig cfgplayerspawnpointsConfig { get; set; }
        public cfgweatherConfig cfgweatherConfig { get; set; }

        //public cfgignorelist cfgignorelist { get; set; }

        //public mapgroupproto mapgroupproto { get; set; }
        //public mapgrouppos mapgrouppos { get; set; }
        //public BindingList<territoriesConfig> territoriesList;

        public EconomyManager() 
        {
            _projectManager = AppServices.GetRequired<ProjectManager>();
        }
        public void SetProject(Project project)
        {
            basePath = Path.Combine(project.ProjectRoot, "mpmissions", project.MpMissionPath);

            _paths["cfgeconomycore"] = Path.Combine(basePath, "cfgeconomycore.xml");
            _paths["cfglimitsdefinition"] = Path.Combine(basePath, "cfglimitsdefinition.xml");
            _paths["cfglimitsdefinitionuser"] = Path.Combine(basePath, "cfglimitsdefinitionuser.xml");
            _paths["cfgenvironment"] = Path.Combine(basePath, "cfgenvironment.xml");
            _paths["cfgeventspawns"] = Path.Combine(basePath, "cfgeventspawns.xml");
            _paths["cfgeventgroups"] = Path.Combine(basePath, "cfgeventgroups.xml");
            _paths["CFGGameplay"] = Path.Combine(basePath, "cfggameplay.json");
            _paths["cfgeffectarea"] = Path.Combine(basePath, "cfgeffectarea.json");
            _paths["cfgundergroundtriggers"] = Path.Combine(basePath, "cfgundergroundtriggers.json");
            _paths["cfgplayerspawnpoints"] = Path.Combine(basePath, "cfgplayerspawnpoints.xml");
            _paths["cfgweather"] = Path.Combine(basePath, "cfgweather.xml");

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

                advanced.LoadWithParameters(basePath, vanillaPath, modPaths);
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
            Console.WriteLine($"\n[Load Economy] Loading all base economy files associated with the current Project.");

            eonomyCoreConfig = new economyCoreConfig(_paths["cfgeconomycore"]);
            LoadConfigWithErrorReport("cfgeconomycore", eonomyCoreConfig);

            cfglimitsdefinitionConfig = new cfglimitsdefinitionConfig(_paths["cfglimitsdefinition"]);
            LoadConfigWithErrorReport("cfglimitsdefinition", cfglimitsdefinitionConfig);

            cfglimitsdefinitionuserConfig = new cfglimitsdefinitionuserConfig(_paths["cfglimitsdefinitionuser"]);
            LoadConfigWithErrorReport("cfglimitsdefinitionuser", cfglimitsdefinitionuserConfig);

            cfgenvironmentConfig = new cfgenvironmentConfig(_paths["cfgenvironment"]);
            LoadConfigWithErrorReport("cfgenvironment", cfgenvironmentConfig);

            cfgeventspawnsConfig = new cfgeventspawnsConfig(_paths["cfgeventspawns"]);
            LoadConfigWithErrorReport("cfgeventspawns", cfgeventspawnsConfig);

            cfgeventgroupsConfig = new cfgeventgroupsConfig(_paths["cfgeventgroups"]);
            LoadConfigWithErrorReport("cfgeventgroups", cfgeventgroupsConfig);

            CFGGameplayConfig = new CFGGameplayConfig(_paths["CFGGameplay"]);
            LoadConfigWithErrorReport("CFGGameplay", CFGGameplayConfig);

            cfgeffectareaConfig = new cfgeffectareaConfig(_paths["cfgeffectarea"]);
            LoadConfigWithErrorReport("cfgeffectarea", cfgeffectareaConfig);

            cfgundergroundtriggersConfig = new cfgundergroundtriggersConfig(_paths["cfgundergroundtriggers"]);
            LoadConfigWithErrorReport("cfgundergroundtriggers", cfgundergroundtriggersConfig);

            cfgplayerspawnpointsConfig = new cfgplayerspawnpointsConfig(_paths["cfgplayerspawnpoints"]);
            LoadConfigWithErrorReport("cfgplayerspawnpoints", cfgplayerspawnpointsConfig);

            cfgweatherConfig = new cfgweatherConfig(_paths["cfgweather"]);
            LoadConfigWithErrorReport("cfgweather", cfgweatherConfig);

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
        public void Reset()
        {

        }
        public IEnumerable<string> Save()
        {
            var configs = new object[]
            {
                eonomyCoreConfig,
                cfglimitsdefinitionConfig,
                cfglimitsdefinitionuserConfig,
                cfgenvironmentConfig,
                cfgeventspawnsConfig,
                cfgeventgroupsConfig,
                CFGGameplayConfig,
                cfgeffectareaConfig,
                cfgundergroundtriggersConfig,
                economyConfig,
                globalsConfig,
                TypesConfig,
                eventsConfig,
                cfgspawnabletypesConfig,
                cfgrandompresetsConfig,
                cfgplayerspawnpointsConfig,
                cfgweatherConfig
            };

            var savedFiles = new List<string>();

            foreach (var obj in configs)
            {
                if (obj is IConfigLoader config)
                {
                    savedFiles.AddRange(config.Save());
                }
            }

            return savedFiles;
        }
        public bool needToSave()
        {
            bool needtosave = false;
            var configs = new object[]
            {
                eonomyCoreConfig,
                cfglimitsdefinitionConfig,
                cfglimitsdefinitionuserConfig,
                cfgenvironmentConfig,
                cfgeventspawnsConfig,
                cfgeventgroupsConfig,
                CFGGameplayConfig,
                cfgeffectareaConfig,
                cfgundergroundtriggersConfig,
                economyConfig,
                globalsConfig,
                TypesConfig,
                eventsConfig,
                cfgspawnabletypesConfig,
                cfgrandompresetsConfig
            };
            foreach (var obj in configs)
            {
                if (obj is not IConfigLoader config)
                    continue;
                if (config.needToSave())
                    needtosave = true;
            }
            return needtosave;
        }

        public void CheckallTypes(string uuname, string name)
        {
            foreach (TypesFile ft in TypesConfig.AllData)
            {
                foreach (TypeEntry type in ft.Data.TypeList)
                {
                    Usage typeusage = type.Usages.FirstOrDefault(x => x.User == uuname);
                    if (typeusage != null)
                    {
                        typeusage.User = name;
                        ft.isDirty = true;
                    }
                }
            }
        }
        public void CheckallTypes(string uuname)
        {
            foreach (TypesFile ft in TypesConfig.AllData)
            {
                foreach (TypeEntry type in ft.Data.TypeList)
                {
                    Usage typeusage = type.Usages.FirstOrDefault(x => x.User == uuname);
                    if (typeusage != null)
                    {
                        type.Usages.Remove(typeusage);
                        ft.isDirty = true;
                    }
                }
            }
        }
        public void CheckallTypesValues(string uuname)
        {
            foreach (TypesFile ft in TypesConfig.AllData)
            {
                foreach (TypeEntry type in ft.Data.TypeList)
                {
                    Value typevalue = type.Values.FirstOrDefault(x => x.User == uuname);
                    if (typevalue != null)
                    {
                        type.Values.Remove(typevalue);
                        ft.isDirty = true;
                    }
                }
            }
        }
        public void CheckallTypesValues(string uuname, string name)
        {
            foreach (TypesFile ft in TypesConfig.AllData)
            {
                foreach (TypeEntry type in ft.Data.TypeList)
                {
                    Value typevalue = type.Values.FirstOrDefault(x => x.User == uuname);
                    if (typevalue != null)
                    {
                        typevalue.User = name;
                        ft.isDirty = true;
                    }
                }
            }
        }
    }
}
