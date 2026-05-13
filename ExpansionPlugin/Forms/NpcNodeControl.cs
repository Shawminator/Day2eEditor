using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ExpansionPlugin
{
    public partial class NpcNodeControl : UserControl
    {
        public Point Center => new(Left + Width / 2, Top + Height / 2);
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PenColor { get; set; }
        public NpcNodeControl(string text, string type)
        {
            InitializeComponent();
            label.Text = text;
            if (type == "Giver")
                PenColor = Color.Green;
            else if (type == "Turn-In")
                PenColor = Color.Red;
        }

        private void NpcNodeControl_Paint(object sender, PaintEventArgs e)
        {
            using var pen = new Pen(PenColor, 2);
            e.Graphics.SmoothingMode =
                System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            e.Graphics.DrawRectangle(
                pen,
                0,
                0,
                Width - 1,
                Height - 1);
        }
    }
}
