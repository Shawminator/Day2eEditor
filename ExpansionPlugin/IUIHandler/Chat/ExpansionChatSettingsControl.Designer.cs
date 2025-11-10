namespace ExpansionPlugin
{
    partial class ExpansionChatSettingsControl
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
            groupBox34 = new GroupBox();
            EnableExpansionChatCB = new CheckBox();
            EnableTransportChatCB = new CheckBox();
            EnablePartyChatCB = new CheckBox();
            EnableGlobalChatCB = new CheckBox();
            groupBox34.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox34
            // 
            groupBox34.Controls.Add(EnableExpansionChatCB);
            groupBox34.Controls.Add(EnableTransportChatCB);
            groupBox34.Controls.Add(EnablePartyChatCB);
            groupBox34.Controls.Add(EnableGlobalChatCB);
            groupBox34.ForeColor = SystemColors.Control;
            groupBox34.Location = new Point(0, 0);
            groupBox34.Margin = new Padding(4, 3, 4, 3);
            groupBox34.Name = "groupBox34";
            groupBox34.Padding = new Padding(4, 3, 4, 3);
            groupBox34.Size = new Size(175, 124);
            groupBox34.TabIndex = 6;
            groupBox34.TabStop = false;
            groupBox34.Text = "Chat";
            // 
            // EnableExpansionChatCB
            // 
            EnableExpansionChatCB.AutoSize = true;
            EnableExpansionChatCB.Location = new Point(11, 65);
            EnableExpansionChatCB.Margin = new Padding(4, 3, 4, 3);
            EnableExpansionChatCB.Name = "EnableExpansionChatCB";
            EnableExpansionChatCB.Size = new Size(146, 19);
            EnableExpansionChatCB.TabIndex = 3;
            EnableExpansionChatCB.Text = "Enable Expansion Chat";
            EnableExpansionChatCB.UseVisualStyleBackColor = true;
            EnableExpansionChatCB.CheckedChanged += EnableExpansionChatCB_CheckedChanged;
            // 
            // EnableTransportChatCB
            // 
            EnableTransportChatCB.AutoSize = true;
            EnableTransportChatCB.Location = new Point(11, 43);
            EnableTransportChatCB.Margin = new Padding(4, 3, 4, 3);
            EnableTransportChatCB.Name = "EnableTransportChatCB";
            EnableTransportChatCB.Size = new Size(141, 19);
            EnableTransportChatCB.TabIndex = 2;
            EnableTransportChatCB.Text = "Enable Transport Chat";
            EnableTransportChatCB.UseVisualStyleBackColor = true;
            EnableTransportChatCB.CheckedChanged += EnableTransportChatCB_CheckedChanged;
            // 
            // EnablePartyChatCB
            // 
            EnablePartyChatCB.AutoSize = true;
            EnablePartyChatCB.Location = new Point(11, 90);
            EnablePartyChatCB.Margin = new Padding(4, 3, 4, 3);
            EnablePartyChatCB.Name = "EnablePartyChatCB";
            EnablePartyChatCB.Size = new Size(119, 19);
            EnablePartyChatCB.TabIndex = 1;
            EnablePartyChatCB.Text = "Enable Party Chat";
            EnablePartyChatCB.UseVisualStyleBackColor = true;
            EnablePartyChatCB.CheckedChanged += EnablePartyChatCB_CheckedChanged;
            // 
            // EnableGlobalChatCB
            // 
            EnableGlobalChatCB.AutoSize = true;
            EnableGlobalChatCB.Location = new Point(11, 22);
            EnableGlobalChatCB.Margin = new Padding(4, 3, 4, 3);
            EnableGlobalChatCB.Name = "EnableGlobalChatCB";
            EnableGlobalChatCB.Size = new Size(126, 19);
            EnableGlobalChatCB.TabIndex = 0;
            EnableGlobalChatCB.Text = "Enable Global Chat";
            EnableGlobalChatCB.UseVisualStyleBackColor = true;
            EnableGlobalChatCB.CheckedChanged += EnableGlobalChatCB_CheckedChanged;
            // 
            // ExpansionChatSettingsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox34);
            ForeColor = SystemColors.Control;
            Name = "ExpansionChatSettingsControl";
            Size = new Size(175, 124);
            groupBox34.ResumeLayout(false);
            groupBox34.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox34;
        private CheckBox EnableExpansionChatCB;
        private CheckBox EnableTransportChatCB;
        private CheckBox EnablePartyChatCB;
        private CheckBox EnableGlobalChatCB;
    }
}
