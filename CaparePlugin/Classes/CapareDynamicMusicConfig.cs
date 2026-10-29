using Day2eEditor;
using System.ComponentModel;

namespace CaparePlugin
{
    /// <summary>
    /// $profile:Capare/Capare_Dynamic_Music/Capare_Dynamic_Music.json
    /// Capare_Scripts - CapareDymamicMusicConfig.c / dynamicmusicplayerregistrycapare.c
    /// </summary>
    public class CapareDynamicMusicConfig : CapareConfigBase<CapareDynamicMusicSettings>
    {
        public CapareDynamicMusicConfig(string path) : base(path)
        {
        }
        protected override CapareDynamicMusicSettings CreateDefaultData()
        {
            return CapareDynamicMusicSettings.CreateDefault();
        }
        protected override IEnumerable<string> ValidateData()
        {
            return Data.FixMissingOrInvalidFields();
        }
    }

    // Matches DynamicMusicPlayerTimeOfDay in vanilla scripts
    public enum CapareDynamicMusicTimeOfDayType
    {
        ANY = -1,
        NIGHT = 0,
        DAY = 1,
        DUSK = 2,
        DAWN = 3
    }

    public class CapareDynamicMusicSettings : IEquatable<CapareDynamicMusicSettings>, IDeepCloneable<CapareDynamicMusicSettings>
    {
        public float GlobalMinWaitTimeSeconds { get; set; }
        public float GlobalMaxWaitTimeSeconds { get; set; }
        public float MenuMinWaitTimeSeconds { get; set; }
        public float MenuMaxWaitTimeSeconds { get; set; }
        public BindingList<CapareDynamicMusicMenu> m_TracksMenu { get; set; }
        public float TimeMinWaitTimeSeconds { get; set; }
        public float TimeMaxWaitTimeSeconds { get; set; }
        public bool UsedeafultTODTracks { get; set; }
        public BindingList<CapareDynamicMusicTimeOfDay> m_TracksTime { get; set; }
        public float StaticMinWaitTimeSeconds { get; set; }
        public float StaticMaxWaitTimeSeconds { get; set; }
        public float StaticPreviousTrackFadeoutSeconds { get; set; }
        public BindingList<CapareDynamicMusicLocationStatic> m_TracksLocationStatic { get; set; }

        public CapareDynamicMusicSettings()
        {
        }

