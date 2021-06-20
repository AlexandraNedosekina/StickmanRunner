using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StickmanRunner
{
    public partial class Form1 : Form
    {
        bool jumping = false;
        int jumpSpeed;
        int forse = 8;
        int score = 0;
        int obstacleSpeed = 10;
        Random rand = new Random();
        int position;
        bool isGameOver = false;

        public Form1()
        {
            InitializeComponent();
            GameReset();
        }


        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            Stickman.Top += jumpSpeed;
            txtScore.Text = "Score: " + score;

            if (jumping == true && forse < 0)
            {
                jumping = false;
            }

            if (jumping == true)
            {
                jumpSpeed = -15;
                forse -= 1;
            }
            else
            {
                jumpSpeed = 12;
            }

            if (Stickman.Top > 317 && jumping == false)
            {
                forse = 8;
                Stickman.Top = 318;
                jumpSpeed = 0;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    x.Left -= obstacleSpeed;

                    if (x.Left < -100)
                    {
                        x.Left = this.ClientSize.Width + rand.Next(200, 500) + (x.Width * 1);
                        score++;
                    }
                    if (Stickman.Bounds.IntersectsWith(x.Bounds))
                    {
                        gameTimer.Stop();
                        Stickman.Image = Properties.Resources.dead;
                        txtScore.Text += " Press R to restart the game! ";
                        isGameOver = true;
                    }
                }
            }

            if (score > 5)
            {
                obstacleSpeed = 15;
            }
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }

        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (jumping == true)
            {
                jumping = false;
            }
            if (e.KeyCode == Keys.R && isGameOver == true)
            {
                GameReset();
            }

        }
        private void GameReset()
        {
            forse = 12;
            jumpSpeed = 0;
            jumping = false;
            score = 0;
            obstacleSpeed = 10;
            txtScore.Text = "Score: " + score;
            Stickman.Image = Properties.Resources._200;
            isGameOver = false;
            Stickman.Top = 318;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    position = this.ClientSize.Width + rand.Next(500, 800) + (x.Width * 15);
                    x.Left = position;
                }
            }
            gameTimer.Start();
        }
    }
}