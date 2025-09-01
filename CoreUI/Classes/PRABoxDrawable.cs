using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Day2eEditor
{
    public class PRABoxDrawable : IMapDrawable
    {

        public Vector3 HalfExtents { get; }
        public Vector3 Orientation { get; }
        public Vector3 Position { get; }
        private readonly Size _mapSize;
        public Color Color { get; set; } = Color.Green;

        public PRABoxDrawable(Vector3 halfExtents, Vector3 orientation, Vector3 position, Size mapSize)
        {
            HalfExtents = halfExtents;
            Orientation = orientation;
            Position = position;
            _mapSize = mapSize;
        }

        public void Draw(Graphics g, RectangleF drawBounds, float zoom, PointF panOffset)
        {
            // Normalize position like MarkerDrawable
            float normalizedX = Position.X / _mapSize.Width;
            float normalizedY = 1f - (Position.Z / _mapSize.Height);

            float screenX = drawBounds.X + normalizedX * drawBounds.Width;
            float screenY = drawBounds.Y + normalizedY * drawBounds.Height;

            // Scale size
            float width = HalfExtents.X * 2 * zoom;
            float height = HalfExtents.Z * 2 * zoom;

            RectangleF rect = new RectangleF(-width / 2, -height / 2, width, height);

            var state = g.Save();

            g.TranslateTransform(screenX, screenY);
            g.RotateTransform(Orientation.X); // Only Z rotation for 2D

            using (var pen = new Pen(Color, 2))
            {
                g.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
            }

            g.Restore(state);
        }

    }
}
