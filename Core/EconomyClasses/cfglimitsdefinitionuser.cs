using System.ComponentModel;
using System.Xml.Serialization;

namespace Day2eEditor
{
    public class cfglimitsdefinitionuserConfig : SingleFileConfigLoaderBase<cfglimitsdefinitionuser>
    {
        public cfglimitsdefinitionuserConfig(string path) : base(path)
        {
        }

        public override void Load()
        {
            HasErrors = false;
            _errors.Clear();

            try
            {
                Data = AppServices.GetRequired<FileService>()
                    .LoadOrCreateXml<cfglimitsdefinitionuser>(
                        _path,
                        createNew: () => new cfglimitsdefinitionuser(),
                        onError: ex =>
                        {
                            HandleLoadError(ex);
                        },
                        configName: "cfglimitsdefinitionuser"
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
                return new[] { _path};
            }

            return Array.Empty<string>();
        }

        protected override cfglimitsdefinitionuser CreateDefaultData()
        {
            return new cfglimitsdefinitionuser();
        }

        protected override void OnAfterLoad(cfglimitsdefinitionuser data)
        {
            // Optional post-load logic
        }

        protected override IEnumerable<string> ValidateData()
        {
            if (Data is null)
                yield break;

            var usageUsers = new HashSet<string>(StringComparer.Ordinal);
            for (int i = 0; i < Data.usageflags.Count; i++)
            {
                var user = Data.usageflags[i];

                if (string.IsNullOrWhiteSpace(user.name))
                {
                    yield return $"usageflags[{i}] has a missing or empty user name.";
                }
                else if (!usageUsers.Add(user.name))
                {
                    yield return $"Duplicate usage user '{user.name}' found.";
                }

                var usageNames = new HashSet<string>(StringComparer.Ordinal);
                for (int j = 0; j < user.usage.Count; j++)
                {
                    var usage = user.usage[j];

                    if (string.IsNullOrWhiteSpace(usage.name))
                    {
                        yield return $"usageflags[{i}].usage[{j}] has a missing or empty name.";
                        continue;
                    }

                    if (!usageNames.Add(usage.name))
                        yield return $"Duplicate usage '{usage.name}' found for usage user '{user.name}'.";
                }
            }

            var valueUsers = new HashSet<string>(StringComparer.Ordinal);
            for (int i = 0; i < Data.valueflags.Count; i++)
            {
                var user = Data.valueflags[i];

                if (string.IsNullOrWhiteSpace(user.name))
                {
                    yield return $"valueflags[{i}] has a missing or empty user name.";
                }
                else if (!valueUsers.Add(user.name))
                {
                    yield return $"Duplicate value user '{user.name}' found.";
                }

                var valueNames = new HashSet<string>(StringComparer.Ordinal);
                for (int j = 0; j < user.value.Count; j++)
                {
                    var value = user.value[j];

                    if (string.IsNullOrWhiteSpace(value.name))
                    {
                        yield return $"valueflags[{i}].value[{j}] has a missing or empty name.";
                        continue;
                    }

                    if (!valueNames.Add(value.name))
                        yield return $"Duplicate value '{value.name}' found for value user '{user.name}'.";
                }
            }
        }
    }

    [XmlRoot("user_lists")]
    public partial class cfglimitsdefinitionuser : IEquatable<cfglimitsdefinitionuser>, IDeepCloneable<cfglimitsdefinitionuser>
    {
        private BindingList<user_listsUser>? _usageflags;
        private BindingList<user_listsUser1>? _valueflags;

        [XmlArrayItem("user", IsNullable = false)]
        public BindingList<user_listsUser> usageflags
        {
            get => _usageflags ??= new BindingList<user_listsUser>();
            set => _usageflags = value;
        }

        [XmlArrayItem("user", IsNullable = false)]
        public BindingList<user_listsUser1> valueflags
        {
            get => _valueflags ??= new BindingList<user_listsUser1>();
            set => _valueflags = value;
        }

        public bool Equals(cfglimitsdefinitionuser? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return usageflags.SequenceEqual(other.usageflags)
                && valueflags.SequenceEqual(other.valueflags);
        }

        public override bool Equals(object? obj) => Equals(obj as cfglimitsdefinitionuser);

        public cfglimitsdefinitionuser Clone()
        {
            return new cfglimitsdefinitionuser
            {
                usageflags = new BindingList<user_listsUser>(usageflags.Select(x => x.Clone()).ToList()),
                valueflags = new BindingList<user_listsUser1>(valueflags.Select(x => x.Clone()).ToList())
            };
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class user_listsUser : IEquatable<user_listsUser>, IDeepCloneable<user_listsUser>
    {
        private BindingList<user_listsUserUsage>? _usage;

        [XmlElement("usage")]
        public BindingList<user_listsUserUsage> usage
        {
            get => _usage ??= new BindingList<user_listsUserUsage>();
            set => _usage = value;
        }

        [XmlAttribute]
        public string? name { get; set; }

        public override string ToString() => name ?? string.Empty;

        public bool Equals(user_listsUser? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Equals(name, other.name, StringComparison.Ordinal)
                && usage.SequenceEqual(other.usage);
        }

        public override bool Equals(object? obj) => Equals(obj as user_listsUser);

        public user_listsUser Clone()
        {
            return new user_listsUser
            {
                name = name,
                usage = new BindingList<user_listsUserUsage>(usage.Select(x => x.Clone()).ToList())
            };
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class user_listsUserUsage : IEquatable<user_listsUserUsage>, IDeepCloneable<user_listsUserUsage>
    {
        [XmlAttribute]
        public string? name { get; set; }

        public override string ToString() => name ?? string.Empty;

        public bool Equals(user_listsUserUsage? other)
        {
            if (other is null) return false;
            return string.Equals(name, other.name, StringComparison.Ordinal);
        }

        public override bool Equals(object? obj) => Equals(obj as user_listsUserUsage);

        public user_listsUserUsage Clone()
        {
            return new user_listsUserUsage
            {
                name = name
            };
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class user_listsUser1 : IEquatable<user_listsUser1>, IDeepCloneable<user_listsUser1>
    {
        private BindingList<user_listsUserValue>? _value;

        [XmlElement("value")]
        public BindingList<user_listsUserValue> value
        {
            get => _value ??= new BindingList<user_listsUserValue>();
            set => _value = value;
        }

        [XmlAttribute]
        public string? name { get; set; }

        public override string ToString() => name ?? string.Empty;

        public bool Equals(user_listsUser1? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Equals(name, other.name, StringComparison.Ordinal)
                && value.SequenceEqual(other.value);
        }

        public override bool Equals(object? obj) => Equals(obj as user_listsUser1);

        public user_listsUser1 Clone()
        {
            return new user_listsUser1
            {
                name = name,
                value = new BindingList<user_listsUserValue>(value.Select(x => x.Clone()).ToList())
            };
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class user_listsUserValue : IEquatable<user_listsUserValue>, IDeepCloneable<user_listsUserValue>
    {
        [XmlAttribute]
        public string? name { get; set; }

        public override string ToString() => name ?? string.Empty;

        public bool Equals(user_listsUserValue? other)
        {
            if (other is null) return false;
            return string.Equals(name, other.name, StringComparison.Ordinal);
        }

        public override bool Equals(object? obj) => Equals(obj as user_listsUserValue);

        public user_listsUserValue Clone()
        {
            return new user_listsUserValue
            {
                name = name
            };
        }
    }
}