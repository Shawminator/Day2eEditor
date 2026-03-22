using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    public partial class TraderPreviewForm : Form
    {
        private readonly ExpansionMarketTrader _trader;

        private TableLayoutPanel _root;
        private Panel _topPanel;
        private TextBox _searchBox;
        private Label _titleLabel;
        private Label _summaryLabel;

        private SplitContainer _split;
        private Panel _leftScrollPanel;
        private Panel _detailsPanel;

        private Label _detailsTitle;
        private TextBox _detailsText;

        private readonly List<PreviewItemCard> _allCards = new();

        public TraderPreviewForm(ExpansionMarketTrader trader)
        {
            _trader = trader ?? throw new ArgumentNullException(nameof(trader));

            InitializeComponent();
            BuildPreview();
        }

        private void InitializeComponent()
        {
            Text = $"Trader Preview - {_trader.DisplayName}";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(1400, 850);
            MinimumSize = new Size(1000, 650);
            BackColor = Color.FromArgb(20, 24, 28);
            ForeColor = Color.White;

            _root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1,
                BackColor = BackColor
            };
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            _root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            Controls.Add(_root);

            BuildTopPanel();
            BuildMainArea();
        }

        private void BuildTopPanel()
        {
            _topPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                BackColor = Color.FromArgb(28, 33, 38)
            };

            _titleLabel = new Label
            {
                AutoSize = false,
                Text = _trader.DisplayName ?? _trader.FileName,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(10, 8),
                Size = new Size(500, 28)
            };

            _summaryLabel = new Label
            {
                AutoSize = false,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.Gainsboro,
                Location = new Point(10, 38),
                Size = new Size(900, 22)
            };

            Label searchLabel = new Label
            {
                Text = "Search",
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.Gainsboro,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(980, 12)
            };

            _searchBox = new TextBox
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(1040, 9),
                Width = 320
            };
            _searchBox.TextChanged += (_, __) => ApplySearchFilter();

            _topPanel.Controls.Add(_titleLabel);
            _topPanel.Controls.Add(_summaryLabel);
            _topPanel.Controls.Add(searchLabel);
            _topPanel.Controls.Add(_searchBox);

            _root.Controls.Add(_topPanel, 0, 0);
        }

        private void BuildMainArea()
        {
            _split = new SplitContainer
            {
                Dock = DockStyle.Fill,
                SplitterDistance = 1020,
                BackColor = BackColor
            };

            _leftScrollPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(8),
                BackColor = Color.FromArgb(18, 22, 26)
            };

            _detailsPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                BackColor = Color.FromArgb(24, 28, 33)
            };

            _detailsTitle = new Label
            {
                Dock = DockStyle.Top,
                Height = 36,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                Text = "Item Details"
            };

            _detailsText = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                BackColor = Color.FromArgb(16, 18, 22),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Consolas", 10),
            };

            _detailsPanel.Controls.Add(_detailsText);
            _detailsPanel.Controls.Add(_detailsTitle);

            _split.Panel1.Controls.Add(_leftScrollPanel);
            _split.Panel2.Controls.Add(_detailsPanel);

            _root.Controls.Add(_split, 0, 1);
        }

        private void BuildPreview()
        {
            _leftScrollPanel.SuspendLayout();
            _leftScrollPanel.Controls.Clear();
            _allCards.Clear();

            List<TraderPreviewCategoryGroup> groups = BuildPreviewGroups();

            FlowLayoutPanel verticalStack = new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Dock = DockStyle.Top,
                Margin = new Padding(0),
                Padding = new Padding(0),
                BackColor = Color.Transparent
            };

            foreach (var group in groups)
            {
                bool hasVisibleItems = group.Items.Any(x =>
                    x.BuySell != ExpansionMarketTraderBuySell.CanBuyAndSellAsAttachmentOnly);

                if (!hasVisibleItems)
                    continue;

                var categoryPanel = CreateCategorySection(group);
                verticalStack.Controls.Add(categoryPanel);
            }

            _leftScrollPanel.Controls.Add(verticalStack);

            int categoryCount = groups.Count;
            int totalItems = groups.Sum(x =>
    x.Items.Count(i => i.BuySell != ExpansionMarketTraderBuySell.CanBuyAndSellAsAttachmentOnly));

            int overrideCount = groups.Sum(x =>
                x.Items.Count(i =>
                    i.IsDirectOverride &&
                    i.BuySell != ExpansionMarketTraderBuySell.CanBuyAndSellAsAttachmentOnly));

            _summaryLabel.Text =
                $"Categories: {categoryCount}    " +
                $"Visible Items: {totalItems}    " +
                $"Direct Overrides: {overrideCount}";

            _leftScrollPanel.ResumeLayout();
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
                        Path = normalizedPath
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
                                Path = ownerPath
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

            var items = group.Items
             .Where(x => x.BuySell != ExpansionMarketTraderBuySell.CanBuyAndSellAsAttachmentOnly)
             .OrderBy(x => x.MarketItem?.ClassName, StringComparer.OrdinalIgnoreCase)
             .ToList();

            FlowLayoutPanel section = new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Margin = new Padding(0, 0, 0, 10),
                Padding = new Padding(0),
                Width = 950,
                BackColor = Color.FromArgb(18, 22, 26)
            };

            Panel header = new Panel
            {
                Width = 950,
                Height = 34,
                Margin = new Padding(0),
                BackColor = Color.FromArgb(40, 46, 54),
                Padding = new Padding(8, 6, 8, 6),
                Cursor = Cursors.Hand
            };

            Label arrowLabel = new Label
            {
                Text = "►",
                Dock = DockStyle.Left,
                Width = 24,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };

            Label nameLabel = new Label
            {
                Text = categoryName.ToUpperInvariant(),
                Dock = DockStyle.Left,
                Width = 560,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };

            Label countLabel = new Label
            {
                Text = $"{items.Count} ITEMS",
                Dock = DockStyle.Right,
                Width = 220,
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.Gainsboro,
                Cursor = Cursors.Hand
            };

            FlowLayoutPanel itemFlow = new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                WrapContents = true,
                FlowDirection = FlowDirection.LeftToRight,
                Margin = new Padding(0),
                Padding = new Padding(6),
                Width = 950,
                BackColor = Color.FromArgb(22, 26, 31),
                Visible = false
            };

            foreach (var entry in items)
            {
                PreviewItemCard card = new PreviewItemCard(
                    entry.MarketItem,
                    entry.BuySell,
                    categoryName,
                    entry.IsDirectOverride);

                card.ItemSelected += Card_ItemSelected;
                itemFlow.Controls.Add(card);
                _allCards.Add(card);
            }

            void ToggleSection(object sender, EventArgs e)
            {
                itemFlow.Visible = !itemFlow.Visible;
                arrowLabel.Text = itemFlow.Visible ? "▼" : "►";
                section.PerformLayout();
                _leftScrollPanel.PerformLayout();
            }

            header.Click += ToggleSection;
            arrowLabel.Click += ToggleSection;
            nameLabel.Click += ToggleSection;
            countLabel.Click += ToggleSection;

            header.Controls.Add(countLabel);
            header.Controls.Add(nameLabel);
            header.Controls.Add(arrowLabel);

            section.Controls.Add(header);
            section.Controls.Add(itemFlow);

            return section;
        }

        private void Card_ItemSelected(object sender, PreviewItemSelectedEventArgs e)
        {
            if (e?.MarketItem == null)
                return;

            _detailsTitle.Text = e.MarketItem.ClassName ?? "Item Details";
            _detailsText.Text = BuildDetailsText(
                e.MarketItem,
                e.BuySell,
                e.SourceLabel,
                e.IsDirectOverride);

            foreach (var card in _allCards)
                card.SetSelected(ReferenceEquals(card, sender));
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
$@"ClassName:           {item.ClassName}
Source:              {sourceLabel}
Direct Override:     {isDirectOverride}
Buy/Sell:            {buySell}

Min Price:           {item.MinPriceThreshold}
Max Price:           {item.MaxPriceThreshold}
Sell %:              {item.SellPricePercent}

Min Stock:           {item.MinStockThreshold}
Max Stock:           {item.MaxStockThreshold}
Quantity %:          {item.QuantityPercent}

Attachments:
{attachments}

Variants:
{variants}";
        }

        private void ApplySearchFilter()
        {
            string search = _searchBox.Text?.Trim() ?? string.Empty;

            foreach (var card in _allCards)
            {
                bool visible = string.IsNullOrWhiteSpace(search) ||
                               (!string.IsNullOrWhiteSpace(card.ItemClassName) &&
                                card.ItemClassName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);

                card.Visible = visible;
            }
        }
    }

    public class TraderPreviewCategoryGroup
    {
        public string Name { get; set; }
        public string Path { get; set; }
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

    public class PreviewItemCard : Panel
    {
        private readonly ExpansionMarketItem _marketItem;
        private readonly ExpansionMarketTraderBuySell _buySell;
        private readonly string _sourceLabel;
        private readonly bool _isDirectOverride;

        private readonly Label _nameLabel;

        public event EventHandler<PreviewItemSelectedEventArgs> ItemSelected;

        public string ItemClassName => _marketItem?.ClassName;

        public PreviewItemCard(
            ExpansionMarketItem marketItem,
            ExpansionMarketTraderBuySell buySell,
            string sourceLabel,
            bool isDirectOverride)
        {
            _marketItem = marketItem;
            _buySell = buySell;
            _sourceLabel = sourceLabel;
            _isDirectOverride = isDirectOverride;

            Width = 180;
            Height = 55;
            Margin = new Padding(6);
            Padding = new Padding(6, 8, 6, 6);
            BackColor = Color.FromArgb(12, 14, 18);
            BorderStyle = BorderStyle.FixedSingle;
            Cursor = Cursors.Hand;

            var nameLabel = new Label
            {
                Dock = DockStyle.Top,
                Height = 24,
                Text = _marketItem.ClassName,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.White
            };

            var tagLabel = new Label
            {
                Dock = DockStyle.Fill,
                Text = BuildTagText(),
                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                ForeColor = Color.Silver
            };

            Controls.Add(tagLabel);
            Controls.Add(nameLabel);

            HookClicks(this);
        }
        private string BuildTagText()
        {
            List<string> tags = new List<string>();

            if (_buySell == ExpansionMarketTraderBuySell.CanOnlyBuy)
                tags.Add("Buy Only");
            else if (_buySell == ExpansionMarketTraderBuySell.CanOnlySell)
                tags.Add("Sell Only");
            else if (_buySell == ExpansionMarketTraderBuySell.CanBuyAndSell)
                tags.Add("Buy and Sell");

            if (_isDirectOverride)
                tags.Add("Override");

            return tags.Count > 0 ? $"[{string.Join(", ", tags)}]" : "";
        }
        private string BuildDisplayName()
        {
            if (_marketItem == null || string.IsNullOrWhiteSpace(_marketItem.ClassName))
                return "(null)";

            List<string> tags = new List<string>();

            if (_buySell == ExpansionMarketTraderBuySell.CanOnlyBuy)
                tags.Add("Buy Only");
            else if (_buySell == ExpansionMarketTraderBuySell.CanOnlySell)
                tags.Add("Sell Only");

            if (tags.Count == 0)
                return _marketItem.ClassName;

            return $"{_marketItem.ClassName}\n[{string.Join(", ", tags)}]";
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
            BackColor = selected
                ? Color.FromArgb(45, 65, 95)
                : (_isDirectOverride ? Color.FromArgb(28, 22, 18) : Color.FromArgb(12, 14, 18));
        }
    }
}