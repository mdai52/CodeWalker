﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeWalker.Tools
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GithubButton_Click(object sender, EventArgs e) {
            // Open the link to the GitHub repository
            System.Diagnostics.Process.Start("https://github.com/dexyfex/CodeWalker");
        }
    }
}
