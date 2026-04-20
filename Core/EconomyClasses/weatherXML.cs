using System;
using System.Xml.Serialization;

namespace Day2eEditor
{
    public class cfgweatherConfig : SingleFileConfigLoaderBase<weather>
    {
        public cfgweatherConfig(string path) : base(path)
        {
        }

        public override void Load()
        {
            HasErrors = false;
            _errors.Clear();

            try
            {
                Data = AppServices.GetRequired<FileService>()
                    .LoadOrCreateXml<weather>(
                        _path,
                        createNew: () => CreateDefaultData(),
                        onError: ex =>
                        {
                            HandleLoadError(ex);
                        },
                        configName: "cfgweather"
                    );

                var issues = ValidateData();
                if (issues?.Any() == true)
                {
                    Console.WriteLine("Validation issues in " + FileName + ":");
                    foreach (var msg in issues)
                        Console.WriteLine("- " + msg);

                    MarkDirty();
                }

                OnAfterLoad(Data);
                ClonedData = CloneData(Data);
            }
            catch (Exception ex)
            {
                HandleLoadError(ex);
            }
        }

        public override  IEnumerable<string> Save()
        {
            if (Data is null)
                return Array.Empty<string>();

            if (!AreEqual(Data, ClonedData) || IsDirty == true)
            {
                ClearDirty();
                AppServices.GetRequired<FileService>().SaveXml(_path, Data);
                ClonedData = CloneData(Data);
                return new[] { Path.GetFileName(_path) };
            }

            return Array.Empty<string>();
        }

        protected override weather CreateDefaultData()
        {
            return new weather();
        }

        protected override void OnAfterLoad(weather data)
        {
            // Optional post-load logic
        }

        protected override IEnumerable<string> ValidateData()
        {
            var issues = new List<string>();

            var overcast = Ensure(Data.overcast, x => Data.overcast = x, "overcast", issues);
            Ensure(overcast.current, x => overcast.current = x, "overcast/current", issues);
            Ensure(overcast.limits, x => overcast.limits = x, "overcast/limits", issues);
            Ensure(overcast.timelimits, x => overcast.timelimits = x, "overcast/timelimits", issues);
            Ensure(overcast.changelimits, x => overcast.changelimits = x, "overcast/changelimits", issues);

            var fog = Ensure(Data.fog, x => Data.fog = x, "fog", issues);
            Ensure(fog.current, x => fog.current = x, "fog/current", issues);
            Ensure(fog.limits, x => fog.limits = x, "fog/limits", issues);
            Ensure(fog.timelimits, x => fog.timelimits = x, "fog/timelimits", issues);
            Ensure(fog.changelimits, x => fog.changelimits = x, "fog/changelimits", issues);

            var rain = Ensure(Data.rain, x => Data.rain = x, "rain", issues);
            Ensure(rain.current, x => rain.current = x, "rain/current", issues);
            Ensure(rain.limits, x => rain.limits = x, "rain/limits", issues);
            Ensure(rain.timelimits, x => rain.timelimits = x, "rain/timelimits", issues);
            Ensure(rain.changelimits, x => rain.changelimits = x, "rain/changelimits", issues);
            Ensure(rain.thresholds, x => rain.thresholds = x, "rain/thresholds", issues);

            var windMagnitude = Ensure(Data.windMagnitude, x => Data.windMagnitude = x, "windMagnitude", issues);
            Ensure(windMagnitude.current, x => windMagnitude.current = x, "windMagnitude/current", issues);
            Ensure(windMagnitude.limits, x => windMagnitude.limits = x, "windMagnitude/limits", issues);
            Ensure(windMagnitude.timelimits, x => windMagnitude.timelimits = x, "windMagnitude/timelimits", issues);
            Ensure(windMagnitude.changelimits, x => windMagnitude.changelimits = x, "windMagnitude/changelimits", issues);

            var windDirection = Ensure(Data.windDirection, x => Data.windDirection = x, "windDirection", issues);
            Ensure(windDirection.current, x => windDirection.current = x, "windDirection/current", issues);
            Ensure(windDirection.limits, x => windDirection.limits = x, "windDirection/limits", issues);
            Ensure(windDirection.timelimits, x => windDirection.timelimits = x, "windDirection/timelimits", issues);
            Ensure(windDirection.changelimits, x => windDirection.changelimits = x, "windDirection/changelimits", issues);

            var snowfall = Ensure(Data.snowfall, x => Data.snowfall = x, "snowfall", issues);
            Ensure(snowfall.current, x => snowfall.current = x, "snowfall/current", issues);
            Ensure(snowfall.limits, x => snowfall.limits = x, "snowfall/limits", issues);
            Ensure(snowfall.timelimits, x => snowfall.timelimits = x, "snowfall/timelimits", issues);
            Ensure(snowfall.changelimits, x => snowfall.changelimits = x, "snowfall/changelimits", issues);
            Ensure(snowfall.thresholds, x => snowfall.thresholds = x, "snowfall/thresholds", issues);

            Ensure(Data.storm, x => Data.storm = x, "storm", issues);

            return issues;
        }

