using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class MarkerDrawable : IMapDrawable
    {
        public PointF MapPosition { get; set; }
        public Color Color { get; set; } = Color.Red;
        public float Radius { get; set; } = 5f;
        public bool Fill { get; set; } = false;
        public int FillAlpha { get; set; } = 60;
        public bool Scaleradius { get; set; } = false;
        public bool Shade { get; set; } = false;
        public int ShadeAlpha { get; set; } = 175;
        public float ShadeSpacing { get; set; } = 7.5f;

        private readonly Size _mapSize;

        public MarkerDrawable(PointF mapPosition, Size mapSize)
        {
            MapPosition = mapPosition;
            _mapSize = mapSize;
        }

        public void Draw(Graphics g, RectangleF drawBounds, float zoom, PointF panOffset)
        {
            float normalizedX = MapPosition.X / _mapSize.Width;
            float normalizedY = 1f - (MapPosition.Y / _mapSize.Height);
            float screenX = drawBounds.X + normalizedX * drawBounds.Width;
            float screenY = drawBounds.Y + normalizedY * drawBounds.Height;
            float screenRadius;
            if (Scaleradius)
            {
                float normalizedRadiusX = Radius / _mapSize.Width;
                float normalizedRadiusY = Radius / _mapSize.Height;
                float screenRadiusX = normalizedRadiusX * drawBounds.Width;
                float screenRadiusY = normalizedRadiusY * drawBounds.Height;
                screenRadius = (screenRadiusX + screenRadiusY) / 2f;
            }
            else
            {
                screenRadius = Radius;
            }
            using (var pen = new Pen(Color, 2))
            {
                g.DrawEllipse(pen,
                    screenX - screenRadius,
                    screenY - screenRadius,
                    screenRadius * 2,
                    screenRadius * 2);
            }
            if (Shade)
            {
                using (var path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    path.AddEllipse(
                        screenX - screenRadius,
                        screenY - screenRadius,
                        screenRadius * 2,
                        screenRadius * 2);

                    if (Fill)
                    {
                        using var fillBrush = new SolidBrush(Color.FromArgb(FillAlpha, Color));
                        g.FillPath(fillBrush, path);
                    }

                    var state = g.Save();
                    g.SetClip(path);
                    using var pen = new Pen(Color.FromArgb(ShadeAlpha, Color), 1f);

                     var b = path.GetBounds();
                    float left = b.Left;
                    float top = b.Top;
                    float width = b.Width;
                    float height = b.Height;

                    float max = width + height;
                    for (float i = -max; i < max * 2; i += ShadeSpacing)
                    {
                        PointF start = new PointF(left + i, top);
                        PointF end = new PointF(left, top + i);
                        g.DrawLine(pen, start, end);
                    }

                    g.Restore(state);
                }
            }
            float dotRadius = 2f; 
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
