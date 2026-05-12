using Day2eEditor;
using ExpansionPlugin.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;


namespace ExpansionPlugin
{
    public partial class QuestFlowPreviewForm : Form
    {
        // ─── View State ─────────────────────────
        private float _zoom = 1.0f;
        private readonly float _minZoom = 0.25f;
        private readonly float _maxZoom = 2.5f;
        private Point _panOffset;
        private Point _panStart;
        private bool _panning;

        // ─── Data ──────────────────────────────
        private readonly ExpansionQuestQuest _selectedQuest;
        private readonly IReadOnlyDictionary<int, ExpansionQuestQuest> _allQuests;

        // ─── Graph ─────────────────────────────
        private readonly Dictionary<int, QuestNodeControl> _questNodes = new();
        private readonly Dictionary<Control, (PointF pos, Size size)> _worldTransforms = new();
        private readonly List<Link> _links = new();


        public QuestFlowPreviewForm(ExpansionQuestQuest selectedQuest, IReadOnlyDictionary<int, ExpansionQuestQuest> allQuests)
        {
            InitializeComponent();
            _selectedQuest = selectedQuest;
            _allQuests = allQuests;

           

            BuildGraph();
        }

        // ───────────────────── Input ─────────────────────
        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode =
                System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using var pen = new Pen(Color.Gainsboro, 2);

