using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Day2eEditor
{
    public class cfgeventgroupsConfig : SingleFileConfigLoaderBase<eventgroupdef>
    {
        public cfgeventgroupsConfig(string path) : base(path)
        {
        }

        public override void Load()
        {
            HasErrors = false;
            _errors.Clear();

            try
            {
                Data = AppServices.GetRequired<FileService>()
                    .LoadOrCreateXml<eventgroupdef>(
                        _path,
                        createNew: () => new eventgroupdef(),
                        onError: ex => HandleLoadError(ex),
                        configName: "eventgroupdef"
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

        protected override eventgroupdef CreateDefaultData()
        {
            return new eventgroupdef();
        }

        protected override void OnAfterLoad(eventgroupdef data)
        {
            // optional
        }

        protected override IEnumerable<string> ValidateData()
        {
            return Enumerable.Empty<string>();
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

        public eventgroupdefGroup? getassociatedgroup(string name)
        {
            return Data?.group?.FirstOrDefault(x => x.name == name);
        }
    }

    [XmlRoot("eventgroupdef")]
    public partial class eventgroupdef : IEquatable<eventgroupdef>, IDeepCloneable<eventgroupdef>
    {
        [XmlElement("group")]
        public BindingList<eventgroupdefGroup> group { get; set; } = new();

        public bool Equals(eventgroupdef? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return group.SequenceEqual(other.group);
        }

        public override bool Equals(object? obj) => Equals(obj as eventgroupdef);

        public eventgroupdef Clone()
        {
            return new eventgroupdef
            {
                group = new BindingList<eventgroupdefGroup>(
                    group.Select(x => x.Clone()).ToList())
            };
        }
    }

    public partial class eventgroupdefGroup : IEquatable<eventgroupdefGroup>, IDeepCloneable<eventgroupdefGroup>
    {
        [XmlElement("child")]
        public BindingList<eventgroupdefGroupChild> child { get; set; } = new();

        [XmlAttribute]
        public string? name { get; set; }

        public override string ToString() => name ?? string.Empty;

        public bool Equals(eventgroupdefGroup? other)
        {
            if (other is null) return false;

            return string.Equals(name, other.name, StringComparison.Ordinal) &&
                   child.SequenceEqual(other.child);
        }

        public override bool Equals(object? obj) => Equals(obj as eventgroupdefGroup);

        public eventgroupdefGroup Clone()
        {
            return new eventgroupdefGroup
            {
                name = name,
                child = new BindingList<eventgroupdefGroupChild>(
                    child.Select(x => x.Clone()).ToList())
            };
        }
    }

    public partial class eventgroupdefGroupChild : IEquatable<eventgroupdefGroupChild>, IDeepCloneable<eventgroupdefGroupChild>
    {
        [XmlAttribute] public string? type { get; set; }

        [XmlAttribute] public bool spawnsecondary { get; set; }
        [XmlIgnore] public bool spawnsecondarySpecified { get; set; }

        [XmlAttribute] public int deloot { get; set; }
        [XmlIgnore] public bool delootSpecified { get; set; }

        [XmlAttribute] public int lootmax { get; set; }
        [XmlIgnore] public bool lootmaxSpecified { get; set; }

        [XmlAttribute] public int lootmin { get; set; }
        [XmlIgnore] public bool lootminSpecified { get; set; }

        [XmlAttribute] public decimal x { get; set; }
        [XmlAttribute] public decimal z { get; set; }
        [XmlAttribute] public decimal a { get; set; }

        [XmlAttribute] public decimal y { get; set; }
        [XmlIgnore] public bool ySpecified { get; set; }

        public override string ToString() => type ?? string.Empty;

        public bool Equals(eventgroupdefGroupChild? other)
        {
            if (other is null) return false;

            return
                type == other.type &&
                spawnsecondary == other.spawnsecondary &&
                spawnsecondarySpecified == other.spawnsecondarySpecified &&
                deloot == other.deloot &&
                delootSpecified == other.delootSpecified &&
                lootmax == other.lootmax &&
                lootmaxSpecified == other.lootmaxSpecified &&
                lootmin == other.lootmin &&
                lootminSpecified == other.lootminSpecified &&
                x == other.x &&
                y == other.y &&
                ySpecified == other.ySpecified &&
                z == other.z &&
                a == other.a;
        }

        public override bool Equals(object? obj) => Equals(obj as eventgroupdefGroupChild);

        public eventgroupdefGroupChild Clone()
        {
            return new eventgroupdefGroupChild
            {
                type = type,
                spawnsecondary = spawnsecondary,
                spawnsecondarySpecified = spawnsecondarySpecified,
                deloot = deloot,
                delootSpecified = delootSpecified,
                lootmax = lootmax,
                lootmaxSpecified = lootmaxSpecified,
                lootmin = lootmin,
                lootminSpecified = lootminSpecified,
                x = x,
                y = y,
                ySpecified = ySpecified,
                z = z,
                a = a
            };
        }
    }
}
