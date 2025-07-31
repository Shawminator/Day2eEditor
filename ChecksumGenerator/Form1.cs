namespace ChecksumGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            if (openfile.ShowDialog() == DialogResult.OK )
            {
                textBox1.Text = openfile.FileName;
                textBox2.Text = ChecksumUtils.ComputeSha256Checksum(textBox1.Text);
            }
        }
    }
}
