using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionLogsConfig : IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path);
        public string FilePath => _path;
        public ExpansionLogsSettings Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }

        public const int CurrentVersion = 8;

        public ExpansionLogsConfig(string path)
        {
            _path = path;
        }

        public void Load()
        {
            Data = AppServices.GetRequired<FileService>().LoadOrCreateJson<ExpansionLogsSettings>(
                _path,
                createNew: () => new ExpansionLogsSettings(CurrentVersion),
                onAfterLoad: cfg => { /* optional: do something after load */ },
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
                configName: "ExpansionLogs"
            );
            var missingFields = Data.FixMissingOrInvalidFields();
            if (missingFields.Any())
            {
                HasErrors = true;
                Errors.AddRange(missingFields);
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
    public class ExpansionLogsSettings
    {
        public int? m_Version { get; set; }
        public int? Safezone { get; set; }
        public int? AdminTools { get; set; }
        public int? ExplosionDamageSystem { get; set; }
        public int? VehicleCarKey { get; set; }
        public int? VehicleTowing { get; set; }
        public int? VehicleLockPicking { get; set; }
        public int? VehicleDestroyed { get; set; }
        public int? VehicleAttachments { get; set; }
        public int? VehicleEnter { get; set; }
        public int? VehicleLeave { get; set; }
        public int? VehicleDeleted { get; set; }
        public int? VehicleEngine { get; set; }
        public int? BaseBuildingRaiding { get; set; }
        public int? CodeLockRaiding { get; set; }
        public int? Territory { get; set; }
        public int? Killfeed { get; set; }
        public int? SpawnSelection { get; set; }
        public int? Party { get; set; }
        public int? MissionAirdrop { get; set; }
        public int? Chat { get; set; }
        public int? Market { get; set; }
        public int? ATM { get; set; }
        public int? AIGeneral { get; set; }
        public int? AIPatrol { get; set; }
        public int? AIObjectPatrol { get; set; }
        public int? LogToScripts { get; set; }
        public int? LogToADM { get; set; }
        public int? Hardline { get; set; }
        public int? Garage { get; set; }
        public int? VehicleCover { get; set; }
        public int? EntityStorage { get; set; }
        public int? Quests { get; set; }


        public ExpansionLogsSettings() { }
        public ExpansionLogsSettings(int CurrentVersion)
        {
            m_Version = CurrentVersion;
            Safezone = 1;
            AdminTools = 1;
            ExplosionDamageSystem = 1;
            LogToScripts = 0;
            LogToADM = 0;
            VehicleDestroyed = 1;
            VehicleCarKey = 1;
            VehicleTowing = 1;
            VehicleLockPicking = 1;
            VehicleAttachments = 1;
            VehicleEnter = 1;
            VehicleLeave = 1;
            VehicleDeleted = 1;
            VehicleEngine = 1;
            BaseBuildingRaiding = 1;
            CodeLockRaiding = 1;
            Territory = 1;
            Killfeed = 1;
            SpawnSelection = 1;
            MissionAirdrop = 1;
            Party = 1;
            Chat = 1;
            AIGeneral = 1;
            AIPatrol = 1;
            AIObjectPatrol = 1;
            Market = 1;
            ATM = 1;
            Hardline = 1;
            Garage = 1;
            VehicleCover = 1;
            EntityStorage = 1;
            Quests = 1;
        }

        public override bool Equals(object obj)
        {
            if (obj is not ExpansionLogsSettings other)
                return false;

            return m_Version == other.m_Version &&
                   Safezone == other.Safezone &&
                   AdminTools == other.AdminTools &&
                   ExplosionDamageSystem == other.ExplosionDamageSystem &&
                   VehicleCarKey == other.VehicleCarKey &&
                   VehicleTowing == other.VehicleTowing &&
                   VehicleLockPicking == other.VehicleLockPicking &&
                   VehicleDestroyed == other.VehicleDestroyed &&
                   VehicleAttachments == other.VehicleAttachments &&
                   VehicleEnter == other.VehicleEnter &&
                   VehicleLeave == other.VehicleLeave &&
                   VehicleDeleted == other.VehicleDeleted &&
                   VehicleEngine == other.VehicleEngine &&
                   BaseBuildingRaiding == other.BaseBuildingRaiding &&
                   CodeLockRaiding == other.CodeLockRaiding &&
                   Territory == other.Territory &&
                   Killfeed == other.Killfeed &&
                   SpawnSelection == other.SpawnSelection &&
                   Party == other.Party &&
                   MissionAirdrop == other.MissionAirdrop &&
                   Chat == other.Chat &&
                   Market == other.Market &&
                   ATM == other.ATM &&
                   AIGeneral == other.AIGeneral &&
                   AIPatrol == other.AIPatrol &&
                   AIObjectPatrol == other.AIObjectPatrol &&
                   LogToScripts == other.LogToScripts &&
                   LogToADM == other.LogToADM &&
                   Hardline == other.Hardline &&
                   Garage == other.Garage &&
                   VehicleCover == other.VehicleCover &&
                   EntityStorage == other.EntityStorage &&
                   Quests == other.Quests;
        }

        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            if (m_Version < ExpansionLogsConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionLogsConfig.CurrentVersion}");
                m_Version = ExpansionLogsConfig.CurrentVersion;
            }

            if (Safezone == null || (Safezone != 0 && Safezone != 1))
            {
                Safezone = 1;
                fixes.Add("Corrected Safezone to 1");
            }

            if (AdminTools == null || (AdminTools != 0 && AdminTools != 1))
            {
                AdminTools = 1;
                fixes.Add("Corrected AdminTools to 1");
            }

            if (ExplosionDamageSystem == null || (ExplosionDamageSystem != 0 && ExplosionDamageSystem != 1))
            {
                ExplosionDamageSystem = 1;
                fixes.Add("Corrected ExplosionDamageSystem to 1");
            }

            if (VehicleCarKey == null || (VehicleCarKey != 0 && VehicleCarKey != 1))
            {
                VehicleCarKey = 1;
                fixes.Add("Corrected VehicleCarKey to 1");
            }

            if (VehicleTowing == null || (VehicleTowing != 0 && VehicleTowing != 1))
            {
                VehicleTowing = 1;
                fixes.Add("Corrected VehicleTowing to 1");
            }

            if (VehicleLockPicking == null || (VehicleLockPicking != 0 && VehicleLockPicking != 1))
            {
                VehicleLockPicking = 1;
                fixes.Add("Corrected VehicleLockPicking to 1");
            }

            if (VehicleDestroyed == null || (VehicleDestroyed != 0 && VehicleDestroyed != 1))
            {
                VehicleDestroyed = 1;
                fixes.Add("Corrected VehicleDestroyed to 1");
            }

            if (VehicleAttachments == null || (VehicleAttachments != 0 && VehicleAttachments != 1))
            {
                VehicleAttachments = 1;
                fixes.Add("Corrected VehicleAttachments to 1");
            }

            if (VehicleEnter == null || (VehicleEnter != 0 && VehicleEnter != 1))
            {
                VehicleEnter = 1;
                fixes.Add("Corrected VehicleEnter to 1");
            }

            if (VehicleLeave == null || (VehicleLeave != 0 && VehicleLeave != 1))
            {
                VehicleLeave = 1;
                fixes.Add("Corrected VehicleLeave to 1");
            }

            if (VehicleDeleted == null || (VehicleDeleted != 0 && VehicleDeleted != 1))
            {
                VehicleDeleted = 1;
                fixes.Add("Corrected VehicleDeleted to 1");
            }

            if (VehicleEngine == null || (VehicleEngine != 0 && VehicleEngine != 1))
            {
                VehicleEngine = 1;
                fixes.Add("Corrected VehicleEngine to 1");
            }

            if (BaseBuildingRaiding == null || (BaseBuildingRaiding != 0 && BaseBuildingRaiding != 1))
            {
                BaseBuildingRaiding = 1;
                fixes.Add("Corrected BaseBuildingRaiding to 1");
            }

            if (CodeLockRaiding == null || (CodeLockRaiding != 0 && CodeLockRaiding != 1))
            {
                CodeLockRaiding = 1;
                fixes.Add("Corrected CodeLockRaiding to 1");
            }

            if (Territory == null || (Territory != 0 && Territory != 1))
            {
                Territory = 1;
                fixes.Add("Corrected Territory to 1");
            }

            if (Killfeed == null || (Killfeed != 0 && Killfeed != 1))
            {
                Killfeed = 1;
                fixes.Add("Corrected Killfeed to 1");
            }

            if (SpawnSelection == null || (SpawnSelection != 0 && SpawnSelection != 1))
            {
                SpawnSelection = 1;
                fixes.Add("Corrected SpawnSelection to 1");
            }

            if (Party == null || (Party != 0 && Party != 1))
            {
                Party = 1;
                fixes.Add("Corrected Party to 1");
            }

            if (MissionAirdrop == null || (MissionAirdrop != 0 && MissionAirdrop != 1))
            {
                MissionAirdrop = 1;
                fixes.Add("Corrected MissionAirdrop to 1");
            }

            if (Chat == null || (Chat != 0 && Chat != 1))
            {
                Chat = 1;
                fixes.Add("Corrected Chat to 1");
            }

            if (Market == null || (Market != 0 && Market != 1))
            {
                Market = 1;
                fixes.Add("Corrected Market to 1");
            }

            if (ATM == null || (ATM != 0 && ATM != 1))
            {
                ATM = 1;
                fixes.Add("Corrected ATM to 1");
            }

            if (AIGeneral == null || (AIGeneral != 0 && AIGeneral != 1))
            {
                AIGeneral = 1;
                fixes.Add("Corrected AIGeneral to 1");
            }

            if (AIPatrol == null || (AIPatrol != 0 && AIPatrol != 1))
            {
                AIPatrol = 1;
                fixes.Add("Corrected AIPatrol to 1");
            }

            if (AIObjectPatrol == null || (AIObjectPatrol != 0 && AIObjectPatrol != 1))
            {
                AIObjectPatrol = 1;
                fixes.Add("Corrected AIObjectPatrol to 1");
            }

            if (LogToScripts == null || (LogToScripts != 0 && LogToScripts != 1))
            {
                LogToScripts = 1;
                fixes.Add("Corrected LogToScripts to 1");
            }

            if (LogToADM == null || (LogToADM != 0 && LogToADM != 1))
            {
                LogToADM = 1;
                fixes.Add("Corrected LogToADM to 1");
            }

            if (Hardline == null || (Hardline != 0 && Hardline != 1))
            {
                Hardline = 1;
                fixes.Add("Corrected Hardline to 1");
            }

            if (Garage == null || (Garage != 0 && Garage != 1))
            {
                Garage = 1;
                fixes.Add("Corrected Garage to 1");
            }

            if (VehicleCover == null || (VehicleCover != 0 && VehicleCover != 1))
            {
                VehicleCover = 1;
                fixes.Add("Corrected VehicleCover to 1");
            }

            if (EntityStorage == null || (EntityStorage != 0 && EntityStorage != 1))
            {
                EntityStorage = 1;
                fixes.Add("Corrected EntityStorage to 1");
            }

            if (Quests == null || (Quests != 0 && Quests != 1))
            {
                Quests = 1;
                fixes.Add("Corrected Quests to 1");
            }

            return fixes;
        }

    }
}