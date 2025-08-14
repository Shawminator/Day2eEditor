namespace EconomyPlugin
{
    partial class TypesCollectionControl
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
            TypesCollectionGB = new GroupBox();
            if0setto1CB = new CheckBox();
            ChangeMinCheckBox = new CheckBox();
            MultiplierButton = new Button();
            MultiplierCB = new ComboBox();
            ChangeMinCB = new CheckBox();
            SetCustomButton = new Button();
            CollectionCustomNUD = new NumericUpDown();
            SyncMIntoNomButton = new Button();
            SyncNomtoMinButton = new Button();
            UpdateTypesFileButton = new Button();
            ZeroiseButton = new Button();
            CollectionNameTB = new TextBox();
            label1 = new Label();
            TypesCollectionGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)CollectionCustomNUD).BeginInit();
            SuspendLayout();
            // 
            // TypesCollectionGB
            // 
            TypesCollectionGB.Controls.Add(if0setto1CB);
            TypesCollectionGB.Controls.Add(ChangeMinCheckBox);
            TypesCollectionGB.Controls.Add(MultiplierButton);
            TypesCollectionGB.Controls.Add(MultiplierCB);
            TypesCollectionGB.Controls.Add(ChangeMinCB);
            TypesCollectionGB.Controls.Add(SetCustomButton);
            TypesCollectionGB.Controls.Add(CollectionCustomNUD);
            TypesCollectionGB.Controls.Add(SyncMIntoNomButton);
            TypesCollectionGB.Controls.Add(SyncNomtoMinButton);
            TypesCollectionGB.Controls.Add(UpdateTypesFileButton);
            TypesCollectionGB.Controls.Add(ZeroiseButton);
            TypesCollectionGB.Controls.Add(CollectionNameTB);
            TypesCollectionGB.Controls.Add(label1);
            TypesCollectionGB.Dock = DockStyle.Fill;
            TypesCollectionGB.ForeColor = SystemColors.Control;
            TypesCollectionGB.Location = new Point(0, 0);
            TypesCollectionGB.Name = "TypesCollectionGB";
            TypesCollectionGB.Size = new Size(645, 231);
            TypesCollectionGB.TabIndex = 0;
            TypesCollectionGB.TabStop = false;
            TypesCollectionGB.Text = "Types Collection";
            // 
            // if0setto1CB
            // 
            if0setto1CB.AutoSize = true;
            if0setto1CB.Location = new Point(483, 107);
            if0setto1CB.Name = "if0setto1CB";
            if0setto1CB.RightToLeft = RightToLeft.Yes;
            if0setto1CB.Size = new Size(143, 19);
            if0setto1CB.TabIndex = 35;
            if0setto1CB.Text = "If Nominal = 0 set as 1";
            if0setto1CB.UseVisualStyleBackColor = true;
            // 
            // ChangeMinCheckBox
            // 
            ChangeMinCheckBox.AutoSize = true;
            ChangeMinCheckBox.Location = new Point(345, 107);
            ChangeMinCheckBox.Name = "ChangeMinCheckBox";
            ChangeMinCheckBox.RightToLeft = RightToLeft.Yes;
            ChangeMinCheckBox.Size = new Size(123, 19);
            ChangeMinCheckBox.TabIndex = 34;
            ChangeMinCheckBox.Text = "Change Minimum";
            ChangeMinCheckBox.UseVisualStyleBackColor = true;
            // 
            // MultiplierButton
            // 
            MultiplierButton.BackColor = Color.FromArgb(60, 63, 65);
            MultiplierButton.Location = new Point(15, 105);
            MultiplierButton.Name = "MultiplierButton";
            MultiplierButton.Size = new Size(159, 23);
            MultiplierButton.TabIndex = 33;
            MultiplierButton.Text = "Do Multiplier";
            MultiplierButton.UseVisualStyleBackColor = false;
            MultiplierButton.Click += button6_Click;
            // 
            // MultiplierCB
            // 
            MultiplierCB.BackColor = Color.FromArgb(60, 63, 65);
            MultiplierCB.ForeColor = SystemColors.Control;
            MultiplierCB.FormattingEnabled = true;
            MultiplierCB.Items.AddRange(new object[] { "x10", "x9", "x8", "x7", "x6", "x5", "x4", "x3", "x2", "x1.5", "/1.5", "/2", "/3", "/4", "/5", "/6", "/7", "/8", "/9", "/10" });
            MultiplierCB.Location = new Point(180, 105);
            MultiplierCB.Name = "MultiplierCB";
            MultiplierCB.Size = new Size(159, 23);
            MultiplierCB.TabIndex = 32;
            // 
            // ChangeMinCB
            // 
            ChangeMinCB.AutoSize = true;
            ChangeMinCB.Location = new Point(345, 77);
            ChangeMinCB.Name = "ChangeMinCB";
            ChangeMinCB.RightToLeft = RightToLeft.Yes;
            ChangeMinCB.Size = new Size(123, 19);
            ChangeMinCB.TabIndex = 31;
            ChangeMinCB.Text = "Change Minimum";
            ChangeMinCB.UseVisualStyleBackColor = true;
            // 
            // SetCustomButton
            // 
            SetCustomButton.BackColor = Color.FromArgb(60, 63, 65);
            SetCustomButton.Location = new Point(15, 76);
            SetCustomButton.Name = "SetCustomButton";
            SetCustomButton.Size = new Size(159, 23);
            SetCustomButton.TabIndex = 30;
            SetCustomButton.Text = "Set to Custom Value";
            SetCustomButton.UseVisualStyleBackColor = false;
            SetCustomButton.Click += button5_Click;
            // 
            // CollectionCustomNUD
            // 
            CollectionCustomNUD.BackColor = Color.FromArgb(60, 63, 65);
            CollectionCustomNUD.ForeColor = SystemColors.Control;
            CollectionCustomNUD.Location = new Point(180, 76);
            CollectionCustomNUD.Maximum = new decimal(new int[] { 50000, 0, 0, 0 });
            CollectionCustomNUD.Name = "CollectionCustomNUD";
            CollectionCustomNUD.Size = new Size(159, 23);
            CollectionCustomNUD.TabIndex = 29;
            CollectionCustomNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // SyncMIntoNomButton
            // 
            SyncMIntoNomButton.BackColor = Color.FromArgb(60, 63, 65);
            SyncMIntoNomButton.Location = new Point(15, 134);
            SyncMIntoNomButton.Name = "SyncMIntoNomButton";
            SyncMIntoNomButton.Size = new Size(159, 23);
            SyncMIntoNomButton.TabIndex = 28;
            SyncMIntoNomButton.Text = "Sync Min to Nom";
            SyncMIntoNomButton.UseVisualStyleBackColor = false;
            SyncMIntoNomButton.Click += button3_Click;
            // 
            // SyncNomtoMinButton
            // 
            SyncNomtoMinButton.BackColor = Color.FromArgb(60, 63, 65);
            SyncNomtoMinButton.Location = new Point(15, 163);
            SyncNomtoMinButton.Name = "SyncNomtoMinButton";
            SyncNomtoMinButton.Size = new Size(159, 23);
            SyncNomtoMinButton.TabIndex = 27;
            SyncNomtoMinButton.Text = "Sync Nom to Min";
            SyncNomtoMinButton.UseVisualStyleBackColor = false;
            SyncNomtoMinButton.Click += button2_Click;
            // 
            // UpdateTypesFileButton
            // 
            UpdateTypesFileButton.BackColor = Color.FromArgb(60, 63, 65);
            UpdateTypesFileButton.Location = new Point(15, 192);
            UpdateTypesFileButton.Name = "UpdateTypesFileButton";
            UpdateTypesFileButton.Size = new Size(159, 23);
            UpdateTypesFileButton.TabIndex = 26;
            UpdateTypesFileButton.Text = "Update Types file";
            UpdateTypesFileButton.UseVisualStyleBackColor = false;
            UpdateTypesFileButton.Click += button1_Click;
            // 
            // ZeroiseButton
            // 
            ZeroiseButton.BackColor = Color.FromArgb(60, 63, 65);
            ZeroiseButton.Location = new Point(15, 47);
            ZeroiseButton.Name = "ZeroiseButton";
            ZeroiseButton.Size = new Size(159, 23);
            ZeroiseButton.TabIndex = 25;
            ZeroiseButton.Text = "Zero all Entries";
            ZeroiseButton.UseVisualStyleBackColor = false;
            ZeroiseButton.Click += button4_Click;
            // 
            // CollectionNameTB
            // 
            CollectionNameTB.BackColor = Color.FromArgb(60, 63, 65);
            CollectionNameTB.BorderStyle = BorderStyle.None;
            CollectionNameTB.ForeColor = SystemColors.ButtonFace;
            CollectionNameTB.Location = new Point(101, 25);
            CollectionNameTB.Margin = new Padding(4, 3, 4, 3);
            CollectionNameTB.Name = "CollectionNameTB";
            CollectionNameTB.ReadOnly = true;
            CollectionNameTB.Size = new Size(175, 16);
            CollectionNameTB.TabIndex = 3;
            CollectionNameTB.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 25);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(70, 15);
            label1.TabIndex = 2;
            label1.Text = "Catgegory:-";
            // 
            // TypesCollectionControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(TypesCollectionGB);
            ForeColor = SystemColors.Control;
            Name = "TypesCollectionControl";
            Size = new Size(645, 231);
            TypesCollectionGB.ResumeLayout(false);
            TypesCollectionGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)CollectionCustomNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox TypesCollectionGB;
        private TextBox CollectionNameTB;
        private Label label1;
        private Button ZeroiseButton;
        private Button UpdateTypesFileButton;
        private Button SyncMIntoNomButton;
        private Button SyncNomtoMinButton;
        private Button SetCustomButton;
        private NumericUpDown CollectionCustomNUD;
        private CheckBox ChangeMinCB;
        private CheckBox ChangeMinCheckBox;
        private Button MultiplierButton;
        private ComboBox MultiplierCB;
        private CheckBox if0setto1CB;
    }
}
