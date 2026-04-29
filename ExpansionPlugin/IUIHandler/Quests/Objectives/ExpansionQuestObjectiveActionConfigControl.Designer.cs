namespace ExpansionPlugin
{
    partial class ExpansionQuestObjectiveActionConfigControl
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
            ObjectivesTresureHuntGB = new GroupBox();
            darkLabel136 = new Label();
            ExecutionAmountNUD = new NumericUpDown();
            ObjectivesTresureHuntGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ExecutionAmountNUD).BeginInit();
            SuspendLayout();
            // 
            // ObjectivesTresureHuntGB
            // 
            ObjectivesTresureHuntGB.Controls.Add(darkLabel136);
            ObjectivesTresureHuntGB.Controls.Add(ExecutionAmountNUD);
            ObjectivesTresureHuntGB.ForeColor = SystemColors.Control;
            ObjectivesTresureHuntGB.Location = new Point(0, 0);
            ObjectivesTresureHuntGB.Margin = new Padding(4, 3, 4, 3);
            ObjectivesTresureHuntGB.Name = "ObjectivesTresureHuntGB";
            ObjectivesTresureHuntGB.Padding = new Padding(4, 3, 4, 3);
            ObjectivesTresureHuntGB.Size = new Size(314, 69);
            ObjectivesTresureHuntGB.TabIndex = 239;
            ObjectivesTresureHuntGB.TabStop = false;
            ObjectivesTresureHuntGB.Text = "Action";
            // 
            // darkLabel136
            // 
            darkLabel136.AutoSize = true;
            darkLabel136.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel136.Location = new Point(15, 25);
            darkLabel136.Margin = new Padding(4, 0, 4, 0);
            darkLabel136.Name = "darkLabel136";
            darkLabel136.Size = new Size(105, 15);
            darkLabel136.TabIndex = 325;
            darkLabel136.Text = "Execution Amount";
            // 
            // ExecutionAmountNUD
            // 
            ExecutionAmountNUD.BackColor = Color.FromArgb(60, 63, 65);
            ExecutionAmountNUD.ForeColor = SystemColors.Control;
            ExecutionAmountNUD.Location = new Point(159, 23);
            ExecutionAmountNUD.Margin = new Padding(4, 3, 4, 3);
            ExecutionAmountNUD.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            ExecutionAmountNUD.Name = "ExecutionAmountNUD";
            ExecutionAmountNUD.Size = new Size(124, 23);
            ExecutionAmountNUD.TabIndex = 324;
            ExecutionAmountNUD.TextAlign = HorizontalAlignment.Center;
            ExecutionAmountNUD.ValueChanged += ExecutionAmountNUD_ValueChanged;
            // 
            // ExpansionQuestObjectiveActionConfigControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(ObjectivesTresureHuntGB);
            ForeColor = SystemColors.Control;
            Name = "ExpansionQuestObjectiveActionConfigControl";
            Size = new Size(314, 69);
            ObjectivesTresureHuntGB.ResumeLayout(false);
            ObjectivesTresureHuntGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ExecutionAmountNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox ObjectivesTresureHuntGB;
        private Label darkLabel136;
        private NumericUpDown ExecutionAmountNUD;
    }
}