        private static T Ensure<T>(T? value, Action<T> assign, string path, List<string> issues)
            where T : class, new()
        {
            if (value is not null)
                return value;

            var created = new T();
            assign(created);
            issues.Add($"Missing {path} section. Created default section.");
            return created;
        }
    }

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public partial class weather : IEquatable<weather>, IDeepCloneable<weather>
    {
        private weatherOvercast? _overcast;
        private weatherFog? _fog;
        private weatherRain? _rain;
        private weatherWindMagnitude? _windMagnitude;
        private weatherWindDirection? _windDirection;
        private weatherSnowfall? _snowfall;
        private weatherStorm? _storm;

        public weatherOvercast overcast
        {
            get => _overcast ??= new weatherOvercast();
            set => _overcast = value;
        }

        public weatherFog fog
        {
            get => _fog;
            set => _fog = value;
        }

        public weatherRain rain
        {
            get => _rain;
            set => _rain = value;
        }

        public weatherWindMagnitude windMagnitude
        {
            get => _windMagnitude;
            set => _windMagnitude = value;
        }

        public weatherWindDirection windDirection
        {
            get => _windDirection;
            set => _windDirection = value;
        }

        public weatherSnowfall snowfall
        {
            get => _snowfall;
            set => _snowfall = value;
        }

        public weatherStorm storm
        {
            get => _storm;
            set => _storm = value;
        }

        [XmlAttribute]
        public int reset { get; set; }

        [XmlAttribute]
        public int enable { get; set; }

        public bool Equals(weather? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(overcast, other.overcast) &&
                   Equals(fog, other.fog) &&
                   Equals(rain, other.rain) &&
                   Equals(windMagnitude, other.windMagnitude) &&
                   Equals(windDirection, other.windDirection) &&
                   Equals(snowfall, other.snowfall) &&
                   Equals(storm, other.storm) &&
                   reset == other.reset &&
                   enable == other.enable;
        }

        public override bool Equals(object? obj) => Equals(obj as weather);

        public weather Clone()
        {
            return new weather
            {
                overcast = overcast.Clone(),
                fog = fog.Clone(),
                rain = rain.Clone(),
                windMagnitude = windMagnitude.Clone(),
                windDirection = windDirection.Clone(),
                snowfall = snowfall.Clone(),
                storm = storm.Clone(),
                reset = reset,
                enable = enable
            };
        }
    }

