namespace CaparePlugin
{
    public interface IUIHandler
    {
        Control GetControl(); // Returns the actual UserControl to show
        void LoadFromData(Type parent, object data, List<TreeNode> selectedNodes);
        event Action DataChanged; // Raised after the control writes a value, used to redraw the map
    }
}
