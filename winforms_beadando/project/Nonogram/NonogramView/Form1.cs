using NonogramModel;
using NonogramModel.Model;

namespace NonogramView
{
    public partial class Form1 : Form
    {
        private IGameState? _gamestate;

        private int boardSize;

        public Form1()
        {
            InitializeComponent();

            openStripMenuItem.Click += OpenSaveFileDialog;
            saveStripMenuItem.Click += SaveSaveFileDialog;
            newStripMenuItem.Click += NewSaveFileDialog;
        }

        List<Button> buttons = new();
        List<Label> rowLabels = new();
        List<Label> columnLabels = new();
        int btnSize;

        bool gamePaused = false;

        private void NewSaveFileDialog(object? sender, EventArgs e)
        {
            if (_gamestate != null)
            {
                (_gamestate as IDisposable)?.Dispose();
            }

            boardSize = 5;

            bool[] testImage =
            {
                false, false, true, false, false,
                false, true, false, true, false,
                true, false, false, false, true,
                true, true, false, true, true,
                false, false, true, false, false
            };

            playPauseButton.Enabled = true;

            gameTimer.Start();
            timeCounterLabel.Text = "00:00";
            liveCounterLabel.Text = "3";

            _gamestate = new GameState(boardSize, testImage);
            _gamestate.CellChanged += OnCellChanged;
            _gamestate.LivesSet += OnLivesSet;
            _gamestate.LoseCondition += OnGameLost;
            _gamestate.WinCondition += OnGameWon;

            _gamestate.RowCleared += OnRowCleared;
            _gamestate.ColumnCleared += OnColumnCleared;

            buttons.Clear();
            rowLabels.Clear();
            columnLabels.Clear();

            buttonPanel.Controls.Clear();
            rowPanel.Controls.Clear();
            columnPanel.Controls.Clear();

            btnSize = buttonPanel.Height / boardSize;


            // Button Panel Setup
            for (int y = 0; y < boardSize; y++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    Button btn = new Button { Width = btnSize, Height = btnSize };
                    btn.Location = new Point(x * btnSize, y * btnSize);
                    btn.Tag = $"{x}:{y}";

                    btn.MouseDown += BoardBtnClick;
                    buttons.Add(btn);
                    buttonPanel.Controls.Add(btn);
                }
            }

            // Row Setup
            for (int y = 0; y < _gamestate.boardSize; y++)
            {
                Label lbl = new Label
                {
                    AutoSize = true,
                    Location = new Point(0, y * btnSize),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Tag = y.ToString(),
                    Font = new Font("Consolas", btnSize / 2, FontStyle.Bold, GraphicsUnit.Pixel),
                    Text = _gamestate.GetRowNumbers(y)
                };

                rowLabels.Add(lbl);
                rowPanel.Controls.Add(lbl);
            }

            // Column Setup
            for (int x = 0; x < _gamestate.boardSize; x++)
            {
                Label lbl = new Label
                {
                    AutoSize = true,
                    Location = new Point(x * btnSize, 0),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Tag = x.ToString(),
                    Font = new Font("Consolas", btnSize / 2, FontStyle.Bold, GraphicsUnit.Pixel),
                    Text = _gamestate.GetColumnNumbers(x)
                };

                columnLabels.Add(lbl);
                columnPanel.Controls.Add(lbl);
            }
        }



        private void OnColumnCleared(object? sender, int e)
        {
            columnLabels[e].ForeColor = SystemColors.ScrollBar;

            for (int y = 0; y < boardSize; y++)
            {
                int i = y * boardSize + e;
                if (buttons[i].Enabled)
                {
                    buttons[i].Enabled = false;
                    _gamestate!.SetBoard(e, y, CellCategory.EXCLUSION);
                }
            }
        }

        private void OnRowCleared(object? sender, int e)
        {
            rowLabels[e].ForeColor = SystemColors.ScrollBar;

            for (int x = 0; x < boardSize; x++)
            {
                int i = e * boardSize + x;
                if (buttons[i].Enabled)
                {
                    buttons[i].Enabled = false;
                    _gamestate!.SetBoard(x, e, CellCategory.EXCLUSION);
                }
            }
        }

        private void OnGameLost(object? sender, EventArgs e)
        {
            gameTimer.Stop();
            playPauseButton.Enabled = false;

            buttons.Clear();
            rowLabels.Clear();
            columnLabels.Clear();

            buttonPanel.Controls.Clear();
            rowPanel.Controls.Clear();
            columnPanel.Controls.Clear();

            MessageBox.Show("You have lost!", "Game Lost", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void OnGameWon(object? sender, EventArgs e)
        {
            gameTimer.Stop();

            playPauseButton.Enabled = false;

            MessageBox.Show("You've WON!", "Game Won", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OnLivesSet(object? sender, int e)
        {
            liveCounterLabel.Text = e.ToString();
        }

        private void OnCellChanged(object? sender, CellChangeEventArgs e)
        {
            int i = e.y * boardSize + e.x;

            if (i >= 0 && i < buttons.Count)
            {
                switch (e.category)
                {
                    case CellCategory.EXCLUSION:
                        buttons[i].BackColor = Color.Gray;
                        break;
                    case CellCategory.SELECTION:
                        buttons[i].BackColor = Color.Black;
                        break;
                    case CellCategory.ORIGINAL:
                        buttons[i].BackColor = Color.Black;
                        break;
                    case CellCategory.INCORRECT:
                        buttons[i].BackColor = Color.Gray;
                        buttons[i].Enabled = false;
                        break;
                    default:
                        break;
                }
            }
        }

        private void BoardBtnClick(object? sender, MouseEventArgs e)
        {
            var btn = sender as Button;
            string[] tag = btn!.Tag!.ToString()!.Split(':');

            int x, y;
            int.TryParse(tag[0], out x);
            int.TryParse(tag[1], out y);

            if (e.Button == MouseButtons.Left)
            {
                _gamestate!.SetBoard(x, y, CellCategory.SELECTION);
            }
            else if (e.Button == MouseButtons.Right)
            {
                _gamestate!.SetBoard(x, y, CellCategory.EXCLUSION);
            }
        }

        private void SaveSaveFileDialog(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OpenSaveFileDialog(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnGameTimerTick(object sender, EventArgs e)
        {
            _gamestate!.TimeCounter++;
            int seconds = _gamestate!.TimeCounter % 60;

            int minutes = _gamestate!.TimeCounter / 60;

            timeCounterLabel.Text = $"{minutes.ToString("00")}:{seconds.ToString("00")}";
        }

        private void OnGamePaused(object sender, EventArgs e)
        {
            if (gamePaused)
            {
                gamePaused = false;
                gameTimer.Enabled = true;

                playPauseButton.Text = "Pause";

                foreach (var btn in buttons) btn.Show();

                foreach (var lbl in rowLabels) lbl.Show();

                foreach (var lbl in columnLabels) lbl.Show();

            }
            else
            {
                gamePaused = true;
                gameTimer.Enabled = false;

                playPauseButton.Text = "Play";

                foreach (var btn in buttons) btn.Hide();

                foreach (var lbl in rowLabels) lbl.Hide();

                foreach (var lbl in columnLabels) lbl.Hide();
            }
        }
    }
}
