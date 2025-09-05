namespace EconomyPlugin
{
    partial class eventspawnZoneControl
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
            EventspawnZoneGB = new GroupBox();
            label126 = new Label();
            eventzonedNUD = new NumericUpDown();
            label122 = new Label();
            label125 = new Label();
            eventzonesminNUD = new NumericUpDown();
            label123 = new Label();
            label124 = new Label();
            eventzonedmaxNUD = new NumericUpDown();
            eventzonesmaxNUD = new NumericUpDown();
            eventzonedminNUD = new NumericUpDown();
            EventspawnZoneGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)eventzonedNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)eventzonesminNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)eventzonedmaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)eventzonesmaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)eventzonedminNUD).BeginInit();
            SuspendLayout();
            // 
            // EventspawnZoneGB
            // 
            EventspawnZoneGB.Controls.Add(label126);
            EventspawnZoneGB.Controls.Add(eventzonedNUD);
            EventspawnZoneGB.Controls.Add(label122);
            EventspawnZoneGB.Controls.Add(label125);
            EventspawnZoneGB.Controls.Add(eventzonesminNUD);
            EventspawnZoneGB.Controls.Add(label123);
            EventspawnZoneGB.Controls.Add(label124);
            EventspawnZoneGB.Controls.Add(eventzonedmaxNUD);
            EventspawnZoneGB.Controls.Add(eventzonesmaxNUD);
            EventspawnZoneGB.Controls.Add(eventzonedminNUD);
            EventspawnZoneGB.ForeColor = SystemColors.Control;
            EventspawnZoneGB.Location = new Point(0, 0);
            EventspawnZoneGB.Margin = new Padding(4, 3, 4, 3);
            EventspawnZoneGB.Name = "EventspawnZoneGB";
            EventspawnZoneGB.Padding = new Padding(4, 3, 4, 3);
            EventspawnZoneGB.Size = new Size(190, 175);
            EventspawnZoneGB.TabIndex = 6;
            EventspawnZoneGB.TabStop = false;
            EventspawnZoneGB.Text = "Zone";
            // 
            // label126
            // 
            label126.AutoSize = true;
            label126.ForeColor = SystemColors.Control;
            label126.Location = new Point(8, 144);
            label126.Margin = new Padding(4, 0, 4, 0);
            label126.Name = "label126";
            label126.Size = new Size(11, 15);
            label126.TabIndex = 8;
            label126.Text = "r";
            // 
            // eventzonedNUD
            // 
            eventzonedNUD.BackColor = Color.FromArgb(60, 63, 65);
            eventzonedNUD.ForeColor = SystemColors.Control;
            eventzonedNUD.Location = new Point(62, 142);
            eventzonedNUD.Margin = new Padding(4, 3, 4, 3);
            eventzonedNUD.Maximum = new decimal(new int[] { 30000, 0, 0, 0 });
            eventzonedNUD.Minimum = new decimal(new int[] { 30000, 0, 0, int.MinValue });
            eventzonedNUD.Name = "eventzonedNUD";
            eventzonedNUD.Size = new Size(115, 23);
            eventzonedNUD.TabIndex = 9;
            eventzonedNUD.TextAlign = HorizontalAlignment.Center;
            eventzonedNUD.ValueChanged += eventzonedNUD_ValueChanged;
            // 
            // label122
            // 
            label122.AutoSize = true;
            label122.ForeColor = SystemColors.Control;
            label122.Location = new Point(8, 114);
            label122.Margin = new Padding(4, 0, 4, 0);
            label122.Name = "label122";
            label122.Size = new Size(36, 15);
            label122.TabIndex = 6;
            label122.Text = "dmax";
            // 
            // label125
            // 
            label125.AutoSize = true;
            label125.ForeColor = SystemColors.Control;
            label125.Location = new Point(8, 24);
            label125.Margin = new Padding(4, 0, 4, 0);
            label125.Name = "label125";
            label125.Size = new Size(33, 15);
            label125.TabIndex = 0;
            label125.Text = "smin";
            // 
            // eventzonesminNUD
            // 
            eventzonesminNUD.BackColor = Color.FromArgb(60, 63, 65);
            eventzonesminNUD.ForeColor = SystemColors.Control;
            eventzonesminNUD.Location = new Point(62, 22);
            eventzonesminNUD.Margin = new Padding(4, 3, 4, 3);
            eventzonesminNUD.Maximum = new decimal(new int[] { 30000, 0, 0, 0 });
            eventzonesminNUD.Minimum = new decimal(new int[] { 30000, 0, 0, int.MinValue });
            eventzonesminNUD.Name = "eventzonesminNUD";
            eventzonesminNUD.Size = new Size(115, 23);
            eventzonesminNUD.TabIndex = 1;
            eventzonesminNUD.TextAlign = HorizontalAlignment.Center;
            eventzonesminNUD.ValueChanged += eventzonesminNUD_ValueChanged;
            // 
            // label123
            // 
            label123.AutoSize = true;
            label123.ForeColor = SystemColors.Control;
            label123.Location = new Point(8, 84);
            label123.Margin = new Padding(4, 0, 4, 0);
            label123.Name = "label123";
            label123.Size = new Size(35, 15);
            label123.TabIndex = 4;
            label123.Text = "dmin";
            // 
            // label124
            // 
            label124.AutoSize = true;
            label124.ForeColor = SystemColors.Control;
            label124.Location = new Point(8, 54);
            label124.Margin = new Padding(4, 0, 4, 0);
            label124.Name = "label124";
            label124.Size = new Size(34, 15);
            label124.TabIndex = 2;
            label124.Text = "smax";
            // 
            // eventzonedmaxNUD
            // 
            eventzonedmaxNUD.BackColor = Color.FromArgb(60, 63, 65);
            eventzonedmaxNUD.ForeColor = SystemColors.Control;
            eventzonedmaxNUD.Location = new Point(62, 112);
            eventzonedmaxNUD.Margin = new Padding(4, 3, 4, 3);
            eventzonedmaxNUD.Maximum = new decimal(new int[] { 30000, 0, 0, 0 });
            eventzonedmaxNUD.Minimum = new decimal(new int[] { 30000, 0, 0, int.MinValue });
            eventzonedmaxNUD.Name = "eventzonedmaxNUD";
            eventzonedmaxNUD.Size = new Size(115, 23);
            eventzonedmaxNUD.TabIndex = 7;
            eventzonedmaxNUD.TextAlign = HorizontalAlignment.Center;
            eventzonedmaxNUD.ValueChanged += eventzonedmaxNUD_ValueChanged;
            // 
            // eventzonesmaxNUD
            // 
            eventzonesmaxNUD.BackColor = Color.FromArgb(60, 63, 65);
            eventzonesmaxNUD.ForeColor = SystemColors.Control;
            eventzonesmaxNUD.Location = new Point(62, 52);
            eventzonesmaxNUD.Margin = new Padding(4, 3, 4, 3);
            eventzonesmaxNUD.Maximum = new decimal(new int[] { 30000, 0, 0, 0 });
            eventzonesmaxNUD.Minimum = new decimal(new int[] { 30000, 0, 0, int.MinValue });
            eventzonesmaxNUD.Name = "eventzonesmaxNUD";
            eventzonesmaxNUD.Size = new Size(115, 23);
            eventzonesmaxNUD.TabIndex = 3;
            eventzonesmaxNUD.TextAlign = HorizontalAlignment.Center;
            eventzonesmaxNUD.ValueChanged += eventzonesmaxNUD_ValueChanged;
            // 
            // eventzonedminNUD
            // 
            eventzonedminNUD.BackColor = Color.FromArgb(60, 63, 65);
            eventzonedminNUD.ForeColor = SystemColors.Control;
            eventzonedminNUD.Location = new Point(62, 82);
            eventzonedminNUD.Margin = new Padding(4, 3, 4, 3);
            eventzonedminNUD.Maximum = new decimal(new int[] { 30000, 0, 0, 0 });
            eventzonedminNUD.Minimum = new decimal(new int[] { 30000, 0, 0, int.MinValue });
            eventzonedminNUD.Name = "eventzonedminNUD";
            eventzonedminNUD.Size = new Size(115, 23);
            eventzonedminNUD.TabIndex = 5;
            eventzonedminNUD.TextAlign = HorizontalAlignment.Center;
            eventzonedminNUD.ValueChanged += eventzonedminNUD_ValueChanged;
            // 
            // eventspawnZoneControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(EventspawnZoneGB);
            ForeColor = SystemColors.Control;
            Name = "eventspawnZoneControl";
            Size = new Size(206, 187);
            EventspawnZoneGB.ResumeLayout(false);
            EventspawnZoneGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)eventzonedNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)eventzonesminNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)eventzonedmaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)eventzonesmaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)eventzonedminNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox EventspawnZoneGB;
        private Label label126;
        private NumericUpDown eventzonedNUD;
        private Label label122;
        private Label label125;
        private NumericUpDown eventzonesminNUD;
        private Label label123;
        private Label label124;
        private NumericUpDown eventzonedmaxNUD;
        private NumericUpDown eventzonesmaxNUD;
        private NumericUpDown eventzonedminNUD;
    }
}
