namespace CaparePlugin
{
    /// <summary>
    /// Wraps the name property of a list entry (reward table, loot table, item set...)
    /// so they can all share CapareNameControl.
    /// </summary>
    public class CapareNamedItem
    {
        private readonly Func<string> _get;
        private readonly Action<string> _set;

        public CapareNamedItem(Func<string> get, Action<string> set)
        {
            _get = get;
            _set = set;
        }
        public string Name
        {
            get => _get();
            set => _set(value);
        }
        public override string ToString() => Name;
    }

    /// <summary>
    /// Tag for a tree node that holds a list of strings (class names, table names, steam ids...)
    /// </summary>
    public class CapareStringList
    {
        public string Title { get; }
        public System.ComponentModel.BindingList<string> List { get; }
        // How new entries are picked when adding
        public CapareStringListSource Source { get; }

        public CapareStringList(string title, System.ComponentModel.BindingList<string> list, CapareStringListSource source)
        {
            Title = title;
            List = list;
            Source = source;
        }
        public override string ToString() => Title;
    }
    public enum CapareStringListSource
    {
        // Free text, one per line
        Text,
        // Class names picked from the economy types
        Types,
        // Reward table names from the loot framework
        RewardTables,
        // Loot tables, item sets and defined items from the loot framework
        RewardEntries,
        // Keycard class names from the keycard config
        KeycardClassNames,
        // Loot box names from the loot box config
        LootBoxNames
    }
}
