namespace ProjectsPlugin
{
    partial class ProjectForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            listView1 = new ListView();
            PluginHeader = new ColumnHeader();
            InstalledHeader = new ColumnHeader();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.BackColor = Color.FromArgb(60, 63, 65);
            listView1.Columns.AddRange(new ColumnHeader[] { PluginHeader, InstalledHeader });
            listView1.ForeColor = SystemColors.Control;
            listView1.FullRowSelect = true;
            listView1.Location = new Point(12, 12);
            listView1.MultiSelect = false;
            listView1.Name = "listView1";
            listView1.Size = new Size(307, 385);
            listView1.TabIndex = 1;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // PluginHeader
            // 
            PluginHeader.Text = "Plugins";
            PluginHeader.Width = 200;
            // 
            // InstalledHeader
            // 
            InstalledHeader.Text = "Installed";
            InstalledHeader.Width = 100;
            // 
            // ProjectForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            ClientSize = new Size(800, 450);
            Controls.Add(listView1);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.None;
            Name = "ProjectForm";
            Text = "Form1";
            FormClosed += ProjectForm_FormClosed;
            Load += ProjectForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private ListView listView1;
        private ColumnHeader PluginHeader;
        private ColumnHeader InstalledHeader;
    }
}
