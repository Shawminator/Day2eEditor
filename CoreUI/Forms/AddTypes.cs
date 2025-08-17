using System.ComponentModel;

namespace Day2eEditor
{
    public partial class AddTypes : Form
    {
        private FormController controller;
        private readonly BindingSource _binding = new();
        BindingList<TypeEntry> _entries = new BindingList<TypeEntry>();

        // Lookup lists (replace with your actual lists as needed)
        private readonly List<listsCategory> _categories = new List<listsCategory>();
        private readonly List<string> _flags = new List<string>();
        private readonly List<string> _tagOptions = new List<string>();
        private readonly List<object> _usageOptions = new List<object>();
        private readonly List<object> _tierOptions = new List<object>();

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
            _categories.Add(new listsCategory(){
                name = "other"
            });
            foreach (listsCategory cat in AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionConfig.Data.categories)
            {
                _categories.Add(cat);
            }
            foreach (listsTag tag in AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionConfig.Data.tags)
            {
                _tagOptions.Add(tag.name);
            }
            _flags.AddRange("count_in_cargo", "count_in_hoarder", "count_in_map", "count_in_player", "crafted", "deloot");
            _usageOptions.AddRange(AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionConfig.Data.usageflags);
            _usageOptions.AddRange(AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionuserConfig.Data.usageflags);
            _tierOptions.AddRange(AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionConfig.Data.valueflags);
            _tierOptions.AddRange(AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionuserConfig.Data.valueflags);
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
            Usage.DataPropertyName = "Usages";
            Tiers.DataPropertyName = "Values";
            _grid.DataSource = _entries;

        }

        private void AddTypes_Load(object sender, EventArgs e)
        {
            var economymanager = AppServices.GetRequired<EconomyManager>();
            _entries.Add(new TypeEntry
            {
                Name = "AKM",
                Nominal = 10,
                Lifetime = 3600,
                Restock = 1200,
                Min = 1,
                QuantMin = 0,
                QuantMax = 100,
                Cost = 50,
                Flags = new Flags { CountInCargo = 1, CountInPlayer = 1 },
                Category = new Category { Name = "weapons", NameSpecified = true },
                Usages = new BindingList<Usage> { new Usage { Name = "military" } },
                Tags = new BindingList<Tag> { new Tag { Name = "gun" } },
                Values = new BindingList<Value> { new Value { Name = "tier1" } }
            });
        }

        private void _grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (_grid.IsCurrentCellInEditMode)
            {
                _grid.EndEdit(); // commit any pending edits
            }

            // Example: Flags column
            if (e.ColumnIndex == _grid.Columns["Flags"].Index)
            {
                var entry = _grid.Rows[e.RowIndex].DataBoundItem as TypeEntry;
                if (entry == null) return;

                var cellRect = _grid.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                var screenPoint = _grid.PointToScreen(new Point(cellRect.X, cellRect.Y));

                var popup = new FlagsEditorForm(entry.Flags, updatedFlags =>
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

            // Example: Category column
            else if (e.ColumnIndex == _grid.Columns["Category"].Index)
            {
                var entry = _grid.Rows[e.RowIndex].DataBoundItem as TypeEntry;
                if (entry == null) return;

                var cellRect = _grid.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                var screenPoint = _grid.PointToScreen(new Point(cellRect.X, cellRect.Y));

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
        }

        private void _grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (_grid.Columns[e.ColumnIndex].Name == "Flags" && e.RowIndex >= 0)
            {
                var entry = _grid.Rows[e.RowIndex].DataBoundItem as TypeEntry;
                if (entry != null && entry.Flags != null)
                {
                    var flags = entry.Flags;
                    var active = new List<string>();
                    if (flags.CountInCargo == 1) active.Add("CountInCargo");
                    if (flags.CountInHoarder == 1) active.Add("CountInHoarder");
                    if (flags.CountInMap == 1) active.Add("CountInMap");
                    if (flags.CountInPlayer == 1) active.Add("CountInPlayer");
                    if (flags.Crafted == 1) active.Add("Crafted");
                    if (flags.Deloot == 1) active.Add("Deloot");
                    e.Value = string.Join(", ", active);
                }
            }
        }
    }

}


