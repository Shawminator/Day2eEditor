using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design.Behavior;

namespace ExpansionPlugin
{
    public class AiPAtrolLegendDrawable : IMapDrawable
    {

        private readonly Size _controlSize;

        public AiPAtrolLegendDrawable(Size controlSize)
        {
            _controlSize = controlSize;
        }
        public void Draw(Graphics g, RectangleF drawBounds, float zoom, PointF panOffset)
        {
            int padding = 10;
            int itemHeight = 18;

            // Bottom-left of the visible screen/control
            float startX = padding;
            float startY = _controlSize.Height - (itemHeight * 10) - padding;

            using (var font = new Font("Tahoma", 8))
            {
                g.DrawString("Legend:", font, Brushes.White, startX, startY);
                startY += itemHeight;

                DrawLegendItem(g, startX, startY, Color.Red, "HALT", font); startY += itemHeight;
                DrawLegendItem(g, startX, startY, Color.Green, "ONCE →", font); startY += itemHeight;
                DrawLegendItem(g, startX, startY, Color.Blue, "LOOP ⟳", font); startY += itemHeight;
                DrawLegendItem(g, startX, startY, Color.Orange, "ALTERNATE ↔", font); startY += itemHeight;
                DrawLegendItem(g, startX, startY, Color.Yellow, "LOOP_OR_ALTERNATE ⟳ or ↔", font); startY += itemHeight;
                DrawLegendItem(g, startX, startY, Color.Blue, "HALT_OR_LOOP (dashed)", font); startY += itemHeight;
                DrawLegendItem(g, startX, startY, Color.Orange, "HALT_OR_ALTERNATE (dashed)", font); startY += itemHeight;
                DrawLegendItem(g, startX, startY, Color.Gray, "ROAMING 🧭", font); startY += itemHeight;
                DrawLegendItem(g, startX, startY, Color.LimeGreen, "ROAMING_LOCAL 🧭", font);
            }
        }
        private void DrawLegendItem(Graphics g, float x, float y, Color color, string label, Font font)
        {
            using (var brush = new SolidBrush(color))
            {
                g.FillRectangle(brush, x, y, 10, 10);
            }
            g.DrawRectangle(Pens.White, x, y, 10, 10);
            g.DrawString(label, font, Brushes.White, x + 15, y - 2);
        }
    }

}
