using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPlugin
{

    public enum PatrolBehaviour
    {
        HALT,
        ONCE,
        LOOP,
        ALTERNATE,
        HALT_OR_ALTERNATE,
        HALT_OR_LOOP,
        ROAMING
    }

    public class AIPatrolDrawable : IMapDrawable
    {
        public PointF MapPosition { get; set; }
        public PointF MapPosition2 { get; set; }
        public Color Color { get; set; } = Color.Red;
        public float Radius { get; set; } = 5f;

        public bool Scaleradius { get; set; } = false;
        public bool WriteString { get; set; } = false;
        public string text { get; set; } = "";
        public PatrolBehaviour Behaviour { get; set; }


        private readonly Size _mapSize;
        
        public AIPatrolDrawable(PointF mapPosition, PointF mapPosition2, Size mapSize, PatrolBehaviour behaviour)
        {
            MapPosition = mapPosition;
            MapPosition2 = mapPosition2;
            _mapSize = mapSize;
            Behaviour = behaviour;
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
            float screenRadius = Scaleradius
                            ? ((Radius / _mapSize.Width * drawBounds.Width) + (Radius / _mapSize.Height * drawBounds.Height)) / 2f
                            : Radius;


            

            PointF center = new PointF(screenX, screenY);
            PointF center2 = new PointF(screenX2, screenY2);

            if (Behaviour == PatrolBehaviour.HALT)
            {
                // No line drawing
            }
            else if (Behaviour == PatrolBehaviour.ROAMING)
            {
                int lineCount = 10;

                // Normalize line length to stay consistent across zoom
                float desiredScreenLength = 100f;
                float normalizedLengthX = desiredScreenLength / _mapSize.Width * drawBounds.Height;
                float normalizedLengthY = desiredScreenLength / _mapSize.Height * drawBounds.Width;
                float lineLength = (normalizedLengthX + normalizedLengthY) / 2f;

                using (var roamPen = new Pen(Color.Gray, 2) { DashStyle = DashStyle.Dot })
                {
                    for (int i = 0; i < lineCount; i++)
                    {
                        double angle = i * (2 * Math.PI / lineCount); // Even spacing
                        float dx = (float)(Math.Cos(angle) * lineLength);
                        float dy = (float)(Math.Sin(angle) * lineLength);

                        PointF endPoint = new PointF(center.X + dx, center.Y + dy);
                        g.DrawLine(roamPen, center, endPoint);
                    }
                }
            }
            else if (Behaviour == PatrolBehaviour.ONCE)
            {
                g.DrawLine(GetArrowPen(), center, center2);
                DrawArrow(g, center, center2, GetArrowPen());
            }
            else if (Behaviour == PatrolBehaviour.LOOP)
            {
                g.DrawLine(GetArrowPen(), center, center2);
                DrawArrow(g, center, center2, GetArrowPen());

                // If this is the last waypoint, draw return arrow to first
                if (MapPosition2 == MapPosition) // Optional check to avoid duplicate arrows
                {
                    DrawArrow(g, center2, center, GetArrowPen());
                }
            }
            else if (Behaviour == PatrolBehaviour.ALTERNATE)
            {
                g.DrawLine(GetArrowPen(), center, center2);
                DrawArrow(g, center, center2, GetArrowPen());
                DrawArrow(g, center2, center, GetArrowPen());
            }
            else if (Behaviour == PatrolBehaviour.HALT_OR_ALTERNATE)
            {
                g.DrawLine(GetArrowPen(true), center, center2);
                DrawArrow(g, center, center2, GetArrowPen());
                DrawArrow(g, center2, center, GetArrowPen());
            }
            else if (Behaviour == PatrolBehaviour.HALT_OR_LOOP)
            {
                g.DrawLine(GetArrowPen(true), center, center2);
                DrawArrow(g, center, center2, GetArrowPen());

                // If this is the last waypoint, draw return arrow to first
                if (MapPosition2 == MapPosition) // Optional check to avoid duplicate arrows
                {
                    DrawArrow(g, center2, center, GetArrowPen());
                }
            }
            using (var pen = new Pen(Color, 2))
            {
                g.DrawEllipse(pen, screenX - screenRadius, screenY - screenRadius, screenRadius * 2, screenRadius * 2);
            }

            using (var brush = new SolidBrush(Color))
            {
                g.FillEllipse(brush, screenX - 2f, screenY - 2f, 4f, 4f);
            }
            if (WriteString)
            {
                Rectangle rect3 = new Rectangle((int)center.X - 2, (int)center.Y - 32, 200, 40);
                using (var font = new Font("Tahoma", 9))
                {
                    g.DrawString(text, font, Brushes.White, rect3);
                }
            }

        }

        private void DrawArrow(Graphics g, PointF from, PointF to, Pen pen)
        {
            float dx = to.X - from.X;
            float dy = to.Y - from.Y;
            float length = (float)Math.Sqrt(dx * dx + dy * dy);

            if (length == 0) return;

            // Normalize direction
            dx /= length;
            dy /= length;

            float arrowSize = 10f;
            float px = -dy;
            float py = dx;

            // Shift the arrow position (e.g., 40% of the way instead of 50%)
            float arrowPosition = 0.4f; // 0 = start, 1 = end
            float shift = arrowPosition * length;

            // Compute the base point of the arrow (slightly toward 'from')
            PointF basePoint = new PointF(from.X + dx * shift, from.Y + dy * shift);

            // Compute arrowhead points
            PointF arrowPoint1 = new PointF(basePoint.X - arrowSize * dx + arrowSize * 0.5f * px,
                                            basePoint.Y - arrowSize * dy + arrowSize * 0.5f * py);
            PointF arrowPoint2 = new PointF(basePoint.X - arrowSize * dx - arrowSize * 0.5f * px,
                                            basePoint.Y - arrowSize * dy - arrowSize * 0.5f * py);

            // Draw the arrowhead
            g.DrawLine(pen, basePoint, arrowPoint1);
            g.DrawLine(pen, basePoint, arrowPoint2);
        }

        private Pen GetArrowPen(bool dashed = false)
        {
            Color arrowColor = Color.Green;
            DashStyle dashStyle = DashStyle.Solid;
            if (dashed) { dashStyle = DashStyle.Dash; }

            switch (Behaviour)
            {
                case PatrolBehaviour.LOOP:
                    arrowColor = Color.Blue;
                    break;
                case PatrolBehaviour.ALTERNATE:
                    arrowColor = Color.Orange;
                    break;
                case PatrolBehaviour.HALT_OR_ALTERNATE:
                    arrowColor = Color.Orange;
                    dashStyle = DashStyle.Dash;
                    break;
                case PatrolBehaviour.HALT_OR_LOOP:
                    arrowColor = Color.Blue;
                    dashStyle = DashStyle.Dash;
                    break;
            }

            return new Pen(arrowColor, 2) { DashStyle = dashStyle };
        }

    }
}
