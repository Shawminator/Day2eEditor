using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2eEditor
{
    public interface IMapDrawable
    {
        void Draw(Graphics g, RectangleF drawBounds, float zoom, PointF panOffset);
    }
    public class DrawingManager
    {
        private readonly List<IMapDrawable> _drawables = new List<IMapDrawable>();

        // Register a new drawable (spawn points, markers, etc.)
        public void RegisterDrawable(IMapDrawable drawable)
        {
            if (!_drawables.Contains(drawable))
            {
                _drawables.Add(drawable);
            }
        }

        // Remove a drawable (e.g., when it's no longer needed)
        public void UnregisterDrawable(IMapDrawable drawable)
        {
            if (_drawables.Contains(drawable))
            {
                _drawables.Remove(drawable);
            }
        }

        // Get all registered drawables
        public List<IMapDrawable> GetDrawables()
        {
            return _drawables;
        }

        // Clear all drawables
        public void Clear()
        {
            _drawables.Clear();
        }
    }

}
