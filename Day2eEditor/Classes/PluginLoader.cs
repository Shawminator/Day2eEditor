
using System.Reflection;
using Day2eEditor;

public class PluginLoader
{
    public IPluginForm LoadPlugin(string dllPath)
    {
        try
        {
            var assembly = Assembly.LoadFrom(dllPath);
            var pluginType = assembly.GetTypes()
                .FirstOrDefault(t => typeof(IPluginForm).IsAssignableFrom(t) && !t.IsAbstract);

            if (pluginType != null)
            {
                return Activator.CreateInstance(pluginType) as IPluginForm;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading plugin: {ex.Message}");
        }

        return null;
    }
}
