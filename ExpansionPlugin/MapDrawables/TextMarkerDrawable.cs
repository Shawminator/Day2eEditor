using Day2eEditor;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ExpansionPlugin
{
    public enum MarkerLabelPlacement
    {
        Right,
        Left,
        Top,
        Bottom,
        Center
    }

    public class TextMarkerDrawable : IMapDrawable
    {
        public PointF MapPosition { get; set; }

        // Marker visuals
        public Color Color { get; set; } = Color.Red;
        public float Radius { get; set; } = 5f;
        public bool Fill { get; set; } = false;
        public int FillAlpha { get; set; } = 60;
        public bool Scaleradius { get; set; } = false;

        // Shade (same as your marker)
        public bool Shade { get; set; } = false;
        public int ShadeAlpha { get; set; } = 175;
        public float ShadeSpacing { get; set; } = 7.5f;

        // Label
        public string? Text { get; set; }
        public Font Font { get; set; } = SystemFonts.DefaultFont;
        public Color TextColor { get; set; } = Color.White;
        public MarkerLabelPlacement TextPlacement { get; set; } = MarkerLabelPlacement.Right;

        /// <summary>Extra pixels away from the circle edge.</summary>
        public float TextPaddingFromCircle { get; set; } = 6f;

        /// <summary>Additional pixel offset after placement.</summary>
        public PointF TextOffset { get; set; } = new PointF(0, 0);

        /// <summary>If true, draw a background box behind the text.</summary>
        public bool TextBackground { get; set; } = true;
        public int TextBackgroundAlpha { get; set; } = 160;
        public Color TextBackgroundColor { get; set; } = Color.Black;
        public float TextBackgroundPadding { get; set; } = 3f;
        public float TextBackgroundCornerRadius { get; set; } = 4f;

        /// <summary>If true, flips label side to keep it within drawBounds.</summary>
        public bool KeepTextInsideBounds { get; set; } = true;

        private readonly Size _mapSize;

        public TextMarkerDrawable(PointF mapPosition, Size mapSize)
        {
            MapPosition = mapPosition;
            _mapSize = mapSize;
        }

        public void Draw(Graphics g, RectangleF drawBounds, float zoom, PointF panOffset)
        {
            // Map -> screen
            float normalizedX = MapPosition.X / _mapSize.Width;
            float normalizedY = 1f - (MapPosition.Y / _mapSize.Height);
            float screenX = drawBounds.X + normalizedX * drawBounds.Width;
            float screenY = drawBounds.Y + normalizedY * drawBounds.Height;

            // Radius -> screen (optionally scaled)
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

            // Circle outline
            using (var pen = new Pen(Color, 2))
            {
                g.DrawEllipse(pen,
                    screenX - screenRadius,
                    screenY - screenRadius,
                    screenRadius * 2,
                    screenRadius * 2);
            }

            // Shade + optional fill (matching your logic)
            if (Shade)
            {
                using (var path = new GraphicsPath())
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
                    using var shadePen = new Pen(Color.FromArgb(ShadeAlpha, Color), 1f);

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
                        g.DrawLine(shadePen, start, end);
                    }

                    g.Restore(state);
                }
            }

            // Center dot
            float dotRadius = 2f;
            using (var brush = new SolidBrush(Color))
            {
                g.FillEllipse(brush,
                    screenX - dotRadius,
                    screenY - dotRadius,
                    dotRadius * 2,
                    dotRadius * 2);
            }

            // Label
            if (!string.IsNullOrWhiteSpace(Text))
            {
                DrawLabel(g, drawBounds, screenX, screenY, screenRadius);
            }
        }

        private void DrawLabel(Graphics g, RectangleF drawBounds, float cx, float cy, float screenRadius)
        {
            // Measure
            var text = Text!;
            var size = g.MeasureString(text, Font);
            var textW = size.Width;
            var textH = size.Height;

            // Decide placement
            var placement = TextPlacement;

            // Compute anchor point (top-left of text box)
            PointF pos = placement switch
            {
                MarkerLabelPlacement.Right => new PointF(cx + screenRadius + TextPaddingFromCircle, cy - textH / 2f),
                MarkerLabelPlacement.Left => new PointF(cx - screenRadius - TextPaddingFromCircle - textW, cy - textH / 2f),
                MarkerLabelPlacement.Top => new PointF(cx - textW / 2f, cy - screenRadius - TextPaddingFromCircle - textH),
                MarkerLabelPlacement.Bottom => new PointF(cx - textW / 2f, cy + screenRadius + TextPaddingFromCircle),
                MarkerLabelPlacement.Center => new PointF(cx - textW / 2f, cy - textH / 2f),
                _ => new PointF(cx + screenRadius + TextPaddingFromCircle, cy - textH / 2f),
            };

            pos = new PointF(pos.X + TextOffset.X, pos.Y + TextOffset.Y);

            // If requested, flip to keep within bounds
            if (KeepTextInsideBounds && placement != MarkerLabelPlacement.Center)
            {
                var rect = MakeTextRect(pos, textW, textH);

                // If off right, try left
                if (rect.Right > drawBounds.Right && placement == MarkerLabelPlacement.Right)
                {
                    placement = MarkerLabelPlacement.Left;
                    pos = new PointF(cx - screenRadius - TextPaddingFromCircle - textW, cy - textH / 2f);
                    pos = new PointF(pos.X + TextOffset.X, pos.Y + TextOffset.Y);
                }

                // If off left, try right
                rect = MakeTextRect(pos, textW, textH);
                if (rect.Left < drawBounds.Left && placement == MarkerLabelPlacement.Left)
                {
                    placement = MarkerLabelPlacement.Right;
                    pos = new PointF(cx + screenRadius + TextPaddingFromCircle, cy - textH / 2f);
                    pos = new PointF(pos.X + TextOffset.X, pos.Y + TextOffset.Y);
                }

                // If off top, try bottom
                rect = MakeTextRect(pos, textW, textH);
                if (rect.Top < drawBounds.Top && placement == MarkerLabelPlacement.Top)
                {
                    placement = MarkerLabelPlacement.Bottom;
                    pos = new PointF(cx - textW / 2f, cy + screenRadius + TextPaddingFromCircle);
                    pos = new PointF(pos.X + TextOffset.X, pos.Y + TextOffset.Y);
                }

                // If off bottom, try top
                rect = MakeTextRect(pos, textW, textH);
                if (rect.Bottom > drawBounds.Bottom && placement == MarkerLabelPlacement.Bottom)
                {
                    placement = MarkerLabelPlacement.Top;
                    pos = new PointF(cx - textW / 2f, cy - screenRadius - TextPaddingFromCircle - textH);
                    pos = new PointF(pos.X + TextOffset.X, pos.Y + TextOffset.Y);
                }
            }

            // Draw background (optional)
            if (TextBackground)
            {
                var bgRect = MakeTextRect(pos, textW, textH);
                bgRect.Inflate(TextBackgroundPadding, TextBackgroundPadding);

                using var bgBrush = new SolidBrush(Color.FromArgb(TextBackgroundAlpha, TextBackgroundColor));
                using var path = RoundedRect(bgRect, TextBackgroundCornerRadius);
                g.FillPath(bgBrush, path);
            }

            // Draw text
            using var textBrush = new SolidBrush(TextColor);
            g.DrawString(text, Font, textBrush, pos);
        }

        private static RectangleF MakeTextRect(PointF pos, float w, float h)
            => new RectangleF(pos.X, pos.Y, w, h);

        private static GraphicsPath RoundedRect(RectangleF rect, float radius)
        {
            var path = new GraphicsPath();
            if (radius <= 0.01f)
            {
                path.AddRectangle(rect);
                path.CloseFigure();
                return path;
            }

            float d = radius * 2f;
            var arc = new RectangleF(rect.X, rect.Y, d, d);

            // top-left
            path.AddArc(arc, 180, 90);

            // top-right
            arc.X = rect.Right - d;
            path.AddArc(arc, 270, 90);

            // bottom-right
            arc.Y = rect.Bottom - d;
            path.AddArc(arc, 0, 90);

            // bottom-left
            arc.X = rect.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }
    }
}
