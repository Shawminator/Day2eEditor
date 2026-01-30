using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionTerritoryConfig : ExpansionBaseIConfigLoader<ExpansionTerritorySettings>
    {
        public const int CurrentVersion = 6;

        public ExpansionTerritoryConfig(string path) : base(path)
        {
        }
        protected override ExpansionTerritorySettings CreateDefaultData()
        {
            return new ExpansionTerritorySettings(CurrentVersion);
        }
        protected override IEnumerable<string> ValidateData()
        {
            return Data.FixMissingOrInvalidFields();
        }
    }
    public class ExpansionTerritorySettings : IEquatable<ExpansionTerritorySettings>, IDeepCloneable<ExpansionTerritorySettings>
    {
        public int m_Version { get; set; }
        public int? EnableTerritories { get; set; }                //! If enabled, use the expansion territory system
        public int? UseWholeMapForInviteList { get; set; }      //! Use it if you want whole map available in invite list, instead only nearby players.
        public decimal? TerritorySize { get; set; }                        //! The radius of a territory in meters.
        public decimal? TerritoryPerimeterSize { get; set; }         //! The radius who prevent territories to overlap
        public int? MaxMembersInTerritory { get; set; }          //! If <= 0, unlimited territory size.
        public int? MaxTerritoryPerPlayer { get; set; }
        public decimal? TerritoryInviteAcceptRadius { get; set; }  //! Players need to be in this radius to be able to accept a territory invite
        public int? AuthenticateCodeLockIfTerritoryMember { get; set; }  //! Territory members don't have to enter code on codelocks in territory
        //! Added with version 4
        public int? InviteCooldown { get; set; }
        //! Added with version 5
        public int? OnlyInviteGroupMember { get; set; }
        public int? MaxCodeLocksOnBBPerTerritory { get; set; }
        public int? MaxCodeLocksOnItemsPerTerritory { get; set; }//! If <= 0, unlimited territory number.


        public ExpansionTerritorySettings() { }
        public ExpansionTerritorySettings(int CurrentVersion)
        {
            m_Version = CurrentVersion;
        }

        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            if (m_Version != ExpansionTerritoryConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionTerritoryConfig.CurrentVersion}");
                m_Version = ExpansionTerritoryConfig.CurrentVersion;
            }
            if (EnableTerritories is null and < 0 or > 1)
            {
                fixes.Add("EnableTerritories set to true.");
                EnableTerritories = 1;
            }
            if (UseWholeMapForInviteList is null and < 0 or > 1)
            {
                fixes.Add("UseWholeMapForInviteList set to false.");
                UseWholeMapForInviteList = 0;
            }
            if (TerritorySize is null)
            {
                fixes.Add("TerritorySize set to 150.");
                TerritorySize = 150;
            }
            if (TerritoryPerimeterSize is null)
            {
                fixes.Add("TerritoryPerimeterSize set to 150.");
                TerritoryPerimeterSize = 150;
            }
            if (MaxMembersInTerritory is null)
            {
                fixes.Add("MaxMembersInTerritory set to 10.");
                MaxMembersInTerritory = 10;
            }
            if (MaxTerritoryPerPlayer is null)
            {
                fixes.Add("MaxTerritoryPerPlayer set to 1.");
                MaxTerritoryPerPlayer = 1;
            }
            if (TerritoryInviteAcceptRadius is null)
            {
                fixes.Add("TerritoryInviteAcceptRadius set to 150.");
                TerritoryInviteAcceptRadius = 150;
            }
            if (AuthenticateCodeLockIfTerritoryMember is null and < 0 or > 1)
            {
                fixes.Add("AuthenticateCodeLockIfTerritoryMember set to false.");
                AuthenticateCodeLockIfTerritoryMember = 0;
            }
            if (InviteCooldown is null)
            {
                fixes.Add("InviteCooldown set to 0.");
                InviteCooldown = 0;
            }
            if (OnlyInviteGroupMember is null and < 0 or > 1)
            {
                fixes.Add("OnlyInviteGroupMember set to false.");
                OnlyInviteGroupMember = 0;
            }
            if (MaxCodeLocksOnBBPerTerritory is null)
            {
                fixes.Add("MaxCodeLocksOnBBPerTerritory set to -1.");
                MaxCodeLocksOnBBPerTerritory = -1;
            }
            if (MaxCodeLocksOnItemsPerTerritory is null)
            {
                fixes.Add("MaxCodeLocksOnItemsPerTerritory set to -1.");
                MaxCodeLocksOnItemsPerTerritory = -1;
            }
            return fixes;
        }
        public bool Equals(ExpansionTerritorySettings other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (m_Version != other.m_Version ||
                EnableTerritories != other.EnableTerritories ||
                UseWholeMapForInviteList != other.UseWholeMapForInviteList ||
                TerritorySize != other.TerritorySize ||
                TerritoryPerimeterSize != other.TerritoryPerimeterSize ||
                MaxMembersInTerritory != other.MaxMembersInTerritory ||
                MaxTerritoryPerPlayer != MaxTerritoryPerPlayer ||
                TerritoryInviteAcceptRadius != other.TerritoryInviteAcceptRadius ||
                AuthenticateCodeLockIfTerritoryMember != other.AuthenticateCodeLockIfTerritoryMember ||
                InviteCooldown != other.InviteCooldown ||
                OnlyInviteGroupMember != other.OnlyInviteGroupMember ||
                MaxCodeLocksOnBBPerTerritory != other.MaxCodeLocksOnBBPerTerritory ||
                MaxCodeLocksOnItemsPerTerritory != other.MaxCodeLocksOnItemsPerTerritory)
                return false;

            return true;

        }
        public override bool Equals(object? obj) => Equals(obj as ExpansionTerritorySettings);
        public ExpansionTerritorySettings Clone()
        {
            return new ExpansionTerritorySettings
            {
                m_Version = m_Version,
                EnableTerritories = this.EnableTerritories,
                UseWholeMapForInviteList = this.UseWholeMapForInviteList,
                TerritorySize = this.TerritorySize,
                TerritoryPerimeterSize = this.TerritoryPerimeterSize,
                MaxMembersInTerritory = this.MaxMembersInTerritory,
                MaxTerritoryPerPlayer = this.MaxTerritoryPerPlayer,
                TerritoryInviteAcceptRadius = this.TerritoryInviteAcceptRadius,
                AuthenticateCodeLockIfTerritoryMember = this.AuthenticateCodeLockIfTerritoryMember,
                InviteCooldown = this.InviteCooldown,
                OnlyInviteGroupMember = this.OnlyInviteGroupMember,
                MaxCodeLocksOnBBPerTerritory = this.MaxCodeLocksOnBBPerTerritory,
                MaxCodeLocksOnItemsPerTerritory = MaxCodeLocksOnItemsPerTerritory
            };
        }
    }
}
