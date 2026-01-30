using Day2eEditor;
using System.ComponentModel;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionInfectedControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private BindingList<string> _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        private void listBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            ListBox lb = sender as ListBox;
            e.DrawBackground();
            Brush myBrush = Brushes.Black;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            }
            else
            {
                myBrush = Brushes.White;
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(60, 63, 65)), e.Bounds);
            }
            e.Graphics.DrawString(lb.Items[e.Index].ToString(), e.Font, myBrush, e.Bounds);
            e.DrawFocusRectangle();
        }
        public ExpansionInfectedControl()
        {
            InitializeComponent();
            setupairdropZombies(listBox2);
        }
        private void setupairdropZombies(ListBox lb)
        {
            lb.Items.Clear();

            lb.Items.AddRange(AppServices.GetRequired<EconomyManager>().TypesConfig.SerachTypes("zmbm_").ToArray());
            lb.Items.AddRange(AppServices.GetRequired<EconomyManager>().TypesConfig.SerachTypes("zmbf_").ToArray());
            lb.Items.AddRange(AppServices.GetRequired<EconomyManager>().TypesConfig.SerachTypes("animal_").ToArray());

        }
        /// <summary>
        /// Returns the UserControl instance
        /// </summary>
        public Control GetControl() => this;

        /// <summary>
        /// Loads data into the control and stores the selected tree nodes
        /// </summary>
        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as BindingList<string> ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            listBox1.DisplayMember = "DisplayName";
            listBox1.ValueMember = "Value";
            listBox1.DataSource = _data;

            _suppressEvents = false;
        }

        #region Helper Methods

        /// <summary>
        /// Clones the data for reset purposes
        /// </summary>

        private BindingList<string> CloneData(BindingList<string> data)
        {
            return new BindingList<string>(data.ToList());
        }


        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                // TODO: Update _nodes.Last().Text based on _data
            }
        }

        #endregion

        private void ExpansionLootitemSetAllChanceButton_Click(object sender, EventArgs e)
        {
            List<string> removezombies = new List<string>();
            foreach (var item in listBox1.SelectedItems)
            {
                removezombies.Add(item.ToString());

            }
            foreach (string s in removezombies)
            {
                _data.Remove(s);
                
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var item in listBox2.SelectedItems)
            {
                string zombie = item.ToString();
                if (!_data.Contains(zombie))
                {
                    _data.Add(zombie);
                }
                else
                {
                    MessageBox.Show("Infected Type allready in the list.....");
                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string zombie = textBox1.Text;
            if (!_data.Contains(zombie))
            {
                _data.Add(zombie);
                
            }
            else
            {
                MessageBox.Show("Infected Type allready in the list.....");
            }
        }
    }
}