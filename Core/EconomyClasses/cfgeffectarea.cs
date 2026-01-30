using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Day2eEditor
{
    public class cfgeffectareaConfig : IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path); // e.g., "types.xml"
        public string FilePath => _path;
        public cfgeffectarea Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }
        public cfgeffectareaConfig(string path)
        {
            _path = path ?? throw new ArgumentNullException(nameof(path));
        }
        public void Load()
        {
            Data = AppServices.GetRequired<FileService>().LoadOrCreateJson<cfgeffectarea>(
                _path,
                createNew: () => new cfgeffectarea(),
                onError: ex => LogError("cfgeffectarea", ex),
                configName: "cfgeffectarea",
                useBoolConvertor: true
            );
            convertpositionstolist();
        }
        public IEnumerable<string> Save()
        {
            if (isDirty)
            {
                convertlisttopositions();
                AppServices.GetRequired<FileService>().SaveJson(_path, Data);
                isDirty = false;
                return new[] { Path.GetFileName(_path) };
            }

            return Array.Empty<string>();
        }
        public bool needToSave()
        {
            return isDirty;
        }
        public void convertpositionstolist()
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
        public void convertlisttopositions()
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
        private void LogError(string context, Exception ex)
        {
            HasErrors = true;
            string message = $"Error in {context}\n{ex.Message}";
            if (ex.InnerException != null)
                message += $"\n{ex.InnerException.Message}";

            Console.WriteLine(message);
            Errors.Add(message);
        }
    }
    public class cfgeffectarea
    {
        public BindingList<Areas> Areas { get; set; }
        public BindingList<decimal[]> SafePositions { get; set; }
        [JsonIgnore]
        public BindingList<cfgeffectareaSafePosition> _positions { get; set; }
    }
    public class cfgeffectareaSafePosition
    {
        public decimal X { get; set; }
        public decimal Z { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
    public class Areas
    {
        public string AreaName { get; set; }
        public string Type { get; set; }
        public string TriggerType { get; set; }
        public Data Data { get; set; }
        public PlayerData PlayerData { get; set; }

        public override string ToString()
        {
            return AreaName;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Areas other)
                return false;

            return AreaName == other.AreaName && Type == other.Type && TriggerType == other.TriggerType;
        }
    }
    public class Data
    {
        public decimal[] Pos { get; set; }
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
        public string ParticleName { get; set; }
        public int? EffectInterval { get; set; }
        public int? EffectDuration { get; set; }
        public bool? EffectModifier { get; set; }

        public void SetIntValue(string mytype, int myvalue)
        {
            GetType().GetProperty(mytype).SetValue(this, myvalue, null);
        }
        public void SetdecimalValue(string mytype, decimal myvalue)
        {
            GetType().GetProperty(mytype).SetValue(this, myvalue, null);
        }
        public override bool Equals(object obj)
        {
            if (obj is not Data other) return false;

            return
                (Pos?.SequenceEqual(other.Pos) ?? other.Pos == null) &&
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
                ParticleName == other.ParticleName &&
                EffectInterval == other.EffectInterval &&
                EffectDuration == other.EffectDuration &&
                EffectModifier == other.EffectModifier;
        }
    }
    public class PlayerData
    {
        public string AroundPartName { get; set; }
        public string TinyPartName { get; set; }
        public string PPERequesterType { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is not PlayerData other)
                return false;

            return AroundPartName == other.AroundPartName && TinyPartName == other.TinyPartName && PPERequesterType == other.PPERequesterType;
        }
    }
}
