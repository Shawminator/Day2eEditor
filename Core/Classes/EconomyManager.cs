
using System.Collections.Generic;
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;

namespace Day2eEditor
{
    public class EconomyManager
    {
        private readonly Dictionary<string, string> _paths = new();
        public string basePath { get; set; }
        public bool HasErrors { get; set; }
        public List<string> Errors = new List<string>();
        public BindingList<EconomyWarning> WarningList { get; } = new();

        public economyCoreConfig eonomyCoreConfig { get; set; }
        public cfglimitsdefinitionConfig cfglimitsdefinitionConfig { get; set; }
        public cfglimitsdefinitionuserConfig cfglimitsdefinitionuserConfig { get; set; }
        public CfgenvironmentConfig cfgenvironmentConfig { get; set; }
        public cfgeventspawnsConfig cfgeventspawnsConfig { get; set; }
        public cfgeventgroupsConfig cfgeventgroupsConfig { get; set; }

        public EconomyConfig economyConfig { get; set; }
        public GlobalsConfig globalsConfig { get; set; }
        public TypesConfig TypesConfig { get; set; }
        public EventsConfig eventsConfig { get; set; }
        public CfgSpawnableTypesConfig cfgspawnabletypesConfig { get; set; }
        public CfgrandompresetsConfig cfgrandompresetsConfig { get; set; }

        public CFGGameplayConfig CFGGameplayConfig { get; set; }
        public CfgeffectareaConfig cfgeffectareaConfig { get; set; }
        public cfgundergroundtriggersConfig cfgundergroundtriggersConfig { get; set; }
        public cfgplayerspawnpointsConfig cfgplayerspawnpointsConfig { get; set; }
        public cfgweatherConfig cfgweatherConfig { get; set; }
        public cfgignorelistConfig cfgignorelistConfig { get; set; }
        public mapgroupposConfig mapgroupposConfig { get; set; }
        public TerritoriesConfig territoriesConfig { get; set; }
        public mapgroupprotoConfig mapgroupprotoConfig { get; set; }
        