    #region Overcast

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherOvercast : IEquatable<weatherOvercast>, IDeepCloneable<weatherOvercast>
    {
        private weatherOvercastCurrent? _current;
        private weatherOvercastLimits? _limits;
        private weatherOvercastTimelimits? _timelimits;
        private weatherOvercastChangelimits? _changelimits;

        public weatherOvercastCurrent current
        {
            get => _current;
            set => _current = value;
        }

        public weatherOvercastLimits limits
        {
            get => _limits;
            set => _limits = value;
        }

        public weatherOvercastTimelimits timelimits
        {
            get => _timelimits;
            set => _timelimits = value;
        }

        public weatherOvercastChangelimits changelimits
        {
            get => _changelimits;
            set => _changelimits = value;
        }

        public bool Equals(weatherOvercast? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(current, other.current) &&
                   Equals(limits, other.limits) &&
                   Equals(timelimits, other.timelimits) &&
                   Equals(changelimits, other.changelimits);
        }

        public override bool Equals(object? obj) => Equals(obj as weatherOvercast);

        public weatherOvercast Clone()
        {
            return new weatherOvercast
            {
                current = current.Clone(),
                limits = limits.Clone(),
                timelimits = timelimits.Clone(),
                changelimits = changelimits.Clone()
            };
        }
    }

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherOvercastCurrent : IEquatable<weatherOvercastCurrent>, IDeepCloneable<weatherOvercastCurrent>
    {
        [XmlAttribute] public decimal actual { get; set; }
        [XmlAttribute] public int time { get; set; }
        [XmlAttribute] public int duration { get; set; }

        public bool Equals(weatherOvercastCurrent? other)
        {
            if (other is null) return false;
            return actual == other.actual && time == other.time && duration == other.duration;
        }

        public override bool Equals(object? obj) => Equals(obj as weatherOvercastCurrent);

        public weatherOvercastCurrent Clone()
        {
            return new weatherOvercastCurrent
            {
                actual = actual,
                time = time,
                duration = duration
            };
        }
    }

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherOvercastLimits : IEquatable<weatherOvercastLimits>, IDeepCloneable<weatherOvercastLimits>
    {
        [XmlAttribute] public decimal min { get; set; }
        [XmlAttribute] public decimal max { get; set; }

        public bool Equals(weatherOvercastLimits? other)
        {
            if (other is null) return false;
            return min == other.min && max == other.max;
        }

        public override bool Equals(object? obj) => Equals(obj as weatherOvercastLimits);

        public weatherOvercastLimits Clone()
        {
            return new weatherOvercastLimits
            {
                min = min,
                max = max
            };
        }
    }

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherOvercastTimelimits : IEquatable<weatherOvercastTimelimits>, IDeepCloneable<weatherOvercastTimelimits>
    {
        [XmlAttribute] public int min { get; set; }
        [XmlAttribute] public int max { get; set; }

        public bool Equals(weatherOvercastTimelimits? other)
        {
            if (other is null) return false;
            return min == other.min && max == other.max;
        }

        public override bool Equals(object? obj) => Equals(obj as weatherOvercastTimelimits);

        public weatherOvercastTimelimits Clone()
        {
            return new weatherOvercastTimelimits
            {
                min = min,
                max = max
            };
        }
    }

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherOvercastChangelimits : IEquatable<weatherOvercastChangelimits>, IDeepCloneable<weatherOvercastChangelimits>
    {
        [XmlAttribute] public decimal min { get; set; }
        [XmlAttribute] public decimal max { get; set; }

        public bool Equals(weatherOvercastChangelimits? other)
        {
            if (other is null) return false;
            return min == other.min && max == other.max;
        }

        public override bool Equals(object? obj) => Equals(obj as weatherOvercastChangelimits);

        public weatherOvercastChangelimits Clone()
        {
            return new weatherOvercastChangelimits
            {
                min = min,
                max = max
            };
        }
    }

    #endregion

