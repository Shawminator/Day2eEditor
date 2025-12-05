using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class MarkerIconDrawable : IMapDrawable
    {
        public Image Image { get; set; }
        public PointF MapPosition { get; set; }
        public bool IsSelected { get; set; } = false;
        
        private readonly Size _mapSize;
        public MarkerIconDrawable(PointF mapPosition, Size mapSize)
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

            // Icon size = circle diameter
            float finalW = 35f;
            float finalH = 35f;

            // Centered draw
            var destRect = new RectangleF(
                screenX - finalW / 2f,
                screenY - finalH / 2f,
                finalW,
                finalH
            );


            // High-quality scaling
            var prevInterp = g.InterpolationMode;
            var prevPixelOffset = g.PixelOffsetMode;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

            g.DrawImage(Image, destRect);

            g.InterpolationMode = prevInterp;
            g.PixelOffsetMode = prevPixelOffset;

            if (IsSelected)
            {

                // Glow underlay (semi-transparent, wider pen)
                using (var glowPen = new Pen(Color.FromArgb(60, Color.DodgerBlue), 6f))
                {
                    glowPen.Alignment = System.Drawing.Drawing2D.PenAlignment.Center; // default
                    float pad = 4f;
                    var glowRect = new RectangleF(
                        destRect.X - pad,
                        destRect.Y - pad,
                        destRect.Width + pad * 2f,
                        destRect.Height + pad * 2f
                    );

                    g.DrawRectangle(glowPen, glowRect.X, glowRect.Y, glowRect.Width, glowRect.Height);
                }

                // Foreground dashed stroke
                using (var pen = new Pen(Color.DodgerBlue, 2f))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    pen.DashPattern = new float[] { 5f, 3f };

                    float pad = 4f;
                    var rect = new RectangleF(
                        destRect.X - pad,
                        destRect.Y - pad,
                        destRect.Width + pad * 2f,
                        destRect.Height + pad * 2f
                    );

                    g.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
                }

            }

        }
    }
}
