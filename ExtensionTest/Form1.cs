using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    }
}
