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
    public class ExpansionQuestQuestConfig : MultiFileConfigLoaderBase<ExpansionQuestQuest>
    {
        public const int CurrentVersion = 22;
        public ExpansionQuestQuestConfig(string path) : base(path)
        {
        }
        protected override ExpansionQuestQuest LoadItem(string filePath)
        {
            var TraderZone = AppServices.GetRequired<FileService>().LoadOrCreateJson(
                    filePath,
                    createNew: () => new ExpansionQuestQuest(),
                    onError: ex => HandleItemError(filePath, ex),
                    configName: "ExpansionQuestQuest",
                    useVecConvertor: true
                );

            TraderZone.SetPath(filePath);
            TraderZone.SetGuid(Guid.NewGuid());
            return TraderZone;
        }
        protected override void SaveItem(ExpansionQuestQuest ExpansionQuestQuest)
        {
            AppServices.GetRequired<FileService>().SaveJson(ExpansionQuestQuest._path, ExpansionQuestQuest, false, true);
        }
        protected override string GetItemFileName(ExpansionQuestQuest ExpansionQuestQuest)
            => ExpansionQuestQuest.FileName;
        protected override string GetItemFilePath(ExpansionQuestQuest ExpansionQuestQuest)
            => ExpansionQuestQuest.FilePath;
        protected override bool ShouldDelete(ExpansionQuestQuest ExpansionQuestQuest)
            => ExpansionQuestQuest.ToDelete;
        protected override Guid GetID(ExpansionQuestQuest ExpansionQuestQuest)
            => ExpansionQuestQuest.Id;
        protected override void DeleteItemFile(ExpansionQuestQuest ExpansionQuestQuest)
        {
            if (!string.IsNullOrWhiteSpace(ExpansionQuestQuest._path) && File.Exists(ExpansionQuestQuest._path))
            {
                File.Delete(ExpansionQuestQuest._path);
            }
        }
        public bool needToSave()
        {
            return false;
        }
        protected override IEnumerable<string> ValidateItem(ExpansionQuestQuest ExpansionQuestQuest)
        {
            return ExpansionQuestQuest.FixMissingOrInvalidFields();
        }
    }
    public class ExpansionQuestQuest: IDeepCloneable<ExpansionQuestQuest>, IEquatable<ExpansionQuestQuest>
    {
        [JsonIgnore]
        public string _path { get; private set; }
        [JsonIgnore]
        public List<string> FolderParts { get; private set; }
        [JsonIgnore]
        public string FileName => Path.GetFileName(_path);
        [JsonIgnore]
        public string FilePath => _path;
        [JsonIgnore]
        public bool ToDelete { get; set; }
        [JsonIgnore]
        public Guid Id { get; set; }

        public void SetPath(string path) => _path = path;
        internal void SetGuid(Guid guid) => Id = guid;

        public int ConfigVersion { get; set; }
        public int? ID { get; set; }
        public int? Type { get; set; }
        public string? Title { get; set; }
        public BindingList<string> Descriptions { get; set; }
        public string? ObjectiveText { get; set; }
        public int? FollowUpQuest { get; set; }
        public int? Repeatable { get; set; }
        public int? IsDailyQuest { get; set; }
        public int? IsWeeklyQuest { get; set; }
        public int CancelQuestOnPlayerDeath { get; set; }
        public int? Autocomplete { get; set; }
        public int? IsGroupQuest { get; set; }
        public string? ObjectSetFileName { get; set; }
        public BindingList<ExpansionQuestItemConfig> QuestItems { get; set; }
        public BindingList<ExpansionQuestRewardConfig> Rewards { get; set; }
        public int? NeedToSelectReward { get; set; }
        public int? RandomReward { get; set; }
        public int? RandomRewardAmount { get; set; }
        public int? RewardsForGroupOwnerOnly { get; set; }
        public int? RewardBehavior { get; set; }
        public BindingList<int> QuestGiverIDs { get; set; }
        public BindingList<int> QuestTurnInIDs { get; set; }
        public int? IsAchievement { get; set; }
        public BindingList<Objectives> Objectives { get; set; }
        public int? QuestColor { get; set; }
        public int? ReputationReward { get; set; }
        public int? ReputationRequirement { get; set; }
        public BindingList<int> PreQuestIDs { get; set; }
        public string? RequiredFaction { get; set; }
        public string? FactionReward { get; set; }
        public int? PlayerNeedQuestItems { get; set; }
        public int? DeleteQuestItems { get; set; }
        public int? SequentialObjectives { get; set; }
        public Dictionary<string, int> FactionReputationRequirements { get; set; }
        public Dictionary<string, int> FactionReputationRewards { get; set; }
        public int? SuppressQuestLogOnCompetion { get; set; }
        public int? Active { get; set; }

        public ExpansionQuestQuest Clone()
        {
            return new ExpansionQuestQuest()
            {
                ConfigVersion = ConfigVersion,
                ID = ID,
                Type = Type,
                Title = Title,
                Descriptions = Descriptions != null
                      ? new BindingList<string>(Descriptions.ToList())
                      : null,
                ObjectiveText = ObjectiveText,
                FollowUpQuest = FollowUpQuest,
                Repeatable = Repeatable,
                IsDailyQuest = IsDailyQuest,
                IsWeeklyQuest = IsWeeklyQuest,
                CancelQuestOnPlayerDeath = CancelQuestOnPlayerDeath,
                Autocomplete = Autocomplete,
                IsGroupQuest = IsGroupQuest,
                ObjectSetFileName = ObjectSetFileName,
                QuestItems = QuestItems != null
                      ? new BindingList<ExpansionQuestItemConfig>(
                          QuestItems.Select(x => x.Clone()).ToList())
                      : null,
                Rewards = Rewards != null
                      ? new BindingList<ExpansionQuestRewardConfig>(
                          Rewards.Select(x => x.Clone()).ToList())
                      : null,
                NeedToSelectReward = NeedToSelectReward,
                RandomReward = RandomReward,
                RandomRewardAmount = RandomRewardAmount,
                RewardsForGroupOwnerOnly = RewardsForGroupOwnerOnly,
                RewardBehavior = RewardBehavior,
                QuestGiverIDs = QuestGiverIDs != null
                      ? new BindingList<int>(QuestGiverIDs.ToList())
                      : null,
                QuestTurnInIDs = QuestTurnInIDs != null
                      ? new BindingList<int>(QuestTurnInIDs.ToList())
                      : null,
                IsAchievement = IsAchievement,
                Objectives = Objectives != null
                      ? new BindingList<Objectives>(
                          Objectives.Select(x => x.Clone()).ToList())
                      : null,
                QuestColor = QuestColor,
                ReputationReward = ReputationReward,
                ReputationRequirement = ReputationRequirement,
                PreQuestIDs = PreQuestIDs != null
                      ? new BindingList<int>(PreQuestIDs.ToList())
                      : null,
                RequiredFaction = RequiredFaction,
                FactionReward = FactionReward,
                PlayerNeedQuestItems = PlayerNeedQuestItems,
                DeleteQuestItems = DeleteQuestItems,
                SequentialObjectives = SequentialObjectives,
                FactionReputationRequirements = FactionReputationRequirements != null
                      ? new Dictionary<string, int>(FactionReputationRequirements)
                      : null,
                FactionReputationRewards = FactionReputationRewards != null
                      ? new Dictionary<string, int>(FactionReputationRewards)
                      : null,
                SuppressQuestLogOnCompetion = SuppressQuestLogOnCompetion,
                Active = Active,
                ToDelete = ToDelete,
                Id = Id
            };
        }
        public override bool Equals(object? obj) => Equals(obj as ExpansionQuestQuest);
        public bool Equals(ExpansionQuestQuest? other)
        {

            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return ConfigVersion == other.ConfigVersion
                && ID == other.ID
                && Type == other.Type
                && Equals(Title, other.Title)
                && ListEquals(Descriptions, other.Descriptions)
                && Equals(ObjectiveText, other.ObjectiveText)
                && FollowUpQuest == other.FollowUpQuest
                && Repeatable == other.Repeatable
                && IsDailyQuest == other.IsDailyQuest
                && IsWeeklyQuest == other.IsWeeklyQuest
                && CancelQuestOnPlayerDeath == other.CancelQuestOnPlayerDeath
                && Autocomplete == other.Autocomplete
                && IsGroupQuest == other.IsGroupQuest
                && Equals(ObjectSetFileName, other.ObjectSetFileName)
                && ListEquals(QuestItems, other.QuestItems)
                && ListEquals(Rewards, other.Rewards)
                && NeedToSelectReward == other.NeedToSelectReward
                && RandomReward == other.RandomReward
                && RandomRewardAmount == other.RandomRewardAmount
                && RewardsForGroupOwnerOnly == other.RewardsForGroupOwnerOnly
                && RewardBehavior == other.RewardBehavior
                && ListEquals(QuestGiverIDs, other.QuestGiverIDs)
                && ListEquals(QuestTurnInIDs, other.QuestTurnInIDs)
                && IsAchievement == other.IsAchievement
                && ListEquals(Objectives, other.Objectives)
                && QuestColor == other.QuestColor
                && ReputationReward == other.ReputationReward
                && ReputationRequirement == other.ReputationRequirement
                && ListEquals(PreQuestIDs, other.PreQuestIDs)
                && Equals(RequiredFaction, other.RequiredFaction)
                && Equals(FactionReward, other.FactionReward)
                && PlayerNeedQuestItems == other.PlayerNeedQuestItems
                && DeleteQuestItems == other.DeleteQuestItems
                && SequentialObjectives == other.SequentialObjectives
                && DictionaryEquals(FactionReputationRequirements, other.FactionReputationRequirements)
                && DictionaryEquals(FactionReputationRewards, other.FactionReputationRewards)
                && SuppressQuestLogOnCompetion == other.SuppressQuestLogOnCompetion
                && Active == other.Active;

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
        private static bool DictionaryEquals<TKey, TValue>(IDictionary<TKey, TValue>? a, IDictionary<TKey, TValue>? b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            if (a.Count != b.Count) return false;

            foreach (var kvp in a)
            {
                if (!b.TryGetValue(kvp.Key, out var value))
                    return false;

                if (!Equals(kvp.Value, value))
                    return false;
            }

            return true;
        }
        internal IEnumerable<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            if (ConfigVersion != ExpansionQuestQuestConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {ConfigVersion} to {ExpansionQuestQuestConfig.CurrentVersion}");
                ConfigVersion = ExpansionQuestQuestConfig.CurrentVersion;
            }


            return fixes;
        }
    }
    public class Objectives : IDeepCloneable<Objectives>, IEquatable<Objectives>
    {
        public int ConfigVersion { get; set; }
        public int ID { get; set; }
        public int ObjectiveType { get; set; }

        public Objectives Clone()
        {
            return new Objectives
            {
                ConfigVersion = ConfigVersion,
                ID = ID,
                ObjectiveType = ObjectiveType,
            };
        }
        public override bool Equals(object? obj) => Equals(obj as Objectives);
        public bool Equals(Objectives? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (ConfigVersion != other.ConfigVersion)
                return false;

            if (ID != other.ID)
                return false;

            if (ObjectiveType != other.ObjectiveType)
                return false;

            return true;
        }
    }
}
