using System.Configuration;

namespace Day2eEditor
{
    public interface IPluginForm
    {
        string pluginIdentifier { get; }
        string pluginName { get; }

        Form GetForm();
        void SetData(List<object> data);
    }
}
