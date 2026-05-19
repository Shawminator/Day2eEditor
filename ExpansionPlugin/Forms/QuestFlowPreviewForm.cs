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
        private PointF GetScreenCenter(Control c)
        {
            return new PointF(
                c.Left + c.Width / 2f,
                c.Top + c.Height / 2f);
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

        // ───────────────────── Get Text From Classes ─────────────────────
        private string GetNPCReferenceText(int id)
        {
            var NPCfiles = AppServices.GetRequired<ExpansionManager>().ExpansionQuestNPCDataConfig.MutableItems;
            ExpansionQuestNPCData npc = NPCfiles.FirstOrDefault(x => x.ID == id);

            return $"{npc.NPCName}";
        }
        private string GetObjectiveReferenceText(Objectives objective)
        {
            if (objective == null)
                return "Objective: Missing";

            var objectiveFiles = AppServices.GetRequired<ExpansionManager>().ExpansionQuestObjectiveConfigConfig.MutableItems;
            ExpansionQuestObjectiveConfig objectiveBase = objectiveFiles.FirstOrDefault(x => x.ID == objective.ID && x.ObjectiveType == objective.ObjectiveType);

            return $"{objectiveBase.ObjectiveType} : {objectiveBase.ObjectiveText}";
        }
        // ───────────────────── Build Graph ─────────────────────
        private void BuildGraph()
        {
            _links.Clear();
            _questNodes.Clear();

            const int startX = 120;
            const int startY = 150;
            const int xSpacing = 450;

            // ─────────────────────────────────────────────
            // 1. Build unified directed graph
            // ─────────────────────────────────────────────

            var outgoing = new Dictionary<int, List<int>>();
            var incoming = new Dictionary<int, List<int>>();

            void AddEdge(int from, int to)
            {
                if (!outgoing.ContainsKey(from))
                    outgoing[from] = new List<int>();

                if (!incoming.ContainsKey(to))
                    incoming[to] = new List<int>();

                outgoing[from].Add(to);
                incoming[to].Add(from);
            }

            foreach (var q in _allQuests.Values)
            {
                if (!q.ID.HasValue)
                    continue;

                int id = q.ID.Value;

                outgoing.TryAdd(id, new List<int>());
                incoming.TryAdd(id, new List<int>());

                // ─────────────────────────────
                // PRE QUESTS (A → B dependency)
                // B requires A
                // ─────────────────────────────
                if (q.PreQuestReferencesList != null)
                {
                    foreach (var pre in q.PreQuestReferencesList)
                    {
                        if (_allQuests.ContainsKey(pre.QuestID))
                        {
                            AddEdge(pre.QuestID, id);
                        }
                    }
                }

                // ─────────────────────────────
                // FOLLOW UP (A → B story flow)
                // A leads to B
                // ─────────────────────────────
                if (q.FollowUpQuestReference?.QuestID > 0 &&
                    _allQuests.ContainsKey(q.FollowUpQuestReference.QuestID))
                {
                    AddEdge(id, q.FollowUpQuestReference.QuestID);
                }
            }

            // ─────────────────────────────────────────────
            // 2. Discover full reachable graph
            // (includes BOTH pre + follow chains)
            // ─────────────────────────────────────────────

            var discovered = new HashSet<int>();
            var stack = new Stack<int>();

            if (_selectedQuest?.ID.HasValue == true)
                stack.Push(_selectedQuest.ID.Value);

            while (stack.Count > 0)
            {
                int id = stack.Pop();
                if (!discovered.Add(id))
                    continue;

                // go forward through full story graph
                if (outgoing.TryGetValue(id, out var next))
                {
                    foreach (var n in next)
                        stack.Push(n);
                }

                // also go backward (important for full context view)
                if (incoming.TryGetValue(id, out var prev))
                {
                    foreach (var p in prev)
                        stack.Push(p);
                }
            }

            var quests = discovered
                .Where(id => _allQuests.ContainsKey(id))
                .Select(id => _allQuests[id])
                .Where(q => q.ID.HasValue)
                .ToList();

            // ─────────────────────────────────────────────
            // 3. Level calculation (based on PRECEDENCE)
            // ─────────────────────────────────────────────

            var levels = new Dictionary<int, int>();

            int GetLevel(int id)
            {
                if (levels.TryGetValue(id, out var l))
                    return l;

                if (!incoming.TryGetValue(id, out var parents) || parents.Count == 0)
                    return levels[id] = 0;

                int maxParent = parents
                    .Select(GetLevel)
                    .DefaultIfEmpty(0)
                    .Max();

                return levels[id] = maxParent + 1;
            }

            foreach (var q in quests)
                GetLevel(q.ID!.Value);

            // ─────────────────────────────────────────────
            // 4. Layout nodes
            // ─────────────────────────────────────────────

            var grouped = quests
                .GroupBy(q => levels[q.ID!.Value])
                .OrderBy(g => g.Key);

            foreach (var group in grouped)
            {
                int x = startX + group.Key * xSpacing;
                int y = startY;

                // IMPORTANT: enforce stable order inside column
                var orderedGroup = group
                    .OrderBy(q => q.ID.Value) // or replace with better ordering if you want
                    .ToList();

                foreach (var quest in orderedGroup)
                {
                    var node = new QuestNodeControl(quest)
                    {
                        Location = new Point(x, y),
                        Highlighted = quest == _selectedQuest
                    };

                    _questNodes[quest.ID!.Value] = node;
                    _canvas.Controls.Add(node);
                    _worldTransforms[node] = (node.Location, node.Size);

                    AddObjectiveNodes(node, quest);
                    AddNpcNodesAbove(quest.QuestGiverIDs, "Giver", node, true);
                    AddNpcNodesAbove(quest.QuestTurnInIDs, "Turn-In", node, false);

                    // ✅ FIX: DO NOT scan canvas anymore
                    int height =
                        node.Height +
                        (quest.Objectives?.Count ?? 0) * 40 +
                        80;

                    y += height + 80;
                }
            }

            // ─────────────────────────────────────────────
            // 5. Draw ALL edges (pre + follow unified)
            // ─────────────────────────────────────────────

            foreach (var from in outgoing)
            {
                if (!_questNodes.TryGetValue(from.Key, out var fromNode))
                    continue;

                foreach (var toId in from.Value)
                {
                    if (_questNodes.TryGetValue(toId, out var toNode))
                    {
                        _links.Add(new Link(fromNode, toNode));
                    }
                }
            }

            _canvas.Invalidate();
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
                var objNode = new ObjectiveNodeControl(GetObjectiveReferenceText(obj), obj.ObjectiveType)
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
                var npc = new NpcNodeControl($"{label}\n{GetNPCReferenceText(id)}", label);

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
}
