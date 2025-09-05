namespace EconomyPlugin
{
    partial class cfgplayerspawnGroupParamsControl
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
            groupBox77 = new GroupBox();
            label160 = new Label();
            GroupParamgroups_as_regularCB = new CheckBox();
            GroupParamsenablegroupsCB = new CheckBox();
            label165 = new Label();
            GroupParamscounterNUD = new NumericUpDown();
            label164 = new Label();
            label163 = new Label();
            GroupParamslifetimeNUD = new NumericUpDown();
            groupBox77.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GroupParamscounterNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GroupParamslifetimeNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox77
            // 
            groupBox77.Controls.Add(label160);
            groupBox77.Controls.Add(GroupParamgroups_as_regularCB);
            groupBox77.Controls.Add(GroupParamsenablegroupsCB);
            groupBox77.Controls.Add(label165);
            groupBox77.Controls.Add(GroupParamscounterNUD);
            groupBox77.Controls.Add(label164);
            groupBox77.Controls.Add(label163);
            groupBox77.Controls.Add(GroupParamslifetimeNUD);
            groupBox77.ForeColor = SystemColors.Control;
            groupBox77.Location = new Point(0, 0);
            groupBox77.Margin = new Padding(4, 3, 4, 3);
            groupBox77.Name = "groupBox77";
            groupBox77.Padding = new Padding(4, 3, 4, 3);
            groupBox77.Size = new Size(229, 123);
            groupBox77.TabIndex = 75;
            groupBox77.TabStop = false;
            groupBox77.Text = "Group Params";
            // 
            // label160
            // 
            label160.AutoSize = true;
            label160.ForeColor = SystemColors.Control;
            label160.Location = new Point(9, 42);
            label160.Margin = new Padding(4, 0, 4, 0);
            label160.Name = "label160";
            label160.Size = new Size(98, 15);
            label160.TabIndex = 13;
            label160.Text = "groups as regular";
            // 
            // GroupParamgroups_as_regularCB
            // 
            GroupParamgroups_as_regularCB.AutoSize = true;
            GroupParamgroups_as_regularCB.Location = new Point(125, 42);
            GroupParamgroups_as_regularCB.Margin = new Padding(4, 3, 4, 3);
            GroupParamgroups_as_regularCB.Name = "GroupParamgroups_as_regularCB";
            GroupParamgroups_as_regularCB.Size = new Size(15, 14);
            GroupParamgroups_as_regularCB.TabIndex = 12;
            GroupParamgroups_as_regularCB.UseVisualStyleBackColor = true;
            // 
            // GroupParamsenablegroupsCB
            // 
            GroupParamsenablegroupsCB.AutoSize = true;
            GroupParamsenablegroupsCB.Location = new Point(125, 18);
            GroupParamsenablegroupsCB.Margin = new Padding(4, 3, 4, 3);
            GroupParamsenablegroupsCB.Name = "GroupParamsenablegroupsCB";
            GroupParamsenablegroupsCB.Size = new Size(15, 14);
            GroupParamsenablegroupsCB.TabIndex = 11;
            GroupParamsenablegroupsCB.UseVisualStyleBackColor = true;
            GroupParamsenablegroupsCB.CheckedChanged += GroupParamsenablegroupsCB_CheckedChanged;
            // 
            // label165
            // 
            label165.AutoSize = true;
            label165.ForeColor = SystemColors.Control;
            label165.Location = new Point(8, 18);
            label165.Margin = new Padding(4, 0, 4, 0);
            label165.Name = "label165";
            label165.Size = new Size(82, 15);
            label165.TabIndex = 0;
            label165.Text = "enable groups";
            // 
            // GroupParamscounterNUD
            // 
            GroupParamscounterNUD.BackColor = Color.FromArgb(60, 63, 65);
            GroupParamscounterNUD.ForeColor = SystemColors.Control;
            GroupParamscounterNUD.Location = new Point(124, 92);
            GroupParamscounterNUD.Margin = new Padding(4, 3, 4, 3);
            GroupParamscounterNUD.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            GroupParamscounterNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            GroupParamscounterNUD.Name = "GroupParamscounterNUD";
            GroupParamscounterNUD.Size = new Size(79, 23);
            GroupParamscounterNUD.TabIndex = 10;
            GroupParamscounterNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // label164
            // 
            label164.AutoSize = true;
            label164.ForeColor = SystemColors.Control;
            label164.Location = new Point(8, 65);
            label164.Margin = new Padding(4, 0, 4, 0);
            label164.Name = "label164";
            label164.Size = new Size(47, 15);
            label164.TabIndex = 7;
            label164.Text = "lifetime";
            // 
            // label163
            // 
            label163.AutoSize = true;
            label163.ForeColor = SystemColors.Control;
            label163.Location = new Point(8, 95);
            label163.Margin = new Padding(4, 0, 4, 0);
            label163.Name = "label163";
            label163.Size = new Size(48, 15);
            label163.TabIndex = 9;
            label163.Text = "counter";
            // 
            // GroupParamslifetimeNUD
            // 
            GroupParamslifetimeNUD.BackColor = Color.FromArgb(60, 63, 65);
            GroupParamslifetimeNUD.ForeColor = SystemColors.Control;
            GroupParamslifetimeNUD.Location = new Point(124, 62);
            GroupParamslifetimeNUD.Margin = new Padding(4, 3, 4, 3);
            GroupParamslifetimeNUD.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            GroupParamslifetimeNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            GroupParamslifetimeNUD.Name = "GroupParamslifetimeNUD";
            GroupParamslifetimeNUD.Size = new Size(79, 23);
            GroupParamslifetimeNUD.TabIndex = 8;
            GroupParamslifetimeNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // cfgplayerspawnGroupParamsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox77);
            ForeColor = SystemColors.Control;
            Name = "cfgplayerspawnGroupParamsControl";
            Size = new Size(238, 129);
            groupBox77.ResumeLayout(false);
            groupBox77.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)GroupParamscounterNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)GroupParamslifetimeNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox77;
        private Label label160;
        private CheckBox GroupParamgroups_as_regularCB;
        private CheckBox GroupParamsenablegroupsCB;
        private Label label165;
        private NumericUpDown GroupParamscounterNUD;
        private Label label164;
        private Label label163;
        private NumericUpDown GroupParamslifetimeNUD;
    }
}
