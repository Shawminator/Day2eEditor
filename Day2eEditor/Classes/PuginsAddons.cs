namespace Day2eEditor
{
    public interface IPluginForm
    {
        string pluginIdentifier { get; }
        string pluginName { get; }
        string pluginVersion { get; }

        Form GetForm();
    }
}
