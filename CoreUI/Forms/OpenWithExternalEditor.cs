using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Day2eEditor
{
    public partial class OpenWithExternalEditor : Form
    {
        private FormController controller;
        private Dictionary<string, string> editors = new Dictionary<string, string>();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string filePath { get; set; }
        public OpenWithExternalEditor()
        {
            InitializeComponent();
            controller = new FormController(
               this,
               TitlePanel,
               null,
               null,
               label1,
               CloseButton,
               null
           );
            LoadEditors();
        }

        private void LoadEditors()
        {
            // Common editor paths
            var possibleEditors = new Dictionary<string, string>
            {
                { "Notepad++", @"C:\Program Files\Notepad++\notepad++.exe" },
                { "VS Code", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Programs\Microsoft VS Code\Code.exe") },
                { "Sublime Text", @"C:\Program Files\Sublime Text\sublime_text.exe" }
            };

            foreach (var editor in possibleEditors)
            {
                if (File.Exists(editor.Value))
                {
                    editors[editor.Key] = editor.Value;
                    comboBoxEditors.Items.Add(editor.Key);
                }
            }

            if (comboBoxEditors.Items.Count > 0)
                comboBoxEditors.SelectedIndex = 0;
            else
                comboBoxEditors.Items.Add("No editors found");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBoxEditors.SelectedItem == null || !editors.ContainsKey(comboBoxEditors.SelectedItem.ToString()))
            {
                MessageBox.Show("Please select a valid editor.");
                return;
            }

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Please select a valid file.");
                return;
            }

            string editorPath = editors[comboBoxEditors.SelectedItem.ToString()];

            try
            {
                Process.Start(editorPath, $"\"{filePath}\"");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open file: {ex.Message}");
            }

        }
    }
}
