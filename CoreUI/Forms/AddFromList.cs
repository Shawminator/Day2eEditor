using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Day2eEditor
{
    public partial class AddFromList : Form
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
        public List<string> GetSelected
        {
            get 
            {

                List<string> selectedTexts = new List<string>();

                foreach (var item in listBox1.SelectedItems)
                {
                    selectedTexts.Add(item.ToString());
                }

                return selectedTexts;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<string> List
        {
            set
            {
                listBox1.DataSource = value;
            }
        }

        public AddFromList()
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
        }

        #region ListBox Drawing
        private void listBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            ListBox lb = sender as ListBox;
            if (lb.SelectedItem == null) return;
            e.DrawBackground();
            Brush myBrush = Brushes.Black;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            }
            else
            {
                myBrush = Brushes.White;
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(60, 63, 65)), e.Bounds);
            }
            e.Graphics.DrawString(lb.Items[e.Index].ToString(), e.Font, myBrush, e.Bounds);
            e.DrawFocusRectangle();
        }
        #endregion
    }
}
