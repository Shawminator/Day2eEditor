using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpansionPlugin.Forms
{
    public partial class TraderPreviewForm : Form
    {
        private readonly ExpansionMarketTrader _trader;
        private readonly MarketMenuColours _Colors;
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

        private Color UiBuyText => FlattenColor(ParseUiColor(_Colors.ColorBuyButton, Color.FromArgb(160, 204, 113, 40)), Color.Black);
        private Color UiSellText => FlattenColor(ParseUiColor(_Colors.ColorSellButton, Color.FromArgb(221, 38, 38, 40)), Color.Black);

        private Color UiMarketIcon => ParseUiColor(_Colors.ColorMarketIcon, UiText);
        private Color UiWarning => ParseUiColor(_Colors.ColorRequirementsNotMet, Color.OrangeRed);
        private Color UiAttachmentInfo => ParseUiColor(_Colors.ColorItemInfoAttachments, Color.Gold);
        public TraderPreviewForm()
        {
            InitializeComponent();
        }
        public TraderPreviewForm(ExpansionMarketTrader trader, MarketMenuColours colors)
        {
            _trader = trader ?? throw new ArgumentNullException(nameof(trader));
            _Colors = colors ?? throw new ArgumentNullException(nameof(colors));

            InitializeComponent();
            Text = $"Trader Preview - {_trader.DisplayName}";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(1400, 850);
            MinimumSize = new Size(1000, 650);
            DoubleBuffered = true;
            BackColor = Color.Black;
            ForeColor = UiText;
            BackgroundImage = LoadPreviewBackground();
            BackgroundImageLayout = ImageLayout.Stretch;
            DoubleBuffered = true;

            _root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1,
                BackColor = Color.Transparent
            };
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            _root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            Controls.Add(_root);

            //BuildPreview();
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
}
