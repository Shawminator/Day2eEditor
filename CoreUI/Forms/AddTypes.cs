namespace Day2eEditor
{
    public partial class AddTypes : Form
    {
        private readonly BindingSource _binding = new();
        public List<TypeEntry> _typeEntries = new List<TypeEntry>();

        // Lookup lists (replace with your actual lists as needed)
        private readonly List<string> _categories = new List<string>();
        private readonly List<string> _flags = new List<string>();
        private readonly List<string> _tagOptions = new List<string>();
        private readonly List<object> _usageOptions = new List<object>();
        private readonly List<object> _tierOptions = new List<object>();

        public AddTypes()
        {
            InitializeComponent();
            Form_Controls.InitializeForm_Controls
            (
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
        }

        private void PopulateDefs()
        {
            _categories.Add("other");
            foreach (listsCategory cat in AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionConfig.Data.categories)
            {
                _categories.Add(cat.name);
            }
            foreach(listsTag tag in  AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionConfig.Data.tags)
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



            _binding.DataSource = _typeEntries;
            _grid.DataSource = _binding;

        }

        private void AddTypes_Load(object sender, EventArgs e)
        {
            var economymanager = AppServices.GetRequired<EconomyManager>();

        }
    }
}


