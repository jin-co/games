﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KBaekQGame
{
    class GameGenerator
    {
        /// <summary>
        /// Generates game board using picture boxes dynamically 
        /// based on the rows and columns that a user has entered
        /// </summary>
        public static void GenerateGame()
        {
            // clear game board
            Game.SpcBoard.Panel2.Controls.Clear();

            // sets starting point
            Game.XStart = (Game.SpcBoard.Panel2.Width / 2) - ((Game.Cols / 2) * (Game.BlockSize + Game.Gap));
            Game.YStart = (Game.SpcBoard.Panel2.Height / 2) - ((Game.Rows / 2) * (Game.BlockSize + Game.Gap));
            Game.XGap = Game.XStart;
            Game.YGap = Game.YStart;

            // generates board
            for (int row = 0; row < Game.Rows; row++)
            {
                for (int col = 0; col < Game.Cols; col++)
                {
                    PictureBox pic = new PictureBox();
                    pic.Width = Game.BlockSize;
                    pic.Height = Game.BlockSize;
                    pic.Left = Game.XGap + (pic.Width * col);
                    pic.Top = Game.YGap + (pic.Height * row);
                    pic.BorderStyle = BorderStyle.Fixed3D;
                    pic.SizeMode = PictureBoxSizeMode.StretchImage;

                    Game.Cubes[row, col] = pic;
                    Game.SpcBoard.Panel2.Controls.Add(pic);
                    pic.Click += Game.Cube_Click;
                    Game.XGap += Game.Gap;
                }
                Game.XGap = Game.XStart;
                Game.YGap += Game.Gap;
            }
            Game.YGap = Game.YStart;
        }
    }
}