        public EconomyManager() 
        {
        }
        public void SetProject(Project project)
        {
            basePath = Path.Combine(project.ProjectRoot, "mpmissions", project.MpMissionPath);

            _paths["cfgeconomycore"] = Path.Combine(basePath, "cfgeconomycore.xml");
            _paths["cfglimitsdefinition"] = Path.Combine(basePath, "cfglimitsdefinition.xml");
            _paths["cfglimitsdefinitionuser"] = Path.Combine(basePath, "cfglimitsdefinitionuser.xml");
            _paths["cfgenvironment"] = Path.Combine(basePath, "cfgenvironment.xml");
            _paths["territoriesConfig"] = Path.Combine(basePath, "");
            _paths["cfgeventspawns"] = Path.Combine(basePath, "cfgeventspawns.xml");
            _paths["cfgeventgroups"] = Path.Combine(basePath, "cfgeventgroups.xml");
            _paths["CFGGameplay"] = Path.Combine(basePath, "cfggameplay.json");
            _paths["cfgeffectarea"] = Path.Combine(basePath, "cfgeffectarea.json");
            _paths["cfgundergroundtriggers"] = Path.Combine(basePath, "cfgundergroundtriggers.json");
            _paths["cfgplayerspawnpoints"] = Path.Combine(basePath, "cfgplayerspawnpoints.xml");
            _paths["cfgweather"] = Path.Combine(basePath, "cfgweather.xml");
            _paths["cfgignorelist"] = Path.Combine(basePath, "cfgignorelist.xml"); 
            _paths["mapgrouppos"] = Path.Combine(basePath, "mapgrouppos.xml");
            _paths["mapgroupproto"] = Path.Combine(basePath, "mapgroupproto.xml");

            _paths["VanillaTypes"] = Path.Combine(basePath, "db", "types.xml");
            _paths["VanillaEvents"] = Path.Combine(basePath, "db", "events.xml");
            _paths["VanillaEconomy"] = Path.Combine(basePath, "db", "economy.xml");
            _paths["VanillaGlobals"] = Path.Combine(basePath, "db", "globals.xml");
            _paths["VanillaSpawnableTypes"] = Path.Combine(basePath, "cfgspawnabletypes.xml");
            _paths["VanillaRandomPresets"] = Path.Combine(basePath, "cfgrandompresets.xml");

            LoadFiles();
        }
        private void LoadConfigWithErrorReport(string name, IConfigLoader config)
        {
            if (config is IParameterizedConfigLoader parameterized)
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

                parameterized.Load(basePath, vanillaPath, modPaths);
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
        private void LoadFiles()
        {
            Console.WriteLine($"\n[Economy Manager] Loading all base economy files associated with the current Project.");

            eonomyCoreConfig = new economyCoreConfig(_paths["cfgeconomycore"]);
            LoadConfigWithErrorReport("cfgeconomycore", eonomyCoreConfig);

            cfglimitsdefinitionConfig = new cfglimitsdefinitionConfig(_paths["cfglimitsdefinition"]);
            LoadConfigWithErrorReport("cfglimitsdefinition", cfglimitsdefinitionConfig);

            cfglimitsdefinitionuserConfig = new cfglimitsdefinitionuserConfig(_paths["cfglimitsdefinitionuser"]);
            LoadConfigWithErrorReport("cfglimitsdefinitionuser", cfglimitsdefinitionuserConfig);

            cfgenvironmentConfig = new CfgenvironmentConfig(_paths["cfgenvironment"]);
            LoadConfigWithErrorReport("cfgenvironment", cfgenvironmentConfig);

            territoriesConfig = new TerritoriesConfig(_paths["territoriesConfig"]);
            LoadConfigWithErrorReport("territoriesConfig", territoriesConfig);

            cfgeventspawnsConfig = new cfgeventspawnsConfig(_paths["cfgeventspawns"]);
            LoadConfigWithErrorReport("cfgeventspawns", cfgeventspawnsConfig);

            cfgeventgroupsConfig = new cfgeventgroupsConfig(_paths["cfgeventgroups"]);
            LoadConfigWithErrorReport("cfgeventgroups", cfgeventgroupsConfig);

            CFGGameplayConfig = new CFGGameplayConfig(_paths["CFGGameplay"]);
            LoadConfigWithErrorReport("CFGGameplay", CFGGameplayConfig);

            cfgeffectareaConfig = new CfgeffectareaConfig(_paths["cfgeffectarea"]);
            LoadConfigWithErrorReport("cfgeffectarea", cfgeffectareaConfig);

            cfgundergroundtriggersConfig = new cfgundergroundtriggersConfig(_paths["cfgundergroundtriggers"]);
            LoadConfigWithErrorReport("cfgundergroundtriggers", cfgundergroundtriggersConfig);

            cfgplayerspawnpointsConfig = new cfgplayerspawnpointsConfig(_paths["cfgplayerspawnpoints"]);
            LoadConfigWithErrorReport("cfgplayerspawnpoints", cfgplayerspawnpointsConfig);

            cfgweatherConfig = new cfgweatherConfig(_paths["cfgweather"]);
            LoadConfigWithErrorReport("cfgweather", cfgweatherConfig);

            cfgignorelistConfig = new cfgignorelistConfig(_paths["cfgignorelist"]);
            LoadConfigWithErrorReport("cfgignorelist", cfgignorelistConfig);

            mapgroupposConfig = new mapgroupposConfig(_paths["mapgrouppos"]);
            LoadConfigWithErrorReport("mapgrouppos", mapgroupposConfig);

            mapgroupprotoConfig = new mapgroupprotoConfig(_paths["mapgroupproto"]);
            LoadConfigWithErrorReport("mapgroupproto", mapgroupprotoConfig);

            Console.WriteLine($"\n**** Starting load of EconomyCore Files Including Vanilla ****");

            economyConfig = new EconomyConfig(basePath);
            LoadConfigWithErrorReport("Economy", economyConfig);

            globalsConfig = new GlobalsConfig(basePath);
            LoadConfigWithErrorReport("Globals", globalsConfig);

            TypesConfig = new TypesConfig(basePath);
            LoadConfigWithErrorReport("Types", TypesConfig);

            eventsConfig = new EventsConfig(basePath);
            LoadConfigWithErrorReport("Events", eventsConfig);

            cfgspawnabletypesConfig = new CfgSpawnableTypesConfig(basePath);
            LoadConfigWithErrorReport("SpawnableTypes", cfgspawnabletypesConfig);

            cfgrandompresetsConfig = new CfgrandompresetsConfig(basePath);
            LoadConfigWithErrorReport("RandomPresets", cfgrandompresetsConfig);

            Save();

            RebuildWarnings();
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
                cfgweatherConfig,
                cfgignorelistConfig,
                mapgroupposConfig,
                territoriesConfig,
                mapgroupprotoConfig
            };

            var savedFiles = new List<string>();
            var projectManager = AppServices.GetRequired<ProjectManager>();
            var uploadTracker = AppServices.GetRequired<UploadTrackerService>();
            foreach (var obj in configs)
            {
                if (obj is IConfigLoader config)
                {
                    IEnumerable<string> savedfiles = config.Save();
                    if (savedfiles.Count() > 0)
                    {
                        savedFiles.AddRange(savedfiles);
                        uploadTracker.MarkForUpload(projectManager.CurrentProject.ProjectName, savedfiles);
                    }
                }
            }
            savedFiles.AddRange(DeleteEmptyDirectoriesFromPath(basePath));
            RebuildWarnings();
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
                cfgrandompresetsConfig,
                cfgplayerspawnpointsConfig,
                cfgweatherConfig,
                cfgignorelistConfig,
                mapgroupposConfig,
                territoriesConfig,
                mapgroupprotoConfig
            };
            foreach (var obj in configs)
            {
                if (obj is not IConfigLoader config)
                    continue;
                if (config.NeedToSave())
                    needtosave = true;
            }
            return needtosave;
        }
        private List<string> DeleteEmptyDirectoriesFromPath(string rootPath)
        {
            var removedFolders = new List<string>();

            if (!Directory.Exists(rootPath))
                return removedFolders;

            var directories = Directory
                .GetDirectories(rootPath, "*", SearchOption.AllDirectories)
                .OrderByDescending(d => d.Count(c =>
                    c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar))
                .ToList();

            foreach (var dir in directories)
            {
                if (!Directory.EnumerateFileSystemEntries(dir).Any())
                {
                    Directory.Delete(dir);
                    var relativePath = Path.GetRelativePath(rootPath, dir);
                    removedFolders.Add("Empty Folder Removed " + relativePath);
                }
            }

            return removedFolders;
        }
        public void CheckallTypes(string uuname, string name)
        {
            foreach (TypesFile ft in TypesConfig.MutableItems)
            {
                foreach (TypeEntry type in ft.Data.TypeList)
                {
                    Usage typeusage = type.Usages.FirstOrDefault(x => x.User == uuname);
                    if (typeusage != null)
                    {
                        typeusage.User = name;
                        ft.IsDirty = true;
                    }
                }
            }
        }
        public void CheckallTypes(string uuname)
        {
            foreach (TypesFile ft in TypesConfig.MutableItems)
            {
                foreach (TypeEntry type in ft.Data.TypeList)
                {
                    Usage typeusage = type.Usages.FirstOrDefault(x => x.User == uuname);
                    if (typeusage != null)
                    {
                        type.Usages.Remove(typeusage);
                        ft.IsDirty = true;
                    }
                }
            }
        }
        public void CheckallTypesValues(string uuname)
        {
            foreach (TypesFile ft in TypesConfig.MutableItems)
            {
                foreach (TypeEntry type in ft.Data.TypeList)
                {
                    Value typevalue = type.Values.FirstOrDefault(x => x.User == uuname);
                    if (typevalue != null)
                    {
                        type.Values.Remove(typevalue);
                        ft.IsDirty = true;
                    }
                }
            }
        }
        public void CheckallTypesValues(string uuname, string name)
        {
            foreach (TypesFile ft in TypesConfig.MutableItems)
            {
                foreach (TypeEntry type in ft.Data.TypeList)
                {
                    Value typevalue = type.Values.FirstOrDefault(x => x.User == uuname);
                    if (typevalue != null)
                    {
                        typevalue.User = name;
                        ft.IsDirty = true;
                    }
                }
            }
        }
        public void SetExternalFiles()
        {
            Console.WriteLine("[Economy Manager] Checking External Data Files");
            checkVanillaSlotNames();
            checkVanillaCharacterClassnames();
        }



