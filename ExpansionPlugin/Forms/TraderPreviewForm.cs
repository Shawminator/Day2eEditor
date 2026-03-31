using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

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

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 2,
                BackColor = Color.Transparent
            };

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50));   // icon
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));   // title/summary
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 340));  // search area

            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            PictureBox traderIconBox = new PictureBox
            {
                Size = new Size(40, 40),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent,
                Image = GetIcon(_trader.TraderIcon, Color.White, 32),
                Dock = DockStyle.Fill
            };

            _titleLabel = new Label
            {
                Text = _trader.DisplayName ?? _trader.FileName,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.BottomLeft
            };

            _summaryLabel = new Label
            {
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.Gainsboro,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.TopLeft
            };

            var searchPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = false,
                Padding = new Padding(0, 8, 0, 0)
            };

            Label searchLabel = new Label
            {
                Text = "Search",
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.Gainsboro,
                Margin = new Padding(0, 6, 8, 0)
            };

            _searchBox = new TextBox
            {
                Width = 250
            };
            _searchBox.TextChanged += (_, __) => ApplySearchFilter();

            searchPanel.Controls.Add(searchLabel);
            searchPanel.Controls.Add(_searchBox);

            layout.Controls.Add(traderIconBox, 0, 0);
            layout.SetRowSpan(traderIconBox, 2);

            layout.Controls.Add(_titleLabel, 1, 0);
            layout.Controls.Add(_summaryLabel, 1, 1);

            layout.Controls.Add(searchPanel, 2, 0);
            layout.SetRowSpan(searchPanel, 2);

            _topPanel.Controls.Add(layout);
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

            int hiddenCount = groups.Sum(x =>
                x.Items.Count(i =>
                i.BuySell == ExpansionMarketTraderBuySell.CanBuyAndSellAsAttachmentOnly));

            _summaryLabel.Text =
                $"Categories: {categoryCount}    " +
                $"Visible Items: {totalItems}    " +
                $"Hidden Items: {hiddenCount}    ";

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

            Color categoryColor = ParseCategoryColor(group.ColorHex);
            Image categoryIcon = GetIcon(group.IconName, categoryColor, 16);

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

            header.Paint += (s, e) =>
            {
                using Pen pen = new Pen(categoryColor, 2);
                Rectangle rect = header.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                e.Graphics.DrawRectangle(pen, rect);
            };

            PictureBox iconBox = new PictureBox
            {
                Dock = DockStyle.Left,
                Width = 20,
                SizeMode = PictureBoxSizeMode.CenterImage,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Image = categoryIcon
            };

            Label arrowLabel = new Label
            {
                Text = "►",
                Dock = DockStyle.Left,
                Width = 24,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = categoryColor,
                Cursor = Cursors.Hand
            };

            Label nameLabel = new Label
            {
                Text = categoryName,
                Dock = DockStyle.Left,
                Width = 540,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = categoryColor,
                TextAlign = ContentAlignment.MiddleLeft,
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
            iconBox.Click += ToggleSection;
            nameLabel.Click += ToggleSection;
            countLabel.Click += ToggleSection;

            header.Controls.Add(countLabel);
            header.Controls.Add(nameLabel);
            header.Controls.Add(iconBox);
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

        private static Color ParseCategoryColor(string hex)
        {
            if (string.IsNullOrWhiteSpace(hex))
                return Color.White;

            hex = hex.Trim().TrimStart('#');

            try
            {
                // Expected format: AARRGGBB
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

            return Color.White;
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
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
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