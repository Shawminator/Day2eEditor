using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionPartyConfig : IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path); // e.g., "types.xml"
        public string FilePath => _path;
        public ExpansionPartySettings Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }
        public const int CurrentVersion = 8;

        public ExpansionPartyConfig(string path)
        {
            _path = path;
        }
        public void Load()
        {
            Data = null;
            Data = AppServices.GetRequired<FileService>().LoadOrCreateJson<ExpansionPartySettings>(
                _path,
                createNew: () => new ExpansionPartySettings(CurrentVersion),
                onAfterLoad: cfg => { },
                onError: ex =>
                {
                    HasErrors = true;
                    Console.WriteLine(
                        "Error in " + Path.GetFileName(_path) + "\n" +
                        ex.Message + "\n" +
                        ex.InnerException?.Message + "\n"
                    );
                    Errors.Add("Error in " + Path.GetFileName(_path) + "\n" +
                        ex.Message + "\n" +
                        ex.InnerException?.Message);
                },
                configName: "ExpansionParty"
            );
            var missingFields = Data.FixMissingOrInvalidFields();
            if (missingFields.Any())
            {
                Console.WriteLine("Validation issues in " + FileName + ":");
                foreach (var issue in missingFields)
                {
                    Console.WriteLine("- " + issue);
                }
                isDirty = true;
            }
        }
        public IEnumerable<string> Save()
        {
            if (isDirty)
            {
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
    }
    public class ExpansionPartySettings
    {
        public int m_Version{ get; set; }
        public int? EnableParties{ get; set; } //! Enable party module, allow players to create parties
        public int? MaxMembersInParty{ get; set; } //! If <= 0, unlimited party size
        public int? UseWholeMapForInviteList{ get; set; } //! Use it if you want whole map available in invite list, instead only nearby players

        public int? ShowPartyMember3DMarkers{ get; set; } //! If enabled, allow to see 3D marker above teammates location
        public int? ShowDistanceUnderPartyMembersMarkers{ get; set; } //! Show the distance of the party member marker
        public int? ShowNameOnPartyMembersMarkers{ get; set; } //! Show the name of the party member marker
        public int? EnableQuickMarker{ get; set; } //! Enable/Diable quick marker option
        public int? ShowDistanceUnderQuickMarkers{ get; set; } //! Show the distance of the quick marker
        public int? ShowNameOnQuickMarkers{ get; set; } //! Show the distance of the quick marker
        public int? CanCreatePartyMarkers{ get; set; } //! Allow player to create party markers

        //! Added with version 2
        public int? ShowPartyMemberHUD{ get; set; } //! Show the party hud interface that displays eacht party members name and health

        //! Added with version 3
        public int? ShowHUDMemberBlood{ get; set; }
        public int? ShowHUDMemberStates{ get; set; }
        public int? ShowHUDMemberStance{ get; set; }

        //! Added with version 4
        public int? ShowPartyMemberMapMarkers{ get; set; }

        //! Added with version 5
        public int? ShowHUDMemberDistance{ get; set; }

        //! Added with version 6
        public int? ForcePartyToHaveTags{ get; set; }

        //! Added with version 7
        public int? InviteCooldown{ get; set; }

        //! Added with version 8
        public int? DisplayPartyTag{ get; set; }

        public ExpansionPartySettings()
        {
        }
        public ExpansionPartySettings(int CurrentVersion)
        {
            m_Version = CurrentVersion;

            //! Added with version 1
            EnableParties = 1;
            MaxMembersInParty = 10;
            UseWholeMapForInviteList = 0;

            ShowPartyMember3DMarkers = 1;
            ShowDistanceUnderPartyMembersMarkers = 1;
            ShowNameOnPartyMembersMarkers = 1;
            EnableQuickMarker = 1;
            ShowDistanceUnderQuickMarkers = 1;
            ShowNameOnQuickMarkers = 1;
            CanCreatePartyMarkers = 1;

            //! Added with version 2
            ShowPartyMemberHUD = 1;

            //! Added with version 3
            ShowHUDMemberBlood = 1;
            ShowHUDMemberStates = 1;
            ShowHUDMemberStance = 1;

            //! Added with version 4
            ShowPartyMemberMapMarkers = 1;

            //! Added with version 5
            ShowHUDMemberDistance = 1;

            //! Added with version 6
            ForcePartyToHaveTags = 0;

            //! Added with version 7
            InviteCooldown = 0;

            //! Added with version 8
            DisplayPartyTag = 1;
        }

        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            if (m_Version < ExpansionPartyConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionPartyConfig.CurrentVersion}");
                m_Version = ExpansionPartyConfig.CurrentVersion;
            }
            if (EnableParties is null or < 0 or > 1)
            {
                EnableParties = 1;
                fixes.Add("Corrected EnableParties");
            }
            if (MaxMembersInParty is null)
            {
                MaxMembersInParty = 10;
                fixes.Add("Corrected MaxMembersInParty");
            }
            if (UseWholeMapForInviteList is null or < 0 or > 1)
            {
                UseWholeMapForInviteList = 0;
                fixes.Add("Corrected UseWholeMapForInviteList");
            }
            if (ShowPartyMember3DMarkers is null or < 0 or > 1)
            {
                ShowPartyMember3DMarkers = 1;
                fixes.Add("Corrected ShowPartyMember3DMarkers");
            }
            if (ShowDistanceUnderPartyMembersMarkers is null or < 0 or > 1)
            {
                ShowDistanceUnderPartyMembersMarkers = 1;
                fixes.Add("Corrected ShowDistanceUnderPartyMembersMarkers");
            }
            if (ShowNameOnPartyMembersMarkers is null or < 0 or > 1)
            {
                ShowNameOnPartyMembersMarkers = 1;
                fixes.Add("Corrected ShowNameOnPartyMembersMarkers");
            }
            if (EnableQuickMarker is null or < 0 or > 1)
            {
                EnableQuickMarker = 1;
                fixes.Add("Corrected EnableQuickMarker");
            }
            if (ShowDistanceUnderQuickMarkers is null or < 0 or > 1)
            {
                ShowDistanceUnderQuickMarkers = 1;
                fixes.Add("Corrected ShowDistanceUnderQuickMarkers");
            }
            if (ShowNameOnQuickMarkers is null or < 0 or > 1)
            {
                ShowNameOnQuickMarkers = 1;
                fixes.Add("Corrected ShowNameOnQuickMarkers");
            }
            if (CanCreatePartyMarkers is null or < 0 or > 1)
            {
                CanCreatePartyMarkers = 1;
                fixes.Add("Corrected CanCreatePartyMarkers");
            }
            if (ShowPartyMemberHUD is null or < 0 or > 1)
            {
                ShowPartyMemberHUD = 1;
                fixes.Add("Corrected ShowPartyMemberHUD");
            }
            if (ShowHUDMemberBlood is null or < 0 or > 1)
            {
                ShowHUDMemberBlood = 1;
                fixes.Add("Corrected ShowHUDMemberBlood");
            }
            if (ShowHUDMemberStates is null or < 0 or > 1)
            {
                ShowHUDMemberStates = 1;
                fixes.Add("Corrected ShowHUDMemberStates");
            }
            if (ShowHUDMemberStance is null or < 0 or > 1)
            {
                ShowHUDMemberStance = 1;
                fixes.Add("Corrected ShowHUDMemberStance");
            }
            if (ShowPartyMemberMapMarkers is null or < 0 or > 1)
            {
                ShowPartyMemberMapMarkers = 1;
                fixes.Add("Corrected ShowPartyMemberMapMarkers");
            }
            if (ShowHUDMemberDistance is null or < 0 or > 1)
            {
                ShowHUDMemberDistance = 1;
                fixes.Add("Corrected ShowHUDMemberDistance");
            }
            if (ForcePartyToHaveTags is null or < 0 or > 1)
            {
                ForcePartyToHaveTags = 0;
                fixes.Add("Corrected ForcePartyToHaveTags");
            }
            if (InviteCooldown is null or < 0 or > 1)
            {
                InviteCooldown = 0;
                fixes.Add("Corrected InviteCooldown");
            }
            if (DisplayPartyTag is null or < 0 or > 1)
            {
                DisplayPartyTag = 1;
                fixes.Add("Corrected DisplayPartyTag");
            }
            


            return fixes;
        }
        public override bool Equals(object obj)
        {
            if (obj is not ExpansionPartySettings other)
                return false;

            return m_Version == other.m_Version &&
                EnableParties == other.EnableParties &&
                MaxMembersInParty == other.MaxMembersInParty &&
                UseWholeMapForInviteList == other.UseWholeMapForInviteList &&
                ShowPartyMember3DMarkers == other.ShowPartyMember3DMarkers &&
                ShowDistanceUnderPartyMembersMarkers == other.ShowDistanceUnderPartyMembersMarkers &&
                ShowNameOnPartyMembersMarkers == other.ShowNameOnPartyMembersMarkers &&
                EnableQuickMarker == other.EnableQuickMarker &&
                ShowDistanceUnderQuickMarkers == other.ShowDistanceUnderQuickMarkers &&
                ShowNameOnQuickMarkers == other.ShowNameOnQuickMarkers &&
                CanCreatePartyMarkers == other.CanCreatePartyMarkers &&
                ShowPartyMemberHUD == other.ShowPartyMemberHUD &&
                ShowHUDMemberBlood == other.ShowHUDMemberBlood &&
                ShowHUDMemberStates == other.ShowHUDMemberStates &&
                ShowHUDMemberStance == other.ShowHUDMemberStance &&
                ShowPartyMemberMapMarkers == other.ShowPartyMemberMapMarkers &&
                ShowHUDMemberDistance == other.ShowHUDMemberDistance &&
                ForcePartyToHaveTags == other.ForcePartyToHaveTags &&
                InviteCooldown == other.InviteCooldown &&
                DisplayPartyTag == other.DisplayPartyTag;
        }
        public ExpansionPartySettings Clone()
        {
            return new ExpansionPartySettings()
            {
                m_Version = this.m_Version,
                EnableParties = this.EnableParties,
                MaxMembersInParty = this.MaxMembersInParty,
                UseWholeMapForInviteList = this.UseWholeMapForInviteList,
                ShowPartyMember3DMarkers = this.ShowPartyMember3DMarkers,
                ShowDistanceUnderPartyMembersMarkers = this.ShowDistanceUnderPartyMembersMarkers,
                ShowNameOnPartyMembersMarkers = this.ShowNameOnPartyMembersMarkers,
                EnableQuickMarker = this.EnableQuickMarker,
                ShowDistanceUnderQuickMarkers = this.ShowDistanceUnderQuickMarkers,
                ShowNameOnQuickMarkers = this.ShowNameOnQuickMarkers,
                CanCreatePartyMarkers = this.CanCreatePartyMarkers,
                ShowPartyMemberHUD = this.ShowPartyMemberHUD,
                ShowHUDMemberBlood = this.ShowHUDMemberBlood,
                ShowHUDMemberStates = this.ShowHUDMemberStates,
                ShowHUDMemberStance = this.ShowHUDMemberStance,
                ShowPartyMemberMapMarkers = this.ShowPartyMemberMapMarkers,
                ShowHUDMemberDistance = this.ShowHUDMemberDistance,
                ForcePartyToHaveTags = this.ForcePartyToHaveTags,
                InviteCooldown = this.InviteCooldown,
                DisplayPartyTag = this.DisplayPartyTag
            };
        }
    }
}
