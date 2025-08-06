using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Day2eEditor
{
    public partial class ErrorDialog : Form
    {
        public ErrorDialog(string title, IEnumerable<string> errors)
        {
            InitializeComponent();
            label1.Text = title;
            string error = string.Join(Environment.NewLine + Environment.NewLine + Environment.NewLine, errors);
            textBox1.Text = "Please fix all errors noted below....." + Environment.NewLine + Environment.NewLine + error;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
