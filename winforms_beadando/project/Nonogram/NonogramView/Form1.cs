using NonogramModel.Model;
using NonogramModel.Persistence;

namespace NonogramView
{
    public partial class Form1 : Form
    {
        INonogram? _nonogramModel;
        int _boardSize;
        int _btnSize;
        bool _gamePaused;

        List<Button> _buttons = new();
        List<Label> _rowLabels = new();
        List<Label> _columnLabels = new();


        public Form1()
        {
            InitializeComponent();

            openStripMenuItem.Click += LoadGameDialog;
            saveStripMenuItem.Click += SaveGameDialog;

            _gamePaused = false;
        }

        #region Toolbar event handlers

        private void SaveGameDialog(object? sender, EventArgs e)
        {
            using (SaveFileDialog sf = new SaveFileDialog())
            {
                sf.InitialDirectory = Environment.CurrentDirectory;
                sf.Filter = "Nonogram Save Files (*.ngm)|*.ngm";
                sf.FileName = "new_save.ngm";
                sf.RestoreDirectory = true;

                if (sf.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        INonogramFileManager fm = new NonogramFileManager(sf.FileName);
                        _nonogramModel!.SaveGame(fm);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occured:\n:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void LoadGameDialog(object? sender, EventArgs e)
        {
            using (OpenFileDialog of = new OpenFileDialog())
            {
                of.InitialDirectory = Environment.CurrentDirectory;
                of.Filter = "Nonogram Save Files (*.ngm)|*.ngm";
                of.RestoreDirectory = true;

                if (of.ShowDialog() == DialogResult.OK)
                {
                    if (System.IO.Path.GetExtension(of.FileName) != ".ngm")
                    {
                        MessageBox.Show("Unsupported file Format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    try
                    {
                        INonogramFileManager fm = new NonogramFileManager(of.FileName);
                        NonogramFileData data = fm.Load();
                        fm.Dispose();

                        _initialiseGame(data);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occured:\n:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }

        private void NewGamePredefined(object sender, EventArgs e)
        {
            var stripItem = sender as ToolStripItem;

            if (stripItem != null && stripItem.Tag != null)
            {
                INonogramFileManager fm = new NonogramFileManager("Input\\" + stripItem.Tag + ".ngm");
                NonogramFileData data = fm.Load();
                fm.Dispose();

                _initialiseGame(data);
            }
        }

        private void NewGameRandom(object sender, EventArgs e)
        {
            var stripItem = sender as ToolStripItem;

            if (stripItem != null && stripItem.Tag != null)
            {
                int boardSize;
                int.TryParse(stripItem.Tag.ToString(), out boardSize);

                int[] randomBoard = new int[boardSize * boardSize];

                Random random = new Random();

                for (int i = 0; i < randomBoard.Length; i++)
                {
                    randomBoard[i] = random.Next(2);
                }
                NonogramFileData data = new NonogramFileData(randomBoard, 3, 0, boardSize);

                _initialiseGame(data);
            }
        }

        private void ExitGame(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        #endregion

        #region Model incoming event handlers

        private void OnColumnCleared(object? sender, int e)
        {
            _columnLabels[e].ForeColor = SystemColors.ScrollBar;
        }

        private void OnRowCleared(object? sender, int e)
        {
            _rowLabels[e].ForeColor = SystemColors.ScrollBar;
        }

        private void OnGameLost(object? sender, EventArgs e)
        {
            gameTimer.Stop();
            playPauseButton.Enabled = false;
            saveStripMenuItem.Enabled = false;

            foreach (var btn in _buttons)
            {
                btn.Enabled = false;
            }


            MessageBox.Show("You have lost!", "Game Lost", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void OnGameWon(object? sender, EventArgs e)
        {
            gameTimer.Stop();
            playPauseButton.Enabled = false;
            saveStripMenuItem.Enabled = false;

            MessageBox.Show("You've WON!", "Game Won", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OnLivesSet(object? sender, int e)
        {
            liveCounterLabel.Text = e.ToString();
        }

        private void OnCellChanged(object? sender, CellChangeEventArgs e)
        {
            int i = e.y * _boardSize + e.x;

            SetButtonAppearance(i, e.category);
        }

        private void SetButtonAppearance(int i, CellCategory category)
        {
            switch (category)
            {
                case CellCategory.EXCLUSION:
                    _buttons[i].Text = "X";
                    _buttons[i].BackColor = Color.White;
                    break;
                case CellCategory.SELECTION:
                    _buttons[i].Text = String.Empty;
                    _buttons[i].BackColor = Color.Black;
                    _buttons[i].Enabled = false;
                    break;
                case CellCategory.PERMANENT_EXCLUSION:
                    _buttons[i].Text = "X";
                    _buttons[i].BackColor = Color.White;
                    _buttons[i].Enabled = false;
                    break;
                case CellCategory.EMPTY:
                    _buttons[i].Text = String.Empty;
                    _buttons[i].BackColor = Color.White;
                    break;
                default:
                    break;
            }
        }

        private void OnModelReady(object? sender, BoardInitialArgs e)
        {
            _nonogramModel!.CellChanged += OnCellChanged;
            _nonogramModel!.LivesSet += OnLivesSet;
            _nonogramModel!.LoseCondition += OnGameLost;
            _nonogramModel!.WinCondition += OnGameWon;

            _nonogramModel!.RowCleared += OnRowCleared;
            _nonogramModel!.ColumnCleared += OnColumnCleared;

            _buttons.Clear();
            _rowLabels.Clear();
            _columnLabels.Clear();

            buttonPanel.Controls.Clear();
            rowPanel.Controls.Clear();
            columnPanel.Controls.Clear();

            NonogramFileData data = e.data;

            liveCounterLabel.Text = data.Lives.ToString();
            timeCounterLabel.Text = $"{data.ElapsedTime / 60}:{data.ElapsedTime % 60}";

            _boardSize = data.BoardSize;

            _btnSize = buttonPanel.Height / _boardSize;

            gameTimer.Start();
            playPauseButton.Enabled = true;
            saveStripMenuItem.Enabled = true;

            // Button Panel Setup
            for (int y = 0; y < _boardSize; y++)
            {
                for (int x = 0; x < _boardSize; x++)
                {
                    int i = y * _boardSize + x;

                    Button btn = new Button
                    {
                        Width = _btnSize,
                        Height = _btnSize,
                        Location = new Point(x * _btnSize, y * _btnSize),
                        Font = new Font("Consolas", _btnSize / 2, FontStyle.Bold, GraphicsUnit.Pixel),
                        Tag = $"{x}:{y}",
                        Capture = false
                    };

                    btn.MouseDown += BoardButton_Click;
                    btn.MouseEnter += BoardButton_Drag;
                    _buttons.Add(btn);
                    buttonPanel.Controls.Add(btn);

                    bool selected = (data.Board[i] & 0b0010) != 0;
                    bool excluded = (data.Board[i] & 0b0100) != 0;
                    bool incorrect = (data.Board[i] & 0b1000) != 0;

                    if (excluded) SetButtonAppearance(i, CellCategory.EXCLUSION);
                    if (incorrect) SetButtonAppearance(i, CellCategory.PERMANENT_EXCLUSION);
                    if (selected) SetButtonAppearance(i, CellCategory.SELECTION);

                }
            }

            // Row Setup
            for (int y = 0; y < _boardSize; y++)
            {
                Label lbl = new Label
                {
                    AutoSize = true,
                    Location = new Point(0, y * _btnSize),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Tag = y.ToString(),
                    Font = new Font("Consolas", _btnSize / 2, FontStyle.Bold, GraphicsUnit.Pixel),
                    Text = e.rowStrings[y]
                };

                _rowLabels.Add(lbl);
                rowPanel.Controls.Add(lbl);
            }

            // Column Setup
            for (int x = 0; x < _boardSize; x++)
            {
                Label lbl = new Label
                {
                    AutoSize = true,
                    Location = new Point(x * _btnSize, 0),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Tag = x.ToString(),
                    Font = new Font("Consolas", _btnSize / 2, FontStyle.Bold, GraphicsUnit.Pixel),
                    Text = e.columnStrings[x]
                };

                _columnLabels.Add(lbl);
                columnPanel.Controls.Add(lbl);
            }

            data.Dispose();
        }

        #endregion

        #region View-related event handlers

        private void BoardButton_Click(object? sender, MouseEventArgs e)
        {
            var btn = sender as Button;
            string[] tag = btn!.Tag!.ToString()!.Split(':');

            int x, y;
            int.TryParse(tag[0], out x);
            int.TryParse(tag[1], out y);

            if (e.Button == MouseButtons.Left)
            {
                _nonogramModel!.SetBoard(x, y, CellCategory.SELECTION);
            }
            else if (e.Button == MouseButtons.Right)
            {
                _nonogramModel!.SetBoard(x, y, CellCategory.EXCLUSION);
            }
        }

        private void BoardButton_Drag(object? sender, EventArgs e)
        {
            var btn = sender as Button;
            string[] tag = btn!.Tag!.ToString()!.Split(':');

            int x, y;
            int.TryParse(tag[0], out x);
            int.TryParse(tag[1], out y);

            if (Control.MouseButtons == MouseButtons.Left)
            {
                _nonogramModel!.SetBoard(x, y, CellCategory.SELECTION);
            }

            if (Control.MouseButtons == MouseButtons.Right)
            {
                _nonogramModel!.SetBoard(x, y, CellCategory.EXCLUSION);
            }
        }

        private void OnGameTimerTick(object sender, EventArgs e)
        {
            _nonogramModel!.ElapsedTime++;
            int seconds = _nonogramModel.ElapsedTime % 60;
            int minutes = _nonogramModel.ElapsedTime / 60;

            timeCounterLabel.Text = $"{minutes.ToString("00")}:{seconds.ToString("00")}";
        }

        private void OnGamePaused(object sender, EventArgs e)
        {
            if (_gamePaused)
            {
                _gamePaused = false;
                gameTimer.Enabled = true;

                playPauseButton.Text = "Pause";

                foreach (var btn in _buttons) btn.Show();
                foreach (var lbl in _rowLabels) lbl.Show();
                foreach (var lbl in _columnLabels) lbl.Show();

            }
            else
            {
                _gamePaused = true;
                gameTimer.Enabled = false;

                playPauseButton.Text = "Play";

                foreach (var btn in _buttons) btn.Hide();
                foreach (var lbl in _rowLabels) lbl.Hide();
                foreach (var lbl in _columnLabels) lbl.Hide();
            }
        }
        #endregion

        #region Private Methods
        private void _initialiseGame(NonogramFileData data)
        {
            if (_nonogramModel != null)
            {
                _nonogramModel.Dispose();
                _nonogramModel = null;
            }

            _nonogramModel = new Nonogram(data);
            _nonogramModel.ModelReady += OnModelReady;
            _nonogramModel.Initialise(data);
        }

        #endregion

    }
}
