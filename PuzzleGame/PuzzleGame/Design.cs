﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuzzleGame
{
    public partial class Design : Form
    {
        public Design()
        {
            InitializeComponent();
        }

        private void designToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void level13X3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PuzzleData.currentLevel = 1;
        }
    }
}
