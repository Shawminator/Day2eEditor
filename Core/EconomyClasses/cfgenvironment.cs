﻿using System.ComponentModel;
using System.Xml.Serialization;

namespace Day2eEditor
{
    public class cfgenvironmentConfig : IConfigLoader
    {
        private readonly string _path;
        public env Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }

        public cfgenvironmentConfig(string path)
        {
            _path = path;
        }


        public void Load()
        {
            Data = AppServices.GetRequired<FileService>().LoadOrCreateXml<env>(
                _path,
                createNew: () => new env(),
                onAfterLoad: cfg =>
                {
                    // Add validation here if needed
                },
                onError: ex =>
                {
                    HasErrors = true;
                    Errors.Add($"Error loading cfgenviroment file: {ex.Message}");
                },
                configName: "cfgenviroment"
            );
        }
        public void Save()
        {
            if (isDirty)
            {
                AppServices.GetRequired<FileService>().SaveXml(_path, Data);
                isDirty = false;
            }
        }
    }
    [XmlRoot("env")]
    public class env
    {
        private envTerritories _territories;

        [XmlElement("territories")]
        public envTerritories territories
        {
            get => _territories ??= new envTerritories();
            set => _territories = value;
        }
    }
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class envTerritories
    {

        private BindingList<envTerritoriesFile> fileField;

        private BindingList<envTerritoriesTerritory> territoryField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("file")]
        public BindingList<envTerritoriesFile> file
        {
            get
            {
                return this.fileField;
            }
            set
            {
                this.fileField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("territory")]
        public BindingList<envTerritoriesTerritory> territory
        {
            get
            {
                return this.territoryField;
            }
            set
            {
                this.territoryField = value;
            }
        }
    }
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class envTerritoriesFile
    {

        private string pathField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string path
        {
            get
            {
                return this.pathField;
            }
            set
            {
                this.pathField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class envTerritoriesTerritory
    {

        private envTerritoriesTerritoryFile fileField;

        private BindingList<envTerritoriesTerritoryAgent> agentField;

        private BindingList<envTerritoriesTerritoryItem> itemField;

        private string typeField;

        private string nameField;

        private string behaviorField;

        /// <remarks/>
        public envTerritoriesTerritoryFile file
        {
            get
            {
                return this.fileField;
            }
            set
            {
                this.fileField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("agent")]
        public BindingList<envTerritoriesTerritoryAgent> agent
        {
            get
            {
                return this.agentField;
            }
            set
            {
                this.agentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("item")]
        public BindingList<envTerritoriesTerritoryItem> item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
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
        public string behavior
        {
            get
            {
                return this.behaviorField;
            }
            set
            {
                this.behaviorField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class envTerritoriesTerritoryFile
    {

        private string usableField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string usable
        {
            get
            {
                return this.usableField;
            }
            set
            {
                this.usableField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class envTerritoriesTerritoryAgent
    {

        private envTerritoriesTerritoryAgentSpawn[] spawnField;

        private envTerritoriesTerritoryAgentItem[] itemField;

        private string typeField;

        private int chanceField;

        private bool chanceFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("spawn")]
        public envTerritoriesTerritoryAgentSpawn[] spawn
        {
            get
            {
                return this.spawnField;
            }
            set
            {
                this.spawnField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("item")]
        public envTerritoriesTerritoryAgentItem[] item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int chance
        {
            get
            {
                return this.chanceField;
            }
            set
            {
                this.chanceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool chanceSpecified
        {
            get
            {
                return this.chanceFieldSpecified;
            }
            set
            {
                this.chanceFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class envTerritoriesTerritoryAgentSpawn
    {

        private string configNameField;

        private int chanceField;

        private bool chanceFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string configName
        {
            get
            {
                return this.configNameField;
            }
            set
            {
                this.configNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int chance
        {
            get
            {
                return this.chanceField;
            }
            set
            {
                this.chanceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool chanceSpecified
        {
            get
            {
                return this.chanceFieldSpecified;
            }
            set
            {
                this.chanceFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class envTerritoriesTerritoryAgentItem
    {

        private string nameField;

        private int valField;

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
        public int val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class envTerritoriesTerritoryItem
    {

        private string nameField;

        private int valField;

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
        public int val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }
}
