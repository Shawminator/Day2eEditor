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

        private DynamicWeatherManager _DynamicWeatherManager { get; set; }

        private TreeNode? currentTreeNode;
        private bool _suppressEvents;

        public DynamicWeatherForm(IPluginForm plugin)
        {
            InitializeComponent();
            _plugin = plugin;
            _DynamicWeatherManager = new DynamicWeatherManager();
            _DynamicWeatherManager.SetDynamicweatherStuff();
            AppServices.Register(_DynamicWeatherManager);

        }
        private void DynamicWeatherForm_Load(object sender, EventArgs e)
        {
            TreeNode Root = new TreeNode("Dynamic Weather")
            {
                Tag = _DynamicWeatherManager.DynamicWeatherPluginConfig
            };
            foreach (WeatherDynamic wd in _DynamicWeatherManager.DynamicWeatherPluginConfig.Data.m_Dynamics)
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
            var savedFiles = _DynamicWeatherManager.Save();
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
        private void DynamicWeatherForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_DynamicWeatherManager.needToSave())
            {
                DialogResult dialogResult = MessageBox.Show("You have Unsaved Changes, do you wish to save", "Unsaved Changes found", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    savefiles();
                }
            }
        }
        private void DynamicWeatherForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            AppServices.Unregister<DynamicWeatherManager>();
        }
        private void DynamicWeatherTV_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _suppressEvents = true;
            currentTreeNode = e.Node;
            if (e.Node.Tag is DynamicWeatherConfig)
            {
                panel2.Visible = false;
            }
            if (e.Node.Tag is WeatherDynamic CurrentDynamicWeather)
            {
                panel2.Visible = true;
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
            _DynamicWeatherManager.DynamicWeatherPluginConfig.Data.m_Dynamics.Add(newweather);
            currentTreeNode.Nodes.Add(new TreeNode(newweather.name)
            {
                Tag = newweather
            });
        }
        private void removeWeatherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _DynamicWeatherManager.DynamicWeatherPluginConfig.Data.m_Dynamics.Remove(currentTreeNode.Tag as WeatherDynamic);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
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
            else if (e.Node.Tag != null && e.Node.Tag is DynamicWeatherConfig)
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
            Helper.SetStringValue(currentTreeNode.Tag as WeatherDynamic, textbox.Name.Substring(0, textbox.Name.Length - 2), textbox.Text);
            currentTreeNode.Text = textbox.Text;
        }
        private void DWPBoolsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            CheckBox chk = sender as CheckBox;
            Helper.SetBoolValue(currentTreeNode.Tag as WeatherDynamic, chk.Name.Substring(0, chk.Name.Length - 2), chk.Checked);
        }
        private void DWPDecimalsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            NumericUpDown nud = sender as NumericUpDown;
            Helper.SetDecimalValue(currentTreeNode.Tag as WeatherDynamic, nud.Name.Substring(0, nud.Name.Length - 3), nud.Value);
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
