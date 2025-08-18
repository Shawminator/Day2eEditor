using Day2eEditor;
using System.ComponentModel;
using System.Xml.Linq;

public partial class TagsEditorForm: Form
{
    private readonly Color _borderColor = SystemColors.Control;
    private readonly int _borderWidth = 2;

    private readonly List<listsTag> _availableTags;
    private readonly BindingList<Tag> _Tags;                  // Original flags
    private readonly Action<listsTag> _addSelectedTag; // Callback when popup changes
    private readonly Action<Tag> _removeSelectedTag; // Callback when popup changes

    public TagsEditorForm(List<listsTag> availbeTags, BindingList<Tag> Tags, Action<listsTag> addSelectedTag, Action<Tag> removeSelectedTag)
    {
        _availableTags = availbeTags;
        _Tags = Tags;
        _addSelectedTag = addSelectedTag;
        _removeSelectedTag = removeSelectedTag;


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
        foreach (var name in _availableTags)
        {
            if (name is listsTag t)
            {
                var cb = AddCheckbox(layout, t.name);
                cb.Tag = t;
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

        Tag _usage = _Tags.FirstOrDefault(x => x.Name == name);
        if(_usage != null) 
        {
            cb.Checked = true;
        }

        cb.CheckedChanged += (s, e) =>
        {
            //updateList
            if(cb.Checked)
            {
                if (cb.Tag is listsTag t)
                {
                    _addSelectedTag?.Invoke(t);
                }
            }
            else if (!cb.Checked)
            {
                if (cb.Tag is listsTag t)
                {
                    Tag removetag = new Tag()
                    {
                        Name = t.name,
                        NameSpecified = true
                    };
                    _removeSelectedTag(removetag);
                }
            }
        };

        panel.Controls.Add(cb);
        return cb;
    }
}