    #region Fog

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherFog : IEquatable<weatherFog>, IDeepCloneable<weatherFog>
    {
        private weatherFogCurrent? _current;
        private weatherFogLimits? _limits;
        private weatherFogTimelimits? _timelimits;
        private weatherFogChangelimits? _changelimits;

        public weatherFogCurrent current
        {
            get => _current;
            set => _current = value;
        }

        public weatherFogLimits limits
        {
            get => _limits;
            set => _limits = value;
        }

        public weatherFogTimelimits timelimits
        {
            get => _timelimits;
            set => _timelimits = value;
        }

        public weatherFogChangelimits changelimits
        {
            get => _changelimits;
            set => _changelimits = value;
        }

        public bool Equals(weatherFog? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(current, other.current) &&
                   Equals(limits, other.limits) &&
                   Equals(timelimits, other.timelimits) &&
                   Equals(changelimits, other.changelimits);
        }

        public override bool Equals(object? obj) => Equals(obj as weatherFog);

        public weatherFog Clone()
        {
            return new weatherFog
            {
                current = current.Clone(),
                limits = limits.Clone(),
                timelimits = timelimits.Clone(),
                changelimits = changelimits.Clone()
            };
        }
    }

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherFogCurrent : IEquatable<weatherFogCurrent>, IDeepCloneable<weatherFogCurrent>
    {
        [XmlAttribute] public decimal actual { get; set; }
        [XmlAttribute] public int time { get; set; }
        [XmlAttribute] public int duration { get; set; }

        public bool Equals(weatherFogCurrent? other)
        {
            if (other is null) return false;
            return actual == other.actual && time == other.time && duration == other.duration;
        }

        public override bool Equals(object? obj) => Equals(obj as weatherFogCurrent);

        public weatherFogCurrent Clone()
        {
            return new weatherFogCurrent
            {
                actual = actual,
                time = time,
                duration = duration
            };
        }
    }

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherFogLimits : IEquatable<weatherFogLimits>, IDeepCloneable<weatherFogLimits>
    {
        [XmlAttribute] public decimal min { get; set; }
        [XmlAttribute] public decimal max { get; set; }

        public bool Equals(weatherFogLimits? other)
        {
            if (other is null) return false;
            return min == other.min && max == other.max;
        }

        public override bool Equals(object? obj) => Equals(obj as weatherFogLimits);

        public weatherFogLimits Clone()
        {
            return new weatherFogLimits
            {
                min = min,
                max = max
            };
        }
    }

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherFogTimelimits : IEquatable<weatherFogTimelimits>, IDeepCloneable<weatherFogTimelimits>
    {
        [XmlAttribute] public int min { get; set; }
        [XmlAttribute] public int max { get; set; }

        public bool Equals(weatherFogTimelimits? other)
        {
            if (other is null) return false;
            return min == other.min && max == other.max;
        }

        public override bool Equals(object? obj) => Equals(obj as weatherFogTimelimits);

        public weatherFogTimelimits Clone()
        {
            return new weatherFogTimelimits
            {
                min = min,
                max = max
            };
        }
    }

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherFogChangelimits : IEquatable<weatherFogChangelimits>, IDeepCloneable<weatherFogChangelimits>
    {
        [XmlAttribute] public decimal min { get; set; }
        [XmlAttribute] public decimal max { get; set; }

        public bool Equals(weatherFogChangelimits? other)
        {
            if (other is null) return false;
            return min == other.min && max == other.max;
        }

        public override bool Equals(object? obj) => Equals(obj as weatherFogChangelimits);

        public weatherFogChangelimits Clone()
        {
            return new weatherFogChangelimits
            {
                min = min,
                max = max
            };
        }
    }

    #endregion

