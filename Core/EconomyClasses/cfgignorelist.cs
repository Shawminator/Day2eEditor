using System.ComponentModel;
using System.Xml.Serialization;

namespace Day2eEditor
{
    public class cfgignorelistConfig : SingleFileConfigLoaderBase<ignore>
    {
        public cfgignorelistConfig(string path) : base(path)
        {
        }
        public override void Load()
        {
            HasErrors = false;
            _errors.Clear();

            try
            {
                Data = AppServices.GetRequired<FileService>()
                    .LoadOrCreateXml<ignore>(
                        _path,
                        createNew: () => new ignore(),
                        onError: ex =>
                        {
                            HandleLoadError(ex);
                        },
                        configName: "cfgignorelist"
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

        public IEnumerable<string> Save()
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
        protected override ignore CreateDefaultData()
        {
            return new ignore();
        }
        protected override void OnAfterLoad(ignore data)
        {
            // Optional post-load logic
        }
        protected override IEnumerable<string> ValidateData()
        {
            if (Data is null)
                yield break;

            var seen = new HashSet<string>(StringComparer.Ordinal);

            for (int i = 0; i < Data.type.Count; i++)
            {
                var entry = Data.type[i];

                if (string.IsNullOrWhiteSpace(entry.name))
                {
                    yield return $"type[{i}] has a missing or empty name.";
                    continue;
                }

                if (!seen.Add(entry.name))
                    yield return $"Duplicate ignore type name '{entry.name}' found.";
            }
        }
    }

    [XmlRoot("ignore")]
    public class ignore : IEquatable<ignore>, IDeepCloneable<ignore>
    {
        private BindingList<ignoreType>? _type;

        [XmlElement("type")]
        public BindingList<ignoreType> type
        {
            get => _type ??= new BindingList<ignoreType>();
            set => _type = value;
        }

        public bool Equals(ignore? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return type.SequenceEqual(other.type);
        }
        public override bool Equals(object? obj) => Equals(obj as ignore);
        public ignore Clone()
        {
            return new ignore
            {
                type = new BindingList<ignoreType>(type.Select(x => x.Clone()).ToList())
            };
        }
    }


    public class ignoreType : IEquatable<ignoreType>, IDeepCloneable<ignoreType>
    {
        [XmlAttribute]
        public string? name { get; set; }

        public bool Equals(ignoreType? other)
        {
            if (other is null) return false;
            return string.Equals(name, other.name, StringComparison.Ordinal);
        }
        public override bool Equals(object? obj) => Equals(obj as ignoreType);
        public ignoreType Clone()
        {
            return new ignoreType
            {
                name = name
            };
        }
    }
}
