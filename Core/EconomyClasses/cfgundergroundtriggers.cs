using System.ComponentModel;

namespace Day2eEditor
{
    public class cfgundergroundtriggersConfig : SingleFileConfigLoaderBase<cfgundergroundtriggers>
    {
        public cfgundergroundtriggersConfig(string path) : base(path)
        {
        }

        protected override cfgundergroundtriggers CreateDefaultData()
        {
            return new cfgundergroundtriggers();
        }

        public override void Load()
        {
            HasErrors = false;
            _errors.Clear();

            try
            {
                Data = AppServices.GetRequired<FileService>().LoadOrCreateJson<cfgundergroundtriggers>(
                    _path,
                    createNew: () => new cfgundergroundtriggers(),
                    onError: ex =>
                    {
                        HandleLoadError(ex);
                    },
                    configName: "cfgundergroundtriggers",
                    useBoolConvertor: false
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
                AppServices.GetRequired<FileService>().SaveJson(_path, Data);
                ClonedData = CloneData(Data);
                return new[] { _path };
            }

            return Array.Empty<string>();
        }

        protected override IEnumerable<string> ValidateData()
        {
            return Data?.FixMissingOrInvalidFields() ?? Enumerable.Empty<string>();
        }

        protected override void OnAfterLoad(cfgundergroundtriggers data)
        {
            // Optional post-load logic
        }
    }

    public class cfgundergroundtriggers : IEquatable<cfgundergroundtriggers>, IDeepCloneable<cfgundergroundtriggers>
    {
        public BindingList<Trigger> Triggers { get; set; } = new();

        internal IEnumerable<string> FixMissingOrInvalidFields()
        {
            var issues = new List<string>();

            Triggers ??= new BindingList<Trigger>();

            for (int i = 0; i < Triggers.Count; i++)
            {
                var trigger = Triggers[i];
                if (trigger == null)
                {
                    issues.Add($"Triggers[{i}] was null and could not be repaired.");
                    continue;
                }

                trigger.Breadcrumbs ??= new BindingList<Breadcrumb>();

                if (trigger.Position == null || trigger.Position.Length != 3)
                {
                    trigger.Position = new decimal[] { 0m, 0m, 0m };
                    issues.Add($"Triggers[{i}] had missing/invalid Position and was reset to [0,0,0].");
                }

                if (trigger.Orientation == null || trigger.Orientation.Length != 3)
                {
                    trigger.Orientation = new decimal[] { 0m, 0m, 0m };
                    issues.Add($"Triggers[{i}] had missing/invalid Orientation and was reset to [0,0,0].");
                }

                if (trigger.Size == null || trigger.Size.Length != 3)
                {
                    trigger.Size = new decimal[] { 0m, 0m, 0m };
                    issues.Add($"Triggers[{i}] had missing/invalid Size and was reset to [0,0,0].");
                }

                for (int j = trigger.Breadcrumbs.Count - 1; j >= 0; j--)
                {
                    var breadcrumb = trigger.Breadcrumbs[j];
                    if (breadcrumb == null)
                    {
                        trigger.Breadcrumbs.RemoveAt(j);
                        issues.Add($"Triggers[{i}].Breadcrumbs[{j}] was null and was removed.");
                        continue;
                    }

                    if (breadcrumb.Position == null || breadcrumb.Position.Length != 3)
                    {
                        trigger.Breadcrumbs.RemoveAt(j);
                        issues.Add($"Triggers[{i}].Breadcrumbs[{j}] had missing/invalid Position and was removed.");
                    }
                }
            }

            return issues;
        }

        public bool Equals(cfgundergroundtriggers? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (Triggers.Count != other.Triggers.Count)
                return false;

            for (int i = 0; i < Triggers.Count; i++)
            {
                if (!Equals(Triggers[i], other.Triggers[i]))
                    return false;
            }

            return true;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as cfgundergroundtriggers);
        }

        public cfgundergroundtriggers Clone()
        {
            return new cfgundergroundtriggers
            {
                Triggers = new BindingList<Trigger>(Triggers.Select(x => x.Clone()).ToList())
            };
        }
    }

