namespace ButtonHunt
{
    public partial class Form1 : Form
    {
        int points = 0;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        DateTime startTime;
        Random rand = new Random(DateTime.Now.Millisecond);

        public Form1()
        {
            InitializeComponent();

            timer.Interval = 1000;
            timer.Tick += UpdateStatusbar;

            pushButton.Text = "\x20AC";
        }

        private void UpdateStatusbar(object? sender, EventArgs e)
        {
            double elapsed = (DateTime.Now - startTime).TotalSeconds;

            statusLabel.Text = $"Points: {points} | Elapsed time: {elapsed:F0} sec";
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            int x = ClientSize.Width - pushButton.Width;
            int y = ClientSize.Height - pushButton.Height - statusStrip.Height;

            pushButton.Location = new Point(rand.Next(x), rand.Next(y));

            if (!timer.Enabled)
            {
                startTime = DateTime.Now;
                timer.Start();
                Form1.ActiveForm.Cursor = Cursors.Cross;
            }
            else { points++; }

            UpdateStatusbar(sender, e);
        }

        private void GameClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && timer.Enabled)
            {
                double elapsed = (DateTime.Now - startTime).TotalSeconds;
                double pushPerSec = points / elapsed;

                MessageBox.Show($"Push per seconds: {pushPerSec:F2}", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
