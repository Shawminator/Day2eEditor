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
    public enum MapMarkerVisibility
    {
        [Description("Not visable")]
        Not_visible = 0,
        [Description("Visible on the World")]
        Visible_on_the_World = 2,                  // (f "m_Is3D" is set to 1, you should probably put "m_Visibility" to 2).,
        [Description("Visible on the Map only")]
        Visible_on_the_Map_only = 4,
        [Description("Visible on the Map and World")]
        Visible_on_the_Map_and_on_the_World = 6        // (If \"m_Is3D\" is set to 1, you should probably put \"m_Visibility\" to 2)."
    };
    public class ExpansionMapConfig : IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path); // e.g., "types.xml"
        public string FilePath => _path;
        public ExpansionMapSettings Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }
        public const int CurrentVersion = 5;
 
        public ExpansionMapConfig(string path)
        {
            _path = path;
        }
        public void Load()
        {
            Data = null;
            Data = AppServices.GetRequired<FileService>().LoadOrCreateJson<ExpansionMapSettings>(
                _path,
                createNew: () => new ExpansionMapSettings(CurrentVersion),
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
                configName: "ExpansionMapSettings"
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
    public class ExpansionMapSettings
    {
        public int m_Version { get; set; } //current version 4
        public int? EnableMap { get; set; }
        public int? UseMapOnMapItem { get; set; }
        public int? ShowPlayerPosition { get; set; }
        public int? ShowMapStats { get; set; }
        public int? NeedPenItemForCreateMarker { get; set; }
        public int? NeedGPSItemForCreateMarker { get; set; }
        public int? CanCreateMarker { get; set; }
        public int? CanCreate3DMarker { get; set; }
        public int? CanOpenMapWithKeyBinding { get; set; }
        public int? ShowDistanceOnPersonalMarkers { get; set; }
        public int? EnableHUDGPS { get; set; }
        public int? NeedGPSItemForKeyBinding { get; set; }
        public int? NeedMapItemForKeyBinding { get; set; }
        public int? EnableServerMarkers { get; set; }
        public int? ShowNameOnServerMarkers { get; set; }
        public int? ShowDistanceOnServerMarkers { get; set; }
        public BindingList<ExpansionServerMarkerData> ServerMarkers { get; set; }
        public int? EnableHUDCompass { get; set; }
        public int? NeedCompassItemForHUDCompass { get; set; }
        public int? NeedGPSItemForHUDCompass { get; set; }
        public int? CompassColor { get; set; }
        public int? CreateDeathMarker { get; set; }
        public int? PlayerLocationNotifier { get; set; }
        public int? CompassBadgesColor { get; set; }

        public ExpansionMapSettings() { }
        public ExpansionMapSettings(int CurrentVersion)
        {
            m_Version = CurrentVersion;
            EnableMap = 1;
            UseMapOnMapItem = 1;
            NeedPenItemForCreateMarker = 0;
            NeedGPSItemForCreateMarker = 0;
            ShowPlayerPosition = 1;
            ShowMapStats = 1;
            CanCreateMarker = 1;
            CanCreate3DMarker = 1;
            ShowDistanceOnPersonalMarkers = 1;

            EnableServerMarkers = 1;

            CanOpenMapWithKeyBinding = 1;

            ShowNameOnServerMarkers = 1;
            ShowDistanceOnServerMarkers = 1;

            EnableHUDGPS = 1;

            NeedGPSItemForKeyBinding = 1;
            NeedMapItemForKeyBinding = 0;

            EnableHUDCompass = 1;
            NeedCompassItemForHUDCompass = 1;
            NeedGPSItemForHUDCompass = 1;
            CompassColor = -1;
            CreateDeathMarker = 1;

            PlayerLocationNotifier = 1;

            CompassBadgesColor = -1946157056;

            ServerMarkers = new BindingList<ExpansionServerMarkerData>();
        }
        public override bool Equals(object obj)
        {
            if (obj is not ExpansionMapSettings other)
                return false;

            return m_Version == other.m_Version &&
                   EnableMap == other.EnableMap &&
                   UseMapOnMapItem == other.UseMapOnMapItem &&
                   ShowPlayerPosition == other.ShowPlayerPosition &&
                   ShowMapStats == other.ShowMapStats &&
                   NeedPenItemForCreateMarker == other.NeedPenItemForCreateMarker &&
                   NeedGPSItemForCreateMarker == other.NeedGPSItemForCreateMarker &&
                   CanCreateMarker == other.CanCreateMarker &&
                   CanCreate3DMarker == other.CanCreate3DMarker &&
                   CanOpenMapWithKeyBinding == other.CanOpenMapWithKeyBinding &&
                   ShowDistanceOnPersonalMarkers == other.ShowDistanceOnPersonalMarkers &&
                   EnableHUDGPS == other.EnableHUDGPS &&
                   NeedGPSItemForKeyBinding == other.NeedGPSItemForKeyBinding &&
                   NeedMapItemForKeyBinding == other.NeedMapItemForKeyBinding &&
                   EnableServerMarkers == other.EnableServerMarkers &&
                   ShowNameOnServerMarkers == other.ShowNameOnServerMarkers &&
                   ShowDistanceOnServerMarkers == other.ShowDistanceOnServerMarkers &&
                   ServerMarkers.SequenceEqual(other.ServerMarkers) &&
                   EnableHUDCompass == other.EnableHUDCompass &&
                   NeedCompassItemForHUDCompass == other.NeedCompassItemForHUDCompass &&
                   NeedGPSItemForHUDCompass == other.NeedGPSItemForHUDCompass &&
                   CompassColor == other.CompassColor &&
                   CreateDeathMarker == other.CreateDeathMarker &&
                   PlayerLocationNotifier == other.PlayerLocationNotifier &&
                   CompassBadgesColor == other.CompassBadgesColor;
        }
        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            if (m_Version < ExpansionGeneralConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionGeneralConfig.CurrentVersion}");
                m_Version = ExpansionGeneralConfig.CurrentVersion;
            }
            if (EnableMap == null || (EnableMap != 0 && EnableMap != 1))
            {
                EnableMap = 0;
                fixes.Add("Corrected EnableMap to 0");
            }
            if (UseMapOnMapItem == null || (UseMapOnMapItem != 0 && UseMapOnMapItem != 1))
            {
                UseMapOnMapItem = 0;
                fixes.Add("Corrected UseMapOnMapItem to 0");
            }
            if (ShowPlayerPosition == null || (ShowPlayerPosition != 0 && ShowPlayerPosition != 1 && ShowPlayerPosition != 2))
            {
                ShowPlayerPosition = 0;
                fixes.Add("Corrected ShowPlayerPosition to 0");
            }
            if (ShowMapStats == null || (ShowMapStats != 0 && ShowMapStats != 1))
            {
                ShowMapStats = 0;
                fixes.Add("Corrected ShowMapStats to 0");
            }
            if (NeedPenItemForCreateMarker == null || (NeedPenItemForCreateMarker != 0 && NeedPenItemForCreateMarker != 1))
            {
                NeedPenItemForCreateMarker = 0;
                fixes.Add("Corrected NeedPenItemForCreateMarker to 0");
            }
            if (NeedGPSItemForCreateMarker == null || (NeedGPSItemForCreateMarker != 0 && NeedGPSItemForCreateMarker != 1))
            {
                NeedGPSItemForCreateMarker = 0;
                fixes.Add("Corrected NeedGPSItemForCreateMarker to 0");
            }
            if (CanCreateMarker == null || (CanCreateMarker != 0 && CanCreateMarker != 1))
            {
                CanCreateMarker = 0;
                fixes.Add("Corrected CanCreateMarker to 0");
            }
            if (CanCreate3DMarker == null || (CanCreate3DMarker != 0 && CanCreate3DMarker != 1))
            {
                CanCreate3DMarker = 0;
                fixes.Add("Corrected CanCreate3DMarker to 0");
            }
            if (CanOpenMapWithKeyBinding == null || (CanOpenMapWithKeyBinding != 0 && CanOpenMapWithKeyBinding != 1))
            {
                CanOpenMapWithKeyBinding = 0;
                fixes.Add("Corrected CanOpenMapWithKeyBinding to 0");
            }
            if (ShowDistanceOnPersonalMarkers == null || (ShowDistanceOnPersonalMarkers != 0 && ShowDistanceOnPersonalMarkers != 1))
            {
                ShowDistanceOnPersonalMarkers = 0;
                fixes.Add("Corrected ShowDistanceOnPersonalMarkers to 0");
            }
            if (EnableHUDGPS == null || (EnableHUDGPS != 0 && EnableHUDGPS != 1))
            {
                EnableHUDGPS = 0;
                fixes.Add("Corrected EnableHUDGPS to 0");
            }
            if (NeedGPSItemForKeyBinding == null || (NeedGPSItemForKeyBinding != 0 && NeedGPSItemForKeyBinding != 1))
            {
                NeedGPSItemForKeyBinding = 0;
                fixes.Add("Corrected NeedGPSItemForKeyBinding to 0");
            }
            if (NeedMapItemForKeyBinding == null || (NeedMapItemForKeyBinding != 0 && NeedMapItemForKeyBinding != 1))
            {
                NeedMapItemForKeyBinding = 0;
                fixes.Add("Corrected NeedMapItemForKeyBinding to 0");
            }
            if (EnableServerMarkers == null || (EnableServerMarkers != 0 && EnableServerMarkers != 1))
            {
                EnableServerMarkers = 0;
                fixes.Add("Corrected EnableServerMarkers to 0");
            }
            if (ShowNameOnServerMarkers == null || (ShowNameOnServerMarkers != 0 && ShowNameOnServerMarkers != 1))
            {
                ShowNameOnServerMarkers = 0;
                fixes.Add("Corrected ShowNameOnServerMarkers to 0");
            }
            if (ShowDistanceOnServerMarkers == null || (ShowDistanceOnServerMarkers != 0 && ShowDistanceOnServerMarkers != 1))
            {
                ShowDistanceOnServerMarkers = 0;
                fixes.Add("Corrected ShowDistanceOnServerMarkers to 0");
            }
            if (EnableHUDCompass == null || (EnableHUDCompass != 0 && EnableHUDCompass != 1))
            {
                EnableHUDCompass = 0;
                fixes.Add("Corrected EnableHUDCompass to 0");
            }
            if (NeedCompassItemForHUDCompass == null || (NeedCompassItemForHUDCompass != 0 && NeedCompassItemForHUDCompass != 1))
            {
                NeedCompassItemForHUDCompass = 0;
                fixes.Add("Corrected NeedCompassItemForHUDCompass to 0");
            }
            if (NeedGPSItemForHUDCompass == null || (NeedGPSItemForHUDCompass != 0 && NeedGPSItemForHUDCompass != 1))
            {
                NeedGPSItemForHUDCompass = 0;
                fixes.Add("Corrected NeedGPSItemForHUDCompass to 0");
            }
            if (CreateDeathMarker == null || (CreateDeathMarker != 0 && CreateDeathMarker != 1))
            {
                CreateDeathMarker = 0;
                fixes.Add("Corrected CreateDeathMarker to 0");
            }
            if (PlayerLocationNotifier == null || (PlayerLocationNotifier != 0 && PlayerLocationNotifier != 1))
            {
                PlayerLocationNotifier = 0;
                fixes.Add("Corrected PlayerLocationNotifier to 0");
            }
            if(CompassColor == null)
            {
                CompassColor = -1;
                fixes.Add("Corrected CompassColor to -1");
            }
            if (CompassBadgesColor == null)
            {
                CompassBadgesColor = -1946157056;
                fixes.Add("Corrected CompassBadgesColor to -1946157056");
            }
            if(ServerMarkers == null)
            {
                ServerMarkers = new BindingList<ExpansionServerMarkerData>();
                if (AppServices.GetRequired<ProjectManager>().CurrentProject.MpMissionPath.Split(".")[1] == "chernarusplus")
                    DefaultChernarusMarkers();
                fixes.Add("Initialized HUDColors");
            }
            foreach(ExpansionServerMarkerData SM in ServerMarkers)
            {
                if(SM.m_Visibility == null || (SM.m_Visibility != 0 && SM.m_Visibility != 2 && SM.m_Visibility != 4 && SM.m_Visibility != 6))
                {
                    SM.m_Visibility = 0;
                    fixes.Add("Corrected m_Visibility to 0");
                }
                if (SM.m_Is3D == null || (SM.m_Is3D != 0 && SM.m_Is3D != 1))
                {
                    SM.m_Is3D = 0;
                    fixes.Add("Corrected m_Is3D to 0");
                }
                if(SM.m_Color == null)
                {
                    SM.m_Color = -13710223;
                    fixes.Add("Corrected m_Color to -13710223");
                }
                if (SM.m_Locked == null || (SM.m_Locked != 0 && SM.m_Locked != 1))
                {
                    SM.m_Locked = 0;
                    fixes.Add("Corrected m_Locked to 0");
                }
                if (SM.m_Persist == null || (SM.m_Persist != 0 && SM.m_Persist != 1))
                {
                    SM.m_Persist = 1;
                    fixes.Add("Corrected m_Persist to 1");
                }
            }
            return fixes;
        }
        void DefaultChernarusMarkers()
        {
            ServerMarkers = new BindingList<ExpansionServerMarkerData>()
            {
                //! Krasnostrav Airsrip Trader
                new ExpansionServerMarkerData()
                {
                    m_UID = "ServerMarker_Trader_Krasno",
                    m_Visibility = 6,
                    m_Is3D = 1,
                    m_Text = "Traders - Krasnostav Airstrip",
                    m_IconName = "Trader",
                    m_Color = -13710223,
                    m_Position = new float[]
                    {
                        11882.0f,
                        143.0f,
                        12466.0f
                    }
                },
                new ExpansionServerMarkerData()
                {
                    m_UID = "ServerMarker_Trader_Kamenka",
                    m_Visibility = 6,
                    m_Is3D = 1,
                    m_Text = "Traders - Kamenka",
                    m_IconName = "Trader",
                    m_Color = -13710223,
                    m_Position = new float[]
                    {
                        1101.0f,
                                    8.0f,
                                    2382.0f
                    },
                    m_Locked = 0,
                    m_Persist = 1
                },
                new ExpansionServerMarkerData()
                {
                    m_UID = "ServerMarker_Boats_Kamenka",
                    m_Visibility = 6,
                    m_Is3D = 1,
                    m_Text = "Boats - Kamenka",
                    m_IconName = "Boat",
                    m_Color = -13710223,
                    m_Position = new float[] {
                                    1756.0f,
                                    4.0f,
                                    2027.0f
                    },
                    m_Locked = 0,
                    m_Persist = 1
                },
                new ExpansionServerMarkerData()
                {
                    m_UID = "ServerMarker_Aircrafts_Balota",
                    m_Visibility = 6,
                    m_Is3D = 1,
                    m_Text = "Aircrafts - Balota",
                    m_IconName = "Helicopter",
                    m_Color = -13710223,
                    m_Position = new float[]{
                                    4973.0f,
                                    12.0f,
                                    2436.0f
                    },

                    m_Locked = 0,
                    m_Persist = 1
                },
                new ExpansionServerMarkerData()
                {
                    m_UID = "ServerMarker_Boats_Svetloyarsk",
                    m_Visibility = 6,
                    m_Is3D = 1,
                    m_Text = "Boats & Fishing - Svetloyarsk",
                    m_IconName = "Boat",
                    m_Color = -13710223,
                    m_Position = new float[]{
                                    14379.0f,
                                    6.0f,
                                    13256.0f
                    },
                    m_Locked = 0,
                    m_Persist = 1
                },
                new ExpansionServerMarkerData()
                {
                    m_UID = "ServerMarker_Trader_Green_Montain",
                    m_Visibility = 6,
                    m_Is3D = 1,
                    m_Text = "Trader - Green Montain",
                    m_IconName = "Trader",
                    m_Color = -13710223,
                    m_Position = new float[] {
                                    3698.0f,
                                    405.0f,
                                    5988.0f
                    },
                    m_Locked = 0,
                    m_Persist = 1
                }
            };
        }
        public ExpansionMapSettings Clone()
        {
            return new ExpansionMapSettings()
            {
                m_Version = this.m_Version,
                   EnableMap = this.EnableMap,
                   UseMapOnMapItem = this.UseMapOnMapItem,
                   ShowPlayerPosition = this.ShowPlayerPosition,
                   ShowMapStats = this.ShowMapStats,
                   NeedPenItemForCreateMarker = this.NeedPenItemForCreateMarker,
                   NeedGPSItemForCreateMarker = this.NeedGPSItemForCreateMarker,
                   CanCreateMarker = this.CanCreateMarker,
                   CanCreate3DMarker = this.CanCreate3DMarker,
                   CanOpenMapWithKeyBinding = this.CanOpenMapWithKeyBinding,
                   ShowDistanceOnPersonalMarkers = this.ShowDistanceOnPersonalMarkers,
                   EnableHUDGPS = this.EnableHUDGPS,
                   NeedGPSItemForKeyBinding = this.NeedGPSItemForKeyBinding,
                   NeedMapItemForKeyBinding = this.NeedMapItemForKeyBinding,
                   EnableServerMarkers = this.EnableServerMarkers,
                   ShowNameOnServerMarkers = this.ShowNameOnServerMarkers,
                   ShowDistanceOnServerMarkers = this.ShowDistanceOnServerMarkers,
                   ServerMarkers = CloneBindingList(this.ServerMarkers, CloneServerMarkers),
                   EnableHUDCompass = this.EnableHUDCompass,
                   NeedCompassItemForHUDCompass = this.NeedCompassItemForHUDCompass,
                   NeedGPSItemForHUDCompass = this.NeedGPSItemForHUDCompass,
                   CompassColor = this.CompassColor,
                   CreateDeathMarker = this.CreateDeathMarker,
                   PlayerLocationNotifier = this.PlayerLocationNotifier,
                   CompassBadgesColor = this.CompassBadgesColor
            };
        }
        private static BindingList<T> CloneBindingList<T>(BindingList<T> source, Func<T, T> cloner)
        {
            if (source == null) return null;
            var result = new BindingList<T>();
            foreach (var item in source)
            {
                result.Add(item == null ? default : cloner(item));
            }
            return result;
        }
        private static ExpansionServerMarkerData CloneServerMarkers(ExpansionServerMarkerData src)
        {
            if (src == null) return null;

            return src.Clone();
        }
    }
    public class ExpansionServerMarkerData
    {
        public string? m_UID { get; set; }
        public int? m_Visibility { get; set; }
        public int? m_Is3D { get; set; }
        public string? m_Text { get; set; }
        public string? m_IconName { get; set; }
        public int? m_Color { get; set; }
        public float[]? m_Position { get; set; }
        public int? m_Locked { get; set; }
        public int? m_Persist { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is not ExpansionServerMarkerData other)
                return false;

            return string.Equals(m_UID, other.m_UID, StringComparison.Ordinal) &&
                   m_Visibility == other.m_Visibility &&
                   m_Is3D == other.m_Is3D &&
                   string.Equals(m_Text, other.m_Text, StringComparison.Ordinal) &&
                   string.Equals(m_IconName, other.m_IconName, StringComparison.Ordinal) &&
                   m_Color == other.m_Color &&
                   FloatArrayEquals(m_Position, other.m_Position) &&
                   m_Locked == other.m_Locked &&
                   m_Persist == other.m_Persist;
        }
        private static bool FloatArrayEquals(float[] a, float[] b, float epsilon = 1e-5f)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            if (a.Length != b.Length) return false;
            for (int i = 0; i < a.Length; i++)
            {
                if (Math.Abs(a[i] - b[i]) > epsilon) return false;
            }
            return true;
        }
        public ExpansionServerMarkerData Clone()
        {
            return new ExpansionServerMarkerData()
            {
                m_UID = this.m_UID,
                m_Visibility = this.m_Visibility,
                m_Is3D = this.m_Is3D,
                m_Text = this.m_Text, 
                m_IconName = this.m_IconName,
                m_Color = this.m_Color,
                m_Position = this.m_Position != null ? (float[])this.m_Position.Clone() : null,
                m_Locked = this.m_Locked,
                m_Persist = this.m_Persist
            };
        }

        public override string ToString()
        {
            return m_UID;
        }
    }
}
