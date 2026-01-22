using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExpansionPlugin
{
    public class ExpansionNotificationSchedulerConfig : IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path); // e.g., "types.xml"
        public string FilePath => _path;
        public ExpansionNotificationSchedulerSettings Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }
        public const int CurrentVersion = 2;
        public ExpansionNotificationSchedulerConfig(string path)
        {
            _path = path;
        }
        public void Load()
        {
            Data = null;
            Data = AppServices.GetRequired<FileService>().LoadOrCreateJson<ExpansionNotificationSchedulerSettings>(
                _path,
                createNew: () => new ExpansionNotificationSchedulerSettings(CurrentVersion),
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
                configName: "ExpansionNotificationSchedulerSettings"
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
    public class ExpansionNotificationSchedulerSettings
    {
        public int? m_Version { get; set; }
        public int? Enabled { get; set; }
        public int? UTC { get; set; }
        public int? UseMissionTime { get; set; }
        public BindingList<ExpansionNotificationSchedule> Notifications { get; set; }


        public ExpansionNotificationSchedulerSettings() { }
        public ExpansionNotificationSchedulerSettings(int CurrentVersion)
        {
            m_Version = CurrentVersion;
            Enabled = 0;
            UTC = 0;
            UseMissionTime = 0;
            Notifications = new BindingList<ExpansionNotificationSchedule>()
            {
                new ExpansionNotificationSchedule()
                {
                    Hour = 22,
                    Minute = 00,
                    Second = 0,
                    Title = "Notification Schedule Test 1",
                    Text = "Lorem ipsum dolor sit amet",
                    Icon = "Info",
                    Color = "FFFFFFFF"
                },
                new ExpansionNotificationSchedule()
                {
                    Hour = 22,
                    Minute = 01,
                    Second = 0,
                    Title = "Notification Schedule Test 2",
                    Text = "Lorem ipsum dolor sit amet",
                    Icon = "Info",
                    Color = "FFFFFFFF"
                }
            };
        }
        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();
            if (m_Version != ExpansionNotificationSchedulerConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionNotificationSchedulerConfig.CurrentVersion}");
                m_Version = ExpansionNotificationSchedulerConfig.CurrentVersion;
            }
            if (Enabled is null or < 0 or > 1)
            {
                Enabled = 1;
                fixes.Add("Corrected Enabled");
            }
            if (UTC is null or < 0 or > 1)
            {
                UTC = 0;
                fixes.Add("Corrected UTC");
            }

            if (UseMissionTime is null or < 0 or > 1)
            {
                UseMissionTime = 0;
                fixes.Add("Corrected UseMissionTime");
            }

            if (Notifications == null)
            {
                Notifications = new BindingList<ExpansionNotificationSchedule>();
                fixes.Add("Created Notifications list");
            }

            foreach (var n in Notifications)
            {
                if (n.Hour is null or < 0 or > 23)
                {
                    n.Hour = 0;
                    fixes.Add("Corrected Notification Hour");
                }

                if (n.Minute is null or < 0 or > 59)
                {
                    n.Minute = 0;
                    fixes.Add("Corrected Notification Minute");
                }

                if (n.Second is null or < 0 or > 59)
                {
                    n.Second = 0;
                    fixes.Add("Corrected Notification Second");
                }

                if (string.IsNullOrWhiteSpace(n.Title))
                {
                    n.Title = "Notification";
                    fixes.Add("Corrected Notification Title");
                }

                if (string.IsNullOrWhiteSpace(n.Text))
                {
                    n.Text = string.Empty;
                    fixes.Add("Corrected Notification Text");
                }

                if (string.IsNullOrWhiteSpace(n.Icon))
                {
                    n.Icon = "Info";
                    fixes.Add("Corrected Notification Icon");
                }

                if (string.IsNullOrWhiteSpace(n.Color))
                {
                    n.Color = "FFFFFFFF";
                    fixes.Add("Corrected Notification Color");
                }
            }

            return fixes;
        }
        public override bool Equals(object obj)
        {
            if (obj is not ExpansionNotificationSchedulerSettings other)
                return false;


            if (m_Version != other.m_Version ||
                   Enabled != other.Enabled ||
                   UTC != other.UTC ||
                   UseMissionTime != other.UseMissionTime)
                return false;

            if (Notifications == null && other.Notifications == null)
                return true;

            if (Notifications == null || other.Notifications == null)
                return false;

            if (Notifications.Count != other.Notifications.Count)
                return false;

            for (int i = 0; i < Notifications.Count; i++)
            {
                if (!Notifications[i].Equals(other.Notifications[i]))
                    return false;
            }

            return true;
        }
        public ExpansionNotificationSchedulerSettings Clone()
        {
            return new ExpansionNotificationSchedulerSettings()
            {
                m_Version = this.m_Version,
                Enabled = this.Enabled,
                UTC = this.UTC,
                UseMissionTime = this.UseMissionTime,
                Notifications = new BindingList<ExpansionNotificationSchedule>(this.Notifications.Select(p => p.Clone()).ToList())
            };
        }
    }
    public class ExpansionNotificationSchedule
    {
        public int? Hour { get; set; }
        public int? Minute { get; set; }
        public int? Second { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public string? Icon { get; set; }
        public string? Color { get; set; }

        public ExpansionNotificationSchedule()
        {
            Color = "FFFFFFFF";
        }

        public override string ToString()
        {
            return Title;
        }
        public override bool Equals(object obj)
        {
            if (obj is not ExpansionNotificationSchedule other)
                return false;

            return Hour == other.Hour &&
                   Minute == other.Minute &&
                   Second == other.Second &&
                   Title == other.Title &&
                   Text == other.Text &&
                   Icon == other.Icon &&
                   Color == other.Color;
        }
        public ExpansionNotificationSchedule Clone()
        {
            return new ExpansionNotificationSchedule
            {
                Hour =this.Hour,
                Minute = this.Minute,
                Second = this.Second,
                Title = this.Title,
                Text = this.Text,
                Icon = this.Icon,
                Color = this.Color
            };
        }
    }
}