    #region Rain

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherRain : IEquatable<weatherRain>, IDeepCloneable<weatherRain>
    {
        private weatherRainCurrent? _current;
        private weatherRainLimits? _limits;
        private weatherRainTimelimits? _timelimits;
        private weatherRainChangelimits? _changelimits;
        private weatherRainThresholds? _thresholds;

        public weatherRainCurrent current
        {
            get => _current;
            set => _current = value;
        }

        public weatherRainLimits limits
        {
            get => _limits;
            set => _limits = value;
        }

        public weatherRainTimelimits timelimits
        {
            get => _timelimits;
            set => _timelimits = value;
        }

        public weatherRainChangelimits changelimits
        {
            get => _changelimits;
            set => _changelimits = value;
        }

        public weatherRainThresholds thresholds
        {
            get => _thresholds;
            set => _thresholds = value;
        }

        public bool Equals(weatherRain? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(current, other.current) &&
                   Equals(limits, other.limits) &&
                   Equals(timelimits, other.timelimits) &&
                   Equals(changelimits, other.changelimits) &&
                   Equals(thresholds, other.thresholds);
        }

        public override bool Equals(object? obj) => Equals(obj as weatherRain);

        public weatherRain Clone()
        {
            return new weatherRain
            {
                current = current.Clone(),
                limits = limits.Clone(),
                timelimits = timelimits.Clone(),
                changelimits = changelimits.Clone(),
                thresholds = thresholds.Clone()
            };
        }
    }

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherRainCurrent : IEquatable<weatherRainCurrent>, IDeepCloneable<weatherRainCurrent>
    {
        [XmlAttribute] public decimal actual { get; set; }
        [XmlAttribute] public int time { get; set; }
        [XmlAttribute] public int duration { get; set; }

        public bool Equals(weatherRainCurrent? other)
        {
            if (other is null) return false;
            return actual == other.actual && time == other.time && duration == other.duration;
        }

        public override bool Equals(object? obj) => Equals(obj as weatherRainCurrent);

        public weatherRainCurrent Clone()
        {
            return new weatherRainCurrent
            {
                actual = actual,
                time = time,
                duration = duration
            };
        }
    }

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherRainLimits : IEquatable<weatherRainLimits>, IDeepCloneable<weatherRainLimits>
    {
        [XmlAttribute] public decimal min { get; set; }
        [XmlAttribute] public decimal max { get; set; }

        public bool Equals(weatherRainLimits? other)
        {
            if (other is null) return false;
            return min == other.min && max == other.max;
        }

        public override bool Equals(object? obj) => Equals(obj as weatherRainLimits);

        public weatherRainLimits Clone()
        {
            return new weatherRainLimits
            {
                min = min,
                max = max
            };
        }
    }

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherRainTimelimits : IEquatable<weatherRainTimelimits>, IDeepCloneable<weatherRainTimelimits>
    {
        [XmlAttribute] public int min { get; set; }
        [XmlAttribute] public int max { get; set; }

        public bool Equals(weatherRainTimelimits? other)
        {
            if (other is null) return false;
            return min == other.min && max == other.max;
        }

        public override bool Equals(object? obj) => Equals(obj as weatherRainTimelimits);

        public weatherRainTimelimits Clone()
        {
            return new weatherRainTimelimits
            {
                min = min,
                max = max
            };
        }
    }

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherRainChangelimits : IEquatable<weatherRainChangelimits>, IDeepCloneable<weatherRainChangelimits>
    {
        [XmlAttribute] public decimal min { get; set; }
        [XmlAttribute] public decimal max { get; set; }

        public bool Equals(weatherRainChangelimits? other)
        {
            if (other is null) return false;
            return min == other.min && max == other.max;
        }

        public override bool Equals(object? obj) => Equals(obj as weatherRainChangelimits);

        public weatherRainChangelimits Clone()
        {
            return new weatherRainChangelimits
            {
                min = min,
                max = max
            };
        }
    }

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherRainThresholds : IEquatable<weatherRainThresholds>, IDeepCloneable<weatherRainThresholds>
    {
        [XmlAttribute] public decimal min { get; set; }
        [XmlAttribute] public decimal max { get; set; }
        [XmlAttribute] public int end { get; set; }

        public bool Equals(weatherRainThresholds? other)
        {
            if (other is null) return false;
            return min == other.min && max == other.max && end == other.end;
        }

        public override bool Equals(object? obj) => Equals(obj as weatherRainThresholds);

        public weatherRainThresholds Clone()
        {
            return new weatherRainThresholds
            {
                min = min,
                max = max,
                end = end
            };
        }
    }

    #endregion

