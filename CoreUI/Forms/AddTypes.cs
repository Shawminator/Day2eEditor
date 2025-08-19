using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace Day2eEditor
{
    public partial class AddTypes : Form
    {
        private FormController controller;
        private readonly BindingSource _binding = new();
        public BindingList<TypeEntry> _entries = new BindingList<TypeEntry>();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string moddir 
        { 
            get { return textBox2.Text; }
            set { textBox2.Text = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string typesname 
        {
            get { return textBox1.Text; } 
            set { textBox1.Text = value; }
        }

        // Lookup lists (replace with your actual lists as needed)
        private readonly List<listsCategory> _categories = new List<listsCategory>();
        private readonly List<string> _flags = new List<string>();
        private readonly List<listsTag> _tagOptions = new List<listsTag>();
        private readonly List<object> _usageOptions = new List<object>();
        private readonly List<object> _ValueOptions = new List<object>();

        public AddTypes()
        {
            InitializeComponent();
            controller = new FormController(
                this,
                TitlePanel,
                ResizePanel,
                TitleLabel,
                label1,
                CloseButton,
                null
            );
            PopulateDefs();
            SetupGrid();
            this.Disposed += (s, e) => controller.Dispose();
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
            foreach (listsTag tag in AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionConfig.Data.tags)
            {
                _tagOptions.Add(tag);
            }
            _flags.AddRange("count_in_cargo", "count_in_hoarder", "count_in_map", "count_in_player", "crafted", "deloot");
            _usageOptions.AddRange(AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionConfig.Data.usageflags);
            _usageOptions.AddRange(AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionuserConfig.Data.usageflags);
            _ValueOptions.AddRange(AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionConfig.Data.valueflags);
            _ValueOptions.AddRange(AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionuserConfig.Data.valueflags);
        }
        private void SetupGrid()
        {
            _grid.DefaultCellStyle.ForeColor = SystemColors.ActiveCaptionText;
            _grid.AutoGenerateColumns = false;
            NameGridColumn.DataPropertyName = "Name";
            nominal.DataPropertyName = "Nominal";
            lifetime.DataPropertyName = "Lifetime";
            restock.DataPropertyName = "Restock";
            min.DataPropertyName = "Min";
            quantmin.DataPropertyName = "QuantMin";
            quantmax.DataPropertyName = "QuantMax";
            cost.DataPropertyName = "Cost";
            Flags.DataPropertyName = "Flags";
            _grid.Columns["Flags"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Category.DataPropertyName = "Category";
            Tags.DataPropertyName = "Tags";
            _grid.Columns["Tags"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Usages.DataPropertyName = "Usages";
            _grid.Columns["Usages"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Values.DataPropertyName = "Values";
            _grid.Columns["Values"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            _grid.DataSource = _entries;

        }
        private void AddTypes_Load(object sender, EventArgs e)
        {
            var economymanager = AppServices.GetRequired<EconomyManager>();
        }
        private void _grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (_grid.IsCurrentCellInEditMode)
            {
                _grid.EndEdit(); // commit any pending edits
            }
            var entry = _grid.Rows[e.RowIndex].DataBoundItem as TypeEntry;
            if (entry == null) return;
            var cellRect = _grid.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
            var screenPoint = _grid.PointToScreen(new Point(cellRect.X, cellRect.Y));
            // Flags column
            if (e.ColumnIndex == _grid.Columns["Flags"].Index)
            {
                var popup = new FlagsEditorForm(_flags, entry.Flags, updatedFlags =>
                {
                    entry.Flags = updatedFlags;
                    _grid.InvalidateCell(e.ColumnIndex, e.RowIndex);
                    _grid.AutoResizeColumn(e.ColumnIndex, DataGridViewAutoSizeColumnMode.AllCells);
                });

                // Close popup when clicking anywhere else
                popup.Deactivate += (s, args) => popup.Close();

                popup.Location = screenPoint;
                popup.Show();
            }

            // Category column
            else if (e.ColumnIndex == _grid.Columns["Category"].Index)
            {
                string listcatname = "";
                if (entry.Category == null)
                    listcatname = "other";
                else listcatname = entry.Category.Name;

                var popup = new CategoryPopupForm(_categories, new listsCategory() { name = listcatname }, selectedCategory =>
                {
                    entry.changecategory(selectedCategory);
                    _grid.InvalidateCell(e.ColumnIndex, e.RowIndex);
                    _grid.AutoResizeColumn(e.ColumnIndex, DataGridViewAutoSizeColumnMode.AllCells);
                });

                // Close popup when clicking anywhere else
                popup.Deactivate += (s, args) => popup.Close();

                popup.Location = screenPoint;
                popup.Show();
            }
            // Usages column
            else if (e.ColumnIndex == _grid.Columns["Tags"].Index)
            {
                var popup = new TagsEditorForm(
                    _tagOptions,
                    entry.Tags,
                    addSelectedTag: tag =>
                    {
                        if (tag is listsTag t)
                        {
                            entry.Addnewtag(t);
                        }
                        _grid.InvalidateCell(e.ColumnIndex, e.RowIndex);
                        _grid.AutoResizeColumn(e.ColumnIndex, DataGridViewAutoSizeColumnMode.AllCells);
                    },
                    removeSelectedTag: tag =>
                    {
                        entry.removetag(tag);
                        _grid.InvalidateCell(e.ColumnIndex, e.RowIndex);
                        _grid.AutoResizeColumn(e.ColumnIndex, DataGridViewAutoSizeColumnMode.AllCells);
                    }

                );

                // Close popup when clicking anywhere else
                popup.Deactivate += (s, args) => popup.Close();

                popup.Location = screenPoint;
                popup.Show();
            }
            // Usages column
            else if (e.ColumnIndex == _grid.Columns["Usages"].Index)
            {
                var popup = new UsageEditorForm(
                    _usageOptions,
                    entry.Usages,
                    addSelectedUsage: usage =>
                    {
                        if (usage is listsUsage u)
                        {
                            entry.AddnewUsage(u);
                        }
                        else if (usage is user_listsUser uu)
                        {
                            entry.AddnewUserUsage(uu);
                        }
                        _grid.InvalidateCell(e.ColumnIndex, e.RowIndex);
                        _grid.AutoResizeColumn(e.ColumnIndex, DataGridViewAutoSizeColumnMode.AllCells);
                    },
                    removeSelectedUsage: usage =>
                    {
                        entry.removeusage(usage);
                        _grid.InvalidateCell(e.ColumnIndex, e.RowIndex);
                        _grid.AutoResizeColumn(e.ColumnIndex, DataGridViewAutoSizeColumnMode.AllCells);
                    }

                );

                // Close popup when clicking anywhere else
                popup.Deactivate += (s, args) => popup.Close();

                popup.Location = screenPoint;
                popup.Show();
            }
            // Values column
            else if (e.ColumnIndex == _grid.Columns["Values"].Index)
            {
                var popup = new ValuesEditorForm(
                    _ValueOptions,
                    entry.Values,
                    addSelectedValue: value =>
                    {
                        if (value is listsValue v)
                        {
                            entry.AddTier(v.name);
                        }
                        else if (value is user_listsUser1 vv)
                        {
                            entry.AdduserTier(vv.name);
                        }
                        _grid.InvalidateCell(e.ColumnIndex, e.RowIndex);
                        _grid.AutoResizeColumn(e.ColumnIndex, DataGridViewAutoSizeColumnMode.AllCells);
                    },
                    removeSelectedValue: value =>
                    {
                        if (value is listsValue v)
                        {
                            entry.removetier(v.name);
                        }
                        else if (value is user_listsUser1 vv)
                        {
                            entry.removeusertier(vv.name);
                        }
                        _grid.InvalidateCell(e.ColumnIndex, e.RowIndex);
                        _grid.AutoResizeColumn(e.ColumnIndex, DataGridViewAutoSizeColumnMode.AllCells);
                    }

                );

                // Close popup when clicking anywhere else
                popup.Deactivate += (s, args) => popup.Close();

                popup.Location = screenPoint;
                popup.Show();
            }
        }
        private void _grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (_grid.Columns[e.ColumnIndex].Name == "Flags" && e.RowIndex >= 0)
            {
                var entry = _grid.Rows[e.RowIndex].DataBoundItem as TypeEntry;
                if (entry != null && entry.Flags != null)
                {
                    var flags = entry.Flags;
                    e.Value = entry.Flags.ToString();
                }
            }
            if (_grid.Columns[e.ColumnIndex].Name == "Tags" && e.RowIndex >= 0)
            {
                var entry = _grid.Rows[e.RowIndex].DataBoundItem as TypeEntry;
                if (entry != null && entry.Usages != null)
                {
                    List<string> TagString = new();
                    foreach (Tag t in entry.Tags)
                    {
                        TagString.Add(t.ToString());
                    }
                    e.Value = string.Join(", ", TagString);
                }
            }
            if (_grid.Columns[e.ColumnIndex].Name == "Usages" && e.RowIndex >= 0)
            {
                var entry = _grid.Rows[e.RowIndex].DataBoundItem as TypeEntry;
                if (entry != null && entry.Usages != null)
                {
                    List<string> UsageString = new();
                    foreach (Usage u in entry.Usages)
                    {
                        UsageString.Add(u.ToString());
                    }
                    e.Value = string.Join(", ", UsageString);
                }
            }
            if (_grid.Columns[e.ColumnIndex].Name == "Values" && e.RowIndex >= 0)
            {
                var entry = _grid.Rows[e.RowIndex].DataBoundItem as TypeEntry;
                if (entry != null && entry.Values != null)
                {
                    List<string> ValueString = new();
                    foreach (Value v in entry.Values)
                    {
                        ValueString.Add(v.ToString());
                    }
                    e.Value = string.Join(", ", ValueString);
                }
            }
        }
        private void SelectProjectFolderbutton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = dialog.SelectedPath.Replace(AppServices.GetRequired<EconomyManager>().basePath + "\\", "").Replace("\\", "/");
                moddir = textBox2.Text;
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                TypesFile newtypes = new TypesFile(openfile.FileName);
                newtypes.Load();
                _entries = newtypes.Data.TypeList;
                _grid.DataSource = _entries;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            _entries.Clear();
            OpenFileDialog openfile = new OpenFileDialog();
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                string[] lines = File.ReadAllText(openfile.FileName).Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string line in lines)
                {
                    _entries.Add(new TypeEntry()
                    {
                        Name = line,
                        NameSpecified = true,
                        Nominal = 10,
                        NominalSpecified = true,
                        Lifetime = 28800,
                        LifetimeSpecified = true,
                        Restock = 0,
                        RestockSpecified = true,
                        Min = 5,
                        MinSpecified = true,
                        QuantMin = -1,
                        QuantMinSpecified = true,
                        QuantMax = -1,
                        QuantMaxSpecified = true,
                        Cost = 100,
                        CostSpecified = true,
                        Flags = new Flags()
                    });
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            _entries.Clear();
            string clipboardText = Clipboard.GetText();
            string[] lines = clipboardText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                _entries.Add(new TypeEntry()
                {
                    Name = line,
                    NameSpecified = true,
                    Nominal = 10,
                    NominalSpecified = true,
                    Lifetime = 28800,
                    LifetimeSpecified = true,
                    Restock = 0,
                    RestockSpecified = true,
                    Min = 5,
                    MinSpecified = true,
                    QuantMin = -1,
                    QuantMinSpecified = true,
                    QuantMax = -1,
                    QuantMaxSpecified = true,
                    Cost = 100,
                    CostSpecified = true,
                    Flags = new Flags()
                });
            }

        }

        private List<List<object>> _copiedCells;
        private void _grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (_grid.SelectedCells.Count == 0) return;

            if (e.Control && e.KeyCode == Keys.C)
            {
                CopySelectedCells();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                PasteCopiedCells();
                e.Handled = true;
            }
        }
        private void CopySelectedCells()
        {
            var selectedCells = _grid.SelectedCells.Cast<DataGridViewCell>()
                                     .OrderBy(c => c.RowIndex)
                                     .ThenBy(c => c.ColumnIndex)
                                     .ToList();

            int minRow = selectedCells.Min(c => c.RowIndex);
            int maxRow = selectedCells.Max(c => c.RowIndex);
            int minCol = selectedCells.Min(c => c.ColumnIndex);
            int maxCol = selectedCells.Max(c => c.ColumnIndex);

            _copiedCells = new List<List<object>>();

            for (int r = minRow; r <= maxRow; r++)
            {
                var rowData = new List<object>();
                for (int c = minCol; c <= maxCol; c++)
                {
                    var entry = (TypeEntry)_grid.Rows[r].DataBoundItem;
                    var property = typeof(TypeEntry).GetProperty(_grid.Columns[c].DataPropertyName);
                    rowData.Add(property.GetValue(entry));
                }
                _copiedCells.Add(rowData);
            }
        }
        private void PasteCopiedCells()
        {
            if (_copiedCells == null || _copiedCells.Count == 0) return;

            // Get the selected cells
            var selectedCells = _grid.SelectedCells.Cast<DataGridViewCell>()
                                      .OrderBy(c => c.RowIndex)
                                      .ThenBy(c => c.ColumnIndex)
                                      .ToList();

            int copiedRows = _copiedCells.Count;
            int copiedCols = _copiedCells[0].Count;

            // Determine bounds of selection
            int minRow = selectedCells.Min(c => c.RowIndex);
            int minCol = selectedCells.Min(c => c.ColumnIndex);

            // Paste only into the selected cells
            foreach (var cell in selectedCells)
            {
                var entry = cell.OwningRow.DataBoundItem as TypeEntry;
                if (entry == null) continue;

                int rOffset = cell.RowIndex - minRow;
                int cOffset = cell.ColumnIndex - minCol;

                // Wrap around if copied block smaller than selection
                var value = _copiedCells[rOffset % copiedRows][cOffset % copiedCols];

                var property = typeof(TypeEntry).GetProperty(_grid.Columns[cell.ColumnIndex].DataPropertyName);
                if (property == null) continue;

                // Handle type conversion safely
                if (value != null && !property.PropertyType.IsAssignableFrom(value.GetType()))
                {
                    try
                    {
                        value = Convert.ChangeType(value, property.PropertyType);
                    }
                    catch
                    {
                        continue; // skip if cannot convert
                    }
                }

                property.SetValue(entry, value);
            }

            _grid.Refresh();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            typesname = textBox1.Text;
        }
    }

}


