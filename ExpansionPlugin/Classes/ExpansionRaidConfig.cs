using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionRaidConfig : IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path); // e.g., "types.xml"
        public string FilePath => _path;
        public ExpansionRaidSettings Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }
        public const int CurrentVersion = 5;

        public ExpansionRaidConfig(string path)
        {
            _path = path;
        }
        public void Load()
        {
            Data = null;
            Data = AppServices.GetRequired<FileService>().LoadOrCreateJson<ExpansionRaidSettings>(
                _path,
                createNew: () => new ExpansionRaidSettings(CurrentVersion),
                onAfterLoad: cfg => { },
                onError: ex =>
                {
                    HasErrors = true;
                    Console.WriteLine(
                        "Error in " + Path.GetFileName(_path) + "\n" +
                        ex.Message + "\n" +
                        ex.InnerException?.Message + "\n"
                    );
                    Errors.Add("Error in " + Path.GetFileName(_path) + "\n" +
                        ex.Message + "\n" +
                        ex.InnerException?.Message);
                },
                configName: "ExpansionRaid"
            );
            var missingFields = Data.FixMissingOrInvalidFields();
            if (missingFields.Any())
            {
                Console.WriteLine("Validation issues in " + FileName + ":");
                foreach (var issue in missingFields)
                {
                    Console.WriteLine("- " + issue);
                }
                isDirty = true;
            }
        }
        public IEnumerable<string> Save()
        {
            if (isDirty)
            {
                AppServices.GetRequired<FileService>().SaveJson(_path, Data);
                isDirty = false;
                return new[] { Path.GetFileName(_path) };
            }

            return Array.Empty<string>();
        }
        public bool needToSave()
        {
            return isDirty;
        }
    }
    public enum ExpansionBaseBuildingRaidMode
    {
        ExpansionBaseBuildingElementscantberaided = -1,
        All = 0,
        DoorsGates,
        DoorsGatesWindows,
        DoorsGatesWindowsWalls,
    };
    public enum RaidLocksOnWallsEnum
    {
        Disabled = 0,
        Enabled,
        OnlyDoor,
        OnlyGate
    };
    public class ExpansionRaidSettings
    {
        public int m_Version { get; set; }
        public ExpansionBaseBuildingRaidMode? BaseBuildingRaidMode { get; set; }         //! 0 = Default, everything can take dmg | 1 = doors and gates | 2 = doors, gates and windows | 3 = any wall/fence
        public decimal? ExplosionTime { get; set; }                                //! Ammount of time it takes for explosive to explode.
        public BindingList<string> ExplosiveDamageWhitelist { get; set; }		//! List of damage sources allowed to damage bases when whitelist is enabled. 
	    public int? EnableExplosiveWhitelist { get; set; }                      //! If enabled, only damage sources listed in ExplosiveDamageWhitelist will be able to damage walls. 
        public decimal? ExplosionDamageMultiplier { get; set; }                    //! Damage multiplier from explosion.
        public decimal? ProjectileDamageMultiplier { get; set; }                   //! Damage multiplier from projectiles.

        public int? CanRaidSafes { get; set; }                                  //! If enabled, make safes raidable
        public int? SafeRaidUseSchedule { get; set; }

        public decimal? SafeExplosionDamageMultiplier { get; set; }                //! Damage multiplier from explosion on safes.
        public decimal? SafeProjectileDamageMultiplier { get; set; }               //! Damage multiplier from explosion on safes.

        public BindingList<string> SafeRaidTools { get; set; }					//! List of tools allowed for raiding safes
	    public int? SafeRaidToolTimeSeconds { get; set; }                        //! Time needed to raid safe with tool
        public int? SafeRaidToolCycles { get; set; }                             //! Number of cycles needed to raid safe
        public decimal? SafeRaidToolDamagePercent { get; set; }                    //! Total damage dealt to tool over time (100 = tool will be in ruined state after all cycles finished)

        public int? CanRaidBarbedWire { get; set; }                             //! If enabled, make barbed wire raidable

        public BindingList<string> BarbedWireRaidTools { get; set; }			//! List of tools allowed for raiding barbed wire
	    public int? BarbedWireRaidToolTimeSeconds { get; set; }                  //! Time needed to raid barbed wire with tool
        public int? BarbedWireRaidToolCycles { get; set; }                       //! Number of cycles needed to raid barbed wire
        public decimal? BarbedWireRaidToolDamagePercent { get; set; }              //! Total damage dealt to tool over time (100 = tool will be in ruined state after all cycles finished)

        public RaidLocksOnWallsEnum? CanRaidLocksOnWalls { get; set; }           //! If set to 1 make locks (both vanilla and Expansion) raidable on walls | 2 = only doors | 3 = only gates
        public int? CanRaidLocksOnFences { get; set; }                          //! If enabled, make locks (both vanilla and Expansion) raidable on fences
        public int? CanRaidLocksOnTents { get; set; }                           //! If enabled, make locks (both vanilla and Expansion) raidable on tents

        public BindingList<string> LockRaidTools { get; set; }					//! List of tools allowed for raiding locks
	    public int? LockOnWallRaidToolTimeSeconds { get; set; }                  //! Time needed to raid lock on wall with tool. Disabled <= 0
        public int? LockOnFenceRaidToolTimeSeconds { get; set; }                 //! Time needed to raid lock on fence with tool. Disabled <= 0
        public int? LockOnTentRaidToolTimeSeconds { get; set; }                  //! Time needed to raid lock on tent with tool. Disabled <= 0
        public int? LockRaidToolCycles { get; set; }                             //! Number of cycles needed to raid lock
        public decimal? LockRaidToolDamagePercent { get; set; }                    //! Total damage dealt to tool over time (100 = tool will be in ruined state after all cycles finished)

        public int? CanRaidLocksOnContainers { get; set; }                          //! If enabled, makes code locked containers raidable
        public int? LockOnContainerRaidUseSchedule { get; set; }

        public BindingList<string> LockOnContainerRaidTools { get; set; }			//! List of tools allowed for raiding locks on containers
	    public int? LockOnContainerRaidToolTimeSeconds { get; set; }                 //! Time needed to raid lock on container with tool
        public int? LockOnContainerRaidToolCycles { get; set; }                      //! Number of cycles needed to raid lock on container
        public decimal? LockOnContainerRaidToolDamagePercent { get; set; }             //! Total damage dealt to tool over time (100 = tool will be in ruined state after all cycles finished)

        public BindingList< ExpansionRaidSchedule> Schedule { get; set; }
        
        
        public ExpansionRaidSettings() { }
        public ExpansionRaidSettings(int CurrentVersion)
        {
            m_Version = CurrentVersion;
            ExplosionTime = 30;
            ExplosiveDamageWhitelist = new BindingList<string>() {
                "Expansion_C4_Explosion",
                "Expansion_RPG_Explosion",
                "Expansion_LAW_Explosion",
                "M79",
                "RGD5Grenade",
                "M67Grenade",
                "FlashGrenade",
                "Land_FuelStation_Feed",
                "Land_FuelStation_Feed_Enoch"
            };

            EnableExplosiveWhitelist = 0;
            ExplosionDamageMultiplier = 50;
            ProjectileDamageMultiplier = 1;

            CanRaidSafes = 1;
            SafeRaidUseSchedule = 1;
            CanRaidLocksOnContainers = 1;
            LockOnContainerRaidUseSchedule = 1;
            SafeExplosionDamageMultiplier = 17;
            SafeProjectileDamageMultiplier = 1;

            LockOnContainerRaidTools = new BindingList<string>() { "ExpansionPropaneTorch" };
            LockOnContainerRaidToolTimeSeconds = 10 * 60;
            LockOnContainerRaidToolCycles = 5;
            LockOnContainerRaidToolDamagePercent = 100;

            SafeRaidTools = new BindingList<string>() { "ExpansionPropaneTorch" };
            SafeRaidToolTimeSeconds = 10 * 60;
            SafeRaidToolCycles = 5;
            SafeRaidToolDamagePercent = 100;

            CanRaidBarbedWire = 1;

            BarbedWireRaidTools = new BindingList<string>() { "ExpansionBoltCutters" };
            BarbedWireRaidToolTimeSeconds = 5 * 60;
            BarbedWireRaidToolCycles = 5;
            BarbedWireRaidToolDamagePercent = 100;

            CanRaidLocksOnWalls = RaidLocksOnWallsEnum.Disabled;
            CanRaidLocksOnFences = 0;
            CanRaidLocksOnTents = 0;

            LockOnWallRaidToolTimeSeconds = 15 * 60;
            LockOnFenceRaidToolTimeSeconds = 15 * 60;
            LockOnTentRaidToolTimeSeconds = 10 * 60;
            LockRaidToolCycles = 5;
            LockRaidToolDamagePercent = 100;

            BaseBuildingRaidMode = ExpansionBaseBuildingRaidMode.All;
            DefaultRaidSchedule();
        }
        private void DefaultRaidSchedule()
        {
            Schedule = new BindingList<ExpansionRaidSchedule>()
            {
                new ExpansionRaidSchedule() { Weekday = "Sunday", StartHour = 0, StartMinute = 0, DurationMinutes = 1440 },
                new ExpansionRaidSchedule() { Weekday = "Monday", StartHour = 0, StartMinute = 0, DurationMinutes = 1440 },
                new ExpansionRaidSchedule() { Weekday = "Tuesday", StartHour = 0, StartMinute = 0, DurationMinutes = 1440 },
                new ExpansionRaidSchedule() { Weekday = "Wednesday", StartHour = 0, StartMinute = 0, DurationMinutes = 1440 },
                new ExpansionRaidSchedule() { Weekday = "Thursday", StartHour = 0, StartMinute = 0, DurationMinutes = 1440 },
                new ExpansionRaidSchedule() { Weekday = "friday", StartHour = 0, StartMinute = 0, DurationMinutes = 1440 },
                new ExpansionRaidSchedule() { Weekday = "Saturday", StartHour = 0, StartMinute = 0, DurationMinutes = 1440 }
            };
        }
        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            if (m_Version != ExpansionRaidConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionRaidConfig.CurrentVersion}");
                m_Version = ExpansionRaidConfig.CurrentVersion;
            }
            if (BaseBuildingRaidMode is null ||!Enum.IsDefined(typeof(ExpansionBaseBuildingRaidMode), BaseBuildingRaidMode))
            {
                BaseBuildingRaidMode = ExpansionBaseBuildingRaidMode.All;
                fixes.Add("Corrected BaseBuildingRaidMode");
            }
            if (ExplosionTime is null || ExplosionTime < 0)
            {
                ExplosionTime = 30;
                fixes.Add("Corrected ExplosionTime");
            }
            if (ExplosiveDamageWhitelist == null)
            {
                ExplosiveDamageWhitelist = new BindingList<string>
                {
                    "Expansion_C4_Explosion",
                    "Expansion_RPG_Explosion",
                    "Expansion_LAW_Explosion",
                    "M79",
                    "RGD5Grenade",
                    "M67Grenade",
                    "FlashGrenade",
                    "Land_FuelStation_Feed",
                    "Land_FuelStation_Feed_Enoch"
                };
                fixes.Add("Restored ExplosiveDamageWhitelist");
            }
            if (EnableExplosiveWhitelist is null || EnableExplosiveWhitelist < 0 || EnableExplosiveWhitelist > 1)
            {
                EnableExplosiveWhitelist = 0;
                fixes.Add("Corrected EnableExplosiveWhitelist");
            }
            if (ExplosionDamageMultiplier is null)
            {
                ExplosionDamageMultiplier = 50;
                fixes.Add("Corrected ExplosionDamageMultiplier");
            }
            if (ProjectileDamageMultiplier is null)
            {
                ProjectileDamageMultiplier = 1;
                fixes.Add("Corrected ProjectileDamageMultiplier");
            }
            if (CanRaidSafes is null || CanRaidSafes < 0 || CanRaidSafes > 1)
            {
                CanRaidSafes = 1;
                fixes.Add("Corrected CanRaidSafes");
            }
            if (SafeRaidUseSchedule is null || SafeRaidUseSchedule < 0 || SafeRaidUseSchedule > 1)
            {
                SafeRaidUseSchedule = 1;
                fixes.Add("Corrected SafeRaidUseSchedule");
            }
            if (SafeExplosionDamageMultiplier is null)
            {
                SafeExplosionDamageMultiplier = 17;
                fixes.Add("Corrected SafeExplosionDamageMultiplier");
            }
            if (SafeProjectileDamageMultiplier is null)
            {
                SafeProjectileDamageMultiplier = 1;
                fixes.Add("Corrected SafeProjectileDamageMultiplier");
            }
            if (SafeRaidTools == null)
            {
                SafeRaidTools = new BindingList<string> { "ExpansionPropaneTorch" };
                fixes.Add("Restored SafeRaidTools");
            }
            if (SafeRaidToolTimeSeconds is null )
            {
                SafeRaidToolTimeSeconds = 10 * 60;
                fixes.Add("Corrected SafeRaidToolTimeSeconds");
            }
            if (SafeRaidToolCycles is null)
            {
                SafeRaidToolCycles = 5;
                fixes.Add("Corrected SafeRaidToolCycles");
            }
            if (SafeRaidToolDamagePercent is null || SafeRaidToolDamagePercent < 0 || SafeRaidToolDamagePercent > 100)
            {
                SafeRaidToolDamagePercent = 100;
                fixes.Add("Corrected SafeRaidToolDamagePercent");
            }
            if (CanRaidLocksOnContainers is null || CanRaidLocksOnContainers < 0 || CanRaidLocksOnContainers > 1)
            {
                CanRaidLocksOnContainers = 1;
                fixes.Add("Corrected CanRaidLocksOnContainers");
            }
            if (LockOnContainerRaidUseSchedule is null || LockOnContainerRaidUseSchedule < 0 || LockOnContainerRaidUseSchedule > 1)
            {
                LockOnContainerRaidUseSchedule = 1;
                fixes.Add("Corrected LockOnContainerRaidUseSchedule");
            }
            if (LockOnContainerRaidTools == null )
            {
                LockOnContainerRaidTools = new BindingList<string> { "ExpansionPropaneTorch" };
                fixes.Add("Restored LockOnContainerRaidTools");
            }
            if (LockOnContainerRaidToolTimeSeconds is null)
            {
                LockOnContainerRaidToolTimeSeconds = 10 * 60;
                fixes.Add("Corrected LockOnContainerRaidToolTimeSeconds");
            }
            if (LockOnContainerRaidToolCycles is null)
            {
                LockOnContainerRaidToolCycles = 5;
                fixes.Add("Corrected LockOnContainerRaidToolCycles");
            }
            if (LockOnContainerRaidToolDamagePercent is null || LockOnContainerRaidToolDamagePercent < 0 || LockOnContainerRaidToolDamagePercent > 100)
            {
                LockOnContainerRaidToolDamagePercent = 100;
                fixes.Add("Corrected LockOnContainerRaidToolDamagePercent");
            }
            if (CanRaidBarbedWire is null || CanRaidBarbedWire < 0 || CanRaidBarbedWire > 1)
            {
                CanRaidBarbedWire = 1;
                fixes.Add("Corrected CanRaidBarbedWire");
            }
            if (BarbedWireRaidTools == null )
            {
                BarbedWireRaidTools = new BindingList<string> { "ExpansionBoltCutters" };
                fixes.Add("Restored BarbedWireRaidTools");
            }
            if (BarbedWireRaidToolTimeSeconds is null)
            {
                BarbedWireRaidToolTimeSeconds = 5 * 60;
                fixes.Add("Corrected BarbedWireRaidToolTimeSeconds");
            }
            if (BarbedWireRaidToolCycles is null)
            {
                BarbedWireRaidToolCycles = 5;
                fixes.Add("Corrected BarbedWireRaidToolCycles");
            }
            if (BarbedWireRaidToolDamagePercent is null || BarbedWireRaidToolDamagePercent < 0 || BarbedWireRaidToolDamagePercent > 100)
            {
                BarbedWireRaidToolDamagePercent = 100;
                fixes.Add("Corrected BarbedWireRaidToolDamagePercent");
            }
            if (CanRaidLocksOnWalls is null || !Enum.IsDefined(typeof(RaidLocksOnWallsEnum), CanRaidLocksOnWalls))
            {
                CanRaidLocksOnWalls = RaidLocksOnWallsEnum.Disabled;
                fixes.Add("Corrected CanRaidLocksOnWalls");
            }
            if (CanRaidLocksOnFences is null || CanRaidLocksOnFences < 0 || CanRaidLocksOnFences > 1)
            {
                CanRaidLocksOnFences = 0;
                fixes.Add("Corrected CanRaidLocksOnFences");
            }
            if (CanRaidLocksOnTents is null || CanRaidLocksOnTents < 0 || CanRaidLocksOnTents > 1)
            {
                CanRaidLocksOnTents = 0;
                fixes.Add("Corrected CanRaidLocksOnTents");
            }
            if (LockOnWallRaidToolTimeSeconds is null )
            {
                LockOnWallRaidToolTimeSeconds = 15 * 60;
                fixes.Add("Corrected LockOnWallRaidToolTimeSeconds");
            }
            if (LockOnFenceRaidToolTimeSeconds is null )
            {
                LockOnFenceRaidToolTimeSeconds = 15 * 60;
                fixes.Add("Corrected LockOnFenceRaidToolTimeSeconds");
            }
            if (LockOnTentRaidToolTimeSeconds is null )
            {
                LockOnTentRaidToolTimeSeconds = 10 * 60;
                fixes.Add("Corrected LockOnTentRaidToolTimeSeconds");
            }
            if (LockRaidToolCycles is null )
            {
                LockRaidToolCycles = 5;
                fixes.Add("Corrected LockRaidToolCycles");
            }
            if (LockRaidToolDamagePercent is null || LockRaidToolDamagePercent < 0 || LockRaidToolDamagePercent > 100)
            {
                LockRaidToolDamagePercent = 100;
                fixes.Add("Corrected LockRaidToolDamagePercent");
            }
            if (Schedule == null)
            {
                DefaultRaidSchedule();
                fixes.Add("Default Raid Schedule generated");
            }
            foreach(ExpansionRaidSchedule rs in Schedule)
            {
                List<string> issues = rs.Validate();
                if (issues.Count > 0)
                {
                    fixes.AddRange(issues);
                }
            }
            return fixes;
        }
        public override bool Equals(object obj)
        {

            if (obj is not ExpansionRaidSettings other)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (m_Version != other.m_Version) return false;
            if (BaseBuildingRaidMode != other.BaseBuildingRaidMode) return false;
            if (ExplosionTime != other.ExplosionTime) return false;
            if (EnableExplosiveWhitelist != other.EnableExplosiveWhitelist) return false;
            if (ExplosionDamageMultiplier != other.ExplosionDamageMultiplier) return false;
            if (ProjectileDamageMultiplier != other.ProjectileDamageMultiplier) return false;

            if (CanRaidSafes != other.CanRaidSafes) return false;
            if (SafeRaidUseSchedule != other.SafeRaidUseSchedule) return false;
            if (SafeExplosionDamageMultiplier != other.SafeExplosionDamageMultiplier) return false;
            if (SafeProjectileDamageMultiplier != other.SafeProjectileDamageMultiplier) return false;

            if (SafeRaidToolTimeSeconds != other.SafeRaidToolTimeSeconds) return false;
            if (SafeRaidToolCycles != other.SafeRaidToolCycles) return false;
            if (SafeRaidToolDamagePercent != other.SafeRaidToolDamagePercent) return false;

            if (CanRaidBarbedWire != other.CanRaidBarbedWire) return false;

            if (BarbedWireRaidToolTimeSeconds != other.BarbedWireRaidToolTimeSeconds) return false;
            if (BarbedWireRaidToolCycles != other.BarbedWireRaidToolCycles) return false;
            if (BarbedWireRaidToolDamagePercent != other.BarbedWireRaidToolDamagePercent) return false;

            if (CanRaidLocksOnWalls != other.CanRaidLocksOnWalls) return false;
            if (CanRaidLocksOnFences != other.CanRaidLocksOnFences) return false;
            if (CanRaidLocksOnTents != other.CanRaidLocksOnTents) return false;

            if (LockOnWallRaidToolTimeSeconds != other.LockOnWallRaidToolTimeSeconds) return false;
            if (LockOnFenceRaidToolTimeSeconds != other.LockOnFenceRaidToolTimeSeconds) return false;
            if (LockOnTentRaidToolTimeSeconds != other.LockOnTentRaidToolTimeSeconds) return false;
            if (LockRaidToolCycles != other.LockRaidToolCycles) return false;
            if (LockRaidToolDamagePercent != other.LockRaidToolDamagePercent) return false;

            if (CanRaidLocksOnContainers != other.CanRaidLocksOnContainers) return false;
            if (LockOnContainerRaidUseSchedule != other.LockOnContainerRaidUseSchedule) return false;

            if (LockOnContainerRaidToolTimeSeconds != other.LockOnContainerRaidToolTimeSeconds) return false;
            if (LockOnContainerRaidToolCycles != other.LockOnContainerRaidToolCycles) return false;
            if (LockOnContainerRaidToolDamagePercent != other.LockOnContainerRaidToolDamagePercent) return false;

            if (!ExplosiveDamageWhitelist.SequenceEqual(other.ExplosiveDamageWhitelist))
                return false;

            if (!SafeRaidTools.SequenceEqual(other.SafeRaidTools))
                return false;

            if (!BarbedWireRaidTools.SequenceEqual(other.BarbedWireRaidTools))
                return false;

            if (!LockRaidTools.SequenceEqual(other.LockRaidTools))
                return false;

            if (!LockOnContainerRaidTools.SequenceEqual(other.LockOnContainerRaidTools))
                return false;

            if (Schedule == null && other.Schedule == null)
                return true;

            if (Schedule == null || other.Schedule == null)
                return false;

            if (Schedule.Count != other.Schedule.Count)
                return false;

            for (int i = 0; i < Schedule.Count; i++)
            {
                if (!Schedule[i].Equals(other.Schedule[i]))
                    return false;
            }

            return true;

        }
        public ExpansionRaidSettings Clone()
        {

            return new ExpansionRaidSettings
            {
                m_Version = this.m_Version,
                BaseBuildingRaidMode = this.BaseBuildingRaidMode,
                ExplosionTime = this.ExplosionTime,

                ExplosiveDamageWhitelist =
                        new BindingList<string>(this.ExplosiveDamageWhitelist.ToList()),

                EnableExplosiveWhitelist = this.EnableExplosiveWhitelist,
                ExplosionDamageMultiplier = this.ExplosionDamageMultiplier,
                ProjectileDamageMultiplier = this.ProjectileDamageMultiplier,

                CanRaidSafes = this.CanRaidSafes,
                SafeRaidUseSchedule = this.SafeRaidUseSchedule,

                SafeExplosionDamageMultiplier = this.SafeExplosionDamageMultiplier,
                SafeProjectileDamageMultiplier = this.SafeProjectileDamageMultiplier,

                SafeRaidTools =
                        new BindingList<string>(this.SafeRaidTools.ToList()),

                SafeRaidToolTimeSeconds = this.SafeRaidToolTimeSeconds,
                SafeRaidToolCycles = this.SafeRaidToolCycles,
                SafeRaidToolDamagePercent = this.SafeRaidToolDamagePercent,

                CanRaidBarbedWire = this.CanRaidBarbedWire,

                BarbedWireRaidTools =
                        new BindingList<string>(this.BarbedWireRaidTools.ToList()),

                BarbedWireRaidToolTimeSeconds = this.BarbedWireRaidToolTimeSeconds,
                BarbedWireRaidToolCycles = this.BarbedWireRaidToolCycles,
                BarbedWireRaidToolDamagePercent = this.BarbedWireRaidToolDamagePercent,

                CanRaidLocksOnWalls = this.CanRaidLocksOnWalls,
                CanRaidLocksOnFences = this.CanRaidLocksOnFences,
                CanRaidLocksOnTents = this.CanRaidLocksOnTents,

                LockRaidTools =
                        new BindingList<string>(this.LockRaidTools.ToList()),

                LockOnWallRaidToolTimeSeconds = this.LockOnWallRaidToolTimeSeconds,
                LockOnFenceRaidToolTimeSeconds = this.LockOnFenceRaidToolTimeSeconds,
                LockOnTentRaidToolTimeSeconds = this.LockOnTentRaidToolTimeSeconds,
                LockRaidToolCycles = this.LockRaidToolCycles,
                LockRaidToolDamagePercent = this.LockRaidToolDamagePercent,

                CanRaidLocksOnContainers = this.CanRaidLocksOnContainers,
                LockOnContainerRaidUseSchedule = this.LockOnContainerRaidUseSchedule,

                LockOnContainerRaidTools =
                        new BindingList<string>(this.LockOnContainerRaidTools.ToList()),

                LockOnContainerRaidToolTimeSeconds = this.LockOnContainerRaidToolTimeSeconds,
                LockOnContainerRaidToolCycles = this.LockOnContainerRaidToolCycles,
                LockOnContainerRaidToolDamagePercent = this.LockOnContainerRaidToolDamagePercent,

                Schedule =
                        new BindingList<ExpansionRaidSchedule>(
                            this.Schedule.Select(x => x.Clone()).ToList())
            };

        }

    }
    public class ExpansionRaidSchedule
    {
        [JsonIgnore]
        private static readonly string[] WEEKDAYS = { "SUNDAY", "MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY", "FRIDAY", "SATURDAY" };


        public string? Weekday { get; set; }
        public int? StartHour { get; set; }
        public int? StartMinute { get; set; }
        public int? DurationMinutes { get; set; }

        public ExpansionRaidSchedule() { }

        public List<string> Validate()
        {
            var issues = new List<string>();

            int weekdayIndex = -1;  //! Everyday
            if (!string.IsNullOrWhiteSpace(Weekday))
            {
                var weekdayUpper = Weekday!.Trim().ToUpperInvariant();
                weekdayIndex = Array.IndexOf(WEEKDAYS, weekdayUpper);
                if (weekdayIndex == -1)
                {
                    issues.Add(
                        $"Invalid Weekday '{Weekday}'. Expected one of: {string.Join(", ", WEEKDAYS)} " +
                        $"or leave empty/null for 'everyday'.");
                }
            }

            if (StartHour is null)
            {
                issues.Add("StartHour is missing.");
            }
            else if (StartHour < 0 || StartHour > 23)
            {
                issues.Add($"Invalid StartHour '{StartHour}'. Expected range: 0–23.");
            }

            if (StartMinute is null)
            {
                issues.Add("StartMinute is missing.");
            }
            else if (StartMinute < 0 || StartMinute > 59)
            {
                issues.Add($"Invalid StartMinute '{StartMinute}'. Expected range: 0–59.");
            }

            if (DurationMinutes is null)
            {
                issues.Add("DurationMinutes is missing.");
            }
            else if (DurationMinutes <= 0)
            {
                issues.Add($"Invalid DurationMinutes '{DurationMinutes}'. Expected > 0.");
            }

            if (StartHour is >= 0 and <= 23 &&
                StartMinute is >= 0 and <= 59 &&
                DurationMinutes is > 0)
            {
                var endTotalMinutes = StartHour * 60 + StartMinute + DurationMinutes;

                if (endTotalMinutes > 24 * 60)
                {
                    var weekdayLabel = string.IsNullOrWhiteSpace(Weekday)
                        ? "everyday"
                        : Weekday!.Trim();

                    issues.Add(
                        $"Invalid duration {DurationMinutes} for start time " +
                        $"{StartHour:D2}:{StartMinute:D2} on {weekdayLabel}. " +
                        "The schedule must not run past 24:00.");
                }
            }

            return issues;

        }
        public override bool Equals(object obj)
        {
            if (obj is not ExpansionRaidSchedule other)
                return false;

            return Weekday == other.Weekday &&
                StartHour == other.StartHour &&
                StartMinute == other.StartMinute &&
                DurationMinutes == other.DurationMinutes;
        }
        public ExpansionRaidSchedule Clone()
        {
            return new ExpansionRaidSchedule()
            {
                Weekday = this.Weekday,
                StartHour = this.StartHour,
                StartMinute = this.StartMinute,
                DurationMinutes = this.DurationMinutes
            };
        }
        public override string ToString()
        {
            string weekdayLabel = string.IsNullOrWhiteSpace(Weekday)
                ? "Everyday"
                : Weekday.Trim();

            string start =
                (StartHour is null || StartMinute is null)
                ? "??:??"
                : $"{StartHour:00}:{StartMinute:00}";

            string duration =
                DurationMinutes is null
                ? "unknown duration"
                : $"{DurationMinutes} min";

            return $"{weekdayLabel} @ {start} for {duration}";
        }
    }
}
