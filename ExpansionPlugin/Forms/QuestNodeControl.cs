using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpansionPlugin.Forms
{
    public partial class QuestNodeControl : UserControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Highlighted { get; set; }
        public ExpansionQuestQuest Quest { get; }
        public Point Center => new(Left + Width / 2, Top + Height / 2);

        public QuestNodeControl(ExpansionQuestQuest quest)
        {
            InitializeComponent();
            Quest = quest;
            QuestID.Text = $"Quest ID: {quest.ID}";
            title.Text = $"{quest.Title}\n{quest.ObjectiveText}";
            flags.Text = BuildFlags(quest);

            if (quest.Objectives != null && quest.Objectives.Any())
            {
                var objPanel = new Panel
                {
                    Dock = DockStyle.Fill
                };

                int y = 0;
                int idx = 1;

                foreach (var obj in quest.Objectives)
                {
                    var lbl = new Label
                    {
                        Text = $"{idx++}. {obj}",
                        AutoSize = false,
                        Height = 18,
                        Top = y,
                        Dock = DockStyle.Top
                    };

                    objPanel.Controls.Add(lbl);
                    lbl.BringToFront();
                    y += lbl.Height;
                }

                Controls.Add(objPanel);
            }
        }
        private static string BuildFlags(ExpansionQuestQuest q)
        {
            List<string> flags = new();

            if (q.IsDailyQuest == 1) flags.Add("Daily");
            if (q.IsWeeklyQuest == 1) flags.Add("Weekly");
            if (q.Repeatable == 1) flags.Add("Repeatable");
            if (q.IsGroupQuest == 1) flags.Add("Group");

            return string.Join(" | ", flags);
        }

        private void QuestNodeControl_Paint(object sender, PaintEventArgs e)
        {
            using var pen = new Pen(Highlighted ? Color.Gold : Color.DimGray, 2);
            e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
        }
    }
}
