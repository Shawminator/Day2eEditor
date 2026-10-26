namespace EconomyPlugin
{
    partial class cfggameplayVehicleDataControl
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
            groupBox95 = new GroupBox();
            canDamageAttachedBikeWheelsCB = new CheckBox();
            canDetachAttachedBikeWheelsCB = new CheckBox();
            canDamageAttachedCarWheelsCB = new CheckBox();
            canDetachAttachedCarWheelsCB = new CheckBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            boatDecayMultiplierNUD = new NumericUpDown();
            label198 = new Label();
            groupBox95.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)boatDecayMultiplierNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox95
            // 
            groupBox95.Controls.Add(canDamageAttachedBikeWheelsCB);
            groupBox95.Controls.Add(canDetachAttachedBikeWheelsCB);
            groupBox95.Controls.Add(canDamageAttachedCarWheelsCB);
            groupBox95.Controls.Add(canDetachAttachedCarWheelsCB);
            groupBox95.Controls.Add(label4);
            groupBox95.Controls.Add(label3);
            groupBox95.Controls.Add(label2);
            groupBox95.Controls.Add(label1);
            groupBox95.Controls.Add(boatDecayMultiplierNUD);
            groupBox95.Controls.Add(label198);
            groupBox95.ForeColor = SystemColors.Control;
            groupBox95.Location = new Point(0, 0);
            groupBox95.Margin = new Padding(4, 3, 4, 3);
            groupBox95.Name = "groupBox95";
            groupBox95.Padding = new Padding(4, 3, 4, 3);
            groupBox95.Size = new Size(414, 178);
            groupBox95.TabIndex = 88;
            groupBox95.TabStop = false;
            groupBox95.Text = "Vehicle Data";
            // 
            // canDamageAttachedBikeWheelsCB
            // 
            canDamageAttachedBikeWheelsCB.AutoSize = true;
            canDamageAttachedBikeWheelsCB.Location = new Point(271, 146);
            canDamageAttachedBikeWheelsCB.Name = "canDamageAttachedBikeWheelsCB";
            canDamageAttachedBikeWheelsCB.Size = new Size(15, 14);
            canDamageAttachedBikeWheelsCB.TabIndex = 78;
            canDamageAttachedBikeWheelsCB.UseVisualStyleBackColor = true;
            canDamageAttachedBikeWheelsCB.CheckedChanged += canDamageAttachedBikeWheelsCB_CheckedChanged;
            // 
            // canDetachAttachedBikeWheelsCB
            // 
            canDetachAttachedBikeWheelsCB.AutoSize = true;
            canDetachAttachedBikeWheelsCB.Location = new Point(271, 116);
            canDetachAttachedBikeWheelsCB.Name = "canDetachAttachedBikeWheelsCB";
            canDetachAttachedBikeWheelsCB.Size = new Size(15, 14);
            canDetachAttachedBikeWheelsCB.TabIndex = 77;
            canDetachAttachedBikeWheelsCB.UseVisualStyleBackColor = true;
            canDetachAttachedBikeWheelsCB.CheckedChanged += canDetachAttachedBikeWheelsCB_CheckedChanged;
            // 
            // canDamageAttachedCarWheelsCB
            // 
            canDamageAttachedCarWheelsCB.AutoSize = true;
            canDamageAttachedCarWheelsCB.Location = new Point(271, 86);
            canDamageAttachedCarWheelsCB.Name = "canDamageAttachedCarWheelsCB";
            canDamageAttachedCarWheelsCB.Size = new Size(15, 14);
            canDamageAttachedCarWheelsCB.TabIndex = 76;
            canDamageAttachedCarWheelsCB.UseVisualStyleBackColor = true;
            canDamageAttachedCarWheelsCB.CheckedChanged += canDamageAttachedCarWheelsCB_CheckedChanged;
            // 
            // canDetachAttachedCarWheelsCB
            // 
            canDetachAttachedCarWheelsCB.AutoSize = true;
            canDetachAttachedCarWheelsCB.Location = new Point(271, 55);
            canDetachAttachedCarWheelsCB.Name = "canDetachAttachedCarWheelsCB";
            canDetachAttachedCarWheelsCB.Size = new Size(15, 14);
            canDetachAttachedCarWheelsCB.TabIndex = 75;
            canDetachAttachedCarWheelsCB.UseVisualStyleBackColor = true;
            canDetachAttachedCarWheelsCB.CheckedChanged += canDetachAttachedCarWheelsCB_CheckedChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = SystemColors.Control;
            label4.Location = new Point(14, 145);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(192, 15);
            label4.TabIndex = 74;
            label4.Text = "Can Damage Attached Bike Wheels";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = SystemColors.Control;
            label3.Location = new Point(14, 115);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(185, 15);
            label3.TabIndex = 73;
            label3.Text = "Can Detach Attached Bike Wheels";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.Control;
            label2.Location = new Point(14, 85);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(188, 15);
            label2.TabIndex = 72;
            label2.Text = "Can Damage Attached Car Wheels";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(14, 55);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(181, 15);
            label1.TabIndex = 71;
            label1.Text = "Can Detach Attached Car Wheels";
            // 
            // boatDecayMultiplierNUD
            // 
            boatDecayMultiplierNUD.BackColor = Color.FromArgb(60, 63, 65);
            boatDecayMultiplierNUD.DecimalPlaces = 2;
            boatDecayMultiplierNUD.ForeColor = SystemColors.Control;
            boatDecayMultiplierNUD.Location = new Point(271, 23);
            boatDecayMultiplierNUD.Margin = new Padding(4, 3, 4, 3);
            boatDecayMultiplierNUD.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            boatDecayMultiplierNUD.Name = "boatDecayMultiplierNUD";
            boatDecayMultiplierNUD.Size = new Size(113, 23);
            boatDecayMultiplierNUD.TabIndex = 70;
            boatDecayMultiplierNUD.TextAlign = HorizontalAlignment.Center;
            boatDecayMultiplierNUD.ValueChanged += boatDecayMultiplierNUD_ValueChanged;
            // 
            // label198
            // 
            label198.AutoSize = true;
            label198.ForeColor = SystemColors.Control;
            label198.Location = new Point(14, 25);
            label198.Margin = new Padding(4, 0, 4, 0);
            label198.Name = "label198";
            label198.Size = new Size(120, 15);
            label198.TabIndex = 69;
            label198.Text = "boat Decay Multiplier";
            // 
            // cfggameplayVehicleDataControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox95);
            ForeColor = SystemColors.Control;
            Name = "cfggameplayVehicleDataControl";
            Size = new Size(414, 178);
            groupBox95.ResumeLayout(false);
            groupBox95.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)boatDecayMultiplierNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox95;
        private NumericUpDown boatDecayMultiplierNUD;
        private Label label198;
        private Label label3;
        private Label label2;
        private Label label1;
        private CheckBox canDetachAttachedCarWheelsCB;
        private Label label4;
        private CheckBox canDamageAttachedBikeWheelsCB;
        private CheckBox canDetachAttachedBikeWheelsCB;
        private CheckBox canDamageAttachedCarWheelsCB;
    }
}