            foreach (var link in _links)
            {
                if (link.From == null || link.To == null)
                    continue;

                PointF a = GetScreenCenter(link.From);
                PointF b = GetScreenCenter(link.To);

                e.Graphics.DrawLine(pen, a, b);
            }
        }
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                _panning = true;
                _panStart = e.Location;
                Cursor = Cursors.Hand;
            }
        }
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_panning) return;

            _panOffset.X += e.X - _panStart.X;
            _panOffset.Y += e.Y - _panStart.Y;
            _panStart = e.Location;

            ApplyZoomToControls();
            _canvas.Invalidate();
        }
        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            _panning = false;
            Cursor = Cursors.Default;
        }
        private void Canvas_MouseWheel(object sender, MouseEventArgs e)
        {
            float oldZoom = _zoom;

            _zoom = e.Delta > 0
                ? Math.Min(_zoom * 1.1f, _maxZoom)
                : Math.Max(_zoom / 1.1f, _minZoom);

            if (Math.Abs(_zoom - oldZoom) < 0.001f)
                return;

            ApplyZoomToControls();
            _canvas.Invalidate();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.D0))
            {
                _zoom = 1.0f;
                _panOffset = Point.Empty;
                ApplyZoomToControls();
                _canvas.Invalidate();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }




        private string GetObjectiveReferenceText(Objectives objective)
        {
            if (objective == null)
                return "Objective: Missing";

            var objectiveFiles = AppServices.GetRequired<ExpansionManager>().ExpansionQuestObjectiveConfigConfig.MutableItems;
            ExpansionQuestObjectiveConfig objectiveBase = objectiveFiles.FirstOrDefault(x => x.ID == objective.ID && x.ObjectiveType == objective.ObjectiveType);

            return $"🔗 {objectiveBase.ObjectiveType} : {objectiveBase.ObjectiveText}";
        }
        private void AddObjectiveNodes(QuestNodeControl questNode, ExpansionQuestQuest quest)
        {
            if (quest.Objectives == null || quest.Objectives.Count == 0)
                return;

            const int objXOffset = 40;
            const int objYOffset = 40;

            Control previous = null;
            int y = questNode.Bottom + objYOffset;

            foreach (var obj in quest.Objectives)
            {
                var objNode = new ObjectiveNodeControl(GetObjectiveReferenceText(obj))
                {
                    Location = new Point(
                        questNode.Left + objXOffset,
                        y)
                };

                _canvas.Controls.Add(objNode);
                _worldTransforms[objNode] = (objNode.Location, objNode.Size);

                if (quest.SequentialObjectives == 1)
                {
                    if (previous != null)
                        _links.Add(new Link(previous, objNode));
                    else
                        _links.Add(new Link(questNode, objNode));

                    previous = objNode;
                }
                else
                {
                    _links.Add(new Link(questNode, objNode));
                }

                y += objNode.Height + 20;
            }
        }


        private void BuildGraph()
        {
            Dictionary<int, int> columnBottoms = new();
            _links.Clear();
            _questNodes.Clear();

            const int startX = 120;
            const int startY = 150;
            const int xSpacing = 450;
            const int ySpacing = 260;

            // 1. Discover all relevant quests
            var discovered = new HashSet<int>();

            void Discover(ExpansionQuestQuest q)
            {
                if (!q.ID.HasValue || !discovered.Add(q.ID.Value))
                    return;

                if (q.PreQuestIDs != null)
                {
                    foreach (var id in q.PreQuestIDs)
                    {
                        if (_allQuests.TryGetValue(id, out var p))
                            Discover(p);
                    }
                }

                foreach (var candidate in _allQuests.Values)
                {
                    if (candidate.PreQuestIDs?.Contains(q.ID.Value) == true)
                        Discover(candidate);
                }
            }

            Discover(_selectedQuest);

            var quests = discovered
                .Select(id => _allQuests[id])
                .Where(q => q.ID.HasValue)
                .ToList();

            // 2. Compute graph levels (column index)
            var levels = new Dictionary<int, int>();

            int GetLevel(int id)
            {
                if (levels.TryGetValue(id, out var l))
                    return l;

                var q = _allQuests[id];
                if (q.PreQuestIDs == null || q.PreQuestIDs.Count == 0)
                    return levels[id] = 0;

                int maxParent = q.PreQuestIDs
                    .Where(pid => _allQuests.ContainsKey(pid))
                    .Select(pid => GetLevel(pid))
                    .DefaultIfEmpty(0)
                    .Max();

                return levels[id] = maxParent + 1;
            }

            foreach (var q in quests)
                GetLevel(q.ID!.Value);

            // 3. Layout nodes by level
            var grouped = quests
                .GroupBy(q => levels[q.ID!.Value])
                .OrderBy(g => g.Key);

            foreach (var group in grouped)
            {
                int x = startX + group.Key * xSpacing;
                int y = startY;

                foreach (var quest in group)
                {
                    var node = new QuestNodeControl(quest)
                    {
                        Location = new Point(x, y),
                        Highlighted = quest == _selectedQuest
                    };

                    _questNodes[quest.ID!.Value] = node;
                    _canvas.Controls.Add(node);
                    _worldTransforms[node] = (node.Location, node.Size);

                    // Objectives
                    AddObjectiveNodes(node, quest);

                    // NPCs (above)
                    AddNpcNodesAbove(quest.QuestGiverIDs, "Giver", node, true);
                    AddNpcNodesAbove(quest.QuestTurnInIDs, "Turn-In", node, false);


                    // Calculate full vertical footprint
                    int questBottom = node.Bottom;

                    // Include objectives
                    foreach (Control ctrl in _canvas.Controls)
                    {
                        if (ctrl is ObjectiveNodeControl &&
                            ctrl.Left >= node.Left - 10 &&
                            ctrl.Left <= node.Right + 10)
                        {
                            questBottom = Math.Max(questBottom, ctrl.Bottom);
                        }
                    }

                    // Include NPCs above
                    foreach (Control ctrl in _canvas.Controls)
                    {
                        if (ctrl is NpcNodeControl &&
                            ctrl.Bottom <= node.Top)
                        {
                            questBottom = Math.Max(questBottom, ctrl.Bottom);
                        }
                    }

                    // Advance Y safely
                    y = questBottom + 100;

                }
            }

            // 4. Create edges using PreQuestIDs ONLY
            foreach (var quest in quests)
            {
                if (quest.PreQuestIDs == null || !quest.ID.HasValue)
                    continue;

                var to = _questNodes[quest.ID.Value];

                foreach (var preId in quest.PreQuestIDs)
                {
                    if (_questNodes.TryGetValue(preId, out var from))
                        _links.Add(new Link(from, to));
                }
            }

            _canvas.Invalidate();
        }
        private void ApplyZoomToControls()
        {
            foreach (var kv in _worldTransforms)
            {
                Control ctrl = kv.Key;
                var (worldPos, worldSize) = kv.Value;

                ctrl.SuspendLayout();

                ctrl.Location = new Point(
                    (int)(worldPos.X * _zoom + _panOffset.X),
                    (int)(worldPos.Y * _zoom + _panOffset.Y));

                ctrl.Size = new Size(
                    (int)(worldSize.Width * _zoom),
                    (int)(worldSize.Height * _zoom));

                ctrl.ResumeLayout();
            }
        }


        private List<ExpansionQuestQuest> BuildPreChain(ExpansionQuestQuest quest, HashSet<int> visited = null)
        {
            visited ??= new HashSet<int>();
            var result = new List<ExpansionQuestQuest>();

            if (!quest.ID.HasValue || !visited.Add(quest.ID.Value))
                return result;

            if (quest.PreQuestIDs != null)
            {
                foreach (var preId in quest.PreQuestIDs)
                {
                    if (_allQuests.TryGetValue(preId, out var preQuest))
                    {
                        result.AddRange(BuildPreChain(preQuest, visited));
                        result.Add(preQuest);
                    }
                }
            }

            return result;
        }

        private List<ExpansionQuestQuest> BuildFollowUpChain(ExpansionQuestQuest quest, HashSet<int> visited = null)
        {
            visited ??= new HashSet<int>();
            var result = new List<ExpansionQuestQuest>();

            if (!quest.ID.HasValue || !visited.Add(quest.ID.Value))
                return result;

            if (quest.FollowUpQuest.HasValue &&
                _allQuests.TryGetValue(quest.FollowUpQuest.Value, out var next))
            {
                result.Add(next);
                result.AddRange(BuildFollowUpChain(next, visited));
            }

            return result;
        }
        private string GetNPCReferenceText(int id)
        {
            var NPCfiles = AppServices.GetRequired<ExpansionManager>().ExpansionQuestNPCDataConfig.MutableItems;
            ExpansionQuestNPCData npc = NPCfiles.FirstOrDefault(x => x.ID == id);

            return $"🔗 {npc.NPCName} ({npc.ClassName}) {npc.GetNPCType()}";
        }

        private void AddNpcNodesAbove(BindingList<int> ids, string label, QuestNodeControl questNode, bool alignLeft)
        {
            if (ids == null || ids.Count == 0)
                return;

            const int verticalSpacing = 10;
            const int horizontalSpacing = 6;

            int npcY = questNode.Top - 80;

            int x = alignLeft
                ? questNode.Left
                : questNode.Right;

            foreach (var id in ids)
            {
                var npc = new NpcNodeControl($"{label}\n{GetNPCReferenceText(id)}");

                if (!alignLeft)
                    x -= npc.Width;

                npc.Location = new Point(x, npcY);


                _canvas.Controls.Add(npc);
                _worldTransforms[npc] = (npc.Location, npc.Size);


                // Link direction
                if (alignLeft)
                    _links.Add(new Link(npc, questNode));     // Giver → Quest
                else
                    _links.Add(new Link(questNode, npc));     // Quest → Turn‑In

                npcY -= npc.Height + verticalSpacing;
                x += alignLeft ? (npc.Width + horizontalSpacing) : 0;
            }
        }

        private PointF GetScreenCenter(Control c)
        {
            return new PointF(
                c.Left + c.Width / 2f,
                c.Top + c.Height / 2f);
        }
        private List<ExpansionQuestQuest> BuildReversePreChain(ExpansionQuestQuest quest, HashSet<int> visited = null)
        {
            visited ??= new HashSet<int>();
            var result = new List<ExpansionQuestQuest>();

            if (!quest.ID.HasValue || !visited.Add(quest.ID.Value))
                return result;

            foreach (var candidate in _allQuests.Values)
            {
                if (candidate.PreQuestIDs == null)
                    continue;

                if (candidate.PreQuestIDs.Contains(quest.ID.Value))
                {
                    result.Add(candidate);
                    result.AddRange(BuildReversePreChain(candidate, visited));
                }
            }

            return result;
        }
    }
    public class Link
    {
        public Control From { get; }
        public Control To { get; set; }

        public Link(Control from, Control to)
        {
            From = from;
            To = to;
        }
    }

    public class NpcNodeControl : UserControl
    {
        public Point Center =>
            new(Left + Width / 2, Top + Height / 2);

        public NpcNodeControl(string text)
        {
            Width = 120;
            Height = 60;
            BackColor = Color.FromArgb(70, 70, 70);
            ForeColor = Color.White;
            Padding = new Padding(6);

            Controls.Add(new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            });

            Paint += (_, e) =>
            {
                using var pen = new Pen(Color.DimGray, 2);
                e.Graphics.DrawEllipse(pen, 0, 0, Width - 1, Height - 1);
            };

        }
    }

    public class ObjectiveNodeControl : UserControl
    {
        public Point Center =>
            new Point(
                Left + Width / 2,
                Top + Height / 2);

        public ObjectiveNodeControl(string text)
        {
            Width = 260;
            Height = 40;
            BackColor = Color.FromArgb(60, 60, 60);
            ForeColor = Color.White;
            Padding = new Padding(6);

            var label = new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoEllipsis = true
            };

            Controls.Add(label);

            Paint += ObjectiveNodeControl_Paint;
        }

        private void ObjectiveNodeControl_Paint(object sender, PaintEventArgs e)
        {
            using var pen = new Pen(Color.SteelBlue, 2);
            e.Graphics.SmoothingMode =
                System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            e.Graphics.DrawRectangle(
                pen,
                0,
                0,
                Width - 1,
                Height - 1);
        }
    }


}
