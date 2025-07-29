using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace _2048WindowsFormsApp
{
    public partial class MainForm : Form
    {
        private int mapSize=4;
        private Label[,] labelsMap;
        private static Random rnd = new Random();
        private int score = 0;
        private int highScore;
        private bool isGameOver = false;
        public string path = "2048, результаты.json";
        private User user;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            WelcomeForm welcome = new WelcomeForm();
            welcome.ShowDialog();

            this.WindowState = FormWindowState.Maximized;
            user = new User(welcome.userNameTextBox.Text);

            highScore = GetHighScore();

            InitMap();
            GenerateNumber();
            ShowScore();
        }

        private void ShowScore()
        {
            scoreLabel.Text = score.ToString();

            highScoreLabel.Text = highScore.ToString();

            if (score >= highScore)
            {
                highScoreLabel.Text = score.ToString();
                highScore = Int32.Parse(highScoreLabel.Text);
            }
        }

        private void GenerateNumber()
        {
            int countLabels = 0;
            var random=new Random();

            while (countLabels<mapSize*mapSize)
            {
                var randomNumberLabel = rnd.Next(mapSize * mapSize);
                var indexRow = randomNumberLabel / mapSize;
                var indexColumn = randomNumberLabel % mapSize;
                if (labelsMap[indexRow, indexColumn].Text == string.Empty)
                {
                    var randomNumber = random.Next(1, 101);
                    if (randomNumber <= 75)
                    {
                        labelsMap[indexRow, indexColumn].Text = "2";
                        ChangeColor(indexRow, indexColumn);
                        break;
                    }
                    else
                    {
                        labelsMap[indexRow, indexColumn].Text = "4";
                        ChangeColor(indexRow, indexColumn);
                        break;
                    }
                }
                countLabels++;
            }
        }

        private void InitMap()
        {
            ClearMap();
            labelsMap = new Label[mapSize, mapSize];

            int startX = (ClientSize.Width - mapSize * 76) / 2;
            int startY = (ClientSize.Height - mapSize * 76) / 2;

            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    var newLabel = CreateLabel(j, i,startX,startY);
                    Controls.Add(newLabel);
                    labelsMap[i, j] = newLabel;
                }
            }
        }

        private void ClearMap()
        {
            if (labelsMap!=null)
            {
                for (int i=0;i<labelsMap.GetLength(0);i++)
                {
                    for (int j=0;j<labelsMap.GetLength(1);j++)
                    {
                        Controls.Remove(labelsMap[i, j]);
                    }
                }
            }
        }

        private Label CreateLabel(int indexRow, int indexColumn,int startX,int startY)
        {
            var label = new Label();
            label.BackColor = GetNumberColor("");
            label.Font = new Font("Modern No. 20", 18F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            label.Size = new Size(70, 70);
            label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            int x = startX + indexRow * 76;
            int y = startY + indexColumn * 76;
            label.Location = new Point(x, y);
            return label;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (isGameOver)
            {
                MessageBox.Show("Ходов больше нет! Игра окончена!", "Конец игры", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string[,] currentState = new string[mapSize, mapSize];
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    currentState[i, j] = labelsMap[i, j].Text;
                }
            }

            if (e.KeyCode == Keys.Right)
            {
                for (int i = 0; i < mapSize; i++)
                {
                    for (int j = mapSize - 1; j >= 0; j--)
                    {
                        if (labelsMap[i, j].Text != string.Empty)
                        {
                            for (int k = j - 1; k >= 0; k--)
                            {
                                if (labelsMap[i, k].Text != string.Empty)
                                {
                                    if (labelsMap[i, j].Text == labelsMap[i, k].Text)
                                    {
                                        var number = Int32.Parse(labelsMap[i, j].Text);
                                        score += number * 2;
                                        labelsMap[i, j].Text = (number * 2).ToString();
                                        labelsMap[i, k].Text = string.Empty;

                                        ChangeColor(i, j);
                                        ChangeColor(i, k);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < mapSize; i++)
                {
                    for (int j = mapSize - 1; j >= 0; j--)
                    {
                        if (labelsMap[i, j].Text == string.Empty)
                        {
                            for (int k = j - 1; k >= 0; k--)
                            {
                                if (labelsMap[i, k].Text != string.Empty)
                                {
                                    labelsMap[i, j].Text = labelsMap[i, k].Text;
                                    labelsMap[i, k].Text = string.Empty;

                                    ChangeColor(i, j);
                                    ChangeColor(i, k);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.Left)
            {
                for (int i = 0; i < mapSize; i++)
                {
                    for (int j = 0; j < mapSize; j++)
                    {
                        if (labelsMap[i, j].Text != string.Empty)
                        {
                            for (int k = j + 1; k < mapSize; k++)
                            {
                                if (labelsMap[i, k].Text != string.Empty)
                                {
                                    if (labelsMap[i, j].Text == labelsMap[i, k].Text)
                                    {
                                        var number = Int32.Parse(labelsMap[i, j].Text);
                                        score += number * 2;
                                        labelsMap[i, j].Text = (number * 2).ToString();
                                        labelsMap[i, k].Text = string.Empty;

                                        ChangeColor(i, j);
                                        ChangeColor(i, k);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < mapSize; i++)
                {
                    for (int j = 0; j < mapSize; j++)
                    {
                        if (labelsMap[i, j].Text == string.Empty)
                        {
                            for (int k = j + 1; k < mapSize; k++)
                            {
                                if (labelsMap[i, k].Text != string.Empty)
                                {
                                    labelsMap[i, j].Text = labelsMap[i, k].Text;
                                    labelsMap[i, k].Text = string.Empty;

                                    ChangeColor(i, j);
                                    ChangeColor(i, k);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    for (int i = 0; i < mapSize; i++)
                    {
                        if (labelsMap[i, j].Text != string.Empty)
                        {
                            for (int k = i + 1; k < mapSize; k++)
                            {
                                if (labelsMap[k, j].Text != string.Empty)
                                {
                                    if (labelsMap[i, j].Text == labelsMap[k, j].Text)
                                    {
                                        var number = Int32.Parse(labelsMap[i, j].Text);
                                        score += number * 2;
                                        labelsMap[i, j].Text = (number * 2).ToString();
                                        labelsMap[k, j].Text = string.Empty;

                                        ChangeColor(i, j);
                                        ChangeColor(k, j);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }

                for (int j = 0; j < mapSize; j++)
                {
                    for (int i = 0; i < mapSize; i++)
                    {
                        if (labelsMap[i, j].Text == string.Empty)
                        {
                            for (int k = i + 1; k < mapSize; k++)
                            {
                                if (labelsMap[k, j].Text != string.Empty)
                                {
                                    labelsMap[i, j].Text = labelsMap[k, j].Text;
                                    labelsMap[k, j].Text = string.Empty;

                                    ChangeColor(i, j);
                                    ChangeColor(k, j);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.Down)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    for (int i = mapSize - 1; i >= 0; i--)
                    {
                        if (labelsMap[i, j].Text != string.Empty)
                        {
                            for (int k = i - 1; k >= 0; k--)
                            {
                                if (labelsMap[k, j].Text != string.Empty)
                                {
                                    if (labelsMap[i, j].Text == labelsMap[k, j].Text)
                                    {
                                        var number = Int32.Parse(labelsMap[i, j].Text);
                                        score += number * 2;
                                        labelsMap[i, j].Text = (number * 2).ToString();
                                        labelsMap[k, j].Text = string.Empty;

                                        ChangeColor(i, j);
                                        ChangeColor(k, j);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }

                for (int j = 0; j < mapSize; j++)
                {
                    for (int i = mapSize - 1; i >= 0; i--)
                    {
                        if (labelsMap[i, j].Text == string.Empty)
                        {
                            for (int k = i - 1; k >= 0; k--)
                            {
                                if (labelsMap[k, j].Text != string.Empty)
                                {
                                    labelsMap[i, j].Text = labelsMap[k, j].Text;
                                    labelsMap[k, j].Text = string.Empty;

                                    ChangeColor(i, j);
                                    ChangeColor(k, j);
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            bool changed = false;
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (currentState[i, j] != labelsMap[i, j].Text)
                    {
                        changed = true;
                        break;
                    }
                }
                if (changed) break;
            }

            if (changed)
            {
                GenerateNumber();
                ShowScore();

                if (WinGame())
                {
                    MessageBox.Show("Поздравляем, вы победили!","Победа",MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (IsGameOver())
                {
                    isGameOver = true;
                    user.Score = score;
                    UserStorage.SaveResults(user);
                }
            }
        }

        private bool WinGame()
        {
            for (int i=0;i<mapSize;i++)
            {
                for (int j=0;j<mapSize;j++)
                {
                    if (labelsMap[i,j].Text =="2048")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isGameOver = false;
            Application.Restart();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var answer = MessageBox.Show("Вы уверены, что хотите покинуть игру?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void roolsGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RoolsForm roolsForm = new RoolsForm();
            roolsForm.ShowDialog();
        }

        private void fourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartNewGame(4);
        }

        private void fiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartNewGame(5);
        }

        private void showResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var results = new ResultsForm();
            results.ShowDialog();
        }

        private bool IsGameOver()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (labelsMap[i, j].Text == string.Empty)
                    {
                        return false;
                    }
                }
            }

            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    var currentNumber = labelsMap[i, j].Text;

                    if (j < mapSize - 1 && currentNumber == labelsMap[i, j + 1].Text)
                    {
                        return false;
                    }

                    if (i < mapSize - 1 && currentNumber == labelsMap[i + 1, j].Text)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private int GetHighScore()
        {
            var users = UserStorage.GetUsersResults();

            highScore = users[0].Score;

            for (int k = 0; k < users.Count; k++)
            {
                if (users[k].Score > highScore)
                {
                    highScore = users[k].Score;
                }
            }
            return highScore;
        }

        private void StartNewGame(int size)
        {
            mapSize = size;
            score = 0;
            isGameOver = false;

            InitMap();
            GenerateNumber();
            ShowScore();
        }

        private Color GetNumberColor(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Color.FromArgb(201,212,212);
            }

            int number = Int32.Parse(value);

            switch (number)
            {
                case 2:
                    return Color.FromArgb(192, 220, 193);
                case 4:
                    return Color.FromArgb(160, 206, 164);
                case 8:
                    return Color.FromArgb(167, 207, 120);
                case 16:
                    return Color.FromArgb(123, 194, 126);
                case 32:
                    return Color.FromArgb(95, 184, 101);
                case 64:
                    return Color.FromArgb(70, 170, 77);
                case 128:
                    return Color.FromArgb(115, 175, 62);
                case 256:
                    return Color.FromArgb(63, 156, 70);
                case 512:
                    return Color.FromArgb(46, 150, 65);
                case 1024:
                    return Color.FromArgb(45, 141, 63);
                case 2048:
                    return Color.FromArgb(44, 122, 58);
                default:
                    return Color.FromArgb(49, 103, 46);
            }
        }

        private void ChangeColor(int numberRow,int numberColumn)
        {
            string value=labelsMap[numberRow,numberColumn].Text;
            labelsMap[numberRow,numberColumn].BackColor=GetNumberColor(value);
        }
    }
}
