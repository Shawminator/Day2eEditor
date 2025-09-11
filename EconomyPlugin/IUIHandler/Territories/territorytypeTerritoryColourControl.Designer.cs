namespace EconomyPlugin
{
    partial class territorytypeTerritoryColourControl
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
            m_Color = new PictureBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_Color).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(m_Color);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(332, 74);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Territory Type Colour";
            // 
            // m_Color
            // 
            m_Color.Location = new Point(7, 22);
            m_Color.Margin = new Padding(4, 3, 4, 3);
            m_Color.Name = "m_Color";
            m_Color.Size = new Size(311, 37);
            m_Color.TabIndex = 119;
            m_Color.TabStop = false;
            m_Color.Click += m_Color_Click;
            // 
            // territorytypeTerritoryColourControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "territorytypeTerritoryColourControl";
            Size = new Size(342, 86);
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)m_Color).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private PictureBox m_Color;
    }
}
