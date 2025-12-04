namespace ExpansionPlugin
{
    partial class ExpansionMapMapControl
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
            groupBox6 = new GroupBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            EnableMapCB = new CheckBox();
            EnableHUDGPSCB = new CheckBox();
            darkLabel264 = new Label();
            ShowPlayerPositionCB = new ComboBox();
            UseMapOnMapItemCB = new CheckBox();
            CanOpenMapWithKeyBindingCB = new CheckBox();
            NeedMapItemForKeyBindingCB = new CheckBox();
            NeedGPSItemForKeyBindingCB = new CheckBox();
            CreateDeathMarkerCB = new CheckBox();
            ShowMapStatsCB = new CheckBox();
            PlayerLocationNotifierCB = new CheckBox();
            groupBox6.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(flowLayoutPanel1);
            groupBox6.Dock = DockStyle.Fill;
            groupBox6.ForeColor = SystemColors.Control;
            groupBox6.Location = new Point(0, 0);
            groupBox6.Margin = new Padding(4, 3, 4, 3);
            groupBox6.Name = "groupBox6";
            groupBox6.Padding = new Padding(4, 3, 4, 3);
            groupBox6.Size = new Size(260, 296);
            groupBox6.TabIndex = 6;
            groupBox6.TabStop = false;
            groupBox6.Text = "Map";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(EnableMapCB);
            flowLayoutPanel1.Controls.Add(EnableHUDGPSCB);
            flowLayoutPanel1.Controls.Add(darkLabel264);
            flowLayoutPanel1.Controls.Add(ShowPlayerPositionCB);
            flowLayoutPanel1.Controls.Add(UseMapOnMapItemCB);
            flowLayoutPanel1.Controls.Add(CanOpenMapWithKeyBindingCB);
            flowLayoutPanel1.Controls.Add(NeedMapItemForKeyBindingCB);
            flowLayoutPanel1.Controls.Add(NeedGPSItemForKeyBindingCB);
            flowLayoutPanel1.Controls.Add(CreateDeathMarkerCB);
            flowLayoutPanel1.Controls.Add(ShowMapStatsCB);
            flowLayoutPanel1.Controls.Add(PlayerLocationNotifierCB);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(4, 19);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(252, 274);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // EnableMapCB
            // 
            EnableMapCB.AutoSize = true;
            EnableMapCB.ForeColor = SystemColors.Control;
            EnableMapCB.Location = new Point(4, 3);
            EnableMapCB.Margin = new Padding(4, 3, 4, 3);
            EnableMapCB.Name = "EnableMapCB";
            EnableMapCB.Size = new Size(88, 19);
            EnableMapCB.TabIndex = 0;
            EnableMapCB.Text = "Enable Map";
            EnableMapCB.TextAlign = ContentAlignment.MiddleRight;
            EnableMapCB.UseVisualStyleBackColor = true;
            EnableMapCB.CheckedChanged += EnableMapCB_CheckedChanged;
            // 
            // EnableHUDGPSCB
            // 
            EnableHUDGPSCB.AutoSize = true;
            EnableHUDGPSCB.ForeColor = SystemColors.Control;
            EnableHUDGPSCB.Location = new Point(4, 28);
            EnableHUDGPSCB.Margin = new Padding(4, 3, 4, 3);
            EnableHUDGPSCB.Name = "EnableHUDGPSCB";
            EnableHUDGPSCB.Size = new Size(113, 19);
            EnableHUDGPSCB.TabIndex = 0;
            EnableHUDGPSCB.Text = "Enable HUD GPS";
            EnableHUDGPSCB.TextAlign = ContentAlignment.MiddleRight;
            EnableHUDGPSCB.UseVisualStyleBackColor = true;
            EnableHUDGPSCB.CheckedChanged += EnableHUDGPSCB_CheckedChanged;
            // 
            // darkLabel264
            // 
            darkLabel264.AutoSize = true;
            darkLabel264.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel264.Location = new Point(4, 50);
            darkLabel264.Margin = new Padding(4, 0, 4, 0);
            darkLabel264.Name = "darkLabel264";
            darkLabel264.Size = new Size(117, 15);
            darkLabel264.TabIndex = 2;
            darkLabel264.Text = "Show Player Position";
            // 
            // ShowPlayerPositionCB
            // 
            ShowPlayerPositionCB.BackColor = Color.FromArgb(60, 63, 65);
            ShowPlayerPositionCB.ForeColor = SystemColors.Control;
            ShowPlayerPositionCB.FormattingEnabled = true;
            ShowPlayerPositionCB.Items.AddRange(new object[] { "not visible", "visible", "visible if the player have a compass" });
            ShowPlayerPositionCB.Location = new Point(4, 68);
            ShowPlayerPositionCB.Margin = new Padding(4, 3, 4, 3);
            ShowPlayerPositionCB.Name = "ShowPlayerPositionCB";
            ShowPlayerPositionCB.Size = new Size(233, 23);
            ShowPlayerPositionCB.TabIndex = 3;
            ShowPlayerPositionCB.SelectedIndexChanged += ShowPlayerPositionCB_SelectedIndexChanged;
            // 
            // UseMapOnMapItemCB
            // 
            UseMapOnMapItemCB.AutoSize = true;
            UseMapOnMapItemCB.ForeColor = SystemColors.Control;
            UseMapOnMapItemCB.Location = new Point(4, 97);
            UseMapOnMapItemCB.Margin = new Padding(4, 3, 4, 3);
            UseMapOnMapItemCB.Name = "UseMapOnMapItemCB";
            UseMapOnMapItemCB.Size = new Size(145, 19);
            UseMapOnMapItemCB.TabIndex = 1;
            UseMapOnMapItemCB.Text = "Use Map On Map Item";
            UseMapOnMapItemCB.TextAlign = ContentAlignment.MiddleRight;
            UseMapOnMapItemCB.UseVisualStyleBackColor = true;
            UseMapOnMapItemCB.CheckedChanged += UseMapOnMapItemCB_CheckedChanged;
            // 
            // CanOpenMapWithKeyBindingCB
            // 
            CanOpenMapWithKeyBindingCB.AutoSize = true;
            CanOpenMapWithKeyBindingCB.ForeColor = SystemColors.Control;
            CanOpenMapWithKeyBindingCB.Location = new Point(4, 122);
            CanOpenMapWithKeyBindingCB.Margin = new Padding(4, 3, 4, 3);
            CanOpenMapWithKeyBindingCB.Name = "CanOpenMapWithKeyBindingCB";
            CanOpenMapWithKeyBindingCB.Size = new Size(200, 19);
            CanOpenMapWithKeyBindingCB.TabIndex = 5;
            CanOpenMapWithKeyBindingCB.Text = "Can Open Map With Key Binding";
            CanOpenMapWithKeyBindingCB.TextAlign = ContentAlignment.MiddleRight;
            CanOpenMapWithKeyBindingCB.UseVisualStyleBackColor = true;
            CanOpenMapWithKeyBindingCB.CheckedChanged += CanOpenMapWithKeyBindingCB_CheckedChanged;
            // 
            // NeedMapItemForKeyBindingCB
            // 
            NeedMapItemForKeyBindingCB.AutoSize = true;
            NeedMapItemForKeyBindingCB.ForeColor = SystemColors.Control;
            NeedMapItemForKeyBindingCB.Location = new Point(4, 147);
            NeedMapItemForKeyBindingCB.Margin = new Padding(4, 3, 4, 3);
            NeedMapItemForKeyBindingCB.Name = "NeedMapItemForKeyBindingCB";
            NeedMapItemForKeyBindingCB.Size = new Size(194, 19);
            NeedMapItemForKeyBindingCB.TabIndex = 7;
            NeedMapItemForKeyBindingCB.Text = "Need Map Item For Key Binding";
            NeedMapItemForKeyBindingCB.TextAlign = ContentAlignment.MiddleCenter;
            NeedMapItemForKeyBindingCB.UseVisualStyleBackColor = true;
            NeedMapItemForKeyBindingCB.CheckedChanged += NeedMapItemForKeyBindingCB_CheckedChanged;
            // 
            // NeedGPSItemForKeyBindingCB
            // 
            NeedGPSItemForKeyBindingCB.AutoSize = true;
            NeedGPSItemForKeyBindingCB.ForeColor = SystemColors.Control;
            NeedGPSItemForKeyBindingCB.Location = new Point(4, 172);
            NeedGPSItemForKeyBindingCB.Margin = new Padding(4, 3, 4, 3);
            NeedGPSItemForKeyBindingCB.Name = "NeedGPSItemForKeyBindingCB";
            NeedGPSItemForKeyBindingCB.Size = new Size(191, 19);
            NeedGPSItemForKeyBindingCB.TabIndex = 6;
            NeedGPSItemForKeyBindingCB.Text = "Need GPS Item For Key Binding";
            NeedGPSItemForKeyBindingCB.TextAlign = ContentAlignment.MiddleRight;
            NeedGPSItemForKeyBindingCB.UseVisualStyleBackColor = true;
            NeedGPSItemForKeyBindingCB.CheckedChanged += NeedGPSItemForKeyBindingCB_CheckedChanged;
            // 
            // CreateDeathMarkerCB
            // 
            CreateDeathMarkerCB.AutoSize = true;
            CreateDeathMarkerCB.ForeColor = SystemColors.Control;
            CreateDeathMarkerCB.Location = new Point(4, 197);
            CreateDeathMarkerCB.Margin = new Padding(4, 3, 4, 3);
            CreateDeathMarkerCB.Name = "CreateDeathMarkerCB";
            CreateDeathMarkerCB.Size = new Size(134, 19);
            CreateDeathMarkerCB.TabIndex = 8;
            CreateDeathMarkerCB.Text = "Create Death Marker";
            CreateDeathMarkerCB.TextAlign = ContentAlignment.MiddleCenter;
            CreateDeathMarkerCB.UseVisualStyleBackColor = true;
            CreateDeathMarkerCB.CheckedChanged += CreateDeathMarkerCB_CheckedChanged;
            // 
            // ShowMapStatsCB
            // 
            ShowMapStatsCB.AutoSize = true;
            ShowMapStatsCB.ForeColor = SystemColors.Control;
            ShowMapStatsCB.Location = new Point(4, 222);
            ShowMapStatsCB.Margin = new Padding(4, 3, 4, 3);
            ShowMapStatsCB.Name = "ShowMapStatsCB";
            ShowMapStatsCB.Size = new Size(110, 19);
            ShowMapStatsCB.TabIndex = 4;
            ShowMapStatsCB.Text = "Show Map Stats";
            ShowMapStatsCB.TextAlign = ContentAlignment.MiddleRight;
            ShowMapStatsCB.UseVisualStyleBackColor = true;
            ShowMapStatsCB.CheckedChanged += ShowMapStatsCB_CheckedChanged;
            // 
            // PlayerLocationNotifierCB
            // 
            PlayerLocationNotifierCB.AutoSize = true;
            PlayerLocationNotifierCB.ForeColor = SystemColors.Control;
            PlayerLocationNotifierCB.Location = new Point(4, 247);
            PlayerLocationNotifierCB.Margin = new Padding(4, 3, 4, 3);
            PlayerLocationNotifierCB.Name = "PlayerLocationNotifierCB";
            PlayerLocationNotifierCB.Size = new Size(150, 19);
            PlayerLocationNotifierCB.TabIndex = 9;
            PlayerLocationNotifierCB.Text = "Player Location Notifier";
            PlayerLocationNotifierCB.TextAlign = ContentAlignment.MiddleCenter;
            PlayerLocationNotifierCB.UseVisualStyleBackColor = true;
            PlayerLocationNotifierCB.CheckedChanged += PlayerLocationNotifierCB_CheckedChanged;
            // 
            // ExpansionMapMapControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox6);
            ForeColor = SystemColors.Control;
            Name = "ExpansionMapMapControl";
            Size = new Size(260, 296);
            groupBox6.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox6;
        private FlowLayoutPanel flowLayoutPanel1;
        private CheckBox NeedGPSItemForKeyBindingCB;
        private CheckBox CanOpenMapWithKeyBindingCB;
        private CheckBox NeedMapItemForKeyBindingCB;
        private CheckBox CreateDeathMarkerCB;
        private CheckBox ShowMapStatsCB;
        private CheckBox PlayerLocationNotifierCB;
        private CheckBox UseMapOnMapItemCB;
        private ComboBox ShowPlayerPositionCB;
        private CheckBox EnableMapCB;
        private Label darkLabel264;
        private CheckBox EnableHUDGPSCB;
    }
}
