using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    public partial class TraderPreviewForm : Form
    {
        private readonly ExpansionMarketTrader _trader;
        private readonly MarketMenuColours _Colors;

        private TableLayoutPanel _root;

        private AlphaPanel vignette;
        private AlphaPanel market_header;
        private AlphaPanel market_footer;
        private AlphaPanel market_header_options;

        private SplitContainer market_menu_content_split;

        private AlphaPanel market_content_panel;
        private AlphaPanel menu_info_background;
        private AlphaPanel market_menu_info_header;

        private AlphaPanel market_categories_scroller;
        private AlphaFlowLayoutPanel market_categories_container;

        private AlphaPanel market_filter_background;
        private TextBox market_filter_box;
        private Label market_title_text;
        private Label market_summary_text;
        private PictureBox market_icon;

        private Label market_item_header_text;
        private RichTextBox market_item_description;

        private readonly List<PreviewItemCard> _allCards = new();

        private Color UiText => ParseUiColor(_Colors.BaseColorText, Color.White);

        private Color UiHeadersFill => ParseUiColor(_Colors.BaseColorHeaders, Color.FromArgb(220, 28, 33, 38));
        private Color UiLabelsFill => ParseUiColor(_Colors.BaseColorLabels, Color.FromArgb(220, 39, 39, 45));
        private Color UiInfoFill => ParseUiColor(_Colors.BaseColorInfoSectionBackground, Color.FromArgb(180, 24, 28, 33));
        private Color UiTooltipFill => ParseUiColor(_Colors.BaseColorTooltipsBackground, Color.FromArgb(220, 16, 18, 22));
        private Color UiVignetteFill => ParseUiColor(_Colors.BaseColorVignette, Color.FromArgb(160, 0, 0, 0));

        private Color UiCategoryBgFill => ParseUiColor(_Colors.ColorCategoryBackground, Color.FromArgb(220, 34, 37, 38));
        private Color UiCategoryCorners => ParseUiColor(_Colors.ColorCategoryCorners, Color.White);
        private Color UiCategoryCollapseIcon => ParseUiColor(_Colors.ColorCategoryCollapseIcon, Color.Gainsboro);
        private Color UiCategoryText => ParseUiColor(_Colors.ColorToggleCategoriesText, UiText);

        private Color UiItemHoverFill => ParseUiColor(_Colors.ColorItemButton, Color.FromArgb(180, 60, 80, 110));
        private Color UiBuyFill => ParseUiColor(_Colors.ColorBuyButton, Color.FromArgb(160, 204, 113, 40));
        private Color UiSellFill => ParseUiColor(_Colors.ColorSellButton, Color.FromArgb(221, 38, 38, 40));
        private Color UiSetQuantityFill => ParseUiColor(_Colors.ColorSetQuantityButton, Color.FromArgb(199, 38, 81, 20));

        private Color UiMarketIcon => ParseUiColor(_Colors.ColorMarketIcon, UiText);
        private Color UiWarning => ParseUiColor(_Colors.ColorRequirementsNotMet, Color.OrangeRed);
        private Color UiAttachmentInfo => ParseUiColor(_Colors.ColorItemInfoAttachments, Color.Gold);

        public TraderPreviewForm(ExpansionMarketTrader trader, MarketMenuColours colors)
        {
            _trader = trader ?? throw new ArgumentNullException(nameof(trader));
            _Colors = colors ?? throw new ArgumentNullException(nameof(colors));

            InitializeComponent();
            BuildPreview();
        }

        private void InitializeComponent()
        {
            Text = $"Trader Preview - {_trader.DisplayName}";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(1400, 850);
            MinimumSize = new Size(1100, 700);
            BackColor = Color.Black;
            ForeColor = UiText;
            BackgroundImage = LoadPreviewBackground();
            BackgroundImageLayout = ImageLayout.Stretch;
            DoubleBuffered = true;

            vignette = new AlphaPanel
            {
                Dock = DockStyle.Fill,
                FillColor = UiVignetteFill,
                BorderColor = Color.Transparent,
                BorderThickness = 0
            };
            Controls.Add(vignette);

            _root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1,
                BackColor = Color.Transparent
            };

            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));   // market_header
            _root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));   // main content

            vignette.Controls.Add(_root);

            BuildMarketHeader();
            BuildMarketContent();

            Resize += (_, __) => UpdateSplitDistance();
        }

        private void BuildMarketHeader()
        {
            market_header = new AlphaPanel
            {
                Dock = DockStyle.Fill,
                FillColor = UiHeadersFill,
                BorderColor = Color.Transparent,
                BorderThickness = 0,
                Padding = new Padding(0)
            };

            Panel market_header_right = new Panel
            {
                Dock = DockStyle.Right,
                Width = 340,
                BackColor = FlattenColor(UiHeadersFill, Color.Black),
                Margin = new Padding(0),
                Padding = new Padding(0)
            };

            market_filter_background = new AlphaPanel
            {
                Location = new Point(10, 7),
                Size = new Size(320, 38),
                FillColor = UiLabelsFill,
                BorderColor = Color.Transparent,
                BorderThickness = 0,
                Padding = new Padding(0)
            };

            Label filterLabel = new Label
            {
                Text = "Search",
                Location = new Point(10, 9),
                Size = new Size(55, 20),
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = FlattenColor(UiText, Color.Black),
                BackColor = FlattenColor(UiLabelsFill, Color.Black)
            };

            market_filter_box = new TextBox
            {
                Location = new Point(68, 10),
                Size = new Size(240, 18),
                BorderStyle = BorderStyle.None,
                BackColor = FlattenColor(UiLabelsFill, Color.Black),
                ForeColor = FlattenColor(UiText, Color.Black),
                Font = new Font("Segoe UI", 9, FontStyle.Regular)
            };
            market_filter_box.TextChanged += (_, __) => ApplySearchFilter();

            market_filter_background.Controls.Add(filterLabel);
            market_filter_background.Controls.Add(market_filter_box);
            market_header_right.Controls.Add(market_filter_background);

            market_icon = new PictureBox
            {
                Location = new Point(8, 6),
                Size = new Size(40, 40),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = FlattenColor(UiHeadersFill, Color.Black),
                Image = GetIcon(_trader.TraderIcon, UiMarketIcon, 32)
            };

            market_title_text = new Label
            {
                Location = new Point(56, 6),
                Size = new Size(520, 22),
                Text = !string.IsNullOrWhiteSpace(_trader.DisplayName)
                    ? _trader.DisplayName
                    : _trader.FileName,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = FlattenColor(UiText, Color.Black),
                BackColor = FlattenColor(UiHeadersFill, Color.Black),
                TextAlign = ContentAlignment.MiddleLeft
            };

            market_summary_text = new Label
            {
                Location = new Point(56, 27),
                Size = new Size(600, 16),
                Font = new Font("Segoe UI", 8.5f, FontStyle.Regular),
                ForeColor = FlattenColor(UiText, Color.Black),
                BackColor = FlattenColor(UiHeadersFill, Color.Black),
                TextAlign = ContentAlignment.MiddleLeft
            };

            market_header.Controls.Add(market_header_right);
            market_header.Controls.Add(market_icon);
            market_header.Controls.Add(market_title_text);
            market_header.Controls.Add(market_summary_text);

            _root.Controls.Add(market_header, 0, 0);
        }

        private void BuildMarketContent()
        {
            market_menu_content_split = new SplitContainer
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                FixedPanel = FixedPanel.None,
                IsSplitterFixed = true,
                SplitterWidth = 2
            };

            market_menu_content_split.Panel1.BackColor = Color.Transparent;
            market_menu_content_split.Panel2.BackColor = Color.Transparent;

            _root.Controls.Add(market_menu_content_split, 0, 1);

            BuildLeftMarketContent();
            BuildRightInfoContent();
            UpdateSplitDistance();
        }

        private void BuildLeftMarketContent()
        {
            market_content_panel = new AlphaPanel
            {
                Dock = DockStyle.Fill,
                FillColor = Color.Transparent,
                BorderColor = Color.Transparent,
                BorderThickness = 0
            };

            var leftLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 1,
                ColumnCount = 1,
                BackColor = Color.Transparent
            };
            leftLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            market_categories_scroller = new AlphaPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FillColor = Color.Transparent,
                BorderColor = Color.Transparent,
                BorderThickness = 0,
                Padding = new Padding(8, 8, 8, 4)
            };

            market_categories_container = new AlphaFlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Dock = DockStyle.Top,
                Margin = new Padding(0),
                Padding = new Padding(0),
                FillColor = Color.Transparent,
                BorderColor = Color.Transparent,
                BorderThickness = 0
            };

            market_categories_scroller.Controls.Add(market_categories_container);

            leftLayout.Controls.Add(market_categories_scroller, 0, 0);

            market_content_panel.Controls.Add(leftLayout);
            market_menu_content_split.Panel1.Controls.Add(market_content_panel);
        }

        private void BuildRightInfoContent()
        {
            var rightLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1,
                BackColor = Color.Transparent
            };
            rightLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));
            rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            market_menu_info_header = new AlphaPanel
            {
                Dock = DockStyle.Fill,
                FillColor = UiHeadersFill,
                BorderColor = Color.Transparent,
                BorderThickness = 0,
                Padding = new Padding(10, 6, 10, 6)
            };

            market_item_header_text = new Label
            {
                Dock = DockStyle.Fill,
                Text = "market_item_header_text",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = UiText,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleLeft
            };
            market_menu_info_header.Controls.Add(market_item_header_text);

            menu_info_background = new AlphaPanel
            {
                Dock = DockStyle.Fill,
                FillColor = UiInfoFill,
                BorderColor = Color.Transparent,
                BorderThickness = 0,
                Padding = new Padding(10)
            };

            market_item_description = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                BackColor = FlattenColor(UiTooltipFill, Color.Black),
                ForeColor = FlattenColor(UiText, Color.Black),
                Font = new Font("Segoe UI", 9f, FontStyle.Regular),
                ScrollBars = RichTextBoxScrollBars.Vertical,
                DetectUrls = false,
                ShortcutsEnabled = false
            };

            menu_info_background.Controls.Add(market_item_description);

            rightLayout.Controls.Add(market_menu_info_header, 0, 0);
            rightLayout.Controls.Add(menu_info_background, 0, 1);

            market_menu_content_split.Panel2.Controls.Add(rightLayout);
        }


        private void UpdateSplitDistance()
        {
            if (market_menu_content_split == null || ClientSize.Width <= 0)
                return;

            int target = (int)Math.Round(market_menu_content_split.Width * 0.78);
            if (target > 100 && target < market_menu_content_split.Width - 100)
                market_menu_content_split.SplitterDistance = target;
        }

        private void BuildPreview()
        {
            market_categories_container.SuspendLayout();
            market_categories_container.Controls.Clear();
            _allCards.Clear();

            List<TraderPreviewCategoryGroup> groups = BuildPreviewGroups();

            var hiddenItems = groups
                .SelectMany(g => g.Items)
                .Where(IsHiddenPart)
                .ToList();

            foreach (var group in groups)
            {
                group.Items = group.Items
                    .Where(x => !IsHiddenPart(x))
                    .ToList();
            }

            groups = groups
                .Where(g => g.Items.Any())
                .ToList();

            if (hiddenItems.Any())
            {
                groups.Add(new TraderPreviewCategoryGroup
                {
                    Name = "Hidden Parts",
                    Path = "__hidden_parts__",
                    ColorHex = null,
                    IconName = null,
                    Items = hiddenItems
                });
            }

            groups = groups
                .OrderBy(x => string.Equals(x.Name, "Hidden Parts", StringComparison.OrdinalIgnoreCase) ? 1 : 0)
                .ThenBy(x => x.Name, StringComparer.OrdinalIgnoreCase)
                .ToList();

            foreach (var group in groups)
            {
                Control category = CreateCategorySection(group);
                market_categories_container.Controls.Add(category);
            }

            int categoryCount = groups.Count;
            int totalItems = groups.Sum(x => x.Items.Count);
            int hiddenCount = hiddenItems.Count;

            market_summary_text.Text =
                $"Categories: {categoryCount}    Visible Items: {totalItems - hiddenCount}    Hidden Items: {hiddenCount}";

            market_categories_container.ResumeLayout();
        }

        private List<TraderPreviewCategoryGroup> BuildPreviewGroups()
        {
            var result = new List<TraderPreviewCategoryGroup>();

            var categoryConfig = AppServices
                .GetRequired<ExpansionManager>()
                .ExpansionMarketCategoryConfig;

            if (_trader.m_Categories != null)
            {
                foreach (var traderCategory in _trader.m_Categories)
                {
                    if (traderCategory?.MarketCategory == null)
                        continue;

                    string path = traderCategory.CategoryPath ?? traderCategory.MarketCategory.GetTraderPath();
                    string normalizedPath = NormalizeCategoryPath(path);

                    string name = !string.IsNullOrWhiteSpace(traderCategory.MarketCategory.DisplayName)
                        ? traderCategory.MarketCategory.DisplayName
                        : traderCategory.MarketCategory.FileName;

                    var group = new TraderPreviewCategoryGroup
                    {
                        Name = name,
                        Path = normalizedPath,
                        ColorHex = traderCategory.MarketCategory.Color,
                        IconName = traderCategory.MarketCategory.Icon
                    };

                    if (traderCategory.MarketCategory.Items != null)
                    {
                        foreach (var item in traderCategory.MarketCategory.Items)
                        {
                            if (item == null || string.IsNullOrWhiteSpace(item.ClassName))
                                continue;

                            group.Items.Add(new TraderPreviewEntry
                            {
                                MarketItem = item,
                                BuySell = traderCategory.BuySell,
                                IsDirectOverride = false
                            });
                        }
                    }

                    result.Add(group);
                }
            }

            if (_trader.m_Items != null)
            {
                foreach (var traderItem in _trader.m_Items)
                {
                    if (traderItem?.MarketItem == null || string.IsNullOrWhiteSpace(traderItem.MarketItem.ClassName))
                        continue;

                    string className = traderItem.MarketItem.ClassName;

                    ExpansionMarketCategory owningCategory =
                        categoryConfig.FindCategoryContainingClassName(className);

                    TraderPreviewCategoryGroup targetGroup = null;

                    if (owningCategory != null)
                    {
                        string ownerPath = NormalizeCategoryPath(owningCategory.GetTraderPath());

                        targetGroup = result.FirstOrDefault(x =>
                            string.Equals(x.Path, ownerPath, StringComparison.OrdinalIgnoreCase));

                        if (targetGroup == null)
                        {
                            targetGroup = new TraderPreviewCategoryGroup
                            {
                                Name = !string.IsNullOrWhiteSpace(owningCategory.DisplayName)
                                    ? owningCategory.DisplayName
                                    : owningCategory.FileName,
                                Path = ownerPath,
                                ColorHex = owningCategory.Color,
                                IconName = owningCategory.Icon
                            };

                            result.Add(targetGroup);
                        }
                    }
                    else
                    {
                        targetGroup = result.FirstOrDefault(x =>
                            string.Equals(x.Name, "Direct Items", StringComparison.OrdinalIgnoreCase));

                        if (targetGroup == null)
                        {
                            targetGroup = new TraderPreviewCategoryGroup
                            {
                                Name = "Direct Items",
                                Path = string.Empty
                            };

                            result.Add(targetGroup);
                        }
                    }

                    var existing = targetGroup.Items.FirstOrDefault(x =>
                        x.MarketItem != null &&
                        string.Equals(x.MarketItem.ClassName, className, StringComparison.OrdinalIgnoreCase));

                    if (existing != null)
                    {
                        existing.BuySell = traderItem.BuySell;
                        existing.IsDirectOverride = true;
                    }
                    else
                    {
                        targetGroup.Items.Add(new TraderPreviewEntry
                        {
                            MarketItem = traderItem.MarketItem,
                            BuySell = traderItem.BuySell,
                            IsDirectOverride = true
                        });
                    }
                }
            }

            return result
                .OrderBy(x => x.Name, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        private static string NormalizeCategoryPath(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            value = value.Trim().Replace('/', '\\').Trim('\\');

            if (value.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                value = value[..^5];

            return value;
        }

        private Control CreateCategorySection(TraderPreviewCategoryGroup group)
        {
            string categoryName = string.IsNullOrWhiteSpace(group.Name)
                ? "Unknown Category"
                : group.Name;

            bool isHiddenPartsGroup = string.Equals(categoryName, "Hidden Parts", StringComparison.OrdinalIgnoreCase);

            Color categoryTextColor = isHiddenPartsGroup
                ? UiAttachmentInfo
                : ParseUiColor(group.ColorHex, UiCategoryText);

            Image categoryIcon = GetIcon(group.IconName, categoryTextColor, 32);

            var items = group.Items
                .OrderBy(x => x.MarketItem?.ClassName, StringComparer.OrdinalIgnoreCase)
                .ToList();

            AlphaFlowLayoutPanel category_root = new AlphaFlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Margin = new Padding(0, 0, 0, 10),
                Padding = new Padding(0),
                Width = 960,
                FillColor = Color.Transparent,
                BorderColor = Color.Transparent,
                BorderThickness = 0
            };

            AlphaPanel category_header_corners = new AlphaPanel
            {
                Width = 960,
                Height = 49,
                Margin = new Padding(0),
                FillColor = Color.Transparent,
                BorderColor = FlattenColor(UiText, Color.Black),
                BorderThickness = 1,
                Padding = new Padding(0),
                Cursor = Cursors.Hand
            };

            AlphaPanel category_header_background = new AlphaPanel
            {
                Location = new Point(2, 2),
                Size = new Size(956, 45),
                FillColor = UiCategoryBgFill,
                BorderColor = Color.Transparent,
                BorderThickness = 0,
                Cursor = Cursors.Hand
            };

            Label category_header_icon = new Label
            {
                Text = "►",
                Location = new Point(6, 2),
                Size = new Size(42, 42),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI Symbol", 16, FontStyle.Bold),
                ForeColor = FlattenColor(UiText, Color.Black),
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };

            PictureBox category_icon = new PictureBox
            {
                Location = new Point(56, 4),
                Size = new Size(38, 38),
                SizeMode = PictureBoxSizeMode.CenterImage,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Image = categoryIcon
            };

            Label category_header_text = new Label
            {
                Text = categoryName,
                Location = new Point(104, 11),
                Size = new Size(610, 24),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = FlattenColor(categoryTextColor, Color.Black),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleLeft,
                Cursor = Cursors.Hand
            };

            Label category_info = new Label
            {
                Text = items.Count.ToString(),
                Location = new Point(820, 11),
                Size = new Size(110, 24),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = FlattenColor(UiText, Color.Black),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleRight,
                Cursor = Cursors.Hand
            };

            AlphaFlowLayoutPanel category_items_container = new AlphaFlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                WrapContents = true,
                FlowDirection = FlowDirection.LeftToRight,
                Margin = new Padding(0),
                Padding = new Padding(7),
                Width = 960,
                FillColor = Color.Transparent,
                BorderColor = Color.Transparent,
                BorderThickness = 0,
                Visible = false
            };

            foreach (var entry in items)
            {
                PreviewItemCard market_item = new PreviewItemCard(
                    entry.MarketItem,
                    entry.BuySell,
                    categoryName,
                    entry.IsDirectOverride,
                    _Colors,
                    isHiddenPartsGroup);

                market_item.ItemSelected += Card_ItemSelected;
                category_items_container.Controls.Add(market_item);
                _allCards.Add(market_item);
            }

            void UpdateCategoryArrow()
            {
                category_header_icon.Text = category_items_container.Visible ? "▼" : "►";
                category_header_icon.ForeColor = category_items_container.Visible
                    ? FlattenColor(UiCategoryText, Color.Black)
                    : FlattenColor(UiText, Color.Black);
            }

            void ToggleSection(object sender, EventArgs e)
            {
                category_items_container.Visible = !category_items_container.Visible;
                UpdateCategoryArrow();
                category_root.PerformLayout();
                market_categories_scroller.PerformLayout();
            }

            UpdateCategoryArrow();

            category_header_corners.Click += ToggleSection;
            category_header_background.Click += ToggleSection;
            category_header_icon.Click += ToggleSection;
            category_icon.Click += ToggleSection;
            category_header_text.Click += ToggleSection;
            category_info.Click += ToggleSection;

            category_header_background.Controls.Add(category_info);
            category_header_background.Controls.Add(category_header_text);
            category_header_background.Controls.Add(category_icon);
            category_header_background.Controls.Add(category_header_icon);
            category_header_corners.Controls.Add(category_header_background);

            category_root.Controls.Add(category_header_corners);
            category_root.Controls.Add(category_items_container);

            return category_root;
        }

        private void Card_ItemSelected(object sender, PreviewItemSelectedEventArgs e)
        {
            if (e?.MarketItem == null)
                return;

            market_item_header_text.Text = e.MarketItem.ClassName ?? "market_item_header_text";
            SetDetailsTextStyled(BuildDetailsText(
                e.MarketItem,
                e.BuySell,
                e.SourceLabel,
                e.IsDirectOverride));

            foreach (var card in _allCards)
                card.SetSelected(ReferenceEquals(card, sender));
        }
        private void SetDetailsTextStyled(string text)
        {
            market_item_description.Clear();
            market_item_description.Text = text;

            string[] headings =
            {
        "ClassName",
        "Source",
        "Direct Override",
        "Buy / Sell",
        "Min Price",
        "Max Price",
        "Sell %",
        "Min Stock",
        "Max Stock",
        "Quantity %",
        "Attachments",
        "Variants"
    };

            foreach (string heading in headings)
            {
                int start = 0;
                while ((start = market_item_description.Text.IndexOf(heading, start, StringComparison.Ordinal)) >= 0)
                {
                    market_item_description.Select(start, heading.Length);
                    market_item_description.SelectionFont = new Font("Segoe UI", 9f, FontStyle.Bold);
                    market_item_description.SelectionColor = FlattenColor(UiText, Color.Black);
                    start += heading.Length;
                }
            }

            market_item_description.Select(0, 0);
        }
        private string BuildDetailsText(
            ExpansionMarketItem item,
            ExpansionMarketTraderBuySell buySell,
            string sourceLabel,
            bool isDirectOverride)
        {
            string attachments = item.SpawnAttachments != null && item.SpawnAttachments.Count > 0
                ? string.Join(Environment.NewLine, item.SpawnAttachments)
                : "(none)";

            string variants = item.Variants != null && item.Variants.Count > 0
                ? string.Join(Environment.NewLine, item.Variants)
                : "(none)";

            return
            $@"ClassName
{item.ClassName}

Source
{sourceLabel}

Direct Override
{isDirectOverride}

Buy / Sell
{buySell}

Min Price
{item.MinPriceThreshold}

Max Price
{item.MaxPriceThreshold}

Sell %
{item.SellPricePercent}

Min Stock
{item.MinStockThreshold}

Max Stock
{item.MaxStockThreshold}

Quantity %
{item.QuantityPercent}

Attachments
{attachments}

Variants
{variants}";
        }

        private void ApplySearchFilter()
        {
            string search = market_filter_box.Text?.Trim() ?? string.Empty;

            foreach (var card in _allCards)
            {
                bool visible = string.IsNullOrWhiteSpace(search) ||
                               (!string.IsNullOrWhiteSpace(card.ItemClassName) &&
                                card.ItemClassName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);

                card.Visible = visible;
            }
        }

        internal static Color ParseUiColor(string hex, Color fallback)
        {
            if (string.IsNullOrWhiteSpace(hex))
                return fallback;

            hex = hex.Trim().TrimStart('#');

            try
            {
                // Expected format: RRGGBBAA
                if (hex.Length == 8)
                {
                    byte a = Convert.ToByte(hex.Substring(6, 2), 16);
                    byte r = Convert.ToByte(hex.Substring(0, 2), 16);
                    byte g = Convert.ToByte(hex.Substring(2, 2), 16);
                    byte b = Convert.ToByte(hex.Substring(4, 2), 16);
                    return Color.FromArgb(a, r, g, b);
                }
            }
            catch
            {
            }

            return fallback;
        }

        internal static Color FlattenColor(Color color, Color background)
        {
            if (color.A >= 255)
                return color;

            float alpha = color.A / 255f;
            int r = (int)Math.Round((color.R * alpha) + (background.R * (1f - alpha)));
            int g = (int)Math.Round((color.G * alpha) + (background.G * (1f - alpha)));
            int b = (int)Math.Round((color.B * alpha) + (background.B * (1f - alpha)));

            return Color.FromArgb(255, r, g, b);
        }

        private Image GetIcon(string iconName, Color tintColor, int size = 16)
        {
            if (string.IsNullOrWhiteSpace(iconName))
                return null;

            try
            {
                string cleanName = iconName.Replace("/", "").Replace("\\", "").Trim();
                string resourceName = $"ExpansionPlugin.Icons.{cleanName}.png";

                using var stream = ResourceHelper.OpenEmbeddedStream(resourceName);
                if (stream == null)
                    return null;

                using var original = Image.FromStream(stream);
                using var resized = new Bitmap(size, size);

                using (Graphics g = Graphics.FromImage(resized))
                {
                    g.Clear(Color.Transparent);
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.DrawImage(original, new Rectangle(0, 0, size, size));
                }
                return TintImage(resized, tintColor);
            }
            catch
            {
                return null;
            }
        }

        private static Bitmap TintImage(Image source, Color tintColor)
        {
            Bitmap tinted = new Bitmap(source.Width, source.Height);

            using (Graphics g = Graphics.FromImage(tinted))
            using (ImageAttributes attributes = new ImageAttributes())
            {
                float r = tintColor.R / 255f;
                float gCol = tintColor.G / 255f;
                float b = tintColor.B / 255f;
                float a = tintColor.A / 255f;

                ColorMatrix matrix = new ColorMatrix(new float[][]
                {
                    new float[] { r, 0, 0, 0, 0 },
                    new float[] { 0, gCol, 0, 0, 0 },
                    new float[] { 0, 0, b, 0, 0 },
                    new float[] { 0, 0, 0, a, 0 },
                    new float[] { 0, 0, 0, 0, 1 }
                });

                attributes.SetColorMatrix(matrix);
                g.DrawImage(
                    source,
                    new Rectangle(0, 0, source.Width, source.Height),
                    0,
                    0,
                    source.Width,
                    source.Height,
                    GraphicsUnit.Pixel,
                    attributes);
            }

            return tinted;
        }

        private static bool IsHiddenPart(TraderPreviewEntry entry)
        {
            return entry != null &&
                   entry.BuySell == ExpansionMarketTraderBuySell.CanBuyAndSellAsAttachmentOnly;
        }

        private Image LoadPreviewBackground()
        {
            try
            {
                using var stream = ResourceHelper.OpenEmbeddedStream("ExpansionPlugin.Images.market_background.png");
                if (stream == null)
                    return null;

                return Image.FromStream(stream);
            }
            catch
            {
                return null;
            }
        }
    }

    public class TraderPreviewCategoryGroup
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string ColorHex { get; set; }
        public string IconName { get; set; }
        public List<TraderPreviewEntry> Items { get; set; } = new();
    }

    public class TraderPreviewEntry
    {
        public ExpansionMarketItem MarketItem { get; set; }
        public ExpansionMarketTraderBuySell BuySell { get; set; }
        public bool IsDirectOverride { get; set; }
    }

    public class PreviewItemSelectedEventArgs : EventArgs
    {
        public ExpansionMarketItem MarketItem { get; }
        public ExpansionMarketTraderBuySell BuySell { get; }
        public string SourceLabel { get; }
        public bool IsDirectOverride { get; }

        public PreviewItemSelectedEventArgs(
            ExpansionMarketItem marketItem,
            ExpansionMarketTraderBuySell buySell,
            string sourceLabel,
            bool isDirectOverride)
        {
            MarketItem = marketItem;
            BuySell = buySell;
            SourceLabel = sourceLabel;
            IsDirectOverride = isDirectOverride;
        }
    }

    public class PreviewItemCard : AlphaPanel
    {
        private readonly ExpansionMarketItem _marketItem;
        private readonly ExpansionMarketTraderBuySell _buySell;
        private readonly string _sourceLabel;
        private readonly bool _isDirectOverride;
        private readonly MarketMenuColours _colors;
        private readonly bool _isHiddenPartGroup;

        private readonly AlphaPanel market_item_header_background;
        private readonly Label market_item_header_text;

        private readonly AlphaPanel market_item_preview_container;
        private readonly Label market_item_preview;

        private readonly AlphaPanel market_item_info_price_background;
        private readonly Label market_item_sell_price_text;
        private readonly Label market_item_buy_price_text;

        private readonly AlphaPanel market_item_info_stock_background;
        private readonly Label market_item_info_stock_player;
        private readonly Label market_item_info_stock;

        private readonly Label market_item_info_badges;

        private Color UiText => TraderPreviewForm.FlattenColor(
            TraderPreviewForm.ParseUiColor(_colors.BaseColorText, Color.White),
            Color.Black);

        private Color UiHeadersFill => TraderPreviewForm.ParseUiColor(
            _colors.BaseColorHeaders,
            Color.FromArgb(220, 28, 33, 38));

        private Color UiLabelsFill => TraderPreviewForm.ParseUiColor(
            _colors.BaseColorLabels,
            Color.FromArgb(220, 39, 39, 45));

        private Color UiInfoFill => TraderPreviewForm.ParseUiColor(
            _colors.BaseColorInfoSectionBackground,
            Color.FromArgb(180, 24, 28, 33));

        private Color UiItemHoverFill => TraderPreviewForm.ParseUiColor(
            _colors.ColorItemButton,
            Color.FromArgb(180, 60, 80, 110));

        private Color UiSetFill => TraderPreviewForm.ParseUiColor(
            _colors.ColorSetQuantityButton,
            Color.FromArgb(199, 38, 81, 20));

        public event EventHandler<PreviewItemSelectedEventArgs> ItemSelected;

        public string ItemClassName => _marketItem?.ClassName;

        public PreviewItemCard(
            ExpansionMarketItem marketItem,
            ExpansionMarketTraderBuySell buySell,
            string sourceLabel,
            bool isDirectOverride,
            MarketMenuColours colors,
            bool isHiddenPartGroup)
        {
            _marketItem = marketItem;
            _buySell = buySell;
            _sourceLabel = sourceLabel;
            _isDirectOverride = isDirectOverride;
            _colors = colors ?? throw new ArgumentNullException(nameof(colors));
            _isHiddenPartGroup = isHiddenPartGroup;

            Width = 184;
            Height = 220;
            Margin = new Padding(7);
            Padding = new Padding(0);
            FillColor = GetDefaultFillColor();
            BorderColor = Color.FromArgb(110, 255, 255, 255);
            BorderThickness = 1;
            Cursor = Cursors.Hand;

            market_item_header_background = new AlphaPanel
            {
                Dock = DockStyle.Top,
                Height = 28,
                FillColor = UiHeadersFill,
                BorderColor = Color.Transparent,
                BorderThickness = 0
            };

            market_item_header_text = new Label
            {
                Dock = DockStyle.Fill,
                Text = _marketItem?.ClassName ?? "(null)",
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                ForeColor = UiText,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoEllipsis = true
            };
            market_item_header_background.Controls.Add(market_item_header_text);

            market_item_info_stock_background = new AlphaPanel
            {
                Dock = DockStyle.Bottom,
                Height = 28,
                FillColor = UiHeadersFill,
                BorderColor = Color.Transparent,
                BorderThickness = 0,
                Padding = new Padding(6, 0, 6, 0)
            };

            var stockLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.Transparent
            };
            stockLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            stockLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            market_item_info_stock_player = new Label
            {
                Dock = DockStyle.Fill,
                Text = $"On You: {GetPlayerStockPreview()}",
                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                ForeColor = UiText,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleLeft
            };

            market_item_info_stock = new Label
            {
                Dock = DockStyle.Fill,
                Text = $"Stock: {GetTraderStockPreview()}",
                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                ForeColor = UiText,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleRight
            };

            stockLayout.Controls.Add(market_item_info_stock_player, 0, 0);
            stockLayout.Controls.Add(market_item_info_stock, 1, 0);
            market_item_info_stock_background.Controls.Add(stockLayout);

            market_item_info_price_background = new AlphaPanel
            {
                Dock = DockStyle.Bottom,
                Height = 30,
                FillColor = UiLabelsFill,
                BorderColor = Color.Transparent,
                BorderThickness = 0,
                Padding = new Padding(6, 0, 6, 0)
            };

            var priceLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.Transparent
            };
            priceLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            priceLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            market_item_sell_price_text = new Label
            {
                Dock = DockStyle.Fill,
                Text = $"Sell: {GetSellPricePreview()}",
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                ForeColor = UiText,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleLeft
            };

            market_item_buy_price_text = new Label
            {
                Dock = DockStyle.Fill,
                Text = $"Buy: {GetBuyPricePreview()}",
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                ForeColor = UiText,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleRight
            };

            priceLayout.Controls.Add(market_item_sell_price_text, 0, 0);
            priceLayout.Controls.Add(market_item_buy_price_text, 1, 0);
            market_item_info_price_background.Controls.Add(priceLayout);

            market_item_preview_container = new AlphaPanel
            {
                Dock = DockStyle.Fill,
                FillColor = UiInfoFill,
                BorderColor = Color.Transparent,
                BorderThickness = 0,
                Padding = new Padding(8)
            };

            market_item_preview = new Label
            {
                Dock = DockStyle.Fill,
                Text = _marketItem?.ClassName ?? "(null)",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.FromArgb(160, UiText),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter
            };

            market_item_info_badges = new Label
            {
                Dock = DockStyle.Bottom,
                Height = 22,
                Text = BuildBadgeText(),
                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                ForeColor = UiText,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter
            };

            market_item_preview_container.Controls.Add(market_item_preview);
            market_item_preview_container.Controls.Add(market_item_info_badges);

            Controls.Add(market_item_preview_container);
            Controls.Add(market_item_info_stock_background);
            Controls.Add(market_item_info_price_background);
            Controls.Add(market_item_header_background);

            HookClicks(this);
        }

        private string BuildBadgeText()
        {
            List<string> tags = new();

            if (_buySell == ExpansionMarketTraderBuySell.CanOnlyBuy)
                tags.Add("Buy Only");
            else if (_buySell == ExpansionMarketTraderBuySell.CanOnlySell)
                tags.Add("Sell Only");
            else if (_buySell == ExpansionMarketTraderBuySell.CanBuyAndSell)
                tags.Add("Buy / Sell");
            else if (_buySell == ExpansionMarketTraderBuySell.CanBuyAndSellAsAttachmentOnly)
                tags.Add("Hidden Part");

            if (_isDirectOverride)
                tags.Add("Override");

            if (_isHiddenPartGroup && !tags.Contains("Hidden Part"))
                tags.Add("Hidden Part");

            return string.Join("   ", tags);
        }

        private string GetBuyPricePreview()
        {
            return _marketItem != null ? _marketItem.MaxPriceThreshold.ToString() : "-";
        }

        private string GetSellPricePreview()
        {
            if (_marketItem == null)
                return "-";

            if (_marketItem.SellPricePercent > 0 && _marketItem.MaxPriceThreshold > 0)
            {
                double sell = (double)(_marketItem.MaxPriceThreshold * (_marketItem.SellPricePercent / 100.0m));
                return Math.Round(sell).ToString();
            }

            return _marketItem.MinPriceThreshold.ToString();
        }

        private string GetTraderStockPreview()
        {
            return _marketItem != null ? _marketItem.MaxStockThreshold.ToString() : "-";
        }

        private string GetPlayerStockPreview()
        {
            return _marketItem != null ? _marketItem.MinStockThreshold.ToString() : "-";
        }

        private Color GetDefaultFillColor()
        {
            if (_isDirectOverride)
                return UiSetFill;

            return UiLabelsFill;
        }

        private void HookClicks(Control parent)
        {
            parent.Click += Card_Click;

            foreach (Control child in parent.Controls)
                HookClicks(child);
        }

        private void Card_Click(object sender, EventArgs e)
        {
            ItemSelected?.Invoke(this,
                new PreviewItemSelectedEventArgs(_marketItem, _buySell, _sourceLabel, _isDirectOverride));
        }

        public void SetSelected(bool selected)
        {
            FillColor = selected ? UiItemHoverFill : GetDefaultFillColor();
            Invalidate();
        }
    }

    public class AlphaPanel : Panel
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color FillColor { get; set; } = Color.Transparent;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color BorderColor { get; set; } = Color.Transparent;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int BorderThickness { get; set; } = 0;

        public AlphaPanel()
        {
            SetStyle(ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            BackColor = Color.Transparent;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            PaintTransparentBackground(this, e.Graphics);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (FillColor.A > 0)
            {
                using SolidBrush brush = new SolidBrush(FillColor);
                e.Graphics.FillRectangle(brush, ClientRectangle);
            }

            if (BorderThickness > 0 && BorderColor.A > 0)
            {
                using Pen pen = new Pen(BorderColor, BorderThickness);
                Rectangle rect = ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                e.Graphics.DrawRectangle(pen, rect);
            }

            base.OnPaint(e);
        }

        internal static void PaintTransparentBackground(Control control, Graphics graphics)
        {
            if (control.Parent == null)
            {
                using SolidBrush brush = new SolidBrush(SystemColors.Control);
                graphics.FillRectangle(brush, control.ClientRectangle);
                return;
            }

            GraphicsState state = graphics.Save();
            graphics.TranslateTransform(-control.Left, -control.Top);

            PaintAncestorChain(control.Parent, graphics);

            graphics.Restore(state);
        }

        private static void PaintAncestorChain(Control control, Graphics graphics)
        {
            if (control.Parent != null)
            {
                GraphicsState state = graphics.Save();
                graphics.TranslateTransform(-control.Left, -control.Top);
                PaintAncestorChain(control.Parent, graphics);
                graphics.Restore(state);
            }
            else
            {
                using SolidBrush brush = new SolidBrush(control.BackColor);
                graphics.FillRectangle(brush, new Rectangle(Point.Empty, control.ClientSize));

                if (control.BackgroundImage != null)
                    DrawBackgroundImage(graphics, control, new Rectangle(Point.Empty, control.ClientSize));
            }

            if (control is AlphaPanel alphaPanel && alphaPanel.FillColor.A > 0)
            {
                using SolidBrush brush = new SolidBrush(alphaPanel.FillColor);
                graphics.FillRectangle(brush, new Rectangle(Point.Empty, control.ClientSize));
            }
            else if (control is AlphaFlowLayoutPanel alphaFlow && alphaFlow.FillColor.A > 0)
            {
                using SolidBrush brush = new SolidBrush(alphaFlow.FillColor);
                graphics.FillRectangle(brush, new Rectangle(Point.Empty, control.ClientSize));
            }
        }

        internal static void DrawBackgroundImage(Graphics graphics, Control control, Rectangle bounds)
        {
            Image image = control.BackgroundImage;
            if (image == null)
                return;

            switch (control.BackgroundImageLayout)
            {
                case ImageLayout.None:
                    graphics.DrawImage(image, 0, 0);
                    break;

                case ImageLayout.Tile:
                    using (TextureBrush tb = new TextureBrush(image))
                        graphics.FillRectangle(tb, bounds);
                    break;

                case ImageLayout.Center:
                    {
                        int x = (control.ClientSize.Width - image.Width) / 2;
                        int y = (control.ClientSize.Height - image.Height) / 2;
                        graphics.DrawImage(image, x, y);
                        break;
                    }

                case ImageLayout.Stretch:
                    graphics.DrawImage(image, bounds);
                    break;

                case ImageLayout.Zoom:
                    {
                        Size imgSize = image.Size;
                        float ratio = Math.Min(
                            (float)control.ClientSize.Width / imgSize.Width,
                            (float)control.ClientSize.Height / imgSize.Height);

                        int w = (int)(imgSize.Width * ratio);
                        int h = (int)(imgSize.Height * ratio);
                        int x = (control.ClientSize.Width - w) / 2;
                        int y = (control.ClientSize.Height - h) / 2;

                        graphics.DrawImage(image, new Rectangle(x, y, w, h));
                        break;
                    }
            }
        }
    }

    public class AlphaFlowLayoutPanel : FlowLayoutPanel
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color FillColor { get; set; } = Color.Transparent;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color BorderColor { get; set; } = Color.Transparent;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int BorderThickness { get; set; } = 0;

        public AlphaFlowLayoutPanel()
        {
            SetStyle(ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            BackColor = Color.Transparent;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            AlphaPanel.PaintTransparentBackground(this, e.Graphics);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (FillColor.A > 0)
            {
                using SolidBrush brush = new SolidBrush(FillColor);
                e.Graphics.FillRectangle(brush, ClientRectangle);
            }

            if (BorderThickness > 0 && BorderColor.A > 0)
            {
                using Pen pen = new Pen(BorderColor, BorderThickness);
                Rectangle rect = ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                e.Graphics.DrawRectangle(pen, rect);
            }

            base.OnPaint(e);
        }
    }
}