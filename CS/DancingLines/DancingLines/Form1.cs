using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DancingLines
{
    public partial class Form1 : Form
    {
        Random rand;
        int i, r, x, y;
        Bitmap picture;

        public Form1()
        {
            InitializeComponent();

            picture = new Bitmap(200, 200);
            pictureBox1.Image = picture;
            pictureBox1.Height = picture.Height;
            pictureBox1.Width = picture.Width;
        }

        private void btDance_Click(object sender, EventArgs e)
        {
            rand = new Random();
            x=picture.Width/2;
            y = picture.Height/2;

            //draw line
            for (i = 0; i < 500; i++)
            {                
                using (Graphics g = Graphics.FromImage(picture))
                {
                    g.DrawRectangle(new Pen(Color.Blue, 1), x, y, 1, 1);
                    
                    pictureBox1.Invalidate();
                }

                //randomly determine where to paint the next point
                r = rand.Next(1, 9);
                switch (r)
                {
                    case 1:
                        x--;
                        y++;
                        break;
                    case 2:
                        y++;
                        break;
                    case 3:
                        x++;
                        y++;
                        break;
                    case 4:
                        x++;
                        break;
                    case 5:
                        x++;
                        y--;
                        break;
                    case 6:
                        y--;
                        break;
                    case 7:
                        y--;
                        x--;
                        break;
                    case 8:
                        x--;
                        break;
                }
            }
        }
    }
}
