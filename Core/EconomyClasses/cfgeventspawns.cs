using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Day2eEditor
{
    public class cfgeventspawnsConfig : SingleFileConfigLoaderBase<eventposdef>
    {
        public cfgeventspawnsConfig(string path) : base(path)
        {
        }

        public override void Load()
        {
            HasErrors = false;
            _errors.Clear();

            try
            {
                Data = AppServices.GetRequired<FileService>()
                    .LoadOrCreateXml<eventposdef>(
                        _path,
                        createNew: () => new eventposdef(),
                        onError: ex => HandleLoadError(ex),
                        configName: "cfgeventspawns"
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

        public override IEnumerable<string> Save()
        {
            if (Data is null)
                return Array.Empty<string>();

            if (!AreEqual(Data, ClonedData) || IsDirty)
            {
                ClearDirty();
                AppServices.GetRequired<FileService>().SaveXml(_path, Data);
                ClonedData = CloneData(Data);
                return new[] { _path };
            }

            return Array.Empty<string>();
        }

        protected override eventposdef CreateDefaultData()
            => new eventposdef();

        protected override IEnumerable<string> ValidateData()
            => Enumerable.Empty<string>();

        public eventposdefEvent? Findevent(string eventpname)
        {
            return Data?.@event?.FirstOrDefault(x => x.name == eventpname);
        }

        public eventposdefEventPos? findeventgroup(string eventgroupname)
        {
            return Data?.@event?
                .SelectMany(e => e.pos ?? new BindingList<eventposdefEventPos>())
                .FirstOrDefault(p => p.group == eventgroupname);
        }

        public void AddNewEventSpawn(eventposdefEvent newvenspawn)
        {
            Data.@event ??= new BindingList<eventposdefEvent>();
            Data.@event.Add(newvenspawn);
            MarkDirty();
        }
    }

    [XmlRoot("eventposdef")]
    public partial class eventposdef : IEquatable<eventposdef>, IDeepCloneable<eventposdef>
    {
        [XmlElement("event")]
        public BindingList<eventposdefEvent> @event { get; set; } = new();

        public bool Equals(eventposdef? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return @event.SequenceEqual(other.@event);
        }

        public override bool Equals(object? obj) => Equals(obj as eventposdef);

        public eventposdef Clone()
        {
            return new eventposdef
            {
                @event = new BindingList<eventposdefEvent>(
                    @event.Select(x => x.Clone()).ToList())
            };
        }
    }

    public partial class eventposdefEvent : IEquatable<eventposdefEvent>, IDeepCloneable<eventposdefEvent>
    {
        [XmlElement("zone")]
        public eventposdefEventZone? zone { get; set; }

        [XmlElement("pos")]
        public BindingList<eventposdefEventPos> pos { get; set; } = new();

        [XmlAttribute] public string? name { get; set; }

        public override string ToString() => name ?? string.Empty;

        public bool Equals(eventposdefEvent? other)
        {
            if (other is null) return false;

            return
                string.Equals(name, other.name, StringComparison.Ordinal) &&
                Equals(zone, other.zone) &&
                pos.SequenceEqual(other.pos);
        }

        public override bool Equals(object? obj) => Equals(obj as eventposdefEvent);

        public eventposdefEvent Clone()
        {
            return new eventposdefEvent
            {
                name = name,
                zone = zone?.Clone(),
                pos = new BindingList<eventposdefEventPos>(
                    pos.Select(x => x.Clone()).ToList())
            };
        }
    }

    public partial class eventposdefEventZone : IEquatable<eventposdefEventZone>, IDeepCloneable<eventposdefEventZone>
    {
        [XmlAttribute] public int smin { get; set; }
        [XmlAttribute] public int smax { get; set; }
        [XmlAttribute] public int dmin { get; set; }
        [XmlAttribute] public int dmax { get; set; }
        [XmlAttribute] public int r { get; set; }

        public bool Equals(eventposdefEventZone? other)
        {
            if (other is null) return false;

            return smin == other.smin &&
                   smax == other.smax &&
                   dmin == other.dmin &&
                   dmax == other.dmax &&
                   r == other.r;
        }

        public override bool Equals(object? obj) => Equals(obj as eventposdefEventZone);

        public eventposdefEventZone Clone()
        {
            return new eventposdefEventZone
            {
                smin = smin,
                smax = smax,
                dmin = dmin,
                dmax = dmax,
                r = r
            };
        }
    }

    public partial class eventposdefEventPos : IEquatable<eventposdefEventPos>, IDeepCloneable<eventposdefEventPos>
    {
        [XmlAttribute] public decimal x { get; set; }
        [XmlAttribute] public decimal y { get; set; }
        [XmlIgnore] public bool ySpecified { get; set; }

        [XmlAttribute] public decimal z { get; set; }

        [XmlAttribute] public decimal a { get; set; }
        [XmlIgnore] public bool aSpecified { get; set; }

        [XmlAttribute] public string? group { get; set; }

        public override string ToString()
        {
            var parts = new List<string> { $"x={x}", $"z={z}" };

            if (ySpecified) parts.Add($"y={y}");
            if (aSpecified) parts.Add($"a={a}");
            if (!string.IsNullOrEmpty(group)) parts.Add($"group={group}");

            return string.Join(", ", parts);
        }

        public string ToExpansionMapString(float y, float a)
        {
            return $"{x} {y} {z}|{a} 0.0 0.0";
        }

        public bool Equals(eventposdefEventPos? other)
        {
            if (other is null) return false;

            return
                x == other.x &&
                y == other.y &&
                ySpecified == other.ySpecified &&
                z == other.z &&
                a == other.a &&
                aSpecified == other.aSpecified &&
                group == other.group;
        }

        public override bool Equals(object? obj) => Equals(obj as eventposdefEventPos);

        public eventposdefEventPos Clone()
        {
            return new eventposdefEventPos
            {
                x = x,
                y = y,
                ySpecified = ySpecified,
                z = z,
                a = a,
                aSpecified = aSpecified,
                group = group
            };
        }
    }
}
