namespace ExpansionPlugin
{
    partial class ExpansionMapServerMarkerControl
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
            groupBox20 = new GroupBox();
            EnableServerMarkersCB = new CheckBox();
            ShowDistanceOnServerMarkersCB = new CheckBox();
            ShowNameOnServerMarkersCB = new CheckBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            groupBox20.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox20
            // 
            groupBox20.Controls.Add(flowLayoutPanel1);
            groupBox20.Dock = DockStyle.Fill;
            groupBox20.ForeColor = SystemColors.Control;
            groupBox20.Location = new Point(0, 0);
            groupBox20.Margin = new Padding(4, 3, 4, 3);
            groupBox20.Name = "groupBox20";
            groupBox20.Padding = new Padding(4, 3, 4, 3);
            groupBox20.Size = new Size(224, 106);
            groupBox20.TabIndex = 7;
            groupBox20.TabStop = false;
            groupBox20.Text = "Server Markers";
            // 
            // EnableServerMarkersCB
            // 
            EnableServerMarkersCB.AutoSize = true;
            EnableServerMarkersCB.ForeColor = SystemColors.Control;
            EnableServerMarkersCB.Location = new Point(4, 3);
            EnableServerMarkersCB.Margin = new Padding(4, 3, 4, 3);
            EnableServerMarkersCB.Name = "EnableServerMarkersCB";
            EnableServerMarkersCB.Size = new Size(141, 19);
            EnableServerMarkersCB.TabIndex = 0;
            EnableServerMarkersCB.Text = "Enable Server Markers";
            EnableServerMarkersCB.TextAlign = ContentAlignment.MiddleCenter;
            EnableServerMarkersCB.UseVisualStyleBackColor = true;
            EnableServerMarkersCB.CheckedChanged += EnableServerMarkersCB_CheckedChanged;
            // 
            // ShowDistanceOnServerMarkersCB
            // 
            ShowDistanceOnServerMarkersCB.AutoSize = true;
            ShowDistanceOnServerMarkersCB.ForeColor = SystemColors.Control;
            ShowDistanceOnServerMarkersCB.Location = new Point(4, 53);
            ShowDistanceOnServerMarkersCB.Margin = new Padding(4, 3, 4, 3);
            ShowDistanceOnServerMarkersCB.Name = "ShowDistanceOnServerMarkersCB";
            ShowDistanceOnServerMarkersCB.Size = new Size(202, 19);
            ShowDistanceOnServerMarkersCB.TabIndex = 2;
            ShowDistanceOnServerMarkersCB.Text = "Show Distance On Server Markers";
            ShowDistanceOnServerMarkersCB.TextAlign = ContentAlignment.MiddleRight;
            ShowDistanceOnServerMarkersCB.UseVisualStyleBackColor = true;
            ShowDistanceOnServerMarkersCB.CheckedChanged += ShowDistanceOnServerMarkersCB_CheckedChanged;
            // 
            // ShowNameOnServerMarkersCB
            // 
            ShowNameOnServerMarkersCB.AutoSize = true;
            ShowNameOnServerMarkersCB.ForeColor = SystemColors.Control;
            ShowNameOnServerMarkersCB.Location = new Point(4, 28);
            ShowNameOnServerMarkersCB.Margin = new Padding(4, 3, 4, 3);
            ShowNameOnServerMarkersCB.Name = "ShowNameOnServerMarkersCB";
            ShowNameOnServerMarkersCB.Size = new Size(189, 19);
            ShowNameOnServerMarkersCB.TabIndex = 1;
            ShowNameOnServerMarkersCB.Text = "Show Name On Server Markers";
            ShowNameOnServerMarkersCB.TextAlign = ContentAlignment.MiddleRight;
            ShowNameOnServerMarkersCB.UseVisualStyleBackColor = true;
            ShowNameOnServerMarkersCB.CheckedChanged += ShowNameOnServerMarkersCB_CheckedChanged;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(EnableServerMarkersCB);
            flowLayoutPanel1.Controls.Add(ShowNameOnServerMarkersCB);
            flowLayoutPanel1.Controls.Add(ShowDistanceOnServerMarkersCB);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(4, 19);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(216, 84);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // ExpansionMapServerMarkerControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox20);
            ForeColor = SystemColors.Control;
            Name = "ExpansionMapServerMarkerControl";
            Size = new Size(224, 106);
            groupBox20.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox20;
        private FlowLayoutPanel flowLayoutPanel1;
        private CheckBox EnableServerMarkersCB;
        private CheckBox ShowNameOnServerMarkersCB;
        private CheckBox ShowDistanceOnServerMarkersCB;
    }
}
