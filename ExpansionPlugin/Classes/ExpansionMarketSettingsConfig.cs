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
    public class ExpansionMarketSettingsConfig : IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path); // e.g., "types.xml"
        public string FilePath => _path;
        public MarketSettings Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }
        public const int CurrentVersion = 17;
        public ExpansionMarketSettingsConfig(string path)
        {
            _path = path;
        }

        public void Load()
        {
            Data = null;
            Data = AppServices.GetRequired<FileService>().LoadOrCreateJson<MarketSettings>(
                _path,
                createNew: () => new MarketSettings(CurrentVersion),
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
                configName: "ExpansionMarketSettings"
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
    public class MarketSettings
    {
        public int m_Version { get; set; }
        public int? MarketSystemEnabled { get; set; }
        public BindingList<string> NetworkCategories { get; set; } //empty atm
        public string? CurrencyIcon { get; set; }
        public int? ATMSystemEnabled { get; set; }
        public int? MaxDepositMoney { get; set; }
        public int? DefaultDepositMoney { get; set; }
        public int? ATMPlayerTransferEnabled { get; set; }
        public int? ATMPartyLockerEnabled { get; set; }
        public int? MaxPartyDepositMoney { get; set; }
        public int? UseWholeMapForATMPlayerList { get; set; }
        public decimal? SellPricePercent { get; set; }
        public int? NetworkBatchSize { get; set; }
        public decimal? MaxVehicleDistanceToTrader { get; set; }
        public decimal? MaxLargeVehicleDistanceToTrader { get; set; }

        public BindingList<string> LargeVehicles { get; set; }
        public BindingList<ExpansionMarketSpawnPosition> LandSpawnPositions { get; set; }
        public BindingList<ExpansionMarketSpawnPosition> AirSpawnPositions { get; set; }
        public BindingList<ExpansionMarketSpawnPosition> WaterSpawnPositions { get; set; }
        public BindingList<ExpansionMarketSpawnPosition> TrainSpawnPositions { get; set; }
        public MarketMenuColours MarketMenuColors { get; set; }
        public BindingList<string> Currencies { get; set; }
        public BindingList<string> VehicleKeys { get; set; }

        public decimal? MaxSZVehicleParkingTime { get; set; }
        public int? SZVehicleParkingTicketFine { get; set; }
        public int? SZVehicleParkingFineUseKey { get; set; }
        public int? DisallowUnpersisted { get; set; }
        public int? DisableClientSellTransactionDetails { get; set; }


        public MarketSettings() { }
        public MarketSettings(int CurrentVersion)
        {
            m_Version = CurrentVersion;
            MarketSystemEnabled = 1;
            NetworkCategories = new BindingList<string>();
            MaxVehicleDistanceToTrader = 120;
            MaxLargeVehicleDistanceToTrader = 744;
            LargeVehicles = new BindingList<string>()
            {
                "expansionlhd"
            };
            LandSpawnPositions = new BindingList<ExpansionMarketSpawnPosition>();
            AirSpawnPositions = new BindingList<ExpansionMarketSpawnPosition>();
            WaterSpawnPositions = new BindingList<ExpansionMarketSpawnPosition>();
            TrainSpawnPositions = new BindingList<ExpansionMarketSpawnPosition>();
            Deafultspawnspositions();
            MarketMenuColors = new MarketMenuColours();

            CurrencyIcon = "DayZExpansion/Core/GUI/icons/misc/coinstack2_64x64.edds";
            SellPricePercent = 75;
            NetworkBatchSize = 100;  //! Sync at most n items per batch

            ATMSystemEnabled = 1;
            MaxDepositMoney = 100000;
            DefaultDepositMoney = 10000;
            ATMPlayerTransferEnabled = 1;
            ATMPartyLockerEnabled = 1;
            MaxPartyDepositMoney = 100000;
            UseWholeMapForATMPlayerList = 0;
            Currencies = new BindingList<string>()
            {
                "expansionbanknotehryvnia"
            };
            VehicleKeys = new BindingList<string>()
            {
                "ExpansionCarKey"
            };
            MaxSZVehicleParkingTime = 1800;  //! 30 minutes
            SZVehicleParkingTicketFine = 0;
            SZVehicleParkingFineUseKey = 1;
            DisallowUnpersisted = 0;
            DisableClientSellTransactionDetails = 0;
        }
        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();
            if (m_Version < ExpansionMarketSettingsConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionMarketSettingsConfig.CurrentVersion}");
                m_Version = ExpansionMarketSettingsConfig.CurrentVersion;
            }

            if(MarketSystemEnabled == null ||(MarketSystemEnabled != 0 && MarketSystemEnabled != 1))
            {
                MarketSystemEnabled = 1;
                fixes.Add("Corrected MarketSystemEnabled");
            }

            if(CurrencyIcon == null)
            {
                CurrencyIcon = "DayZExpansion/Core/GUI/icons/misc/coinstack2_64x64.edds";
                fixes.Add("Set default CurrencyIcon");
            }

            // ATM system enabled (0/1)
            if (ATMSystemEnabled == null || (ATMSystemEnabled != 0 && ATMSystemEnabled != 1))
            {
                ATMSystemEnabled = 1;
                fixes.Add("Corrected ATMSystemEnabled");
            }

            // Max deposit money (>0)
            if (MaxDepositMoney == null || MaxDepositMoney <= 0)
            {
                MaxDepositMoney = 100000;
                fixes.Add("Set default MaxDepositMoney");
            }

            // Default deposit money (>=0 and <= MaxDepositMoney)
            if (DefaultDepositMoney == null || DefaultDepositMoney < 0 ||
                (MaxDepositMoney != null && DefaultDepositMoney > MaxDepositMoney))
            {
                DefaultDepositMoney = 10000;
                fixes.Add("Set default DefaultDepositMoney");
            }

            // ATM player transfer enabled (0/1)
            if (ATMPlayerTransferEnabled == null || (ATMPlayerTransferEnabled != 0 && ATMPlayerTransferEnabled != 1))
            {
                ATMPlayerTransferEnabled = 1;
                fixes.Add("Corrected ATMPlayerTransferEnabled");
            }

            // ATM party locker enabled (0/1)
            if (ATMPartyLockerEnabled == null || (ATMPartyLockerEnabled != 0 && ATMPartyLockerEnabled != 1))
            {
                ATMPartyLockerEnabled = 1;
                fixes.Add("Corrected ATMPartyLockerEnabled");
            }

            // Max party deposit money (>0)
            if (MaxPartyDepositMoney == null || MaxPartyDepositMoney <= 0)
            {
                MaxPartyDepositMoney = 100000;
                fixes.Add("Set default MaxPartyDepositMoney");
            }

            // Use whole map for ATM player list (0/1)
            if (UseWholeMapForATMPlayerList == null || (UseWholeMapForATMPlayerList != 0 && UseWholeMapForATMPlayerList != 1))
            {
                UseWholeMapForATMPlayerList = 0;
                fixes.Add("Corrected UseWholeMapForATMPlayerList");
            }

            // Sell price percent (0–100)
            if (SellPricePercent == null)
            {
                SellPricePercent = 75m;
                fixes.Add("Set default SellPricePercent");
            }

            // Network batch size (>0)
            if (NetworkBatchSize == null || NetworkBatchSize <= 0)
            {
                NetworkBatchSize = 100; // Sync at most n items per batch
                fixes.Add("Set default NetworkBatchSize");
            }

            // Max vehicle distance to trader (>0)
            if (MaxVehicleDistanceToTrader == null || MaxVehicleDistanceToTrader <= 0m)
            {
                MaxVehicleDistanceToTrader = 120m;
                fixes.Add("Set default MaxVehicleDistanceToTrader");
            }

            // Max large vehicle distance to trader (>0)
            if (MaxLargeVehicleDistanceToTrader == null || MaxLargeVehicleDistanceToTrader <= 0m)
            {
                MaxLargeVehicleDistanceToTrader = 744m;
                fixes.Add("Set default MaxLargeVehicleDistanceToTrader");
            }

            // Max SZ vehicle parking time (>=0)
            if (MaxSZVehicleParkingTime == null || MaxSZVehicleParkingTime < 0m)
            {
                MaxSZVehicleParkingTime = 1800m; // 30 minutes
                fixes.Add("Set default MaxSZVehicleParkingTime");
            }

            // SZ vehicle parking ticket fine (>=0)
            if (SZVehicleParkingTicketFine == null || SZVehicleParkingTicketFine < 0)
            {
                SZVehicleParkingTicketFine = 0;
                fixes.Add("Set default SZVehicleParkingTicketFine");
            }

            // SZ vehicle parking fine use key (0/1)
            if (SZVehicleParkingFineUseKey == null || (SZVehicleParkingFineUseKey != 0 && SZVehicleParkingFineUseKey != 1))
            {
                SZVehicleParkingFineUseKey = 1;
                fixes.Add("Corrected SZVehicleParkingFineUseKey");
            }

            // Disallow unpersisted (0/1)
            if (DisallowUnpersisted == null || (DisallowUnpersisted != 0 && DisallowUnpersisted != 1))
            {
                DisallowUnpersisted = 0;
                fixes.Add("Corrected DisallowUnpersisted");
            }

            // Disable client sell transaction details (0/1)
            if (DisableClientSellTransactionDetails == null || (DisableClientSellTransactionDetails != 0 && DisableClientSellTransactionDetails != 1))
            {
                DisableClientSellTransactionDetails = 0;
                fixes.Add("Corrected DisableClientSellTransactionDetails");
            }
            if(NetworkCategories == null)
            {
                NetworkCategories = new BindingList<string>();
                fixes.Add("Initilised NetworkCategories");
            }
            if (LargeVehicles == null)
            {
                LargeVehicles = new BindingList<string>();
                fixes.Add("Initilised LargeVehicles");
            }
            if(Currencies == null)
            {
                Currencies = new BindingList<string>();
                fixes.Add("Initilised Currencies");
            }
            if(VehicleKeys == null)
            {
                VehicleKeys = new BindingList<string>();
                fixes.Add("Initilised VehicleKeys");
            }
            if (LandSpawnPositions == null)
            {
                LandSpawnPositions = new BindingList<ExpansionMarketSpawnPosition>();
                fixes.Add("Initilised LandSpawnPositions");
            }
            if (AirSpawnPositions == null)
            {
                AirSpawnPositions = new BindingList<ExpansionMarketSpawnPosition>();
                fixes.Add("Initilised AirSpawnPositions");
            }
            if (WaterSpawnPositions == null)
            {
                WaterSpawnPositions = new BindingList<ExpansionMarketSpawnPosition>();
                fixes.Add("Initilised WaterSpawnPositions");
            }
            if (TrainSpawnPositions == null)
            {
                TrainSpawnPositions = new BindingList<ExpansionMarketSpawnPosition>();
                fixes.Add("Initilised TrainSpawnPositions");
            }
            if(MarketMenuColors == null)
            {
                MarketMenuColors = new MarketMenuColours();
            }
            // Helper function to set default color if null or whitespace
            string SetDefaultColor(string currentValue, string defaultValue, string name)
            {
                if (string.IsNullOrWhiteSpace(currentValue))
                {
                    fixes.Add($"Set default {name}");
                    return defaultValue;
                }
                return currentValue;
            }

            MarketMenuColors.BaseColorVignette = SetDefaultColor(MarketMenuColors.BaseColorVignette, "000000C8", "BaseColorVignette");
            MarketMenuColors.BaseColorHeaders = SetDefaultColor(MarketMenuColors.BaseColorHeaders, "13171BFF", "BaseColorHeaders");
            MarketMenuColors.BaseColorLabels = SetDefaultColor(MarketMenuColors.BaseColorLabels, "27272DFF", "BaseColorLabels");
            MarketMenuColors.BaseColorText = SetDefaultColor(MarketMenuColors.BaseColorText, "FBFCFEFF", "BaseColorText");
            MarketMenuColors.BaseColorCheckboxes = SetDefaultColor(MarketMenuColors.BaseColorCheckboxes, "FBFCFEFF", "BaseColorCheckboxes");
            MarketMenuColors.BaseColorInfoSectionBackground = SetDefaultColor(MarketMenuColors.BaseColorInfoSectionBackground, "2225268C", "BaseColorInfoSectionBackground");
            MarketMenuColors.BaseColorTooltipsHeaders = SetDefaultColor(MarketMenuColors.BaseColorTooltipsHeaders, "000000F0", "BaseColorTooltipsHeaders");
            MarketMenuColors.BaseColorTooltipsBackground = SetDefaultColor(MarketMenuColors.BaseColorTooltipsBackground, "000000DC", "BaseColorTooltipsBackground");
            MarketMenuColors.ColorDecreaseQuantityButton = SetDefaultColor(MarketMenuColors.ColorDecreaseQuantityButton, "DD262614", "ColorDecreaseQuantityButton");
            MarketMenuColors.ColorDecreaseQuantityIcon = SetDefaultColor(MarketMenuColors.ColorDecreaseQuantityIcon, "DD262628", "ColorDecreaseQuantityIcon");
            MarketMenuColors.ColorSetQuantityButton = SetDefaultColor(MarketMenuColors.ColorSetQuantityButton, "C7265114", "ColorSetQuantityButton");
            MarketMenuColors.ColorIncreaseQuantityButton = SetDefaultColor(MarketMenuColors.ColorIncreaseQuantityButton, "A0CC7114", "ColorIncreaseQuantityButton");
            MarketMenuColors.ColorIncreaseQuantityIcon = SetDefaultColor(MarketMenuColors.ColorIncreaseQuantityIcon, "A0CC7128", "ColorIncreaseQuantityIcon");
            MarketMenuColors.ColorSellPanel = SetDefaultColor(MarketMenuColors.ColorSellPanel, "27272DFF", "ColorSellPanel");
            MarketMenuColors.ColorSellButton = SetDefaultColor(MarketMenuColors.ColorSellButton, "DD262628", "ColorSellButton");
            MarketMenuColors.ColorBuyPanel = SetDefaultColor(MarketMenuColors.ColorBuyPanel, "27272DFF", "ColorBuyPanel");
            MarketMenuColors.ColorBuyButton = SetDefaultColor(MarketMenuColors.ColorBuyButton, "A0CC7128", "ColorBuyButton");
            MarketMenuColors.ColorMarketIcon = SetDefaultColor(MarketMenuColors.ColorMarketIcon, "E241428C", "ColorMarketIcon");
            MarketMenuColors.ColorFilterOptionsButton = SetDefaultColor(MarketMenuColors.ColorFilterOptionsButton, "C726518C", "ColorFilterOptionsButton");
            MarketMenuColors.ColorFilterOptionsIcon = SetDefaultColor(MarketMenuColors.ColorFilterOptionsIcon, "C726518C", "ColorFilterOptionsIcon");
            MarketMenuColors.ColorSearchFilterButton = SetDefaultColor(MarketMenuColors.ColorSearchFilterButton, "C726518C", "ColorSearchFilterButton");
            MarketMenuColors.ColorCategoryButton = SetDefaultColor(MarketMenuColors.ColorCategoryButton, "C726518C", "ColorCategoryButton");
            MarketMenuColors.ColorCategoryCollapseIcon = SetDefaultColor(MarketMenuColors.ColorCategoryCollapseIcon, "C726518C", "ColorCategoryCollapseIcon");
            MarketMenuColors.ColorCurrencyDenominationText = SetDefaultColor(MarketMenuColors.ColorCurrencyDenominationText, "FFB418FF", "ColorCurrencyDenominationText");
            MarketMenuColors.ColorItemButton = SetDefaultColor(MarketMenuColors.ColorItemButton, "C726518C", "ColorItemButton");
            MarketMenuColors.ColorItemInfoIcon = SetDefaultColor(MarketMenuColors.ColorItemInfoIcon, "FFB418FF", "ColorItemInfoIcon");
            MarketMenuColors.ColorItemInfoTitle = SetDefaultColor(MarketMenuColors.ColorItemInfoTitle, "FFB418FF", "ColorItemInfoTitle");
            MarketMenuColors.ColorItemInfoHasContainerItems = SetDefaultColor(MarketMenuColors.ColorItemInfoHasContainerItems, "FFB418FF", "ColorItemInfoHasContainerItems");
            MarketMenuColors.ColorItemInfoHasAttachments = SetDefaultColor(MarketMenuColors.ColorItemInfoHasAttachments, "FFB418FF", "ColorItemInfoHasAttachments");
            MarketMenuColors.ColorItemInfoHasBullets = SetDefaultColor(MarketMenuColors.ColorItemInfoHasBullets, "FFB418FF", "ColorItemInfoHasBullets");
            MarketMenuColors.ColorItemInfoIsAttachment = SetDefaultColor(MarketMenuColors.ColorItemInfoIsAttachment, "FFB418FF", "ColorItemInfoIsAttachment");
            MarketMenuColors.ColorItemInfoIsEquiped = SetDefaultColor(MarketMenuColors.ColorItemInfoIsEquiped, "FFB418FF", "ColorItemInfoIsEquiped");
            MarketMenuColors.ColorItemInfoAttachments = SetDefaultColor(MarketMenuColors.ColorItemInfoAttachments, "FFB418FF", "ColorItemInfoAttachments");
            MarketMenuColors.ColorToggleCategoriesText = SetDefaultColor(MarketMenuColors.ColorToggleCategoriesText, "C726518C", "ColorToggleCategoriesText");
            MarketMenuColors.ColorCategoryCorners = SetDefaultColor(MarketMenuColors.ColorCategoryCorners, "FBFCFEFF", "ColorCategoryCorners");
            MarketMenuColors.ColorCategoryBackground = SetDefaultColor(MarketMenuColors.ColorCategoryBackground, "222526FF", "ColorCategoryBackground");
            MarketMenuColors.ColorPlayerStock = SetDefaultColor(MarketMenuColors.ColorPlayerStock, "2980B9FF", "ColorPlayerStock");
            MarketMenuColors.ColorRequirementsNotMet = SetDefaultColor(MarketMenuColors.ColorRequirementsNotMet, "EB4635FF", "ColorRequirementsNotMet");


            return fixes;
        }
        private void Deafultspawnspositions()
        {
            ExpansionMarketSpawnPosition position = new ExpansionMarketSpawnPosition();
            position.Position = new float[] { 11903.4f, 140.0f, 12455.1f };
            position.Orientation = new float[] { 24.0f, 0.0f, 0.0f };
            LandSpawnPositions.Add(position);

            position = new ExpansionMarketSpawnPosition();
            position.Position = new float[] { 11898.4f, 140.0f, 12481.6f };
            position.Orientation = new float[] { 24.0f, 0.0f, 0.0f };
            LandSpawnPositions.Add(position);

            position = new ExpansionMarketSpawnPosition();
            position.Position = new float[] { 11878.0f, 140.0f, 12482.8f };
            position.Orientation = new float[] { 24.0f, 0.0f, 0.0f };
            LandSpawnPositions.Add(position);

            //! Cars - Vehicle Trader - Kamenka
            position = new ExpansionMarketSpawnPosition();
            position.Position = new float[] { 1145.0f, 6.0f, 2405.0f };
            position.Orientation = new float[] { 0.0f, 0.0f, 0.0f };
            LandSpawnPositions.Add(position);

            // Cars - Vehicle Trader - Green Mountain Trader
            position = new ExpansionMarketSpawnPosition();
            position.Position = new float[] { 3722.77f, 402.0f, 6018.93f };
            position.Orientation = new float[] { 138.0f, 0.0f, 0.0f };
            LandSpawnPositions.Add(position);

            position = new ExpansionMarketSpawnPosition();
            position.Position = new float[] { 3737.19f, 402.7f, 6001.95f };
            position.Orientation = new float[] { 138.0f, 0.0f, 0.0f };
            LandSpawnPositions.Add(position);

            //! Aircraft - Aircraft Trader - Krasno
            position = new ExpansionMarketSpawnPosition();
            position.Position = new float[] { 12178.9f, 140.0f, 12638.4f };
            position.Orientation = new float[] { -157.2f, 0.0f, 0.0f };
            AirSpawnPositions.Add(position);

            position = new ExpansionMarketSpawnPosition();
            position.Position = new float[] { 12126.7f, 140.0f, 12664.7f };
            position.Orientation = new float[] { -66.6f, 0.0f, 0.0f };
            AirSpawnPositions.Add(position);

            //! Aircraft - Aircraft Trader - Balota
            position = new ExpansionMarketSpawnPosition();
            position.Position = new float[] { 5006.27f, 9.5f, 2491.1f };
            position.Orientation = new float[] { -131.7f, 0.0f, 0.0f };
            AirSpawnPositions.Add(position);

            position = new ExpansionMarketSpawnPosition();
            position.Position = new float[] { 4982.0f, 9.5f, 2468.0f };
            position.Orientation = new float[] { -131.7f, 0.0f, 0.0f };
            AirSpawnPositions.Add(position);

            position = new ExpansionMarketSpawnPosition();
            position.Position = new float[] { 4968.0f, 9.5f, 2513.0f };
            position.Orientation = new float[] { -131.7f, 0.0f, 0.0f };
            AirSpawnPositions.Add(position);

            //! Water - Boats Trader - Kamenka
            position = new ExpansionMarketSpawnPosition();
            position.Position = new float[] { 1759.0f, 0.0f, 1994.0f };
            position.Orientation = new float[] { 0.0f, 0.0f, 0.0f };
            WaterSpawnPositions.Add(position);

            //! Water - Boats Trader - Sventlo
            position = new ExpansionMarketSpawnPosition();
            position.Position = new float[] { 14347.8f, 0.0f, 13235.8f };
            position.Orientation = new float[] { -147.5f, 0.0f, 0.0f };
            WaterSpawnPositions.Add(position);

            position = new ExpansionMarketSpawnPosition();
            position.Position = new float[] { 14344.1f, 0.0f, 13219.7f };
            position.Orientation = new float[] { -147.5f, 0.0f, 0.0f };
            WaterSpawnPositions.Add(position);

            position = new ExpansionMarketSpawnPosition();
            position.Position = new float[] { 14360.9f, 0.0f, 13246.7f };
            position.Orientation = new float[] { -147.5f, 0.0f, 0.0f };
            WaterSpawnPositions.Add(position);

            //! Water - LHD - Boats Trader - Kamenka
            position = new ExpansionMarketSpawnPosition();
            position.Position = new float[] { 1760.0f, 0.0f, 1730.0f };
            position.Orientation = new float[] { 0.0f, 0.0f, 0.0f };
            WaterSpawnPositions.Add(position);

            //! Water - LHD - Boats Trader - Sventlo
            position = new ExpansionMarketSpawnPosition();
            position.Position = new float[] { 14540.0f, 0.0f, 12995.0f };
            position.Orientation = new float[] { 0.0f, 0.0f, 0.0f };
            WaterSpawnPositions.Add(position);
        }
        public override bool Equals(object obj)
        {
            if (obj is not MarketSettings other)
                return false;


            return m_Version == other.m_Version &&
                MarketSystemEnabled == other.MarketSystemEnabled &&
                CurrencyIcon == other.CurrencyIcon &&
                ATMSystemEnabled == other.ATMSystemEnabled &&
                MaxDepositMoney == other.MaxDepositMoney &&
                DefaultDepositMoney == other.DefaultDepositMoney &&
                ATMPlayerTransferEnabled == other.ATMPlayerTransferEnabled &&
                ATMPartyLockerEnabled == other.ATMPartyLockerEnabled &&
                MaxPartyDepositMoney == other.MaxPartyDepositMoney &&
                UseWholeMapForATMPlayerList == other.UseWholeMapForATMPlayerList &&
                SellPricePercent == other.SellPricePercent &&
                NetworkBatchSize == other.NetworkBatchSize &&
                MaxVehicleDistanceToTrader == other.MaxVehicleDistanceToTrader &&
                MaxLargeVehicleDistanceToTrader == other.MaxLargeVehicleDistanceToTrader &&
                MaxSZVehicleParkingTime == other.MaxSZVehicleParkingTime &&
                SZVehicleParkingTicketFine == other.SZVehicleParkingTicketFine &&
                SZVehicleParkingFineUseKey == other.SZVehicleParkingFineUseKey &&
                DisallowUnpersisted == other.DisallowUnpersisted &&
                DisableClientSellTransactionDetails == other.DisableClientSellTransactionDetails &&
                MarketMenuColors.Equals(other.MarketMenuColors) &&
                SequenceEqual(NetworkCategories, other.NetworkCategories) &&
                SequenceEqual(LargeVehicles, other.LargeVehicles) &&
                SequenceEqual(Currencies, other.Currencies) &&
                SequenceEqual(VehicleKeys, other.VehicleKeys) &&
                SequenceEqual(LandSpawnPositions, other.LandSpawnPositions) &&
                SequenceEqual(AirSpawnPositions, other.AirSpawnPositions) &&
                SequenceEqual(WaterSpawnPositions, other.WaterSpawnPositions) &&
                SequenceEqual(TrainSpawnPositions, other.TrainSpawnPositions);

        }
        private bool SequenceEqual(BindingList<string> a, BindingList<string> b)
        {
            return a != null && b != null && a.SequenceEqual(b);
        }
        private bool SequenceEqual(BindingList<ExpansionMarketSpawnPosition> a, BindingList<ExpansionMarketSpawnPosition> b)
        {
            return a != null && b != null && a.SequenceEqual(b);
        }
    }
    public class MarketMenuColours
    {
        public string? BaseColorVignette { get; set; }
        public string? BaseColorHeaders { get; set; }
        public string? BaseColorLabels { get; set; }
        public string? BaseColorText { get; set; }
        public string? BaseColorCheckboxes { get; set; }
        public string? BaseColorInfoSectionBackground { get; set; }
        public string? BaseColorTooltipsHeaders { get; set; }
        public string? BaseColorTooltipsBackground { get; set; }
        public string? ColorDecreaseQuantityButton { get; set; }
        public string? ColorDecreaseQuantityIcon { get; set; }
        public string? ColorSetQuantityButton { get; set; }
        public string? ColorIncreaseQuantityButton { get; set; }
        public string? ColorIncreaseQuantityIcon { get; set; }
        public string? ColorSellPanel { get; set; }
        public string? ColorSellButton { get; set; }
        public string? ColorBuyPanel { get; set; }
        public string? ColorBuyButton { get; set; }
        public string? ColorMarketIcon { get; set; }
        public string? ColorFilterOptionsButton { get; set; }
        public string? ColorFilterOptionsIcon { get; set; }
        public string? ColorSearchFilterButton { get; set; }
        public string? ColorCategoryButton { get; set; }
        public string? ColorCategoryCollapseIcon { get; set; }
        public string? ColorCurrencyDenominationText { get; set; }
        public string? ColorItemButton { get; set; }
        public string? ColorItemInfoIcon { get; set; }
        public string? ColorItemInfoTitle { get; set; }
        public string? ColorItemInfoHasContainerItems { get; set; }
        public string? ColorItemInfoHasAttachments { get; set; }
        public string? ColorItemInfoHasBullets { get; set; }
        public string? ColorItemInfoIsAttachment { get; set; }
        public string? ColorItemInfoIsEquiped { get; set; }
        public string? ColorItemInfoAttachments { get; set; }
        public string? ColorToggleCategoriesText { get; set; }
        public string? ColorCategoryCorners { get; set; }
        public string? ColorCategoryBackground { get; set; }
        public string? ColorPlayerStock { get; set; }
        public string? ColorRequirementsNotMet { get; set; }

        public MarketMenuColours()
        {
            BaseColorVignette = "000000C8";
            BaseColorHeaders = "13171BFF";
            BaseColorLabels = "27272DFF";
            BaseColorText = "FBFCFEFF";
            BaseColorCheckboxes = "FBFCFEFF";
            BaseColorInfoSectionBackground = "2225268C";
            BaseColorTooltipsHeaders = "000000F0";
            BaseColorTooltipsBackground = "000000DC";
            ColorDecreaseQuantityButton = "DD262614";
            ColorDecreaseQuantityIcon = "DD262628";
            ColorSetQuantityButton = "C7265114";
            ColorIncreaseQuantityButton = "A0CC7114";
            ColorIncreaseQuantityIcon = "A0CC7128";
            ColorSellPanel = "27272DFF";
            ColorSellButton = "DD262628";
            ColorBuyPanel = "27272DFF";
            ColorBuyButton = "A0CC7128";
            ColorMarketIcon = "E241428C";
            ColorFilterOptionsButton = "C726518C";
            ColorFilterOptionsIcon = "C726518C";
            ColorSearchFilterButton = "C726518C";
            ColorCategoryButton = "C726518C";
            ColorCategoryCollapseIcon = "C726518C";
            ColorCurrencyDenominationText = "FFB418FF";
            ColorItemButton = "C726518C";
            ColorItemInfoIcon = "FFB418FF";
            ColorItemInfoTitle = "FFB418FF";
            ColorItemInfoHasContainerItems = "FFB418FF";
            ColorItemInfoHasAttachments = "FFB418FF";
            ColorItemInfoHasBullets = "FFB418FF";
            ColorItemInfoIsAttachment = "FFB418FF";
            ColorItemInfoIsEquiped = "FFB418FF";
            ColorItemInfoAttachments = "FFB418FF";
            ColorToggleCategoriesText = "C726518C";
            ColorCategoryCorners = "FBFCFEFF";
            ColorCategoryBackground = "222526FF";
            ColorPlayerStock = "2980B9FF";
            ColorRequirementsNotMet = "EB4635FF";
        }
        public override bool Equals(object obj)
        {
            if (obj is not MarketMenuColours other)
                return false;


            return BaseColorVignette == other.BaseColorVignette &&
                       BaseColorHeaders == other.BaseColorHeaders &&
                       BaseColorLabels == other.BaseColorLabels &&
                       BaseColorText == other.BaseColorText &&
                       BaseColorCheckboxes == other.BaseColorCheckboxes &&
                       BaseColorInfoSectionBackground == other.BaseColorInfoSectionBackground &&
                       BaseColorTooltipsHeaders == other.BaseColorTooltipsHeaders &&
                       BaseColorTooltipsBackground == other.BaseColorTooltipsBackground &&
                       ColorDecreaseQuantityButton == other.ColorDecreaseQuantityButton &&
                       ColorDecreaseQuantityIcon == other.ColorDecreaseQuantityIcon &&
                       ColorSetQuantityButton == other.ColorSetQuantityButton &&
                       ColorIncreaseQuantityButton == other.ColorIncreaseQuantityButton &&
                       ColorIncreaseQuantityIcon == other.ColorIncreaseQuantityIcon &&
                       ColorSellPanel == other.ColorSellPanel &&
                       ColorSellButton == other.ColorSellButton &&
                       ColorBuyPanel == other.ColorBuyPanel &&
                       ColorBuyButton == other.ColorBuyButton &&
                       ColorMarketIcon == other.ColorMarketIcon &&
                       ColorFilterOptionsButton == other.ColorFilterOptionsButton &&
                       ColorFilterOptionsIcon == other.ColorFilterOptionsIcon &&
                       ColorSearchFilterButton == other.ColorSearchFilterButton &&
                       ColorCategoryButton == other.ColorCategoryButton &&
                       ColorCategoryCollapseIcon == other.ColorCategoryCollapseIcon &&
                       ColorCurrencyDenominationText == other.ColorCurrencyDenominationText &&
                       ColorItemButton == other.ColorItemButton &&
                       ColorItemInfoIcon == other.ColorItemInfoIcon &&
                       ColorItemInfoTitle == other.ColorItemInfoTitle &&
                       ColorItemInfoHasContainerItems == other.ColorItemInfoHasContainerItems &&
                       ColorItemInfoHasAttachments == other.ColorItemInfoHasAttachments &&
                       ColorItemInfoHasBullets == other.ColorItemInfoHasBullets &&
                       ColorItemInfoIsAttachment == other.ColorItemInfoIsAttachment &&
                       ColorItemInfoIsEquiped == other.ColorItemInfoIsEquiped &&
                       ColorItemInfoAttachments == other.ColorItemInfoAttachments &&
                       ColorToggleCategoriesText == other.ColorToggleCategoriesText &&
                       ColorCategoryCorners == other.ColorCategoryCorners &&
                       ColorCategoryBackground == other.ColorCategoryBackground &&
                       ColorPlayerStock == other.ColorPlayerStock &&
                       ColorRequirementsNotMet == other.ColorRequirementsNotMet;
        }
        
    }
    public class ExpansionMarketSpawnPosition
    {
        public float[] Position { get; set; }
        public float[] Orientation { get; set; }

        public override string ToString()
        {
            return Position[0].ToString("F6") + " " + Position[1].ToString("F6") + " " + Position[2].ToString("F6");
        }
        public override bool Equals(object obj)
        {
            if (obj is not ExpansionMarketSpawnPosition other)
                return false;


            return Position.SequenceEqual(other.Position) &&
               Orientation.SequenceEqual(other.Orientation);

        }
    }
}
