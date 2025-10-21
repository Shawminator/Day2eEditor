using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoreUI.Forms
{
    public partial class AdvancedColorPickerForm : Form
    {
        private FormController controller;
        private Color baseColor = Color.Red;
        private int alpha = 255;
        public string SelectedColorHex { get; private set; }
        public Color SelectedColor { get; private set; }

        public AdvancedColorPickerForm(Color initialColor)
        {
            baseColor = Color.FromArgb(initialColor.A, initialColor.R, initialColor.G, initialColor.B);
            alpha = initialColor.A;
            InitializeComponent();
            controller = new FormController(
                this,
                TitlePanel,
                null,
                TitleLabel,
                label1,
                CloseButton,
                null
            );
            UpdatePreview();

            trackBarAlpha.Value = alpha;
            lblAlphaValue.Text = $"Alpha: {alpha}";
            
            this.Disposed += (s, e) => controller.Dispose();
        }
        private void btnPickColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    baseColor = colorDialog.Color;
                    UpdatePreview();
                }
            }
        }

        private void trackBarAlpha_Scroll(object sender, EventArgs e)
        {
            alpha = trackBarAlpha.Value;
            lblAlphaValue.Text = $"Alpha: {alpha}";
            UpdatePreview();
        }

        private void UpdatePreview()
        {
            SelectedColor = Color.FromArgb(alpha, baseColor.R, baseColor.G, baseColor.B);
            panelPreview.BackColor = SelectedColor;
            SelectedColorHex = $"0x{SelectedColor.ToArgb():X8}";
            lblColorCode.Text = $"ARGB: {SelectedColorHex}";
            lblColorCodeints.Text = $"A:{alpha}, R:{baseColor.R}, G:{baseColor.G}, B:{baseColor.B}";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
