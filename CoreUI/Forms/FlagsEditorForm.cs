using Day2eEditor;

public partial class FlagsEditorForm : Form
{
    private readonly Color _borderColor = SystemColors.Control;
    private readonly int _borderWidth = 2;

    private readonly Flags _flags;                  // Original flags
    private readonly Action<Flags> _onFlagsUpdated; // Callback when popup changes
    private readonly Flags _tempFlags;
    private readonly List<string> _flagslist;// Temporary copy for editing

    public FlagsEditorForm(List<string> flagslist, Flags flags, Action<Flags> onFlagsUpdated)
    {
        _flagslist = flagslist;
        _flags = flags;
        _onFlagsUpdated = onFlagsUpdated;

        // Copy current flag values to temporary object
        _tempFlags = new Flags
        {
            count_in_cargo = flags.count_in_cargo,
            count_in_hoarder = flags.count_in_hoarder,
            count_in_map = flags.count_in_map,
            count_in_player = flags.count_in_player,
            crafted = flags.crafted,
            deloot = flags.deloot
        };

        this.BackColor = Color.FromArgb(60, 63, 65);
        this.ForeColor = SystemColors.Control;
        this.FormBorderStyle = FormBorderStyle.None;
        this.StartPosition = FormStartPosition.Manual;
        this.Padding = new Padding(_borderWidth);
        this.TopMost = true;

        this.Deactivate += (s, e) => CloseAndApply();

        BuildCheckboxes();
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
    private void BuildCheckboxes()
    {
        this.Controls.Clear();

        int borderWidth = 2;
        this.Padding = new Padding(borderWidth);

        var layout = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.TopDown,
            Padding = new Padding(5),
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            Dock = DockStyle.Fill  // fills the form but respects the padding
        };

        int maxWidth = 0;
        foreach (var name in _flagslist)
        {
            var cb = AddCheckbox(layout, name);
            if (cb.PreferredSize.Width > maxWidth)
                maxWidth = cb.PreferredSize.Width;
        }

        this.Controls.Add(layout);

        // Set form width to fit largest checkbox + padding, height auto via layout
        this.ClientSize = new Size(maxWidth + layout.Padding.Horizontal + this.Padding.Horizontal + 10, layout.PreferredSize.Height + this.Padding.Vertical);
    }

    private CheckBox AddCheckbox(FlowLayoutPanel panel, string name)
    {
        var value = (int)typeof(Flags).GetProperty(name).GetValue(_tempFlags);
        var cb = new CheckBox
        {
            Text = name,
            Checked = value == 1,
            AutoSize = true
        };

        cb.CheckedChanged += (s, e) =>
        {
            // Update temp flags
            typeof(Flags).GetProperty(name).SetValue(_tempFlags, cb.Checked ? 1 : 0);

            // Immediately notify main form to refresh cell string
            _flags.count_in_cargo = _tempFlags.count_in_cargo;
            _flags.count_in_hoarder = _tempFlags.count_in_hoarder;
            _flags.count_in_map = _tempFlags.count_in_map;
            _flags.count_in_player = _tempFlags.count_in_player;
            _flags.crafted = _tempFlags.crafted;
            _flags.deloot = _tempFlags.deloot;

            _onFlagsUpdated?.Invoke(_flags);
        };

        panel.Controls.Add(cb);
        return cb;
    }

    private void CloseAndApply()
    {
        // Final update before closing
        _flags.count_in_cargo = _tempFlags.count_in_cargo;
        _flags.count_in_hoarder = _tempFlags.count_in_hoarder;
        _flags.count_in_map = _tempFlags.count_in_map;
        _flags.count_in_player = _tempFlags.count_in_player;
        _flags.crafted = _tempFlags.crafted;
        _flags.deloot = _tempFlags.deloot;

        _onFlagsUpdated?.Invoke(_flags);
        this.Close();
    }
}
