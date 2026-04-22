using System.ComponentModel;
using System.Text;
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
        public ProjectServerSettings ServerSettings { get; set; }
                = new ProjectServerSettings();


        public void AddNames(string _ProjectName)
        {
            ProjectName = _ProjectName;
        }
        public override string ToString()
        {
            return ProjectName;
        }
    }
   public sealed class ProjectServerSettings
    {
        public TransferProtocol Protocol { get; set; }

        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }

        public string Username { get; set; } = string.Empty;
        public string EncryptedPassword { get; set; } = string.Empty;

        public bool PassiveMode { get; set; } = true;
    }
    public enum TransferProtocol
    {
        Ftp,
        Ftps,
        Sftp
    }

}
