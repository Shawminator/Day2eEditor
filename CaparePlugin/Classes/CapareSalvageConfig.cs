using Day2eEditor;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CaparePlugin
{
    /// <summary>
    /// $profile:Capare_Workbench/CapareSalvageConfig.json
    /// Capare_WorkBench - CapareSalvageConfig.c
    /// </summary>
    public class CapareSalvageConfig : CapareConfigBase<CapareSalvageData>
    {
        public CapareSalvageConfig(string path) : base(path)
        {
        }
        protected override CapareSalvageData CreateDefaultData()
        {
            return CapareSalvageData.CreateDefault();
        }
        protected override IEnumerable<string> ValidateData()
        {
            return Data.FixMissingOrInvalidFields();
        }
    }

    public class CapareSalvageData : IEquatable<CapareSalvageData>, IDeepCloneable<CapareSalvageData>
    {
        // Baskets ScrapSalvageRules knows about
        public static readonly string[] Baskets =
        {
            "Scrap_WeaponParts_Basket",
            "Scrap_Electronics_Basket",
            "Scrap_MechanicalParts_Basket",
            "Scrap_Wire_Basket",
            "Scrap_Springs_Basket",
            "Scrap_General_Basket",
            "Scrap_Fabric_Basket",
            "Scrap_Leather_Basket"
        };

        // classname -> basket, checked first
        [JsonConverter(typeof(CapareSalvageOverrideListConverter))]
        public BindingList<CapareSalvageOverride> SalvageOverrides { get; set; }
        // basket -> scrap per gram
        [JsonConverter(typeof(CapareSalvageYieldRateListConverter))]
        public BindingList<CapareSalvageYieldRate> SalvageYieldRates { get; set; }
        public CapareSalvageTimingSettings SalvageTiming { get; set; }
        // Never salvageable. Matched with IsKindOf, so a base class covers every variant
        public BindingList<string> SalvageExclusions { get; set; }

        public CapareSalvageData()
        {
        }

        // Same values DefaultRates() and DefaultExclusions() write in the mod
        public static CapareSalvageData CreateDefault()
        {
            var data = new CapareSalvageData()
            {
                SalvageOverrides = new BindingList<CapareSalvageOverride>(),
                SalvageYieldRates = new BindingList<CapareSalvageYieldRate>(),
                SalvageTiming = new CapareSalvageTimingSettings(),
                SalvageExclusions = new BindingList<string>()
            };
            data.SalvageOverrides.Add(new CapareSalvageOverride("MetalWire", "Scrap_Wire_Basket"));
            data.SalvageOverrides.Add(new CapareSalvageOverride("BarbedWire", "Scrap_Wire_Basket"));
            data.SalvageOverrides.Add(new CapareSalvageOverride("CableReel", "Scrap_Wire_Basket"));
            foreach (string metalItem in new[] { "Hammer", "Wrench", "LugWrench", "PipeWrench", "Crowbar", "SledgeHammer", "Pipe", "CanOpener", "Shovel", "FieldShovel",
                "Pickaxe", "Iceaxe", "FarmingHoe", "Pitchfork", "MeatTenderizer", "FryingPan", "Pot", "CanisterGasoline", "Canteen", "Mace" })
            {
                data.SalvageOverrides.Add(new CapareSalvageOverride(metalItem, "Scrap_MechanicalParts_Basket"));
            }
            data.SalvageOverrides.Add(new CapareSalvageOverride("WaterBottle", "Scrap_General_Basket"));
            data.SalvageOverrides.Add(new CapareSalvageOverride("GlassBottle", "Scrap_General_Basket"));
            data.SalvageOverrides.Add(new CapareSalvageOverride("FilteringBottle", "Scrap_General_Basket"));

            data.SalvageYieldRates.Add(new CapareSalvageYieldRate("Scrap_WeaponParts_Basket", 0.05f));
            data.SalvageYieldRates.Add(new CapareSalvageYieldRate("Scrap_Electronics_Basket", 0.05f));
            data.SalvageYieldRates.Add(new CapareSalvageYieldRate("Scrap_MechanicalParts_Basket", 0.04f));
            data.SalvageYieldRates.Add(new CapareSalvageYieldRate("Scrap_Wire_Basket", 0.03f));
            data.SalvageYieldRates.Add(new CapareSalvageYieldRate("Scrap_Springs_Basket", 0.05f));
            data.SalvageYieldRates.Add(new CapareSalvageYieldRate("Scrap_General_Basket", 0.02f));
            data.SalvageYieldRates.Add(new CapareSalvageYieldRate("Scrap_Fabric_Basket", 0.03f));
            data.SalvageYieldRates.Add(new CapareSalvageYieldRate("Scrap_Leather_Basket", 0.03f));

            foreach (string exclusion in new[] { "ExplosivesBase", "LandMineTrap", "RemoteDetonator", "ShippingContainerKeys_ColorBase", "HandcuffKeys",
                "ScientificBriefcaseKeys", "PunchedCard", "BaseBuildingBase", "FenceKit", "SeaChest", "CarScript", "CarDoor", "CarWheel", "CarBattery",
                "TruckBattery", "SparkPlug", "GlowPlug", "EngineOil", "CarRadiator", "Edible_Base" })
            {
                data.SalvageExclusions.Add(exclusion);
            }
            return data;
        }

        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();
            if (SalvageOverrides == null)
            {
                SalvageOverrides = new BindingList<CapareSalvageOverride>();
                fixes.Add("Initialized SalvageOverrides");
            }
            if (SalvageYieldRates == null)
            {
                SalvageYieldRates = new BindingList<CapareSalvageYieldRate>();
                fixes.Add("Initialized SalvageYieldRates");
            }
            if (SalvageTiming == null)
            {
                SalvageTiming = new CapareSalvageTimingSettings();
                fixes.Add("Initialized SalvageTiming");
            }
            if (SalvageExclusions == null)
            {
                SalvageExclusions = new BindingList<string>();
                fixes.Add("Initialized SalvageExclusions");
            }
            return fixes;
        }

        public bool Equals(CapareSalvageData other) => CapareJson.AreEqual(this, other);
        public override bool Equals(object? obj) => Equals(obj as CapareSalvageData);
        public override int GetHashCode() => base.GetHashCode();
        public CapareSalvageData Clone() => CapareJson.Clone(this);
    }
    public class CapareSalvageOverride
    {
        public string ClassName { get; set; }
        public string Basket { get; set; }

        public CapareSalvageOverride() { }
        public CapareSalvageOverride(string className, string basket)
        {
            ClassName = className;
            Basket = basket;
        }
        public override string ToString() => $"{ClassName} -> {Basket}";
    }
    public class CapareSalvageYieldRate
    {
        public string Basket { get; set; }
        public float Rate { get; set; }

        public CapareSalvageYieldRate() { }
        public CapareSalvageYieldRate(string basket, float rate)
        {
            Basket = basket;
            Rate = rate;
        }
        public override string ToString() => $"{Basket}: {Rate}";
    }
    // Salvage progress-bar length:
    //   BaseSeconds + scrap yield * SecondsPerScrap + item kg * SecondsPerKg, clamped to MinSeconds..MaxSeconds
    public class CapareSalvageTimingSettings
    {
        public float BaseSeconds { get; set; } = 5;
        public float SecondsPerScrap { get; set; } = 0.05f;
        public float SecondsPerKg { get; set; } = 1;
        public float MinSeconds { get; set; } = 5;
        public float MaxSeconds { get; set; } = 60;
    }

    // The mod stores these as DayZ maps (json objects); a list keeps the file order and lets the key be edited
    public class CapareSalvageOverrideListConverter : JsonConverter<BindingList<CapareSalvageOverride>>
    {
        public override BindingList<CapareSalvageOverride> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var list = new BindingList<CapareSalvageOverride>();
            foreach (var pair in JsonSerializer.Deserialize<Dictionary<string, string>>(ref reader, options) ?? new())
                list.Add(new CapareSalvageOverride(pair.Key, pair.Value));
            return list;
        }
        public override void Write(Utf8JsonWriter writer, BindingList<CapareSalvageOverride> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            foreach (var entry in value.GroupBy(x => x.ClassName).Select(g => g.Last()))
                writer.WriteString(entry.ClassName ?? "", entry.Basket ?? "");
            writer.WriteEndObject();
        }
    }
    public class CapareSalvageYieldRateListConverter : JsonConverter<BindingList<CapareSalvageYieldRate>>
    {
        public override BindingList<CapareSalvageYieldRate> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var list = new BindingList<CapareSalvageYieldRate>();
            foreach (var pair in JsonSerializer.Deserialize<Dictionary<string, float>>(ref reader, options) ?? new())
                list.Add(new CapareSalvageYieldRate(pair.Key, pair.Value));
            return list;
        }
        public override void Write(Utf8JsonWriter writer, BindingList<CapareSalvageYieldRate> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            foreach (var entry in value.GroupBy(x => x.Basket).Select(g => g.Last()))
                writer.WriteNumber(entry.Basket ?? "", entry.Rate);
            writer.WriteEndObject();
        }
    }
}
