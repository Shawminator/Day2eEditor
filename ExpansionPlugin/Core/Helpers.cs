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
        public static void InsertFolderNodeAtTop(TreeNodeCollection nodes, TreeNode folderNode)
        {
            int insertIndex = 0;

            while (insertIndex < nodes.Count &&
                   nodes[insertIndex].Tag is string tag &&
                   tag.StartsWith("MarketCategoryRelativePath:", StringComparison.Ordinal))
            {
                if (string.Compare(nodes[insertIndex].Text, folderNode.Text, StringComparison.OrdinalIgnoreCase) > 0)
                    break;

                insertIndex++;
            }

            nodes.Insert(insertIndex, folderNode);
        }
        public static void InsertFileNodeAfterFolders(TreeNodeCollection nodes, TreeNode fileNode)
        {
            int insertIndex = 0;

            // First: skip ALL folder nodes
            while (insertIndex < nodes.Count &&
                   nodes[insertIndex].Tag is string tag &&
                   tag.StartsWith("MarketCategoryRelativePath:", StringComparison.Ordinal))
            {
                insertIndex++;
            }

            // Second: insert in sorted position among file nodes
            while (insertIndex < nodes.Count)
            {
                if (string.Compare(nodes[insertIndex].Text, fileNode.Text, StringComparison.OrdinalIgnoreCase) > 0)
                    break;

                insertIndex++;
            }

            nodes.Insert(insertIndex, fileNode);
        }
        public static bool TryValidateFolderName(string folder, out string error)
        {
            error = null;

            if (string.IsNullOrWhiteSpace(folder))
            {
                error = "Folder name cannot be empty.";
                return false;
            }

            folder = folder.Trim();

            // Disallow path separators explicitly
            if (folder.Contains('/') || folder.Contains('\\'))
            {
                error = "Folder name cannot contain '/' or '\\'.";
                return false;
            }

            // Invalid file name chars
            if (folder.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                error = "Folder name contains invalid characters.";
                return false;
            }

            // Optional: Windows reserved names
            string[] reservedNames =
            {
                "CON","PRN","AUX","NUL",
                "COM1","COM2","COM3","COM4","COM5","COM6","COM7","COM8","COM9",
                "LPT1","LPT2","LPT3","LPT4","LPT5","LPT6","LPT7","LPT8","LPT9"
            };

            if (reservedNames.Contains(folder, StringComparer.OrdinalIgnoreCase))
            {
                error = $"'{folder}' is a reserved system name.";
                return false;
            }

            return true;
        }
        public static string SanitizePath(string input)
        {
            // Replace spaces with underscores
            string result = input.Replace(" ", "_");

            // Get invalid filename characters
            char[] invalidChars = Path.GetInvalidFileNameChars();

            // Remove invalid characters
            result = new string(result
                .Where(c => !invalidChars.Contains(c))
                .ToArray());

            return result;
        }
    }
}
