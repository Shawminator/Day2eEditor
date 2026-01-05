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
                configName: "ExpansionNotificationSettings"
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
    public class ExpansionNotificationSettings
    {
        public int m_Version { get; set; }
        public int? EnableNotification { get; set; }
        public int? ShowPlayerJoinServer { get; set; }
        public int? JoinMessageType { get; set; }
        public int? ShowPlayerLeftServer { get; set; }
        public int? LeftMessageType { get; set; }
        public int? ShowAirdropStarted { get; set; }
        public int? ShowAirdropClosingOn { get; set; }
        public int? ShowAirdropDropped { get; set; }
        public int? ShowAirdropEnded { get; set; }
        public int? ShowPlayerAirdropStarted { get; set; }
        public int? ShowPlayerAirdropClosingOn { get; set; }
        public int? ShowPlayerAirdropDropped { get; set; }
        public int? ShowTerritoryNotifications { get; set; }
        public int? EnableKillFeed { get; set; }
        public int? KillFeedMessageType { get; set; }
        public int? KillFeedFall { get; set; }
        public int? KillFeedCarHitDriver { get; set; }
        public int? KillFeedCarHitNoDriver { get; set; }
        public int? KillFeedCarCrash { get; set; }
        public int? KillFeedCarCrashCrew { get; set; }
        public int? KillFeedHeliHitDriver { get; set; }
        public int? KillFeedHeliHitNoDriver { get; set; }
        public int? KillFeedHeliCrash { get; set; }
        public int? KillFeedHeliCrashCrew { get; set; }
        public int? KillFeedBoatHitDriver { get; set; }
        public int? KillFeedBoatHitNoDriver { get; set; }
        public int? KillFeedBoatCrash { get; set; }
        public int? KillFeedBoatCrashCrew { get; set; }
        public int? KillFeedBarbedWire { get; set; }
        public int? KillFeedFire { get; set; }
        public int? KillFeedWeaponExplosion { get; set; }
        public int? KillFeedDehydration { get; set; }
        public int? KillFeedStarvation { get; set; }
        public int? KillFeedBleeding { get; set; }
        public int? KillFeedStatusEffects { get; set; }
        public int? KillFeedSuicide { get; set; }
        public int? KillFeedWeapon { get; set; }
        public int? KillFeedMeleeWeapon { get; set; }
        public int? KillFeedBarehands { get; set; }
        public int? KillFeedInfected { get; set; }
        public int? KillFeedAnimal { get; set; }
        public int? KillFeedAI { get; set; }
        public int? KillFeedKilledUnknown { get; set; }
        public int? KillFeedDiedUnknown { get; set; }
        public int? EnableKillFeedDiscordMsg { get; set; }

        public ExpansionNotificationSettings() { }
        public ExpansionNotificationSettings(int CurrentVersion)
        {
            m_Version = CurrentVersion;
            EnableNotification = 1;

            ShowPlayerJoinServer = 1;
            JoinMessageType = 1;
            ShowPlayerLeftServer = 1;
            LeftMessageType = 1;

            ShowAirdropStarted = 1;
            ShowAirdropClosingOn = 1;
            ShowAirdropDropped = 1;
            ShowAirdropEnded = 1;
            ShowPlayerAirdropStarted = 1;
            ShowPlayerAirdropClosingOn = 1;
            ShowPlayerAirdropDropped = 1;

            ShowTerritoryNotifications = 1;

            EnableKillFeed = 1;
            KillFeedMessageType = 1;

            //! These are not implemented, uncomment once done
            //ShowDistanceOnKillFeed = 1;
            //ShowVictimOnKillFeed = 1;
            //ShowKillerOnKillFeed = 1;
            //ShowWeaponOnKillFeed = 1;

            KillFeedFall = 1;
            KillFeedCarHitDriver = 1;
            KillFeedCarHitNoDriver = 1;
            KillFeedCarCrash = 1;
            KillFeedCarCrashCrew = 1;
            KillFeedHeliHitDriver = 1;
            KillFeedHeliHitNoDriver = 1;
            KillFeedHeliCrash = 1;
            KillFeedHeliCrashCrew = 1;
            KillFeedBoatHitDriver = 1;
            KillFeedBoatHitNoDriver = 1;
            KillFeedBoatCrash = 1;
            KillFeedBoatCrashCrew = 1;
            /*KillFeedPlaneHitDriver = 1;
            KillFeedPlaneHitNoDriver = 1;
            KillFeedBikeHitDriver = 1;
            KillFeedBikeHitNoDriver = 1;*/
            KillFeedBarbedWire = 1;
            KillFeedFire = 1;
            KillFeedWeaponExplosion = 1;
            KillFeedDehydration = 1;
            KillFeedStarvation = 1;
            KillFeedBleeding = 1;
            KillFeedStatusEffects = 1;
            KillFeedSuicide = 1;
            KillFeedWeapon = 1;
            KillFeedMeleeWeapon = 1;
            KillFeedBarehands = 1;
            KillFeedInfected = 1;
            KillFeedAnimal = 1;
            KillFeedAI = 1;
            KillFeedKilledUnknown = 1;
            KillFeedDiedUnknown = 1;

            EnableKillFeedDiscordMsg = 0;
        }
        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();
            if (m_Version != ExpansionNotificationConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionNotificationConfig.CurrentVersion}");
                m_Version = ExpansionNotificationConfig.CurrentVersion;
            }
            if (EnableNotification is null or < 0 or > 1) { EnableNotification = 1; fixes.Add("Corrected EnableNotification"); }
            if (ShowPlayerJoinServer is null or < 0 or > 1) { ShowPlayerJoinServer = 1; fixes.Add("Corrected ShowPlayerJoinServer"); }
            if (LeftMessageType is null or < 0 or > 2) { LeftMessageType = 1; fixes.Add("Corrected LeftMessageType"); }
            if (ShowAirdropStarted is null or < 0 or > 1) { ShowAirdropStarted = 1; fixes.Add("Corrected ShowAirdropStarted"); }
            if (ShowAirdropClosingOn is null or < 0 or > 1) { ShowAirdropClosingOn = 1; fixes.Add("Corrected ShowAirdropClosingOn"); }
            if (ShowAirdropDropped is null or < 0 or > 1) { ShowAirdropDropped = 1; fixes.Add("Corrected ShowAirdropDropped"); }
            if (ShowAirdropEnded is null or < 0 or > 1) { ShowAirdropEnded = 1; fixes.Add("Corrected ShowAirdropEnded"); }
            if (ShowPlayerAirdropStarted is null or < 0 or > 1) { ShowPlayerAirdropStarted = 1; fixes.Add("Corrected ShowPlayerAirdropStarted"); }
            if (ShowPlayerAirdropClosingOn is null or < 0 or > 1) { ShowPlayerAirdropClosingOn = 1; fixes.Add("Corrected ShowPlayerAirdropClosingOn"); }
            if (ShowPlayerAirdropDropped is null or < 0 or > 1) { ShowPlayerAirdropDropped = 1; fixes.Add("Corrected ShowPlayerAirdropDropped"); }
            if (ShowTerritoryNotifications is null or < 0 or > 1) { ShowTerritoryNotifications = 1; fixes.Add("Corrected ShowTerritoryNotifications"); }
            if (EnableKillFeed is null or < 0 or > 1) { EnableKillFeed = 1; fixes.Add("Corrected EnableKillFeed"); }
            if (KillFeedMessageType is null or < 0 or > 1) { KillFeedMessageType = 1; fixes.Add("Corrected KillFeedMessageType"); }
            if (KillFeedFall is null or < 0 or > 1) { KillFeedFall = 1; fixes.Add("Corrected KillFeedFall"); }
            if (KillFeedCarHitDriver is null or < 0 or > 1) { KillFeedCarHitDriver = 1; fixes.Add("Corrected KillFeedCarHitDriver"); }
            if (KillFeedCarHitNoDriver is null or < 0 or > 1) { KillFeedCarHitNoDriver = 1; fixes.Add("Corrected KillFeedCarHitNoDriver"); }
            if (KillFeedCarCrash is null or < 0 or > 1) { KillFeedCarCrash = 1; fixes.Add("Corrected KillFeedCarCrash"); }
            if (KillFeedCarCrashCrew is null or < 0 or > 1) { KillFeedCarCrashCrew = 1; fixes.Add("Corrected KillFeedCarCrashCrew"); }
            if (KillFeedHeliHitDriver is null or < 0 or > 1) { KillFeedHeliHitDriver = 1; fixes.Add("Corrected KillFeedHeliHitDriver"); }
            if (KillFeedHeliHitNoDriver is null or < 0 or > 1) { KillFeedHeliHitNoDriver = 1; fixes.Add("Corrected KillFeedHeliHitNoDriver"); }
            if (KillFeedHeliCrash is null or < 0 or > 1) { KillFeedHeliCrash = 1; fixes.Add("Corrected KillFeedHeliCrash"); }
            if (KillFeedHeliCrashCrew is null or < 0 or > 1) { KillFeedHeliCrashCrew = 1; fixes.Add("Corrected KillFeedHeliCrashCrew"); }
            if (KillFeedBoatHitDriver is null or < 0 or > 1) { KillFeedBoatHitDriver = 1; fixes.Add("Corrected KillFeedBoatHitDriver"); }
            if (KillFeedBoatHitNoDriver is null or < 0 or > 1) { KillFeedBoatHitNoDriver = 1; fixes.Add("Corrected KillFeedBoatHitNoDriver"); }
            if (KillFeedBoatCrash is null or < 0 or > 1) { KillFeedBoatCrash = 1; fixes.Add("Corrected KillFeedBoatCrash"); }
            if (KillFeedBoatCrashCrew is null or < 0 or > 1) { KillFeedBoatCrashCrew = 1; fixes.Add("Corrected KillFeedBoatCrashCrew"); }
            if (KillFeedBarbedWire is null or < 0 or > 1) { KillFeedBarbedWire = 1; fixes.Add("Corrected KillFeedBarbedWire"); }
            if (KillFeedFire is null or < 0 or > 1) { KillFeedFire = 1; fixes.Add("Corrected KillFeedFire"); }
            if (KillFeedWeaponExplosion is null or < 0 or > 1) { KillFeedWeaponExplosion = 1; fixes.Add("Corrected KillFeedWeaponExplosion"); }
            if (KillFeedDehydration is null or < 0 or > 1) { KillFeedDehydration = 1; fixes.Add("Corrected KillFeedDehydration"); }
            if (KillFeedStarvation is null or < 0 or > 1) { KillFeedStarvation = 1; fixes.Add("Corrected KillFeedStarvation"); }
            if (KillFeedBleeding is null or < 0 or > 1) { KillFeedBleeding = 1; fixes.Add("Corrected KillFeedBleeding"); }
            if (KillFeedStatusEffects is null or < 0 or > 1) { KillFeedStatusEffects = 1; fixes.Add("Corrected KillFeedStatusEffects"); }
            if (KillFeedSuicide is null or < 0 or > 1) { KillFeedSuicide = 1; fixes.Add("Corrected KillFeedSuicide"); }
            if (KillFeedWeapon is null or < 0 or > 1) { KillFeedWeapon = 1; fixes.Add("Corrected KillFeedWeapon"); }
            if (KillFeedMeleeWeapon is null or < 0 or > 1) { KillFeedMeleeWeapon = 1; fixes.Add("Corrected KillFeedMeleeWeapon"); }
            if (KillFeedBarehands is null or < 0 or > 1) { KillFeedBarehands = 1; fixes.Add("Corrected KillFeedBarehands"); }
            if (KillFeedInfected is null or < 0 or > 1) { KillFeedInfected = 1; fixes.Add("Corrected KillFeedInfected"); }
            if (KillFeedAnimal is null or < 0 or > 1) { KillFeedAnimal = 1; fixes.Add("Corrected KillFeedAnimal"); }
            if (KillFeedAI is null or < 0 or > 1) { KillFeedAI = 1; fixes.Add("Corrected KillFeedAI"); }
            if (KillFeedKilledUnknown is null or < 0 or > 1) { KillFeedKilledUnknown = 1; fixes.Add("Corrected KillFeedKilledUnknown"); }
            if (KillFeedDiedUnknown is null or < 0 or > 1) { KillFeedDiedUnknown = 1; fixes.Add("Corrected KillFeedDiedUnknown"); }
            if (EnableKillFeedDiscordMsg is null or < 0 or > 1) { EnableKillFeedDiscordMsg = 0; fixes.Add("Corrected EnableKillFeedDiscordMsg"); }
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
    }
}
