using System.ComponentModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                    useBoolConvertor: false,
                    useVecConvertor: true
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
                AppServices.GetRequired<FileService>().SaveJson(_path, Data, false, true);
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

                if (trigger.Position == null)
                {
                    trigger.Position = new Vec3( 0m, 0m, 0m );
                    issues.Add($"Triggers[{i}] had missing/invalid Position and was reset to [0,0,0].");
                }

                if (trigger.Orientation == null)
                {
                    trigger.Orientation = new Vec3(0m, 0m, 0m);
                    issues.Add($"Triggers[{i}] had missing/invalid Orientation and was reset to [0,0,0].");
                }

                if (trigger.Size == null)
                {
                    trigger.Size = new Vec3(0m, 0m, 0m);
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
        public bool? CustommSpawn { get; set; }
        public BindingList<int>? ParentNetworkId { get; set; }
        public string? Comment { get; set; }
        public Vec3? Position { get; set; }
        public Vec3? Orientation { get; set; }
        public Vec3? Size { get; set; }
        public decimal EyeAccommodation { get; set; }
        public decimal? InterpolationSpeed { get; set; }
        public int? UseLinePointFade { get; set; }
        public string? AmbientSoundType { get; set; }
        public string? AmbientSoundSet { get; set; }
        public BindingList<Breadcrumb> Breadcrumbs { get; set; } = new();


        public EUndergroundTriggerType gettriggertype()
        {
            if (Breadcrumbs.Count() > 0) //TODO: simpler check
            {
                if (Breadcrumbs.Count() > 32)
                {
                    Console.WriteLine($"[ERROR]max 'Breadcrumb' count is 32, found:{Breadcrumbs.Count()}");
                }
                return EUndergroundTriggerType.TRANSITIONING;
            }
            else
            {
                if (EyeAccommodation == 1.0m)
                {
                    return EUndergroundTriggerType.OUTER;
                }
                else
                {
                    return EUndergroundTriggerType.INNER;
                }
            }
        }

        public bool Equals(Trigger? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return
                CustommSpawn == other.CustommSpawn &&
                Helper.ListEquals(ParentNetworkId, other.ParentNetworkId) &&
                Comment == other.Comment &&
                Equals(Position, other.Position) &&
                Equals(Orientation, other.Orientation) &&
                Equals(Size, other.Size) &&
                EyeAccommodation == other.EyeAccommodation &&
                UseLinePointFade == other.UseLinePointFade &&
                string.Equals(AmbientSoundType, other.AmbientSoundType, StringComparison.Ordinal) &&
                string.Equals(AmbientSoundSet, other.AmbientSoundSet, StringComparison.Ordinal) &&
                Helper.ListEquals(Breadcrumbs, other.Breadcrumbs) &&
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
                CustommSpawn = this.CustommSpawn,
                ParentNetworkId = this.ParentNetworkId,
                Comment = this.Comment,
                Position = this.Position.Clone(),
                Orientation = this.Orientation.Clone(),
                Size = this.Size.Clone(),
                EyeAccommodation = EyeAccommodation,
                InterpolationSpeed = InterpolationSpeed,
                UseLinePointFade = UseLinePointFade,
                AmbientSoundType = AmbientSoundType,
                AmbientSoundSet = AmbientSoundSet,
                Breadcrumbs = new BindingList<Breadcrumb>(Breadcrumbs.Select(x => x.Clone()).ToList())
            };
        }
    }

    public class Breadcrumb : IEquatable<Breadcrumb>, IDeepCloneable<Breadcrumb>
    {
        public Vec3? Position { get; set; }
        public decimal EyeAccommodation { get; set; }
        public int? UseRaycast { get; set; }
        public decimal? Radius { get; set; }
        public int? LightLerp { get; set; }
        public BreadcrumbExternalValueController? ExternalValueController { get; set; }

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

            if (!Equals(Position, other.Position))
                return false;
            if (EyeAccommodation != other.EyeAccommodation)
                return false;
            if (UseRaycast != other.UseRaycast)
                return false;
            if (Radius != other.Radius)
                return false;
            if (LightLerp != other.LightLerp)
                return false;
            if (!Equals(ExternalValueController, other.ExternalValueController))
                return false;

            return true;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Breadcrumb);
        }

        public Breadcrumb Clone()
        {
            return new Breadcrumb
            {
                Position = Position.Clone(),
                EyeAccommodation = EyeAccommodation,
                UseRaycast = UseRaycast,
                Radius = Radius,
                LightLerp = LightLerp,
                ExternalValueController = ExternalValueController?.Clone()
            };
        }
    }
    public class BreadcrumbExternalValueController : IEquatable<BreadcrumbExternalValueController>, IDeepCloneable<BreadcrumbExternalValueController>
    {
        public string Type { get; set; }
        public BindingList<string> Params { get; set; }
        public bool Equals(BreadcrumbExternalValueController? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (!Helper.ListEquals(Params, other.Params))
                return false;
            if (Type != other.Type)
                return false;

            return true;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as BreadcrumbExternalValueController);
        }

        public BreadcrumbExternalValueController Clone()
        {
            return new BreadcrumbExternalValueController
            {
                Type = Type,
                Params = new BindingList<string>(Params.ToList()),
            };
        }


    }



    public enum EUndergroundTriggerType
    {
        UNDEFINED,
        TRANSITIONING,
        OUTER,
        INNER
    }
}