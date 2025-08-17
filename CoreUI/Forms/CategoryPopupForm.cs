namespace Day2eEditor
{
    // The popup form for selecting a single category
    public partial class CategoryPopupForm : Form
    {
        private readonly Color _borderColor = Color.White;
        private readonly int _borderWidth = 2;

        private readonly List<listsCategory> _categories;
        private readonly Action<listsCategory> _onCategorySelected;
        private listsCategory _selectedCategory;

        public CategoryPopupForm(List<listsCategory> categories, listsCategory current, Action<listsCategory> onCategorySelected)
        {
           
            _categories = categories;
            _onCategorySelected = onCategorySelected;
            _selectedCategory = current;

            this.BackColor = Color.FromArgb(60, 63, 65);
            this.ForeColor = SystemColors.Control;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.Padding = new Padding(_borderWidth);
            this.TopMost = true;

            this.Deactivate += (s, e) => Close();

            BuildList();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            int borderWidth = 2;
            using (var pen = new Pen(Color.White, borderWidth))
            {
                e.Graphics.DrawRectangle(
                    pen,
                    new Rectangle(0, 0, this.ClientSize.Width - 1, this.ClientSize.Height - 1)
                );
            }
        }
        private void BuildList()
        {
            this.Controls.Clear();

            var layout = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                Padding = new Padding(5),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Fill
            };

            int maxWidth = 0;

            foreach (var cat in _categories)
            {
                var rb = new RadioButton
                {
                    Text = cat.name,
                    Checked = _selectedCategory?.name == cat.name,
                    AutoSize = true
                };
                rb.CheckedChanged += (s, e) =>
                {
                    if (rb.Checked)
                    {
                        _selectedCategory = cat;
                        _onCategorySelected?.Invoke(_selectedCategory);
                        Close();
                    }
                };

                layout.Controls.Add(rb);

                if (rb.PreferredSize.Width > maxWidth)
                    maxWidth = rb.PreferredSize.Width;
            }

            this.Controls.Add(layout);

            // Fit form to largest radio button plus padding
            this.ClientSize = new Size(maxWidth + layout.Padding.Horizontal + this.Padding.Horizontal + 10, layout.PreferredSize.Height + this.Padding.Vertical);
        }

        private void InitializeComponent()
        {

        }
    }

}
