namespace ExpansionPlugin
{
    partial class ExpansionMapPersonalMarkersControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox21 = new GroupBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            CanCreateMarkerCB = new CheckBox();
            NeedGPSItemForCreateMarkerCB = new CheckBox();
            CanCreate3DMarkerCB = new CheckBox();
            ShowDistanceOnPersonalMarkersCB = new CheckBox();
            NeedPenItemForCreateMarkerCB = new CheckBox();
            groupBox21.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox21
            // 
            groupBox21.Controls.Add(flowLayoutPanel1);
            groupBox21.Dock = DockStyle.Fill;
            groupBox21.ForeColor = SystemColors.Control;
            groupBox21.Location = new Point(0, 0);
            groupBox21.Margin = new Padding(4, 3, 4, 3);
            groupBox21.Name = "groupBox21";
            groupBox21.Padding = new Padding(4, 3, 4, 3);
            groupBox21.Size = new Size(243, 155);
            groupBox21.TabIndex = 8;
            groupBox21.TabStop = false;
            groupBox21.Text = "Personal Markers";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(CanCreateMarkerCB);
            flowLayoutPanel1.Controls.Add(NeedGPSItemForCreateMarkerCB);
            flowLayoutPanel1.Controls.Add(CanCreate3DMarkerCB);
            flowLayoutPanel1.Controls.Add(ShowDistanceOnPersonalMarkersCB);
            flowLayoutPanel1.Controls.Add(NeedPenItemForCreateMarkerCB);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(4, 19);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(235, 133);
            flowLayoutPanel1.TabIndex = 5;
            // 
            // CanCreateMarkerCB
            // 
            CanCreateMarkerCB.AutoSize = true;
            CanCreateMarkerCB.ForeColor = SystemColors.Control;
            CanCreateMarkerCB.Location = new Point(4, 3);
            CanCreateMarkerCB.Margin = new Padding(4, 3, 4, 3);
            CanCreateMarkerCB.Name = "CanCreateMarkerCB";
            CanCreateMarkerCB.Size = new Size(124, 19);
            CanCreateMarkerCB.TabIndex = 0;
            CanCreateMarkerCB.Text = "Can Create Marker";
            CanCreateMarkerCB.TextAlign = ContentAlignment.MiddleRight;
            CanCreateMarkerCB.UseVisualStyleBackColor = true;
            CanCreateMarkerCB.CheckedChanged += CanCreateMarkerCB_CheckedChanged;
            // 
            // NeedGPSItemForCreateMarkerCB
            // 
            NeedGPSItemForCreateMarkerCB.AutoSize = true;
            NeedGPSItemForCreateMarkerCB.ForeColor = SystemColors.Control;
            NeedGPSItemForCreateMarkerCB.Location = new Point(4, 28);
            NeedGPSItemForCreateMarkerCB.Margin = new Padding(4, 3, 4, 3);
            NeedGPSItemForCreateMarkerCB.Name = "NeedGPSItemForCreateMarkerCB";
            NeedGPSItemForCreateMarkerCB.Size = new Size(202, 19);
            NeedGPSItemForCreateMarkerCB.TabIndex = 3;
            NeedGPSItemForCreateMarkerCB.Text = "Need GPS Item For Create Marker";
            NeedGPSItemForCreateMarkerCB.TextAlign = ContentAlignment.MiddleRight;
            NeedGPSItemForCreateMarkerCB.UseVisualStyleBackColor = true;
            NeedGPSItemForCreateMarkerCB.CheckedChanged += NeedGPSItemForCreateMarkerCB_CheckedChanged;
            // 
            // CanCreate3DMarkerCB
            // 
            CanCreate3DMarkerCB.AutoSize = true;
            CanCreate3DMarkerCB.ForeColor = SystemColors.Control;
            CanCreate3DMarkerCB.Location = new Point(4, 53);
            CanCreate3DMarkerCB.Margin = new Padding(4, 3, 4, 3);
            CanCreate3DMarkerCB.Name = "CanCreate3DMarkerCB";
            CanCreate3DMarkerCB.Size = new Size(141, 19);
            CanCreate3DMarkerCB.TabIndex = 1;
            CanCreate3DMarkerCB.Text = "Can Create 3D Marker";
            CanCreate3DMarkerCB.TextAlign = ContentAlignment.MiddleCenter;
            CanCreate3DMarkerCB.UseVisualStyleBackColor = true;
            CanCreate3DMarkerCB.CheckedChanged += CanCreate3DMarkerCB_CheckedChanged;
            // 
            // ShowDistanceOnPersonalMarkersCB
            // 
            ShowDistanceOnPersonalMarkersCB.AutoSize = true;
            ShowDistanceOnPersonalMarkersCB.ForeColor = SystemColors.Control;
            ShowDistanceOnPersonalMarkersCB.Location = new Point(4, 78);
            ShowDistanceOnPersonalMarkersCB.Margin = new Padding(4, 3, 4, 3);
            ShowDistanceOnPersonalMarkersCB.Name = "ShowDistanceOnPersonalMarkersCB";
            ShowDistanceOnPersonalMarkersCB.Size = new Size(215, 19);
            ShowDistanceOnPersonalMarkersCB.TabIndex = 4;
            ShowDistanceOnPersonalMarkersCB.Text = "Show Distance On Personal Markers";
            ShowDistanceOnPersonalMarkersCB.TextAlign = ContentAlignment.MiddleRight;
            ShowDistanceOnPersonalMarkersCB.UseVisualStyleBackColor = true;
            ShowDistanceOnPersonalMarkersCB.CheckedChanged += ShowDistanceOnPersonalMarkersCB_CheckedChanged;
            // 
            // NeedPenItemForCreateMarkerCB
            // 
            NeedPenItemForCreateMarkerCB.AutoSize = true;
            NeedPenItemForCreateMarkerCB.ForeColor = SystemColors.Control;
            NeedPenItemForCreateMarkerCB.Location = new Point(4, 103);
            NeedPenItemForCreateMarkerCB.Margin = new Padding(4, 3, 4, 3);
            NeedPenItemForCreateMarkerCB.Name = "NeedPenItemForCreateMarkerCB";
            NeedPenItemForCreateMarkerCB.Size = new Size(201, 19);
            NeedPenItemForCreateMarkerCB.TabIndex = 2;
            NeedPenItemForCreateMarkerCB.Text = "Need Pen Item For Create Marker";
            NeedPenItemForCreateMarkerCB.TextAlign = ContentAlignment.MiddleRight;
            NeedPenItemForCreateMarkerCB.UseVisualStyleBackColor = true;
            NeedPenItemForCreateMarkerCB.CheckedChanged += NeedPenItemForCreateMarkerCB_CheckedChanged;
            // 
            // ExpansionMapPersonalMarkersControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox21);
            ForeColor = SystemColors.Control;
            Name = "ExpansionMapPersonalMarkersControl";
            Size = new Size(243, 155);
            groupBox21.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox21;
        private FlowLayoutPanel flowLayoutPanel1;
        private CheckBox CanCreateMarkerCB;
        private CheckBox NeedGPSItemForCreateMarkerCB;
        private CheckBox CanCreate3DMarkerCB;
        private CheckBox ShowDistanceOnPersonalMarkersCB;
        private CheckBox NeedPenItemForCreateMarkerCB;
    }
}
