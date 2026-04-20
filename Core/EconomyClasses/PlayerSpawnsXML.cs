using Day2eEditor;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Day2eEditor
{
    public class cfgplayerspawnpointsConfig : SingleFileConfigLoaderBase<playerspawnpoints>
    {
        public cfgplayerspawnpointsConfig(string path) : base(path)
        {
        }

        public override void Load()
        {
            HasErrors = false;
            _errors.Clear();

            try
            {
                Data = AppServices.GetRequired<FileService>()
                    .LoadOrCreateXml<playerspawnpoints>(
                        _path,
                        createNew: () => new playerspawnpoints(),
                        onError: ex =>
                        {
                            HandleLoadError(ex);
                        },
                        configName: "cfgplayerspawnpoints"
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

        protected override playerspawnpoints CreateDefaultData()
        {
            return new playerspawnpoints();
        }

        protected override void OnAfterLoad(playerspawnpoints data)
        {
            // Optional post-load logic
        }

        protected override IEnumerable<string> ValidateData()
        {
            var issues = new List<string>();
            issues.AddRange(ValidateSection(Data.fresh, x => Data.fresh = x, "fresh"));
            issues.AddRange(ValidateSection(Data.hop, x => Data.hop = x, "hop"));
            issues.AddRange(ValidateSection(Data.travel, x => Data.travel = x, "travel"));

            return issues;
        }

        private static IEnumerable<string> ValidateSection(
            playerspawnpointssection? section,
            Action<playerspawnpointssection> assignSection,
            string sectionName)
        {
            var issues = new List<string>();

            section = Ensure(section, assignSection, sectionName, issues);

            Ensure(section.spawn_params,
                x => section.spawn_params = x,
                $"{sectionName}.spawn_params",
                issues);

            Ensure(section.generator_params,
                x => section.generator_params = x,
                $"{sectionName}.generator_params",
                issues);

            Ensure(section.group_params,
                x => section.group_params = x,
                $"{sectionName}.group_params",
                issues);

            var bubbles = Ensure(section.generator_posbubbles,
                x => section.generator_posbubbles = x,
                $"{sectionName}.generator_posbubbles",
                issues);

            var groupNames = new HashSet<string>(StringComparer.Ordinal);
            int index = 0;

            foreach (var item in bubbles)
            {
                switch (item)
                {
                    case playerspawnpointsGroup g:
                        if (string.IsNullOrWhiteSpace(g.name))
                        {
                            issues.Add($"{sectionName}.generator_posbubbles[{index}] group has a missing or empty name.");
                        }
                        else if (!groupNames.Add(g.name))
                        {
                            issues.Add($"{sectionName}.generator_posbubbles[{index}] duplicate group name '{g.name}' found.");
                        }

                        if (g.pos is null)
                        {
                            g.pos = new BindingList<playerspawnpointsGroupPos>();
                            issues.Add($"{sectionName}.generator_posbubbles[{index}].pos was missing. Created empty pos list.");
                        }

                        break;

                    case playerspawnpointsGroupPos:
                        break;

                    case null:
                        issues.Add($"{sectionName}.generator_posbubbles[{index}] contains a null item.");
                        break;

                    default:
                        issues.Add($"{sectionName}.generator_posbubbles[{index}] contains unsupported type '{item.GetType().Name}'.");
                        break;
                }

                index++;
            }
            return issues;
        }
        private static T Ensure<T>(
            T? value,
            Action<T> assign,
            string path,
            List<string> issues)
            where T : class, new()
        {
            if (value is not null)
                return value;

            var created = new T();
            assign(created);
            issues.Add($"Missing {path}. Created default section.");
            return created;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public partial class playerspawnpoints : IEquatable<playerspawnpoints>, IDeepCloneable<playerspawnpoints>
    {
        private playerspawnpointssection? _fresh;
        private playerspawnpointssection? _hop;
        private playerspawnpointssection? _travel;

        public playerspawnpointssection fresh
        {
            get => _fresh ;
            set => _fresh = value;
        }

        public playerspawnpointssection hop
        {
            get => _hop ;
            set => _hop = value;
        }

        public playerspawnpointssection travel
        {
            get => _travel ;
            set => _travel = value;
        }

        public bool Equals(playerspawnpoints? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(fresh, other.fresh) &&
                   Equals(hop, other.hop) &&
                   Equals(travel, other.travel);
        }

        public override bool Equals(object? obj) => Equals(obj as playerspawnpoints);

        public playerspawnpoints Clone()
        {
            return new playerspawnpoints
            {
                fresh = fresh.Clone(),
                hop = hop.Clone(),
                travel = travel.Clone()
            };
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class playerspawnpointssection : IEquatable<playerspawnpointssection>, IDeepCloneable<playerspawnpointssection>
    {
        private playerspawnpointsSpawn_params? _spawn_params;
        private playerspawnpointsGenerator_params? _generator_params;
        private playerspawnpointsGroup_params? _group_params;
        private BindingList<object>? _generator_posbubbles;

        public playerspawnpointsSpawn_params spawn_params
        {
            get => _spawn_params;
            set => _spawn_params = value;
        }

        public playerspawnpointsGenerator_params generator_params
        {
            get => _generator_params;
            set => _generator_params = value;
        }

        public playerspawnpointsGroup_params group_params
        {
            get => _group_params;
            set => _group_params = value;
        }

        [XmlArray("generator_posbubbles")]
        [XmlArrayItem("group", typeof(playerspawnpointsGroup))]
        [XmlArrayItem("pos", typeof(playerspawnpointsGroupPos))]
        public BindingList<object> generator_posbubbles
        {
            get => _generator_posbubbles ??= new BindingList<object>();
            set => _generator_posbubbles = value;
        }

        [XmlIgnore]
        public IEnumerable<playerspawnpointsGroup> Groups =>
            generator_posbubbles.OfType<playerspawnpointsGroup>();

        [XmlIgnore]
        public IEnumerable<playerspawnpointsGroupPos> Positions =>
            generator_posbubbles.OfType<playerspawnpointsGroupPos>();

        public bool Equals(playerspawnpointssection? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(spawn_params, other.spawn_params) &&
                   Equals(generator_params, other.generator_params) &&
                   Equals(group_params, other.group_params) &&
                   GeneratorPosBubblesEqual(generator_posbubbles, other.generator_posbubbles);
        }

        public override bool Equals(object? obj) => Equals(obj as playerspawnpointssection);

        public playerspawnpointssection Clone()
        {
            return new playerspawnpointssection
            {
                spawn_params = spawn_params.Clone(),
                generator_params = generator_params.Clone(),
                group_params = group_params.Clone(),
                generator_posbubbles = new BindingList<object>(
                    generator_posbubbles.Select(CloneBubble).ToList()
                )
            };
        }

        private static bool GeneratorPosBubblesEqual(IEnumerable<object>? a, IEnumerable<object>? b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;

            return a.SequenceEqual(b, new GeneratorPosBubbleComparer());
        }

        private static object CloneBubble(object item)
        {
            return item switch
            {
                playerspawnpointsGroup g => g.Clone(),
                playerspawnpointsGroupPos p => p.Clone(),
                _ => throw new InvalidOperationException($"Unsupported generator_posbubbles item type: {item.GetType().FullName}")
            };
        }

        private sealed class GeneratorPosBubbleComparer : IEqualityComparer<object>
        {
            public new bool Equals(object? x, object? y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (x is null || y is null) return false;
                if (x.GetType() != y.GetType()) return false;

                return x switch
                {
                    playerspawnpointsGroup gx when y is playerspawnpointsGroup gy => gx.Equals(gy),
                    playerspawnpointsGroupPos px when y is playerspawnpointsGroupPos py => px.Equals(py),
                    _ => false
                };
            }

            public int GetHashCode(object obj)
            {
                throw new NotSupportedException();
            }
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class playerspawnpointsSpawn_params : IEquatable<playerspawnpointsSpawn_params>, IDeepCloneable<playerspawnpointsSpawn_params>
    {
        public decimal min_dist_infected { get; set; }
        public decimal max_dist_infected { get; set; }
        public decimal min_dist_player { get; set; }
        public decimal max_dist_player { get; set; }
        public decimal min_dist_static { get; set; }
        public decimal max_dist_static { get; set; }

        [XmlIgnore]
        public bool min_dist_triggerSpecified { get; set; }

        public decimal min_dist_trigger { get; set; }

        [XmlIgnore]
        public bool max_dist_triggerSpecified { get; set; }

        public decimal max_dist_trigger { get; set; }

        public bool Equals(playerspawnpointsSpawn_params? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return min_dist_infected == other.min_dist_infected
                && max_dist_infected == other.max_dist_infected
                && min_dist_player == other.min_dist_player
                && max_dist_player == other.max_dist_player
                && min_dist_static == other.min_dist_static
                && max_dist_static == other.max_dist_static
                && min_dist_triggerSpecified == other.min_dist_triggerSpecified
                && min_dist_trigger == other.min_dist_trigger
                && max_dist_triggerSpecified == other.max_dist_triggerSpecified
                && max_dist_trigger == other.max_dist_trigger;
        }

        public override bool Equals(object? obj) => Equals(obj as playerspawnpointsSpawn_params);

        public playerspawnpointsSpawn_params Clone()
        {
            return new playerspawnpointsSpawn_params
            {
                min_dist_infected = min_dist_infected,
                max_dist_infected = max_dist_infected,
                min_dist_player = min_dist_player,
                max_dist_player = max_dist_player,
                min_dist_static = min_dist_static,
                max_dist_static = max_dist_static,
                min_dist_triggerSpecified = min_dist_triggerSpecified,
                min_dist_trigger = min_dist_trigger,
                max_dist_triggerSpecified = max_dist_triggerSpecified,
                max_dist_trigger = max_dist_trigger
            };
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class playerspawnpointsGenerator_params : IEquatable<playerspawnpointsGenerator_params>, IDeepCloneable<playerspawnpointsGenerator_params>
    {
        public decimal grid_density { get; set; }
        public decimal grid_width { get; set; }
        public decimal grid_height { get; set; }
        public decimal min_dist_static { get; set; }
        public decimal max_dist_static { get; set; }
        public decimal min_steepness { get; set; }
        public decimal max_steepness { get; set; }
        public bool allow_in_water { get; set; }

        public bool Equals(playerspawnpointsGenerator_params? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return grid_density == other.grid_density
                && grid_width == other.grid_width
                && grid_height == other.grid_height
                && min_dist_static == other.min_dist_static
                && max_dist_static == other.max_dist_static
                && min_steepness == other.min_steepness
                && max_steepness == other.max_steepness
                && allow_in_water == other.allow_in_water;
        }

        public override bool Equals(object? obj) => Equals(obj as playerspawnpointsGenerator_params);

        public playerspawnpointsGenerator_params Clone()
        {
            return new playerspawnpointsGenerator_params
            {
                grid_density = grid_density,
                grid_width = grid_width,
                grid_height = grid_height,
                min_dist_static = min_dist_static,
                max_dist_static = max_dist_static,
                min_steepness = min_steepness,
                max_steepness = max_steepness,
                allow_in_water = allow_in_water
            };
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class playerspawnpointsGroup_params : IEquatable<playerspawnpointsGroup_params>, IDeepCloneable<playerspawnpointsGroup_params>
    {
        public bool enablegroups { get; set; }
        public bool groups_as_regular { get; set; } = true;
        public int lifetime { get; set; }
        public int counter { get; set; }

        public bool Equals(playerspawnpointsGroup_params? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return enablegroups == other.enablegroups
                && groups_as_regular == other.groups_as_regular
                && lifetime == other.lifetime
                && counter == other.counter;
        }

        public override bool Equals(object? obj) => Equals(obj as playerspawnpointsGroup_params);

        public playerspawnpointsGroup_params Clone()
        {
            return new playerspawnpointsGroup_params
            {
                enablegroups = enablegroups,
                groups_as_regular = groups_as_regular,
                lifetime = lifetime,
                counter = counter
            };
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class playerspawnpointsGroup : IEquatable<playerspawnpointsGroup>, IDeepCloneable<playerspawnpointsGroup>
    {
        private BindingList<playerspawnpointsGroupPos>? _pos;

        [XmlElement("pos")]
        public BindingList<playerspawnpointsGroupPos> pos
        {
            get => _pos;
            set => _pos = value;
        }

        [XmlAttribute]
        public string? name { get; set; }

        [XmlAttribute]
        public int lifetime { get; set; }

        [XmlIgnore]
        public bool lifetimeSpecified { get; set; }

        [XmlAttribute]
        public int counter { get; set; }

        [XmlIgnore]
        public bool counterSpecified { get; set; }

        public override string ToString() => name ?? string.Empty;

        public bool Equals(playerspawnpointsGroup? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Equals(name, other.name, StringComparison.OrdinalIgnoreCase)
                && lifetime == other.lifetime
                && lifetimeSpecified == other.lifetimeSpecified
                && counter == other.counter
                && counterSpecified == other.counterSpecified
                && pos.SequenceEqual(other.pos);
        }

        public override bool Equals(object? obj) => Equals(obj as playerspawnpointsGroup);

        public playerspawnpointsGroup Clone()
        {
            return new playerspawnpointsGroup
            {
                name = name,
                lifetime = lifetime,
                lifetimeSpecified = lifetimeSpecified,
                counter = counter,
                counterSpecified = counterSpecified,
                pos = new BindingList<playerspawnpointsGroupPos>(pos.Select(x => x.Clone()).ToList())
            };
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class playerspawnpointsGroupPos : IEquatable<playerspawnpointsGroupPos>, IDeepCloneable<playerspawnpointsGroupPos>
    {
        [XmlAttribute]
        public decimal x { get; set; }

        [XmlAttribute]
        public decimal z { get; set; }

        public override string ToString() => $"X:{x} , Z:{z}";

        public bool Equals(playerspawnpointsGroupPos? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return x == other.x && z == other.z;
        }

        public override bool Equals(object? obj) => Equals(obj as playerspawnpointsGroupPos);

        public playerspawnpointsGroupPos Clone()
        {
            return new playerspawnpointsGroupPos
            {
                x = x,
                z = z
            };
        }
    }

}