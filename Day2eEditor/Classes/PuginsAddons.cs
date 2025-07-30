namespace Day2eEditor
{
    public interface IPluginForm
    {
        string pluginIdentifier { get; }
        string pluginName { get; }
        decimal pluginVersion { get; }

        Form GetForm();
    }
}
