﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2eEditor
{
    public class Manifest
    {
        public AppInfo MainApp { get; set; }
        public List<PluginInfo> Plugins { get; set; }
    }

    public class AppInfo
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
}
