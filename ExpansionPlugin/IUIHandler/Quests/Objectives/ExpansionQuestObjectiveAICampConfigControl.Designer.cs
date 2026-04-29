namespace ExpansionPlugin
{
    partial class ExpansionQuestObjectiveAICampConfigControl
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
            darkLabel100 = new Label();
            QuestObjectovesInfectedDeletionRadiusNUD = new NumericUpDown();
            darkLabel64 = new Label();
            darkLabel65 = new Label();
            ObjectivesAICampMinDistanceNUD = new NumericUpDown();
            ObjectivesAICampMaxDistanceNUD = new NumericUpDown();
            ObjectivesTresureHuntGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)QuestObjectovesInfectedDeletionRadiusNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ObjectivesAICampMinDistanceNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ObjectivesAICampMaxDistanceNUD).BeginInit();
            SuspendLayout();
            // 
            // ObjectivesTresureHuntGB
            // 
            ObjectivesTresureHuntGB.Controls.Add(darkLabel100);
            ObjectivesTresureHuntGB.Controls.Add(QuestObjectovesInfectedDeletionRadiusNUD);
            ObjectivesTresureHuntGB.Controls.Add(darkLabel64);
            ObjectivesTresureHuntGB.Controls.Add(darkLabel65);
            ObjectivesTresureHuntGB.Controls.Add(ObjectivesAICampMinDistanceNUD);
            ObjectivesTresureHuntGB.Controls.Add(ObjectivesAICampMaxDistanceNUD);
            ObjectivesTresureHuntGB.ForeColor = SystemColors.Control;
            ObjectivesTresureHuntGB.Location = new Point(0, 0);
            ObjectivesTresureHuntGB.Margin = new Padding(4, 3, 4, 3);
            ObjectivesTresureHuntGB.Name = "ObjectivesTresureHuntGB";
            ObjectivesTresureHuntGB.Padding = new Padding(4, 3, 4, 3);
            ObjectivesTresureHuntGB.Size = new Size(318, 118);
            ObjectivesTresureHuntGB.TabIndex = 240;
            ObjectivesTresureHuntGB.TabStop = false;
            ObjectivesTresureHuntGB.Text = "Action";
            // 
            // darkLabel100
            // 
            darkLabel100.AutoSize = true;
            darkLabel100.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel100.Location = new Point(15, 25);
            darkLabel100.Margin = new Padding(4, 0, 4, 0);
            darkLabel100.Name = "darkLabel100";
            darkLabel100.Size = new Size(135, 15);
            darkLabel100.TabIndex = 279;
            darkLabel100.Text = "Infected Deletion Radius";
            // 
            // QuestObjectovesInfectedDeletionRadiusNUD
            // 
            QuestObjectovesInfectedDeletionRadiusNUD.BackColor = Color.FromArgb(60, 63, 65);
            QuestObjectovesInfectedDeletionRadiusNUD.ForeColor = SystemColors.Control;
            QuestObjectovesInfectedDeletionRadiusNUD.Location = new Point(174, 23);
            QuestObjectovesInfectedDeletionRadiusNUD.Margin = new Padding(4, 3, 4, 3);
            QuestObjectovesInfectedDeletionRadiusNUD.Maximum = new decimal(new int[] { 30000, 0, 0, 0 });
            QuestObjectovesInfectedDeletionRadiusNUD.Name = "QuestObjectovesInfectedDeletionRadiusNUD";
            QuestObjectovesInfectedDeletionRadiusNUD.Size = new Size(124, 23);
            QuestObjectovesInfectedDeletionRadiusNUD.TabIndex = 278;
            QuestObjectovesInfectedDeletionRadiusNUD.TextAlign = HorizontalAlignment.Center;
            QuestObjectovesInfectedDeletionRadiusNUD.ValueChanged += QuestObjectovesInfectedDeletionRadiusNUD_ValueChanged;
            // 
            // darkLabel64
            // 
            darkLabel64.AutoSize = true;
            darkLabel64.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel64.Location = new Point(15, 85);
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
            darkLabel65.Location = new Point(15, 55);
            darkLabel65.Margin = new Padding(4, 0, 4, 0);
            darkLabel65.Name = "darkLabel65";
            darkLabel65.Size = new Size(76, 15);
            darkLabel65.TabIndex = 276;
            darkLabel65.Text = "Min Distance";
            // 
            // ObjectivesAICampMinDistanceNUD
            // 
            ObjectivesAICampMinDistanceNUD.BackColor = Color.FromArgb(60, 63, 65);
            ObjectivesAICampMinDistanceNUD.DecimalPlaces = 2;
            ObjectivesAICampMinDistanceNUD.ForeColor = SystemColors.Control;
            ObjectivesAICampMinDistanceNUD.Location = new Point(174, 53);
            ObjectivesAICampMinDistanceNUD.Margin = new Padding(4, 3, 4, 3);
            ObjectivesAICampMinDistanceNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            ObjectivesAICampMinDistanceNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            ObjectivesAICampMinDistanceNUD.Name = "ObjectivesAICampMinDistanceNUD";
            ObjectivesAICampMinDistanceNUD.Size = new Size(124, 23);
            ObjectivesAICampMinDistanceNUD.TabIndex = 274;
            ObjectivesAICampMinDistanceNUD.TextAlign = HorizontalAlignment.Center;
            ObjectivesAICampMinDistanceNUD.ValueChanged += ObjectivesAICampMinDistanceNUD_ValueChanged;
            // 
            // ObjectivesAICampMaxDistanceNUD
            // 
            ObjectivesAICampMaxDistanceNUD.BackColor = Color.FromArgb(60, 63, 65);
            ObjectivesAICampMaxDistanceNUD.DecimalPlaces = 2;
            ObjectivesAICampMaxDistanceNUD.ForeColor = SystemColors.Control;
            ObjectivesAICampMaxDistanceNUD.Location = new Point(174, 83);
            ObjectivesAICampMaxDistanceNUD.Margin = new Padding(4, 3, 4, 3);
            ObjectivesAICampMaxDistanceNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            ObjectivesAICampMaxDistanceNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            ObjectivesAICampMaxDistanceNUD.Name = "ObjectivesAICampMaxDistanceNUD";
            ObjectivesAICampMaxDistanceNUD.Size = new Size(124, 23);
            ObjectivesAICampMaxDistanceNUD.TabIndex = 275;
            ObjectivesAICampMaxDistanceNUD.TextAlign = HorizontalAlignment.Center;
            ObjectivesAICampMaxDistanceNUD.ValueChanged += ObjectivesAICampMaxDistanceNUD_ValueChanged;
            // 
            // ExpansionQuestObjectiveAICampConfigControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(ObjectivesTresureHuntGB);
            ForeColor = SystemColors.Control;
            Name = "ExpansionQuestObjectiveAICampConfigControl";
            Size = new Size(318, 118);
            ObjectivesTresureHuntGB.ResumeLayout(false);
            ObjectivesTresureHuntGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)QuestObjectovesInfectedDeletionRadiusNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)ObjectivesAICampMinDistanceNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)ObjectivesAICampMaxDistanceNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox ObjectivesTresureHuntGB;
        private Label darkLabel100;
        private NumericUpDown QuestObjectovesInfectedDeletionRadiusNUD;
        private Label darkLabel64;
        private Label darkLabel65;
        private NumericUpDown ObjectivesAICampMinDistanceNUD;
        private NumericUpDown ObjectivesAICampMaxDistanceNUD;
    }
}
