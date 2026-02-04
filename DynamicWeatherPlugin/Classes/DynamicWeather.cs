using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DynamicWeatherPlugin
{
    public class DynamicWeatherConfig : DynamicWeatherBaseIConfigLoader<DynamicWeatherSettings>
    {
        public DynamicWeatherConfig(string path) : base(path)
        { 
        }
        public override void Load()
        {
            try
            {
                Data = new DynamicWeatherSettings()
                {
                    m_Dynamics = AppServices.GetRequired<FileService>().LoadOrCreateJson<BindingList<WeatherDynamic>>(
                        _path,
                        createNew: () => new BindingList<WeatherDynamic>(),
                        onError: ex =>
                        {
                            HandleLoadError(ex);
                        },
                        configName: FileName
                    )
                };
                ClonedData = CloneData(Data);
            }
            catch (Exception ex)
            {
                HandleLoadError(ex);
            }

        }
        public override IEnumerable<string> Save()
        {
            if (Data is null)
                return Array.Empty<string>();

            if (!AreEqual(Data, ClonedData) || isDirty == true)
            {
                isDirty = false;
                AppServices.GetRequired<FileService>().SaveJson(_path, Data.m_Dynamics);
                ClonedData = CloneData(Data);
                return new[] { Path.GetFileName(_path) };
            }

            return Array.Empty<string>();
        }
        protected override DynamicWeatherSettings CreateDefaultData()
        {
            return new DynamicWeatherSettings();
        }
    }
    public class DynamicWeatherSettings : IEquatable<DynamicWeatherSettings>, IDeepCloneable<DynamicWeatherSettings>
    {
        public BindingList<WeatherDynamic> m_Dynamics { get; set; }
        public DynamicWeatherSettings()
        {
        }
        public bool Equals(DynamicWeatherSettings obj)
        {
            if (obj is not DynamicWeatherSettings other)
                return false;

            if (m_Dynamics == null && other.m_Dynamics == null)
                return true;

            if (m_Dynamics == null || other.m_Dynamics == null || m_Dynamics.Count != other.m_Dynamics.Count)
                return false;

            for (int i = 0; i < m_Dynamics.Count; i++)
            {
                if (!m_Dynamics[i].Equals(other.m_Dynamics[i]))
                    return false;
            }

            return true;
        }
        public override bool Equals(object? obj) => Equals(obj as DynamicWeatherSettings);
        public DynamicWeatherSettings Clone()
        {
            return new DynamicWeatherSettings
            {
                m_Dynamics = new BindingList<WeatherDynamic>(this.m_Dynamics.Select(x => x.Clone()).ToList()),
            };
        }
    }
    public class WeatherDynamic
    {
        public bool chat_message { get; set; }
        public bool notify_message { get; set; }
        public string name { get; set; }
        public decimal transition_min { get; set; }
        public decimal transition_max { get; set; }
        public decimal duration_min { get; set; }
        public decimal duration_max { get; set; }
        public decimal overcast_min { get; set; }
        public decimal overcast_max { get; set; }
        public bool use_dyn_vol_fog { get; set; }
        public decimal dyn_vol_fog_dist_min { get; set; }
        public decimal dyn_vol_fog_dist_max { get; set; }
        public decimal dyn_vol_fog_height_min { get; set; }
        public decimal dyn_vol_fog_height_max { get; set; }
        public decimal dyn_vol_fog_bias { get; set; }
        public decimal fog_transition_time { get; set; }
        public decimal fog_min { get; set; }
        public decimal fog_max { get; set; }
        public decimal wind_speed_min { get; set; }
        public decimal wind_speed_max { get; set; }
        public decimal wind_dir_min { get; set; }
        public decimal wind_dir_max { get; set; }
        public decimal rain_min { get; set; }
        public decimal rain_max { get; set; }
        public decimal snowfall_min { get; set; }
        public decimal snowfall_max { get; set; }
        public decimal snowfall_threshold_min { get; set; }
        public decimal snowfall_threshold_max { get; set; }
        public decimal snowfall_threshold_timeout { get; set; }
        public decimal snowflake_scale_min { get; set; }
        public decimal snowflake_scale_max { get; set; }
        public bool use_snowflake_scale { get; set; }
        public bool storm { get; set; }
        public decimal thunder_threshold { get; set; }
        public decimal thunder_timeout { get; set; }
        public bool use_global_temperature { get; set; }
        public decimal global_temperature_override { get; set; }

        public override string ToString()
        {
            return name;
        }

        public override bool Equals(object obj)
        {
            if (obj is not WeatherDynamic other)
                return false;

            return chat_message == other.chat_message &&
                   notify_message == other.notify_message &&
                   name == other.name &&
                   transition_min == other.transition_min &&
                   transition_max == other.transition_max &&
                   duration_min == other.duration_min &&
                   duration_max == other.duration_max &&
                   overcast_min == other.overcast_min &&
                   overcast_max == other.overcast_max &&
                   use_dyn_vol_fog == other.use_dyn_vol_fog &&
                   dyn_vol_fog_dist_min == other.dyn_vol_fog_dist_min &&
                   dyn_vol_fog_dist_max == other.dyn_vol_fog_dist_max &&
                   dyn_vol_fog_height_min == other.dyn_vol_fog_height_min &&
                   dyn_vol_fog_height_max == other.dyn_vol_fog_height_max &&
                   dyn_vol_fog_bias == other.dyn_vol_fog_bias &&
                   fog_transition_time == other.fog_transition_time &&
                   fog_min == other.fog_min &&
                   fog_max == other.fog_max &&
                   wind_speed_min == other.wind_speed_min &&
                   wind_speed_max == other.wind_speed_max &&
                   wind_dir_min == other.wind_dir_min &&
                   wind_dir_max == other.wind_dir_max &&
                   rain_min == other.rain_min &&
                   rain_max == other.rain_max &&
                   snowfall_min == other.snowfall_min &&
                   snowfall_max == other.snowfall_max &&
                   snowfall_threshold_min == other.snowfall_threshold_min &&
                   snowfall_threshold_max == other.snowfall_threshold_max &&
                   snowfall_threshold_timeout == other.snowfall_threshold_timeout &&
                   snowflake_scale_min == other.snowflake_scale_min &&
                   snowflake_scale_max == other.snowflake_scale_max &&
                   use_snowflake_scale == other.use_snowflake_scale &&
                   storm == other.storm &&
                   thunder_threshold == other.thunder_threshold &&
                   thunder_timeout == other.thunder_timeout &&
                   use_global_temperature == other.use_global_temperature &&
                   global_temperature_override == other.global_temperature_override;
        }
        public WeatherDynamic Clone()
        {
            return new WeatherDynamic()
            {
                chat_message = this.chat_message,
                notify_message = this.notify_message,
                name = this.name,
                transition_min = this.transition_min,
                transition_max = this.transition_max,
                duration_min = this.duration_min,
                duration_max = this.duration_max,
                overcast_min = this.overcast_min,
                overcast_max = this.overcast_max,
                use_dyn_vol_fog = this.use_dyn_vol_fog,
                dyn_vol_fog_dist_min = this.dyn_vol_fog_dist_min,
                dyn_vol_fog_dist_max = this.dyn_vol_fog_dist_max,
                dyn_vol_fog_height_min = this.dyn_vol_fog_height_min,
                dyn_vol_fog_height_max = this.dyn_vol_fog_height_max,
                dyn_vol_fog_bias = this.dyn_vol_fog_bias,
                fog_transition_time = this.fog_transition_time,
                fog_min = this.fog_min,
                fog_max = this.fog_max,
                wind_speed_min = this.wind_speed_min,
                wind_speed_max = this.wind_speed_max,
                wind_dir_min = this.wind_dir_min,
                wind_dir_max = this.wind_dir_max,
                rain_min = this.rain_min,
                rain_max = this.rain_max,
                snowfall_min = this.snowfall_min,
                snowfall_max = this.snowfall_max,
                snowfall_threshold_min = this.snowfall_threshold_min,
                snowfall_threshold_max = this.snowfall_threshold_max,
                snowfall_threshold_timeout = this.snowfall_threshold_timeout,
                snowflake_scale_min = this.snowflake_scale_min,
                snowflake_scale_max = this.snowflake_scale_max,
                use_snowflake_scale = this.use_snowflake_scale,
                storm = this.storm,
                thunder_threshold = this.thunder_threshold,
                thunder_timeout = this.thunder_timeout,
                use_global_temperature = this.use_global_temperature,
                global_temperature_override = this.global_temperature_override
            };
        }
    }
}
