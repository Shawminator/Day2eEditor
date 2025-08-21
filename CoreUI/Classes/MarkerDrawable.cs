using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2eEditor
{
    public class MarkerDrawable : IMapDrawable
    {
        public PointF MapPosition { get; set; }
        public Color Color { get; set; } = Color.Red;
        public float Radius { get; set; } = 5f;

        private readonly Size _mapSize;

        public MarkerDrawable(PointF mapPosition, Size mapSize)
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

            // Outer circle
            using (var pen = new Pen(Color, 2))
            {
                g.DrawEllipse(pen,
                    screenX - Radius,
                    screenY - Radius,
                    Radius * 2,
                    Radius * 2);
            }

            // Inner dot
            float dotRadius = Radius * 0.3f; // adjust as needed
            using (var brush = new SolidBrush(Color))
            {
                g.FillEllipse(brush,
                    screenX - dotRadius,
                    screenY - dotRadius,
                    dotRadius * 2,
                    dotRadius * 2);
            }
        }
    }

}
