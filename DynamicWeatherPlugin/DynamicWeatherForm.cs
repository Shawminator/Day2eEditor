using Day2eEditor;
using System.ComponentModel;
using System.Text.Json;
using System.Xml.Linq;
using static FileService;

namespace DynamicWeatherPlugin
{
    public partial class DynamicWeatherForm : Form
    {
        private IPluginForm _plugin;
        public string DynamicWeatherPluginPath { get; private set; }

        public DynamicWeatherPluginConfig DynamicWeatherPlugin;
        private bool _suppressEvents;
        private TreeNode? currentTreeNode;
        private DynamicWeatherPluginConfig _originalData;

        public DynamicWeatherForm(IPluginForm plugin)
        {
            InitializeComponent();
            _plugin = plugin;
        }
        private void DynamicWeatherForm_Load(object sender, EventArgs e)
        {
            DynamicWeatherPluginPath = Path.Combine(AppServices.GetRequired<EconomyManager>().basePath, "weather.json");
            DynamicWeatherPlugin = new DynamicWeatherPluginConfig();
            DynamicWeatherPlugin.Filename = DynamicWeatherPluginPath;
            if (File.Exists(DynamicWeatherPluginPath))
            {
                DynamicWeatherPlugin.m_Dynamics = new BindingList<WeatherDynamic>(JsonSerializer.Deserialize<WeatherDynamic[]>(File.ReadAllText(DynamicWeatherPluginPath), new JsonSerializerOptions { Converters = { new BoolConverter() } }).ToList());
                DynamicWeatherPlugin.isDirty = false;
            }
            else
            {
                DynamicWeatherPlugin.CreateDefaults();
                DynamicWeatherPlugin.Save();
                MessageBox.Show("No Config Found, default Config Created....");
            }

            _originalData = CloneData(DynamicWeatherPlugin);
            TreeNode Root = new TreeNode("Dynamic Weather")
            {
                Tag = DynamicWeatherPlugin
            };
            foreach (WeatherDynamic wd in DynamicWeatherPlugin.m_Dynamics)
            {
                TreeNode wdnode = new TreeNode(wd.name)
                {
                    Tag = wd
                };
                Root.Nodes.Add(wdnode);
            }
            DynamicWeatherTV.Nodes.Add(Root);
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            savefiles();
        }
        public void savefiles(bool updated = false)
        {
            var savedFiles = DynamicWeatherPlugin.Save();
            Console.WriteLine("Saved files:");
            foreach (var file in savedFiles)
            {
                Console.WriteLine(file);
            }
            if (savedFiles.Count() > 0)
            {
                ShowSavedFilesMessage(savedFiles);
            }
        }
        private void ShowSavedFilesMessage(IEnumerable<string> files)
        {
            // Build a nice multiline string
            var fileListText = string.Join(Environment.NewLine, files);

            // Limit length so the box doesn't get too tall
            if (files.Count() > 15)
            {
                fileListText = string.Join(Environment.NewLine, files.Take(15)) +
                               Environment.NewLine + $"...and {files.Count() - 15} more";
            }

            MessageBox.Show(
                $"The following files were saved successfully:\n\n{fileListText}",
                "Save Complete",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
        private DynamicWeatherPluginConfig CloneData(DynamicWeatherPluginConfig data)
        {
            var clone = new DynamicWeatherPluginConfig
            {
                isDirty = data.isDirty,
                Filename = data.Filename,
                m_Dynamics = new BindingList<WeatherDynamic>()
            };

            foreach (var dynamic in data.m_Dynamics)
            {
                clone.m_Dynamics.Add(new WeatherDynamic
                {
                    chat_message = dynamic.chat_message,
                    notify_message = dynamic.notify_message,
                    name = dynamic.name,
                    transition_min = dynamic.transition_min,
                    transition_max = dynamic.transition_max,
                    duration_min = dynamic.duration_min,
                    duration_max = dynamic.duration_max,
                    overcast_min = dynamic.overcast_min,
                    overcast_max = dynamic.overcast_max,
                    use_dyn_vol_fog = dynamic.use_dyn_vol_fog,
                    dyn_vol_fog_dist_min = dynamic.dyn_vol_fog_dist_min,
                    dyn_vol_fog_dist_max = dynamic.dyn_vol_fog_dist_max,
                    dyn_vol_fog_height_min = dynamic.dyn_vol_fog_height_min,
                    dyn_vol_fog_height_max = dynamic.dyn_vol_fog_height_max,
                    dyn_vol_fog_bias = dynamic.dyn_vol_fog_bias,
                    fog_transition_time = dynamic.fog_transition_time,
                    fog_min = dynamic.fog_min,
                    fog_max = dynamic.fog_max,
                    wind_speed_min = dynamic.wind_speed_min,
                    wind_speed_max = dynamic.wind_speed_max,
                    wind_dir_min = dynamic.wind_dir_min,
                    wind_dir_max = dynamic.wind_dir_max,
                    rain_min = dynamic.rain_min,
                    rain_max = dynamic.rain_max,
                    snowfall_min = dynamic.snowfall_min,
                    snowfall_max = dynamic.snowfall_max,
                    snowfall_threshold_min = dynamic.snowfall_threshold_min,
                    snowfall_threshold_max = dynamic.snowfall_threshold_max,
                    snowfall_threshold_timeout = dynamic.snowfall_threshold_timeout,
                    snowflake_scale_min = dynamic.snowflake_scale_min,
                    snowflake_scale_max = dynamic.snowflake_scale_max,
                    use_snowflake_scale = dynamic.use_snowflake_scale,
                    storm = dynamic.storm,
                    thunder_threshold = dynamic.thunder_threshold,
                    thunder_timeout = dynamic.thunder_timeout,
                    use_global_temperature = dynamic.use_global_temperature,
                    global_temperature_override = dynamic.global_temperature_override
                });
            }

            return clone;
        }
        public void HasChanges()
        {
            if (DynamicWeatherPlugin != null)
            {
                DynamicWeatherPlugin.isDirty = !DynamicWeatherPlugin.Equals(_originalData);
            }
        }
        private void DynamicWeatherTV_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _suppressEvents = true;
            currentTreeNode = e.Node;

            if (e.Node.Tag is WeatherDynamic CurrentDynamicWeather)
            {
                chat_messageCB.Checked = CurrentDynamicWeather.chat_message;
                notify_messageCB.Checked = CurrentDynamicWeather.notify_message;
                nameTB.Text = CurrentDynamicWeather.name;
                transition_minNUD.Value = CurrentDynamicWeather.transition_min;
                transition_maxNUD.Value = CurrentDynamicWeather.transition_max;
                duration_minNUD.Value = CurrentDynamicWeather.duration_min;
                duration_maxNUD.Value = CurrentDynamicWeather.duration_max;
                overcast_minNUD.Value = CurrentDynamicWeather.overcast_min;
                overcast_maxNUD.Value = CurrentDynamicWeather.overcast_max;

                use_dyn_vol_fogCB.Checked = CurrentDynamicWeather.use_dyn_vol_fog;
                dyn_vol_fog_dist_minNUD.Value = CurrentDynamicWeather.dyn_vol_fog_dist_min;
                dyn_vol_fog_dist_maxNUD.Value = CurrentDynamicWeather.dyn_vol_fog_dist_max;
                dyn_vol_fog_height_minNUD.Value = CurrentDynamicWeather.dyn_vol_fog_height_min;
                dyn_vol_fog_height_maxNUD.Value = CurrentDynamicWeather.dyn_vol_fog_height_max;
                dyn_vol_fog_biasNUD.Value = CurrentDynamicWeather.dyn_vol_fog_bias;
                fog_transition_timeNUD.Value = CurrentDynamicWeather.fog_transition_time;
                fog_minNUD.Value = CurrentDynamicWeather.fog_min;
                fog_maxNUD.Value = CurrentDynamicWeather.fog_max;
                rain_minNUD.Value = CurrentDynamicWeather.rain_min;
                rain_maxNUD.Value = CurrentDynamicWeather.rain_max;
                wind_speed_minNUD.Value = CurrentDynamicWeather.wind_speed_min;
                wind_speed_maxNUD.Value = CurrentDynamicWeather.wind_speed_max;
                wind_dir_minNUD.Value = CurrentDynamicWeather.wind_dir_min;
                wind_dir_maxNUD.Value = CurrentDynamicWeather.wind_dir_max;
                snowfall_minNUD.Value = CurrentDynamicWeather.snowfall_min;
                snowfall_maxNUD.Value = CurrentDynamicWeather.snowfall_max;
                snowfall_threshold_minNUD.Value = CurrentDynamicWeather.snowfall_threshold_min;
                snowfall_threshold_maxNUD.Value = CurrentDynamicWeather.snowfall_threshold_max;
                snowfall_threshold_timeoutNUD.Value = CurrentDynamicWeather.snowfall_threshold_timeout;
                use_snowflake_scaleCB.Checked = CurrentDynamicWeather.use_snowflake_scale;
                snowflake_scale_minNUD.Value = CurrentDynamicWeather.snowflake_scale_min;
                snowflake_scale_maxNUD.Value = CurrentDynamicWeather.snowflake_scale_max;

                stormCB.Checked = CurrentDynamicWeather.storm;
                thunder_thresholdNUD.Value = CurrentDynamicWeather.thunder_threshold;
                thunder_timeoutNUD.Value = CurrentDynamicWeather.thunder_timeout;

                use_global_temperatureCB.Checked = CurrentDynamicWeather.use_global_temperature;
                global_temperature_overrideNUD.Value = CurrentDynamicWeather.global_temperature_override;

                _suppressEvents = false;
            }
        }
        private void addNewWeatherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WeatherDynamic newweather = new WeatherDynamic()
            {
                chat_message = true,
                notify_message = true,
                name = "TEMPLATE",
                transition_min = 1.0m,
                transition_max = 2.0m,
                duration_min = 30.0m,
                duration_max = 60.0m,
                overcast_min = 0.0m,
                overcast_max = 1.0m,
                fog_min = 0.0m,
                fog_max = 1.0m,
                use_dyn_vol_fog = true,
                dyn_vol_fog_dist_min = 0.2m,
                dyn_vol_fog_dist_max = 0.5m,
                dyn_vol_fog_height_min = 0.1m,
                dyn_vol_fog_height_max = 0.3m,
                dyn_vol_fog_bias = 10.0m,
                fog_transition_time = 60.0m,
                wind_speed_min = 0.0m,
                wind_speed_max = 1.0m,
                wind_dir_min = 0.0m,
                wind_dir_max = 360.0m,
                rain_min = 0.0m,
                rain_max = 1.0m,
                snowfall_min = 0.0m,
                snowfall_max = 1.0m,
                snowfall_threshold_min = 0.5m,
                snowfall_threshold_max = 1.0m,
                snowfall_threshold_timeout = 60m,
                snowflake_scale_min = 0.5m,
                snowflake_scale_max = 2.0m,
                use_snowflake_scale = true,
                storm = true,
                thunder_threshold = 0.8m,
                thunder_timeout = 0.0m,
                use_global_temperature = true,
                global_temperature_override = 35.0m
            };
            DynamicWeatherPlugin.m_Dynamics.Add(newweather);
            currentTreeNode.Nodes.Add(new TreeNode(newweather.name)
            {
                Tag = newweather
            });
            HasChanges();
        }
        private void removeWeatherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DynamicWeatherPlugin.m_Dynamics.Remove(currentTreeNode.Tag as WeatherDynamic);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
            HasChanges();
        }
        private void DynamicWeatherTV_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            DynamicWeatherTV.SelectedNode = e.Node;
            currentTreeNode = e.Node;

