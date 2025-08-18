using Day2eEditor;
using System.ComponentModel;
using System.Xml.Linq;

public partial class UsageEditorForm: Form
{
    private readonly Color _borderColor = SystemColors.Control;
    private readonly int _borderWidth = 2;

    private readonly List<object> _availableUsages;
    private readonly BindingList<Usage> _Usages;                  // Original flags
    private readonly Action<object> _addSelectedUsage; // Callback when popup changes
    private readonly Action<Usage> _removeSelectedUsage; // Callback when popup changes

    public UsageEditorForm(List<object> availbeUsages, BindingList<Usage> Usages, Action<object> addSelectedUsage, Action<Usage> removeSelectedUsage)
    {
        _availableUsages = availbeUsages;
        _Usages = Usages;
        _addSelectedUsage = addSelectedUsage;
        _removeSelectedUsage = removeSelectedUsage;


        this.BackColor = Color.FromArgb(60, 63, 65);
        this.ForeColor = SystemColors.Control;
        this.FormBorderStyle = FormBorderStyle.None;
        this.StartPosition = FormStartPosition.Manual;
        this.Padding = new Padding(_borderWidth);
        this.TopMost = true;

        this.Deactivate += (s, e) => Close();

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
        foreach (var name in _availableUsages)
        {
            if (name is listsUsage u)
            {
                var cb = AddCheckbox(layout, u.name);
                cb.Tag = u;
                if (cb.PreferredSize.Width > maxWidth)
                    maxWidth = cb.PreferredSize.Width;
            }
            if (name is user_listsUser uu)
            {
                var cb = AddCheckbox(layout, uu.name);
                cb.Tag = uu;
                if (cb.PreferredSize.Width > maxWidth)
                    maxWidth = cb.PreferredSize.Width;
            }
        }

        this.Controls.Add(layout);

        // Set form width to fit largest checkbox + padding, height auto via layout
        this.ClientSize = new Size(maxWidth + layout.Padding.Horizontal + this.Padding.Horizontal + 10, layout.PreferredSize.Height + this.Padding.Vertical);
    }

    private CheckBox AddCheckbox(FlowLayoutPanel panel, string name)
    {
        var cb = new CheckBox
        {
            Text = name,
            AutoSize = true
        };

        Usage _usage = _Usages.FirstOrDefault(x => x.Name == name);
        if(_usage == null)
        {
            _usage = _Usages.FirstOrDefault(x => x.User == name);
        }
        if(_usage != null) 
        {
            cb.Checked = true;
        }

        cb.CheckedChanged += (s, e) =>
        {
            //updateList
            if(cb.Checked)
            {
                if (cb.Tag is listsUsage u)
                {
                    _addSelectedUsage?.Invoke(u);
                }
                else if (cb.Tag is user_listsUser uu)
                {
                    _addSelectedUsage?.Invoke(uu);
                }
            }
            else if (!cb.Checked)
            {
                if (cb.Tag is listsUsage u)
                {
                    Usage removeusage = new Usage()
                    {
                        Name = u.name,
                        NameSpecified = true
                    };
                    _removeSelectedUsage(removeusage);
                }
                else if (cb.Tag is user_listsUser uu)
                {
                    Usage removeusage = new Usage()
                    {
                        User = uu.name,
                        UserSpecified = true
                    };
                    _removeSelectedUsage(removeusage);
                }
            }
        };

        panel.Controls.Add(cb);
        return cb;
    }
}
