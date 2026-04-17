using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2eEditor
{
    public enum WarningSeverity
    {
        Info,
        Warning
    }
    public enum WarningCode
    {
        DuplicateTypeName,
        DuplicateEventName,
        DuplicateSpawnableName,
        DuplicateRandomPresetName,

        MissingEventSpawn,
        MissingEventGroup,
        MissingReferencedType,
        MissingReferencedSpawnable,
        MissingTerritoryFile,
        MissingObjectSpawnerFile,

        InvalidTypeEntry,
        UnknownCategory,
        UnknownTier,
        UnknownUsageFlag,
        UnknownValueFlag,
        UnknownTag,

        UnusedEventGroup,
        UnusedRandomPreset,
        UnusedSpawnableType,
        UnusedTerritoryFile,

        EmptyTypesFile,
        EmptyEventsFile,
        EmptySpawnableTypesFile,
        EmptyRandomPresetsFile,

        OrphanedCeEntry,
        DuplicateCeFileEntry,
        FileRegisteredButMissing,
        FileExistsButNotRegistered
    }

    public class EconomyWarning
    {
        public WarningCode Code { get; set; }
        public WarningSeverity Severity { get; set; } = WarningSeverity.Warning;

        public string Title { get; set; }
        public string Message { get; set; }

        public object SourceObject { get; set; }
        public object RelatedObject { get; set; }

        public string SourceFile { get; set; }
        public string RelatedFile { get; set; }

        public string Key { get; set; }   // stable unique-ish identity for de-duping
        public string Group { get; set; } // e.g. "Missing References", "Unused Content"

        public static string GetWarningGroup(WarningCode code)
        {
            return code switch
            {
                WarningCode.MissingEventSpawn => "Missing References",
                WarningCode.MissingEventGroup => "Missing References",
                WarningCode.MissingReferencedType => "Missing References",
                WarningCode.MissingReferencedSpawnable => "Missing References",
                WarningCode.MissingTerritoryFile => "Missing References",
                WarningCode.MissingObjectSpawnerFile => "Missing References",

                WarningCode.DuplicateTypeName => "Duplicates",
                WarningCode.DuplicateEventName => "Duplicates",
                WarningCode.DuplicateSpawnableName => "Duplicates",
                WarningCode.DuplicateRandomPresetName => "Duplicates",
                WarningCode.DuplicateCeFileEntry => "Duplicates",

                WarningCode.UnusedEventGroup => "Unused Content",
                WarningCode.UnusedRandomPreset => "Unused Content",
                WarningCode.UnusedSpawnableType => "Unused Content",
                WarningCode.UnusedTerritoryFile => "Unused Content",

                WarningCode.EmptyTypesFile => "Empty Files",
                WarningCode.EmptyEventsFile => "Empty Files",
                WarningCode.EmptySpawnableTypesFile => "Empty Files",
                WarningCode.EmptyRandomPresetsFile => "Empty Files",

                WarningCode.OrphanedCeEntry => "Registration Issues",
                WarningCode.FileRegisteredButMissing => "Registration Issues",
                WarningCode.FileExistsButNotRegistered => "Registration Issues",

                WarningCode.InvalidTypeEntry => "Type Validation",
                WarningCode.UnknownCategory => "Type Validation",
                WarningCode.UnknownTier => "Type Validation",
                WarningCode.UnknownUsageFlag => "Type Validation",
                WarningCode.UnknownValueFlag => "Type Validation",
                WarningCode.UnknownTag => "Type Validation",


                _ => "Other"
            };
        }
    }
}
