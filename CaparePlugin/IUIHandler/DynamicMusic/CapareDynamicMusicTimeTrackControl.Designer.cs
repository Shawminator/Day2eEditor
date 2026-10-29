namespace CaparePlugin
{
    partial class CapareDynamicMusicTimeTrackControl
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
            m_SoundSetL = new Label();
            m_SoundSetTB = new TextBox();
            m_TimeOfDayL = new Label();
            m_TimeOfDayComboBox = new ComboBox();
            groupBox1.SuspendLayout();

            SuspendLayout();
            //
            // groupBox1
            //
            groupBox1.Controls.Add(m_SoundSetL);
            groupBox1.Controls.Add(m_SoundSetTB);
            groupBox1.Controls.Add(m_TimeOfDayL);
            groupBox1.Controls.Add(m_TimeOfDayComboBox);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(435, 86);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Time Of Day Track";
            //
            // m_SoundSetL
            //
            m_SoundSetL.AutoSize = true;
            m_SoundSetL.ForeColor = Color.FromArgb(220, 220, 220);
            m_SoundSetL.Location = new Point(11, 25);
            m_SoundSetL.Margin = new Padding(4, 0, 4, 0);
            m_SoundSetL.Name = "m_SoundSetL";
            m_SoundSetL.Size = new Size(63, 15);
            m_SoundSetL.TabIndex = 0;
            m_SoundSetL.Text = "Sound Set";
            //
            // m_SoundSetTB
            //
            m_SoundSetTB.BackColor = Color.FromArgb(60, 63, 65);
            m_SoundSetTB.ForeColor = SystemColors.Control;
            m_SoundSetTB.Location = new Point(190, 22);
            m_SoundSetTB.Margin = new Padding(4, 3, 4, 3);
            m_SoundSetTB.Name = "m_SoundSetTB";
            m_SoundSetTB.Size = new Size(230, 23);
            m_SoundSetTB.TabIndex = 1;
            m_SoundSetTB.TextChanged += m_SoundSetTB_TextChanged;
            //
            // m_TimeOfDayL
            //
            m_TimeOfDayL.AutoSize = true;
            m_TimeOfDayL.ForeColor = Color.FromArgb(220, 220, 220);
            m_TimeOfDayL.Location = new Point(11, 54);
            m_TimeOfDayL.Margin = new Padding(4, 0, 4, 0);
            m_TimeOfDayL.Name = "m_TimeOfDayL";
            m_TimeOfDayL.Size = new Size(77, 15);
            m_TimeOfDayL.TabIndex = 2;
            m_TimeOfDayL.Text = "Time Of Day";
            //
            // m_TimeOfDayComboBox
            //
            m_TimeOfDayComboBox.BackColor = Color.FromArgb(60, 63, 65);
            m_TimeOfDayComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            m_TimeOfDayComboBox.FlatStyle = FlatStyle.Flat;
            m_TimeOfDayComboBox.ForeColor = SystemColors.Control;
            m_TimeOfDayComboBox.FormattingEnabled = true;
            m_TimeOfDayComboBox.Location = new Point(190, 51);
            m_TimeOfDayComboBox.Margin = new Padding(4, 3, 4, 3);
            m_TimeOfDayComboBox.Name = "m_TimeOfDayComboBox";
            m_TimeOfDayComboBox.Size = new Size(230, 23);
            m_TimeOfDayComboBox.TabIndex = 3;
            m_TimeOfDayComboBox.SelectedIndexChanged += m_TimeOfDayComboBox_SelectedIndexChanged;
            //
            // CapareDynamicMusicTimeTrackControl
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "CapareDynamicMusicTimeTrackControl";
            Size = new Size(435, 92);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();

            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label m_SoundSetL;
        private TextBox m_SoundSetTB;
        private Label m_TimeOfDayL;
        private ComboBox m_TimeOfDayComboBox;
    }
}
