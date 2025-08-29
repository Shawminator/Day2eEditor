namespace EconomyPlugin
{
    partial class cfggameplayUIDataControl
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
            groupBox31 = new GroupBox();
            use3DMapCB = new CheckBox();
            groupBox32 = new GroupBox();
            label2 = new Label();
            hitDirectionStyleCB = new ComboBox();
            label1 = new Label();
            hitDirectionBehaviourCB = new ComboBox();
            m_Color = new PictureBox();
            darkLabel54 = new Label();
            hitDirectionScatterNUD = new NumericUpDown();
            label55 = new Label();
            hitIndicationPostProcessEnabledCB = new CheckBox();
            hitDirectionOverrideEnabledCB = new CheckBox();
            hitDirectionMaxDurationNUD = new NumericUpDown();
            hitDirectionBreakPointRelativeNUD = new NumericUpDown();
            label53 = new Label();
            label54 = new Label();
            groupBox31.SuspendLayout();
            groupBox32.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_Color).BeginInit();
            ((System.ComponentModel.ISupportInitialize)hitDirectionScatterNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)hitDirectionMaxDurationNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)hitDirectionBreakPointRelativeNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox31
            // 
            groupBox31.Controls.Add(use3DMapCB);
            groupBox31.Controls.Add(groupBox32);
            groupBox31.ForeColor = SystemColors.Control;
            groupBox31.Location = new Point(0, 0);
            groupBox31.Margin = new Padding(4, 3, 4, 3);
            groupBox31.Name = "groupBox31";
            groupBox31.Padding = new Padding(4, 3, 4, 3);
            groupBox31.Size = new Size(625, 223);
            groupBox31.TabIndex = 85;
            groupBox31.TabStop = false;
            groupBox31.Text = "UI Data";
            // 
            // use3DMapCB
            // 
            use3DMapCB.AutoSize = true;
            use3DMapCB.Location = new Point(28, 18);
            use3DMapCB.Margin = new Padding(4, 3, 4, 3);
            use3DMapCB.Name = "use3DMapCB";
            use3DMapCB.Size = new Size(88, 19);
            use3DMapCB.TabIndex = 85;
            use3DMapCB.Text = "use 3D Map";
            use3DMapCB.UseVisualStyleBackColor = true;
            use3DMapCB.CheckedChanged += use3DMapCB_CheckedChanged;
            // 
            // groupBox32
            // 
            groupBox32.Controls.Add(label2);
            groupBox32.Controls.Add(hitDirectionStyleCB);
            groupBox32.Controls.Add(label1);
            groupBox32.Controls.Add(hitDirectionBehaviourCB);
            groupBox32.Controls.Add(m_Color);
            groupBox32.Controls.Add(darkLabel54);
            groupBox32.Controls.Add(hitDirectionScatterNUD);
            groupBox32.Controls.Add(label55);
            groupBox32.Controls.Add(hitIndicationPostProcessEnabledCB);
            groupBox32.Controls.Add(hitDirectionOverrideEnabledCB);
            groupBox32.Controls.Add(hitDirectionMaxDurationNUD);
            groupBox32.Controls.Add(hitDirectionBreakPointRelativeNUD);
            groupBox32.Controls.Add(label53);
            groupBox32.Controls.Add(label54);
            groupBox32.ForeColor = SystemColors.Control;
            groupBox32.Location = new Point(7, 42);
            groupBox32.Margin = new Padding(4, 3, 4, 3);
            groupBox32.Name = "groupBox32";
            groupBox32.Padding = new Padding(4, 3, 4, 3);
            groupBox32.Size = new Size(611, 175);
            groupBox32.TabIndex = 84;
            groupBox32.TabStop = false;
            groupBox32.Text = "Hit Indication Data";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.Control;
            label2.Location = new Point(21, 85);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(32, 15);
            label2.TabIndex = 122;
            label2.Text = "Style";
            // 
            // hitDirectionStyleCB
            // 
            hitDirectionStyleCB.FormattingEnabled = true;
            hitDirectionStyleCB.Items.AddRange(new object[] { "Splash", "Spike", "Arrow" });
            hitDirectionStyleCB.Location = new Point(128, 81);
            hitDirectionStyleCB.Name = "hitDirectionStyleCB";
            hitDirectionStyleCB.Size = new Size(167, 23);
            hitDirectionStyleCB.TabIndex = 121;
            hitDirectionStyleCB.SelectedIndexChanged += hitDirectionStyleCB_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(21, 53);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 120;
            label1.Text = "Behaviour";
            // 
            // hitDirectionBehaviourCB
            // 
            hitDirectionBehaviourCB.FormattingEnabled = true;
            hitDirectionBehaviourCB.Items.AddRange(new object[] { "Disabled", "Static", "Dynamic(WIP)" });
            hitDirectionBehaviourCB.Location = new Point(128, 51);
            hitDirectionBehaviourCB.Name = "hitDirectionBehaviourCB";
            hitDirectionBehaviourCB.Size = new Size(167, 23);
            hitDirectionBehaviourCB.TabIndex = 119;
            hitDirectionBehaviourCB.SelectedIndexChanged += hitDirectionBehaviourCB_SelectedIndexChanged;
            // 
            // m_Color
            // 
            m_Color.Location = new Point(20, 139);
            m_Color.Margin = new Padding(4, 3, 4, 3);
            m_Color.Name = "m_Color";
            m_Color.Size = new Size(275, 15);
            m_Color.TabIndex = 118;
            m_Color.TabStop = false;
            m_Color.Click += m_Color_Click;
            // 
            // darkLabel54
            // 
            darkLabel54.AutoSize = true;
            darkLabel54.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel54.Location = new Point(21, 117);
            darkLabel54.Margin = new Padding(4, 0, 4, 0);
            darkLabel54.Name = "darkLabel54";
            darkLabel54.Size = new Size(144, 15);
            darkLabel54.TabIndex = 117;
            darkLabel54.Text = "Direction Indicator Colour";
            // 
            // hitDirectionScatterNUD
            // 
            hitDirectionScatterNUD.BackColor = Color.FromArgb(60, 63, 65);
            hitDirectionScatterNUD.DecimalPlaces = 1;
            hitDirectionScatterNUD.ForeColor = SystemColors.Control;
            hitDirectionScatterNUD.Location = new Point(509, 81);
            hitDirectionScatterNUD.Margin = new Padding(4, 3, 4, 3);
            hitDirectionScatterNUD.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            hitDirectionScatterNUD.Name = "hitDirectionScatterNUD";
            hitDirectionScatterNUD.Size = new Size(79, 23);
            hitDirectionScatterNUD.TabIndex = 76;
            hitDirectionScatterNUD.TextAlign = HorizontalAlignment.Center;
            hitDirectionScatterNUD.ValueChanged += hitDirectionScatterNUD_ValueChanged;
            // 
            // label55
            // 
            label55.AutoSize = true;
            label55.ForeColor = SystemColors.Control;
            label55.Location = new Point(314, 85);
            label55.Margin = new Padding(4, 0, 4, 0);
            label55.Name = "label55";
            label55.Size = new Size(94, 15);
            label55.TabIndex = 75;
            label55.Text = "Direction Scatter";
            // 
            // hitIndicationPostProcessEnabledCB
            // 
            hitIndicationPostProcessEnabledCB.AutoSize = true;
            hitIndicationPostProcessEnabledCB.Location = new Point(314, 113);
            hitIndicationPostProcessEnabledCB.Margin = new Padding(4, 3, 4, 3);
            hitIndicationPostProcessEnabledCB.Name = "hitIndicationPostProcessEnabledCB";
            hitIndicationPostProcessEnabledCB.Size = new Size(137, 19);
            hitIndicationPostProcessEnabledCB.TabIndex = 74;
            hitIndicationPostProcessEnabledCB.Text = "Post Process Enabled";
            hitIndicationPostProcessEnabledCB.UseVisualStyleBackColor = true;
            hitIndicationPostProcessEnabledCB.CheckedChanged += hitIndicationPostProcessEnabledCB_CheckedChanged;
            // 
            // hitDirectionOverrideEnabledCB
            // 
            hitDirectionOverrideEnabledCB.AutoSize = true;
            hitDirectionOverrideEnabledCB.Location = new Point(21, 22);
            hitDirectionOverrideEnabledCB.Margin = new Padding(4, 3, 4, 3);
            hitDirectionOverrideEnabledCB.Name = "hitDirectionOverrideEnabledCB";
            hitDirectionOverrideEnabledCB.Size = new Size(116, 19);
            hitDirectionOverrideEnabledCB.TabIndex = 71;
            hitDirectionOverrideEnabledCB.Text = "Override Enabled";
            hitDirectionOverrideEnabledCB.UseVisualStyleBackColor = true;
            hitDirectionOverrideEnabledCB.CheckedChanged += hitDirectionOverrideEnabledCB_CheckedChanged;
            // 
            // hitDirectionMaxDurationNUD
            // 
            hitDirectionMaxDurationNUD.BackColor = Color.FromArgb(60, 63, 65);
            hitDirectionMaxDurationNUD.DecimalPlaces = 1;
            hitDirectionMaxDurationNUD.ForeColor = SystemColors.Control;
            hitDirectionMaxDurationNUD.Location = new Point(509, 20);
            hitDirectionMaxDurationNUD.Margin = new Padding(4, 3, 4, 3);
            hitDirectionMaxDurationNUD.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            hitDirectionMaxDurationNUD.Name = "hitDirectionMaxDurationNUD";
            hitDirectionMaxDurationNUD.Size = new Size(79, 23);
            hitDirectionMaxDurationNUD.TabIndex = 70;
            hitDirectionMaxDurationNUD.TextAlign = HorizontalAlignment.Center;
            hitDirectionMaxDurationNUD.ValueChanged += hitDirectionMaxDurationNUD_ValueChanged;
            // 
            // hitDirectionBreakPointRelativeNUD
            // 
            hitDirectionBreakPointRelativeNUD.BackColor = Color.FromArgb(60, 63, 65);
            hitDirectionBreakPointRelativeNUD.DecimalPlaces = 1;
            hitDirectionBreakPointRelativeNUD.ForeColor = SystemColors.Control;
            hitDirectionBreakPointRelativeNUD.Location = new Point(509, 51);
            hitDirectionBreakPointRelativeNUD.Margin = new Padding(4, 3, 4, 3);
            hitDirectionBreakPointRelativeNUD.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            hitDirectionBreakPointRelativeNUD.Name = "hitDirectionBreakPointRelativeNUD";
            hitDirectionBreakPointRelativeNUD.Size = new Size(79, 23);
            hitDirectionBreakPointRelativeNUD.TabIndex = 70;
            hitDirectionBreakPointRelativeNUD.TextAlign = HorizontalAlignment.Center;
            hitDirectionBreakPointRelativeNUD.ValueChanged += hitDirectionBreakPointRelativeNUD_ValueChanged;
            // 
            // label53
            // 
            label53.AutoSize = true;
            label53.ForeColor = SystemColors.Control;
            label53.Location = new Point(314, 55);
            label53.Margin = new Padding(4, 0, 4, 0);
            label53.Name = "label53";
            label53.Size = new Size(111, 15);
            label53.TabIndex = 69;
            label53.Text = "Break Point Relative";
            // 
            // label54
            // 
            label54.AutoSize = true;
            label54.ForeColor = SystemColors.Control;
            label54.Location = new Point(314, 22);
            label54.Margin = new Padding(4, 0, 4, 0);
            label54.Name = "label54";
            label54.Size = new Size(79, 15);
            label54.TabIndex = 69;
            label54.Text = "Max Duration";
            // 
            // cfggameplayUIDataControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox31);
            ForeColor = SystemColors.Control;
            Name = "cfggameplayUIDataControl";
            Size = new Size(637, 226);
            groupBox31.ResumeLayout(false);
            groupBox31.PerformLayout();
            groupBox32.ResumeLayout(false);
            groupBox32.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)m_Color).EndInit();
            ((System.ComponentModel.ISupportInitialize)hitDirectionScatterNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)hitDirectionMaxDurationNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)hitDirectionBreakPointRelativeNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox31;
        private CheckBox use3DMapCB;
        private GroupBox groupBox32;
        private PictureBox m_Color;
        private Label darkLabel54;
        private NumericUpDown hitDirectionScatterNUD;
        private Label label55;
        private CheckBox hitIndicationPostProcessEnabledCB;
        private CheckBox hitDirectionOverrideEnabledCB;
        private NumericUpDown hitDirectionMaxDurationNUD;
        private NumericUpDown hitDirectionBreakPointRelativeNUD;
        private Label label53;
        private Label label54;
        private Label label1;
        private ComboBox hitDirectionBehaviourCB;
        private ComboBox hitDirectionStyleCB;
        private Label label2;
    }
}