    #region WindMagnitude

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherWindMagnitude : IEquatable<weatherWindMagnitude>, IDeepCloneable<weatherWindMagnitude>
    {
        private weatherWindMagnitudeCurrent? _current;
        private weatherWindMagnitudeLimits? _limits;
        private weatherWindMagnitudeTimelimits? _timelimits;
        private weatherWindMagnitudeChangelimits? _changelimits;

        public weatherWindMagnitudeCurrent current
        {
            get => _current ;
            set => _current = value;
        }

        public weatherWindMagnitudeLimits limits
        {
            get => _limits;
            set => _limits = value;
        }

        public weatherWindMagnitudeTimelimits timelimits
        {
            get => _timelimits;
            set => _timelimits = value;
        }

        public weatherWindMagnitudeChangelimits changelimits
        {
            get => _changelimits;
            set => _changelimits = value;
        }

        public bool Equals(weatherWindMagnitude? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(current, other.current) &&
                   Equals(limits, other.limits) &&
                   Equals(timelimits, other.timelimits) &&
                   Equals(changelimits, other.changelimits);
        }

        public override bool Equals(object? obj) => Equals(obj as weatherWindMagnitude);

        public weatherWindMagnitude Clone()
        {
            return new weatherWindMagnitude
            {
                current = current.Clone(),
                limits = limits.Clone(),
                timelimits = timelimits.Clone(),
                changelimits = changelimits.Clone()
            };
        }
    }

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherWindMagnitudeCurrent : IEquatable<weatherWindMagnitudeCurrent>, IDeepCloneable<weatherWindMagnitudeCurrent>
    {
        [XmlAttribute] public decimal actual { get; set; }
        [XmlAttribute] public int time { get; set; }
        [XmlAttribute] public int duration { get; set; }

        public bool Equals(weatherWindMagnitudeCurrent? other)
        {
            if (other is null) return false;
            return actual == other.actual && time == other.time && duration == other.duration;
        }

        public override bool Equals(object? obj) => Equals(obj as weatherWindMagnitudeCurrent);

        public weatherWindMagnitudeCurrent Clone()
        {
            return new weatherWindMagnitudeCurrent
            {
                actual = actual,
                time = time,
                duration = duration
            };
        }
    }

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherWindMagnitudeLimits : IEquatable<weatherWindMagnitudeLimits>, IDeepCloneable<weatherWindMagnitudeLimits>
    {
        [XmlAttribute] public decimal min { get; set; }
        [XmlAttribute] public decimal max { get; set; }

        public bool Equals(weatherWindMagnitudeLimits? other)
        {
            if (other is null) return false;
            return min == other.min && max == other.max;
        }

        public override bool Equals(object? obj) => Equals(obj as weatherWindMagnitudeLimits);

        public weatherWindMagnitudeLimits Clone()
        {
            return new weatherWindMagnitudeLimits
            {
                min = min,
                max = max
            };
        }
    }

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherWindMagnitudeTimelimits : IEquatable<weatherWindMagnitudeTimelimits>, IDeepCloneable<weatherWindMagnitudeTimelimits>
    {
        [XmlAttribute] public int min { get; set; }
        [XmlAttribute] public int max { get; set; }

        public bool Equals(weatherWindMagnitudeTimelimits? other)
        {
            if (other is null) return false;
            return min == other.min && max == other.max;
        }

        public override bool Equals(object? obj) => Equals(obj as weatherWindMagnitudeTimelimits);

        public weatherWindMagnitudeTimelimits Clone()
        {
            return new weatherWindMagnitudeTimelimits
            {
                min = min,
                max = max
            };
        }
    }

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherWindMagnitudeChangelimits : IEquatable<weatherWindMagnitudeChangelimits>, IDeepCloneable<weatherWindMagnitudeChangelimits>
    {
        [XmlAttribute] public decimal min { get; set; }
        [XmlAttribute] public decimal max { get; set; }

        public bool Equals(weatherWindMagnitudeChangelimits? other)
        {
            if (other is null) return false;
            return min == other.min && max == other.max;
        }

        public override bool Equals(object? obj) => Equals(obj as weatherWindMagnitudeChangelimits);

        public weatherWindMagnitudeChangelimits Clone()
        {
            return new weatherWindMagnitudeChangelimits
            {
                min = min,
                max = max
            };
        }
    }

    #endregion

