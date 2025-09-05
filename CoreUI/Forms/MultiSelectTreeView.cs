using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Day2eEditor
{
    public class MultiSelectTreeView : TreeView
    {
         [Category("Behavior")]
        public event EventHandler<NodeRequestTextEventArgs> RequestDisplayText;

        [Category("Behavior")]
        public event EventHandler<NodeRequestTextEventArgs> RequestEditText;

        public List<TreeNode> SelectedNodesList
        {
            get { return m_coll.Cast<TreeNode>().ToList(); }
        }

        private bool m_allowMultiForBatch = false;

        protected override void OnAfterLabelEdit(NodeLabelEditEventArgs e)
        {
            if (e.Label != null) // if the user cancelled the edit this event is still raised, just with a null label
            {
                NodeRequestTextEventArgs displayTextArgs;

                displayTextArgs = new NodeRequestTextEventArgs(e.Node, e.Label);
                this.OnRequestDisplayText(displayTextArgs);

                e.CancelEdit = true; // cancel the built in operation so we can substitute our own

                if (!displayTextArgs.Cancel)
                    e.Node.Text = displayTextArgs.Label;
            }

            base.OnAfterLabelEdit(e);
        }

        protected override void OnBeforeLabelEdit(NodeLabelEditEventArgs e)
        {
            NodeRequestTextEventArgs editTextArgs;

            // get the text to apply to the label
            editTextArgs = new NodeRequestTextEventArgs(e.Node, e.Node.Text);
            this.OnRequestEditText(editTextArgs);

            // cancel the edit if required
            if (editTextArgs.Cancel)
                e.CancelEdit = true;

            // apply the text to the EDIT control
            if (!e.CancelEdit)
            {
                IntPtr editHandle;

                editHandle = NativeMethods.SendMessage(this.Handle, NativeMethods.TVM_GETEDITCONTROL, IntPtr.Zero, IntPtr.Zero); // Get the handle of the EDIT control
                if (editHandle != IntPtr.Zero)
                    NativeMethods.SendMessage(editHandle, NativeMethods.WM_SETTEXT, IntPtr.Zero, editTextArgs.Label); // And apply the text. Simples.
            }

            base.OnBeforeLabelEdit(e);
        }

        /// <summary>
        /// Raises the <see cref="RequestDisplayText" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnRequestDisplayText(NodeRequestTextEventArgs e)
        {
            EventHandler<NodeRequestTextEventArgs> handler;

            handler = this.RequestDisplayText;

            if (handler != null)
                handler(this, e);
        }

        protected virtual void OnRequestEditText(NodeRequestTextEventArgs e)
        {
            EventHandler<NodeRequestTextEventArgs> handler;

            handler = this.RequestEditText;

            if (handler != null)
                handler(this, e);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool SetMultiselect
        {
            get
            {
                return this.m_CanMultiSelect;
            }
            set
            {
                this.m_CanMultiSelect = value;
            }
        }

        // Token: 0x06000003 RID: 3 RVA: 0x00002072 File Offset: 0x00001072
        public MultiSelectTreeView()
        {
            this.m_coll = new ArrayList();
        }

        // Token: 0x06000004 RID: 4 RVA: 0x0000208E File Offset: 0x0000108E
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        // Token: 0x17000002 RID: 2
        // (get) Token: 0x06000005 RID: 5 RVA: 0x0000209C File Offset: 0x0000109C
        // (set) Token: 0x06000006 RID: 6 RVA: 0x000020B4 File Offset: 0x000010B4
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]

        // Token: 0x17000002 RID: 2
        // (get) Token: 0x06000005 RID: 5 RVA: 0x0000209C File Offset: 0x0000109C
        // (set) Token: 0x06000006 RID: 6 RVA: 0x000020B4 File Offset: 0x000010B4
        public ArrayList SelectedNodes
        {
            get
            {
                return this.m_coll;
            }
            set
            {
                this.removePaintFromNodes();
                this.m_coll.Clear();
                this.m_coll = value;
                this.paintSelectedNodes();
            }
        }

        // Token: 0x06000007 RID: 7 RVA: 0x000020D8 File Offset: 0x000010D8
        protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
        {
            base.OnBeforeSelect(e);

            bool bControl = Control.ModifierKeys == Keys.Control;
            bool bShift = Control.ModifierKeys == Keys.Shift;

            // START OF A NEW BATCH: no modifiers => reset selection context
            if (!bControl && !bShift)
            {
                // clear previous multi-selection visuals and data immediately
                if (this.m_coll != null && this.m_coll.Count > 0)
                {
                    this.removePaintFromNodes();
                    this.m_coll.Clear();
                }

                // first node in this batch
                this.m_firstNode = e.Node;
                this.m_lastNode = e.Node;

                // allow multi for this batch only if the first node is a TypeEntry
                this.m_allowMultiForBatch = (e.Node.Tag is TypeEntry);

                // let OnAfterSelect run normally (it will add the node as single select)
                return;
            }

            // If we get here, Ctrl or Shift is pressed (attempting multi-select)
            // If there is no current batch (no first node), initialize it from the clicked node
            if (this.m_firstNode == null)
            {
                this.m_firstNode = e.Node;
                this.m_allowMultiForBatch = (e.Node.Tag is TypeEntry);
            }

            // If the current batch does NOT allow multi select, treat this click as a single-select
            if (!this.m_allowMultiForBatch)
            {
                // disable modifiers for this event so it will be handled as single-select
                bControl = false;
                bShift = false;
            }

            // If Ctrl and the node is already in selection, toggle-off (keep previous behaviour)
            bool flag = bControl && this.m_coll.Contains(e.Node);
            if (flag)
            {
                e.Cancel = true; // keep toggle behavior
                this.removePaintFromNodes();
                this.m_coll.Remove(e.Node);
                this.paintSelectedNodes();
                return;
            }
            if (e.Node.Tag is TypeEntry == false)
            {
                e.Cancel = true; // keep toggle behavior
                this.removePaintFromNodes();
                //this.m_coll.Remove(e.Node);
                this.paintSelectedNodes();
                return;
            }
            // normal multi-select flow will continue in OnAfterSelect
            this.m_lastNode = e.Node;
            if (!bShift)
                this.m_firstNode = e.Node;
        }

        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            base.OnAfterSelect(e);

            bool bControl = Control.ModifierKeys == Keys.Control;
            bool bShift = Control.ModifierKeys == Keys.Shift;

            // If the batch does not allow multi-select, force single-select behavior
            if (!this.m_allowMultiForBatch)
            {
                // ensure we behave as single-select: clear and select only this node
                if (this.m_coll != null && this.m_coll.Count > 0)
                {
                    this.removePaintFromNodes();
                    this.m_coll.Clear();
                }

                this.m_coll.Add(e.Node);
                this.paintSelectedNodes();
                return;
            }

            // From here on, the batch allows multi (first node was a TypeEntry) and modifiers apply

            if (bControl)
            {
                if (!this.m_coll.Contains(e.Node))
                    this.m_coll.Add(e.Node);
                else
                {
                    this.removePaintFromNodes();
                    this.m_coll.Remove(e.Node);
                }
                this.paintSelectedNodes();
            }
            else if (bShift)
            {
                Queue myQueue = new Queue();
                TreeNode uppernode = this.m_firstNode;
                TreeNode bottomnode = e.Node;

                bool bParent = this.isParent(this.m_firstNode, e.Node);
                if (!bParent)
                {
                    bParent = this.isParent(bottomnode, uppernode);
                    if (bParent)
                    {
                        TreeNode t = uppernode;
                        uppernode = bottomnode;
                        bottomnode = t;
                    }
                }

                if (bParent)
                {
                    for (TreeNode i = bottomnode; i != uppernode.Parent; i = i.Parent)
                    {
                        if (!this.m_coll.Contains(i) && i.Tag is TypeEntry)
                        {
                            myQueue.Enqueue(i);
                        }
                    }
                }
                else
                {
                    bool sameLevel = (uppernode.Parent == null && bottomnode.Parent == null) ||
                                     (uppernode.Parent != null && uppernode.Parent.Nodes.Contains(bottomnode));

                    if (sameLevel)
                    {
                        int nIndexUpper = uppernode.Index;
                        int nIndexBottom = bottomnode.Index;
                        if (nIndexBottom < nIndexUpper)
                        {
                            TreeNode t2 = uppernode;
                            uppernode = bottomnode;
                            bottomnode = t2;
                            nIndexUpper = uppernode.Index;
                            nIndexBottom = bottomnode.Index;
                        }
                        TreeNode j = uppernode;
                        while (nIndexUpper <= nIndexBottom)
                        {
                            if (!this.m_coll.Contains(j) && j.Tag is TypeEntry)
                            {
                                myQueue.Enqueue(j);
                            }
                            j = j.NextNode;
                            nIndexUpper++;
                        }
                    }
                    else
                    {
                        if (!this.m_coll.Contains(uppernode) && uppernode.Tag is TypeEntry)
                        {
                            myQueue.Enqueue(uppernode);
                        }
                        if (!this.m_coll.Contains(bottomnode) && bottomnode.Tag is TypeEntry)
                        {
                            myQueue.Enqueue(bottomnode);
                        }
                    }
                }

                this.m_coll.AddRange(myQueue);
                this.paintSelectedNodes();
                this.m_firstNode = e.Node;
            }
            else
            {
                // No modifiers (shouldn't normally reach here because we handled single click at OnBeforeSelect),
                // but keep safe: make single selection
                if (this.m_coll != null && this.m_coll.Count > 0)
                {
                    this.removePaintFromNodes();
                    this.m_coll.Clear();
                }
                this.m_coll.Add(e.Node);
                this.paintSelectedNodes();
            }
        }

        // Token: 0x06000009 RID: 9 RVA: 0x00002444 File Offset: 0x00001444
        protected bool isParent(TreeNode parentNode, TreeNode childNode)
        {
            bool flag = parentNode == childNode;
            bool flag2;
            if (flag)
            {
                flag2 = true;
            }
            else
            {
                TreeNode i = childNode;
                bool bFound = false;
                while (!bFound && i != null)
                {
                    i = i.Parent;
                    bFound = i == parentNode;
                }
                flag2 = bFound;
            }
            return flag2;
        }

        // Token: 0x0600000A RID: 10 RVA: 0x00002488 File Offset: 0x00001488
        protected void paintSelectedNodes()
        {
            foreach (object obj in this.m_coll)
            {
                TreeNode i = (TreeNode)obj;
                i.BackColor = SystemColors.Highlight;
                i.ForeColor = SystemColors.HighlightText;
            }
        }

        // Token: 0x0600000B RID: 11 RVA: 0x000024F8 File Offset: 0x000014F8
        protected void removePaintFromNodes()
        {
            bool flag = this.m_coll.Count == 0;
            if (!flag)
            {
                TreeNode n0 = (TreeNode)this.m_coll[0];
                bool flag2 = n0.TreeView == null;
                if (!flag2)
                {
                    Color back = n0.TreeView.BackColor;
                    Color fore = n0.TreeView.ForeColor;
                    foreach (object obj in this.m_coll)
                    {
                        TreeNode i = (TreeNode)obj;
                        i.BackColor = back;
                        i.ForeColor = fore;
                    }
                }
            }
        }

        // Token: 0x04000001 RID: 1
        protected ArrayList m_coll;

        // Token: 0x04000002 RID: 2
        protected TreeNode m_lastNode;

        // Token: 0x04000003 RID: 3
        protected TreeNode m_firstNode;

        // Token: 0x04000004 RID: 4
        protected bool m_CanMultiSelect = true;
    }
    public class NodeRequestTextEventArgs : CancelEventArgs
    {
        #region Constructors

        public NodeRequestTextEventArgs(TreeNode node, string label)
          : this()
        {
            this.Node = node;
            this.Label = label;
        }

        protected NodeRequestTextEventArgs()
        { }

        #endregion

        #region Properties

        public string Label { get; set; }

        public TreeNode Node { get; protected set; }

        #endregion
    }
    public static class NativeMethods
    {

        #region Constants

        public const int TVN_FIRST = -400;

        public const int TVN_BEGINLABELEDITW = (TVN_FIRST - 59);

        public const int TVN_BEGINLABELEDIT = TVN_BEGINLABELEDITW;

        public const int TVM_GETEDITCONTROL = 0x110F;

        public const int WM_SETTEXT = 0xC;

        public const int WM_USER = 0x0400;

        public const int WM_NOTIFY = 0x004E;

        public const int WM_REFLECT = WM_USER + 0x1c00;

        #endregion

        #region Class Members

        [DllImport("USER32", EntryPoint = "SendMessage", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("USER32", EntryPoint = "SendMessage", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, string lParam);

        #endregion

        #region Nested Types

        [StructLayout(LayoutKind.Sequential)]
        public struct NMHDR
        {
            public IntPtr hwndFrom;

            public IntPtr idFrom;

            public int code;
        }

        #endregion
    }
    public static class TreeNodeExtensions
    {
        public static object FindChildOfType(this TreeNode node, Type type)
        {
            if (node == null)
                return null;

            foreach (TreeNode child in node.Nodes)
            {
                if (child.Tag != null && type.IsInstanceOfType(child.Tag))
                    return child.Tag;

                // recurse into children
                var result = child.FindChildOfType(type);
                if (result != null)
                    return result;
            }

            return null;
        }
        public static object FindParentOfType(this TreeNode node, Type type)
        {
            while (node != null)
            {
                if (node.Tag != null && type.IsInstanceOfType(node.Tag))
                    return node.Tag;

                node = node.Parent;
            }

            return null;
        }
        public static T FindChildOfType<T>(this TreeNode node) where T : class
        {
            if (node == null)
                return null;

            foreach (TreeNode child in node.Nodes)
            {
                if (child.Tag is T match)
                    return match;

                // recurse into grandchildren
                var result = child.FindChildOfType<T>();
                if (result != null)
                    return result;
            }

            return null;
        }
        public static T FindParentOfType<T>(this TreeNode node) where T : class
        {
            while (node != null)
            {
                if (node.Tag is T match)
                    return match;

                node = node.Parent;
            }

            return null;
        }
    }
}
