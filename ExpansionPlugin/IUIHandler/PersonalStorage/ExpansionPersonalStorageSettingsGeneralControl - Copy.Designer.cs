namespace ExpansionPlugin
{
    partial class ExpansionPersonalStorageSettingsGeneralControl
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
            label1 = new Label();
            EnabledCB = new CheckBox();
            label2 = new Label();
            label3 = new Label();
            UsePersonalStorageCaseCB = new CheckBox();
            MaxItemsPerStorageNUD = new NumericUpDown();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MaxItemsPerStorageNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(MaxItemsPerStorageNUD);
            groupBox1.Controls.Add(UsePersonalStorageCaseCB);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(EnabledCB);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(383, 127);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Genral";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(14, 25);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(49, 15);
            label1.TabIndex = 49;
            label1.Text = "Enabled";
            // 
            // EnabledCB
            // 
            EnabledCB.AutoSize = true;
            EnabledCB.ForeColor = SystemColors.Control;
            EnabledCB.Location = new Point(198, 26);
            EnabledCB.Margin = new Padding(4, 3, 4, 3);
            EnabledCB.Name = "EnabledCB";
            EnabledCB.Size = new Size(15, 14);
            EnabledCB.TabIndex = 48;
            EnabledCB.TextAlign = ContentAlignment.MiddleRight;
            EnabledCB.UseVisualStyleBackColor = true;
            EnabledCB.CheckedChanged += EnabledCB_CheckedChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.Control;
            label2.Location = new Point(14, 55);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(145, 15);
            label2.TabIndex = 50;
            label2.Text = "Use Personal Storage Case";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = SystemColors.Control;
            label3.Location = new Point(14, 85);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(124, 15);
            label3.TabIndex = 51;
            label3.Text = "Max Items Per Storage";
            // 
            // UsePersonalStorageCaseCB
            // 
            UsePersonalStorageCaseCB.AutoSize = true;
            UsePersonalStorageCaseCB.ForeColor = SystemColors.Control;
            UsePersonalStorageCaseCB.Location = new Point(198, 56);
            UsePersonalStorageCaseCB.Margin = new Padding(4, 3, 4, 3);
            UsePersonalStorageCaseCB.Name = "UsePersonalStorageCaseCB";
            UsePersonalStorageCaseCB.Size = new Size(15, 14);
            UsePersonalStorageCaseCB.TabIndex = 52;
            UsePersonalStorageCaseCB.TextAlign = ContentAlignment.MiddleRight;
            UsePersonalStorageCaseCB.UseVisualStyleBackColor = true;
            UsePersonalStorageCaseCB.CheckedChanged += UsePersonalStorageCaseCB_CheckedChanged;
            // 
            // MaxItemsPerStorageNUD
            // 
            MaxItemsPerStorageNUD.BackColor = Color.FromArgb(60, 63, 65);
            MaxItemsPerStorageNUD.ForeColor = SystemColors.Control;
            MaxItemsPerStorageNUD.Location = new Point(198, 83);
            MaxItemsPerStorageNUD.Margin = new Padding(4, 3, 4, 3);
            MaxItemsPerStorageNUD.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            MaxItemsPerStorageNUD.Name = "MaxItemsPerStorageNUD";
            MaxItemsPerStorageNUD.Size = new Size(151, 23);
            MaxItemsPerStorageNUD.TabIndex = 53;
            MaxItemsPerStorageNUD.TextAlign = HorizontalAlignment.Center;
            MaxItemsPerStorageNUD.ValueChanged += MaxItemsPerStorageNUD_ValueChanged;
            // 
            // ExpansionPersonalStorageSettingsGeneralControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionPersonalStorageSettingsGeneralControl";
            Size = new Size(383, 127);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)MaxItemsPerStorageNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label label1;
        private CheckBox EnabledCB;
        private Label label3;
        private Label label2;
        private CheckBox UsePersonalStorageCaseCB;
        private NumericUpDown MaxItemsPerStorageNUD;
    }
}