    #region WindDirection

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherWindDirection : IEquatable<weatherWindDirection>, IDeepCloneable<weatherWindDirection>
    {
        private weatherWindDirectionCurrent? _current;
        private weatherWindDirectionLimits? _limits;
        private weatherWindDirectionTimelimits? _timelimits;
        private weatherWindDirectionChangelimits? _changelimits;

        public weatherWindDirectionCurrent current
        {
            get => _current;
            set => _current = value;
        }

        public weatherWindDirectionLimits limits
        {
            get => _limits;
            set => _limits = value;
        }

        public weatherWindDirectionTimelimits timelimits
        {
            get => _timelimits;
            set => _timelimits = value;
        }

        public weatherWindDirectionChangelimits changelimits
        {
            get => _changelimits;
            set => _changelimits = value;
        }

        public bool Equals(weatherWindDirection? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(current, other.current) &&
                   Equals(limits, other.limits) &&
                   Equals(timelimits, other.timelimits) &&
                   Equals(changelimits, other.changelimits);
        }

        public override bool Equals(object? obj) => Equals(obj as weatherWindDirection);

        public weatherWindDirection Clone()
        {
            return new weatherWindDirection
            {
                current = current.Clone(),
                limits = limits.Clone(),
                timelimits = timelimits.Clone(),
                changelimits = changelimits.Clone()
            };
        }
    }

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherWindDirectionCurrent : IEquatable<weatherWindDirectionCurrent>, IDeepCloneable<weatherWindDirectionCurrent>
    {
        [XmlAttribute] public decimal actual { get; set; }
        [XmlAttribute] public int time { get; set; }
        [XmlAttribute] public int duration { get; set; }

        public bool Equals(weatherWindDirectionCurrent? other)
        {
            if (other is null) return false;
            return actual == other.actual && time == other.time && duration == other.duration;
        }

        public override bool Equals(object? obj) => Equals(obj as weatherWindDirectionCurrent);

        public weatherWindDirectionCurrent Clone()
        {
            return new weatherWindDirectionCurrent
            {
                actual = actual,
                time = time,
                duration = duration
            };
        }
    }

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherWindDirectionLimits : IEquatable<weatherWindDirectionLimits>, IDeepCloneable<weatherWindDirectionLimits>
    {
        [XmlAttribute] public decimal min { get; set; }
        [XmlAttribute] public decimal max { get; set; }

        public bool Equals(weatherWindDirectionLimits? other)
        {
            if (other is null) return false;
            return min == other.min && max == other.max;
        }

        public override bool Equals(object? obj) => Equals(obj as weatherWindDirectionLimits);

        public weatherWindDirectionLimits Clone()
        {
            return new weatherWindDirectionLimits
            {
                min = min,
                max = max
            };
        }
    }

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherWindDirectionTimelimits : IEquatable<weatherWindDirectionTimelimits>, IDeepCloneable<weatherWindDirectionTimelimits>
    {
        [XmlAttribute] public int min { get; set; }
        [XmlAttribute] public int max { get; set; }

        public bool Equals(weatherWindDirectionTimelimits? other)
        {
            if (other is null) return false;
            return min == other.min && max == other.max;
        }

        public override bool Equals(object? obj) => Equals(obj as weatherWindDirectionTimelimits);

        public weatherWindDirectionTimelimits Clone()
        {
            return new weatherWindDirectionTimelimits
            {
                min = min,
                max = max
            };
        }
    }

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherWindDirectionChangelimits : IEquatable<weatherWindDirectionChangelimits>, IDeepCloneable<weatherWindDirectionChangelimits>
    {
        [XmlAttribute] public decimal min { get; set; }
        [XmlAttribute] public decimal max { get; set; }

        public bool Equals(weatherWindDirectionChangelimits? other)
        {
            if (other is null) return false;
            return min == other.min && max == other.max;
        }

        public override bool Equals(object? obj) => Equals(obj as weatherWindDirectionChangelimits);

        public weatherWindDirectionChangelimits Clone()
        {
            return new weatherWindDirectionChangelimits
            {
                min = min,
                max = max
            };
        }
    }

    #endregion

