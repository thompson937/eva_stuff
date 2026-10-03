namespace Grid
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int cellNumber = 3;

        private void radioButton_Click(object sender, EventArgs e)
        {
            var rb = sender as RadioButton;

            cellNumber = Convert.ToInt32(rb!.Tag!.ToString());
        }

        List<Button> buttons = new List<Button>();
        int cellH, cellW;

        private void button1_Click(object sender, EventArgs e)
        {
            buttons.Clear();
            panel1.Controls.Clear();

            cellH = panel1.Height / cellNumber;
            cellW = panel1.Width / cellNumber;

            for (int i = 0; i < cellNumber; i++)
            {
                for (int j = 0; j < cellNumber; j++)
                {
                    Button b = new Button { Width = cellW, Height = cellH };
                    b.Location = new Point(j * cellW, i * cellH);
                    b.Click += B_Click;
                    b.MouseEnter += B_Enter;
                    buttons.Add(b);
                    panel1.Controls.Add(b);
                }
            }
        }

        private void B_Enter(object? sender, EventArgs e)
        {
            var b = sender as Button;

            b!.BackColor = Color.RebeccaPurple;
        }

        private void B_Click(object? sender, EventArgs e)
        {
            var b = sender as Button;
            b!.BackColor = Color.Black;
        }
    }
}
