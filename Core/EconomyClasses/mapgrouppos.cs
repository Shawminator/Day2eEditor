using System.ComponentModel;
using System.Xml.Serialization;

namespace Day2eEditor
{
    public class mapgroupposConfig : SingleFileConfigLoaderBase<map>
    {
        public mapgroupposConfig(string path) : base(path)
        {
        }

        public override void Load()
        {
            HasErrors = false;
            _errors.Clear();

            try
            {
                Data = AppServices.GetRequired<FileService>()
                    .LoadOrCreateXml<map>(
                        _path,
                        createNew: () => new map(),
                        onError: ex =>
                        {
                            HandleLoadError(ex);
                        },
                        configName: "mapgrouppos"
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

        protected override map CreateDefaultData()
        {
            return new map();
        }

        protected override void OnAfterLoad(map data)
        {
            // Optional post-load logic
        }

        protected override IEnumerable<string> ValidateData()
        {
            if (Data is null)
                yield break;

            var seenNames = new HashSet<string>(StringComparer.Ordinal);

            for (int i = 0; i < Data.group.Count; i++)
            {
                var entry = Data.group[i];

                if (string.IsNullOrWhiteSpace(entry.name))
                {
                    yield return $"group[{i}] has a missing or empty name.";
                }
                if (string.IsNullOrWhiteSpace(entry.pos))
                    yield return $"group[{i}] has a missing or empty pos.";

                if (string.IsNullOrWhiteSpace(entry.rpy))
                    yield return $"group[{i}] has a missing or empty rpy.";
            }
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public partial class map : IEquatable<map>, IDeepCloneable<map>
    {
        private BindingList<mapGroup>? _group;

        [XmlElement("group")]
        public BindingList<mapGroup> group
        {
            get => _group ??= new BindingList<mapGroup>();
            set => _group = value;
        }

        public bool Equals(map? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return group.SequenceEqual(other.group);
        }

        public override bool Equals(object? obj) => Equals(obj as map);

        public map Clone()
        {
            return new map
            {
                group = new BindingList<mapGroup>(group.Select(x => x.Clone()).ToList())
            };
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class mapGroup : IEquatable<mapGroup>, IDeepCloneable<mapGroup>
    {
        [XmlAttribute]
        public string? name { get; set; }

        [XmlAttribute]
        public string? pos { get; set; }

        [XmlAttribute]
        public string? rpy { get; set; }

        [XmlAttribute]
        public float a { get; set; }

        public override string ToString() => name ?? string.Empty;

        public bool Equals(mapGroup? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Equals(name, other.name, StringComparison.Ordinal) &&
                   string.Equals(pos, other.pos, StringComparison.Ordinal) &&
                   string.Equals(rpy, other.rpy, StringComparison.Ordinal) &&
                   a == other.a;
        }

        public override bool Equals(object? obj) => Equals(obj as mapGroup);

        public mapGroup Clone()
        {
            return new mapGroup
            {
                name = name,
                pos = pos,
                rpy = rpy,
                a = a
            };
        }
    }
}