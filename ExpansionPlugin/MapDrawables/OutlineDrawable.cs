using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace ExpansionPlugin
{
    public class OutlineDrawable : IMapDrawable
    {
        public IReadOnlyList<Vec3> Points { get; }
        public Color Color { get; set; } = Color.Gray;
        public float LineWidth { get; set; } = 2f;
        public float[] DashPattern { get; set; } = new[] { 6f, 4f };

        private readonly Size _mapSize;

        public OutlineDrawable(IEnumerable<Vec3> points, Size mapSize)
        {
            if (points == null)
                throw new ArgumentNullException(nameof(points));

            Points = points.ToList();
            _mapSize = mapSize;
        }

        public void Draw(Graphics g, RectangleF drawBounds, float zoom, PointF panOffset)
        {
            if (Points.Count == 0)
                return;

            float minX = Points.Min(p => p.X);
            float maxX = Points.Max(p => p.X);
            float minZ = Points.Min(p => p.Z);
            float maxZ = Points.Max(p => p.Z);

            float nMinX = minX / _mapSize.Width;
            float nMaxX = maxX / _mapSize.Width;
            float nMinY = 1f - (maxZ / _mapSize.Height); 
            float nMaxY = 1f - (minZ / _mapSize.Height);

            float screenX = drawBounds.X + nMinX * drawBounds.Width;
            float screenY = drawBounds.Y + nMinY * drawBounds.Height;
            float screenW = (nMaxX - nMinX) * drawBounds.Width;
            float screenH = (nMaxY - nMinY) * drawBounds.Height;

            if (screenW <= 0 || screenH <= 0)
                return;

            using var pen = new Pen(Color, LineWidth)
            {
                DashStyle = DashStyle.Custom,
                DashPattern = DashPattern
            };

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawRectangle(pen, screenX, screenY, screenW, screenH);
        }
    }
}