        #region Warning
        public void ClearWarnings()
        {
            WarningList.Clear();
        }
        public void AddWarning(EconomyWarning warning)
        {
            if (warning == null)
                return;

            if (string.IsNullOrWhiteSpace(warning.Group))
                warning.Group = EconomyWarning.GetWarningGroup(warning.Code);

            if (string.IsNullOrWhiteSpace(warning.Key))
                warning.Key = $"{warning.Code}|{warning.SourceFile}|{warning.Message}";

            bool exists = WarningList.Any(w => string.Equals(w.Key, warning.Key, StringComparison.OrdinalIgnoreCase));
            if (!exists)
                WarningList.Add(warning);
        }
        public void RebuildWarnings()
        {
            ClearWarnings();

            CheckDuplicateTypes();
            CheckDuplicateEvents();
            //CheckDuplicateSpawnables();
            //CheckDuplicateRandomPresets();

            CheckEmptyFiles();

            CheckTypeEntryWarnings();
            //CheckMissingEventSpawns();
            //CheckMissingEventGroups();
            CheckMissingTerritoryLinks();
            //CheckMissingObjectSpawnerFiles();

            CheckUnusedEventGroups();
            //CheckUnusedRandomPresets();
            //CheckUnusedSpawnableTypes();
            //CheckUnusedTerritoryFiles();

            CheckCeRegistrations();
        }

