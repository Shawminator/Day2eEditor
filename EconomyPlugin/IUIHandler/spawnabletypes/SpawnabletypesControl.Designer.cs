namespace EconomyPlugin
{
    partial class SpawnabletypesControl
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
            SpawnableTypeGB = new GroupBox();
            flowLayoutPanel6 = new FlowLayoutPanel();
            SpawnableTypeTB = new TextBox();
            darkButton30 = new Button();
            SpawnableTypeGB.SuspendLayout();
            flowLayoutPanel6.SuspendLayout();
            SuspendLayout();
            // 
            // SpawnableTypeGB
            // 
            SpawnableTypeGB.Controls.Add(flowLayoutPanel6);
            SpawnableTypeGB.ForeColor = SystemColors.ButtonHighlight;
            SpawnableTypeGB.Location = new Point(0, 0);
            SpawnableTypeGB.Margin = new Padding(4, 3, 4, 3);
            SpawnableTypeGB.Name = "SpawnableTypeGB";
            SpawnableTypeGB.Padding = new Padding(4, 3, 4, 3);
            SpawnableTypeGB.Size = new Size(282, 104);
            SpawnableTypeGB.TabIndex = 86;
            SpawnableTypeGB.TabStop = false;
            SpawnableTypeGB.Text = "Spawnable Type";
            // 
            // flowLayoutPanel6
            // 
            flowLayoutPanel6.Controls.Add(SpawnableTypeTB);
            flowLayoutPanel6.Controls.Add(darkButton30);
            flowLayoutPanel6.Dock = DockStyle.Fill;
            flowLayoutPanel6.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel6.Location = new Point(4, 19);
            flowLayoutPanel6.Margin = new Padding(4, 3, 4, 3);
            flowLayoutPanel6.Name = "flowLayoutPanel6";
            flowLayoutPanel6.Size = new Size(274, 82);
            flowLayoutPanel6.TabIndex = 84;
            // 
            // SpawnableTypeTB
            // 
            SpawnableTypeTB.BackColor = Color.FromArgb(60, 63, 65);
            SpawnableTypeTB.ForeColor = SystemColors.Control;
            SpawnableTypeTB.Location = new Point(4, 3);
            SpawnableTypeTB.Margin = new Padding(4, 3, 4, 3);
            SpawnableTypeTB.Name = "SpawnableTypeTB";
            SpawnableTypeTB.ReadOnly = true;
            SpawnableTypeTB.Size = new Size(254, 23);
            SpawnableTypeTB.TabIndex = 81;
            // 
            // darkButton30
            // 
            darkButton30.FlatStyle = FlatStyle.Flat;
            darkButton30.Location = new Point(4, 32);
            darkButton30.Margin = new Padding(4, 3, 4, 3);
            darkButton30.Name = "darkButton30";
            darkButton30.Size = new Size(254, 31);
            darkButton30.TabIndex = 5;
            darkButton30.Text = "Change Item";
            darkButton30.Click += darkButton30_Click;
            // 
            // SpawnabletypesControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(SpawnableTypeGB);
            ForeColor = SystemColors.Control;
            Name = "SpawnabletypesControl";
            Size = new Size(293, 116);
            SpawnableTypeGB.ResumeLayout(false);
            flowLayoutPanel6.ResumeLayout(false);
            flowLayoutPanel6.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox SpawnableTypeGB;
        private FlowLayoutPanel flowLayoutPanel6;
        private TextBox SpawnableTypeTB;
        private Button darkButton30;
    }
}
