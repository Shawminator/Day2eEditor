using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Day2eEditor
{
    public partial class AddTypes : Form
    {

        public List<TypeEntry> _typeEntries = new List<TypeEntry>();

        // Lookup lists (replace with your actual lists as needed)
        private readonly List<listsCategory> _categories = new List<listsCategory>();
        private readonly List<listsTag> _tagOptions = new List<listsTag>();
        private readonly List<object> _usageOptions = new List<object>();
        private readonly List<object> _tierOptions = new List<object>();

        public AddTypes()
        {
            InitializeComponent();
            PopulateDefs();
            SetupGrid();
        }

        private void PopulateDefs()
        {
            _categories.Add(new listsCategory()
            {
                name = "other"
            });
            foreach (listsCategory cat in AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionConfig.Data.categories)
            {
                _categories.Add(cat);
            }
            _tagOptions.AddRange(AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionConfig.Data.tags);
            _usageOptions.AddRange(AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionConfig.Data.usageflags);
            _usageOptions.AddRange(AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionuserConfig.Data.usageflags);
            _tierOptions.AddRange(AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionConfig.Data.valueflags);
            _tierOptions.AddRange(AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionuserConfig.Data.valueflags);
        }

        private void SetupGrid()
        {
            _grid.AutoGenerateColumns = false;

            // Name column
            _grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Name",
                HeaderText = "Name"
            });

            string[] valueProps = { "Nominal", "Lifetime", "Restock", "Min", "QuantMin", "QuantMax", "Cost" };

            foreach (var prop in valueProps)
            {
                _grid.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = prop,
                    HeaderText = prop
                });
            }

            string[] flagProps = {
                "CountInCargo", "CountInHoarder", "CountInMap",
                "CountInPlayer", "Crafted", "Deloot"
            };

            foreach (var flag in flagProps)
            {
                _grid.Columns.Add(new DataGridViewCheckBoxColumn
                {
                    HeaderText = flag,
                    DataPropertyName = $"Flags.{flag}"
                });
            }

            // Category dropdown
            var categoryColumn = new DataGridViewComboBoxColumn
            {
                DataPropertyName = "Category",
                HeaderText = "Category",
                DataSource = _categories,
                DisplayMember = "name",
                ValueMember = "name"
            };
            _grid.Columns.Add(categoryColumn);

            // Tags button column
            _grid.Columns.Add(new DataGridViewButtonColumn
            {
                HeaderText = "Tags",
                Text = "Edit Tags",
                UseColumnTextForButtonValue = true
            });

            // Usages button column
            _grid.Columns.Add(new DataGridViewButtonColumn
            {
                HeaderText = "Usages",
                Text = "Edit Usages",
                UseColumnTextForButtonValue = true
            });

            // Tiers button column
            _grid.Columns.Add(new DataGridViewButtonColumn
            {
                HeaderText = "Tiers",
                Text = "Edit Tiers",
                UseColumnTextForButtonValue = true
            });
            _grid.CellEndEdit += Grid_CellEndEdit;
            _grid.CellClick += Grid_CellClick;
        }

        private void Grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;

            var column = _grid.Columns[e.ColumnIndex];
            if (column.HeaderText != "Name") return;

            var cell = _grid.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var newName = cell.Value?.ToString()?.Trim();

            if (!string.IsNullOrEmpty(newName))
            {
                var existing = _typeEntries.FirstOrDefault(t => t.Name == newName);
                if (existing == null)
                {
                    var newEntry = new TypeEntry
                    {
                        Name = newName,
                        NameSpecified = true,
                        Flags = new Flags(),
                        Category = new Category(),
                        Usages = new BindingList<Usage>(),
                        Values = new BindingList<Value>()
                    };
                    _typeEntries.Add(newEntry);
                    _grid.Rows[e.RowIndex].DataBoundItem = newEntry;
                    _grid.Refresh();
                }
            }
        }

        private void Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var entry = _grid.Rows[e.RowIndex].DataBoundItem as TypeEntry;
            if (entry == null) return;

            if (_grid.Columns[e.ColumnIndex].HeaderText == "Tags")
            {
                var dialog = new MultiSelectDialog(_tagOptions);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    entry.Tags.Clear();
                    foreach (listsTag tag in dialog.SelectedItems)
                    {
                        entry.Tags.Add(new Tag { Name = tag.name, NameSpecified = true });
                    }
                }
            }
            else if (_grid.Columns[e.ColumnIndex].HeaderText == "Usages")
            {
                var dialog = new MultiSelectDialog(_usageOptions);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    entry.Usages.Clear();
                    foreach (var item in dialog.SelectedItems)
                    {
                        var usage = new Usage();
                        if (item is listsTag tag)
                        {
                            usage.Name = tag.name;
                            usage.NameSpecified = true;
                        }
                        else
                        {
                            usage.User = item.ToString();
                            usage.UserSpecified = true;
                        }
                        entry.Usages.Add(usage);
                    }
                }
            }
            else if (_grid.Columns[e.ColumnIndex].HeaderText == "Tiers")
            {
                var dialog = new MultiSelectDialog(_tierOptions);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    entry.Values.Clear();
                    foreach (var item in dialog.SelectedItems)
                    {
                        var value = new Value();
                        if (item is listsTag tag)
                        {
                            value.Name = tag.name;
                            value.NameSpecified = true;
                        }
                        else
                        {
                            value.User = item.ToString();
                            value.UserSpecified = true;
                        }
                        entry.Values.Add(value);
                    }
                }
            }

            _grid.Refresh();
        }

        private void AddTypes_Load(object sender, EventArgs e)
        {
            var economymanager = AppServices.GetRequired<EconomyManager>();

        }
    }

    public class MultiSelectDialog : Form
    {
        public List<object> SelectedItems { get; private set; } = new();

        public MultiSelectDialog(IEnumerable<object> options)
        {
            var listBox = new CheckedListBox { Dock = DockStyle.Fill };
            foreach (var item in options)
                listBox.Items.Add(item);

            var okButton = new Button { Text = "OK", Dock = DockStyle.Bottom };
            okButton.Click += (s, e) =>
            {
                foreach (var item in listBox.CheckedItems)
                    SelectedItems.Add(item);
                DialogResult = DialogResult.OK;
            };

            Controls.Add(listBox);
            Controls.Add(okButton);
        }
    }

}


