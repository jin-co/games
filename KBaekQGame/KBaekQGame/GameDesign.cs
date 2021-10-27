﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KBaekQGame
{
    /* Game design form that creates initial game board
     * depending on the user input
     */
    public partial class GameDesign : Form
    {
        // variables
        int rows, cols, xGap, yGap, gap = 5, blockSize = 30, xStart, yStart;

        // list
        PictureBox[,] cubes = new PictureBox[,] { };

        // test 
        //PictureBox toolBoxPic = new PictureBox();
        Image toolBoxPic;
        PictureBox temp = new PictureBox();
        

        public GameDesign()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Generates cubes based on the rows and columns 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            // clear game board
            spcBoard.Panel2.Controls.Clear();

            // checks input error
            if (!int.TryParse(txtRows.Text, out rows) || !int.TryParse(txtCols.Text, out cols))
            {
                MessageBox.Show(                     
                    "Please provide numbers for rows and columns",
                    "Q Game",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            // sets array size
            cubes = new PictureBox[rows, cols];

            // sets starting point
            xStart = (spcBoard.Panel2.Width / 2) - ((cols / 2) * (blockSize + gap));
            yStart = (spcBoard.Panel2.Height / 2) - ((rows / 2) * (blockSize + gap));
            xGap = xStart;
            yGap = yStart;

            // generates board
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    PictureBox pic = new PictureBox();
                    pic.Width = blockSize;
                    pic.Height = blockSize;
                    pic.Left = xGap + (pic.Width * col);
                    pic.Top = yGap + (pic.Height * row);
                    pic.BorderStyle = BorderStyle.Fixed3D;
                    pic.SizeMode = PictureBoxSizeMode.StretchImage;
                    cubes[row, col] = pic;
                    spcBoard.Panel2.Controls.Add(pic);
                    pic.Click += Cube_Click;
                    xGap += gap;
                }
                xGap = xStart;
                yGap += gap;
            }
            yGap = yStart;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Closes the design form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Selects the image chosen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cube_Click(object sender, EventArgs e)
        {
            PictureBox clicked = (PictureBox)sender;
            clicked.Image = toolBoxPic;
            
            rtbTest.Text = "chosen";
        }

        private void ToolBox_Click(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            if (pic.AccessibleName.Equals("none"))
            {
                toolBoxPic = null;
                return;
            }
            toolBoxPic = pic.Image;
        }

        private void ToolBoxBtn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string fileName = btn.Name.Substring(3).ToLower();
            if (fileName.Equals("none"))
            {
                toolBoxPic = null;
                return;
            }
            string path = Path.GetFullPath(@"../../../");
            toolBoxPic = Image.FromFile(path + $@"KBaekQGame\Resources\{fileName}.jpg");
        }
    }
}