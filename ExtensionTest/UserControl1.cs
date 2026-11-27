using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ExtensionTest
{
    public partial class UserControl1 : UserControl
    {
        IGExtHandle Handler;
        public UserControl1(IGExtHandle handle)
        {
            Handler = handle;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Handler.ChangedTitle(textBox3.Text);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Handler.ChangedFavicon(textBox2.Text);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                string text = textBox1.Text;
                if (checkBox1.Checked)
                    listView1.Items.Add(text);
                else
                    switch (numericUpDown1.Value)
                    {
                        case 0: checkedListBox1.Items.Add(text); break;
                        case 1: checkedListBox2.Items.Add(text); break;
                        case 2: checkedListBox3.Items.Add(text); break;
                        default: break;
                    }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Handler.Log(textBox4.Text);
        }
    }
}
