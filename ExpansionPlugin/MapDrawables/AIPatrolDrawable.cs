using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class AIPatrolDrawable : IMapDrawable
    {
        public PointF MapPosition { get; set; }
        public PointF MapPosition2 { get; set; }
        public Color Color { get; set; } = Color.Red;
        public float Radius { get; set; } = 5f;

        public bool Scaleradius { get; set; } = false;
        public bool WriteString { get; set; } = false;
        public string text { get; set; } = "";

        private readonly Size _mapSize;

        public AIPatrolDrawable(PointF mapPosition, PointF mapPosition2, Size mapSize)
        {
            MapPosition = mapPosition;
            MapPosition2 = mapPosition2;
            _mapSize = mapSize;
        }

        public void Draw(Graphics g, RectangleF drawBounds, float zoom, PointF panOffset)
        {
            // Enable anti-aliasing for smoother rendering
            g.SmoothingMode = SmoothingMode.AntiAlias;


            // Convert map → screen
            float normalizedX = MapPosition.X / _mapSize.Width;
            float normalizedY = 1f - (MapPosition.Y / _mapSize.Height);
            float normalizedX2 = MapPosition2.X / _mapSize.Width;
            float normalizedY2 = 1f - (MapPosition2.Y / _mapSize.Height);

            float screenX = drawBounds.X + normalizedX * drawBounds.Width;
            float screenY = drawBounds.Y + normalizedY * drawBounds.Height;
            float screenX2 = drawBounds.X + normalizedX2 * drawBounds.Width;
            float screenY2 = drawBounds.Y + normalizedY2 * drawBounds.Height;


            // Convert radius from map units to screen pixels
            float screenRadius;
            if (Scaleradius)
            {
                // Convert map radius to normalized units
                float normalizedRadiusX = Radius / _mapSize.Width;
                float normalizedRadiusY = Radius / _mapSize.Height;

                // Convert normalized units to screen space
                float screenRadiusX = normalizedRadiusX * drawBounds.Width;
                float screenRadiusY = normalizedRadiusY * drawBounds.Height;

                // Average the X and Y scaling to get a consistent radius
                screenRadius = (screenRadiusX + screenRadiusY) / 2f;
            }
            else
            {
                screenRadius = Radius;
            }

            // Outer circle
            using (var pen = new Pen(Color, 2))
            {
                g.DrawEllipse(pen,
                    screenX - screenRadius,
                    screenY - screenRadius,
                    screenRadius * 2,
                    screenRadius * 2);
            }
            // Inner dot
            //float dotRadius = 5f * 0.3f; // adjust as needed
            float dotRadius = 2f; // adjust as needed
            using (var brush = new SolidBrush(Color))
            {
                g.FillEllipse(brush,
                    screenX - dotRadius,
                    screenY - dotRadius,
                    dotRadius * 2,
                    dotRadius * 2);
            }
            

            //Draw Line and directions indicator
            PointF center = new PointF(screenX , screenY );
            PointF center2 = new PointF(screenX2 , screenY2 );
            using (var pen2 = new Pen(Color.Green, 2))
            {
                g.DrawLine(pen2, center, center2);

                // Calculate direction vector
                float dx = center2.X - center.X;
                float dy = center2.Y - center.Y;
                float length = (float)Math.Sqrt(dx * dx + dy * dy);

                if (length > 0)
                {
                    // Normalize direction
                    dx /= length;
                    dy /= length;

                    // Arrow size
                    float arrowSize = 10f;

                    // Perpendicular vector
                    float px = -dy;
                    float py = dx;

                    // Midpoint of the line
                    PointF mid = new PointF(
                        center.X + dx * length / 2f,
                        center.Y + dy * length / 2f);

                    // Arrow points
                    PointF arrowPoint1 = new PointF(
                        mid.X - arrowSize * dx + arrowSize * 0.5f * px,
                        mid.Y - arrowSize * dy + arrowSize * 0.5f * py);

                    PointF arrowPoint2 = new PointF(
                        mid.X - arrowSize * dx - arrowSize * 0.5f * px,
                        mid.Y - arrowSize * dy - arrowSize * 0.5f * py);

                    // Draw arrowhead at midpoint
                    g.DrawLine(pen2, mid, arrowPoint1);
                    g.DrawLine(pen2, mid, arrowPoint2);
                }
            }



            if (WriteString)
            {
                Rectangle rect3 = new Rectangle((int)center.X - (int)dotRadius, (int)center.Y - ((int)dotRadius + 30), 200, 40);
                using (var font = new Font("Tahoma", 9))
                {
                    g.DrawString(text, font, Brushes.White, rect3);
                }

            }
        }
    }
}
