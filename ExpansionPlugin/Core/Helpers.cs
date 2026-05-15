using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExpansionPlugin
{
    public enum EmoteID
    {
        GREETING = 1,
        SOS = 2,
        HEART = 3,
        TAUNT = 4,
        LYINGDOWN = 5,
        TAUNTKISS = 6,
        FACEPALM = 7,
        TAUNTELBOW = 8,
        THUMB = 9,
        THROAT = 10,
        SUICIDE = 11,
        DANCE = 12,
        CAMPFIRE = 13,
        SITA = 14,
        SITB = 15,
        THUMBDOWN = 16,

        DABBING = 32,
        TIMEOUT = 35,
        CLAP = 39,
        POINT = 40,
        SILENT = 43,
        SALUTE = 44,
        RPS = 45,
        WATCHING = 46,
        HOLD = 47,
        LISTENING = 48,
        POINTSELF = 49,
        LOOKATME = 50,
        TAUNTTHINK = 51,
        MOVE = 52,
        DOWN = 53,
        COME = 54,
        RPS_R = 55,
        RPS_P = 56,
        RPS_S = 57,
        NOD = 58,
        SHAKE = 59,
        SHRUG = 60,
        SURRENDER = 61,
        VOMIT = 62,

        DEBUG = 1000
    }
    public static class Helpers
    {
        public static  TreeNode FindNodeByTag(TreeNodeCollection nodes, object target)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag == target)
                    return node;

                var found = FindNodeByTag(node.Nodes, target);
                if (found != null)
                    return found;
            }

            return null;
        }
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
        public static bool GetTraderFromMissionFile(
            string line,
            out string npcClassName,
            out string traderName,
            out BindingList<Vec3> positions,
            out Vec3 rotation,
            out List<TraderNPCItem> items,
            out TraderNPCSpecialProperties special)
        {
            npcClassName = string.Empty;
            traderName = string.Empty;
            positions = new BindingList<Vec3>();
            rotation = new Vec3();
            items = new List<TraderNPCItem>();
            special = new TraderNPCSpecialProperties();

            if (string.IsNullOrWhiteSpace(line))
                return false;

            string[] tokens = line.Split('|');
            if (tokens.Length < 3)
                return false;

            // --- Name split ---
            int dotIndex = tokens[0].IndexOf('.');
            if (dotIndex > 0)
            {
                npcClassName = tokens[0].Substring(0, dotIndex).Trim();
                traderName = tokens[0].Substring(dotIndex + 1).Trim();
            }
            else
            {
                npcClassName = tokens[0].Trim();
            }

            // --- Positions ---
            string[] positionTokens = tokens[1].Split(',');
            foreach (string positionToken in positionTokens)
            {
                string trimmed = positionToken.Trim();
                if (!string.IsNullOrEmpty(trimmed))
                {
                    positions.Add(new Vec3(trimmed));
                }
            }

            // --- Rotation ---
            rotation = new Vec3(tokens[2].Trim());

            // --- Gear / Items / Special Props ---
            if (tokens.Count() > 3)
            {
                string[] gearTokens = tokens[3].Split(',');

                foreach (string token in gearTokens)
                {
                    string trimmed = token.Trim();
                    if (string.IsNullOrEmpty(trimmed))
                        continue;

                    // 🔹 Special properties (key:value)
                    if (trimmed.Contains(":"))
                    {
                        var parts = trimmed.Split(':', 2);
                        string key = parts[0].ToLower();
                        string value = parts.Length > 1 ? parts[1].Trim() : string.Empty;

                        switch (key)
                        {
                            case "name":
                                special.Name = value;
                                break;
                            case "loadout":
                                special.Loadout = value;
                                break;
                            case "faction":
                                special.Faction = value;
                                break;
                        }

                        continue;
                    }

                    // 🔹 Item + attachments
                    var itemParts = trimmed.Split('+');

                    var item = new TraderNPCItem
                    {
                        ClassName = itemParts[0].Trim()
                    };

                    for (int i = 1; i < itemParts.Length; i++)
                    {
                        string attachment = itemParts[i].Trim();
                        if (!string.IsNullOrEmpty(attachment))
                        {
                            item.Attachments.Add(attachment);
                        }
                    }

                    items.Add(item);
                }
            }

            return true;
        }

        public static string BuildTraderMissionLine(ExpansionTraderMaps trader)
        {
            if (trader == null)
                throw new ArgumentNullException(nameof(trader));

            var parts = new List<string>();

            // --- NPC class + trader name ---
            if (!string.IsNullOrEmpty(trader.TraderName))
                parts.Add($"{trader.NpcClassName}.{trader.TraderName}");
            else
                parts.Add(trader.NpcClassName);

            // --- Positions ---
            var positions = trader.Positions
                .Select(p => p.GetString())
                .Where(s => !string.IsNullOrWhiteSpace(s));

            parts.Add(string.Join(",", positions));

            // --- Rotation ---
            parts.Add(trader.Rotation.GetString());

            // --- Items + Special props ---
            var gear = new List<string>();

            // Items (with attachments)
            foreach (var item in trader.Items)
            {
                if (string.IsNullOrWhiteSpace(item.ClassName))
                    continue;

                if (item.Attachments.Count > 0)
                {
                    gear.Add(item.ClassName + "+" + string.Join("+", item.Attachments));
                }
                else
                {
                    gear.Add(item.ClassName);
                }
            }

            // Special properties (only if set)
            if (!string.IsNullOrWhiteSpace(trader.Special.Name))
                gear.Add($"name:{trader.Special.Name}");

            if (!string.IsNullOrWhiteSpace(trader.Special.Loadout))
                gear.Add($"loadout:{trader.Special.Loadout}");

            if (!string.IsNullOrWhiteSpace(trader.Special.Faction))
                gear.Add($"faction:{trader.Special.Faction}");

            if (gear.Count > 0)
                parts.Add(string.Join(",", gear));

            return string.Join("|", parts);
        }
        public static string GetNPCReferenceText(int id)
        {
            var NPCfiles = AppServices.GetRequired<ExpansionManager>().ExpansionQuestNPCDataConfig.MutableItems;
            ExpansionQuestNPCData npc = NPCfiles.FirstOrDefault(x => x.ID == id);

            return $"🔗 {npc.NPCName} ({npc.ClassName}) {npc.GetNPCType()}";
        }
    }
}
