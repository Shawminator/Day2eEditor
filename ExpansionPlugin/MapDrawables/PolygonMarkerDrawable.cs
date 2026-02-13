using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class PolygonDrawable : IMapDrawable
    {
        public IReadOnlyList<PointF> MapPoints { get; }
        public Color Color { get; set; } = Color.Red;
        public float StrokeWidth { get; set; } = 2f;
        public bool Fill { get; set; } = false;
        public int FillAlpha { get; set; } = 60;

        public bool Shade { get; set; } = false;
        public int ShadeAlpha { get; set; } = 175;
        public float ShadeSpacing { get; set; } = 7.5f;
        public bool DrawVertices { get; set; } = false;
        public float VertexRadius { get; set; } = 3f;
        public int VertexAlpha { get; set; } = 255;

        public Vec3 Selectedvec3 { get; set; }

        private readonly Size _mapSize;

        public PolygonDrawable(IEnumerable<PointF> mapPoints, Size mapSize)
        {
            if (mapPoints == null) throw new ArgumentNullException(nameof(mapPoints));

            var pts = mapPoints.ToList();
            if (pts.Count < 3) throw new ArgumentException("Polygon requires at least 3 points.", nameof(mapPoints));

            MapPoints = pts;
            _mapSize = mapSize;
        }

        public void Draw(Graphics g, RectangleF drawBounds, float zoom, PointF panOffset)
        {
            var screenPoints = new PointF[MapPoints.Count];
            for (int i = 0; i < MapPoints.Count; i++)
            {
                var mp = MapPoints[i];

                float normalizedX = mp.X / _mapSize.Width;
                float normalizedY = 1f - (mp.Y / _mapSize.Height);

                float screenX = drawBounds.X + normalizedX * drawBounds.Width;
                float screenY = drawBounds.Y + normalizedY * drawBounds.Height;

                screenPoints[i] = new PointF(screenX, screenY);
            }
            PointF? selectedpoint = null;
            if(Selectedvec3 != null)
            {
                PointF mp = new PointF(Selectedvec3.X, Selectedvec3.Z);

                float normalizedX = mp.X / _mapSize.Width;
                float normalizedY = 1f - (mp.Y / _mapSize.Height);

                float screenX = drawBounds.X + normalizedX * drawBounds.Width;
                float screenY = drawBounds.Y + normalizedY * drawBounds.Height;

                selectedpoint = new PointF(screenX, screenY);
            }

            // Build path
            using var path = new GraphicsPath();
            path.AddPolygon(screenPoints);

            // Optional fill
            if (Fill)
            {
                using var fillBrush = new SolidBrush(Color.FromArgb(FillAlpha, Color));
                g.FillPath(fillBrush, path);
            }

            // Optional shading clipped to polygon
            if (Shade)
            {
                var state = g.Save();
                g.SetClip(path);

                using var pen = new Pen(Color.FromArgb(ShadeAlpha, Color), 1f);

                // Shade over the polygon's bounds
                var b = path.GetBounds();
                float left = b.Left;
                float top = b.Top;
                float width = b.Width;
                float height = b.Height;

                // Draw diagonal lines that cover the bounding box
                float max = width + height;
                for (float i = -max; i < max * 2; i += ShadeSpacing)
                {
                    PointF start = new PointF(left + i, top);
                    PointF end = new PointF(left, top + i);
                    g.DrawLine(pen, start, end);
                }

                g.Restore(state);
            }

            // Outline
            using (var pen = new Pen(Color, StrokeWidth))
            {
                g.DrawPath(pen, path);
            }

            // Draw vertex points as small circles
            if (DrawVertices)
            {
                using var brush = new SolidBrush(Color.FromArgb(VertexAlpha, Color));

                float d = VertexRadius * 2f;

                using var fill = new SolidBrush(Color.FromArgb(VertexAlpha, Color));
                using var outline = new Pen(Color.Black, 1f);

                for (int i = 0; i < screenPoints.Length; i++)
                {
                    var p = screenPoints[i];
                    if (selectedpoint != null && p == selectedpoint)
                    {
                        fill.Color = Color.Yellow;
                    }
                    else
                    {
                        fill.Color = Color;
                    }
                    var rect = new RectangleF(
                        p.X - VertexRadius,
                        p.Y - VertexRadius,
                        VertexRadius * 2,
                        VertexRadius * 2
                    );

                    g.FillEllipse(fill, rect);
                    g.DrawEllipse(outline, rect);
                }
            }
        }
    }
    public static class PolygonPanTarget
    {
        public static PointF GetPanTargetXZ(IReadOnlyList<Vec3> pts)
        {
            if (pts == null || pts.Count < 1)
                throw new ArgumentException("Polygon requires at least 3 points.", nameof(pts));

            // Bounds center
            float minX = float.MaxValue, maxX = float.MinValue;
            float minZ = float.MaxValue, maxZ = float.MinValue;

            for (int i = 0; i < pts.Count; i++)
            {
                var p = pts[i];
                if (p.X < minX) minX = p.X;
                if (p.X > maxX) maxX = p.X;
                if (p.Z < minZ) minZ = p.Z;
                if (p.Z > maxZ) maxZ = p.Z;
            }

            var boundsCenter = new PointF((minX + maxX) / 2f, (minZ + maxZ) / 2f);
            if (PointInPolygonXZ(boundsCenter, pts))
                return boundsCenter;

            // Average of vertices (often inside, but not guaranteed)
            float sx = 0f, sz = 0f;
            for (int i = 0; i < pts.Count; i++)
            {
                sx += pts[i].X;
                sz += pts[i].Z;
            }
            var avg = new PointF(sx / pts.Count, sz / pts.Count);
            if (PointInPolygonXZ(avg, pts))
                return avg;

            // Fallback: closest vertex to bounds center (always valid)
            int bestIdx = 0;
            float bestD2 = Dist2XZ(pts[0], boundsCenter);

            for (int i = 1; i < pts.Count; i++)
            {
                float d2 = Dist2XZ(pts[i], boundsCenter);
                if (d2 < bestD2)
                {
                    bestD2 = d2;
                    bestIdx = i;
                }
            }

            return new PointF(pts[bestIdx].X, pts[bestIdx].Z);
        }

        // Ray casting point-in-polygon test in X/Z
        private static bool PointInPolygonXZ(PointF p, IReadOnlyList<Vec3> poly)
        {
            bool inside = false;
            int n = poly.Count;

            for (int i = 0, j = n - 1; i < n; j = i++)
            {
                float xi = poly[i].X, zi = poly[i].Z;
                float xj = poly[j].X, zj = poly[j].Z;

                bool intersect =
                    ((zi > p.Y) != (zj > p.Y)) &&
                    (p.X < (xj - xi) * (p.Y - zi) / ((zj - zi) == 0 ? 1e-12f : (zj - zi)) + xi);

                if (intersect)
                    inside = !inside;
            }

            return inside;
        }

        private static float Dist2XZ(Vec3 v, PointF p)
        {
            float dx = v.X - p.X;
            float dz = v.Z - p.Y;
            return dx * dx + dz * dz;
        }
    }
}
