using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Snake
{
    public partial class Form1 : Form
    {
        int cols = 50, rows = 25, score = 0, dx = 0, dy = 0, front = 0, back = 0;
        Piece[] snake = new Piece[1250];
        List<int> available = new List<int>();
        bool[,] visit;

        Random rand = new Random();
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        private Direction currentDirection = Direction.Right;
        public Form1()
        {
            InitializeComponent();
            intial();
            launchTimer();
        }

        private void launchTimer()
        {
            timer.Interval = 50;
            timer.Tick += move;
            timer.Start();
        }

        private void move(object sender, EventArgs e)
        {
            int x = snake[front].Location.X, y = snake[front].Location.Y;
            if (dx == 0 && dy == 0)
            {
                return;
            }
            if (game_over(x + dx, y + dy))
            {
                timer.Stop();
                MessageBox.Show("Гру закінчено!");
                return;
            }
            if (collisionFood(x + dx, y + dy))
            {
                score += 1;
                lblScore.Text = "Рахунок: " + score.ToString();
                if (hits((y + dy) / 20, (x + dx) / 20))
                {
                    return;
                }
                Piece head = new Piece(x + dx, y + dy);
                front = (front - 1 + 1250) % 1250;
                snake[front] = head;
                visit[head.Location.Y / 20, head.Location.X / 20] = true;
                Controls.Add(head);
                randomFood();
            }
            else
            {
                if (hits((y + dy) / 20, (x + dx) / 20)) return;
                visit[snake[back].Location.Y / 20, snake[back].Location.X / 20] = false;
                front = (front - 1 + 1250) % 1250;
                snake[front] = snake[back];
                snake[front].Location = new Point(x + dx, y + dy);
                back = (back - 1 + 1250) % 1250;
                visit[(y + dy) / 20, (x + dx) / 20] = true;
            }
        }

        private void randomFood()
        {
            available.Clear();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (!visit[i, j])
                    {
                        available.Add(i * cols + j);
                    }
                }
            }
            int idx = rand.Next(available.Count) % available.Count;
            lblFood.Left = (available[idx] % cols) * 20;
            lblFood.Top = (available[idx] / cols) * 20;

            // Зміна позиції їжі, щоб з'являлась в межах поля 960 на 460
            lblFood.Left = Math.Min(lblFood.Left, 960 - lblFood.Width);
            lblFood.Top = Math.Min(lblFood.Top, 460 - lblFood.Height);
        }


        private bool hits(int x, int y)
        {
            if (visit[x, y])
            {
                timer.Stop();
                MessageBox.Show("Змійка вкусила свого хвоста!");
                return true;
            }
            return false;
        }

        private bool collisionFood(int x, int y)
        {
            int foodLeft = lblFood.Location.X;
            int foodTop = lblFood.Location.Y;
            int foodRight = foodLeft + lblFood.Width - 1;  
            int foodBottom = foodTop + lblFood.Height - 1;  

            int snakeLeft = x;
            int snakeTop = y;
            int snakeRight = x + snake[front].Width - 1;
            int snakeBottom = y + snake[front].Height - 1;


            bool isInsideHorizontal = (snakeLeft >= foodLeft && snakeLeft <= foodRight) || (snakeRight >= foodLeft && snakeRight <= foodRight);
            bool isInsideVertical = (snakeTop >= foodTop && snakeTop <= foodBottom) || (snakeBottom >= foodTop && snakeBottom <= foodBottom);

            return isInsideHorizontal && isInsideVertical;
        }



        private bool game_over(int x, int y)
        {
            return x <= 0 || y <= 0 || x >= 1000 || y >= 500;
        }

        private void intial()
        {
            visit = new bool[rows, cols];
            Piece head = new Piece((rand.Next() % cols) * 20, (rand.Next() % rows) * 20);
            lblFood.Location = new Point((rand.Next() % cols) * 20, (rand.Next() % rows) * 20);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    visit[i, j] = false;
                    available.Add(i * cols + j);
                }
            }
            visit[head.Location.Y / 20, head.Location.X / 20] = true;
            available.Remove(head.Location.Y / 20 * cols + head.Location.X / 20);
            Controls.Add(head); snake[front] = head;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ClientSize = new Size(1020, 520);
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            Panel leftColumn = new Panel();
            leftColumn.Location = new Point(0, 0);
            leftColumn.Size = new Size(20, ClientSize.Height);
            leftColumn.BackColor = Color.DarkKhaki;

            Panel rightColumn = new Panel();
            rightColumn.Location = new Point(ClientSize.Width - 20, 0);
            rightColumn.Size = new Size(20, ClientSize.Height);
            rightColumn.BackColor = Color.DarkKhaki;
            Panel topRow = new Panel();
            topRow.Location = new Point(0, 0);
            topRow.Size = new Size(ClientSize.Width, 20);
            topRow.BackColor = Color.DarkKhaki;

            Panel bottomRow = new Panel();
            bottomRow.Location = new Point(0, ClientSize.Height - 20);
            bottomRow.Size = new Size(ClientSize.Width, 20);
            bottomRow.BackColor = Color.DarkKhaki;

            Controls.Add(leftColumn);
            Controls.Add(rightColumn);
            Controls.Add(topRow);
            Controls.Add(bottomRow);
        }

        private void Snake_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    if (currentDirection != Direction.Left)
                    {
                        currentDirection = Direction.Right;
                        dx = 20;
                        dy = 0;
                    }
                    break;
                case Keys.Left:
                    if (currentDirection != Direction.Right)
                    {
                        currentDirection = Direction.Left;
                        dx = -20;
                        dy = 0;
                    }
                    break;
                case Keys.Up:
                    if (currentDirection != Direction.Down)
                    {
                        currentDirection = Direction.Up;
                        dx = 0;
                        dy = -20;
                    }
                    break;
                case Keys.Down:
                    if (currentDirection != Direction.Up)
                    {
                        currentDirection = Direction.Down;
                        dx = 0;
                        dy = 20;
                    }
                    break;
            }
        }
    }
}