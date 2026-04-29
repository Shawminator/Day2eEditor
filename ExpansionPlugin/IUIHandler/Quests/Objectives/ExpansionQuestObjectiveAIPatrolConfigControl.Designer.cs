namespace ExpansionPlugin
{
    partial class ExpansionQuestObjectiveAIPatrolConfigControl
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
            darkLabel64 = new Label();
            darkLabel65 = new Label();
            ObjectivesAIPatrolMaxDistanceNUD = new NumericUpDown();
            ObjectivesAIPatrolMinDistanceNUD = new NumericUpDown();
            ObjectivesTresureHuntGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ObjectivesAIPatrolMaxDistanceNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ObjectivesAIPatrolMinDistanceNUD).BeginInit();
            SuspendLayout();
            // 
            // ObjectivesTresureHuntGB
            // 
            ObjectivesTresureHuntGB.Controls.Add(darkLabel64);
            ObjectivesTresureHuntGB.Controls.Add(darkLabel65);
            ObjectivesTresureHuntGB.Controls.Add(ObjectivesAIPatrolMaxDistanceNUD);
            ObjectivesTresureHuntGB.Controls.Add(ObjectivesAIPatrolMinDistanceNUD);
            ObjectivesTresureHuntGB.ForeColor = SystemColors.Control;
            ObjectivesTresureHuntGB.Location = new Point(0, 0);
            ObjectivesTresureHuntGB.Margin = new Padding(4, 3, 4, 3);
            ObjectivesTresureHuntGB.Name = "ObjectivesTresureHuntGB";
            ObjectivesTresureHuntGB.Padding = new Padding(4, 3, 4, 3);
            ObjectivesTresureHuntGB.Size = new Size(318, 87);
            ObjectivesTresureHuntGB.TabIndex = 241;
            ObjectivesTresureHuntGB.TabStop = false;
            ObjectivesTresureHuntGB.Text = "Action";
            // 
            // darkLabel64
            // 
            darkLabel64.AutoSize = true;
            darkLabel64.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel64.Location = new Point(16, 55);
            darkLabel64.Margin = new Padding(4, 0, 4, 0);
            darkLabel64.Name = "darkLabel64";
            darkLabel64.Size = new Size(77, 15);
            darkLabel64.TabIndex = 277;
            darkLabel64.Text = "Max Distance";
            // 
            // darkLabel65
            // 
            darkLabel65.AutoSize = true;
            darkLabel65.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel65.Location = new Point(16, 25);
            darkLabel65.Margin = new Padding(4, 0, 4, 0);
            darkLabel65.Name = "darkLabel65";
            darkLabel65.Size = new Size(76, 15);
            darkLabel65.TabIndex = 276;
            darkLabel65.Text = "Min Distance";
            // 
            // ObjectivesAIPatrolMaxDistanceNUD
            // 
            ObjectivesAIPatrolMaxDistanceNUD.BackColor = Color.FromArgb(60, 63, 65);
            ObjectivesAIPatrolMaxDistanceNUD.DecimalPlaces = 2;
            ObjectivesAIPatrolMaxDistanceNUD.ForeColor = SystemColors.Control;
            ObjectivesAIPatrolMaxDistanceNUD.Location = new Point(174, 23);
            ObjectivesAIPatrolMaxDistanceNUD.Margin = new Padding(4, 3, 4, 3);
            ObjectivesAIPatrolMaxDistanceNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            ObjectivesAIPatrolMaxDistanceNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            ObjectivesAIPatrolMaxDistanceNUD.Name = "ObjectivesAIPatrolMaxDistanceNUD";
            ObjectivesAIPatrolMaxDistanceNUD.Size = new Size(124, 23);
            ObjectivesAIPatrolMaxDistanceNUD.TabIndex = 274;
            ObjectivesAIPatrolMaxDistanceNUD.TextAlign = HorizontalAlignment.Center;
            ObjectivesAIPatrolMaxDistanceNUD.ValueChanged += ObjectivesAIPatrolMaxDistanceNUD_ValueChanged;
            // 
            // ObjectivesAIPatrolMinDistanceNUD
            // 
            ObjectivesAIPatrolMinDistanceNUD.BackColor = Color.FromArgb(60, 63, 65);
            ObjectivesAIPatrolMinDistanceNUD.DecimalPlaces = 2;
            ObjectivesAIPatrolMinDistanceNUD.ForeColor = SystemColors.Control;
            ObjectivesAIPatrolMinDistanceNUD.Location = new Point(174, 53);
            ObjectivesAIPatrolMinDistanceNUD.Margin = new Padding(4, 3, 4, 3);
            ObjectivesAIPatrolMinDistanceNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            ObjectivesAIPatrolMinDistanceNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            ObjectivesAIPatrolMinDistanceNUD.Name = "ObjectivesAIPatrolMinDistanceNUD";
            ObjectivesAIPatrolMinDistanceNUD.Size = new Size(124, 23);
            ObjectivesAIPatrolMinDistanceNUD.TabIndex = 275;
            ObjectivesAIPatrolMinDistanceNUD.TextAlign = HorizontalAlignment.Center;
            ObjectivesAIPatrolMinDistanceNUD.ValueChanged += ObjectivesAIPatrolMinDistanceNUD_ValueChanged;
            // 
            // ExpansionQuestObjectiveAIPatrolConfigControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(ObjectivesTresureHuntGB);
            ForeColor = SystemColors.Control;
            Name = "ExpansionQuestObjectiveAIPatrolConfigControl";
            Size = new Size(318, 87);
            ObjectivesTresureHuntGB.ResumeLayout(false);
            ObjectivesTresureHuntGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ObjectivesAIPatrolMaxDistanceNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)ObjectivesAIPatrolMinDistanceNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox ObjectivesTresureHuntGB;
        private Label darkLabel64;
        private Label darkLabel65;
        private NumericUpDown ObjectivesAIPatrolMaxDistanceNUD;
        private NumericUpDown ObjectivesAIPatrolMinDistanceNUD;
    }
}
