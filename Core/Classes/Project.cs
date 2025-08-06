using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Day2eEditor
{
    public class ProjectStore
    {
        public bool ShowChangeLog { get; set; } = true;
        public string ActiveProject { get; set; } = string.Empty;
        public BindingList<Project> Projects { get; set; } = new();
    }
    public class Project
    {
        public string ProjectName { get; set; }
        public string ProjectRoot { get; set; }
        public string ProfileName { get; set; }
        public string MpMissionPath { get; set; }
        public string MapPath { get; set; }
        public int MapSize { get; set; }
        public bool CreateBackups { get; set; }

        public void AddNames(string _ProjectName)
        {
            ProjectName = _ProjectName;
        }
        public override string ToString()
        {
            return ProjectName;
        }
    }
}