        // Same values CapareDynamicMusicDefaultConfig() writes in the mod
        public static CapareDynamicMusicSettings CreateDefault()
        {
            return new CapareDynamicMusicSettings()
            {
                GlobalMinWaitTimeSeconds = 3.0f,
                GlobalMaxWaitTimeSeconds = 5.0f,
                MenuMinWaitTimeSeconds = 1.0f,
                MenuMaxWaitTimeSeconds = 3.0f,
                m_TracksMenu = new BindingList<CapareDynamicMusicMenu>()
                {
                    new CapareDynamicMusicMenu("capare_music_menu_set")
                },
                TimeMinWaitTimeSeconds = 480.0f,
                TimeMaxWaitTimeSeconds = 720.0f,
                UsedeafultTODTracks = true,
                m_TracksTime = new BindingList<CapareDynamicMusicTimeOfDay>()
                {
                    new CapareDynamicMusicTimeOfDay("PassageSoundSet", 1),
                    new CapareDynamicMusicTimeOfDay("LonelinessSoundSet", 1),
                    new CapareDynamicMusicTimeOfDay("apocalypseSoundSet", 1),
                    new CapareDynamicMusicTimeOfDay("Spawn1NightSoundSet", 0),
                    new CapareDynamicMusicTimeOfDay("thedistantsunSoundSet", 0),
                    new CapareDynamicMusicTimeOfDay("auroraSoundSet", 0),
                    new CapareDynamicMusicTimeOfDay("LonelySoundSet", 0),
                    new CapareDynamicMusicTimeOfDay("SilenceSoundSet", 3),
                    new CapareDynamicMusicTimeOfDay("auroraSoundSet", 3),
                    new CapareDynamicMusicTimeOfDay("UndertowSoundSet", 2),
                    new CapareDynamicMusicTimeOfDay("TragedySoundSet", 2)
                },
                StaticMinWaitTimeSeconds = 480.0f,
                StaticMaxWaitTimeSeconds = 720.0f,
                StaticPreviousTrackFadeoutSeconds = 15.0f,
                m_TracksLocationStatic = new BindingList<CapareDynamicMusicLocationStatic>()
                {
                    new CapareDynamicMusicLocationStatic("SoftDramaSoundSet", new BindingList<BindingList<Vec3>>()
                    {
                        new BindingList<Vec3>() { new Vec3(10484.0f, 0.0f, 5308.0f), new Vec3(11030.0f, 0.0f, 6250.0f) }
                    }, -1)
                }
            };
        }

        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();
            if (m_TracksMenu == null)
            {
                m_TracksMenu = new BindingList<CapareDynamicMusicMenu>();
                fixes.Add("Initialized m_TracksMenu");
            }
            if (m_TracksTime == null)
            {
                m_TracksTime = new BindingList<CapareDynamicMusicTimeOfDay>();
                fixes.Add("Initialized m_TracksTime");
            }
            if (m_TracksLocationStatic == null)
            {
                m_TracksLocationStatic = new BindingList<CapareDynamicMusicLocationStatic>();
                fixes.Add("Initialized m_TracksLocationStatic");
            }
            foreach (CapareDynamicMusicLocationStatic location in m_TracksLocationStatic)
            {
                if (location.locationBoundaries == null)
                {
                    location.locationBoundaries = new BindingList<BindingList<Vec3>>();
                    fixes.Add($"Initialized locationBoundaries for {location.m_SoundSet}");
                }
                // Each boundary is a rectangle made from 2 opposite corners
                foreach (BindingList<Vec3> rectangle in location.locationBoundaries)
                {
                    while (rectangle.Count < 2)
                    {
                        rectangle.Add(new Vec3(0f, 0f, 0f));
                        fixes.Add($"Added missing rectangle corner for {location.m_SoundSet}");
                    }
                }
            }
            return fixes;
        }

        public bool Equals(CapareDynamicMusicSettings other) => CapareJson.AreEqual(this, other);
        public override bool Equals(object? obj) => Equals(obj as CapareDynamicMusicSettings);
        public override int GetHashCode() => base.GetHashCode();
        public CapareDynamicMusicSettings Clone() => CapareJson.Clone(this);
    }
    public class CapareDynamicMusicMenu
    {
        public string m_SoundSet { get; set; }

        public CapareDynamicMusicMenu() { }
        public CapareDynamicMusicMenu(string soundset)
        {
            m_SoundSet = soundset;
        }
        public override string ToString() => m_SoundSet;
    }
    public class CapareDynamicMusicTimeOfDay
    {
        public string m_SoundSet { get; set; }
        public int m_TimeOfDay { get; set; }

        public CapareDynamicMusicTimeOfDay() { }
        public CapareDynamicMusicTimeOfDay(string soundset, int timeofday)
        {
            m_SoundSet = soundset;
            m_TimeOfDay = timeofday;
        }
        public override string ToString() => $"{m_SoundSet} ({(CapareDynamicMusicTimeOfDayType)m_TimeOfDay})";
    }
    public class CapareDynamicMusicLocationStatic
    {
        public string m_SoundSet { get; set; }
        public int m_TimeOfDay { get; set; }
        public BindingList<BindingList<Vec3>> locationBoundaries { get; set; }
        public bool m_runImmediately { get; set; }

        public CapareDynamicMusicLocationStatic() { }
        public CapareDynamicMusicLocationStatic(string soundset, BindingList<BindingList<Vec3>> boundaries, int timeofday, bool runImmediately = false)
        {
            m_SoundSet = soundset;
            locationBoundaries = boundaries;
            m_TimeOfDay = timeofday;
            m_runImmediately = runImmediately;
        }
        public override string ToString() => $"{m_SoundSet} ({(CapareDynamicMusicTimeOfDayType)m_TimeOfDay})";
    }
}
