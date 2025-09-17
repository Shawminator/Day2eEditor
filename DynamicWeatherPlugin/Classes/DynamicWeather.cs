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
    public class DynamicWeatherPluginConfig
    {
        [JsonIgnore]
        public bool isDirty { get; set; }
        [JsonIgnore]
        public string Filename { get; set; }

        public BindingList<WeatherDynamic> m_Dynamics { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is not DynamicWeatherPluginConfig other)
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
        public IEnumerable<string> Save()
        {
            List<string> filesnames = new List<string>();
            if (isDirty)
            {
                AppServices.GetRequired<FileService>().SaveJson(Filename, m_Dynamics);
                isDirty = false;
                filesnames.Add(Path.GetFileName(Filename));
            }
            return filesnames.ToArray();
        }

        internal void CreateDefaults()
        {
            m_Dynamics = new BindingList<WeatherDynamic>
            {
                new WeatherDynamic
                {
                    chat_message = true,
                    notify_message = true,
                    name = "HOT SUN",
                    transition_min = 0.1m,
                    transition_max = 0.2m,
                    duration_min = 0.5m,
                    duration_max = 1.0m,
                    overcast_min = 0.0m,
                    overcast_max = 0.1m,
                    use_dyn_vol_fog = false,
                    dyn_vol_fog_dist_min = 0.0m,
                    dyn_vol_fog_dist_max = 0.0m,
                    dyn_vol_fog_height_min = 0.0m,
                    dyn_vol_fog_height_max = 0.0m,
                    dyn_vol_fog_bias = 0.0m,
                    fog_transition_time = 0.0m,
                    fog_min = 0.0m,
                    fog_max = 0.0m,
                    wind_speed_min = 0.0m,
                    wind_speed_max = 0.1m,
                    wind_dir_min = 0.0m,
                    wind_dir_max = 360.0m,
                    rain_min = 0.0m,
                    rain_max = 0.0m,
                    snowfall_min = 0.0m,
                    snowfall_max = 0.0m,
                    snowflake_scale_min = 0.0m,
                    snowflake_scale_max = 0.0m,
                    use_snowflake_scale = false,
                    storm = false,
                    thunder_threshold = 0.0m,
                    thunder_timeout = 0.0m,
                    use_global_temperature = true,
                    global_temperature_override = 35.0m
                },
                new WeatherDynamic
                {
                    chat_message = true,
                    notify_message = true,
                    name = "QUICK FREEZE",
                    transition_min = 0.1m,
                    transition_max = 0.2m,
                    duration_min = 0.5m,
                    duration_max = 1.0m,
                    overcast_min = 0.2m,
                    overcast_max = 0.4m,
                    use_dyn_vol_fog = false,
                    dyn_vol_fog_dist_min = 0.0m,
                    dyn_vol_fog_dist_max = 0.0m,
                    dyn_vol_fog_height_min = 0.0m,
                    dyn_vol_fog_height_max = 0.0m,
                    dyn_vol_fog_bias = 0.0m,
                    fog_transition_time = 0.0m,
                    fog_min = 0.1m,
                    fog_max = 0.3m,
                    wind_speed_min = 0.0m,
                    wind_speed_max = 0.2m,
                    wind_dir_min = 0.0m,
                    wind_dir_max = 360.0m,
                    rain_min = 0.0m,
                    rain_max = 0.0m,
                    snowfall_min = 0.6m,
                    snowfall_max = 1.0m,
                    snowflake_scale_min = 1.0m,
                    snowflake_scale_max = 3.0m,
                    use_snowflake_scale = true,
                    storm = false,
                    thunder_threshold = 0.0m,
                    thunder_timeout = 0.0m,
                    use_global_temperature = true,
                    global_temperature_override = -15.0m
                },
                new WeatherDynamic
                {
                    chat_message = true,
                    notify_message = true,
                    name = "MICRO STORM",
                    transition_min = 0.1m,
                    transition_max = 0.2m,
                    duration_min = 0.5m,
                    duration_max = 1.0m,
                    overcast_min = 0.8m,
                    overcast_max = 1.0m,
                    use_dyn_vol_fog = true,
                    dyn_vol_fog_dist_min = 0.4m,
                    dyn_vol_fog_dist_max = 0.6m,
                    dyn_vol_fog_height_min = 0.2m,
                    dyn_vol_fog_height_max = 0.3m,
                    dyn_vol_fog_bias = 10.0m,
                    fog_transition_time = 5.0m,
                    fog_min = 0.5m,
                    fog_max = 0.8m,
                    wind_speed_min = 0.4m,
                    wind_speed_max = 0.8m,
                    wind_dir_min = 0.0m,
                    wind_dir_max = 360.0m,
                    rain_min = 0.4m,
                    rain_max = 0.8m,
                    snowfall_min = 0.0m,
                    snowfall_max = 0.0m,
                    snowflake_scale_min = 0.0m,
                    snowflake_scale_max = 0.0m,
                    use_snowflake_scale = false,
                    storm = true,
                    thunder_threshold = 0.0m,
                    thunder_timeout = 5.0m,
                    use_global_temperature = false,
                    global_temperature_override = 0.0m
                },
                new WeatherDynamic
                {
                    chat_message = true,
                    notify_message = true,
                    name = "FULL OVERCAST QUICK",
                    transition_min = 0.1m,
                    transition_max = 0.2m,
                    duration_min = 0.5m,
                    duration_max = 1.0m,
                    overcast_min = 1.0m,
                    overcast_max = 1.0m,
                    use_dyn_vol_fog = true,
                    dyn_vol_fog_dist_min = 0.5m,
                    dyn_vol_fog_dist_max = 0.7m,
                    dyn_vol_fog_height_min = 0.3m,
                    dyn_vol_fog_height_max = 0.5m,
                    dyn_vol_fog_bias = 10.0m,
                    fog_transition_time = 5.0m,
                    fog_min = 0.3m,
                    fog_max = 0.6m,
                    wind_speed_min = 0.1m,
                    wind_speed_max = 0.3m,
                    wind_dir_min = 0.0m,
                    wind_dir_max = 360.0m,
                    rain_min = 0.0m,
                    rain_max = 0.2m,
                    snowfall_min = 0.0m,
                    snowfall_max = 0.0m,
                    snowflake_scale_min = 0.0m,
                    snowflake_scale_max = 0.0m,
                    use_snowflake_scale = false,
                    storm = false,
                    thunder_threshold = 0.0m,
                    thunder_timeout = 0.0m,
                    use_global_temperature = false,
                    global_temperature_override = 0.0m
                }
            };

            // Mark as dirty since defaults were created
            isDirty = true;
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

    }
}
