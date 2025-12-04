using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionMapPersonalMarkersControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionMapSettings _data;
        private ExpansionMapSettings _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionMapPersonalMarkersControl()
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
            _data = data as ExpansionMapSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            NeedPenItemForCreateMarkerCB.Checked = _data.NeedPenItemForCreateMarker == 1 ? true : false;
            NeedGPSItemForCreateMarkerCB.Checked = _data.NeedGPSItemForCreateMarker == 1 ? true : false;
            CanCreateMarkerCB.Checked = _data.CanCreateMarker == 1 ? true : false;
            CanCreate3DMarkerCB.Checked = _data.CanCreate3DMarker == 1 ? true : false;
            ShowDistanceOnPersonalMarkersCB.Checked = _data.ShowDistanceOnPersonalMarkers == 1 ? true : false;

            _suppressEvents = false;
        }

        /// <summary>
        /// Applies changes to the data and updates the original snapshot
        /// </summary>
        public void ApplyChanges()
        {
            _originalData = CloneData(_data);
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

        #region Helper Methods

        /// <summary>
        /// Clones the data for reset purposes
        /// </summary>

        private ExpansionMapSettings CloneData(ExpansionMapSettings data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            return new ExpansionMapSettings
            {
                m_Version = data.m_Version,
                EnableMap = data.EnableMap,
                UseMapOnMapItem = data.UseMapOnMapItem,
                ShowPlayerPosition = data.ShowPlayerPosition,
                ShowMapStats = data.ShowMapStats,
                NeedPenItemForCreateMarker = data.NeedPenItemForCreateMarker,
                NeedGPSItemForCreateMarker = data.NeedGPSItemForCreateMarker,
                CanCreateMarker = data.CanCreateMarker,
                CanCreate3DMarker = data.CanCreate3DMarker,
                CanOpenMapWithKeyBinding = data.CanOpenMapWithKeyBinding,
                ShowDistanceOnPersonalMarkers = data.ShowDistanceOnPersonalMarkers,
                EnableHUDGPS = data.EnableHUDGPS,
                NeedGPSItemForKeyBinding = data.NeedGPSItemForKeyBinding,
                NeedMapItemForKeyBinding = data.NeedMapItemForKeyBinding,
                EnableServerMarkers = data.EnableServerMarkers,
                ShowNameOnServerMarkers = data.ShowNameOnServerMarkers,
                ShowDistanceOnServerMarkers = data.ShowDistanceOnServerMarkers,
                ServerMarkers = CloneServerMarkers(data.ServerMarkers),
                EnableHUDCompass = data.EnableHUDCompass,
                NeedCompassItemForHUDCompass = data.NeedCompassItemForHUDCompass,
                NeedGPSItemForHUDCompass = data.NeedGPSItemForHUDCompass,
                CompassColor = data.CompassColor,
                CreateDeathMarker = data.CreateDeathMarker,
                PlayerLocationNotifier = data.PlayerLocationNotifier,
                CompassBadgesColor = data.CompassBadgesColor
            };
        }

        private static BindingList<ExpansionServerMarkerData>? CloneServerMarkers(
            BindingList<ExpansionServerMarkerData>? source)
        {
            if (source == null) return null;

            var list = new BindingList<ExpansionServerMarkerData>();
            foreach (var marker in source)
            {
                list.Add(CloneMarker(marker));
            }
            return list;
        }

        private static ExpansionServerMarkerData CloneMarker(ExpansionServerMarkerData? m)
        {
            if (m == null) return new ExpansionServerMarkerData();

            return new ExpansionServerMarkerData
            {
                m_UID = m.m_UID,
                m_Visibility = m.m_Visibility,
                m_Is3D = m.m_Is3D,
                m_Text = m.m_Text,
                m_IconName = m.m_IconName,
                m_Color = m.m_Color,
                m_Position = m.m_Position != null ? (float[])m.m_Position.Clone() : null, // deep clone array
                m_Locked = m.m_Locked,
                m_Persist = m.m_Persist
            };
        }


        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                // TODO: Update _nodes.Last().Text based on _data
            }
        }

        #endregion

        private void CanCreateMarkerCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CanCreateMarker = CanCreateMarkerCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void NeedGPSItemForCreateMarkerCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.NeedGPSItemForCreateMarker = NeedGPSItemForCreateMarkerCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void CanCreate3DMarkerCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CanCreate3DMarker = CanCreate3DMarkerCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void ShowDistanceOnPersonalMarkersCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ShowDistanceOnPersonalMarkers = ShowDistanceOnPersonalMarkersCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void NeedPenItemForCreateMarkerCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.NeedPenItemForCreateMarker = NeedPenItemForCreateMarkerCB.Checked == true ? 1 : 0;
            HasChanges();
        }
    }
}