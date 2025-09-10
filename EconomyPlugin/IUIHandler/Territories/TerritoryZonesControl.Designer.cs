namespace EconomyPlugin
{
    partial class TerritoryZonesControl
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
            groupBox1 = new GroupBox();
            groupBox3 = new GroupBox();
            TerritoriesZonesRadiusNUD = new NumericUpDown();
            label151 = new Label();
            groupBox2 = new GroupBox();
            TerritoriesZonesUseYCB = new CheckBox();
            TerritoriesZonesPOSXNUD = new NumericUpDown();
            TerritoriesZonesPOSZNUD = new NumericUpDown();
            TerritoriesZonesPOSYNUD = new NumericUpDown();
            label193 = new Label();
            label194 = new Label();
            groupBox72 = new GroupBox();
            TerritoriesZonesDynamicMaxNUD = new NumericUpDown();
            TerritoriesZonesDynamicMinNUD = new NumericUpDown();
            label150 = new Label();
            label149 = new Label();
            groupBox73 = new GroupBox();
            TerritoriesZonesStaticMaxNUD = new NumericUpDown();
            TerritoriesZonesStaticMInNUD = new NumericUpDown();
            label152 = new Label();
            label153 = new Label();
            groupBox74 = new GroupBox();
            TerritoriesZonesDynamicRB = new RadioButton();
            TerritoriesZonesHuntingGroundRB = new RadioButton();
            TerritoriesZonesGrazeRB = new RadioButton();
            TerritoriesZonesWaterRB = new RadioButton();
            TerritoriesZonesRestRB = new RadioButton();
            TerritoriesZonesDynamicTB = new TextBox();
            groupBox1.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TerritoriesZonesRadiusNUD).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TerritoriesZonesPOSXNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TerritoriesZonesPOSZNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TerritoriesZonesPOSYNUD).BeginInit();
            groupBox72.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TerritoriesZonesDynamicMaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TerritoriesZonesDynamicMinNUD).BeginInit();
            groupBox73.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TerritoriesZonesStaticMaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TerritoriesZonesStaticMInNUD).BeginInit();
            groupBox74.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(groupBox3);
            groupBox1.Controls.Add(groupBox2);
            groupBox1.Controls.Add(groupBox72);
            groupBox1.Controls.Add(groupBox73);
            groupBox1.Controls.Add(groupBox74);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1130, 155);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Territory Zone";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(TerritoriesZonesRadiusNUD);
            groupBox3.Controls.Add(label151);
            groupBox3.ForeColor = SystemColors.Control;
            groupBox3.Location = new Point(852, 26);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(262, 59);
            groupBox3.TabIndex = 169;
            groupBox3.TabStop = false;
            groupBox3.Text = "Radius";
            // 
            // TerritoriesZonesRadiusNUD
            // 
            TerritoriesZonesRadiusNUD.BackColor = Color.FromArgb(60, 63, 65);
            TerritoriesZonesRadiusNUD.ForeColor = SystemColors.Control;
            TerritoriesZonesRadiusNUD.Location = new Point(116, 24);
            TerritoriesZonesRadiusNUD.Margin = new Padding(4, 3, 4, 3);
            TerritoriesZonesRadiusNUD.Maximum = new decimal(new int[] { 50000, 0, 0, 0 });
            TerritoriesZonesRadiusNUD.Name = "TerritoriesZonesRadiusNUD";
            TerritoriesZonesRadiusNUD.Size = new Size(131, 23);
            TerritoriesZonesRadiusNUD.TabIndex = 168;
            TerritoriesZonesRadiusNUD.TextAlign = HorizontalAlignment.Center;
            TerritoriesZonesRadiusNUD.ValueChanged += TerritoriesZonesRadiusNUD_ValueChanged;
            // 
            // label151
            // 
            label151.AutoSize = true;
            label151.ForeColor = SystemColors.Control;
            label151.Location = new Point(14, 26);
            label151.Margin = new Padding(4, 0, 4, 0);
            label151.Name = "label151";
            label151.Size = new Size(53, 15);
            label151.TabIndex = 167;
            label151.Text = "Radius - ";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(TerritoriesZonesUseYCB);
            groupBox2.Controls.Add(TerritoriesZonesPOSXNUD);
            groupBox2.Controls.Add(TerritoriesZonesPOSZNUD);
            groupBox2.Controls.Add(TerritoriesZonesPOSYNUD);
            groupBox2.Controls.Add(label193);
            groupBox2.Controls.Add(label194);
            groupBox2.ForeColor = SystemColors.Control;
            groupBox2.Location = new Point(567, 22);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(279, 118);
            groupBox2.TabIndex = 166;
            groupBox2.TabStop = false;
            groupBox2.Text = "Position";
            // 
            // TerritoriesZonesUseYCB
            // 
            TerritoriesZonesUseYCB.AutoSize = true;
            TerritoriesZonesUseYCB.CheckAlign = ContentAlignment.MiddleRight;
            TerritoriesZonesUseYCB.Location = new Point(10, 53);
            TerritoriesZonesUseYCB.Margin = new Padding(4, 3, 4, 3);
            TerritoriesZonesUseYCB.Name = "TerritoriesZonesUseYCB";
            TerritoriesZonesUseYCB.RightToLeft = RightToLeft.Yes;
            TerritoriesZonesUseYCB.Size = new Size(58, 19);
            TerritoriesZonesUseYCB.TabIndex = 169;
            TerritoriesZonesUseYCB.Text = ":Use Y";
            TerritoriesZonesUseYCB.UseVisualStyleBackColor = true;
            // 
            // TerritoriesZonesPOSXNUD
            // 
            TerritoriesZonesPOSXNUD.BackColor = Color.FromArgb(60, 63, 65);
            TerritoriesZonesPOSXNUD.DecimalPlaces = 6;
            TerritoriesZonesPOSXNUD.ForeColor = SystemColors.Control;
            TerritoriesZonesPOSXNUD.Location = new Point(74, 22);
            TerritoriesZonesPOSXNUD.Margin = new Padding(4, 3, 4, 3);
            TerritoriesZonesPOSXNUD.Maximum = new decimal(new int[] { 50000, 0, 0, 0 });
            TerritoriesZonesPOSXNUD.Minimum = new decimal(new int[] { 50000, 0, 0, int.MinValue });
            TerritoriesZonesPOSXNUD.Name = "TerritoriesZonesPOSXNUD";
            TerritoriesZonesPOSXNUD.Size = new Size(186, 23);
            TerritoriesZonesPOSXNUD.TabIndex = 160;
            TerritoriesZonesPOSXNUD.TextAlign = HorizontalAlignment.Center;
            TerritoriesZonesPOSXNUD.ValueChanged += TerritoriesZonesPOSXNUD_ValueChanged;
            // 
            // TerritoriesZonesPOSZNUD
            // 
            TerritoriesZonesPOSZNUD.BackColor = Color.FromArgb(60, 63, 65);
            TerritoriesZonesPOSZNUD.DecimalPlaces = 6;
            TerritoriesZonesPOSZNUD.ForeColor = SystemColors.Control;
            TerritoriesZonesPOSZNUD.Location = new Point(74, 82);
            TerritoriesZonesPOSZNUD.Margin = new Padding(4, 3, 4, 3);
            TerritoriesZonesPOSZNUD.Maximum = new decimal(new int[] { 50000, 0, 0, 0 });
            TerritoriesZonesPOSZNUD.Minimum = new decimal(new int[] { 50000, 0, 0, int.MinValue });
            TerritoriesZonesPOSZNUD.Name = "TerritoriesZonesPOSZNUD";
            TerritoriesZonesPOSZNUD.Size = new Size(186, 23);
            TerritoriesZonesPOSZNUD.TabIndex = 161;
            TerritoriesZonesPOSZNUD.TextAlign = HorizontalAlignment.Center;
            TerritoriesZonesPOSZNUD.ValueChanged += TerritoriesZonesPOSZNUD_ValueChanged;
            // 
            // TerritoriesZonesPOSYNUD
            // 
            TerritoriesZonesPOSYNUD.BackColor = Color.FromArgb(60, 63, 65);
            TerritoriesZonesPOSYNUD.DecimalPlaces = 6;
            TerritoriesZonesPOSYNUD.ForeColor = SystemColors.Control;
            TerritoriesZonesPOSYNUD.Location = new Point(74, 52);
            TerritoriesZonesPOSYNUD.Margin = new Padding(4, 3, 4, 3);
            TerritoriesZonesPOSYNUD.Maximum = new decimal(new int[] { 50000, 0, 0, 0 });
            TerritoriesZonesPOSYNUD.Minimum = new decimal(new int[] { 50000, 0, 0, int.MinValue });
            TerritoriesZonesPOSYNUD.Name = "TerritoriesZonesPOSYNUD";
            TerritoriesZonesPOSYNUD.Size = new Size(186, 23);
            TerritoriesZonesPOSYNUD.TabIndex = 164;
            TerritoriesZonesPOSYNUD.TextAlign = HorizontalAlignment.Center;
            TerritoriesZonesPOSYNUD.Visible = false;
            // 
            // label193
            // 
            label193.AutoSize = true;
            label193.Location = new Point(47, 24);
            label193.Margin = new Padding(4, 0, 4, 0);
            label193.Name = "label193";
            label193.Size = new Size(17, 15);
            label193.TabIndex = 162;
            label193.Text = "X:";
            label193.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label194
            // 
            label194.AutoSize = true;
            label194.Location = new Point(47, 84);
            label194.Margin = new Padding(4, 0, 4, 0);
            label194.Name = "label194";
            label194.Size = new Size(17, 15);
            label194.TabIndex = 163;
            label194.Text = "Z:";
            label194.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // groupBox72
            // 
            groupBox72.Controls.Add(TerritoriesZonesDynamicMaxNUD);
            groupBox72.Controls.Add(TerritoriesZonesDynamicMinNUD);
            groupBox72.Controls.Add(label150);
            groupBox72.Controls.Add(label149);
            groupBox72.ForeColor = SystemColors.Control;
            groupBox72.Location = new Point(298, 84);
            groupBox72.Margin = new Padding(4, 3, 4, 3);
            groupBox72.Name = "groupBox72";
            groupBox72.Padding = new Padding(4, 3, 4, 3);
            groupBox72.Size = new Size(262, 66);
            groupBox72.TabIndex = 158;
            groupBox72.TabStop = false;
            groupBox72.Text = "dynamic";
            // 
            // TerritoriesZonesDynamicMaxNUD
            // 
            TerritoriesZonesDynamicMaxNUD.BackColor = Color.FromArgb(60, 63, 65);
            TerritoriesZonesDynamicMaxNUD.ForeColor = SystemColors.Control;
            TerritoriesZonesDynamicMaxNUD.Location = new Point(168, 22);
            TerritoriesZonesDynamicMaxNUD.Margin = new Padding(4, 3, 4, 3);
            TerritoriesZonesDynamicMaxNUD.Maximum = new decimal(new int[] { 30000, 0, 0, 0 });
            TerritoriesZonesDynamicMaxNUD.Minimum = new decimal(new int[] { 30000, 0, 0, int.MinValue });
            TerritoriesZonesDynamicMaxNUD.Name = "TerritoriesZonesDynamicMaxNUD";
            TerritoriesZonesDynamicMaxNUD.Size = new Size(79, 23);
            TerritoriesZonesDynamicMaxNUD.TabIndex = 132;
            TerritoriesZonesDynamicMaxNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // TerritoriesZonesDynamicMinNUD
            // 
            TerritoriesZonesDynamicMinNUD.BackColor = Color.FromArgb(60, 63, 65);
            TerritoriesZonesDynamicMinNUD.ForeColor = SystemColors.Control;
            TerritoriesZonesDynamicMinNUD.Location = new Point(48, 22);
            TerritoriesZonesDynamicMinNUD.Margin = new Padding(4, 3, 4, 3);
            TerritoriesZonesDynamicMinNUD.Maximum = new decimal(new int[] { 30000, 0, 0, 0 });
            TerritoriesZonesDynamicMinNUD.Minimum = new decimal(new int[] { 30000, 0, 0, int.MinValue });
            TerritoriesZonesDynamicMinNUD.Name = "TerritoriesZonesDynamicMinNUD";
            TerritoriesZonesDynamicMinNUD.Size = new Size(79, 23);
            TerritoriesZonesDynamicMinNUD.TabIndex = 131;
            TerritoriesZonesDynamicMinNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // label150
            // 
            label150.AutoSize = true;
            label150.ForeColor = SystemColors.Control;
            label150.Location = new Point(14, 24);
            label150.Margin = new Padding(4, 0, 4, 0);
            label150.Name = "label150";
            label150.Size = new Size(28, 15);
            label150.TabIndex = 130;
            label150.Text = "Min";
            // 
            // label149
            // 
            label149.AutoSize = true;
            label149.ForeColor = SystemColors.Control;
            label149.Location = new Point(131, 24);
            label149.Margin = new Padding(4, 0, 4, 0);
            label149.Name = "label149";
            label149.Size = new Size(30, 15);
            label149.TabIndex = 133;
            label149.Text = "Max";
            // 
            // groupBox73
            // 
            groupBox73.Controls.Add(TerritoriesZonesStaticMaxNUD);
            groupBox73.Controls.Add(TerritoriesZonesStaticMInNUD);
            groupBox73.Controls.Add(label152);
            groupBox73.Controls.Add(label153);
            groupBox73.ForeColor = SystemColors.Control;
            groupBox73.Location = new Point(298, 19);
            groupBox73.Margin = new Padding(4, 3, 4, 3);
            groupBox73.Name = "groupBox73";
            groupBox73.Padding = new Padding(4, 3, 4, 3);
            groupBox73.Size = new Size(262, 66);
            groupBox73.TabIndex = 159;
            groupBox73.TabStop = false;
            groupBox73.Text = "Static";
            // 
            // TerritoriesZonesStaticMaxNUD
            // 
            TerritoriesZonesStaticMaxNUD.BackColor = Color.FromArgb(60, 63, 65);
            TerritoriesZonesStaticMaxNUD.ForeColor = SystemColors.Control;
            TerritoriesZonesStaticMaxNUD.Location = new Point(168, 22);
            TerritoriesZonesStaticMaxNUD.Margin = new Padding(4, 3, 4, 3);
            TerritoriesZonesStaticMaxNUD.Maximum = new decimal(new int[] { 30000, 0, 0, 0 });
            TerritoriesZonesStaticMaxNUD.Minimum = new decimal(new int[] { 30000, 0, 0, int.MinValue });
            TerritoriesZonesStaticMaxNUD.Name = "TerritoriesZonesStaticMaxNUD";
            TerritoriesZonesStaticMaxNUD.Size = new Size(79, 23);
            TerritoriesZonesStaticMaxNUD.TabIndex = 132;
            TerritoriesZonesStaticMaxNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // TerritoriesZonesStaticMInNUD
            // 
            TerritoriesZonesStaticMInNUD.BackColor = Color.FromArgb(60, 63, 65);
            TerritoriesZonesStaticMInNUD.ForeColor = SystemColors.Control;
            TerritoriesZonesStaticMInNUD.Location = new Point(48, 22);
            TerritoriesZonesStaticMInNUD.Margin = new Padding(4, 3, 4, 3);
            TerritoriesZonesStaticMInNUD.Maximum = new decimal(new int[] { 30000, 0, 0, 0 });
            TerritoriesZonesStaticMInNUD.Minimum = new decimal(new int[] { 30000, 0, 0, int.MinValue });
            TerritoriesZonesStaticMInNUD.Name = "TerritoriesZonesStaticMInNUD";
            TerritoriesZonesStaticMInNUD.Size = new Size(79, 23);
            TerritoriesZonesStaticMInNUD.TabIndex = 131;
            TerritoriesZonesStaticMInNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // label152
            // 
            label152.AutoSize = true;
            label152.ForeColor = SystemColors.Control;
            label152.Location = new Point(14, 24);
            label152.Margin = new Padding(4, 0, 4, 0);
            label152.Name = "label152";
            label152.Size = new Size(28, 15);
            label152.TabIndex = 130;
            label152.Text = "Min";
            // 
            // label153
            // 
            label153.AutoSize = true;
            label153.ForeColor = SystemColors.Control;
            label153.Location = new Point(131, 24);
            label153.Margin = new Padding(4, 0, 4, 0);
            label153.Name = "label153";
            label153.Size = new Size(30, 15);
            label153.TabIndex = 133;
            label153.Text = "Max";
            // 
            // groupBox74
            // 
            groupBox74.Controls.Add(TerritoriesZonesDynamicRB);
            groupBox74.Controls.Add(TerritoriesZonesHuntingGroundRB);
            groupBox74.Controls.Add(TerritoriesZonesGrazeRB);
            groupBox74.Controls.Add(TerritoriesZonesWaterRB);
            groupBox74.Controls.Add(TerritoriesZonesRestRB);
            groupBox74.Controls.Add(TerritoriesZonesDynamicTB);
            groupBox74.ForeColor = SystemColors.Control;
            groupBox74.Location = new Point(11, 18);
            groupBox74.Margin = new Padding(4, 3, 4, 3);
            groupBox74.Name = "groupBox74";
            groupBox74.Padding = new Padding(4, 3, 4, 3);
            groupBox74.Size = new Size(279, 132);
            groupBox74.TabIndex = 157;
            groupBox74.TabStop = false;
            groupBox74.Text = "AI Usage";
            // 
            // TerritoriesZonesDynamicRB
            // 
            TerritoriesZonesDynamicRB.AutoSize = true;
            TerritoriesZonesDynamicRB.Location = new Point(8, 75);
            TerritoriesZonesDynamicRB.Margin = new Padding(4, 3, 4, 3);
            TerritoriesZonesDynamicRB.Name = "TerritoriesZonesDynamicRB";
            TerritoriesZonesDynamicRB.Size = new Size(72, 19);
            TerritoriesZonesDynamicRB.TabIndex = 7;
            TerritoriesZonesDynamicRB.TabStop = true;
            TerritoriesZonesDynamicRB.Text = "Dynamic";
            TerritoriesZonesDynamicRB.UseVisualStyleBackColor = true;
            // 
            // TerritoriesZonesHuntingGroundRB
            // 
            TerritoriesZonesHuntingGroundRB.AutoSize = true;
            TerritoriesZonesHuntingGroundRB.Location = new Point(134, 48);
            TerritoriesZonesHuntingGroundRB.Margin = new Padding(4, 3, 4, 3);
            TerritoriesZonesHuntingGroundRB.Name = "TerritoriesZonesHuntingGroundRB";
            TerritoriesZonesHuntingGroundRB.Size = new Size(109, 19);
            TerritoriesZonesHuntingGroundRB.TabIndex = 6;
            TerritoriesZonesHuntingGroundRB.TabStop = true;
            TerritoriesZonesHuntingGroundRB.Text = "HuntingGround";
            TerritoriesZonesHuntingGroundRB.UseVisualStyleBackColor = true;
            // 
            // TerritoriesZonesGrazeRB
            // 
            TerritoriesZonesGrazeRB.AutoSize = true;
            TerritoriesZonesGrazeRB.Location = new Point(134, 22);
            TerritoriesZonesGrazeRB.Margin = new Padding(4, 3, 4, 3);
            TerritoriesZonesGrazeRB.Name = "TerritoriesZonesGrazeRB";
            TerritoriesZonesGrazeRB.Size = new Size(54, 19);
            TerritoriesZonesGrazeRB.TabIndex = 5;
            TerritoriesZonesGrazeRB.TabStop = true;
            TerritoriesZonesGrazeRB.Text = "Graze";
            TerritoriesZonesGrazeRB.UseVisualStyleBackColor = true;
            // 
            // TerritoriesZonesWaterRB
            // 
            TerritoriesZonesWaterRB.AutoSize = true;
            TerritoriesZonesWaterRB.Location = new Point(8, 48);
            TerritoriesZonesWaterRB.Margin = new Padding(4, 3, 4, 3);
            TerritoriesZonesWaterRB.Name = "TerritoriesZonesWaterRB";
            TerritoriesZonesWaterRB.Size = new Size(56, 19);
            TerritoriesZonesWaterRB.TabIndex = 4;
            TerritoriesZonesWaterRB.TabStop = true;
            TerritoriesZonesWaterRB.Text = "Water";
            TerritoriesZonesWaterRB.UseVisualStyleBackColor = true;
            // 
            // TerritoriesZonesRestRB
            // 
            TerritoriesZonesRestRB.AutoSize = true;
            TerritoriesZonesRestRB.Location = new Point(8, 22);
            TerritoriesZonesRestRB.Margin = new Padding(4, 3, 4, 3);
            TerritoriesZonesRestRB.Name = "TerritoriesZonesRestRB";
            TerritoriesZonesRestRB.Size = new Size(47, 19);
            TerritoriesZonesRestRB.TabIndex = 3;
            TerritoriesZonesRestRB.TabStop = true;
            TerritoriesZonesRestRB.Text = "Rest";
            TerritoriesZonesRestRB.UseVisualStyleBackColor = true;
            // 
            // TerritoriesZonesDynamicTB
            // 
            TerritoriesZonesDynamicTB.BackColor = Color.FromArgb(69, 73, 74);
            TerritoriesZonesDynamicTB.BorderStyle = BorderStyle.FixedSingle;
            TerritoriesZonesDynamicTB.ForeColor = SystemColors.Control;
            TerritoriesZonesDynamicTB.Location = new Point(7, 99);
            TerritoriesZonesDynamicTB.Margin = new Padding(4, 3, 4, 3);
            TerritoriesZonesDynamicTB.Name = "TerritoriesZonesDynamicTB";
            TerritoriesZonesDynamicTB.Size = new Size(264, 23);
            TerritoriesZonesDynamicTB.TabIndex = 2;
            // 
            // TerritoryZonesControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "TerritoryZonesControl";
            Size = new Size(1137, 161);
            groupBox1.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TerritoriesZonesRadiusNUD).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TerritoriesZonesPOSXNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)TerritoriesZonesPOSZNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)TerritoriesZonesPOSYNUD).EndInit();
            groupBox72.ResumeLayout(false);
            groupBox72.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TerritoriesZonesDynamicMaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)TerritoriesZonesDynamicMinNUD).EndInit();
            groupBox73.ResumeLayout(false);
            groupBox73.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TerritoriesZonesStaticMaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)TerritoriesZonesStaticMInNUD).EndInit();
            groupBox74.ResumeLayout(false);
            groupBox74.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox74;
        private RadioButton TerritoriesZonesDynamicRB;
        private RadioButton TerritoriesZonesHuntingGroundRB;
        private RadioButton TerritoriesZonesGrazeRB;
        private RadioButton TerritoriesZonesWaterRB;
        private RadioButton TerritoriesZonesRestRB;
        private TextBox TerritoriesZonesDynamicTB;
        private GroupBox groupBox72;
        private NumericUpDown TerritoriesZonesDynamicMaxNUD;
        private NumericUpDown TerritoriesZonesDynamicMinNUD;
        private Label label150;
        private Label label149;
        private GroupBox groupBox73;
        private NumericUpDown TerritoriesZonesStaticMaxNUD;
        private NumericUpDown TerritoriesZonesStaticMInNUD;
        private Label label152;
        private Label label153;
        private GroupBox groupBox2;
        private NumericUpDown TerritoriesZonesPOSXNUD;
        private NumericUpDown TerritoriesZonesPOSZNUD;
        private NumericUpDown TerritoriesZonesPOSYNUD;
        private Label label193;
        private Label label194;
        private CheckBox TerritoriesZonesUseYCB;
        private Label label151;
        private NumericUpDown TerritoriesZonesRadiusNUD;
        private GroupBox groupBox3;
    }
}
