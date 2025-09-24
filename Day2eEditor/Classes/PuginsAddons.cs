using System.Configuration;

namespace Day2eEditor
{
    public interface IPluginForm
    {
        string pluginIdentifier { get; }
        string pluginName { get; }

        Form GetForm();
    }

    public class PluginEntry
    {
        public string Name { get; set; }
        public string Identifier { get; set; }
        public Type PluginType { get; set; }
        public Image Icon { get; set; }

        public override string ToString() => Name;
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class PluginInfoAttribute : Attribute
    {
        public string Name { get; }
        public string Identifier { get; }
        public string IconResourceName { get; }

        public PluginInfoAttribute(string name, string identifier, string iconResourceName = null)
        {
            Name = name;
            Identifier = identifier;
            IconResourceName = iconResourceName;
        }
    }
}
