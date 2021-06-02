using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace SquareChaser
{
    public partial class Form1 : Form
    {
        //Global Variables
        Rectangle player1 = new Rectangle(175, 100, 25, 25);
        Rectangle player2 = new Rectangle(375, 100, 25, 25);
        Rectangle square = new Rectangle(395, 200, 10, 10);
        Rectangle powerUp = new Rectangle(150, 350, 5, 5);
        Rectangle powerDown = new Rectangle(500, 300, 5, 5);

        int player1score = 0;
        int player2score = 0;

        int player1speed = 8;
        int player2speed = 8;

        bool wDown = false;
        bool sDown = false;
        bool aDown = false;
        bool dDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool leftArrowDown = false;
        bool rightArrowDown = false;

        SolidBrush blackBrush = new SolidBrush(Color.Black);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);

        SoundPlayer musicPlayer;
        Random randGen = new Random();

        public Form1()
        {
            InitializeComponent();
            //Adding start up music
            musicPlayer = new SoundPlayer(Properties.Resources.gameStart);
            musicPlayer.Play();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //Adding random num generator for moving objects after being collected
            int xCoordinate;
            int yCoordinate;

            xCoordinate = randGen.Next(1, 601);
            yCoordinate = randGen.Next(1, 401);

            //Move Player 1
            if (wDown == true && player1.Y > 0)
            {
                player1.Y -= player1speed;
                outputLabel.Text = "";
            }

            if (sDown == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y += player1speed;
                outputLabel.Text = "";
            }

            if (aDown == true && player1.X > 0)
            {
                player1.X -= player1speed;
                outputLabel.Text = "";
            }

            if (dDown == true && player1.X < this.Width - player1.Width)
            {
                player1.X += player1speed;
                outputLabel.Text = "";
            }

            //Move Player 2
            if (upArrowDown == true && player2.Y > 0)
            {
                player2.Y -= player2speed;
                outputLabel.Text = "";
            }

            if (downArrowDown == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y += player2speed;
                outputLabel.Text = "";
            }

            if (rightArrowDown == true && player2.X < this.Width - player2.Width)
            {
                player2.X += player2speed;
                outputLabel.Text = "";
            }

            if (leftArrowDown == true && player2.X > 0)
            {
                player2.X -= player2speed;
                outputLabel.Text = "";
            }

            //Adding score to player who gets square
            if (player1.IntersectsWith(square))
            {
                player1score++;
                p1ScoreLabel.Text = $"{player1score}";
                musicPlayer = new SoundPlayer(Properties.Resources.pointScored);
                musicPlayer.Play();

                square.X = xCoordinate;
                square.Y = yCoordinate;
                
            }

            if (player2.IntersectsWith(square))
            {
                player2score++;
                p2ScoreLabel.Text = $"{player2score}";
                musicPlayer = new SoundPlayer(Properties.Resources.pointScored);
                musicPlayer.Play();

                square.X = xCoordinate;
                square.Y = yCoordinate;
            }

            //Making player who grabs powerup faster
            if (player1.IntersectsWith(powerUp))
            {
                player1speed++;
                musicPlayer = new SoundPlayer(Properties.Resources.speedUp);
                musicPlayer.Play();

                powerUp.X = xCoordinate;
                powerUp.Y = yCoordinate;
            }

            if (player2.IntersectsWith(powerUp))
            {
                player2speed++;
                musicPlayer = new SoundPlayer(Properties.Resources.speedUp);
                musicPlayer.Play();

                powerUp.X = xCoordinate;
                powerUp.Y = yCoordinate;
            }

            //Making player who grabs blue square deduct a point
            if (player1.IntersectsWith(powerDown))
            {
                player1score--;
                p1ScoreLabel.Text = $"{player1score}";
                musicPlayer = new SoundPlayer(Properties.Resources.powerDown);
                musicPlayer.Play();

                powerDown.X = xCoordinate;
                powerDown.Y = yCoordinate;
            }

            if (player2.IntersectsWith(powerDown))
            {
                player2score--;
                p2ScoreLabel.Text = $"{player2score}";
                musicPlayer = new SoundPlayer(Properties.Resources.powerDown);
                musicPlayer.Play();

                powerDown.X = xCoordinate;
                powerDown.Y = yCoordinate;
            }

            //Declaring a winner
            if (player1score == 5)
            {
                gameTimer.Enabled = false;
                outputLabel.ForeColor = Color.Red;
                outputLabel.Text = "Red Wins!!!";
                musicPlayer = new SoundPlayer(Properties.Resources.gameOver);
                musicPlayer.Play();
            }
            else if (player2score == 5)
            {
                gameTimer.Enabled = false;
                outputLabel.ForeColor = Color.Black;
                outputLabel.Text = "Black Wins!!!";
                musicPlayer = new SoundPlayer(Properties.Resources.gameOver);
                musicPlayer.Play();
            }

            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Adding both players and square
            e.Graphics.FillRectangle(redBrush, player1);
            e.Graphics.FillRectangle(blackBrush, player2);
            e.Graphics.FillRectangle(whiteBrush, square);
            e.Graphics.FillRectangle(yellowBrush, powerUp);
            e.Graphics.FillRectangle(blueBrush, powerDown);
        }
    }
}
