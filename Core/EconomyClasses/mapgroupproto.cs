using System.ComponentModel;
using System.Xml.Serialization;

namespace Day2eEditor
{
    public class mapgroupprotoConfig : SingleFileConfigLoaderBase<prototype>
    {
        public mapgroupprotoConfig(string path) : base(path)
        {
        }

        public override void Load()
        {
            HasErrors = false;
            _errors.Clear();

            try
            {
                Data = AppServices.GetRequired<FileService>()
                    .LoadOrCreateXml<prototype>(
                        _path,
                        createNew: () => new prototype(),
                        onError: ex =>
                        {
                            HandleLoadError(ex);
                        },
                        configName: "prototype"
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

        protected override prototype CreateDefaultData()
        {
            return new prototype();
        }

        protected override void OnAfterLoad(prototype data)
        {
            // Optional post-load logic
        }

        protected override IEnumerable<string> ValidateData()
        {
            if (Data is null)
                yield break;

            var defaultNames = new HashSet<string>(StringComparer.Ordinal);
            for (int i = 0; i < Data.defaults.Count; i++)
            {
                var item = Data.defaults[i];

                if (string.IsNullOrWhiteSpace(item.name))
                    yield return $"defaults[{i}] has a missing or empty name.";
                else if (!defaultNames.Add(item.name))
                    yield return $"Duplicate default name '{item.name}' found.";
            }

            var groupNames = new HashSet<string>(StringComparer.Ordinal);
            for (int i = 0; i < Data.group.Count; i++)
            {
                var g = Data.group[i];

                if (string.IsNullOrWhiteSpace(g.name))
                    yield return $"group[{i}] has a missing or empty name.";
                else if (!groupNames.Add(g.name))
                    yield return $"Duplicate group name '{g.name}' found.";

                for (int j = 0; j < g.value.Count; j++)
                {
                    var value = g.value[j];
                    if (string.IsNullOrWhiteSpace(value.name) && string.IsNullOrWhiteSpace(value.user))
                        yield return $"group[{i}].value[{j}] must have either a name or a user.";
                }

                for (int j = 0; j < g.usage.Count; j++)
                {
                    var usage = g.usage[j];
                    if (string.IsNullOrWhiteSpace(usage.name))
                        yield return $"group[{i}].usage[{j}] has a missing or empty name.";
                }

                for (int j = 0; j < g.container.Count; j++)
                {
                    var container = g.container[j];

                    if (string.IsNullOrWhiteSpace(container.name))
                        yield return $"group[{i}].container[{j}] has a missing or empty name.";

                    for (int k = 0; k < container.category.Count; k++)
                    {
                        if (string.IsNullOrWhiteSpace(container.category[k].name))
                            yield return $"group[{i}].container[{j}].category[{k}] has a missing or empty name.";
                    }

                    for (int k = 0; k < container.tag.Count; k++)
                    {
                        if (string.IsNullOrWhiteSpace(container.tag[k].name))
                            yield return $"group[{i}].container[{j}].tag[{k}] has a missing or empty name.";
                    }

                    for (int k = 0; k < container.point.Count; k++)
                    {
                        if (string.IsNullOrWhiteSpace(container.point[k].pos))
                            yield return $"group[{i}].container[{j}].point[{k}] has a missing or empty pos.";
                    }
                }

                for (int j = 0; j < g.dispatch.Count; j++)
                {
                    var proxy = g.dispatch[j];

                    if (string.IsNullOrWhiteSpace(proxy.type))
                        yield return $"group[{i}].dispatch[{j}] has a missing or empty type.";
                    if (string.IsNullOrWhiteSpace(proxy.pos))
                        yield return $"group[{i}].dispatch[{j}] has a missing or empty pos.";
                    if (string.IsNullOrWhiteSpace(proxy.rpy))
                        yield return $"group[{i}].dispatch[{j}] has a missing or empty rpy.";
                }
            }
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public partial class prototype : IEquatable<prototype>, IDeepCloneable<prototype>
    {
        private BindingList<prototypeDefault>? _defaults;
        private BindingList<prototypeGroup>? _group;

        [XmlArrayItem("default", IsNullable = false)]
        public BindingList<prototypeDefault> defaults
        {
            get => _defaults ??= new BindingList<prototypeDefault>();
            set => _defaults = value;
        }

        [XmlElement("group")]
        public BindingList<prototypeGroup> group
        {
            get => _group ??= new BindingList<prototypeGroup>();
            set => _group = value;
        }

        public bool Equals(prototype? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return defaults.SequenceEqual(other.defaults) &&
                   group.SequenceEqual(other.group);
        }

        public override bool Equals(object? obj) => Equals(obj as prototype);

        public prototype Clone()
        {
            return new prototype
            {
                defaults = new BindingList<prototypeDefault>(defaults.Select(x => x.Clone()).ToList()),
                group = new BindingList<prototypeGroup>(group.Select(x => x.Clone()).ToList())
            };
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class prototypeDefault : IEquatable<prototypeDefault>, IDeepCloneable<prototypeDefault>
    {
        [XmlAttribute]
        public string? name { get; set; }

        [XmlAttribute]
        public int lootmax { get; set; }

        [XmlIgnore]
        public bool lootmaxSpecified { get; set; }

        [XmlAttribute]
        public string? enabled { get; set; }

        [XmlAttribute]
        public string? de { get; set; }

        [XmlAttribute]
        public int width { get; set; }

        [XmlIgnore]
        public bool widthSpecified { get; set; }

        [XmlAttribute]
        public int height { get; set; }

        [XmlIgnore]
        public bool heightSpecified { get; set; }

        public override string ToString()
        {
            string toname = "";
            toname += $"name : {name}";
            if (de != null)
                toname += $", de : {de}";
            if (lootmaxSpecified)
                toname += $", lootmax : {lootmax}";
            if (enabled != null)
                toname += $", enabled : {enabled}";
            if (widthSpecified)
                toname += $", width : {width}";
            if (heightSpecified)
                toname += $", height : {height}";

            return toname;
        }

        public bool Equals(prototypeDefault? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Equals(name, other.name, StringComparison.Ordinal) &&
                   lootmax == other.lootmax &&
                   lootmaxSpecified == other.lootmaxSpecified &&
                   string.Equals(enabled, other.enabled, StringComparison.Ordinal) &&
                   string.Equals(de, other.de, StringComparison.Ordinal) &&
                   width == other.width &&
                   widthSpecified == other.widthSpecified &&
                   height == other.height &&
                   heightSpecified == other.heightSpecified;
        }

        public override bool Equals(object? obj) => Equals(obj as prototypeDefault);

        public prototypeDefault Clone()
        {
            return new prototypeDefault
            {
                name = name,
                lootmax = lootmax,
                lootmaxSpecified = lootmaxSpecified,
                enabled = enabled,
                de = de,
                width = width,
                widthSpecified = widthSpecified,
                height = height,
                heightSpecified = heightSpecified
            };
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class prototypeGroup : IEquatable<prototypeGroup>, IDeepCloneable<prototypeGroup>
    {
        private BindingList<prototypeGroupValue>? _value;
        private BindingList<prototypeGroupUsage>? _usage;
        private BindingList<prototypeGroupContainer>? _container;
        private BindingList<prototypeGroupProxy>? _dispatch;

        [XmlElement("value")]
        public BindingList<prototypeGroupValue> value
        {
            get => _value ??= new BindingList<prototypeGroupValue>();
            set => _value = value;
        }

        [XmlElement("usage")]
        public BindingList<prototypeGroupUsage> usage
        {
            get => _usage ??= new BindingList<prototypeGroupUsage>();
            set => _usage = value;
        }

        [XmlElement("container")]
        public BindingList<prototypeGroupContainer> container
        {
            get => _container ??= new BindingList<prototypeGroupContainer>();
            set => _container = value;
        }

        [XmlArrayItem("proxy", IsNullable = true)]
        public BindingList<prototypeGroupProxy> dispatch
        {
            get => _dispatch ??= new BindingList<prototypeGroupProxy>();
            set => _dispatch = value;
        }

        [XmlAttribute]
        public string? name { get; set; }

        [XmlAttribute]
        public int lootmax { get; set; }

        [XmlIgnore]
        public bool lootmaxSpecified { get; set; }

        public override string ToString() => name ?? string.Empty;

        public void AddTier(string tier)
        {
            if (!value.Any(x => x.name == tier))
                value.Add(new prototypeGroupValue { name = tier });

            for (int i = 0; i < value.Count; i++)
            {
                if (value[i].name == null)
                {
                    value.RemoveAt(i);
                    i--;
                }
            }
        }

        public void removetier(string tier)
        {
            if (value.Any(x => x.name == tier))
                value.Remove(value.First(x => x.name == tier));
        }

        public void AdduserTier(string tier)
        {
            if (!value.Any(x => x.user == tier))
                value.Add(new prototypeGroupValue { user = tier });

            for (int i = 0; i < value.Count; i++)
            {
                if (value[i].user == null)
                {
                    value.RemoveAt(i);
                    i--;
                }
            }
        }

        public void removeusertier(string tier)
        {
            if (value.Any(x => x.user == tier))
                value.Remove(value.First(x => x.user == tier));
        }

        public void removetiers()
        {
            _value = null;
        }

        public void AddnewUsage(listsUsage u)
        {
            if (!usage.Any(x => x.name == u.name))
                usage.Add(new prototypeGroupUsage { name = u.name });
        }

        public void removeusage(prototypeGroupUsage u)
        {
            var usagetoremove = usage.FirstOrDefault(x => x.name == u.name);
            if (usagetoremove != null)
                usage.Remove(usagetoremove);
        }

        public bool Equals(prototypeGroup? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Equals(name, other.name, StringComparison.Ordinal) &&
                   lootmax == other.lootmax &&
                   lootmaxSpecified == other.lootmaxSpecified &&
                   value.SequenceEqual(other.value) &&
                   usage.SequenceEqual(other.usage) &&
                   container.SequenceEqual(other.container) &&
                   dispatch.SequenceEqual(other.dispatch);
        }

        public override bool Equals(object? obj) => Equals(obj as prototypeGroup);

        public bool ShouldSerializedispatch()
        {
            return dispatch.Count > 0;
        }

        public prototypeGroup Clone()
        {
            return new prototypeGroup
            {
                name = name,
                lootmax = lootmax,
                lootmaxSpecified = lootmaxSpecified,
                value = new BindingList<prototypeGroupValue>(value.Select(x => x.Clone()).ToList()),
                usage = new BindingList<prototypeGroupUsage>(usage.Select(x => x.Clone()).ToList()),
                container = new BindingList<prototypeGroupContainer>(container.Select(x => x.Clone()).ToList()),
                dispatch = new BindingList<prototypeGroupProxy>(dispatch.Select(x => x.Clone()).ToList())
            };
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class prototypeGroupValue : IEquatable<prototypeGroupValue>, IDeepCloneable<prototypeGroupValue>
    {
        [XmlAttribute]
        public string? user { get; set; }

        [XmlAttribute]
        public string? name { get; set; }

        public override string ToString() => name ?? string.Empty;

        public bool Equals(prototypeGroupValue? other)
        {
            if (other is null) return false;
            return string.Equals(user, other.user, StringComparison.Ordinal) &&
                   string.Equals(name, other.name, StringComparison.Ordinal);
        }

        public override bool Equals(object? obj) => Equals(obj as prototypeGroupValue);

        public prototypeGroupValue Clone()
        {
            return new prototypeGroupValue
            {
                user = user,
                name = name
            };
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class prototypeGroupUsage : IEquatable<prototypeGroupUsage>, IDeepCloneable<prototypeGroupUsage>
    {
        [XmlAttribute]
        public string? name { get; set; }

        public override string ToString() => name ?? string.Empty;

        public bool Equals(prototypeGroupUsage? other)
        {
            if (other is null) return false;
            return string.Equals(name, other.name, StringComparison.Ordinal);
        }

        public override bool Equals(object? obj) => Equals(obj as prototypeGroupUsage);

        public prototypeGroupUsage Clone()
        {
            return new prototypeGroupUsage
            {
                name = name
            };
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class prototypeGroupContainer : IEquatable<prototypeGroupContainer>, IDeepCloneable<prototypeGroupContainer>
    {
        private BindingList<prototypeGroupContainerCategory>? _category;
        private BindingList<prototypeGroupContainerTag>? _tag;
        private BindingList<prototypeGroupContainerPoint>? _point;

        [XmlElement("category")]
        public BindingList<prototypeGroupContainerCategory> category
        {
            get => _category ??= new BindingList<prototypeGroupContainerCategory>();
            set => _category = value;
        }

        [XmlElement("tag")]
        public BindingList<prototypeGroupContainerTag> tag
        {
            get => _tag ??= new BindingList<prototypeGroupContainerTag>();
            set => _tag = value;
        }

        [XmlElement("point")]
        public BindingList<prototypeGroupContainerPoint> point
        {
            get => _point ??= new BindingList<prototypeGroupContainerPoint>();
            set => _point = value;
        }

        [XmlAttribute]
        public string? name { get; set; }

        [XmlAttribute]
        public int lootmax { get; set; }

        [XmlIgnore]
        public bool lootmaxSpecified { get; set; }

        public override string ToString() => name ?? string.Empty;

        public void AddnewCategory(listsCategory c)
        {
            if (!category.Any(x => x.name == c.name))
                category.Add(new prototypeGroupContainerCategory { name = c.name });
        }

        public void removecategory(prototypeGroupContainerCategory c)
        {
            var cattoremove = category.FirstOrDefault(x => x.name == c.name);
            if (cattoremove != null)
                category.Remove(cattoremove);
        }

        public void Addnewtag(listsTag t)
        {
            if (!tag.Any(x => x.name == t.name))
                tag.Add(new prototypeGroupContainerTag { name = t.name });
        }

        public void removetag(prototypeGroupContainerTag t)
        {
            var tagtoremove = tag.FirstOrDefault(x => x.name == t.name);
            if (tagtoremove != null)
                tag.Remove(tagtoremove);
        }

        public bool Equals(prototypeGroupContainer? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Equals(name, other.name, StringComparison.Ordinal) &&
                   lootmax == other.lootmax &&
                   lootmaxSpecified == other.lootmaxSpecified &&
                   category.SequenceEqual(other.category) &&
                   tag.SequenceEqual(other.tag) &&
                   point.SequenceEqual(other.point);
        }

        public override bool Equals(object? obj) => Equals(obj as prototypeGroupContainer);

        public prototypeGroupContainer Clone()
        {
            return new prototypeGroupContainer
            {
                name = name,
                lootmax = lootmax,
                lootmaxSpecified = lootmaxSpecified,
                category = new BindingList<prototypeGroupContainerCategory>(category.Select(x => x.Clone()).ToList()),
                tag = new BindingList<prototypeGroupContainerTag>(tag.Select(x => x.Clone()).ToList()),
                point = new BindingList<prototypeGroupContainerPoint>(point.Select(x => x.Clone()).ToList())
            };
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class prototypeGroupContainerCategory : IEquatable<prototypeGroupContainerCategory>, IDeepCloneable<prototypeGroupContainerCategory>
    {
        [XmlAttribute]
        public string? name { get; set; }

        public override string ToString() => name ?? string.Empty;

        public bool Equals(prototypeGroupContainerCategory? other)
        {
            if (other is null) return false;
            return string.Equals(name, other.name, StringComparison.Ordinal);
        }

        public override bool Equals(object? obj) => Equals(obj as prototypeGroupContainerCategory);

        public prototypeGroupContainerCategory Clone()
        {
            return new prototypeGroupContainerCategory
            {
                name = name
            };
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class prototypeGroupContainerTag : IEquatable<prototypeGroupContainerTag>, IDeepCloneable<prototypeGroupContainerTag>
    {
        [XmlAttribute]
        public string? name { get; set; }

        public override string ToString() => name ?? string.Empty;

        public bool Equals(prototypeGroupContainerTag? other)
        {
            if (other is null) return false;
            return string.Equals(name, other.name, StringComparison.Ordinal);
        }

        public override bool Equals(object? obj) => Equals(obj as prototypeGroupContainerTag);

        public prototypeGroupContainerTag Clone()
        {
            return new prototypeGroupContainerTag
            {
                name = name
            };
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class prototypeGroupContainerPoint : IEquatable<prototypeGroupContainerPoint>, IDeepCloneable<prototypeGroupContainerPoint>
    {
        [XmlAttribute]
        public string? pos { get; set; }

        [XmlAttribute]
        public decimal range { get; set; }

        [XmlAttribute]
        public decimal height { get; set; }

        [XmlAttribute]
        public int flags { get; set; }

        [XmlIgnore]
        public bool flagsSpecified { get; set; }

        public bool Equals(prototypeGroupContainerPoint? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Equals(pos, other.pos, StringComparison.Ordinal) &&
                   range == other.range &&
                   height == other.height &&
                   flags == other.flags &&
                   flagsSpecified == other.flagsSpecified;
        }

        public override bool Equals(object? obj) => Equals(obj as prototypeGroupContainerPoint);

        public prototypeGroupContainerPoint Clone()
        {
            return new prototypeGroupContainerPoint
            {
                pos = pos,
                range = range,
                height = height,
                flags = flags,
                flagsSpecified = flagsSpecified
            };
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class prototypeGroupProxy : IEquatable<prototypeGroupProxy>, IDeepCloneable<prototypeGroupProxy>
    {
        [XmlAttribute]
        public string? type { get; set; }

        [XmlAttribute]
        public string? pos { get; set; }

        [XmlAttribute]
        public string? rpy { get; set; }

        public bool Equals(prototypeGroupProxy? other)
        {
            if (other is null) return false;
            return string.Equals(type, other.type, StringComparison.Ordinal) &&
                   string.Equals(pos, other.pos, StringComparison.Ordinal) &&
                   string.Equals(rpy, other.rpy, StringComparison.Ordinal);
        }

        public override bool Equals(object? obj) => Equals(obj as prototypeGroupProxy);

        public prototypeGroupProxy Clone()
        {
            return new prototypeGroupProxy
            {
                type = type,
                pos = pos,
                rpy = rpy
            };
        }
    }
}