using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2eEditor
{
    public class Manifest
    {
        public AppInfo MainApp { get; set; }
        public CoreInfo Core { get; set; }
        public CoreUIInfo CoreUI { get; set; }
        public List<PluginInfo> Plugins { get; set; }
        public List<MapAddonInfo> MapAddons { get; set; }
    }

    public class AppInfo
    {
        public string Version { get; set; }       // e.g., "2.3.0"
        public string Url { get; set; }           // Direct URL to MainApp.zip
        public string Checksum { get; set; }      // e.g., "sha256-abcdef123456..."
    }

    public class CoreInfo
    {
        public string Version { get; set; }       // e.g., "2.3.0"
        public string Url { get; set; }           // Direct URL to MainApp.zip
        public string Checksum { get; set; }      // e.g., "sha256-abcdef123456..."
    }

    public class CoreUIInfo
    {
        public string Version { get; set; }       // e.g., "2.3.0"
        public string Url { get; set; }           // Direct URL to MainApp.zip
        public string Checksum { get; set; }      // e.g., "sha256-abcdef123456..."
    }
    public class PluginInfo
    {
        public string Name { get; set; }          // e.g., "MyPluginA"
        public string Version { get; set; }       // e.g., "1.2.0"
        public string Url { get; set; }           // Direct URL to plugin DLL
        public string Checksum { get; set; }      // e.g., "sha256-123abc..."
    }

    public class MapAddonInfo
    {
        public string Name { get; set; }          // e.g., "ChernarusPlus_MapAddon.zip"
        public string Version { get; set; }       // e.g., "1.0.0"
        public string Url { get; set; }           // Direct URL to plugin DLL
        public string Checksum { get; set; }      // e.g., "sha256-123abc..."
        public MapInfo MapInfo { get; set; }
    }
    public class MapInfo
    {
        public string MapPng { get; set; }
        public string MapXYZ { get; set; }
    }
}
