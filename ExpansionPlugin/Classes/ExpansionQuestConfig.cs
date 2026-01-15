using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionQuestConfig : IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path); // e.g., "types.xml"
        public string FilePath => _path;
        public ExpansionQuestSettings Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }
        public const int CurrentVersion = 10;

        public ExpansionQuestConfig(string path)
        {
            _path = path;
        }
        public void Load()
        {
            Data = null;
            Data = AppServices.GetRequired<FileService>().LoadOrCreateJson<ExpansionQuestSettings>(
                _path,
                createNew: () => new ExpansionQuestSettings(CurrentVersion),
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
                configName: "ExpansionQuest"
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
    public class ExpansionQuestSettings
    {
        public int m_Version { get; set; }
        public int? EnableQuests { get; set; }
        public int? EnableQuestLogTab { get; set; }
        public int? CreateQuestNPCMarkers { get; set; }
        public string? QuestAcceptedTitle { get; set; }
        public string? QuestAcceptedText { get; set; }
        public string? QuestCompletedTitle { get; set; }
        public string? QuestCompletedText { get; set; }
        public string? QuestFailedTitle { get; set; }
        public string? QuestFailedText { get; set; }
        public string? QuestCanceledTitle { get; set; }
        public string? QuestCanceledText { get; set; }
        public string? QuestTurnInTitle { get; set; }
        public string? QuestTurnInText { get; set; }
        public string? QuestObjectiveCompletedTitle { get; set; }
        public string? QuestObjectiveCompletedText { get; set; }

        public string? QuestCooldownTitle { get; set; }
        public string? QuestCooldownText { get; set; }

        public string? QuestNotInGroupTitle { get; set; }
        public string? QuestNotInGroupText { get; set; }

        public string? QuestNotGroupOwnerTitle { get; set; }
        public string? QuestNotGroupOwnerText { get; set; }
        public int? GroupQuestMode { get; set; }

        public string? AchievementCompletedTitle { get; set; }
        public string? AchievementCompletedText { get; set; }

        public string? WeeklyResetDay { get; set; }
        public int? WeeklyResetMinute { get; set; }
        public int? WeeklyResetHour { get; set; }
        public int? DailyResetHour { get; set; }
        public int? DailyResetMinute { get; set; }
        public int? UseUTCTime { get; set; }
        public int? UseQuestNPCIndicators { get; set; }
        public int? MaxActiveQuests { get; set; }

        public ExpansionQuestSettings() { } 
        public ExpansionQuestSettings(int CurrentVersion)
        {
            m_Version = CurrentVersion;
            EnableQuests = 1;
            EnableQuestLogTab = 1;
            CreateQuestNPCMarkers = 1;

            QuestAcceptedTitle = "Quest Accepted";
            QuestAcceptedText = "The quest %1 has been accepted!";

            QuestCompletedTitle = "Quest Completed";
            QuestCompletedText = "All objectives of the quest %1 have been completed";

            QuestFailedTitle = "Quest Failed";
            QuestFailedText = "The quest %1 failed!";

            QuestCanceledTitle = "Quest Canceled";
            QuestCanceledText = "The quest %1 has been canceled!";

            QuestTurnInTitle = "Quest Turn-In";
            QuestTurnInText = "The quest %1 has been completed!";

            QuestObjectiveCompletedTitle = "Objective Completed";
            QuestObjectiveCompletedText = "You have completed the objective %1 of the quest %2.";

            AchievementCompletedTitle = "Achievement \"%1\" completed!";
            AchievementCompletedText = "%1";

            QuestCooldownTitle = "Quest Cooldown";
            QuestCooldownText = "This quest is still on cooldown! Come back in %1";

            QuestNotInGroupTitle = "Group Quest";
            QuestNotInGroupText = "Group quests can only be accepted while in a group!";

            QuestNotGroupOwnerTitle = "Group Quest";
            QuestNotGroupOwnerText = "Only a group owner can accept and turn-in a group quest!";

            WeeklyResetDay = "Wednesday";
            WeeklyResetHour = 8;
            WeeklyResetMinute = 0;
            DailyResetHour = 8;
            DailyResetMinute = 0;

            UseUTCTime = 0;

            UseQuestNPCIndicators = 1;
            MaxActiveQuests = -1;

            GroupQuestMode = 0;
        }

        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            if (m_Version != ExpansionQuestConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionQuestConfig.CurrentVersion}");
                m_Version = ExpansionQuestConfig.CurrentVersion;
            }
            if (EnableQuests is null or < 0 or > 1)
            {
                EnableQuests = 1;
                fixes.Add("Corrected EnableQuests");
            }
            if (EnableQuestLogTab is null or < 0 or > 1)
            {
                EnableQuestLogTab = 1;
                fixes.Add("Corrected EnableQuestLogTab");
            }
            if (CreateQuestNPCMarkers is null or < 0 or > 1)
            {
                CreateQuestNPCMarkers = 1;
                fixes.Add("Corrected CreateQuestNPCMarkers");
            }
            if (string.IsNullOrWhiteSpace(QuestAcceptedTitle))
            {
                QuestAcceptedTitle = "Quest Accepted";
                fixes.Add("Corrected QuestAcceptedTitle");
            }
            if (string.IsNullOrWhiteSpace(QuestAcceptedText))
            {
                QuestAcceptedText = "The quest %1 has been accepted!";
                fixes.Add("Corrected QuestAcceptedText");
            }
            if (string.IsNullOrWhiteSpace(QuestCompletedTitle))
            {
                QuestCompletedTitle = "Quest Completed";
                fixes.Add("Corrected QuestCompletedTitle");
            }
            if (string.IsNullOrWhiteSpace(QuestCompletedText))
            {
                QuestCompletedText = "All objectives of the quest %1 have been completed";
                fixes.Add("Corrected QuestCompletedText");
            }
            if (string.IsNullOrWhiteSpace(QuestFailedTitle))
            {
                QuestFailedTitle = "Quest Failed";
                fixes.Add("Corrected QuestFailedTitle");
            }
            if (string.IsNullOrWhiteSpace(QuestFailedText))
            {
                QuestFailedText = "The quest %1 failed!";
                fixes.Add("Corrected QuestFailedText");
            }
            if (string.IsNullOrWhiteSpace(QuestCanceledTitle))
            {
                QuestCanceledTitle = "Quest Canceled";
                fixes.Add("Corrected QuestCanceledTitle");
            }
            if (string.IsNullOrWhiteSpace(QuestCanceledText))
            {
                QuestCanceledText = "The quest %1 has been canceled!";
                fixes.Add("Corrected QuestCanceledText");
            }
            if (string.IsNullOrWhiteSpace(QuestTurnInTitle))
            {
                QuestTurnInTitle = "Quest Turn-In";
                fixes.Add("Corrected QuestTurnInTitle");
            }
            if (string.IsNullOrWhiteSpace(QuestTurnInText))
            {
                QuestTurnInText = "The quest %1 has been completed!";
                fixes.Add("Corrected QuestTurnInText");
            }
            if (string.IsNullOrWhiteSpace(QuestObjectiveCompletedTitle))
            {
                QuestObjectiveCompletedTitle = "Objective Completed";
                fixes.Add("Corrected QuestObjectiveCompletedTitle");
            }
            if (string.IsNullOrWhiteSpace(QuestObjectiveCompletedText))
            {
                QuestObjectiveCompletedText = "You have completed the objective %1 of the quest %2.";
                fixes.Add("Corrected QuestObjectiveCompletedText");
            }
            if (string.IsNullOrWhiteSpace(AchievementCompletedTitle))
            {
                AchievementCompletedTitle = "Achievement \"%1\" completed!";
                fixes.Add("Corrected AchievementCompletedTitle");
            }
            if (string.IsNullOrWhiteSpace(AchievementCompletedText))
            {
                AchievementCompletedText = "%1";
                fixes.Add("Corrected AchievementCompletedText");
            }
            if (string.IsNullOrWhiteSpace(QuestCooldownTitle))
            {
                QuestCooldownTitle = "Quest Cooldown";
                fixes.Add("Corrected QuestCooldownTitle");
            }
            if (string.IsNullOrWhiteSpace(QuestCooldownText))
            {
                QuestCooldownText = "This quest is still on cooldown! Come back in %1";
                fixes.Add("Corrected QuestCooldownText");
            }
            if (string.IsNullOrWhiteSpace(QuestNotInGroupTitle))
            {
                QuestNotInGroupTitle = "Group Quest";
                fixes.Add("Corrected QuestNotInGroupTitle");
            }
            if (string.IsNullOrWhiteSpace(QuestNotInGroupText))
            {
                QuestNotInGroupText = "Group quests can only be accepted while in a group!";
                fixes.Add("Corrected QuestNotInGroupText");
            }
            if (string.IsNullOrWhiteSpace(QuestNotGroupOwnerTitle))
            {
                QuestNotGroupOwnerTitle = "Group Quest";
                fixes.Add("Corrected QuestNotGroupOwnerTitle");
            }
            if (string.IsNullOrWhiteSpace(QuestNotGroupOwnerText))
            {
                QuestNotGroupOwnerText = "Only a group owner can accept and turn-in a group quest!";
                fixes.Add("Corrected QuestNotGroupOwnerText");
            }
            if (GroupQuestMode is null or < 0 or > 3)
            {
                GroupQuestMode = 0;
                fixes.Add("Corrected GroupQuestMode");
            }
            if (string.IsNullOrWhiteSpace(WeeklyResetDay))
            {
                WeeklyResetDay = "Wednesday";
                fixes.Add("Corrected WeeklyResetDay");
            }
            if (WeeklyResetHour is null or < 0 or > 23)
            {
                WeeklyResetHour = 8;
                fixes.Add("Corrected WeeklyResetHour");
            }
            if (WeeklyResetMinute is null or < 0 or > 59)
            {
                WeeklyResetMinute = 0;
                fixes.Add("Corrected WeeklyResetMinute");
            }
            if (DailyResetHour is null or < 0 or > 23)
            {
                DailyResetHour = 8;
                fixes.Add("Corrected DailyResetHour");
            }
            if (DailyResetMinute is null or < 0 or > 59)
            {
                DailyResetMinute = 0;
                fixes.Add("Corrected DailyResetMinute");
            }
            if (UseUTCTime is null or < 0 or > 1)
            {
                UseUTCTime = 0;
                fixes.Add("Corrected UseUTCTime");
            }
            if (UseQuestNPCIndicators is null or < 0 or > 1)
            {
                UseQuestNPCIndicators = 1;
                fixes.Add("Corrected UseQuestNPCIndicators");
            }
            if (MaxActiveQuests is null)
            {
                MaxActiveQuests = -1;
                fixes.Add("Corrected MaxActiveQuests");
            }
            return fixes;
        }
        public bool Equals(ExpansionQuestSettings? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                m_Version == other.m_Version &&
                EnableQuests == other.EnableQuests &&
                EnableQuestLogTab == other.EnableQuestLogTab &&
                CreateQuestNPCMarkers == other.CreateQuestNPCMarkers &&

                QuestAcceptedTitle == other.QuestAcceptedTitle &&
                QuestAcceptedText == other.QuestAcceptedText &&
                QuestCompletedTitle == other.QuestCompletedTitle &&
                QuestCompletedText == other.QuestCompletedText &&
                QuestFailedTitle == other.QuestFailedTitle &&
                QuestFailedText == other.QuestFailedText &&
                QuestCanceledTitle == other.QuestCanceledTitle &&
                QuestCanceledText == other.QuestCanceledText &&
                QuestTurnInTitle == other.QuestTurnInTitle &&
                QuestTurnInText == other.QuestTurnInText &&
                QuestObjectiveCompletedTitle == other.QuestObjectiveCompletedTitle &&
                QuestObjectiveCompletedText == other.QuestObjectiveCompletedText &&

                QuestCooldownTitle == other.QuestCooldownTitle &&
                QuestCooldownText == other.QuestCooldownText &&

                QuestNotInGroupTitle == other.QuestNotInGroupTitle &&
                QuestNotInGroupText == other.QuestNotInGroupText &&

                QuestNotGroupOwnerTitle == other.QuestNotGroupOwnerTitle &&
                QuestNotGroupOwnerText == other.QuestNotGroupOwnerText &&
                GroupQuestMode == other.GroupQuestMode &&

                AchievementCompletedTitle == other.AchievementCompletedTitle &&
                AchievementCompletedText == other.AchievementCompletedText &&

                WeeklyResetDay == other.WeeklyResetDay &&
                WeeklyResetMinute == other.WeeklyResetMinute &&
                WeeklyResetHour == other.WeeklyResetHour &&
                DailyResetHour == other.DailyResetHour &&
                DailyResetMinute == other.DailyResetMinute &&
                UseUTCTime == other.UseUTCTime &&
                UseQuestNPCIndicators == other.UseQuestNPCIndicators &&
                MaxActiveQuests == other.MaxActiveQuests;
        }
        public ExpansionQuestSettings Clone()
        {
            return new ExpansionQuestSettings
            {
                m_Version = this.m_Version,
                EnableQuests = this.EnableQuests,
                EnableQuestLogTab = this.EnableQuestLogTab,
                CreateQuestNPCMarkers = this.CreateQuestNPCMarkers,

                QuestAcceptedTitle = this.QuestAcceptedTitle,
                QuestAcceptedText = this.QuestAcceptedText,
                QuestCompletedTitle = this.QuestCompletedTitle,
                QuestCompletedText = this.QuestCompletedText,
                QuestFailedTitle = this.QuestFailedTitle,
                QuestFailedText = this.QuestFailedText,
                QuestCanceledTitle = this.QuestCanceledTitle,
                QuestCanceledText = this.QuestCanceledText,
                QuestTurnInTitle = this.QuestTurnInTitle,
                QuestTurnInText = this.QuestTurnInText,
                QuestObjectiveCompletedTitle = this.QuestObjectiveCompletedTitle,
                QuestObjectiveCompletedText = this.QuestObjectiveCompletedText,

                QuestCooldownTitle = this.QuestCooldownTitle,
                QuestCooldownText = this.QuestCooldownText,

                QuestNotInGroupTitle = this.QuestNotInGroupTitle,
                QuestNotInGroupText = this.QuestNotInGroupText,

                QuestNotGroupOwnerTitle = this.QuestNotGroupOwnerTitle,
                QuestNotGroupOwnerText = this.QuestNotGroupOwnerText,
                GroupQuestMode = this.GroupQuestMode,

                AchievementCompletedTitle = this.AchievementCompletedTitle,
                AchievementCompletedText = this.AchievementCompletedText,

                WeeklyResetDay = this.WeeklyResetDay,
                WeeklyResetMinute = this.WeeklyResetMinute,
                WeeklyResetHour = this.WeeklyResetHour,
                DailyResetHour = this.DailyResetHour,
                DailyResetMinute = this.DailyResetMinute,
                UseUTCTime = this.UseUTCTime,
                UseQuestNPCIndicators = this.UseQuestNPCIndicators,
                MaxActiveQuests = this.MaxActiveQuests
            };
        }

    }
}
