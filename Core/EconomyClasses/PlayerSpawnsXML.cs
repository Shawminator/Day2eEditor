using Day2eEditor;
using System.ComponentModel;
using System.Xml.Serialization;

namespace DayZeLib
{
    public class cfgplayerspawnpointsConfig : IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path); // e.g., "types.xml"
        public string FilePath => _path;
        public playerspawnpoints Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }

        public cfgplayerspawnpointsConfig(string path)
        {
            _path = path;
        }
        public void Load()
        {
            Data = AppServices.GetRequired<FileService>().LoadOrCreateXml<playerspawnpoints>(
               _path,
               createNew: () => new playerspawnpoints(),
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
               configName: "cfgplayerspawnpoints"
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



    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class playerspawnpoints
    {
        private playerspawnpointssection freshField;
        private playerspawnpointssection hopField;
        private playerspawnpointssection travelField;

        /// <remarks/>
        public playerspawnpointssection fresh
        {
            get
            {
                return this.freshField;
            }
            set
            {
                this.freshField = value;
            }
        }

        /// <remarks/>
        public playerspawnpointssection hop
        {
            get
            {
                return this.hopField;
            }
            set
            {
                this.hopField = value;
            }
        }

        /// <remarks/>
        public playerspawnpointssection travel
        {
            get
            {
                return this.travelField;
            }
            set
            {
                this.travelField = value;
            }
        }

        public playerspawnpoints()
        {
            fresh = new playerspawnpointssection();
            hop = new playerspawnpointssection();
            travel = new playerspawnpointssection();
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class playerspawnpointssection
    {
        private playerspawnpointsSpawn_params spawn_paramsField;
        private playerspawnpointsGenerator_params generator_paramsField;
        private playerspawnpointsGroup_params group_paramsField;
        private BindingList<object> generator_posbubblesField;

        public playerspawnpointsSpawn_params spawn_params
        {
            get => spawn_paramsField;
            set => spawn_paramsField = value;
        }
        public playerspawnpointsGenerator_params generator_params
        {
            get => generator_paramsField;
            set => generator_paramsField = value;
        }
        public playerspawnpointsGroup_params group_params
        {
            get => group_paramsField;
            set => group_paramsField = value;
        }
        [XmlArray("generator_posbubbles")]
        [XmlArrayItem("group", typeof(playerspawnpointsGroup))]
        [XmlArrayItem("pos", typeof(playerspawnpointsGroupPos))]
        public BindingList<object> generator_posbubbles { get; set; }

        /// <summary>
        /// Strongly typed helper for groups
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public IEnumerable<playerspawnpointsGroup> Groups =>
            generator_posbubbles?.OfType<playerspawnpointsGroup>() ?? Enumerable.Empty<playerspawnpointsGroup>();

        /// <summary>
        /// Strongly typed helper for positions
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public IEnumerable<playerspawnpointsGroupPos> Positions =>
            generator_posbubbles?.OfType<playerspawnpointsGroupPos>() ?? Enumerable.Empty<playerspawnpointsGroupPos>();

        public playerspawnpointssection()
        {
            spawn_params = new playerspawnpointsSpawn_params();
            generator_params = new playerspawnpointsGenerator_params();
            group_params = new playerspawnpointsGroup_params();
            generator_posbubbles = new BindingList<object>();
        }
    }
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class playerspawnpointsSpawn_params
    {
        private decimal min_dist_infectedField;
        private decimal max_dist_infectedField;
        private decimal min_dist_playerField;
        private decimal max_dist_playerField;
        private decimal min_dist_staticField;
        private decimal max_dist_staticField;
        private bool min_dist_triggerFieldSpecified;
        private decimal min_dist_triggerField;
        private bool max_dist_triggerFieldSpecified;
        private decimal max_dist_triggerField;

        /// <remarks/>
        public decimal min_dist_infected
        {
            get
            {
                return this.min_dist_infectedField;
            }
            set
            {
                this.min_dist_infectedField = value;
            }
        }

        /// <remarks/>
        public decimal max_dist_infected
        {
            get
            {
                return this.max_dist_infectedField;
            }
            set
            {
                this.max_dist_infectedField = value;
            }
        }

        /// <remarks/>
        public decimal min_dist_player
        {
            get
            {
                return this.min_dist_playerField;
            }
            set
            {
                this.min_dist_playerField = value;
            }
        }

        /// <remarks/>
        public decimal max_dist_player
        {
            get
            {
                return this.max_dist_playerField;
            }
            set
            {
                this.max_dist_playerField = value;
            }
        }

        /// <remarks/>
        public decimal min_dist_static
        {
            get
            {
                return this.min_dist_staticField;
            }
            set
            {
                this.min_dist_staticField = value;
            }
        }

        /// <remarks/>
        public decimal max_dist_static
        {
            get
            {
                return this.max_dist_staticField;
            }
            set
            {
                this.max_dist_staticField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool min_dist_triggerSpecified
        {
            get
            {
                return this.min_dist_triggerFieldSpecified;
            }
            set
            {
                this.min_dist_triggerFieldSpecified = value;
            }
        }
        /// <remarks/>
        public decimal min_dist_trigger
        {
            get
            {
                return this.min_dist_triggerField;
            }
            set
            {
                this.min_dist_triggerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool max_dist_triggerSpecified
        {
            get
            {
                return this.max_dist_triggerFieldSpecified;
            }
            set
            {
                this.max_dist_triggerFieldSpecified = value;
            }
        }
        /// <remarks/>
        public decimal max_dist_trigger
        {
            get
            {
                return this.max_dist_triggerField;
            }
            set
            {
                this.max_dist_triggerField = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is not playerspawnpointsSpawn_params other)
                return false;

            return this.min_dist_infected == other.min_dist_infected
                && this.max_dist_infected == other.max_dist_infected
                && this.min_dist_player == other.min_dist_player
                && this.max_dist_player == other.max_dist_player
                && this.min_dist_static == other.min_dist_static
                && this.max_dist_static == other.max_dist_static
                && this.min_dist_triggerSpecified == other.min_dist_triggerSpecified
                && this.min_dist_trigger == other.min_dist_trigger
                && this.max_dist_triggerSpecified == other.max_dist_triggerSpecified
                && this.max_dist_trigger == other.max_dist_trigger;
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class playerspawnpointsGenerator_params
    {
        private decimal grid_densityField;
        private decimal grid_widthField;
        private decimal grid_heightField;
        private decimal min_dist_staticField;
        private decimal max_dist_staticField;
        private decimal min_steepnessField;
        private decimal max_steepnessField;
        private bool allow_in_waterfield;

        /// <remarks/>
        public decimal grid_density
        {
            get
            {
                return this.grid_densityField;
            }
            set
            {
                this.grid_densityField = value;
            }
        }

        /// <remarks/>
        public decimal grid_width
        {
            get
            {
                return this.grid_widthField;
            }
            set
            {
                this.grid_widthField = value;
            }
        }

        /// <remarks/>
        public decimal grid_height
        {
            get
            {
                return this.grid_heightField;
            }
            set
            {
                this.grid_heightField = value;
            }
        }

        /// <remarks/>
        public decimal min_dist_static
        {
            get
            {
                return this.min_dist_staticField;
            }
            set
            {
                this.min_dist_staticField = value;
            }
        }

        /// <remarks/>
        public decimal max_dist_static
        {
            get
            {
                return this.max_dist_staticField;
            }
            set
            {
                this.max_dist_staticField = value;
            }
        }

        /// <remarks/>
        public decimal min_steepness
        {
            get
            {
                return this.min_steepnessField;
            }
            set
            {
                this.min_steepnessField = value;
            }
        }

        /// <remarks/>
        public decimal max_steepness
        {
            get
            {
                return this.max_steepnessField;
            }
            set
            {
                this.max_steepnessField = value;
            }
        }

        /// <remarks/>
        public bool allow_in_water
        {
            get
            {
                return this.allow_in_waterfield;
            }
            set
            {
                this.allow_in_waterfield = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is not playerspawnpointsGenerator_params other)
                return false;

            return this.grid_density == other.grid_density
                && this.grid_width == other.grid_width
                && this.grid_height == other.grid_height
                && this.min_dist_static == other.min_dist_static
                && this.max_dist_static == other.max_dist_static
                && this.min_steepness == other.min_steepness
                && this.allow_in_water == other.allow_in_water;
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class playerspawnpointsGroup_params
    {
        private bool enablegroupsField;
        private bool groups_as_regularField;
        private int lifetimeField;
        private int counterField;

        /// <remarks/>
        public bool enablegroups
        {
            get
            {
                return this.enablegroupsField;
            }
            set
            {
                this.enablegroupsField = value;
            }
        }
        /// <remarks/>
        public bool groups_as_regular
        {
            get
            {
                return this.groups_as_regularField;
            }
            set
            {
                this.groups_as_regularField = value;
            }
        }

        /// <remarks/>
        public int lifetime
        {
            get
            {
                return this.lifetimeField;
            }
            set
            {
                this.lifetimeField = value;
            }
        }

        /// <remarks/>
        public int counter
        {
            get
            {
                return this.counterField;
            }
            set
            {
                this.counterField = value;
            }
        }

        public playerspawnpointsGroup_params()
        {
            groups_as_regular = true;
        }

        public override bool Equals(object obj)
        {
            if (obj is not playerspawnpointsGroup_params other)
                return false;

            return this.enablegroups == other.enablegroups
                && this.groups_as_regular == other.groups_as_regular
                && this.lifetime == other.lifetime
                && this.counter == other.counter;
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class playerspawnpointsGroup
    {
        private BindingList<playerspawnpointsGroupPos> posField;
        private string nameField;
        private int lifetimeField;
        private bool lifetimeFieldSpecified;
        private int counterField;
        private bool counterFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("pos")]
        public BindingList<playerspawnpointsGroupPos> pos
        {
            get
            {
                return this.posField;
            }
            set
            {
                this.posField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int lifetime
        {
            get
            {
                return this.lifetimeField;
            }
            set
            {
                this.lifetimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool lifetimeSpecified
        {
            get
            {
                return this.lifetimeFieldSpecified;
            }
            set
            {
                this.lifetimeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int counter
        {
            get
            {
                return this.counterField;
            }
            set
            {
                this.counterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool counterSpecified
        {
            get
            {
                return this.counterFieldSpecified;
            }
            set
            {
                this.counterFieldSpecified = value;
            }
        }

        public override string ToString()
        {
            return name;
        }

        public override bool Equals(object obj)
        {
            if (obj is not playerspawnpointsGroup other)
                return false;

            return string.Equals(this.name, other.name, StringComparison.OrdinalIgnoreCase)
                && this.lifetime == other.lifetime
                && this.lifetimeFieldSpecified == other.lifetimeFieldSpecified
                && this.counter == other.counter
                && this.counterSpecified == other.counterSpecified;
        }

    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class playerspawnpointsGroupPos
    {

        private decimal xField;
        private decimal zField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal x
        {
            get
            {
                return this.xField;
            }
            set
            {
                this.xField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal z
        {
            get
            {
                return this.zField;
            }
            set
            {
                this.zField = value;
            }
        }

        public override string ToString()
        {
            return "X:" + xField.ToString() + " , Z:" + zField.ToString();
        }

    }








    //Old Format of playerspanfile, we will use this to convert to new format....

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(ElementName = "playerspawnpoints", Namespace = "", IsNullable = false)]
    public partial class playerspawnpoints_old
    {
        private playerspawnpointsFresh_old freshField;
        private playerspawnpointsHop_old hopField;
        private playerspawnpointsTravel_old travelField;

        /// <remarks/>
        public playerspawnpointsFresh_old fresh
        {
            get
            {
                return this.freshField;
            }
            set
            {
                this.freshField = value;
            }
        }

        /// <remarks/>
        public playerspawnpointsHop_old hop
        {
            get
            {
                return this.hopField;
            }
            set
            {
                this.hopField = value;
            }
        }

        /// <remarks/>
        public playerspawnpointsTravel_old travel
        {
            get
            {
                return this.travelField;
            }
            set
            {
                this.travelField = value;
            }
        }

        public playerspawnpoints convertToNewFormat()
        {
            playerspawnpoints newsp = new playerspawnpoints();
            newsp.fresh.generator_params.grid_density = fresh.generator_params.grid_density;
            newsp.fresh.generator_params.grid_width = fresh.generator_params.grid_width;
            newsp.fresh.generator_params.grid_height = fresh.generator_params.grid_height;
            newsp.fresh.generator_params.min_dist_static = fresh.generator_params.min_dist_static;
            newsp.fresh.generator_params.max_dist_static = fresh.generator_params.max_dist_static;
            newsp.fresh.generator_params.min_steepness = fresh.generator_params.min_steepness;
            newsp.fresh.generator_params.max_dist_static = fresh.generator_params.max_dist_static;
            newsp.hop.generator_params = newsp.fresh.generator_params;
            newsp.travel.generator_params = newsp.fresh.generator_params;

            newsp.fresh.spawn_params.min_dist_infected = fresh.spawn_params.min_dist_infected;
            newsp.fresh.spawn_params.max_dist_infected = fresh.spawn_params.max_dist_infected;
            newsp.fresh.spawn_params.min_dist_player = fresh.spawn_params.min_dist_player;
            newsp.fresh.spawn_params.max_dist_player = fresh.spawn_params.max_dist_player;
            newsp.fresh.spawn_params.min_dist_static = fresh.spawn_params.min_dist_static;
            newsp.fresh.spawn_params.max_dist_static = fresh.spawn_params.max_dist_static;
            newsp.hop.spawn_params = newsp.fresh.spawn_params;
            newsp.travel.spawn_params = newsp.fresh.spawn_params;

            newsp.fresh.group_params.counter = -1;
            newsp.fresh.group_params.lifetime = 360;
            newsp.fresh.group_params.enablegroups = true;
            newsp.hop.group_params = newsp.fresh.group_params;
            newsp.travel.group_params = newsp.fresh.group_params;

            if (fresh.generator_posbubbles.Count > 0)
            {
                newsp.fresh.generator_posbubbles = new BindingList<object>(
                    fresh.generator_posbubbles.Cast<object>().ToList()
                );
            }

            if (travel != null && travel.generator_posbubbles.Count > 0)
            {
                newsp.travel.generator_posbubbles = new BindingList<object>(
                    travel.generator_posbubbles.Cast<object>().ToList()
                );
            }

            if (hop != null && hop.generator_posbubbles.Count > 0)
            {
                newsp.hop.generator_posbubbles = new BindingList<object>(
                    hop.generator_posbubbles.Cast<object>().ToList()
                );
            }

            return newsp;
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class playerspawnpointsFresh_old
    {

        private playerspawnpointsFreshSpawn_params_old spawn_paramsField;

        private playerspawnpointsFreshGenerator_params_old generator_paramsField;

        private BindingList<playerspawnpointsGroupPos> generator_posbubblesField;

        /// <remarks/>
        public playerspawnpointsFreshSpawn_params_old spawn_params
        {
            get
            {
                return this.spawn_paramsField;
            }
            set
            {
                this.spawn_paramsField = value;
            }
        }

        /// <remarks/>
        public playerspawnpointsFreshGenerator_params_old generator_params
        {
            get
            {
                return this.generator_paramsField;
            }
            set
            {
                this.generator_paramsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("pos", IsNullable = false)]
        public BindingList<playerspawnpointsGroupPos> generator_posbubbles
        {
            get
            {
                return this.generator_posbubblesField;
            }
            set
            {
                this.generator_posbubblesField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class playerspawnpointsFreshSpawn_params_old
    {

        private decimal min_dist_infectedField;

        private decimal max_dist_infectedField;

        private decimal min_dist_playerField;

        private decimal max_dist_playerField;

        private decimal min_dist_staticField;

        private decimal max_dist_staticField;

        /// <remarks/>
        public decimal min_dist_infected
        {
            get
            {
                return this.min_dist_infectedField;
            }
            set
            {
                this.min_dist_infectedField = value;
            }
        }

        /// <remarks/>
        public decimal max_dist_infected
        {
            get
            {
                return this.max_dist_infectedField;
            }
            set
            {
                this.max_dist_infectedField = value;
            }
        }

        /// <remarks/>
        public decimal min_dist_player
        {
            get
            {
                return this.min_dist_playerField;
            }
            set
            {
                this.min_dist_playerField = value;
            }
        }

        /// <remarks/>
        public decimal max_dist_player
        {
            get
            {
                return this.max_dist_playerField;
            }
            set
            {
                this.max_dist_playerField = value;
            }
        }

        /// <remarks/>
        public decimal min_dist_static
        {
            get
            {
                return this.min_dist_staticField;
            }
            set
            {
                this.min_dist_staticField = value;
            }
        }

        /// <remarks/>
        public decimal max_dist_static
        {
            get
            {
                return this.max_dist_staticField;
            }
            set
            {
                this.max_dist_staticField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class playerspawnpointsFreshGenerator_params_old
    {

        private decimal grid_densityField;

        private decimal grid_widthField;

        private decimal grid_heightField;

        private decimal min_dist_staticField;

        private decimal max_dist_staticField;

        private decimal min_steepnessField;

        private decimal max_steepnessField;

        /// <remarks/>
        public decimal grid_density
        {
            get
            {
                return this.grid_densityField;
            }
            set
            {
                this.grid_densityField = value;
            }
        }

        /// <remarks/>
        public decimal grid_width
        {
            get
            {
                return this.grid_widthField;
            }
            set
            {
                this.grid_widthField = value;
            }
        }

        /// <remarks/>
        public decimal grid_height
        {
            get
            {
                return this.grid_heightField;
            }
            set
            {
                this.grid_heightField = value;
            }
        }

        /// <remarks/>
        public decimal min_dist_static
        {
            get
            {
                return this.min_dist_staticField;
            }
            set
            {
                this.min_dist_staticField = value;
            }
        }

        /// <remarks/>
        public decimal max_dist_static
        {
            get
            {
                return this.max_dist_staticField;
            }
            set
            {
                this.max_dist_staticField = value;
            }
        }

        /// <remarks/>
        public decimal min_steepness
        {
            get
            {
                return this.min_steepnessField;
            }
            set
            {
                this.min_steepnessField = value;
            }
        }

        /// <remarks/>
        public decimal max_steepness
        {
            get
            {
                return this.max_steepnessField;
            }
            set
            {
                this.max_steepnessField = value;
            }
        }
    }
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class playerspawnpointsHop_old
    {

        private BindingList<playerspawnpointsGroupPos> generator_posbubblesField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("pos", IsNullable = false)]
        public BindingList<playerspawnpointsGroupPos> generator_posbubbles
        {
            get
            {
                return this.generator_posbubblesField;
            }
            set
            {
                this.generator_posbubblesField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class playerspawnpointsTravel_old
    {

        private BindingList<playerspawnpointsGroupPos> generator_posbubblesField;

        /// <remarks/>
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("pos", IsNullable = false)]
        public BindingList<playerspawnpointsGroupPos> generator_posbubbles
        {
            get
            {
                return this.generator_posbubblesField;
            }
            set
            {
                this.generator_posbubblesField = value;
            }
        }
    }


}
