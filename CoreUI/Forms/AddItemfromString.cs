using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Day2eEditor
{
    public partial class AddItemfromString : Form
    {
        private FormController controller;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string TitleLable
        {
            set
            {
                TitleLabel.Text = value;
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<string> addedtypes { get; set; }
        public AddItemfromString()
        {
            InitializeComponent();
            controller = new FormController(
                this,
                panel1,
                null,
                TitleLabel,
                null,
                CloseButton,
                null
            );
            addedtypes = new List<string>();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            addedtypes = richTextBox1.Lines.ToList();
        }

        private void darkButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
