using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionNotificationConfig : IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path); // e.g., "types.xml"
        public string FilePath => _path;
        public ExpansionNotificationSettings Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }
        public const int CurrentVersion = 5;
        public ExpansionNotificationConfig(string path)
        {
            _path = path;
        }
        public void Load()
        {
            Data = null;
            Data = AppServices.GetRequired<FileService>().LoadOrCreateJson<ExpansionNotificationSettings>(
                _path,
                createNew: () => new ExpansionNotificationSettings(CurrentVersion),
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
                configName: "ExpansionNotificationSettings",
                useBoolConvertor: true
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
                AppServices.GetRequired<FileService>().SaveJson(_path, Data, true);
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
    public class ExpansionNotificationSettings
    {
        public int m_Version { get; set; }
        public bool? EnableNotification { get; set; }
        public bool? ShowPlayerJoinServer { get; set; }
        public ExpansionAnnouncementType? JoinMessageType { get; set; }
        public bool? ShowPlayerLeftServer { get; set; }
        public ExpansionAnnouncementType? LeftMessageType { get; set; }
        public bool? ShowAirdropStarted { get; set; }
        public bool? ShowAirdropClosingOn { get; set; }
        public bool? ShowAirdropDropped { get; set; }
        public bool? ShowAirdropEnded { get; set; }
        public bool? ShowPlayerAirdropStarted { get; set; }
        public bool? ShowPlayerAirdropClosingOn { get; set; }
        public bool? ShowPlayerAirdropDropped { get; set; }
        //public bool? ShowAIMissionStarted { get; set; }
        //public bool? ShowAIMissionAction { get; set; }
        //public bool? ShowAIMissionEnded { get; set; }
        public bool? ShowTerritoryNotifications { get; set; }
        public bool? EnableKillFeed { get; set; }
        public ExpansionAnnouncementType? KillFeedMessageType { get; set; }
        public bool? KillFeedFall { get; set; }
        public bool? KillFeedCarHitDriver { get; set; }
        public bool? KillFeedCarHitNoDriver { get; set; }
        public bool? KillFeedCarCrash { get; set; }
        public bool? KillFeedCarCrashCrew { get; set; }
        public bool? KillFeedHeliHitDriver { get; set; }
        public bool? KillFeedHeliHitNoDriver { get; set; }
        public bool? KillFeedHeliCrash { get; set; }
        public bool? KillFeedHeliCrashCrew { get; set; }
        public bool? KillFeedBoatHitDriver { get; set; }
        public bool? KillFeedBoatHitNoDriver { get; set; }
        public bool? KillFeedBoatCrash { get; set; }
        public bool? KillFeedBoatCrashCrew { get; set; }
        public bool? KillFeedBarbedWire { get; set; }
        public bool? KillFeedFire { get; set; }
        public bool? KillFeedWeaponExplosion { get; set; }
        public bool? KillFeedDehydration { get; set; }
        public bool? KillFeedStarvation { get; set; }
        public bool? KillFeedBleeding { get; set; }
        public bool? KillFeedStatusEffects { get; set; }
        public bool? KillFeedSuicide { get; set; }
        public bool? KillFeedWeapon { get; set; }
        public bool? KillFeedMeleeWeapon { get; set; }
        public bool? KillFeedBarehands { get; set; }
        public bool? KillFeedInfected { get; set; }
        public bool? KillFeedAnimal { get; set; }
        public bool? KillFeedAI { get; set; }
        public bool? KillFeedKilledUnknown { get; set; }
        public bool? KillFeedDiedUnknown { get; set; }
        public bool? EnableKillFeedDiscordMsg { get; set; }

        public ExpansionNotificationSettings() { }
        public ExpansionNotificationSettings(int CurrentVersion)
        {
            m_Version = CurrentVersion;
            EnableNotification = true;

            ShowPlayerJoinServer = true;
            JoinMessageType = ExpansionAnnouncementType.NOTIFICATION;
            ShowPlayerLeftServer = true;
            LeftMessageType = ExpansionAnnouncementType.NOTIFICATION;

            ShowAirdropStarted = true;
            ShowAirdropClosingOn = true;
            ShowAirdropDropped = true;
            ShowAirdropEnded = true;
            ShowPlayerAirdropStarted = true;
            ShowPlayerAirdropClosingOn = true;
            ShowPlayerAirdropDropped = true;

            ShowTerritoryNotifications = true;

            EnableKillFeed = true;
            KillFeedMessageType = ExpansionAnnouncementType.NOTIFICATION;

            //! These are not implemented, uncomment once done
            //ShowDistanceOnKillFeed = true;
            //ShowVictimOnKillFeed = true;
            //ShowKillerOnKillFeed = true;
            //ShowWeaponOnKillFeed = true;

            KillFeedFall = true;
            KillFeedCarHitDriver = true;
            KillFeedCarHitNoDriver = true;
            KillFeedCarCrash = true;
            KillFeedCarCrashCrew = true;
            KillFeedHeliHitDriver = true;
            KillFeedHeliHitNoDriver = true;
            KillFeedHeliCrash = true;
            KillFeedHeliCrashCrew = true;
            KillFeedBoatHitDriver = true;
            KillFeedBoatHitNoDriver = true;
            KillFeedBoatCrash = true;
            KillFeedBoatCrashCrew = true;
            /*KillFeedPlaneHitDriver = true;
            KillFeedPlaneHitNoDriver = true;
            KillFeedBikeHitDriver = true;
            KillFeedBikeHitNoDriver = true;*/
            KillFeedBarbedWire = true;
            KillFeedFire = true;
            KillFeedWeaponExplosion = true;
            KillFeedDehydration = true;
            KillFeedStarvation = true;
            KillFeedBleeding = true;
            KillFeedStatusEffects = true;
            KillFeedSuicide = true;
            KillFeedWeapon = true;
            KillFeedMeleeWeapon = true;
            KillFeedBarehands = true;
            KillFeedInfected = true;
            KillFeedAnimal = true;
            KillFeedAI = true;
            KillFeedKilledUnknown = true;
            KillFeedDiedUnknown = true;

            EnableKillFeedDiscordMsg = false;
        }
        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();
            if (m_Version != ExpansionNotificationConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionNotificationConfig.CurrentVersion}");
                m_Version = ExpansionNotificationConfig.CurrentVersion;
            }
            if (EnableNotification is null ) { EnableNotification = true; fixes.Add("Corrected EnableNotification"); }
            if (ShowPlayerJoinServer is null) { ShowPlayerJoinServer = true; fixes.Add("Corrected ShowPlayerJoinServer"); }
            if (!Enum.IsDefined(typeof(ExpansionAnnouncementType), JoinMessageType) ||
                    (JoinMessageType != ExpansionAnnouncementType.CHAT && JoinMessageType != ExpansionAnnouncementType.NOTIFICATION && JoinMessageType != ExpansionAnnouncementType.MUTEDNOTIFICATION))
            {
                fixes.Add($"Corrected JoinMessageType from '{JoinMessageType}' to '{ExpansionAnnouncementType.NOTIFICATION}'");
                JoinMessageType = ExpansionAnnouncementType.NOTIFICATION;
            }
            if (ShowPlayerLeftServer is null ) { ShowPlayerLeftServer = true; fixes.Add("Corrected ShowPlayerLeftServer"); }
            if (!Enum.IsDefined(typeof(ExpansionAnnouncementType), LeftMessageType) ||
                    (LeftMessageType != ExpansionAnnouncementType.CHAT && LeftMessageType != ExpansionAnnouncementType.NOTIFICATION && LeftMessageType != ExpansionAnnouncementType.MUTEDNOTIFICATION))
            {
                fixes.Add($"Corrected LeftMessageType from '{LeftMessageType}' to '{ExpansionAnnouncementType.NOTIFICATION}'");
                LeftMessageType = ExpansionAnnouncementType.NOTIFICATION;
            }
            if (ShowAirdropStarted is null ) { ShowAirdropStarted = true; fixes.Add("Corrected ShowAirdropStarted"); }
            if (ShowAirdropClosingOn is null) { ShowAirdropClosingOn = true; fixes.Add("Corrected ShowAirdropClosingOn"); }
            if (ShowAirdropDropped is null ) { ShowAirdropDropped = true; fixes.Add("Corrected ShowAirdropDropped"); }
            if (ShowAirdropEnded is null ) { ShowAirdropEnded = true; fixes.Add("Corrected ShowAirdropEnded"); }
            if (ShowPlayerAirdropStarted is null ) { ShowPlayerAirdropStarted = true; fixes.Add("Corrected ShowPlayerAirdropStarted"); }
            if (ShowPlayerAirdropClosingOn is null) { ShowPlayerAirdropClosingOn = true; fixes.Add("Corrected ShowPlayerAirdropClosingOn"); }
            if (ShowPlayerAirdropDropped is null) { ShowPlayerAirdropDropped = true; fixes.Add("Corrected ShowPlayerAirdropDropped"); }
            if (ShowTerritoryNotifications is null) { ShowTerritoryNotifications = true; fixes.Add("Corrected ShowTerritoryNotifications"); }
            if (EnableKillFeed is null) { EnableKillFeed = true; fixes.Add("Corrected EnableKillFeed"); }
            if (!Enum.IsDefined(typeof(ExpansionAnnouncementType), KillFeedMessageType) ||
                    (KillFeedMessageType != ExpansionAnnouncementType.CHAT && KillFeedMessageType != ExpansionAnnouncementType.NOTIFICATION && KillFeedMessageType != ExpansionAnnouncementType.MUTEDNOTIFICATION))
            {
                fixes.Add($"Corrected VehicleSync from '{KillFeedMessageType}' to '{ExpansionAnnouncementType.NOTIFICATION}'");
                KillFeedMessageType = ExpansionAnnouncementType.NOTIFICATION;
            }
            if (KillFeedFall is null) { KillFeedFall = true; fixes.Add("Corrected KillFeedFall"); }
            if (KillFeedCarHitDriver is null) { KillFeedCarHitDriver = true; fixes.Add("Corrected KillFeedCarHitDriver"); }
            if (KillFeedCarHitNoDriver is null) { KillFeedCarHitNoDriver = true; fixes.Add("Corrected KillFeedCarHitNoDriver"); }
            if (KillFeedCarCrash is null) { KillFeedCarCrash = true; fixes.Add("Corrected KillFeedCarCrash"); }
            if (KillFeedCarCrashCrew is null) { KillFeedCarCrashCrew = true; fixes.Add("Corrected KillFeedCarCrashCrew"); }
            if (KillFeedHeliHitDriver is null) { KillFeedHeliHitDriver = true; fixes.Add("Corrected KillFeedHeliHitDriver"); }
            if (KillFeedHeliHitNoDriver is null) { KillFeedHeliHitNoDriver = true; fixes.Add("Corrected KillFeedHeliHitNoDriver"); }
            if (KillFeedHeliCrash is null) { KillFeedHeliCrash = true; fixes.Add("Corrected KillFeedHeliCrash"); }
            if (KillFeedHeliCrashCrew is null) { KillFeedHeliCrashCrew = true; fixes.Add("Corrected KillFeedHeliCrashCrew"); }
            if (KillFeedBoatHitDriver is null) { KillFeedBoatHitDriver = true; fixes.Add("Corrected KillFeedBoatHitDriver"); }
            if (KillFeedBoatHitNoDriver is null) { KillFeedBoatHitNoDriver = true; fixes.Add("Corrected KillFeedBoatHitNoDriver"); }
            if (KillFeedBoatCrash is null) { KillFeedBoatCrash = true; fixes.Add("Corrected KillFeedBoatCrash"); }
            if (KillFeedBoatCrashCrew is null) { KillFeedBoatCrashCrew = true; fixes.Add("Corrected KillFeedBoatCrashCrew"); }
            if (KillFeedBarbedWire is null) { KillFeedBarbedWire = true; fixes.Add("Corrected KillFeedBarbedWire"); }
            if (KillFeedFire is null) { KillFeedFire = true; fixes.Add("Corrected KillFeedFire"); }
            if (KillFeedWeaponExplosion is null) { KillFeedWeaponExplosion = true; fixes.Add("Corrected KillFeedWeaponExplosion"); }
            if (KillFeedDehydration is null) { KillFeedDehydration = true; fixes.Add("Corrected KillFeedDehydration"); }
            if (KillFeedStarvation is null) { KillFeedStarvation = true; fixes.Add("Corrected KillFeedStarvation"); }
            if (KillFeedBleeding is null) { KillFeedBleeding = true; fixes.Add("Corrected KillFeedBleeding"); }
            if (KillFeedStatusEffects is null) { KillFeedStatusEffects = true; fixes.Add("Corrected KillFeedStatusEffects"); }
            if (KillFeedSuicide is null) { KillFeedSuicide = true; fixes.Add("Corrected KillFeedSuicide"); }
            if (KillFeedWeapon is null) { KillFeedWeapon = true; fixes.Add("Corrected KillFeedWeapon"); }
            if (KillFeedMeleeWeapon is null) { KillFeedMeleeWeapon = true; fixes.Add("Corrected KillFeedMeleeWeapon"); }
            if (KillFeedBarehands is null) { KillFeedBarehands = true; fixes.Add("Corrected KillFeedBarehands"); }
            if (KillFeedInfected is null) { KillFeedInfected = true; fixes.Add("Corrected KillFeedInfected"); }
            if (KillFeedAnimal is null) { KillFeedAnimal = true; fixes.Add("Corrected KillFeedAnimal"); }
            if (KillFeedAI is null) { KillFeedAI = true; fixes.Add("Corrected KillFeedAI"); }
            if (KillFeedKilledUnknown is null) { KillFeedKilledUnknown = true; fixes.Add("Corrected KillFeedKilledUnknown"); }
            if (KillFeedDiedUnknown is null) { KillFeedDiedUnknown = true; fixes.Add("Corrected KillFeedDiedUnknown"); }
            if (EnableKillFeedDiscordMsg is null) { EnableKillFeedDiscordMsg = false; fixes.Add("Corrected EnableKillFeedDiscordMsg"); }
            return fixes;
        }
        public override bool Equals(object obj)
        {
            if (obj is not ExpansionNotificationSettings other)
                return false;

            return m_Version == other.m_Version &&
                   EnableNotification == other.EnableNotification &&
                   ShowPlayerJoinServer == other.ShowPlayerJoinServer &&
                   JoinMessageType == other.JoinMessageType &&
                   ShowPlayerLeftServer == other.ShowPlayerLeftServer &&
                   LeftMessageType == other.LeftMessageType &&
                   ShowAirdropStarted == other.ShowAirdropStarted &&
                   ShowAirdropClosingOn == other.ShowAirdropClosingOn &&
                   ShowAirdropDropped == other.ShowAirdropDropped &&
                   ShowAirdropEnded == other.ShowAirdropEnded &&
                   ShowPlayerAirdropStarted == other.ShowPlayerAirdropStarted &&
                   ShowPlayerAirdropClosingOn == other.ShowPlayerAirdropClosingOn &&
                   ShowPlayerAirdropDropped == other.ShowPlayerAirdropDropped &&
                   ShowTerritoryNotifications == other.ShowTerritoryNotifications &&
                   EnableKillFeed == other.EnableKillFeed &&
                   KillFeedMessageType == other.KillFeedMessageType &&
                   KillFeedFall == other.KillFeedFall &&
                   KillFeedCarHitDriver == other.KillFeedCarHitDriver &&
                   KillFeedCarHitNoDriver == other.KillFeedCarHitNoDriver &&
                   KillFeedCarCrash == other.KillFeedCarCrash &&
                   KillFeedCarCrashCrew == other.KillFeedCarCrashCrew &&
                   KillFeedHeliHitDriver == other.KillFeedHeliHitDriver &&
                   KillFeedHeliHitNoDriver == other.KillFeedHeliHitNoDriver &&
                   KillFeedHeliCrash == other.KillFeedHeliCrash &&
                   KillFeedHeliCrashCrew == other.KillFeedHeliCrashCrew &&
                   KillFeedBoatHitDriver == other.KillFeedBoatHitDriver &&
                   KillFeedBoatHitNoDriver == other.KillFeedBoatHitNoDriver &&
                   KillFeedBoatCrash == other.KillFeedBoatCrash &&
                   KillFeedBoatCrashCrew == other.KillFeedBoatCrashCrew &&
                   KillFeedBarbedWire == other.KillFeedBarbedWire &&
                   KillFeedFire == other.KillFeedFire &&
                   KillFeedWeaponExplosion == other.KillFeedWeaponExplosion &&
                   KillFeedDehydration == other.KillFeedDehydration &&
                   KillFeedStarvation == other.KillFeedStarvation &&
                   KillFeedBleeding == other.KillFeedBleeding &&
                   KillFeedStatusEffects == other.KillFeedStatusEffects &&
                   KillFeedSuicide == other.KillFeedSuicide &&
                   KillFeedWeapon == other.KillFeedWeapon &&
                   KillFeedMeleeWeapon == other.KillFeedMeleeWeapon &&
                   KillFeedBarehands == other.KillFeedBarehands &&
                   KillFeedInfected == other.KillFeedInfected &&
                   KillFeedAnimal == other.KillFeedAnimal &&
                   KillFeedAI == other.KillFeedAI &&
                   KillFeedKilledUnknown == other.KillFeedKilledUnknown &&
                   KillFeedDiedUnknown == other.KillFeedDiedUnknown &&
                   EnableKillFeedDiscordMsg == other.EnableKillFeedDiscordMsg;
        }
        public ExpansionNotificationSettings Clone()
        {
            return new ExpansionNotificationSettings
            {
                m_Version = this.m_Version,
                EnableNotification = this.EnableNotification,
                ShowPlayerJoinServer = this.ShowPlayerJoinServer,
                JoinMessageType = this.JoinMessageType,
                ShowPlayerLeftServer = this.ShowPlayerLeftServer,
                LeftMessageType = this.LeftMessageType,
                ShowAirdropStarted = this.ShowAirdropStarted,
                ShowAirdropClosingOn = this.ShowAirdropClosingOn,
                ShowAirdropDropped = this.ShowAirdropDropped,
                ShowAirdropEnded = this.ShowAirdropEnded,
                ShowPlayerAirdropStarted = this.ShowPlayerAirdropStarted,
                ShowPlayerAirdropClosingOn = this.ShowPlayerAirdropClosingOn,
                ShowPlayerAirdropDropped = this.ShowPlayerAirdropDropped,
                ShowTerritoryNotifications = this.ShowTerritoryNotifications,
                EnableKillFeed = this.EnableKillFeed,
                KillFeedMessageType = this.KillFeedMessageType,
                KillFeedFall = this.KillFeedFall,
                KillFeedCarHitDriver = this.KillFeedCarHitDriver,
                KillFeedCarHitNoDriver = this.KillFeedCarHitNoDriver,
                KillFeedCarCrash = this.KillFeedCarCrash,
                KillFeedCarCrashCrew = this.KillFeedCarCrashCrew,
                KillFeedHeliHitDriver = this.KillFeedHeliHitDriver,
                KillFeedHeliHitNoDriver = this.KillFeedHeliHitNoDriver,
                KillFeedHeliCrash = this.KillFeedHeliCrash,
                KillFeedHeliCrashCrew = this.KillFeedHeliCrashCrew,
                KillFeedBoatHitDriver = this.KillFeedBoatHitDriver,
                KillFeedBoatHitNoDriver = this.KillFeedBoatHitNoDriver,
                KillFeedBoatCrash = this.KillFeedBoatCrash,
                KillFeedBoatCrashCrew = this.KillFeedBoatCrashCrew,
                KillFeedBarbedWire = this.KillFeedBarbedWire,
                KillFeedFire = this.KillFeedFire,
                KillFeedWeaponExplosion = this.KillFeedWeaponExplosion,
                KillFeedDehydration = this.KillFeedDehydration,
                KillFeedStarvation = this.KillFeedStarvation,
                KillFeedBleeding = this.KillFeedBleeding,
                KillFeedStatusEffects = this.KillFeedStatusEffects,
                KillFeedSuicide = this.KillFeedSuicide,
                KillFeedWeapon = this.KillFeedWeapon,
                KillFeedMeleeWeapon = this.KillFeedMeleeWeapon,
                KillFeedBarehands = this.KillFeedBarehands,
                KillFeedInfected = this.KillFeedInfected,
                KillFeedAnimal = this.KillFeedAnimal,
                KillFeedAI = this.KillFeedAI,
                KillFeedKilledUnknown = this.KillFeedKilledUnknown,
                KillFeedDiedUnknown = this.KillFeedDiedUnknown,
                EnableKillFeedDiscordMsg = this.EnableKillFeedDiscordMsg,

            };
        }

        internal void SetBoolValue(string v, bool @checked)
        {
            GetType().GetProperty(v).SetValue(this, @checked, null);
        }
    }
    public enum ExpansionAnnouncementType
    {
        CHAT = 0,
        NOTIFICATION,
        MUTEDNOTIFICATION
    };
}
