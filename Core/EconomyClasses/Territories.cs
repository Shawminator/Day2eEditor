using Core;
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

            if (AppServices.GetRequired<EconomyManager>().cfgenvironmentConfig.Data.territories == null)
                return;

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
            data.SetGuid(Guid.NewGuid());
            return data;
        }
        protected override void SaveItem(territorytype item)
        {
            AppServices.GetRequired<FileService>().SaveXml(item.FilePath, item);
        }

        protected override string GetItemFileName(territorytype item) => item.FileName;
        protected override string GetItemFilePath(territorytype territorytype)
           => territorytype.FilePath;
        protected override Guid GetID(territorytype item) => item.Id;

        protected override bool ShouldDelete(territorytype item) => item.ToDelete;
        protected override void DeleteItemFile(territorytype item)
        {
            if (!string.IsNullOrWhiteSpace(item._path) && File.Exists(item._path))
            {
                File.Delete(item._path);
            }
        }
        public territorytype Createnewterritorytype(string filename)
        {
            territorytype territorytype = new territorytype()
            {
                Id = Guid.NewGuid(),
                territory = new BindingList<territorytypeTerritory>()
                
            };
            territorytype.SetPath(filename);
            MutableItems.Add(territorytype);
            return territorytype;
        }
        public void removeTerritoryFile(territorytype territorytype)
        {
            territorytype.ToDelete = true;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot("territory-type", Namespace = "", IsNullable = false)]
    public partial class territorytype : IDeepCloneable<territorytype>, IEquatable<territorytype>
    {
        [XmlIgnore]
        public string _path = string.Empty;

        [XmlIgnore]
        public string FileName => Path.GetFileName(_path);

        [XmlIgnore]
        public string FilePath => _path;

        [XmlIgnore]
        public Guid Id { get; set; }

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

        public void SetPath(string path) => _path = path;
        internal void SetGuid(Guid guid) => Id = guid;

        public territorytype Clone()
        {
            var clone = new territorytype
            {
                territory = new BindingList<territorytypeTerritory>(territory.Select(x => x.Clone()).ToList())
            };
            clone.SetPath(_path);
            clone.SetGuid(Id);
            return clone;
        }

        public bool Equals(territorytype? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (Id != other.Id) return false;

            if (_path != other._path)
                return false;
            
            if(!Helper.ListEquals(territory, other.territory)) 
                return false;

            return true;
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
                   Helper.ListEquals(zone, other.zone);
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

            return name == other.name &&
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