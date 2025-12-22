using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class VehicleSpawnDrawable : IMapDrawable
    {
        public PointF MapPosition { get; set; }
        public Color Color { get; set; } = Color.Red;
        public float screenRadius { get; set; } = 10f;
        public float[] Orientation { get; set; } = new float[] { 0, 0, 0 };


        private readonly Size _mapSize;

        public VehicleSpawnDrawable(PointF mapPosition, Size mapSize)
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


            // Convert radius from map units to screen pixels
            using (var pen = new Pen(Color, 2))
            {
                g.DrawEllipse(pen,
                    screenX - screenRadius,
                    screenY - screenRadius,
                    screenRadius * 2,
                    screenRadius * 2);
            }
            float dotRadius = 2f; // adjust as needed
            using (var brush = new SolidBrush(Color))
            {
                g.FillEllipse(brush,
                    screenX - dotRadius,
                    screenY - dotRadius,
                    dotRadius * 2,
                    dotRadius * 2);
            }

            // Arrow from center based on Orientation[0]
            float orientationDeg = (Orientation != null && Orientation.Length > 0) ? Orientation[0] : 0f;
            float rad = (float)(Math.PI / 180.0) * orientationDeg;

            // Map compass to screen axes:
            // 0° = North => (dx=0, dy<0), 90° = East => (dx>0, dy=0), etc.
            float dx = (float)Math.Sin(rad);
            float dy = (float)-Math.Cos(rad); // negative because screen Y goes down

            // Length of arrow (adjust as needed)
            float arrowLength = screenRadius * 3f;

            PointF start = new PointF(screenX, screenY);
            PointF end = new PointF(screenX + dx * arrowLength, screenY + dy * arrowLength);

            using (var arrowPen = new Pen(Color, 2f))
            {
                // Nice arrowhead
                var head = new AdjustableArrowCap(4f, 6f, true); // (width, height, filled)
                arrowPen.CustomEndCap = head;

                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.DrawLine(arrowPen, start, end);
            }

        }
    }

}
