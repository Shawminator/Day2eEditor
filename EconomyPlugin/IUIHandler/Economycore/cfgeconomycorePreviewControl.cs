using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class cfgeconomycorePreviewControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private economyCoreConfig _data;
        private economyCoreConfig _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfgeconomycorePreviewControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Returns the UserControl instance
        /// </summary>
        public Control GetControl() => this;

        /// <summary>
        /// Loads data into the control and stores the selected tree nodes
        /// </summary>
        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as economyCoreConfig ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            var serializer = new XmlSerializer(typeof(economycore));
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            var sw = new StringWriter();
            sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
            var xmlWriter = XmlWriter.Create(sw, new XmlWriterSettings() { OmitXmlDeclaration = true, Indent = true });
            serializer.Serialize(xmlWriter, _data.Data, ns);


            // Format XML nicely
            string formattedXml = PrettyPrintXml(sw.ToString());

            // Load into RichTextBox
            xmlPreview.Text = formattedXml;

            // Apply simple highlighting
            HighlightXml(xmlPreview);

            _suppressEvents = false;
        }
        private string PrettyPrintXml(string xml)
        {
            try
            {
                var doc = new XmlDocument();
                doc.LoadXml(xml);
                using var stringWriter = new StringWriter();
                using var xmlTextWriter = new XmlTextWriter(stringWriter) { Formatting = Formatting.Indented };
                doc.Save(xmlTextWriter);
                return stringWriter.ToString();
            }
            catch
            {
                return xml; // fallback to raw text if invalid
            }
        }
        private void HighlightXml(RichTextBox rtb)
        {
            rtb.SuspendLayout();

            // Reset formatting
            rtb.SelectAll();
            rtb.SelectionColor = Color.Black;

            // Highlight tags
            HighlightPattern(rtb, @"<[^>]+>", Color.Blue);

            // Highlight attributes
            HighlightPattern(rtb, @"\s+\w+\=", Color.Red);

            // Highlight attribute values
            HighlightPattern(rtb, "\".*?\"", Color.Brown);

            rtb.DeselectAll();
            rtb.ResumeLayout();
        }

        private void HighlightPattern(RichTextBox rtb, string pattern, Color color)
        {
            var matches = System.Text.RegularExpressions.Regex.Matches(rtb.Text, pattern);
            foreach (System.Text.RegularExpressions.Match m in matches)
            {
                rtb.Select(m.Index, m.Length);
                rtb.SelectionColor = color;
            }
        }
        /// <summary>
        /// Applies changes to the data and updates the original snapshot
        /// </summary>
        public void ApplyChanges()
        {
        }

        /// <summary>
        /// Resets control fields to the original data
        /// </summary>
        public void Reset()
        {
            // TODO: Reset control fields to _originalData
        }

        /// <summary>
        /// Checks if there are changes and updates the parent file's dirty state
        /// </summary>
        public void HasChanges()
        {
            var parentObj = _nodes.Last().FindParentOfType(_parentType);
            if (parentObj != null)
            {
                dynamic parent = parentObj;
                parent.isDirty = !_data.Equals(_originalData);
            }
        }
    }
}