    public class Trigger : IEquatable<Trigger>, IDeepCloneable<Trigger>
    {
        public decimal[]? Position { get; set; }
        public decimal[]? Orientation { get; set; }
        public decimal[]? Size { get; set; }
        public decimal EyeAccommodation { get; set; }
        public int? UseLinePointFade { get; set; }
        public string? AmbientSoundType { get; set; }
        public BindingList<Breadcrumb> Breadcrumbs { get; set; } = new();
        public decimal? InterpolationSpeed { get; set; }

        public string gettriggertype()
        {
            if (EyeAccommodation == 1 && Breadcrumbs.Count == 0)
            {
                if (InterpolationSpeed == null)
                    InterpolationSpeed = 1;
                return "Outer";
            }
            else if (EyeAccommodation == 0 && Breadcrumbs.Count == 0)
            {
                if (InterpolationSpeed == null)
                    InterpolationSpeed = 1;
                return "Inner";
            }
            else if (Breadcrumbs.Count == 0)
            {
                InterpolationSpeed = 1;
                return "Transition";
            }
            else
            {
                InterpolationSpeed = null;
                return "Transition";
            }
        }

        public bool Equals(Trigger? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return
                ((Position?.SequenceEqual(other.Position ?? Array.Empty<decimal>()) ?? other.Position == null)) &&
                ((Orientation?.SequenceEqual(other.Orientation ?? Array.Empty<decimal>()) ?? other.Orientation == null)) &&
                ((Size?.SequenceEqual(other.Size ?? Array.Empty<decimal>()) ?? other.Size == null)) &&
                EyeAccommodation == other.EyeAccommodation &&
                UseLinePointFade == other.UseLinePointFade &&
                string.Equals(AmbientSoundType, other.AmbientSoundType, StringComparison.Ordinal) &&
                BreadcrumbsEqual(Breadcrumbs, other.Breadcrumbs) &&
                InterpolationSpeed == other.InterpolationSpeed;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Trigger);
        }

        public Trigger Clone()
        {
            return new Trigger
            {
                Position = Position != null ? (decimal[])Position.Clone() : null,
                Orientation = Orientation != null ? (decimal[])Orientation.Clone() : null,
                Size = Size != null ? (decimal[])Size.Clone() : null,
                EyeAccommodation = EyeAccommodation,
                UseLinePointFade = UseLinePointFade,
                AmbientSoundType = AmbientSoundType,
                Breadcrumbs = new BindingList<Breadcrumb>(Breadcrumbs.Select(x => x.Clone()).ToList()),
                InterpolationSpeed = InterpolationSpeed
            };
        }

        private static bool BreadcrumbsEqual(IList<Breadcrumb>? a, IList<Breadcrumb>? b)
        {
            if (ReferenceEquals(a, b))
                return true;
            if (a is null || b is null)
                return false;
            if (a.Count != b.Count)
                return false;

            for (int i = 0; i < a.Count; i++)
            {
                if (!Equals(a[i], b[i]))
                    return false;
            }

            return true;
        }
    }

    public class Breadcrumb : IEquatable<Breadcrumb>, IDeepCloneable<Breadcrumb>
    {
        public decimal[]? Position { get; set; }
        public decimal EyeAccommodation { get; set; }
        public int? UseRaycast { get; set; }
        public decimal? Radius { get; set; }
        public int? LightLerp { get; set; }

        public string getbreadcrumbtype()
        {
            switch (EyeAccommodation)
            {
                case 0:
                    return "Inner";
                case 1:
                    return "Outer";
                default:
                    return "Transition";
            }
        }

        public bool Equals(Breadcrumb? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return
                ((Position?.SequenceEqual(other.Position ?? Array.Empty<decimal>()) ?? other.Position == null)) &&
                EyeAccommodation == other.EyeAccommodation &&
                UseRaycast == other.UseRaycast &&
                Radius == other.Radius &&
                LightLerp == other.LightLerp;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Breadcrumb);
        }

        public Breadcrumb Clone()
        {
            return new Breadcrumb
            {
                Position = Position != null ? (decimal[])Position.Clone() : null,
                EyeAccommodation = EyeAccommodation,
                UseRaycast = UseRaycast,
                Radius = Radius,
                LightLerp = LightLerp
            };
        }
    }
}