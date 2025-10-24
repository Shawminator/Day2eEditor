using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Security.Policy;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Day2eEditor
{
    public class MapViewerControl : Control
    {
        private DrawingManager _drawingManager = new DrawingManager();
        public event EventHandler<MapClickEventArgs> MapDoubleClicked;
        public event EventHandler<MapClickEventArgs> MapsingleClicked;
        
        private Image _image;
        private float _zoom = 1.0f;
        private PointF _panOffset = new PointF(0, 0);
        private Point _lastMousePos;
        private bool _isPanning = false;
        private RectangleF _drawnImageBounds;
        private Point _mousePosition;
        private Size _mapSize = new Size(1024, 1024); // Logical map coordinate system


        private Timer _clickTimer = new Timer { Interval = 100 };
        private MouseEventArgs _lastClick;
        private bool _isFirstClick = true;
        private bool _isDoubleClick = false;
        private int _elapsed = 0;

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
        public bool HasInitialPainted = false;

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
            Cursor = Cursors.Hand;
            _clickTimer.Tick += ClickTimer_Tick;
            DoubleBuffered = true;
            ResizeRedraw = true;
            SetStyle(ControlStyles.ResizeRedraw |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.UserPaint, true);
        }
        private void ClickTimer_Tick(object sender, EventArgs e)
        {
            _elapsed += _clickTimer.Interval;

            if (_elapsed >= SystemInformation.DoubleClickTime)
            {
                _clickTimer.Stop();

                var mapCoord = GetMapCoordinate(_lastClick.Location);

                if (_isDoubleClick)
                {
                    // Double-click action
                    MapDoubleClicked?.Invoke(this, new MapClickEventArgs(mapCoord));
                }
                else
                {
                    // Single-click action
                    //MessageBox.Show($"Left-click at Map Coord:\nX: {mapCoord.X:0.##}, Z: {mapCoord.Y:0.##}","Map Coordinate");
                    MapsingleClicked?.Invoke(this, new MapClickEventArgs(mapCoord));
                }

                // Reset click state
                _isFirstClick = true;
                _isDoubleClick = false;
                _elapsed = 0;
            }
        }
        public void LoadMap(Image image, int mapSize)
        {
            _image = image;
            _mapSize = new Size(mapSize,mapSize);
            _zoom = 1.0f;
            _panOffset = new PointF(0, 0);
            Invalidate();
        }
        public void ClearMap()
        {
            if (_image != null)
            {
                _image.Dispose();
                _image = null;
            }
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
        public PointF MapToScreen(PointF mapCoord)
        {
            if (_image == null || _image.Width == 0 || _image.Height == 0)
                return PointF.Empty;

            // Normalize map coordinates to [0..1]
            float normalizedX = mapCoord.X / _mapSize.Width;
            float normalizedY = 1f - (mapCoord.Y / _mapSize.Height); // flip Y to match your GetMapCoordinate

            // Convert to image pixel space
            float imageX = normalizedX * _image.Width;
            float imageY = normalizedY * _image.Height;

            // Convert to drawn screen coordinates
            float screenX = _drawnImageBounds.X + imageX * _drawnImageBounds.Width / _image.Width;
            float screenY = _drawnImageBounds.Y + imageY * _drawnImageBounds.Height / _image.Height;

            return new PointF(screenX, screenY);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_image != null)
            {
                float drawWidth = _image.Width * _zoom;
                float drawHeight = _image.Height * _zoom;

                float drawX = _panOffset.X;
                float drawY = _panOffset.Y;

                _drawnImageBounds = new RectangleF(drawX, drawY, drawWidth, drawHeight);

                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                e.Graphics.DrawImage(_image, _drawnImageBounds);
            }

            foreach (var drawable in _drawingManager.GetDrawables())
            {
                drawable.Draw(e.Graphics, _drawnImageBounds, _zoom, _panOffset);
            }

            using (var pen = new Pen(Color.Gray, 1))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
            }

            if (_image != null)
            {
                var mapCoord = GetMapCoordinate(_mousePosition);
                string coordText = $"X: {mapCoord.X:0.##}, Y: {mapCoord.Y:0.##}";

                using var font = new Font("Segoe UI", 9);
                using var brush = new SolidBrush(Color.White);
                using var backBrush = new SolidBrush(Color.FromArgb(128, 0, 0, 0)); // Semi-transparent background

                var textSize = e.Graphics.MeasureString(coordText, font);

                float padding = 4; // padding from edges
                float textX = Width - textSize.Width - padding * 2; // top-right x-coordinate
                float textY = padding; // top padding
                var textRect = new RectangleF(textX, textY, textSize.Width + padding * 2, textSize.Height + padding);
                e.Graphics.FillRectangle(backBrush, textRect);
                e.Graphics.DrawString(coordText, font, brush, new PointF(textX + padding, textY + padding / 2));
            }
            HasInitialPainted = true;
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Right)
            {
                Cursor.Current = Cursors.SizeAll;
                _isPanning = true;
                _lastMousePos = e.Location;
                return;
            }
            if (e.Button == MouseButtons.Left)
            {
                _lastClick = e;

                if (_isFirstClick)
                {
                    _isFirstClick = false;
                    _elapsed = 0;
                    _isDoubleClick = false;
                    _clickTimer.Start();
                }
                else
                {
                    if ((Math.Abs(e.X - _lastClick.X) <= SystemInformation.DoubleClickSize.Width / 2) &&
                        (Math.Abs(e.Y - _lastClick.Y) <= SystemInformation.DoubleClickSize.Height / 2))
                    {
                        _isDoubleClick = true;
                    }
                }
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
                Cursor = Cursors.Hand;
            }
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (_image == null) return;

            float oldZoom = _zoom;
            float zoomFactor = e.Delta > 0 ? 1.1f : 0.9f;
            _zoom *= zoomFactor;
            _zoom = Math.Clamp(_zoom, 0.1f, 10f);

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
        public void CenterOn(PointF mapCoord)
        {
            if (_image == null) return;

            var screenPoint = MapToScreen(mapCoord);
            var controlCenter = new PointF(Width / 2f, Height / 2f);
            float dx = controlCenter.X - screenPoint.X;
            float dy = controlCenter.Y - screenPoint.Y;
            _panOffset.X += dx;
            _panOffset.Y += dy;
            Invalidate();
        }
        public void EnsureVisible(PointF mapCoord)
        {
            if (_image == null) return;

            var screenPoint = MapToScreen(mapCoord);
            var visibleRect = new RectangleF(0, 0, Width, Height);
            if (visibleRect.Contains(screenPoint))
                return;

            CenterOn(mapCoord);
        }


    }
    public class MapClickEventArgs : EventArgs
    {
        public PointF MapCoordinates { get; }

        public MapClickEventArgs(PointF mapCoords)
        {
            MapCoordinates = mapCoords;
        }
    }
}
