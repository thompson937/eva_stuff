using NonogramModel;
using NonogramModel.GameStates;

namespace NonogramView
{
    public partial class Form1 : Form
    {
        private IGameState? _gamestate;

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

        private void NewSaveFileDialog(object? sender, EventArgs e)
        {
            bool[] testImage =
            {
                false, false, true, false, false,
                false, true, false, true, false,
                true, false, false, false, true,
                true, true, false, true, true,
                false, false, true, false, false
            };


            _gamestate = new GameState(5, testImage);
            _gamestate.BoardChanged += OnBoardChanged;
            buttons.Clear();
            rowLabels.Clear();
            columnLabels.Clear();

            buttonPanel.Controls.Clear();
            rowPanel.Controls.Clear();
            columnPanel.Controls.Clear();

            btnSize = buttonPanel.Height / _gamestate.boardSize;


            // Button Panel Setup
            for (int y = 0; y < _gamestate.boardSize; y++)
            {
                for (int x = 0; x < _gamestate.boardSize; x++)
                {
                    Button btn = new Button { Width = btnSize, Height = btnSize };
                    btn.Location = new Point(x * btnSize, y * btnSize);
                    btn.Tag = $"{x}:{y}";

                    if ((_gamestate.GetBoard(x,y) & 0b001) != 0)
                    {
                        btn.BackColor = Color.Black;
                    }

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

        private void OnBoardChanged(object? sender, EventArgs e)
        {
            foreach (var button in buttons)
            {
                string[] tag = button!.Tag!.ToString()!.Split(':');

                int x, y;
                int.TryParse(tag[0], out x);
                int.TryParse(tag[1], out y);

                int boardState = _gamestate!.GetBoard(x, y);

                if ((boardState & 0b100) != 0)
                {
                    button.BackColor = Color.Gray;
                }
                else if ((boardState & 0b010) != 0)
                {
                    button.BackColor = Color.Black;
                }
                else
                {
                    button.BackColor = Color.White;
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
                _gamestate!.SetBoard(x, y, UserInputType.SELECTION);
            }
            else if (e.Button == MouseButtons.Right)
            {
                _gamestate!.SetBoard(x, y, UserInputType.EXCLUSION);
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
    }
}
