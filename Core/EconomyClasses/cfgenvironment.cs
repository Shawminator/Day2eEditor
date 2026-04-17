using System.ComponentModel;
using System.Xml.Serialization;

namespace Day2eEditor
{
    public class CfgenvironmentConfig : SingleFileConfigLoaderBase<env>
    {
        public CfgenvironmentConfig(string path) : base(path)
        {
        }
        public override void Load()
        {
            HasErrors = false;
            _errors.Clear();

            try
            {

                Data = AppServices.GetRequired<FileService>()
                    .LoadOrCreateXml<env>(
                        _path,
                        createNew: () => new env(),
                        onError: ex =>
                        {
                            HandleLoadError(ex);
                        },
                        configName: "cfgenviroment"
                    );
                var issues = ValidateData();
                if (issues?.Any() == true)
                {
                    Console.WriteLine("Validation issues in " + FileName + ":");
                    foreach (var msg in issues)
                        Console.WriteLine("- " + msg);

                    MarkDirty();
                }
                OnAfterLoad(Data);
                ClonedData = CloneData(Data);
            }
            catch (Exception ex)
            {
                HandleLoadError(ex);
            }
        }
        public override IEnumerable<string> Save()
        {
            if (Data is null)
                return Array.Empty<string>();


            if (!AreEqual(Data, ClonedData) || IsDirty == true)
            {
                ClearDirty();
                AppServices.GetRequired<FileService>().SaveXml(_path, Data);
                ClonedData = CloneData(Data);
                return new[] { Path.GetFileName(_path) };
            }

            return Array.Empty<string>();
        }
        protected override env CreateDefaultData()
        {
            return new env();
        }

        protected override void OnAfterLoad(env data)
        {
            // Optional post-load logic
        }

        protected override IEnumerable<string> ValidateData()
        {
            return Enumerable.Empty<string>();
        }
    }

    [XmlRoot("env")]
    public class env : IEquatable<env>, IDeepCloneable<env>
    {
        private envTerritories? _territories;

        [XmlElement("territories")]
        public envTerritories territories
        {
            get => _territories ??= new envTerritories();
            set => _territories = value;
        }

        public bool Equals(env? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(territories, other.territories);
        }

        public override bool Equals(object? obj) => Equals(obj as env);

        public env Clone()
        {
            return new env
            {
                territories = territories?.Clone() ?? new envTerritories()
            };
        }
    }
    public partial class envTerritories : IEquatable<envTerritories>, IDeepCloneable<envTerritories>
    {
        [XmlElement("file")]
        public BindingList<envTerritoriesFile> file { get; set; } = new();
        [XmlElement("territory")]

        public BindingList<envTerritoriesTerritory> territory { get; set; } = new();

        public envTerritoriesFile? GetUsableFile(string usable)
        {
            return file.FirstOrDefault(x => Path.GetFileNameWithoutExtension(x.path) == usable);
        }

        public bool Equals(envTerritories? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return file.SequenceEqual(other.file) &&
                   territory.SequenceEqual(other.territory);
        }

        public override bool Equals(object? obj) => Equals(obj as envTerritories);

        public envTerritories Clone()
        {
            return new envTerritories
            {
                file = new BindingList<envTerritoriesFile>(file.Select(x => x.Clone()).ToList()),
                territory = new BindingList<envTerritoriesTerritory>(territory.Select(x => x.Clone()).ToList())
            };
        }
    }
    public partial class envTerritoriesFile : IEquatable<envTerritoriesFile>, IDeepCloneable<envTerritoriesFile>
    {
        [XmlAttribute]
        public string? path { get; set; }

        public bool Equals(envTerritoriesFile? other)
        {
            if (other is null) return false;
            return string.Equals(path, other.path, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object? obj) => Equals(obj as envTerritoriesFile);

        public envTerritoriesFile Clone()
        {
            return new envTerritoriesFile
            {
                path = path
            };
        }
    }

    public partial class envTerritoriesTerritory : IEquatable<envTerritoriesTerritory>, IDeepCloneable<envTerritoriesTerritory>
    {
        [XmlElement("file")]
        public envTerritoriesTerritoryFile? file { get; set; }

        [XmlElement("agent")]
        public BindingList<envTerritoriesTerritoryAgent> agent { get; set; } = new();

        [XmlElement("item")]
        public BindingList<envTerritoriesTerritoryItem> item { get; set; } = new();

        [XmlAttribute] public string? type { get; set; }
        [XmlAttribute] public string? name { get; set; }
        [XmlAttribute] public string? behavior { get; set; }

        public bool Equals(envTerritoriesTerritory? other)
        {
            if (other is null) return false;

            return
                Equals(file, other.file) &&
                agent.SequenceEqual(other.agent) &&
                item.SequenceEqual(other.item) &&
                string.Equals(type, other.type, StringComparison.Ordinal) &&
                string.Equals(name, other.name, StringComparison.Ordinal) &&
                string.Equals(behavior, other.behavior, StringComparison.Ordinal);
        }

        public override bool Equals(object? obj) => Equals(obj as envTerritoriesTerritory);

