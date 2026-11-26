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
    }
}
