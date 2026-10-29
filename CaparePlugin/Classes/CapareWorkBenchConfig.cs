using Day2eEditor;
using System.ComponentModel;
using System.Reflection;
using System.Text.Json;
using static FileService;

namespace CaparePlugin
{
    /// <summary>
    /// $profile:Capare_Workbench/CapareWorkBenchConfig.json
    /// Capare_WorkBench - CapareWorkBenchConfig.c
    /// </summary>
    public class CapareWorkBenchConfig : CapareConfigBase<CapareWorkBenchData>
    {
        public CapareWorkBenchConfig(string path) : base(path)
        {
        }
        protected override CapareWorkBenchData CreateDefaultData()
        {
            return CapareWorkBenchData.CreateDefault();
        }
        protected override IEnumerable<string> ValidateData()
        {
            return Data.FixMissingOrInvalidFields();
        }
        protected override void OnBeforeSave(CapareWorkBenchData data)
        {
            data.AssignRecipeIds();
        }
    }

    public class CapareWorkBenchData : IEquatable<CapareWorkBenchData>, IDeepCloneable<CapareWorkBenchData>
    {
        public static readonly string[] CraftTypes = { "craft" };
        // Manuals that unlock a recipe, empty = always known
        public static readonly string[] RecipeCategories = { "", "CapareAmmoManual", "CapareDeadlySkillsManual", "CapareSurvivalManual", "CapareToolsManual", "CapareWeaponsManual" };

        public bool DebugLogging { get; set; }
        public CapareRepairSettings Repairing { get; set; }
        public CapareToolWearSettings ToolWear { get; set; }
        public BindingList<CapareWorkBenchRecipe> WorkbenchRecipes { get; set; }

        public CapareWorkBenchData()
        {
        }

        // The mod's default recipe list is generated from the same source as SampleConfigs/CapareWorkBenchConfig.json,
        // so that file is embedded as the default
        public static CapareWorkBenchData CreateDefault()
        {
            using Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CaparePlugin.Defaults.CapareWorkBenchConfig.json");
            if (stream != null)
            {
                var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true, Converters = { new BoolConverter() } };
                CapareWorkBenchData? data = JsonSerializer.Deserialize<CapareWorkBenchData>(stream, options);
                if (data != null)
                {
                    data.FixMissingOrInvalidFields();
                    return data;
                }
            }
            var empty = new CapareWorkBenchData();
            empty.FixMissingOrInvalidFields();
            return empty;
        }

        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();
            if (Repairing == null)
            {
                Repairing = new CapareRepairSettings();
                fixes.Add("Initialized Repairing");
            }
            if (ToolWear == null)
            {
                ToolWear = new CapareToolWearSettings();
                fixes.Add("Initialized ToolWear");
            }
            if (WorkbenchRecipes == null)
            {
                WorkbenchRecipes = new BindingList<CapareWorkBenchRecipe>();
                fixes.Add("Initialized WorkbenchRecipes");
            }
            foreach (CapareWorkBenchRecipe recipe in WorkbenchRecipes)
            {
                if (recipe.Recipe_Ingredients == null)
                {
                    recipe.Recipe_Ingredients = new BindingList<CapareRecipeIngredient>();
                    fixes.Add($"Initialized Recipe_Ingredients for {recipe.Recipe_Name}");
                }
                if (recipe.AttachmentsNeeded == null)
                {
                    recipe.AttachmentsNeeded = new BindingList<string>();
                    fixes.Add($"Initialized AttachmentsNeeded for {recipe.Recipe_Name}");
                }
            }
            return fixes;
        }

        // Same as AssignRecipeIds() in the mod, which runs every time the server loads the file
        public void AssignRecipeIds()
        {
            for (int i = 0; i < WorkbenchRecipes.Count; i++)
            {
                WorkbenchRecipes[i].RecipeId = i + 1;
            }
        }

        public bool Equals(CapareWorkBenchData other) => CapareJson.AreEqual(this, other);
        public override bool Equals(object? obj) => Equals(obj as CapareWorkBenchData);
        public override int GetHashCode() => base.GetHashCode();
        public CapareWorkBenchData Clone() => CapareJson.Clone(this);
    }
    public class CapareWorkBenchRecipe
    {
        public string Recipe_Name { get; set; }
        public string ResultName { get; set; }
        public string ResultDescription { get; set; }
        public string PreviewName { get; set; }
        public bool IsQuantity { get; set; }
        public int ResultCount { get; set; }
        public string CraftType { get; set; }
        public string RecipeCategory { get; set; }
        public int CraftTimeSeconds { get; set; }
        public float PowerConsumption { get; set; }
        public float GasConsumption { get; set; }
        public bool IsAmmo { get; set; }
        public int RecipeId { get; set; }
        public BindingList<CapareRecipeIngredient> Recipe_Ingredients { get; set; }
        public BindingList<string> AttachmentsNeeded { get; set; }

        public CapareWorkBenchRecipe() { }
        public static CapareWorkBenchRecipe CreateNew(string name)
        {
            return new CapareWorkBenchRecipe()
            {
                Recipe_Name = name,
                ResultName = "",
                ResultDescription = "",
                PreviewName = "",
                IsQuantity = false,
                ResultCount = 1,
                CraftType = "craft",
                RecipeCategory = "",
                CraftTimeSeconds = 5,
                PowerConsumption = 0,
                GasConsumption = 0,
                IsAmmo = false,
                Recipe_Ingredients = new BindingList<CapareRecipeIngredient>(),
                AttachmentsNeeded = new BindingList<string>()
            };
        }
        public override string ToString() => Recipe_Name;
    }
    public class CapareRecipeIngredient
    {
        public string IngredientName { get; set; }
        public string PreviewImage { get; set; }
        public string PreviewName { get; set; }
        public bool IsQuantity { get; set; }
        public int IngredientAmount { get; set; }
        public bool DestroyAfterUse { get; set; }
        public float HealthDecrease { get; set; }

        public CapareRecipeIngredient() { }
        public static CapareRecipeIngredient CreateNew(string name)
        {
            return new CapareRecipeIngredient()
            {
                IngredientName = name,
                PreviewImage = "",
                PreviewName = "",
                IsQuantity = false,
                IngredientAmount = 1,
                DestroyAfterUse = true,
                HealthDecrease = 0
            };
        }
        public override string ToString() => $"{IngredientName} x{IngredientAmount}";
    }
    public class CapareRepairSettings
    {
        public bool AllowRepairFromRuined { get; set; } = true;
        public int RuinedPunishmentMultiplier { get; set; } = 5;
        public int SewingKitUsage { get; set; } = 10;
        public int LeatherSewingKitUsage { get; set; } = 15;
        public int WeaponCleaningKitUsage { get; set; } = 20;
        public int WhetstoneUsage { get; set; } = 5;
        public int TireRepairKitUsage { get; set; } = 25;
        public int ElectronicRepairKitUsage { get; set; } = 10;
        public int EpoxyPuttyUsage { get; set; } = 10;
    }
    // Health lost per successful craft/salvage that uses the tool. 0 = never wears.
    public class CapareToolWearSettings
    {
        public float ToolDamagePerUse { get; set; } = 5;
        public float WearPartDamagePerUse { get; set; } = 8;
    }
}
