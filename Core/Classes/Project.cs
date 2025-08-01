using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Day2eEditor
{
    public class ProjectStore
    {
        public bool ShowChangeLog { get; set; } = true;
        public string ActiveProject { get; set; } = string.Empty;
        public List<Project> Projects { get; set; } = new();
    }
    public class ProjectData
    {
        public string ProjectName { get; set; }
        public string ProjectRoot { get; set; }
        public string ProfileName { get; set; }
        public string MpMissionPath { get; set; }
        public string MapPath { get; set; }
        public int MapSize { get; set; }
        public bool CreateBackups { get; set; }
    }
    public class Project : ProjectData
    {
        [JsonIgnore] public bool HasWarnings { get; set; }

    }

}
