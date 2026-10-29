namespace Day2eEditor
{
    // The category, tag, usage and value names declared in cfglimitsdefinition.xml and
    // cfglimitsdefinitionuser.xml. Types and map group prototypes may only use these.
    public sealed class DefinedFlags
    {
        public const string DefinitionFile = "cfglimitsdefinition.xml";
        public const string UserDefinitionFile = "cfglimitsdefinitionuser.xml";

        public HashSet<string> Categories { get; } = new(StringComparer.Ordinal);
        public HashSet<string> Tags { get; } = new(StringComparer.Ordinal);
        public HashSet<string> Usages { get; } = new(StringComparer.Ordinal);
        public HashSet<string> Values { get; } = new(StringComparer.Ordinal);
        public HashSet<string> UserUsages { get; } = new(StringComparer.Ordinal);
        public HashSet<string> UserValues { get; } = new(StringComparer.Ordinal);

        // Returns null when either definition file failed to load: checking against an
        // incomplete list would strip flags that are actually valid.
        public static DefinedFlags? From(cfglimitsdefinitionConfig? definitions, cfglimitsdefinitionuserConfig? userDefinitions)
        {
            if (definitions?.Data == null || definitions.HasErrors ||
                userDefinitions?.Data == null || userDefinitions.HasErrors)
                return null;

            var flags = new DefinedFlags();
            AddNames(flags.Categories, definitions.Data.categories?.Select(x => x.name));
            AddNames(flags.Tags, definitions.Data.tags?.Select(x => x.name));
            AddNames(flags.Usages, definitions.Data.usageflags?.Select(x => x.name));
            AddNames(flags.Values, definitions.Data.valueflags?.Select(x => x.name));
            AddNames(flags.UserUsages, userDefinitions.Data.usageflags?.Select(x => x.name));
            AddNames(flags.UserValues, userDefinitions.Data.valueflags?.Select(x => x.name));
            return flags;
        }

        private static void AddNames(HashSet<string> set, IEnumerable<string?>? names)
        {
            if (names == null)
                return;

            foreach (var name in names)
            {
                if (!string.IsNullOrWhiteSpace(name))
                    set.Add(name);
            }
        }
    }
}
