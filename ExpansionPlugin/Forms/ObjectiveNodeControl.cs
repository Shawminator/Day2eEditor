using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    public partial class ObjectiveNodeControl : UserControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ExpansionQuestObjectiveType Objectivetype { get; set; }
        
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color pencolour { get; set;  }
        
        public Point Center => new Point(Left + Width / 2, Top + Height / 2);
        public ObjectiveNodeControl(string text, ExpansionQuestObjectiveType objectivetype)
        {
            InitializeComponent();
            label.Text = text;
            Objectivetype = objectivetype;
            GetObjectivecolour();
        }

        private void GetObjectivecolour()
        {
            switch (Objectivetype)
            {
                case ExpansionQuestObjectiveType.NONE:
                    pencolour = Color.Gray;
                    break;

                case ExpansionQuestObjectiveType.TARGET:
                    pencolour = Color.Red;
                    break;

                case ExpansionQuestObjectiveType.TRAVEL:
                    pencolour = Color.Blue;
                    break;

                case ExpansionQuestObjectiveType.COLLECT:
                    pencolour = Color.Green;
                    break;

                case ExpansionQuestObjectiveType.DELIVERY:
                    pencolour = Color.Orange;
                    break;

                case ExpansionQuestObjectiveType.TREASUREHUNT:
                    pencolour = Color.Gold;
                    break;

                case ExpansionQuestObjectiveType.AIPATROL:
                    pencolour = Color.Purple;
                    break;

                case ExpansionQuestObjectiveType.AICAMP:
                    pencolour = Color.Brown;
                    break;

                case ExpansionQuestObjectiveType.AIESCORT:
                    pencolour = Color.Cyan;
                    break;

                case ExpansionQuestObjectiveType.ACTION:
                    pencolour = Color.Magenta;
                    break;

                case ExpansionQuestObjectiveType.CRAFTING:
                    pencolour = Color.Yellow;
                    break;

                default:
                    pencolour = Color.Black;
                    break;
            }
        }

        private void NodeContro_Paint(object sender, PaintEventArgs e)
        {
            using var pen = new Pen(pencolour, 2);
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
