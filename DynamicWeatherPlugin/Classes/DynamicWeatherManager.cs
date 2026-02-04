using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWeatherPlugin
{
    public class DynamicWeatherManager
    {
        private readonly Dictionary<string, string> _paths = new();
        public string basePath { get; set; }
        public string profilePath { get; set; }
        public bool HasErrors { get; set; }
        public List<string> Errors = new List<string>();

        public DynamicWeatherConfig DynamicWeatherPluginConfig { get; set; }

        public DynamicWeatherManager() { }

        public void SetDynamicweatherStuff()
        {
            basePath = Path.Combine(AppServices.GetRequired<ProjectManager>().CurrentProject.ProjectRoot, "mpmissions", AppServices.GetRequired<ProjectManager>().CurrentProject.MpMissionPath);
            profilePath = Path.Combine(AppServices.GetRequired<ProjectManager>().CurrentProject.ProjectRoot, AppServices.GetRequired<ProjectManager>().CurrentProject.ProfileName);


            //Settings files in profiles
            _paths["DynamicWeather"] = Path.Combine(basePath, "weather.json");

            Console.WriteLine($"\n[Dynamic Weather Manager] Loading....");
            DynamicWeatherPluginConfig = new DynamicWeatherConfig(_paths["DynamicWeather"]);
            LoadConfigWithErrorReport("ExpansionLoadouts", DynamicWeatherPluginConfig);
        }
        private void LoadConfigWithErrorReport(string name, IConfigLoader config)
        {
            if (config is IConfigLoader loader)
            {
                config.Load();
            }

            if (config.HasErrors)
            {
                HasErrors = true;
                Errors.AddRange(config.Errors.Select(e => $"[{name}] {e}"));
            }
        }
        public IEnumerable<string> Save()
        {
            var configs = new object[]
            {
                DynamicWeatherPluginConfig
            };

            var savedFiles = new List<string>();

            foreach (var obj in configs)
            {
                if (obj is IConfigLoader config)
                {
                    savedFiles.AddRange(config.Save());
                }
            }

            return savedFiles;
        }
        public bool needToSave()
        {
            bool needtosave = false;
            var configs = new object[]
            {
                DynamicWeatherPluginConfig
            };
            foreach (var obj in configs)
            {
                if (obj is not IConfigLoader config)
                    continue;
                if (config.needToSave())
                    needtosave = true;
            }
            return needtosave;
        }
    }
}
