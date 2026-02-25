using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionP2pMarketTradersConfig : MultiFileConfigLoader<ExpansionP2PMarketTraderConfig>
    {
        public ExpansionP2pMarketTradersConfig(string path) : base(path)
        {
        }
        protected override ExpansionP2PMarketTraderConfig LoadItem(string filePath)
        {
            var P2PTrader = AppServices.GetRequired<FileService>().LoadOrCreateJson(
                    filePath,
                    createNew: () => new ExpansionP2PMarketTraderConfig(),
                    onError: ex => HandleItemError(filePath, ex),
                    configName: "P2PTrader",
                    useVecConvertor: true
                );

            P2PTrader.SetPath(filePath);
            P2PTrader.SetGuid(Guid.NewGuid());
            return P2PTrader;
        }

        protected override void SaveItem(ExpansionP2PMarketTraderConfig P2PTrader)
        {
            AppServices.GetRequired<FileService>().SaveJson(P2PTrader._path, P2PTrader, false, true);
        }
        protected override string GetItemFileName(ExpansionP2PMarketTraderConfig P2PTrader)
            => P2PTrader.FileName;

        protected override bool ShouldDelete(ExpansionP2PMarketTraderConfig P2PTrader)
            => P2PTrader.ToDelete;
        protected override Guid GetID(ExpansionP2PMarketTraderConfig P2PTrader)
            => P2PTrader.Id;
        protected override void DeleteItemFile(ExpansionP2PMarketTraderConfig P2PTrader)
        {
            if (!string.IsNullOrWhiteSpace(P2PTrader._path) && File.Exists(P2PTrader._path))
                File.Delete(P2PTrader._path);
        }
        internal bool AddNewLoadoutFile(ExpansionP2PMarketTraderConfig P2PTrader)
        {
            bool exists = Items.Any(ld => ld.FileName.ToLower() == P2PTrader.FileName.ToLower());

            if (exists)
                return false; // File with same name already exists

            Items.Add(P2PTrader);
            return true;

        }
        internal void RemoveFile(ExpansionP2PMarketTraderConfig P2PTrader)
        {
            P2PTrader.ToDelete = true;
        }
        public bool needToSave()
        {
            return false;
        }
    }
    public class ExpansionP2PMarketTraderConfig : IDeepCloneable<ExpansionP2PMarketTraderConfig>, IEquatable<ExpansionP2PMarketTraderConfig>
    {
        const int VERSION = 8;

        [JsonIgnore]
        public string _path { get; private set; }
        [JsonIgnore]
        public string FileName => Path.GetFileName(_path);
        [JsonIgnore]
        public bool ToDelete { get; set; }
        [JsonIgnore]
        public Guid Id { get; set; }

        public void SetPath(string path) => _path = path;
        internal void SetGuid(Guid guid) => Id = guid;

        public int m_Version { get; set; }
        public int? m_TraderID { get; set; }
        public string? m_ClassName { get; set; }
        public Vec3? m_Position { get; set; }
        public Vec3? m_Orientation { get; set; }
        public string? m_LoadoutFile { get; set; }
        public Vec3? m_VehicleSpawnPosition { get; set; }
        public Vec3? m_WatercraftSpawnPosition { get; set; }
        public Vec3? m_AircraftSpawnPosition { get; set; }

        public string? m_DisplayName { get; set; }
        public string? m_DisplayIcon { get; set; }
        public string? m_Faction { get; set; }
        public BindingList<Vec3> m_Waypoints { get; set; }
        public int? m_EmoteID { get; set; }
        public int? m_EmoteIsStatic { get; set; }
        public string? m_RequiredFaction { get; set; }
        public int? m_UseReputation { get; set; }
        public int? m_MinRequiredReputation { get; set; }
        public int? m_MaxRequiredReputation { get; set; }
        public int? m_RequiredCompletedQuestID { get; set; }
        public int? m_IsGlobalTrader { get; set; }
        public BindingList<string> m_Currencies { get; set; }
        public int? m_DisplayCurrencyValue { get; set; }
        public string? m_DisplayCurrencyName { get; set; }


        public bool Equals(ExpansionP2PMarketTraderConfig other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (m_Version != other.m_Version ||
                m_TraderID != other.m_TraderID ||
                m_ClassName != other.m_ClassName ||
                m_Position != other.m_Position ||
                m_Orientation != other.m_Orientation ||
                m_LoadoutFile != other.m_LoadoutFile ||
                m_VehicleSpawnPosition != other.m_VehicleSpawnPosition ||
                m_WatercraftSpawnPosition != other.m_WatercraftSpawnPosition ||
                m_AircraftSpawnPosition != other.m_AircraftSpawnPosition ||
                m_DisplayName != other.m_DisplayName ||
                m_DisplayIcon != other.m_DisplayIcon ||
                m_Faction != other.m_Faction ||
                m_EmoteID != other.m_EmoteID ||
                m_EmoteIsStatic != other.m_EmoteIsStatic ||
                m_RequiredFaction != other.m_RequiredFaction ||
                m_UseReputation != other.m_UseReputation ||
                m_MinRequiredReputation != other.m_MinRequiredReputation ||
                m_MaxRequiredReputation != other.m_MaxRequiredReputation ||
                m_RequiredCompletedQuestID != other.m_RequiredCompletedQuestID ||
                m_IsGlobalTrader != other.m_IsGlobalTrader ||
                m_DisplayCurrencyValue != other.m_DisplayCurrencyValue ||
                m_DisplayCurrencyName != other.m_DisplayCurrencyName)
            {
                return false;
            }

            if (!ListEquals(m_Waypoints, other.m_Waypoints))
                return false;

            if (!ListEquals(m_Currencies, other.m_Currencies))
                return false;

            return true;
        }
        private static bool ListEquals<T>(IList<T> a, IList<T> b)
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
        public override bool Equals(object? obj) => Equals(obj as ExpansionP2PMarketTraderConfig);
        public ExpansionP2PMarketTraderConfig Clone()
        {
            return new ExpansionP2PMarketTraderConfig
            {
                m_Version = this.m_Version,
                m_TraderID = this.m_TraderID,
                m_ClassName = this.m_ClassName,

                m_Position = this.m_Position?.Clone(),
                m_Orientation = this.m_Orientation?.Clone(),

                m_LoadoutFile = this.m_LoadoutFile,
                m_VehicleSpawnPosition = this.m_VehicleSpawnPosition?.Clone(),
                m_WatercraftSpawnPosition = this.m_WatercraftSpawnPosition?.Clone(),
                m_AircraftSpawnPosition = this.m_AircraftSpawnPosition?.Clone(),

                m_DisplayName = this.m_DisplayName,
                m_DisplayIcon = this.m_DisplayIcon,
                m_Faction = this.m_Faction,

                m_Waypoints = new BindingList<Vec3>(
                    this.m_Waypoints?.Select(w => w.Clone()).ToList()
                    ?? new List<Vec3>()
                ),

                m_EmoteID = this.m_EmoteID,
                m_EmoteIsStatic = this.m_EmoteIsStatic,
                m_RequiredFaction = this.m_RequiredFaction,
                m_UseReputation = this.m_UseReputation,
                m_MinRequiredReputation = this.m_MinRequiredReputation,
                m_MaxRequiredReputation = this.m_MaxRequiredReputation,
                m_RequiredCompletedQuestID = this.m_RequiredCompletedQuestID,
                m_IsGlobalTrader = this.m_IsGlobalTrader,

                m_Currencies = new BindingList<string>(
                    this.m_Currencies?.ToList() ?? new List<string>()
                ),

                m_DisplayCurrencyValue = this.m_DisplayCurrencyValue,
                m_DisplayCurrencyName = this.m_DisplayCurrencyName
            };
        }

    }
}
