using ELTE.DocuStat.Model;

namespace DocuStatView
{
    public partial class Form1 : Form
    {
        IDocumentStatistics? _documentStatistics;

        readonly string _prefixCharacters;
        readonly string _prefixNonWhitespaceCharacters;
        readonly string _prefixSentences;
        readonly string _prefixProperNouns;
        readonly string _prefixColemanIndex;
        readonly string _prefixFlesch;

        public Form1()
        {
            InitializeComponent();

            OpenFileDialogMenuItem.Click += OpenDialog;
            ResetFilterMenuItem.Click += ResetFilter;
            ExitMenuItem.Click += ExitMenu;
            spinBoxMinLength.ValueChanged += UpdateWordFilter;
            spinBoxMinOccur.ValueChanged += UpdateWordFilter;
            textBoxIgnoredWords.TextChanged += UpdateWordFilter;

            _prefixCharacters = labelChar.Text;
            // [...]
        }

        private void UpdateWordFilter(object? sender, EventArgs e)
        {
            _documentStatistics?.MinWordLength = Convert.ToInt32(spinBoxMinLength.Value);
            _documentStatistics?.MinWordOccurrence = Convert.ToInt32(spinBoxMinOccur.Value);
            _documentStatistics?.IgnoredWords = textBoxIgnoredWords.Text.Split(',');
        }

        private void ExitMenu(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ResetFilter(object? sender, EventArgs e)
        {
            
        }

        private void OpenDialog(object? sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = Environment.CurrentDirectory;
                ofd.RestoreDirectory = true;
                ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _documentStatistics = new DocumentStatistics(ofd.FileName);
                        _documentStatistics.FileContentReady += UpdateFileContent;
                        _documentStatistics.TextStatisticsReady += UpdateTextStatistics;
                        _documentStatistics.FilteredWordCountChanged += UpdateWordList;

                        UpdateWordFilter(this, EventArgs.Empty);
                        _documentStatistics.Load();
                    }
                    catch (IOException ex)
                    {

                        MessageBox.Show("File read error:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void UpdateWordList(object? sender, EventArgs e)
        {
            if (_documentStatistics == null)
            {
                return;
            }

            listBoxCounter.Items.Clear();
            listBoxCounter.BeginUpdate();

            foreach (var item in _documentStatistics.FilteredWordCount)
            {
                listBoxCounter.Items.Add(item.Key + ": " + item.Value);
            }

            listBoxCounter.EndUpdate();
        }

        private void UpdateTextStatistics(object? sender, TextStatisticsReadyEventArgs e)
        {
            labelChar.Text = $"{_prefixCharacters} {e.CharacterCount}";
            // [...]
        }

        private void UpdateFileContent(object? sender, EventArgs e)
        {
            if (_documentStatistics?.FileContent == textBox.Text)
            {
                return;
            }

            textBox.Text = _documentStatistics?.FileContent;
            listBoxCounter.Items.Clear();
        }
    }
}