            if (e.Button != MouseButtons.Right) return;

            if (e.Node.Tag != null && e.Node.Tag is WeatherDynamic)
            {
                DynamicWeatherCM.Items.Clear();
                DynamicWeatherCM.Items.Add(removeWeatherToolStripMenuItem);
                DynamicWeatherCM.Show(Cursor.Position);
            }
            else if (e.Node.Tag != null && e.Node.Tag is DynamicWeatherPluginConfig)
            {
                DynamicWeatherCM.Items.Clear();
                DynamicWeatherCM.Items.Add(addNewWeatherToolStripMenuItem);
                DynamicWeatherCM.Show(Cursor.Position);
            }
        }
        private void DWPstringTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            TextBox textbox = sender as TextBox;
            ShellHelper.SetStringValue(currentTreeNode.Tag as WeatherDynamic, textbox.Name.Substring(0, textbox.Name.Length - 2), textbox.Text);
            currentTreeNode.Text = textbox.Text;
            HasChanges();
        }
        private void DWPBoolsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            CheckBox chk = sender as CheckBox;
            ShellHelper.SetBoolValue(currentTreeNode.Tag as WeatherDynamic, chk.Name.Substring(0, chk.Name.Length - 2), chk.Checked);
            HasChanges();
        }
        private void DWPDecimalsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            NumericUpDown nud = sender as NumericUpDown;
            ShellHelper.SetDecimalValue(currentTreeNode.Tag as WeatherDynamic, nud.Name.Substring(0, nud.Name.Length - 3), nud.Value);
            HasChanges();
        }
        private void DynamicWeatherForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DynamicWeatherPlugin.isDirty)
            {
                DialogResult dialogResult = MessageBox.Show("You have Unsaved Changes, do you wish to save", "Unsaved Changes found", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    savefiles();
                }
            }
        }
    }

    [PluginInfo("Dynamic Weather Manager", "DynamicWeatherPlugin", "DynamicWeatherPlugin.DynamicWeather.png")]
    public class PluginDynamicWeather : IPluginForm, IDisposable
    {
        private bool disposed = false;

        public string pluginIdentifier => "DynamicWeatherPlugin";
        public string pluginName => "Dynamic Weather  Manager";

        public Form GetForm()
        {
            return new DynamicWeatherForm(this);
        }
        public override string ToString()
        {
            return pluginName;
        }

        public void Dispose()
        {
            if (!disposed)
            {
                // Dispose any resources (e.g., file handles, etc.)
                disposed = true;
            }
        }
    }
}
