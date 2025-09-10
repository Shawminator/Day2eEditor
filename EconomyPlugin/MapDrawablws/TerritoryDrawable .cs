using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconomyPlugin
{
    public class TerritoryDrawable : IMapDrawable
    {
        public PointF MapPosition { get; set; }
        public Color Color { get; set; } = Color.Red;
        public float Radius { get; set; } = 5f;

        public bool Scaleradius { get; set; } = false;
        public bool IsSelected { get; set;  } = false;

        private readonly Size _mapSize;

        public TerritoryDrawable(PointF mapPosition, Size mapSize)
        {
            MapPosition = mapPosition;
            _mapSize = mapSize;
        }

        public void Draw(Graphics g, RectangleF drawBounds, float zoom, PointF panOffset)
        {
            // Convert map → screen
            float normalizedX = MapPosition.X / _mapSize.Width;
            float normalizedY = 1f - (MapPosition.Y / _mapSize.Height);

            float screenX = drawBounds.X + normalizedX * drawBounds.Width;
            float screenY = drawBounds.Y + normalizedY * drawBounds.Height;


            // Adjust radius based on zoom if Scaleradius is true
            //float scaledRadius = Scaleradius ? Radius * zoom : Radius;


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

            // Selected Square around circle
            if (IsSelected)
            {

                g.DrawLine(Pens.LimeGreen, screenX - 5, screenY, screenX + 5, screenY);
                g.DrawLine(Pens.LimeGreen, screenX, screenY - 5, screenX, screenY + 5);

                // Padding to keep the selection box outside the circle
                float padding = 4f;

                using (var pen = new Pen(Color.LimeGreen) { Width = 2, DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
                {
                    g.DrawRectangle(pen,
                        screenX - screenRadius - padding,
                        screenY - screenRadius - padding,
                        (screenRadius * 2) + (padding * 2),
                        (screenRadius * 2) + (padding * 2));

                }

                using (var brush = new SolidBrush(Color.FromArgb(25, Color.LimeGreen)))
                {
                    g.FillRectangle(brush,
                        screenX - screenRadius - padding,
                        screenY - screenRadius - padding,
                        (screenRadius * 2) + (padding * 2),
                        (screenRadius * 2) + (padding * 2));
                }

            }
        }
    }

}
