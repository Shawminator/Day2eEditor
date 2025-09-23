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
    public class ExpansionAIConfig : IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path); // e.g., "types.xml"
        public string FilePath => _path;
        public ExpansionAISettings Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }
        const int CurrentVersion = 15;
        public ExpansionAIConfig(string path)
        {
            _path = path;
        }

        public void Load()
        {
            Data = AppServices.GetRequired<FileService>().LoadOrCreateJson<ExpansionAISettings>(
                _path,
                createNew: () => new ExpansionAISettings(CurrentVersion),
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
                configName: "ExpansionAISettings"
            );
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

        public bool checkver()
        {
            if (Data.m_Version != CurrentVersion)
            {
                Data.m_Version = CurrentVersion;
                isDirty = true;
                return true;
            }
            return false;
        }
    }
    public class AILightEntries
    {
        public int Key { get; set; }
        public decimal Value { get; set; }

        public override string ToString() => Key.ToString();
    }
    public class ExpansionAISettings
    {
        public int m_Version { get; set; }

        public decimal AccuracyMin { get; set; }
        public decimal AccuracyMax { get; set; }

        public decimal ThreatDistanceLimit { get; set; }
        public decimal NoiseInvestigationDistanceLimit { get; set; }
        public decimal DamageMultiplier { get; set; }
        public decimal DamageReceivedMultiplier { get; set; }

        public BindingList<string> Admins { get; set; }

        public int Vaulting { get; set; }
        public decimal SniperProneDistanceThreshold { get; set; }
        public int Manners { get; set; }
        public int MemeLevel { get; set; }
        public int CanRecruitFriendly { get; set; }
        public int CanRecruitGuards { get; set; }
        public BindingList<string> PreventClimb { get; set; }
        public decimal FormationScale { get; set; } //added in version 13
        public BindingList<string> PlayerFactions { get; set; }
        public int LogAIHitBy { get; set; }
        public int LogAIKilled { get; set; }

        public int EnableZombieVehicleAttackHandler { get; set; }
        public int EnableZombieVehicleAttackPhysics { get; set; }

        public Dictionary<int, decimal> LightingConfigMinNightVisibilityMeters { get; set; }

        [JsonIgnore]
        public BindingList<AILightEntries> AILightEntries { get; set; }

        public void createlistfromdict()
        {
            AILightEntries = new BindingList<AILightEntries>(LightingConfigMinNightVisibilityMeters.Select(kvp => new AILightEntries { Key = kvp.Key, Value = kvp.Value }).ToList());
        }
        public void CreateDictionary()
        {
            LightingConfigMinNightVisibilityMeters = AILightEntries.ToDictionary(e => e.Key, e => e.Value);
        }

        public ExpansionAISettings()
        {

        }
        public ExpansionAISettings(int CurrentVersion)
        {
            m_Version = CurrentVersion;
            AccuracyMin = (decimal)0.35;
            AccuracyMax = (decimal)0.95;
            ThreatDistanceLimit = (decimal)1000.0;
            NoiseInvestigationDistanceLimit = (decimal)500.0;
            DamageMultiplier = (decimal)1.0;
            Admins = new BindingList<string>();
            Vaulting = 1;
            SniperProneDistanceThreshold = (decimal)0.0;
            Manners = 0;
            MemeLevel = 1;
            CanRecruitFriendly = 1;
            CanRecruitGuards = 0;
            PreventClimb = new BindingList<string>();
            FormationScale = (decimal)1.0;
            PlayerFactions = new BindingList<string>();
            LogAIHitBy = 1;
            LogAIKilled = 1;
            EnableZombieVehicleAttackHandler = 0;
            EnableZombieVehicleAttackPhysics = 0;
            LightingConfigMinNightVisibilityMeters = new Dictionary<int, decimal>
            {
                {0, 100.0m },
                {1, 10.0m }
            };
        }
    }
}
