using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionNameTagsConfig : IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path); // e.g., "types.xml"
        public string FilePath => _path;
        public NameTagsSettings Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }
        public const int CurrentVersion = 4;
        public ExpansionNameTagsConfig(string path)
        {
            _path = path;
        }
        public void Load()
        {
            Data = null;
            Data = AppServices.GetRequired<FileService>().LoadOrCreateJson<NameTagsSettings>(
                _path,
                createNew: () => new NameTagsSettings(CurrentVersion),
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
                configName: "ExpansionNameTagsSettings"
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
    public class NameTagsSettings
    {
        public int m_Version { get; set; }
        public int? EnablePlayerTags { get; set; }
        public int? PlayerTagViewRange { get; set; }
        public string? PlayerTagsIcon { get; set; }
        public int? PlayerTagsColor { get; set; }
        public int? PlayerNameColor { get; set; }
        public int? OnlyInSafeZones { get; set; }
        public int? OnlyInTerritories { get; set; }
        public int? ShowPlayerItemInHands { get; set; }
        public int? ShowNPCTags { get; set; }
        public int? ShowPlayerFaction { get; set; }
        public int? UseRarityColorForItemInHands { get; set; }

        public NameTagsSettings() { }
        public NameTagsSettings(int CurrentVersion)
        {
            m_Version = CurrentVersion;
            EnablePlayerTags = 1;
            PlayerTagViewRange = 5;
            PlayerTagsIcon = "Persona";
            PlayerTagsColor = -1;
            PlayerNameColor = -1;
            OnlyInSafeZones = 0;
            OnlyInTerritories = 0;
            ShowPlayerItemInHands = 0;
            ShowNPCTags = 0;
            ShowPlayerFaction = 0;
            UseRarityColorForItemInHands = 0;
        }
        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();
            if (m_Version != ExpansionNameTagsConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionNameTagsConfig.CurrentVersion}");
                m_Version = ExpansionNameTagsConfig.CurrentVersion;
            }
            if (EnablePlayerTags is null or < 0 or > 0)
            {
                EnablePlayerTags = 1;
                fixes.Add("Corrected Enabled");
            }
            if (PlayerTagViewRange is null or < 0)
            {
                PlayerTagViewRange = 5;
                fixes.Add("Corrected PlayerTagViewRange");
            }

            if (string.IsNullOrWhiteSpace(PlayerTagsIcon))
            {
                PlayerTagsIcon = "Persona";
                fixes.Add("Corrected PlayerTagsIcon");
            }

            if (PlayerTagsColor is null)
            {
                PlayerTagsColor = -1;
                fixes.Add("Corrected PlayerTagsColor");
            }

            if (PlayerNameColor is null)
            {
                PlayerNameColor = -1;
                fixes.Add("Corrected PlayerNameColor");
            }

            if (OnlyInSafeZones is null or < 0 or > 1)
            {
                OnlyInSafeZones = 0;
                fixes.Add("Corrected OnlyInSafeZones");
            }

            if (OnlyInTerritories is null or < 0 or > 1)
            {
                OnlyInTerritories = 0;
                fixes.Add("Corrected OnlyInTerritories");
            }

            if (ShowPlayerItemInHands is null or < 0 or > 1)
            {
                ShowPlayerItemInHands = 0;
                fixes.Add("Corrected ShowPlayerItemInHands");
            }

            if (ShowNPCTags is null or < 0 or > 1)
            {
                ShowNPCTags = 0;
                fixes.Add("Corrected ShowNPCTags");
            }

            if (ShowPlayerFaction is null or < 0 or > 1)
            {
                ShowPlayerFaction = 0;
                fixes.Add("Corrected ShowPlayerFaction");
            }

            if (UseRarityColorForItemInHands is null or < 0 or > 1)
            {
                UseRarityColorForItemInHands = 0;
                fixes.Add("Corrected UseRarityColorForItemInHands");
            }
            return fixes;
        }
        public override bool Equals(object obj)
        {
            if (obj is not NameTagsSettings other)
                return false;


            return m_Version == other.m_Version &&
               EnablePlayerTags == other.EnablePlayerTags &&
               PlayerTagViewRange == other.PlayerTagViewRange &&
               PlayerTagsIcon == other.PlayerTagsIcon &&
               PlayerTagsColor == other.PlayerTagsColor &&
               PlayerNameColor == other.PlayerNameColor &&
               OnlyInSafeZones == other.OnlyInSafeZones &&
               OnlyInTerritories == other.OnlyInTerritories &&
               ShowPlayerItemInHands == other.ShowPlayerItemInHands &&
               ShowNPCTags == other.ShowNPCTags &&
               ShowPlayerFaction == other.ShowPlayerFaction &&
               UseRarityColorForItemInHands == other.UseRarityColorForItemInHands;
        }
        public NameTagsSettings Clone()
        {
            return new NameTagsSettings
            {
                m_Version = this.m_Version,
                EnablePlayerTags = this.EnablePlayerTags,
               PlayerTagViewRange = this.PlayerTagViewRange,
               PlayerTagsIcon = this.PlayerTagsIcon,
               PlayerTagsColor = this.PlayerTagsColor,
               PlayerNameColor = this.PlayerNameColor,
               OnlyInSafeZones = this.OnlyInSafeZones,
               OnlyInTerritories = this.OnlyInTerritories,
               ShowPlayerItemInHands = this.ShowPlayerItemInHands,
               ShowNPCTags = this.ShowNPCTags,
               ShowPlayerFaction = this.ShowPlayerFaction,
               UseRarityColorForItemInHands = this.UseRarityColorForItemInHands;
            };
        }
    }
}
