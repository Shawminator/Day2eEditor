using DayZeLib;
using System;

namespace Day2eEditor
{
    public class cfgweatherConfig : IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path);
        public string FilePath => _path;
        public weather Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }

        public cfgweatherConfig(string path)
        {
            _path = path;
        }
        public void Load()
        {
            Data = AppServices.GetRequired<FileService>().LoadOrCreateXml<weather>(
               _path,
               createNew: () => new weather(),
               onAfterLoad: cfg => { /* optional: do something after load */ },
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
               configName: "cfgweather"
           );
        }
        public IEnumerable<string> Save()
        {
            if (isDirty)
            {
                AppServices.GetRequired<FileService>().SaveXml(_path, Data);
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
    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    [System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
    public partial class weather
    {
        private weatherOvercast overcastField;
        private weatherFog fogField;
        private weatherRain rainField;
        private weatherWindMagnitude windMagnitudeField;
        private weatherWindDirection windDirectionField;
        private weatherSnowfall snowfallField;
        private weatherStorm stormField;
        private int resetField;
        private int enableField;

        public weatherOvercast overcast { get => overcastField; set => overcastField = value; }
        public weatherFog fog { get => fogField; set => fogField = value; }
        public weatherRain rain { get => rainField; set => rainField = value; }
        public weatherWindMagnitude windMagnitude { get => windMagnitudeField; set => windMagnitudeField = value; }
        public weatherWindDirection windDirection { get => windDirectionField; set => windDirectionField = value; }
        public weatherSnowfall snowfall { get => snowfallField; set => snowfallField = value; }
        public weatherStorm storm { get => stormField; set => stormField = value; }

        [System.Xml.Serialization.XmlAttribute] public int reset { get => resetField; set => resetField = value; }
        [System.Xml.Serialization.XmlAttribute] public int enable { get => enableField; set => enableField = value; }

        public override bool Equals(object obj) =>
            obj is weather w &&
            reset == w.reset &&
            enable == w.enable;

    }

    #region Overcast

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherOvercast
    {
        private weatherOvercastCurrent currentField;
        private weatherOvercastLimits limitsField;
        private weatherOvercastTimelimits timelimitsField;
        private weatherOvercastChangelimits changelimitsField;

        public weatherOvercastCurrent current { get => currentField; set => currentField = value; }
        public weatherOvercastLimits limits { get => limitsField; set => limitsField = value; }
        public weatherOvercastTimelimits timelimits { get => timelimitsField; set => timelimitsField = value; }
        public weatherOvercastChangelimits changelimits { get => changelimitsField; set => changelimitsField = value; }

        public override bool Equals(object obj) =>
            obj is weatherOvercast o &&
            Equals(current, o.current) &&
            Equals(limits, o.limits) &&
            Equals(timelimits, o.timelimits) &&
            Equals(changelimits, o.changelimits);

        public override int GetHashCode() => HashCode.Combine(current, limits, timelimits, changelimits);
    }

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherOvercastCurrent
    {
        private decimal actualField;
        private int timeField;
        private int durationField;

        [System.Xml.Serialization.XmlAttribute] public decimal actual { get => actualField; set => actualField = value; }
        [System.Xml.Serialization.XmlAttribute] public int time { get => timeField; set => timeField = value; }
        [System.Xml.Serialization.XmlAttribute] public int duration { get => durationField; set => durationField = value; }

        public override bool Equals(object obj) =>
            obj is weatherOvercastCurrent o && actual == o.actual && time == o.time && duration == o.duration;

        public override int GetHashCode() => HashCode.Combine(actual, time, duration);
    }

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherOvercastLimits
    {
        private decimal minField;
        private decimal maxField;

        [System.Xml.Serialization.XmlAttribute] public decimal min { get => minField; set => minField = value; }
        [System.Xml.Serialization.XmlAttribute] public decimal max { get => maxField; set => maxField = value; }

        public override bool Equals(object obj) => obj is weatherOvercastLimits o && min == o.min && max == o.max;
        public override int GetHashCode() => HashCode.Combine(min, max);
    }

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherOvercastTimelimits
    {
        private int minField;
        private int maxField;

        [System.Xml.Serialization.XmlAttribute] public int min { get => minField; set => minField = value; }
        [System.Xml.Serialization.XmlAttribute] public int max { get => maxField; set => maxField = value; }

        public override bool Equals(object obj) => obj is weatherOvercastTimelimits o && min == o.min && max == o.max;
        public override int GetHashCode() => HashCode.Combine(min, max);
    }

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherOvercastChangelimits
    {
        private decimal minField;
        private decimal maxField;

        [System.Xml.Serialization.XmlAttribute] public decimal min { get => minField; set => minField = value; }
        [System.Xml.Serialization.XmlAttribute] public decimal max { get => maxField; set => maxField = value; }

        public override bool Equals(object obj) => obj is weatherOvercastChangelimits o && min == o.min && max == o.max;
        public override int GetHashCode() => HashCode.Combine(min, max);
    }

    #endregion

    #region Fog

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherFog
    {
        private weatherFogCurrent currentField;
        private weatherFogLimits limitsField;
        private weatherFogTimelimits timelimitsField;
        private weatherFogChangelimits changelimitsField;

        public weatherFogCurrent current { get => currentField; set => currentField = value; }
        public weatherFogLimits limits { get => limitsField; set => limitsField = value; }
        public weatherFogTimelimits timelimits { get => timelimitsField; set => timelimitsField = value; }
        public weatherFogChangelimits changelimits { get => changelimitsField; set => changelimitsField = value; }

        public override bool Equals(object obj) =>
            obj is weatherFog o &&
            Equals(current, o.current) &&
            Equals(limits, o.limits) &&
            Equals(timelimits, o.timelimits) &&
            Equals(changelimits, o.changelimits);

        public override int GetHashCode() => HashCode.Combine(current, limits, timelimits, changelimits);
    }

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherFogCurrent
    {
        private decimal actualField;
        private int timeField;
        private int durationField;

        [System.Xml.Serialization.XmlAttribute] public decimal actual { get => actualField; set => actualField = value; }
        [System.Xml.Serialization.XmlAttribute] public int time { get => timeField; set => timeField = value; }
        [System.Xml.Serialization.XmlAttribute] public int duration { get => durationField; set => durationField = value; }

        public override bool Equals(object obj) =>
            obj is weatherFogCurrent o && actual == o.actual && time == o.time && duration == o.duration;

        public override int GetHashCode() => HashCode.Combine(actual, time, duration);
    }

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherFogLimits
    {
        private decimal minField;
        private decimal maxField;

        [System.Xml.Serialization.XmlAttribute] public decimal min { get => minField; set => minField = value; }
        [System.Xml.Serialization.XmlAttribute] public decimal max { get => maxField; set => maxField = value; }

        public override bool Equals(object obj) => obj is weatherFogLimits o && min == o.min && max == o.max;
        public override int GetHashCode() => HashCode.Combine(min, max);
    }

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherFogTimelimits
    {
        private int minField;
        private int maxField;

        [System.Xml.Serialization.XmlAttribute] public int min { get => minField; set => minField = value; }
        [System.Xml.Serialization.XmlAttribute] public int max { get => maxField; set => maxField = value; }

        public override bool Equals(object obj) => obj is weatherFogTimelimits o && min == o.min && max == o.max;
        public override int GetHashCode() => HashCode.Combine(min, max);
    }

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherFogChangelimits
    {
        private decimal minField;
        private decimal maxField;

        [System.Xml.Serialization.XmlAttribute] public decimal min { get => minField; set => minField = value; }
        [System.Xml.Serialization.XmlAttribute] public decimal max { get => maxField; set => maxField = value; }

        public override bool Equals(object obj) => obj is weatherFogChangelimits o && min == o.min && max == o.max;
        public override int GetHashCode() => HashCode.Combine(min, max);
    }

    #endregion

    #region Rain

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherRain
    {
        private weatherRainCurrent currentField;
        private weatherRainLimits limitsField;
        private weatherRainTimelimits timelimitsField;
        private weatherRainChangelimits changelimitsField;
        private weatherRainThresholds thresholdsField;

        public weatherRainCurrent current { get => currentField; set => currentField = value; }
        public weatherRainLimits limits { get => limitsField; set => limitsField = value; }
        public weatherRainTimelimits timelimits { get => timelimitsField; set => timelimitsField = value; }
        public weatherRainChangelimits changelimits { get => changelimitsField; set => changelimitsField = value; }
        public weatherRainThresholds thresholds { get => thresholdsField; set => thresholdsField = value; }

        public override bool Equals(object obj) =>
            obj is weatherRain o &&
            Equals(current, o.current) &&
            Equals(limits, o.limits) &&
            Equals(timelimits, o.timelimits) &&
            Equals(changelimits, o.changelimits) &&
            Equals(thresholds, o.thresholds);

        public override int GetHashCode() => HashCode.Combine(current, limits, timelimits, changelimits, thresholds);
    }

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherRainCurrent
    {
        private decimal actualField;
        private int timeField;
        private int durationField;

        [System.Xml.Serialization.XmlAttribute] public decimal actual { get => actualField; set => actualField = value; }
        [System.Xml.Serialization.XmlAttribute] public int time { get => timeField; set => timeField = value; }
        [System.Xml.Serialization.XmlAttribute] public int duration { get => durationField; set => durationField = value; }

        public override bool Equals(object obj) =>
            obj is weatherRainCurrent o && actual == o.actual && time == o.time && duration == o.duration;

        public override int GetHashCode() => HashCode.Combine(actual, time, duration);
    }

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherRainLimits
    {
        private decimal minField;
        private decimal maxField;

        [System.Xml.Serialization.XmlAttribute] public decimal min { get => minField; set => minField = value; }
        [System.Xml.Serialization.XmlAttribute] public decimal max { get => maxField; set => maxField = value; }

        public override bool Equals(object obj) => obj is weatherRainLimits o && min == o.min && max == o.max;
        public override int GetHashCode() => HashCode.Combine(min, max);
    }

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherRainTimelimits
    {
        private int minField;
        private int maxField;

        [System.Xml.Serialization.XmlAttribute] public int min { get => minField; set => minField = value; }
        [System.Xml.Serialization.XmlAttribute] public int max { get => maxField; set => maxField = value; }

        public override bool Equals(object obj) => obj is weatherRainTimelimits o && min == o.min && max == o.max;
        public override int GetHashCode() => HashCode.Combine(min, max);
    }

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherRainChangelimits
    {
        private decimal minField;
        private decimal maxField;

        [System.Xml.Serialization.XmlAttribute] public decimal min { get => minField; set => minField = value; }
        [System.Xml.Serialization.XmlAttribute] public decimal max { get => maxField; set => maxField = value; }

        public override bool Equals(object obj) => obj is weatherRainChangelimits o && min == o.min && max == o.max;
        public override int GetHashCode() => HashCode.Combine(min, max);
    }

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherRainThresholds
    {
        private decimal minField;
        private decimal maxField;
        private int endField;

        [System.Xml.Serialization.XmlAttribute] public decimal min { get => minField; set => minField = value; }
        [System.Xml.Serialization.XmlAttribute] public decimal max { get => maxField; set => maxField = value; }
        [System.Xml.Serialization.XmlAttribute] public int end { get => endField; set => endField = value; }

        public override bool Equals(object obj) => obj is weatherRainThresholds o && min == o.min && max == o.max && end == o.end;
        public override int GetHashCode() => HashCode.Combine(min, max, end);
    }

    #endregion

    #region WindMagnitude

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherWindMagnitude
    {
        private weatherWindMagnitudeCurrent currentField;
        private weatherWindMagnitudeLimits limitsField;
        private weatherWindMagnitudeTimelimits timelimitsField;
        private weatherWindMagnitudeChangelimits changelimitsField;

        public weatherWindMagnitudeCurrent current { get => currentField; set => currentField = value; }
        public weatherWindMagnitudeLimits limits { get => limitsField; set => limitsField = value; }
        public weatherWindMagnitudeTimelimits timelimits { get => timelimitsField; set => timelimitsField = value; }
        public weatherWindMagnitudeChangelimits changelimits { get => changelimitsField; set => changelimitsField = value; }

        public override bool Equals(object obj) =>
            obj is weatherWindMagnitude o &&
            Equals(current, o.current) &&
            Equals(limits, o.limits) &&
            Equals(timelimits, o.timelimits) &&
            Equals(changelimits, o.changelimits);

        public override int GetHashCode() => HashCode.Combine(current, limits, timelimits, changelimits);
    }

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherWindMagnitudeCurrent
    {
        private decimal actualField;
        private int timeField;
        private int durationField;

        [System.Xml.Serialization.XmlAttribute] public decimal actual { get => actualField; set => actualField = value; }
        [System.Xml.Serialization.XmlAttribute] public int time { get => timeField; set => timeField = value; }
        [System.Xml.Serialization.XmlAttribute] public int duration { get => durationField; set => durationField = value; }

        public override bool Equals(object obj) =>
            obj is weatherWindMagnitudeCurrent o && actual == o.actual && time == o.time && duration == o.duration;

        public override int GetHashCode() => HashCode.Combine(actual, time, duration);
    }

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherWindMagnitudeLimits
    {
        private decimal minField;
        private decimal maxField;

        [System.Xml.Serialization.XmlAttribute] public decimal min { get => minField; set => minField = value; }
        [System.Xml.Serialization.XmlAttribute] public decimal max { get => maxField; set => maxField = value; }

        public override bool Equals(object obj) => obj is weatherWindMagnitudeLimits o && min == o.min && max == o.max;
        public override int GetHashCode() => HashCode.Combine(min, max);
    }

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherWindMagnitudeTimelimits
    {
        private int minField;
        private int maxField;

        [System.Xml.Serialization.XmlAttribute] public int min { get => minField; set => minField = value; }
        [System.Xml.Serialization.XmlAttribute] public int max { get => maxField; set => maxField = value; }

        public override bool Equals(object obj) => obj is weatherWindMagnitudeTimelimits o && min == o.min && max == o.max;
        public override int GetHashCode() => HashCode.Combine(min, max);
    }

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherWindMagnitudeChangelimits
    {
        private decimal minField;
        private decimal maxField;

        [System.Xml.Serialization.XmlAttribute] public decimal min { get => minField; set => minField = value; }
        [System.Xml.Serialization.XmlAttribute] public decimal max { get => maxField; set => maxField = value; }

        public override bool Equals(object obj) => obj is weatherWindMagnitudeChangelimits o && min == o.min && max == o.max;
        public override int GetHashCode() => HashCode.Combine(min, max);
    }

    #endregion

    #region WindDirection

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherWindDirection
    {
        private weatherWindDirectionCurrent currentField;
        private weatherWindDirectionLimits limitsField;
        private weatherWindDirectionTimelimits timelimitsField;
        private weatherWindDirectionChangelimits changelimitsField;

        public weatherWindDirectionCurrent current { get => currentField; set => currentField = value; }
        public weatherWindDirectionLimits limits { get => limitsField; set => limitsField = value; }
        public weatherWindDirectionTimelimits timelimits { get => timelimitsField; set => timelimitsField = value; }
        public weatherWindDirectionChangelimits changelimits { get => changelimitsField; set => changelimitsField = value; }

        public override bool Equals(object obj) =>
            obj is weatherWindDirection o &&
            Equals(current, o.current) &&
            Equals(limits, o.limits) &&
            Equals(timelimits, o.timelimits) &&
            Equals(changelimits, o.changelimits);

        public override int GetHashCode() => HashCode.Combine(current, limits, timelimits, changelimits);
    }

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherWindDirectionCurrent
    {
        private decimal actualField;
        private int timeField;
        private int durationField;

        [System.Xml.Serialization.XmlAttribute] public decimal actual { get => actualField; set => actualField = value; }
        [System.Xml.Serialization.XmlAttribute] public int time { get => timeField; set => timeField = value; }
        [System.Xml.Serialization.XmlAttribute] public int duration { get => durationField; set => durationField = value; }

        public override bool Equals(object obj) =>
            obj is weatherWindDirectionCurrent o && actual == o.actual && time == o.time && duration == o.duration;

        public override int GetHashCode() => HashCode.Combine(actual, time, duration);
    }

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherWindDirectionLimits
    {
        private decimal minField;
        private decimal maxField;

        [System.Xml.Serialization.XmlAttribute] public decimal min { get => minField; set => minField = value; }
        [System.Xml.Serialization.XmlAttribute] public decimal max { get => maxField; set => maxField = value; }

        public override bool Equals(object obj) => obj is weatherWindDirectionLimits o && min == o.min && max == o.max;
        public override int GetHashCode() => HashCode.Combine(min, max);
    }

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherWindDirectionTimelimits
    {
        private int minField;
        private int maxField;

        [System.Xml.Serialization.XmlAttribute] public int min { get => minField; set => minField = value; }
        [System.Xml.Serialization.XmlAttribute] public int max { get => maxField; set => maxField = value; }

        public override bool Equals(object obj) => obj is weatherWindDirectionTimelimits o && min == o.min && max == o.max;
        public override int GetHashCode() => HashCode.Combine(min, max);
    }

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherWindDirectionChangelimits
    {
        private decimal minField;
        private decimal maxField;

        [System.Xml.Serialization.XmlAttribute] public decimal min { get => minField; set => minField = value; }
        [System.Xml.Serialization.XmlAttribute] public decimal max { get => maxField; set => maxField = value; }

        public override bool Equals(object obj) => obj is weatherWindDirectionChangelimits o && min == o.min && max == o.max;
        public override int GetHashCode() => HashCode.Combine(min, max);
    }

    #endregion

    #region Snowfall

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherSnowfall
    {
        private weatherSnowfallCurrent currentField;
        private weatherSnowfallLimits limitsField;
        private weatherSnowfallTimelimits timelimitsField;
        private weatherSnowfallChangelimits changelimitsField;
        private weatherSnowfallThresholds thresholdsField;

        public weatherSnowfallCurrent current { get => currentField; set => currentField = value; }
        public weatherSnowfallLimits limits { get => limitsField; set => limitsField = value; }
        public weatherSnowfallTimelimits timelimits { get => timelimitsField; set => timelimitsField = value; }
        public weatherSnowfallChangelimits changelimits { get => changelimitsField; set => changelimitsField = value; }
        public weatherSnowfallThresholds thresholds { get => thresholdsField; set => thresholdsField = value; }

        public override bool Equals(object obj) =>
            obj is weatherSnowfall o &&
            Equals(current, o.current) &&
            Equals(limits, o.limits) &&
            Equals(timelimits, o.timelimits) &&
            Equals(changelimits, o.changelimits) &&
            Equals(thresholds, o.thresholds);

        public override int GetHashCode() => HashCode.Combine(current, limits, timelimits, changelimits, thresholds);
    }

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherSnowfallCurrent
    {
        private decimal actualField;
        private int timeField;
        private int durationField;

        [System.Xml.Serialization.XmlAttribute] public decimal actual { get => actualField; set => actualField = value; }
        [System.Xml.Serialization.XmlAttribute] public int time { get => timeField; set => timeField = value; }
        [System.Xml.Serialization.XmlAttribute] public int duration { get => durationField; set => durationField = value; }

        public override bool Equals(object obj) =>
            obj is weatherSnowfallCurrent o && actual == o.actual && time == o.time && duration == o.duration;

        public override int GetHashCode() => HashCode.Combine(actual, time, duration);
    }

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherSnowfallLimits
    {
        private decimal minField;
        private decimal maxField;

        [System.Xml.Serialization.XmlAttribute] public decimal min { get => minField; set => minField = value; }
        [System.Xml.Serialization.XmlAttribute] public decimal max { get => maxField; set => maxField = value; }

        public override bool Equals(object obj) => obj is weatherSnowfallLimits o && min == o.min && max == o.max;
        public override int GetHashCode() => HashCode.Combine(min, max);
    }

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherSnowfallTimelimits
    {
        private int minField;
        private int maxField;

        [System.Xml.Serialization.XmlAttribute] public int min { get => minField; set => minField = value; }
        [System.Xml.Serialization.XmlAttribute] public int max { get => maxField; set => maxField = value; }

        public override bool Equals(object obj) => obj is weatherSnowfallTimelimits o && min == o.min && max == o.max;
        public override int GetHashCode() => HashCode.Combine(min, max);
    }

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherSnowfallChangelimits
    {
        private decimal minField;
        private decimal maxField;

        [System.Xml.Serialization.XmlAttribute] public decimal min { get => minField; set => minField = value; }
        [System.Xml.Serialization.XmlAttribute] public decimal max { get => maxField; set => maxField = value; }

        public override bool Equals(object obj) => obj is weatherSnowfallChangelimits o && min == o.min && max == o.max;
        public override int GetHashCode() => HashCode.Combine(min, max);
    }

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherSnowfallThresholds
    {
        private decimal minField;
        private decimal maxField;
        private int endField;

        [System.Xml.Serialization.XmlAttribute] public decimal min { get => minField; set => minField = value; }
        [System.Xml.Serialization.XmlAttribute] public decimal max { get => maxField; set => maxField = value; }
        [System.Xml.Serialization.XmlAttribute] public int end { get => endField; set => endField = value; }

        public override bool Equals(object obj) => obj is weatherSnowfallThresholds o && min == o.min && max == o.max && end == o.end;
        public override int GetHashCode() => HashCode.Combine(min, max, end);
    }

    #endregion

    #region Storm

    [Serializable, System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class weatherStorm
    {
        private decimal densityField;
        private decimal thresholdField;
        private int timeoutField;

        [System.Xml.Serialization.XmlAttribute] public decimal density { get => densityField; set => densityField = value; }
        [System.Xml.Serialization.XmlAttribute] public decimal threshold { get => thresholdField; set => thresholdField = value; }
        [System.Xml.Serialization.XmlAttribute] public int timeout { get => timeoutField; set => timeoutField = value; }

        public override bool Equals(object obj) => obj is weatherStorm o && density == o.density && threshold == o.threshold && timeout == o.timeout;
        public override int GetHashCode() => HashCode.Combine(density, threshold, timeout);
    }

    #endregion
}
