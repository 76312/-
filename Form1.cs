using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 黑白棋
{
    public partial class Form1 : Form
    {
        Graphics g;
        int temp = 0;
        int pos_x = 0;//上一步坐标
        int pos_y = 0;

        int num_black = 0;//黑棋数
        int num_write = 0;//白棋数
        int successer = 0;//胜利者，0表示未分胜负，1为黑色方，2为白色方，-1代表棋盘已满



        int[,] map = new int[15,15];
        public Form1()
        {
            InitializeComponent();
            this.Width = 650;
            this.Height = 480;
            
            //初始化棋盘
            for(int i=0;i<15;i++)
            {
                for(int j = 0; j < 15; j++)
                {
                    map[i,j] = 0; 
                }
            }
        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            g = this.CreateGraphics();
            Pen pen_black = new Pen(Color.Black);
            pen_black.Width = 2;
            for (int i = 0; i < 15; i++)
            {
                g.DrawLine(pen_black, 10, 10 + 30 * i, 430, 10 + 30 * i);
                g.DrawLine(pen_black, 10 + 30 * i, 10, 10 + 30 * i, 430);
                for (int j = 0; j < 15; j++)
                {

                    if (map[i, j] == 1)
                    {
                        g.FillEllipse(Brushes.Black, new Rectangle(i * 30, j * 30 , 20, 20));
                    }
                    else if (map[i, j] == 2)
                    {
                        g.FillEllipse(Brushes.White, new Rectangle(i * 30, j * 30, 20, 20));
                    }

                }
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Point p = Control.MousePosition;
            p = this.PointToClient(p);
            foundPoint(ref p);
            this.RefreshMap();
            successer = JudgeResult();
            //if(p.X<=450&&p.Y<=450)
            //{
            //    if (temp == 0)
            //    {
            //        temp = 1;
            //        g.FillEllipse(Brushes.Black, new Rectangle(p.X - 25, p.Y - 25, 50, 50));
            //    }
            //    else
            //    {
            //        temp = 0;
            //        g.FillEllipse(Brushes.White, new Rectangle(p.X - 25, p.Y - 25, 50, 50));
            //    }
            //}
        }

        //判断点击的下棋地址
        void foundPoint(ref Point p)
        {
            int x = p.X/30;
            int y = p.Y/30;

            Console.WriteLine("坐标："+ x +','+y);
            try
            {
                if (map[x, y] == 0)
                {
                    pos_x = x;
                    pos_y = y;
                    if (temp == 0)
                    {
                        map[x, y] = 1;
                        label4.Text = Convert.ToString(Convert.ToInt32(label4.Text) + 1);
                        label6.Text = Convert.ToString(Convert.ToInt32(label6.Text) + 1);
                        temp = 1;
                    }
                    else
                    {
                        map[x, y] = 2;
                        label5.Text = Convert.ToString(Convert.ToInt32(label5.Text) + 1);
                        label6.Text = Convert.ToString(Convert.ToInt32(label6.Text) + 1);
                        temp = 0;
                    }
                }
            }
            catch { }
        }

        //刷新界面
        void RefreshMap()
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {

                    if (map[i, j] == 1)
                    {
                        g.FillEllipse(Brushes.Black, new Rectangle(i * 30, j * 30, 20, 20));
                    }
                    else if (map[i, j] == 2)
                    {
                        g.FillEllipse(Brushes.White, new Rectangle(i * 30, j * 30, 20, 20));
                    }

                }
            }
        }

        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            this.RefreshMap();
        }


        //判断胜负；
        private int JudgeResult()
        {
            //判断竖列
            if (pos_y < 11 &&
                map[pos_x, pos_y + 4] == map[pos_x, pos_y] &&
                map[pos_x, pos_y + 3] == map[pos_x, pos_y] &&
                map[pos_x, pos_y + 2] == map[pos_x, pos_y] &&
                map[pos_x, pos_y + 1] == map[pos_x, pos_y])
            {
                EndToGame();
            }
            else if (pos_y < 12 && pos_y > 0 &&
                 map[pos_x, pos_y - 1] == map[pos_x, pos_y] &&
                 map[pos_x, pos_y + 3] == map[pos_x, pos_y] &&
                 map[pos_x, pos_y + 2] == map[pos_x, pos_y] &&
                 map[pos_x, pos_y + 1] == map[pos_x, pos_y])
            {
                EndToGame();
            }
            else if (pos_y < 13 && pos_y > 1 &&
                 map[pos_x, pos_y - 1] == map[pos_x, pos_y] &&
                 map[pos_x, pos_y - 2] == map[pos_x, pos_y] &&
                 map[pos_x, pos_y + 2] == map[pos_x, pos_y] &&
                 map[pos_x, pos_y + 1] == map[pos_x, pos_y])
            {
                EndToGame();
            }
            else if (pos_y < 14 && pos_y > 2 &&
                 map[pos_x, pos_y - 1] == map[pos_x, pos_y] &&
                 map[pos_x, pos_y - 3] == map[pos_x, pos_y] &&
                 map[pos_x, pos_y - 2] == map[pos_x, pos_y] &&
                 map[pos_x, pos_y + 1] == map[pos_x, pos_y])
            {
                EndToGame();
            }
            else if (pos_y > 3 &&
                 map[pos_x, pos_y - 4] == map[pos_x, pos_y] &&
                 map[pos_x, pos_y - 3] == map[pos_x, pos_y] &&
                 map[pos_x, pos_y - 2] == map[pos_x, pos_y] &&
                 map[pos_x, pos_y - 1] == map[pos_x, pos_y])
            {
                EndToGame();
            }

            //判断横列
            else if (pos_x < 11 &&
                 map[pos_x + 1, pos_y] == map[pos_x, pos_y] &&
                 map[pos_x + 2, pos_y] == map[pos_x, pos_y] &&
                 map[pos_x + 3, pos_y] == map[pos_x, pos_y] &&
                 map[pos_x + 4, pos_y] == map[pos_x, pos_y])
            {
                EndToGame();
            }
            else if (pos_x < 12 && pos_x > 0 &&
                 map[pos_x + 1, pos_y] == map[pos_x, pos_y] &&
                 map[pos_x + 2, pos_y] == map[pos_x, pos_y] &&
                 map[pos_x + 3, pos_y] == map[pos_x, pos_y] &&
                 map[pos_x - 1, pos_y] == map[pos_x, pos_y])
            {
                EndToGame();
            }
            else if (pos_x < 13 && pos_x > 1 &&
                map[pos_x + 1, pos_y] == map[pos_x, pos_y] &&
                map[pos_x + 2, pos_y] == map[pos_x, pos_y] &&
                map[pos_x - 1, pos_y] == map[pos_x, pos_y] &&
                map[pos_x - 2, pos_y] == map[pos_x, pos_y])
            {
                EndToGame();
            }
            else if (pos_x < 14 && pos_x > 2 &&
                map[pos_x + 1, pos_y] == map[pos_x, pos_y] &&
                map[pos_x - 3, pos_y] == map[pos_x, pos_y] &&
                map[pos_x - 1, pos_y] == map[pos_x, pos_y] &&
                map[pos_x - 2, pos_y] == map[pos_x, pos_y])
            {
                EndToGame();
            }
            else if (pos_x > 3 &&
                map[pos_x - 3, pos_y] == map[pos_x, pos_y] &&
                map[pos_x - 4, pos_y] == map[pos_x, pos_y] &&
                map[pos_x - 1, pos_y] == map[pos_x, pos_y] &&
                map[pos_x - 2, pos_y] == map[pos_x, pos_y])
            {
                EndToGame();
            }

            //判断左斜
            else if (pos_x < 11 && pos_y < 11 &&
                map[pos_x + 1, pos_y + 1] == map[pos_x, pos_y] &&
                map[pos_x + 2, pos_y + 2] == map[pos_x, pos_y] &&
                map[pos_x + 3, pos_y + 3] == map[pos_x, pos_y] &&
                map[pos_x + 4, pos_y + 4] == map[pos_x, pos_y])
            {
                EndToGame();
            }
            else if (pos_x < 12 && pos_y < 12 && pos_x > 0 && pos_y > 0 &&
                map[pos_x + 1, pos_y + 1] == map[pos_x, pos_y] &&
                map[pos_x + 2, pos_y + 2] == map[pos_x, pos_y] &&
                map[pos_x + 3, pos_y + 3] == map[pos_x, pos_y] &&
                map[pos_x - 1, pos_y - 1] == map[pos_x, pos_y])
            {
                EndToGame();
            }
            else if (pos_x <13 && pos_y < 13 && pos_x > 1 && pos_y > 1 &&
                map[pos_x + 1, pos_y + 1] == map[pos_x, pos_y] &&
                map[pos_x + 2, pos_y + 2] == map[pos_x, pos_y] &&
                map[pos_x - 1, pos_y - 1] == map[pos_x, pos_y] &&
                map[pos_x - 2, pos_y - 2] == map[pos_x, pos_y])
            {
                EndToGame();
            }
            else if (pos_x < 14 && pos_y < 14 && pos_x > 2 && pos_y > 2 &&
                map[pos_x - 1, pos_y - 1] == map[pos_x, pos_y] &&
                map[pos_x - 2, pos_y - 2] == map[pos_x, pos_y] &&
                map[pos_x - 3, pos_y - 3] == map[pos_x, pos_y] &&
                map[pos_x + 1, pos_y + 1] == map[pos_x, pos_y])
            {
                EndToGame();
            }
            else if (pos_x > 3 && pos_y > 3 &&
                map[pos_x - 1, pos_y - 1] == map[pos_x, pos_y] &&
                map[pos_x - 2, pos_y - 2] == map[pos_x, pos_y] &&
                map[pos_x - 3, pos_y - 3] == map[pos_x, pos_y] &&
                map[pos_x - 4, pos_y - 4] == map[pos_x, pos_y])
            {
                EndToGame();
            }

            //判断右斜
            else if (pos_x < 13 && 14-pos_y < 13 && pos_x > 1 && 14 - pos_y > 1 &&
                map[pos_x + 1, pos_y - 1] == map[pos_x, pos_y] &&
                map[pos_x + 2, pos_y - 2] == map[pos_x, pos_y] &&
                map[pos_x - 1, pos_y + 1] == map[pos_x, pos_y] &&
                map[pos_x - 2, pos_y + 2] == map[pos_x, pos_y])
            {
                EndToGame();
            }
            else if (pos_x < 14 && 14-pos_y < 14 && pos_x > 2 && 14 - pos_y > 2 &&
                map[pos_x + 1, pos_y - 1] == map[pos_x, pos_y] &&
                map[pos_x - 3, pos_y + 3] == map[pos_x, pos_y] &&
                map[pos_x - 1, pos_y + 1] == map[pos_x, pos_y] &&
                map[pos_x - 2, pos_y + 2] == map[pos_x, pos_y])
            {
                EndToGame();
            }
            else if (pos_x > 3 && 14 - pos_y > 3 &&
                map[pos_x - 3, pos_y + 3] == map[pos_x, pos_y] &&
                map[pos_x - 4, pos_y + 4] == map[pos_x, pos_y] &&
                map[pos_x - 1, pos_y + 1] == map[pos_x, pos_y] &&
                map[pos_x - 2, pos_y + 2] == map[pos_x, pos_y])
            {
                EndToGame();
            }
            else if (pos_x < 11 && 14 - pos_y < 11 &&
                map[pos_x + 3, pos_y - 3] == map[pos_x, pos_y] &&
                map[pos_x + 4, pos_y - 4] == map[pos_x, pos_y] &&
                map[pos_x + 1, pos_y - 1] == map[pos_x, pos_y] &&
                map[pos_x + 2, pos_y - 2] == map[pos_x, pos_y])
            {
                EndToGame();
            }
            else if (pos_x < 12 && 14-pos_y < 12 && pos_x > 1 && 14 - pos_y > 1 &&
                map[pos_x - 1, pos_y + 1] == map[pos_x, pos_y] &&
                map[pos_x + 3, pos_y - 3] == map[pos_x, pos_y] &&
                map[pos_x + 1, pos_y - 1] == map[pos_x, pos_y] &&
                map[pos_x + 2, pos_y - 2] == map[pos_x, pos_y])
            {
                EndToGame();
            }
            return 0;
        }
            //游戏结束
            void EndToGame()
        {
            //显示游戏结果
            if (map[pos_x, pos_y] == 1)
            {
                MessageBox.Show("游戏结束!" + '\n' + "黑色方胜利！");
            }
            else
            { 
                MessageBox.Show("游戏结束!" + '\n' + "白色方胜利！"); 
            }
            label4.Text = "0";
            label5.Text = "0";
            label6.Text = "0";
            //刷新游戏界面
            //初始化棋盘
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    map[i, j] = 0;
                }
            }
            g.Clear(Color.LimeGreen);
            Pen pen_black = new Pen(Color.Black);
            pen_black.Width = 2;
            for (int i = 0; i < 15; i++)
            {
                g.DrawLine(pen_black, 10, 10 + 30 * i, 430, 10 + 30 * i);
                g.DrawLine(pen_black, 10 + 30 * i, 10, 10 + 30 * i, 430);
                for (int j = 0; j < 15; j++)
                {

                    if (map[i, j] == 1)
                    {
                        g.FillEllipse(Brushes.Black, new Rectangle(i * 30, j * 30, 20, 20));
                    }
                    else if (map[i, j] == 2)
                    {
                        g.FillEllipse(Brushes.White, new Rectangle(i * 30, j * 30, 20, 20));
                    }

                }
            }
            this.RefreshMap();
        }
    }
}
