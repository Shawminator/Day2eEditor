using System.ComponentModel;
using System.Xml.Serialization;

namespace Day2eEditor
{
    public class TerritoriesConfig : MultiFileConfigLoaderBase<territorytype>
    {
        public TerritoriesConfig(string basePath) : base(basePath)
        {
        }
        public override void Load()
        {
            ResetState();

            var files = AppServices.GetRequired<EconomyManager>()
                .cfgenvironmentConfig.Data.territories.file;

            foreach (var envFile in files ?? new BindingList<envTerritoriesFile>())
            {
                var fullPath = Path.Combine(BasePath, envFile.path ?? string.Empty);

                try
                {
                    var item = LoadItem(fullPath);
                    OnAfterItemLoad(item, fullPath);
                    _clonedItems[GetID(item)] = item.Clone();
                    MutableItems.Add(item);
                }
                catch (Exception ex)
                {
                    HasErrors = true;
                    HandleItemError(fullPath, ex);
                }
            }

            OnAfterLoadAll();
        }

        protected override territorytype LoadItem(string filePath)
        {
            var data = AppServices.GetRequired<FileService>().LoadOrCreateXml<territorytype>(
                filePath,
                createNew: () => new territorytype(),
                onError: ex =>
                {
                    throw ex;
                },
                configName: "territorytype"
            );

            data.SetPath(filePath);
            return data;
        }

        protected override void SaveItem(territorytype item)
        {
            AppServices.GetRequired<FileService>().SaveXml(item.FilePath, item);
            item.IsDirty = false;
        }

        protected override string GetItemFileName(territorytype item) => item.FileName;

        protected override Guid GetID(territorytype item) => item.Id;

        protected override bool ShouldDelete(territorytype item) => item.ToDelete;

        protected override void DeleteItemFile(territorytype item)
        {
            if (!string.IsNullOrWhiteSpace(item.FilePath) && File.Exists(item.FilePath))
            {
                File.Delete(item.FilePath);
                Helper.DeleteEmptyFoldersUpToBase(
                    Path.GetDirectoryName(item.FilePath),
                    AppServices.GetRequired<EconomyManager>().basePath);
            }
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot("territory-type", Namespace = "", IsNullable = false)]
    public partial class territorytype : IDeepCloneable<territorytype>, IEquatable<territorytype>
    {
        [XmlIgnore]
        private string _path = string.Empty;

        [XmlIgnore]
        public string FileName => Path.GetFileName(_path);

        [XmlIgnore]
        public string FilePath => _path;

        [XmlIgnore]
        public Guid Id { get; set; } = Guid.NewGuid();

        [XmlIgnore]
        public bool IsDirty { get; set; }

        [XmlIgnore]
        public bool ToDelete { get; set; }

        private BindingList<territorytypeTerritory>? _territory;

        [XmlElement("territory")]
        public BindingList<territorytypeTerritory> territory
        {
            get => _territory ??= new BindingList<territorytypeTerritory>();
            set => _territory = value;
        }

        public void SetPath(string path)
        {
            _path = path;
        }

        public territorytype Clone()
        {
            var clone = new territorytype
            {
                Id = Id,
                IsDirty = IsDirty,
                ToDelete = ToDelete,
                territory = new BindingList<territorytypeTerritory>(
                    territory.Select(x => x.Clone()).ToList())
            };

            clone.SetPath(_path);
            return clone;
        }

        public bool Equals(territorytype? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Id == other.Id &&
                   string.Equals(_path, other._path, StringComparison.OrdinalIgnoreCase) &&
                   ToDelete == other.ToDelete &&
                   territory.SequenceEqual(other.territory);
        }

        public override bool Equals(object? obj) => Equals(obj as territorytype);
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class territorytypeTerritory : IDeepCloneable<territorytypeTerritory>, IEquatable<territorytypeTerritory>
    {
        private BindingList<territorytypeTerritoryZone>? _zone;

        [XmlElement("zone")]
        public BindingList<territorytypeTerritoryZone> zone
        {
            get => _zone ??= new BindingList<territorytypeTerritoryZone>();
            set => _zone = value;
        }

        [XmlAttribute]
        public long color { get; set; }

        public territorytypeTerritory Clone()
        {
            return new territorytypeTerritory
            {
                color = color,
                zone = new BindingList<territorytypeTerritoryZone>(zone.Select(x => x.Clone()).ToList())
            };
        }

        public bool Equals(territorytypeTerritory? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return color == other.color &&
                   zone.SequenceEqual(other.zone);
        }

        public override bool Equals(object? obj) => Equals(obj as territorytypeTerritory);
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class territorytypeTerritoryZone : IDeepCloneable<territorytypeTerritoryZone>, IEquatable<territorytypeTerritoryZone>
    {
        [XmlAttribute]
        public string? name { get; set; }

        [XmlAttribute]
        public int smin { get; set; }

        [XmlAttribute]
        public int smax { get; set; }

        [XmlAttribute]
        public int dmin { get; set; }

        [XmlAttribute]
        public int dmax { get; set; }

        [XmlAttribute]
        public decimal x { get; set; }

        [XmlAttribute]
        public decimal y { get; set; }

        [XmlIgnore]
        public bool ySpecified { get; set; }

        [XmlAttribute]
        public decimal z { get; set; }

        [XmlAttribute]
        public decimal r { get; set; }

        public override string ToString()
        {
            return $"Name:{name}, X:{x}, Z:{z}";
        }

        public territorytypeTerritoryZone Clone()
        {
            return new territorytypeTerritoryZone
            {
                name = name,
                smin = smin,
                smax = smax,
                dmin = dmin,
                dmax = dmax,
                x = x,
                y = y,
                ySpecified = ySpecified,
                z = z,
                r = r
            };
        }

        public bool Equals(territorytypeTerritoryZone? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Equals(name, other.name, StringComparison.Ordinal) &&
                   smin == other.smin &&
                   smax == other.smax &&
                   dmin == other.dmin &&
                   dmax == other.dmax &&
                   x == other.x &&
                   y == other.y &&
                   ySpecified == other.ySpecified &&
                   z == other.z &&
                   r == other.r;
        }

        public override bool Equals(object? obj) => Equals(obj as territorytypeTerritoryZone);
    }
}