        public envTerritoriesTerritory Clone()
        {
            return new envTerritoriesTerritory
            {
                file = file?.Clone(),
                agent = new BindingList<envTerritoriesTerritoryAgent>(agent.Select(x => x.Clone()).ToList()),
                item = new BindingList<envTerritoriesTerritoryItem>(item.Select(x => x.Clone()).ToList()),
                type = type,
                name = name,
                behavior = behavior
            };
        }
    }

    public partial class envTerritoriesTerritoryFile : IEquatable<envTerritoriesTerritoryFile>, IDeepCloneable<envTerritoriesTerritoryFile>
    {
        [XmlAttribute]
        public string? usable { get; set; }

        public bool Equals(envTerritoriesTerritoryFile? other)
        {
            if (other is null) return false;
            return string.Equals(usable, other.usable, StringComparison.Ordinal);
        }

        public override bool Equals(object? obj) => Equals(obj as envTerritoriesTerritoryFile);

        public envTerritoriesTerritoryFile Clone()
        {
            return new envTerritoriesTerritoryFile
            {
                usable = usable
            };
        }
    }

    public partial class envTerritoriesTerritoryAgent : IEquatable<envTerritoriesTerritoryAgent>, IDeepCloneable<envTerritoriesTerritoryAgent>
    {
        [XmlElement("spawn")]
        public envTerritoriesTerritoryAgentSpawn[]? spawn { get; set; }
        [XmlElement("item")]
        public envTerritoriesTerritoryAgentItem[]? item { get; set; }

        [XmlAttribute] public string? type { get; set; }
        [XmlAttribute] public int chance { get; set; }

        [XmlIgnore] public bool chanceSpecified { get; set; }

        public bool Equals(envTerritoriesTerritoryAgent? other)
        {
            if (other is null) return false;

            return
                (spawn?.SequenceEqual(other.spawn ?? Array.Empty<envTerritoriesTerritoryAgentSpawn>()) ?? other.spawn == null) &&
                (item?.SequenceEqual(other.item ?? Array.Empty<envTerritoriesTerritoryAgentItem>()) ?? other.item == null) &&
                string.Equals(type, other.type, StringComparison.Ordinal) &&
                chance == other.chance &&
                chanceSpecified == other.chanceSpecified;
        }

        public override bool Equals(object? obj) => Equals(obj as envTerritoriesTerritoryAgent);

        public envTerritoriesTerritoryAgent Clone()
        {
            return new envTerritoriesTerritoryAgent
            {
                spawn = spawn?.Select(x => x.Clone()).ToArray(),
                item = item?.Select(x => x.Clone()).ToArray(),
                type = type,
                chance = chance,
                chanceSpecified = chanceSpecified
            };
        }
    }

    public partial class envTerritoriesTerritoryAgentSpawn : IEquatable<envTerritoriesTerritoryAgentSpawn>, IDeepCloneable<envTerritoriesTerritoryAgentSpawn>
    {
        [XmlAttribute] public string? configName { get; set; }
        [XmlAttribute] public int chance { get; set; }

        [XmlIgnore] public bool chanceSpecified { get; set; }

        public bool Equals(envTerritoriesTerritoryAgentSpawn? other)
        {
            if (other is null) return false;

            return
                string.Equals(configName, other.configName, StringComparison.Ordinal) &&
                chance == other.chance &&
                chanceSpecified == other.chanceSpecified;
        }

        public override bool Equals(object? obj) => Equals(obj as envTerritoriesTerritoryAgentSpawn);

        public envTerritoriesTerritoryAgentSpawn Clone()
        {
            return new envTerritoriesTerritoryAgentSpawn
            {
                configName = configName,
                chance = chance,
                chanceSpecified = chanceSpecified
            };
        }
    }

    public partial class envTerritoriesTerritoryAgentItem : IEquatable<envTerritoriesTerritoryAgentItem>, IDeepCloneable<envTerritoriesTerritoryAgentItem>
    {
        [XmlAttribute] public string? name { get; set; }
        [XmlAttribute] public int val { get; set; }

        public bool Equals(envTerritoriesTerritoryAgentItem? other)
        {
            if (other is null) return false;
            return name == other.name && val == other.val;
        }

        public override bool Equals(object? obj) => Equals(obj as envTerritoriesTerritoryAgentItem);

        public envTerritoriesTerritoryAgentItem Clone()
        {
            return new envTerritoriesTerritoryAgentItem { name = name, val = val };
        }
    }

    public partial class envTerritoriesTerritoryItem : IEquatable<envTerritoriesTerritoryItem>, IDeepCloneable<envTerritoriesTerritoryItem>
    {
        [XmlAttribute] public string? name { get; set; }
        [XmlAttribute] public int val { get; set; }

        public bool Equals(envTerritoriesTerritoryItem? other)
        {
            if (other is null) return false;
            return name == other.name && val == other.val;
        }

        public override bool Equals(object? obj) => Equals(obj as envTerritoriesTerritoryItem);

        public envTerritoriesTerritoryItem Clone()
        {
            return new envTerritoriesTerritoryItem { name = name, val = val };
        }
    }
}
