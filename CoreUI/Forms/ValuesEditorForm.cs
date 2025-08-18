using Day2eEditor;
using System.ComponentModel;
using System.Xml.Linq;

public partial class ValuesEditorForm: Form
{
    private readonly Color _borderColor = SystemColors.Control;
    private readonly int _borderWidth = 2;

    private readonly List<object> _availableValues;
    private readonly BindingList<Value> _Values;                  // Original flags
    private readonly Action<object> _addSelectedValue; // Callback when popup changes
    private readonly Action<object> _removeSelectedValue; // Callback when popup changes

    public ValuesEditorForm(List<object> availbeValues, BindingList<Value> Values, Action<object> addSelectedValue, Action<object> removeSelectedValue)
    {
        _availableValues = availbeValues;
        _Values = Values;
        _addSelectedValue = addSelectedValue;
        _removeSelectedValue = removeSelectedValue;


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
        foreach (var name in _availableValues)
        {
            if (name is listsValue v)
            {
                var cb = AddCheckbox(layout, v.name);
                cb.Tag = v;
                if (cb.PreferredSize.Width > maxWidth)
                    maxWidth = cb.PreferredSize.Width;
            }
            if (name is user_listsUser1 vv)
            {
                var cb = AddCheckbox(layout, vv.name);
                cb.Tag = vv;
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

        if (_Values != null)
        {
            Value _Value = _Values.FirstOrDefault(x => x.Name == name);
            if (_Value == null)
            {
                _Value = _Values.FirstOrDefault(x => x.User == name);
            }
            if (_Value != null)
            {
                cb.Checked = true;
            }
        }

        cb.CheckedChanged += (s, e) =>
        {
            //updateList
            if (cb.Checked)
            {
                if (cb.Tag is listsValue v)
                {
                    _addSelectedValue?.Invoke(v);
                }
                else if (cb.Tag is user_listsUser1 vv)
                {
                    _addSelectedValue?.Invoke(vv);
                }
            }
            else if (!cb.Checked)
            {
                if (cb.Tag is listsValue v)
                {
                    _removeSelectedValue(v);
                }
                else if (cb.Tag is user_listsUser1 vv)
                {
                    _removeSelectedValue(vv);
                }
            }
        };

        panel.Controls.Add(cb);
        return cb;
    }
}
