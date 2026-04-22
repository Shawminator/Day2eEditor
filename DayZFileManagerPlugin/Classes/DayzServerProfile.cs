using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayZFileManagerPlugin
{

    public sealed class DayZServerProfile
    {
        // Identity (derived)
        public string Name { get; }

        // Connection
        public TransferProtocol Protocol { get; init; }
        public string Host { get; init; } = string.Empty;
        public int Port { get; init; }
        public string Username { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;

        // DayZ paths (derived)
        public string ServerRoot { get; }
        public string MissionFolder { get; }
        public string ProfilesFolder { get; }

        // FTP-specific
        public bool PassiveMode { get; init; } = true;

        private DayZServerProfile(
            string name,
            string serverRoot,
            string missionFolder,
            string profilesFolder)
        {
            Name = name;
            ServerRoot = serverRoot;
            MissionFolder = missionFolder;
            ProfilesFolder = profilesFolder;
        }

        public static DayZServerProfile FromProject(
            Project currentProject,
            TransferProtocol protocol,
            string host,
            int port,
            string username,
            string password,
            bool passiveMode = true)
        {
            if (currentProject == null)
                throw new ArgumentNullException(nameof(currentProject));

            return new DayZServerProfile(
                name: currentProject.ProjectName,
                serverRoot: currentProject.ProjectRoot,
                missionFolder: currentProject.MpMissionPath,
                profilesFolder: currentProject.ProfileName)
            {
                Protocol = protocol,
                Host = host,
                Port = port,
                Username = username,
                Password = password,
                PassiveMode = passiveMode
            };
        }

        public override string ToString() => Name;
    }

}
