using NonogramModel;

namespace NonogramView
{
    public partial class Form1 : Form
    {
        private INonogramModel? _nonogram;

        public Form1()
        {
            InitializeComponent();

            openStripMenuItem.Click += OpenSaveFileDialog;
            saveStripMenuItem.Click += SaveSaveFileDialog;
            newStripMenuItem.Click += NewSaveFileDialog;
        }

        private void NewSaveFileDialog(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
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
