using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public static class Helpers
    {
        public static void InsertNodeAlphabetically(TreeNodeCollection nodes, TreeNode newNode)
        {
            int index = 0;

            while (index < nodes.Count &&
                   string.Compare(nodes[index].Text, newNode.Text, StringComparison.OrdinalIgnoreCase) < 0)
            {
                index++;
            }

            nodes.Insert(index, newNode);
        }
    }
}
