using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Day2eEditor
{
    public class CfgeffectareaConfig : SingleFileConfigLoaderBase<Cfgeffectarea>
    {
        public CfgeffectareaConfig(string path) : base(path)
        {
        }
        protected override Cfgeffectarea CreateDefaultData()
        {
            return new Cfgeffectarea();
        }
        public override IEnumerable<string> Save()
        {
            if (Data is null)
                return Array.Empty<string>();

            if (!AreEqual(Data, ClonedData) || IsDirty == true)
            {
                ClearDirty();
                ConvertListToPositions();
                AppServices.GetRequired<FileService>().SaveJson(_path, Data);
                ClonedData = CloneData(Data);
                return new[] { Path.GetFileName(_path) };
            }

            return Array.Empty<string>();
        }
        protected override IEnumerable<string> ValidateData()
        {
            return Data?.FixMissingOrInvalidFields() ?? Enumerable.Empty<string>();
        }
        public void ConvertPositionsToList()
        {
            if (Data.SafePositions != null)
            {
                Data._positions = new BindingList<cfgeffectareaSafePosition>();
                for (int i = 0; i < Data.SafePositions.Count; i++)
                {
                    Data._positions.Add(new cfgeffectareaSafePosition()
                    {
                        X = Data.SafePositions[i][0],
                        Z = Data.SafePositions[i][1],
                        Name = Data.SafePositions[i][0].ToString("0.##") + "," + Data.SafePositions[i][1].ToString("0.##")
                    }
                    );
                }
                Data.SafePositions = null;
            }
        }
        public void ConvertListToPositions()
        {
            if (Data._positions != null)
            {
                Data.SafePositions = new BindingList<decimal[]>();
                foreach (cfgeffectareaSafePosition pos in Data._positions)
                {
                    Data.SafePositions.Add(new decimal[] { pos.X, pos.Z });
                }
            }
            else
            {
                Data.SafePositions = null;
            }
        }
        protected override void OnAfterLoad(Cfgeffectarea data)
        {
            ConvertPositionsToList();
        }
    }
    public class Cfgeffectarea : IEquatable<Cfgeffectarea>, IDeepCloneable<Cfgeffectarea>
    {
        public BindingList<Areas> Areas { get; set; } = new();
        public BindingList<decimal[]> SafePositions { get; set; } = new();

        [JsonIgnore]
        public BindingList<cfgeffectareaSafePosition> _positions { get; set; } = new();

        internal IEnumerable<string> FixMissingOrInvalidFields()
        {
            var issues = new List<string>();

            Areas ??= new BindingList<Areas>();
            SafePositions ??= new BindingList<decimal[]>();
            _positions ??= new BindingList<cfgeffectareaSafePosition>();

            for (int i = 0; i < Areas.Count; i++)
            {
                var area = Areas[i];
                if (area == null)
                {
                    issues.Add($"Areas[{i}] was null and could not be repaired.");
                    continue;
                }

                area.Data ??= new Data();
                area.PlayerData ??= new PlayerData();

                if (string.IsNullOrWhiteSpace(area.AreaName))
                    issues.Add($"Areas[{i}] is missing AreaName.");

                if (area.Data.Pos == null || area.Data.Pos.Length != 3)
                {
                    area.Data.Pos = new decimal[] { 0m, 0m, 0m };
                    issues.Add($"Areas[{i}] had missing/invalid Pos and was reset to [0,0,0].");
                }
            }

            for (int i = SafePositions.Count - 1; i >= 0; i--)
            {
                var pos = SafePositions[i];
                if (pos == null || pos.Length < 2)
                {
                    SafePositions.RemoveAt(i);
                    issues.Add($"SafePositions[{i}] was invalid and was removed.");
                }
            }

            return issues;
        }

        public bool Equals(Cfgeffectarea? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return
                AreasEqual(Areas, other.Areas) &&
                PositionsEqual(_positions, other._positions);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Cfgeffectarea);
        }

        public Cfgeffectarea Clone()
        {
            return new Cfgeffectarea
            {
                Areas = new BindingList<Areas>(Areas?.Select(x => x.Clone()).ToList() ?? new List<Areas>()),
                SafePositions = new BindingList<decimal[]>(SafePositions?.Select(x => x?.ToArray() ?? Array.Empty<decimal>()).ToList()?? new List<decimal[]>()),
                _positions = new BindingList<cfgeffectareaSafePosition>(_positions?.Select(x => x.Clone()).ToList() ?? new List<cfgeffectareaSafePosition>())
            };
        }

        private static bool AreasEqual(IList<Areas>? a, IList<Areas>? b)
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

        private static bool SafePositionsEqual(IList<decimal[]>? a, IList<decimal[]>? b)
        {
            if (ReferenceEquals(a, b))
                return true;
            if (a is null || b is null)
                return false;
            if (a.Count != b.Count)
                return false;

            for (int i = 0; i < a.Count; i++)
            {
                var left = a[i];
                var right = b[i];

                if (ReferenceEquals(left, right))
                    continue;
                if (left is null || right is null)
                    return false;
                if (!left.SequenceEqual(right))
                    return false;
            }

            return true;
        }

        private static bool PositionsEqual(IList<cfgeffectareaSafePosition>? a, IList<cfgeffectareaSafePosition>? b)
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

    public class cfgeffectareaSafePosition : IEquatable<cfgeffectareaSafePosition>, IDeepCloneable<cfgeffectareaSafePosition>
    {
        public decimal X { get; set; }
        public decimal Z { get; set; }
        public string? Name { get; set; }

        public override string ToString()
        {
            return Name ?? string.Empty;
        }

        public bool Equals(cfgeffectareaSafePosition? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return
                X == other.X &&
                Z == other.Z &&
                string.Equals(Name, other.Name, StringComparison.Ordinal);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as cfgeffectareaSafePosition);
        }

        public cfgeffectareaSafePosition Clone()
        {
            return new cfgeffectareaSafePosition
            {
                X = X,
                Z = Z,
                Name = Name
            };
        }
    }

    public class Areas : IEquatable<Areas>, IDeepCloneable<Areas>
    {
        public string? AreaName { get; set; }
        public string? Type { get; set; }
        public string? TriggerType { get; set; }
        public Data? Data { get; set; }
        public PlayerData? PlayerData { get; set; }

        public override string ToString()
        {
            return AreaName ?? string.Empty;
        }

        public bool Equals(Areas? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return
                string.Equals(AreaName, other.AreaName, StringComparison.Ordinal) &&
                string.Equals(Type, other.Type, StringComparison.Ordinal) &&
                string.Equals(TriggerType, other.TriggerType, StringComparison.Ordinal) &&
                Equals(Data, other.Data) &&
                Equals(PlayerData, other.PlayerData);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Areas);
        }

        public Areas Clone()
        {
            return new Areas
            {
                AreaName = AreaName,
                Type = Type,
                TriggerType = TriggerType,
                Data = Data?.Clone(),
                PlayerData = PlayerData?.Clone()
            };
        }
    }

    public class Data : IEquatable<Data>, IDeepCloneable<Data>
    {
        public decimal[]? Pos { get; set; }
        public decimal Radius { get; set; }
        public decimal PosHeight { get; set; }
        public decimal NegHeight { get; set; }
        public int? InnerRingCount { get; set; }
        public int? InnerPartDist { get; set; }
        public bool? OuterRingToggle { get; set; }
        public int? OuterPartDist { get; set; }
        public int? OuterOffset { get; set; }
        public int? VerticalLayers { get; set; }
        public int? VerticalOffset { get; set; }
        public string? ParticleName { get; set; }
        public int? EffectInterval { get; set; }
        public int? EffectDuration { get; set; }
        public bool? EffectModifier { get; set; }

        public void SetIntValue(string mytype, int myvalue)
        {
            GetType().GetProperty(mytype)?.SetValue(this, myvalue, null);
        }

        public void SetDecimalValue(string mytype, decimal myvalue)
        {
            GetType().GetProperty(mytype)?.SetValue(this, myvalue, null);
        }

        public bool Equals(Data? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return
                ((Pos?.SequenceEqual(other.Pos ?? Array.Empty<decimal>()) ?? other.Pos == null)) &&
                Radius == other.Radius &&
                PosHeight == other.PosHeight &&
                NegHeight == other.NegHeight &&
                InnerRingCount == other.InnerRingCount &&
                InnerPartDist == other.InnerPartDist &&
                OuterRingToggle == other.OuterRingToggle &&
                OuterPartDist == other.OuterPartDist &&
                OuterOffset == other.OuterOffset &&
                VerticalLayers == other.VerticalLayers &&
                VerticalOffset == other.VerticalOffset &&
                string.Equals(ParticleName, other.ParticleName, StringComparison.Ordinal) &&
                EffectInterval == other.EffectInterval &&
                EffectDuration == other.EffectDuration &&
                EffectModifier == other.EffectModifier;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Data);
        }

        public Data Clone()
        {
            return new Data
            {
                Pos = Pos != null ? (decimal[])Pos.Clone() : null,
                Radius = Radius,
                PosHeight = PosHeight,
                NegHeight = NegHeight,
                InnerRingCount = InnerRingCount,
                InnerPartDist = InnerPartDist,
                OuterRingToggle = OuterRingToggle,
                OuterPartDist = OuterPartDist,
                OuterOffset = OuterOffset,
                VerticalLayers = VerticalLayers,
                VerticalOffset = VerticalOffset,
                ParticleName = ParticleName,
                EffectInterval = EffectInterval,
                EffectDuration = EffectDuration,
                EffectModifier = EffectModifier
            };
        }
    }

    public class PlayerData : IEquatable<PlayerData>, IDeepCloneable<PlayerData>
    {
        public string? AroundPartName { get; set; }
        public string? TinyPartName { get; set; }
        public string? PPERequesterType { get; set; }

        public bool Equals(PlayerData? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return
                string.Equals(AroundPartName, other.AroundPartName, StringComparison.Ordinal) &&
                string.Equals(TinyPartName, other.TinyPartName, StringComparison.Ordinal) &&
                string.Equals(PPERequesterType, other.PPERequesterType, StringComparison.Ordinal);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as PlayerData);
        }

        public PlayerData Clone()
        {
            return new PlayerData
            {
                AroundPartName = AroundPartName,
                TinyPartName = TinyPartName,
                PPERequesterType = PPERequesterType
            };
        }
    }
}

