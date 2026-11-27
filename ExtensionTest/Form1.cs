using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace ExtensionTest
{
    public partial class Form1 : Form
    {
        IGExtHandle Handler;
        public Form1(IGExtHandle handle)
        {
            Handler = handle;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Text = textBox3.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (WebClient web = new WebClient()) {
                try
                {
                    byte[] data = web.DownloadData(textBox2.Text);
                    using (MemoryStream mem = new MemoryStream(data))
                    {
                        Icon = new Icon(mem);
                    }
                } catch (WebException ex) { }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text)){
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
