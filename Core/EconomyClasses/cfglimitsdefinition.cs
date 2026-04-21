using System.ComponentModel;
using System.Xml.Serialization;

namespace Day2eEditor
{
    public class cfglimitsdefinitionConfig : SingleFileConfigLoaderBase<cfglimitsdefinition>
    {
        public cfglimitsdefinitionConfig(string path) : base(path)
        {
        }

        public override void Load()
        {
            HasErrors = false;
            _errors.Clear();

            try
            {
                Data = AppServices.GetRequired<FileService>()
                    .LoadOrCreateXml<cfglimitsdefinition>(
                        _path,
                        createNew: () => new cfglimitsdefinition(),
                        onError: ex =>
                        {
                            HandleLoadError(ex);
                        },
                        configName: "cfglimitsdefinition"
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
                return new[] { _path };
            }

            return Array.Empty<string>();
        }

        protected override cfglimitsdefinition CreateDefaultData()
        {
            return new cfglimitsdefinition();
        }

        protected override void OnAfterLoad(cfglimitsdefinition data)
        {
            // Optional post-load logic
        }

        protected override IEnumerable<string> ValidateData()
        {
            if (Data is null)
                yield break;

            foreach (var issue in ValidateUniqueNames(Data.categories, "categories"))
                yield return issue;

            foreach (var issue in ValidateUniqueNames(Data.tags, "tags"))
                yield return issue;

            foreach (var issue in ValidateUniqueNames(Data.usageflags, "usageflags"))
                yield return issue;

            foreach (var issue in ValidateUniqueNames(Data.valueflags, "valueflags"))
                yield return issue;
        }

        private static IEnumerable<string> ValidateUniqueNames<T>(IEnumerable<T> items, string section)
            where T : INamedEntry
        {
            var seen = new HashSet<string>(StringComparer.Ordinal);
            int index = 0;

            foreach (var item in items)
            {
                if (string.IsNullOrWhiteSpace(item.name))
                {
                    yield return $"{section}[{index}] has a missing or empty name.";
                    index++;
                    continue;
                }

                if (!seen.Add(item.name))
                    yield return $"Duplicate name '{item.name}' found in {section}.";

                index++;
            }
        }
    }

    [XmlRoot("lists")]
    public partial class cfglimitsdefinition : IEquatable<cfglimitsdefinition>, IDeepCloneable<cfglimitsdefinition>
    {
        private BindingList<listsCategory>? _categories;
        private BindingList<listsTag>? _tags;
        private BindingList<listsUsage>? _usageflags;
        private BindingList<listsValue>? _valueflags;

        [XmlArrayItem("category", IsNullable = false)]
        public BindingList<listsCategory> categories
        {
            get => _categories ??= new BindingList<listsCategory>();
            set => _categories = value;
        }

        [XmlArrayItem("tag", IsNullable = false)]
        public BindingList<listsTag> tags
        {
            get => _tags ??= new BindingList<listsTag>();
            set => _tags = value;
        }

        [XmlArrayItem("usage", IsNullable = false)]
        public BindingList<listsUsage> usageflags
        {
            get => _usageflags ??= new BindingList<listsUsage>();
            set => _usageflags = value;
        }

        [XmlArrayItem("value", IsNullable = false)]
        public BindingList<listsValue> valueflags
        {
            get => _valueflags ??= new BindingList<listsValue>();
            set => _valueflags = value;
        }

        public bool Equals(cfglimitsdefinition? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return categories.SequenceEqual(other.categories) &&
                   tags.SequenceEqual(other.tags) &&
                   usageflags.SequenceEqual(other.usageflags) &&
                   valueflags.SequenceEqual(other.valueflags);
        }

        public override bool Equals(object? obj) => Equals(obj as cfglimitsdefinition);

        public cfglimitsdefinition Clone()
        {
            return new cfglimitsdefinition
            {
                categories = new BindingList<listsCategory>(categories.Select(x => x.Clone()).ToList()),
                tags = new BindingList<listsTag>(tags.Select(x => x.Clone()).ToList()),
                usageflags = new BindingList<listsUsage>(usageflags.Select(x => x.Clone()).ToList()),
                valueflags = new BindingList<listsValue>(valueflags.Select(x => x.Clone()).ToList())
            };
        }
    }

    public interface INamedEntry
    {
        string? name { get; set; }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class listsCategory : IEquatable<listsCategory>, IDeepCloneable<listsCategory>, INamedEntry
    {
        [XmlAttribute]
        public string? name { get; set; }

        public override string ToString() => name ?? string.Empty;

        public bool Equals(listsCategory? other)
        {
            if (other is null) return false;
            return string.Equals(name, other.name, StringComparison.Ordinal);
        }

        public override bool Equals(object? obj) => Equals(obj as listsCategory);

        public listsCategory Clone()
        {
            return new listsCategory
            {
                name = name
            };
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class listsTag : IEquatable<listsTag>, IDeepCloneable<listsTag>, INamedEntry
    {
        [XmlAttribute]
        public string? name { get; set; }

        public override string ToString() => name ?? string.Empty;

        public bool Equals(listsTag? other)
        {
            if (other is null) return false;
            return string.Equals(name, other.name, StringComparison.Ordinal);
        }

        public override bool Equals(object? obj) => Equals(obj as listsTag);

        public listsTag Clone()
        {
            return new listsTag
            {
                name = name
            };
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class listsUsage : IEquatable<listsUsage>, IDeepCloneable<listsUsage>, INamedEntry
    {
        [XmlAttribute]
        public string? name { get; set; }

        public override string ToString() => name ?? string.Empty;

        public bool Equals(listsUsage? other)
        {
            if (other is null) return false;
            return string.Equals(name, other.name, StringComparison.Ordinal);
        }

        public override bool Equals(object? obj) => Equals(obj as listsUsage);

        public listsUsage Clone()
        {
            return new listsUsage
            {
                name = name
            };
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class listsValue : IEquatable<listsValue>, IDeepCloneable<listsValue>, INamedEntry
    {
        [XmlAttribute]
        public string? name { get; set; }

        public override string ToString() => name ?? string.Empty;

        public bool Equals(listsValue? other)
        {
            if (other is null) return false;
            return string.Equals(name, other.name, StringComparison.Ordinal);
        }

        public override bool Equals(object? obj) => Equals(obj as listsValue);

        public listsValue Clone()
        {
            return new listsValue
            {
                name = name
            };
        }
    }
}