        private void CheckDuplicateTypes()
        {
            foreach (var file in TypesConfig.MutableItems)
            {
                if (file.Data?.TypeList == null || file.Data.TypeList.Count == 0)
                    continue;

                var duplicates = file.Data.TypeList
                    .GroupBy(t => t.Name, StringComparer.OrdinalIgnoreCase)
                    .Where(g => g.Count() > 1);

                foreach (var group in duplicates)
                {
                    foreach (var item in group)
                    {
                        AddWarning(new EconomyWarning
                        {
                            Code = WarningCode.DuplicateTypeName,
                            Title = $"Duplicate type in {file.FileName}: {group.Key}",
                            Message = $"Type '{group.Key}' appears multiple times in '{file.FileName}'.",
                            SourceObject = item,
                            RelatedObject = file,
                            SourceFile = file.FilePath,
                            Key = $"DuplicateTypeName|{file.FilePath}|{group.Key}"
                        });
                    }
                }
            }
        }
        private void CheckDuplicateEvents()
        {
            foreach (var file in eventsConfig.MutableItems)
            {
                if (file.Data?.@event == null || file.Data.@event.Count == 0)
                    continue;

                var duplicates = file.Data.@event
                    .GroupBy(e => e.name, StringComparer.OrdinalIgnoreCase)
                    .Where(g => g.Count() > 1);

                foreach (var group in duplicates)
                {
                    AddWarning(new EconomyWarning
                    {
                        Code = WarningCode.DuplicateEventName,
                        Title = $"Duplicate event in {file.FileName}: {group.Key}",
                        Message = $"Event '{group.Key}' appears {group.Count()} times in '{file.FileName}'.",
                        SourceObject = file, // cleaner than per-item spam
                        SourceFile = file.FilePath,
                        Key = $"DuplicateEventName|{file.FilePath}|{group.Key}"
                    });
                }
            }
        }
        private void CheckEmptyFiles()
        {
            foreach (var file in TypesConfig.MutableItems)
            {
                if (file.Data?.TypeList == null || file.Data.TypeList.Count == 0)
                {
                    AddWarning(new EconomyWarning
                    {
                        Code = WarningCode.EmptyTypesFile,
                        Title = $"Empty types file: {file.FileName}",
                        Message = $"'{file.FileName}' contains no types.",
                        SourceObject = file,
                        SourceFile = file.FilePath,
                        Key = $"EmptyTypesFile|{file.FilePath}"
                    });
                }
            }

            foreach (var file in eventsConfig.MutableItems)
            {
                if (file.Data?.@event == null || file.Data.@event.Count == 0)
                {
                    AddWarning(new EconomyWarning
                    {
                        Code = WarningCode.EmptyEventsFile,
                        Title = $"Empty events file: {file.FileName}",
                        Message = $"'{file.FileName}' contains no events.",
                        SourceObject = file,
                        SourceFile = file.FilePath,
                        Key = $"EmptyEventsFile|{file.FilePath}"
                    });
                }
            }
        }
        private void CheckTypeEntryWarnings()
        {
            HashSet<string> validCategories = GetValidCategoryNames();
            HashSet<string> validUsageFlags = GetValidUsageFlagNames();
            HashSet<string> validValueFlags = GetValidValueFlagNames();
            HashSet<string> validTagNames = GetValidTagNames();

            foreach (var file in TypesConfig.MutableItems)
            {
                if (file.Data?.TypeList == null)
                    continue;

                foreach (var type in file.Data.TypeList)
                {
                    CheckSingleTypeEntry(file, type, validCategories, validUsageFlags, validValueFlags, validTagNames);
                }
            }
        }
        private void CheckSingleTypeEntry(
    TypesFile file,
    TypeEntry type,
    HashSet<string> validCategories,
    HashSet<string> validUsageFlags,
    HashSet<string> validValueFlags,
    HashSet<string> validTagNames)
        {
            if (type == null)
                return;

            string typeName = string.IsNullOrWhiteSpace(type.Name) ? "<unnamed>" : type.Name;

            // min > nominal
            if (type.Min > type.Nominal)
            {
                AddWarning(new EconomyWarning
                {
                    Code = WarningCode.InvalidTypeEntry,
                    Title = $"Invalid nominal/min in {file.FileName}: {typeName}",
                    Message = $"Type '{typeName}' has min ({type.Min}) greater than nominal ({type.Nominal}).",
                    SourceObject = type,
                    RelatedObject = file,
                    SourceFile = file.FilePath,
                    Key = $"InvalidTypeEntry|MinGreaterThanNominal|{file.FilePath}|{typeName}"
                });
            }

            // quant min/max
            if (type.QuantMin > type.QuantMax)
            {
                AddWarning(new EconomyWarning
                {
                    Code = WarningCode.InvalidTypeEntry,
                    Title = $"Invalid quantity range in {file.FileName}: {typeName}",
                    Message = $"Type '{typeName}' has quantmin ({type.QuantMin}) greater than quantmax ({type.QuantMax}).",
                    SourceObject = type,
                    RelatedObject = file,
                    SourceFile = file.FilePath,
                    Key = $"InvalidTypeEntry|QuantRange|{file.FilePath}|{typeName}"
                });
            }

            // category
            if (type.Category != null && !string.IsNullOrWhiteSpace(type.Category.Name))
            {
                if (!validCategories.Contains(type.Category.Name))
                {
                    AddWarning(new EconomyWarning
                    {
                        Code = WarningCode.UnknownCategory,
                        Title = $"Unknown category in {file.FileName}: {typeName}",
                        Message = $"Type '{typeName}' uses category '{type.Category.Name}' which is not defined in limits definitions.",
                        SourceObject = type,
                        RelatedObject = file,
                        SourceFile = file.FilePath,
                        Key = $"UnknownCategory|{file.FilePath}|{typeName}|{type.Category.Name}"
                    });
                }
            }

            // usage flags
            if (type.Usages != null)
            {
                foreach (var usage in type.Usages)
                {
                    if (string.IsNullOrWhiteSpace(usage.Name))
                        continue;

                    if (!validUsageFlags.Contains(usage.Name))
                    {
                        AddWarning(new EconomyWarning
                        {
                            Code = WarningCode.UnknownUsageFlag,
                            Title = $"Unknown usage flag in {file.FileName}: {typeName}",
                            Message = $"Type '{typeName}' references usage '{usage.Name}' which is not defined in limits definitions.",
                            SourceObject = type,
                            RelatedObject = usage,
                            SourceFile = file.FilePath,
                            Key = $"UnknownUsageFlag|{file.FilePath}|{typeName}|{usage.Name}"
                        });
                    }
                }
            }

            // value flags
            if (type.Values != null)
            {
                foreach (var value in type.Values)
                {
                    if (string.IsNullOrWhiteSpace(value.Name))
                        continue;

                    if (!validValueFlags.Contains(value.Name))
                    {
                        AddWarning(new EconomyWarning
                        {
                            Code = WarningCode.UnknownValueFlag,
                            Title = $"Unknown value flag in {file.FileName}: {typeName}",
                            Message = $"Type '{typeName}' references value '{value.Name}' which is not defined in limits definitions.",
                            SourceObject = type,
                            RelatedObject = value,
                            SourceFile = file.FilePath,
                            Key = $"UnknownValueFlag|{file.FilePath}|{typeName}|{value.Name}"
                        });
                    }
                }
            }

            // tags
            if (type.Tags != null)
            {
                foreach (var tag in type.Tags)
                {
                    if (string.IsNullOrWhiteSpace(tag.Name))
                        continue;

                    if (!validTagNames.Contains(tag.Name))
                    {
                        AddWarning(new EconomyWarning
                        {
                            Code = WarningCode.UnknownTag,
                            Title = $"Unknown tag in {file.FileName}: {typeName}",
                            Message = $"Type '{typeName}' references tag '{tag.Name}' which is not defined in limits definitions.",
                            SourceObject = type,
                            RelatedObject = tag,
                            SourceFile = file.FilePath,
                            Key = $"UnknownTag|{file.FilePath}|{typeName}|{tag.Name}"
                        });
                    }
                }
            }
        }
        private HashSet<string> GetValidCategoryNames()
        {
            var result = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            if (cfglimitsdefinitionConfig?.Data?.categories != null)
            {
                foreach (var item in cfglimitsdefinitionConfig.Data.categories)
                    result.Add(item.name);
            }

            return result;
        }
        private HashSet<string> GetValidUsageFlagNames()
        {
            var result = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            if (cfglimitsdefinitionConfig?.Data?.usageflags != null)
            {
                foreach (var item in cfglimitsdefinitionConfig.Data.usageflags)
                    result.Add(item.name);
            }

            if (cfglimitsdefinitionuserConfig?.Data?.usageflags != null)
            {
                foreach (var item in cfglimitsdefinitionuserConfig.Data.usageflags)
                    result.Add(item.name);
            }

            return result;
        }
        private HashSet<string> GetValidValueFlagNames()
        {
            var result = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            if (cfglimitsdefinitionConfig?.Data?.valueflags != null)
            {
                foreach (var item in cfglimitsdefinitionConfig.Data.valueflags)
                    result.Add(item.name);
            }

            if (cfglimitsdefinitionuserConfig?.Data?.valueflags != null)
            {
                foreach (var item in cfglimitsdefinitionuserConfig.Data.valueflags)
                    result.Add(item.name);
            }

            return result;
        }
        private HashSet<string> GetValidTagNames()
        {
            var result = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            if (cfglimitsdefinitionConfig?.Data?.tags != null)
            {
                foreach (var item in cfglimitsdefinitionConfig.Data.tags)
                    result.Add(item.name);
            }

            return result;
        }
        private void CheckUnusedEventGroups()
        {
            var usedGroups = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var evtSpawn in cfgeventspawnsConfig.Data.@event)
            {
                if (evtSpawn.pos == null)
                    continue;

                foreach (var pos in evtSpawn.pos)
                {
                    if (!string.IsNullOrWhiteSpace(pos.group))
                        usedGroups.Add(pos.group);
                }
            }