    #region Snowfall

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherSnowfall : IEquatable<weatherSnowfall>, IDeepCloneable<weatherSnowfall>
    {
        private weatherSnowfallCurrent? _current;
        private weatherSnowfallLimits? _limits;
        private weatherSnowfallTimelimits? _timelimits;
        private weatherSnowfallChangelimits? _changelimits;
        private weatherSnowfallThresholds? _thresholds;

        public weatherSnowfallCurrent current
        {
            get => _current ;
            set => _current = value;
        }

        public weatherSnowfallLimits limits
        {
            get => _limits;
            set => _limits = value;
        }

        public weatherSnowfallTimelimits timelimits
        {
            get => _timelimits;
            set => _timelimits = value;
        }

        public weatherSnowfallChangelimits changelimits
        {
            get => _changelimits ;
            set => _changelimits = value;
        }

        public weatherSnowfallThresholds thresholds
        {
            get => _thresholds;
            set => _thresholds = value;
        }

        public bool Equals(weatherSnowfall? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(current, other.current) &&
                   Equals(limits, other.limits) &&
                   Equals(timelimits, other.timelimits) &&
                   Equals(changelimits, other.changelimits) &&
                   Equals(thresholds, other.thresholds);
        }

        public override bool Equals(object? obj) => Equals(obj as weatherSnowfall);

        public weatherSnowfall Clone()
        {
            return new weatherSnowfall
            {
                current = current.Clone(),
                limits = limits.Clone(),
                timelimits = timelimits.Clone(),
                changelimits = changelimits.Clone(),
                thresholds = thresholds.Clone()
            };
        }
    }

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherSnowfallCurrent : IEquatable<weatherSnowfallCurrent>, IDeepCloneable<weatherSnowfallCurrent>
    {
        [XmlAttribute] public decimal actual { get; set; }
        [XmlAttribute] public int time { get; set; }
        [XmlAttribute] public int duration { get; set; }

        public bool Equals(weatherSnowfallCurrent? other)
        {
            if (other is null) return false;
            return actual == other.actual && time == other.time && duration == other.duration;
        }

        public override bool Equals(object? obj) => Equals(obj as weatherSnowfallCurrent);

        public weatherSnowfallCurrent Clone()
        {
            return new weatherSnowfallCurrent
            {
                actual = actual,
                time = time,
                duration = duration
            };
        }
    }

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherSnowfallLimits : IEquatable<weatherSnowfallLimits>, IDeepCloneable<weatherSnowfallLimits>
    {
        [XmlAttribute] public decimal min { get; set; }
        [XmlAttribute] public decimal max { get; set; }

        public bool Equals(weatherSnowfallLimits? other)
        {
            if (other is null) return false;
            return min == other.min && max == other.max;
        }

        public override bool Equals(object? obj) => Equals(obj as weatherSnowfallLimits);

        public weatherSnowfallLimits Clone()
        {
            return new weatherSnowfallLimits
            {
                min = min,
                max = max
            };
        }
    }

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherSnowfallTimelimits : IEquatable<weatherSnowfallTimelimits>, IDeepCloneable<weatherSnowfallTimelimits>
    {
        [XmlAttribute] public int min { get; set; }
        [XmlAttribute] public int max { get; set; }

        public bool Equals(weatherSnowfallTimelimits? other)
        {
            if (other is null) return false;
            return min == other.min && max == other.max;
        }

        public override bool Equals(object? obj) => Equals(obj as weatherSnowfallTimelimits);

        public weatherSnowfallTimelimits Clone()
        {
            return new weatherSnowfallTimelimits
            {
                min = min,
                max = max
            };
        }
    }

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherSnowfallChangelimits : IEquatable<weatherSnowfallChangelimits>, IDeepCloneable<weatherSnowfallChangelimits>
    {
        [XmlAttribute] public decimal min { get; set; }
        [XmlAttribute] public decimal max { get; set; }

        public bool Equals(weatherSnowfallChangelimits? other)
        {
            if (other is null) return false;
            return min == other.min && max == other.max;
        }

        public override bool Equals(object? obj) => Equals(obj as weatherSnowfallChangelimits);

        public weatherSnowfallChangelimits Clone()
        {
            return new weatherSnowfallChangelimits
            {
                min = min,
                max = max
            };
        }
    }

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherSnowfallThresholds : IEquatable<weatherSnowfallThresholds>, IDeepCloneable<weatherSnowfallThresholds>
    {
        [XmlAttribute] public decimal min { get; set; }
        [XmlAttribute] public decimal max { get; set; }
        [XmlAttribute] public int end { get; set; }

        public bool Equals(weatherSnowfallThresholds? other)
        {
            if (other is null) return false;
            return min == other.min && max == other.max && end == other.end;
        }

        public override bool Equals(object? obj) => Equals(obj as weatherSnowfallThresholds);

        public weatherSnowfallThresholds Clone()
        {
            return new weatherSnowfallThresholds
            {
                min = min,
                max = max,
                end = end
            };
        }
    }

    #endregion

    #region Storm

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class weatherStorm : IEquatable<weatherStorm>, IDeepCloneable<weatherStorm>
    {
        [XmlAttribute] public decimal density { get; set; }
        [XmlAttribute] public decimal threshold { get; set; }
        [XmlAttribute] public int timeout { get; set; }

        public bool Equals(weatherStorm? other)
        {
            if (other is null) return false;
            return density == other.density &&
                   threshold == other.threshold &&
                   timeout == other.timeout;
        }

        public override bool Equals(object? obj) => Equals(obj as weatherStorm);

        public weatherStorm Clone()
        {
            return new weatherStorm
            {
                density = density,
                threshold = threshold,
                timeout = timeout
            };
        }
    }

    #endregion
}