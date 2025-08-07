using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconomyPlugin
{
    public interface IUIHandler
    {
        void LoadFromData(object data, TreeNode node);
        void ApplyChanges();
        void Reset(); // Revert changes to the initial state
        void HasChanges(); // Check if any changes have been made
        Control GetControl(); // Returns the actual UserControl to show
    }
}
