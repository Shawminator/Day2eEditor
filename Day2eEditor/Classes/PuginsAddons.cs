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

        public override string ToString() => Name;
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class PluginInfoAttribute : Attribute
    {
        public string Name { get; }
        public string Identifier { get; }

        public PluginInfoAttribute(string name, string identifier)
        {
            Name = name;
            Identifier = identifier;
        }
    }
}
