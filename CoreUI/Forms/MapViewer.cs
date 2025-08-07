using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Day2eEditor
{
    public class MapViewerControl : Control
    {
        private DrawingManager _drawingManager = new DrawingManager();

        private Image _image;
        private float _zoom = 1.0f;
        private PointF _panOffset = new PointF(0, 0);
        private Point _lastMousePos;
        private bool _isPanning = false;
        private RectangleF _drawnImageBounds;
        private Point _mousePosition;
        private Size _mapSize = new Size(1024, 1024); // Logical map coordinate system

       
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size MapSize
        {
            get => _mapSize;
            set
            {
                _mapSize = value;
                Invalidate();
            }
        }

        // Allows plugin to register drawables
        public void RegisterDrawable(IMapDrawable drawable)
        {
            _drawingManager.RegisterDrawable(drawable);
            Invalidate(); // Trigger a redraw
        }

        // Allows plugin to unregister drawables
        public void UnregisterDrawable(IMapDrawable drawable)
        {
            _drawingManager.UnregisterDrawable(drawable);
            Invalidate(); // Trigger a redraw
        }

        // Optionally, to clear all drawables
        public void ClearDrawables()
        {
            _drawingManager.Clear();
            Invalidate(); // Trigger a redraw
        }


        public MapViewerControl()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            SetStyle(ControlStyles.ResizeRedraw |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.UserPaint, true);
        }
        public void LoadMap(Image image, int mapSize)
        {
            _image = image;
            _mapSize = new Size(mapSize,mapSize);
            _zoom = 1.0f;
            _panOffset = new PointF(0, 0);
            Invalidate();
        }
        public PointF GetMapCoordinate(Point screenPoint)
        {
            if (_image == null || _image.Width == 0 || _image.Height == 0)
                return PointF.Empty;

            // Convert screen point to image space based on actual drawn bounds
            float imageX = (screenPoint.X - _drawnImageBounds.X) / _drawnImageBounds.Width * _image.Width;
            float imageY = (screenPoint.Y - _drawnImageBounds.Y) / _drawnImageBounds.Height * _image.Height;

            // This allows out-of-bounds points (e.g., negative coords)
            float normalizedX = imageX / _image.Width;
            float normalizedY = imageY / _image.Height;

            float mapX = normalizedX * _mapSize.Width;
            float mapY = (1f - normalizedY) * _mapSize.Height; // Flip Y to have 0 at bottom

            return new PointF(mapX, mapY);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_image != null)
            {
                // Image size after scaling
                float drawWidth = _image.Width * _zoom;
                float drawHeight = _image.Height * _zoom;

                // Final position after panning
                float drawX = _panOffset.X;
                float drawY = _panOffset.Y;

                // Save this for later coordinate conversion
                _drawnImageBounds = new RectangleF(drawX, drawY, drawWidth, drawHeight);

                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                e.Graphics.DrawImage(_image, _drawnImageBounds);
            }

            // Optional: draw control border (for visibility)
            using (var pen = new Pen(Color.Gray, 1))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
            }

            // Add mouse position coordinates display in the top-left corner
            if (_image != null)
            {
                // Convert the mouse screen position to map coordinates
                var mapCoord = GetMapCoordinate(_mousePosition);

                // Prepare the text to display
                string coordText = $"X: {mapCoord.X:0.##}, Y: {mapCoord.Y:0.##}";

                // Set the font and brush for drawing the text
                using var font = new Font("Segoe UI", 9);
                using var brush = new SolidBrush(Color.White);
                using var backBrush = new SolidBrush(Color.FromArgb(128, 0, 0, 0)); // Semi-transparent background

                // Measure the size of the text
                var textSize = e.Graphics.MeasureString(coordText, font);

                // Draw background rectangle for the label
                var textRect = new RectangleF(4, 4, textSize.Width + 8, textSize.Height + 4);
                e.Graphics.FillRectangle(backBrush, textRect);

                // Draw the text with padding
                e.Graphics.DrawString(coordText, font, brush, new PointF(8, 6));
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                var mapCoord = GetMapCoordinate(e.Location);
                MessageBox.Show($"Right-click at Map Coord:\nX: {mapCoord.X:0.##}, Y: {mapCoord.Y:0.##}", "Map Coordinate");
            }
            else if (e.Button == MouseButtons.Right)
            {
                _isPanning = true;
                _lastMousePos = e.Location;
                Cursor = Cursors.Hand;
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            _mousePosition = e.Location;
            if (_isPanning)
            {
                var dx = e.X - _lastMousePos.X;
                var dy = e.Y - _lastMousePos.Y;

                _panOffset.X += dx;
                _panOffset.Y += dy;

                _lastMousePos = e.Location;
                
            }
            Invalidate();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Right)
            {
                _isPanning = false;
                Cursor = Cursors.Default;
            }
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (_image == null) return;

            float oldZoom = _zoom;
            float zoomFactor = e.Delta > 0 ? 1.1f : 0.9f;
            _zoom *= zoomFactor;

            // Clamp zoom
            _zoom = Math.Clamp(_zoom, 0.1f, 10f);

            // Adjust pan to keep zoom centered under mouse
            var mouseBeforeZoom = new PointF(
                (e.X - _panOffset.X) / oldZoom,
                (e.Y - _panOffset.Y) / oldZoom
            );

            var mouseAfterZoom = new PointF(
                (e.X - _panOffset.X) / _zoom,
                (e.Y - _panOffset.Y) / _zoom
            );

            _panOffset.X += (mouseAfterZoom.X - mouseBeforeZoom.X) * _zoom;
            _panOffset.Y += (mouseAfterZoom.Y - mouseBeforeZoom.Y) * _zoom;

            Invalidate();
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }
    }
}
