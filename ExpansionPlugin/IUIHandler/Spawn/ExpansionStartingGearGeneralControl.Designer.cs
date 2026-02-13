namespace ExpansionPlugin
{
    partial class ExpansionStartingGearGeneralControl
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
            groupBox49 = new GroupBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            ApplyEnergySourcesCB = new CheckBox();
            SetRandomHealthSGCB = new CheckBox();
            EnableStartingGearCB = new CheckBox();
            groupBox49.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox49
            // 
            groupBox49.Controls.Add(label3);
            groupBox49.Controls.Add(label2);
            groupBox49.Controls.Add(label1);
            groupBox49.Controls.Add(ApplyEnergySourcesCB);
            groupBox49.Controls.Add(SetRandomHealthSGCB);
            groupBox49.Controls.Add(EnableStartingGearCB);
            groupBox49.ForeColor = SystemColors.Control;
            groupBox49.Location = new Point(0, 0);
            groupBox49.Margin = new Padding(4, 3, 4, 3);
            groupBox49.Name = "groupBox49";
            groupBox49.Padding = new Padding(4, 3, 4, 3);
            groupBox49.Size = new Size(195, 118);
            groupBox49.TabIndex = 3;
            groupBox49.TabStop = false;
            groupBox49.Text = "General Settings";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 85);
            label3.Name = "label3";
            label3.Size = new Size(121, 15);
            label3.TabIndex = 5;
            label3.Text = "Apply Energy Sources";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 55);
            label2.Name = "label2";
            label2.Size = new Size(109, 15);
            label2.TabIndex = 4;
            label2.Text = "Set Random Health";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 25);
            label1.Name = "label1";
            label1.Size = new Size(113, 15);
            label1.TabIndex = 3;
            label1.Text = "Enable Starting Gear";
            // 
            // ApplyEnergySourcesCB
            // 
            ApplyEnergySourcesCB.AutoSize = true;
            ApplyEnergySourcesCB.Location = new Point(161, 86);
            ApplyEnergySourcesCB.Margin = new Padding(4, 3, 4, 3);
            ApplyEnergySourcesCB.Name = "ApplyEnergySourcesCB";
            ApplyEnergySourcesCB.Size = new Size(15, 14);
            ApplyEnergySourcesCB.TabIndex = 2;
            ApplyEnergySourcesCB.Tag = "ApplyEnergySources";
            ApplyEnergySourcesCB.UseVisualStyleBackColor = true;
            ApplyEnergySourcesCB.CheckedChanged += ApplyEnergySourcesCB_CheckedChanged;
            // 
            // SetRandomHealthSGCB
            // 
            SetRandomHealthSGCB.AutoSize = true;
            SetRandomHealthSGCB.Location = new Point(161, 56);
            SetRandomHealthSGCB.Margin = new Padding(4, 3, 4, 3);
            SetRandomHealthSGCB.Name = "SetRandomHealthSGCB";
            SetRandomHealthSGCB.Size = new Size(15, 14);
            SetRandomHealthSGCB.TabIndex = 1;
            SetRandomHealthSGCB.Tag = "SetRandomHealthSG";
            SetRandomHealthSGCB.UseVisualStyleBackColor = true;
            SetRandomHealthSGCB.CheckedChanged += SetRandomHealthSGCB_CheckedChanged;
            // 
            // EnableStartingGearCB
            // 
            EnableStartingGearCB.AutoSize = true;
            EnableStartingGearCB.Location = new Point(161, 26);
            EnableStartingGearCB.Margin = new Padding(4, 3, 4, 3);
            EnableStartingGearCB.Name = "EnableStartingGearCB";
            EnableStartingGearCB.Size = new Size(15, 14);
            EnableStartingGearCB.TabIndex = 0;
            EnableStartingGearCB.Tag = "EnableStartingGear";
            EnableStartingGearCB.UseVisualStyleBackColor = true;
            EnableStartingGearCB.CheckedChanged += EnableStartingGearCB_CheckedChanged;
            // 
            // ExpansionStartingGearGeneralControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox49);
            ForeColor = SystemColors.Control;
            Name = "ExpansionStartingGearGeneralControl";
            Size = new Size(195, 118);
            groupBox49.ResumeLayout(false);
            groupBox49.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox49;
        private Label label3;
        private Label label2;
        private Label label1;
        private CheckBox ApplyEnergySourcesCB;
        private CheckBox SetRandomHealthSGCB;
        private CheckBox EnableStartingGearCB;
    }
}
