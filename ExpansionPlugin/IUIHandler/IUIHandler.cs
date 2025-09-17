using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public interface IUIHandler
    {
        Control GetControl(); // Returns the actual UserControl to show
        void LoadFromData(Type parent, object data, List<TreeNode> selectedNodes);
        void ApplyChanges();
        void Reset(); // Revert changes to the initial state
        void HasChanges(); // Check if any changes have been made
    }
    public interface ITreeNodeHandler
    {
        void Show(TreeNode node, List<TreeNode> selected, ExpansionForm form);
    }
}