            foreach (var group in cfgeventgroupsConfig.Data.group)
            {
                if (!usedGroups.Contains(group.name))
                {
                    AddWarning(new EconomyWarning
                    {
                        Code = WarningCode.UnusedEventGroup,
                        Title = $"Unused event group: {group.name}",
                        Message = $"Event group '{group.name}' is not referenced by any event spawn position.",
                        SourceObject = group,
                        SourceFile = cfgeventgroupsConfig.FilePath,
                        Key = $"UnusedEventGroup|{group.name}"
                    });
                }
            }
        }
        private void CheckMissingTerritoryLinks()
        {
            if (cfgenvironmentConfig?.Data?.territories?.territory == null)
                return;

            foreach (var territoryRef in cfgenvironmentConfig.Data.territories.territory)
            {
                var usable = cfgenvironmentConfig.Data.territories.GetUsableFile(territoryRef.file.usable);

                if (usable == null)
                {
                    AddWarning(new EconomyWarning
                    {
                        Code = WarningCode.MissingTerritoryFile,
                        Title = $"Missing usable territory file for {territoryRef.name}",
                        Message = $"Territory '{territoryRef.name}' does not resolve to a usable file in cfgenvironment.xml.",
                        SourceObject = territoryRef,
                        SourceFile = cfgenvironmentConfig.FilePath,
                        Key = $"MissingTerritoryFile|{territoryRef.name}|unresolved"
                    });
                    continue;
                }

                bool exists = territoriesConfig.MutableItems.Any(t =>
                    string.Equals(Path.GetFileName(t.FilePath), Path.GetFileName(usable.path), StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(t.FileName, Path.GetFileName(usable.path), StringComparison.OrdinalIgnoreCase));

                if (!exists)
                {
                    AddWarning(new EconomyWarning
                    {
                        Code = WarningCode.MissingTerritoryFile,
                        Title = $"Missing territory file: {usable.path}",
                        Message = $"Territory '{territoryRef.name}' points to '{usable.path}', but no loaded territory file matches it.",
                        SourceObject = territoryRef,
                        RelatedFile = usable.path,
                        SourceFile = cfgenvironmentConfig.FilePath,
                        Key = $"MissingTerritoryFile|{territoryRef.name}|{usable.path}"
                    });
                }
            }
        }
        private void CheckCeRegistrations()
        {
            var registered = new HashSet<string>(
                eonomyCoreConfig.Data.ce
                    .SelectMany(ce => ce.file.Select(f => NormalizeCeKey(ce.folder, f.name))),
                StringComparer.OrdinalIgnoreCase);

            foreach (var file in TypesConfig.MutableItems)
            {
                if (!file.IsModded)
                    continue;

                var key = NormalizeCeKey(file.ModFolder, file.FileName);
                if (!registered.Contains(key))
                {
                    AddWarning(new EconomyWarning
                    {
                        Code = WarningCode.FileExistsButNotRegistered,
                        Title = $"Unregistered CE file: {file.FileName}",
                        Message = $"Custom file '{file.FileName}' exists but is not registered in cfgeconomycore.xml.",
                        SourceObject = file,
                        SourceFile = file.FilePath,
                        Key = $"FileExistsButNotRegistered|{key}"
                    });
                }
            }
        }
        private string NormalizeCeKey(string folder, string name)
        {
            folder ??= string.Empty;
            name ??= string.Empty;

            string combined = Path.Combine(folder, name)
                .Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar)
                .Trim();

            return combined;
        }
        #endregion

        private void checkVanillaCharacterClassnames()
        {
            string filePath = "Data\\VanillaCharacterClassnames.txt";

            // Ensure the directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            List<string> fileCharacterClassnames = new List<string>();

            if (File.Exists(filePath))
            {
                fileCharacterClassnames = File.ReadAllLines(filePath).ToList();
            }

            // Add any missing entries from the static list
            bool updated = false;
            foreach (string slot in VanillaCharacterClassnames)
            {
                if (!fileCharacterClassnames.Contains(slot))
                {
                    fileCharacterClassnames.Add(slot);
                    updated = true;
                }
            }

            // If there were updates, write back to the file
            if (updated || !File.Exists(filePath))
            {
                File.WriteAllLines(filePath, fileCharacterClassnames);
            }
        }
        private void checkVanillaSlotNames()
        {
            string filePath = "Data\\VanillaSlotNames.txt";

            // Ensure the directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            List<string> fileSlotNames = new List<string>();

            if (File.Exists(filePath))
            {
                fileSlotNames = File.ReadAllLines(filePath).ToList();
            }

            // Add any missing entries from the static list
            bool updated = false;
            foreach (string slot in VanillaSlotNames)
            {
                if (!fileSlotNames.Contains(slot))
                {
                    fileSlotNames.Add(slot);
                    updated = true;
                }
            }

            // If there were updates, write back to the file
            if (updated || !File.Exists(filePath))
            {
                File.WriteAllLines(filePath, fileSlotNames);
            }

        }
        static List<string> VanillaSlotNames = new List<string>()
        {
            "Armband",
            "Back",
            "Body",
            "Cargo",
            "Eyewear",
            "Feet",
            "Gloves",
            "Hands",
            "Headgear",
            "Hips",
            "LeftHand",
            "Legs",
            "Mask",
            "shoulderL",
            "shoulderR",
            "Vest",
        };
        static List<string> VanillaCharacterClassnames = new List<string>()
        {
            "SurvivorF_Baty",
            "SurvivorF_Eva",
            "SurvivorF_Frida",
            "SurvivorF_Gabi",
            "SurvivorF_Helga",
            "SurvivorF_Irena",
            "SurvivorF_Judy",
            "SurvivorF_Keiko",
            "SurvivorF_Linda",
            "SurvivorF_Maria",
            "SurvivorF_Naomi",
            "SurvivorM_Denis",
            "SurvivorM_Boris",
            "SurvivorM_Cyril",
            "SurvivorM_Elias",
            "SurvivorM_Francis",
            "SurvivorM_Guo",
            "SurvivorM_Hassan",
            "SurvivorM_Indar",
            "SurvivorM_Jose",
            "SurvivorM_Kaito",
            "SurvivorM_Lewis",
            "SurvivorM_Manua",
            "SurvivorM_Mirek",
            "SurvivorM_Niki",
            "SurvivorM_Oliver",
            "SurvivorM_Peter",
            "SurvivorM_Quinn",
            "SurvivorM_Rolf",
            "SurvivorM_Seth",
            "SurvivorM_Taiki"
        };
    }
}
