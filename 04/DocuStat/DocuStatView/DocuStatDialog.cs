using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ELTE.DocuStat.Model;
using ELTE.DocuStat.Persistence;

namespace ELTE.DocuStat.View
{
    public partial class DocuStatDialog : Form
    {
        private IDocumentStatistics? _documentStatistics;

        private readonly string _prefixCharacters;
        private readonly string _prefixNonWhitespaceCharacters;
        private readonly string _prefixSentences;
        private readonly string _prefixProperNouns;
        private readonly string _prefixColemanLiauIndex;
        private readonly string _prefixFleschReadingEase;

        public DocuStatDialog()
        {
            InitializeComponent();

            openFileDialogMenuItem.Click += OpenDialog;
            resetFilterMenuItem.Click += ResetFilter;

            spinBoxMinLength.ValueChanged += UpdateWordFilter;
            spinBoxMinOccurrence.ValueChanged += UpdateWordFilter;
            textBoxIgnoredWords.TextChanged += UpdateWordFilter;

            _prefixCharacters = labelCharacters.Text;
            _prefixNonWhitespaceCharacters = labelNonWhitespaceCharacters.Text;
            _prefixSentences = labelSentences.Text;
            _prefixProperNouns = labelProperNouns.Text;
            _prefixColemanLiauIndex = labelColemanLiauIndex.Text;
            _prefixFleschReadingEase = labelFleschReadingEase.Text;
        }

        private void OpenDialog(object? sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.CurrentDirectory;
                openFileDialog.Filter = "txt files (*.txt)|*.txt|PDF files (*.pdf)|*.pdf";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    IFileManager? filemanager = FileManagerFactory.CreateForPath(openFileDialog.FileName);

                    if (filemanager == null)
                    {
                        MessageBox.Show("Unsupported File Format!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    try
                    {
                        _documentStatistics = new DocumentStatistics(filemanager);
                        _documentStatistics.FileContentReady += UpdateFileContent;
                        _documentStatistics.TextStatisticsReady += UpdateTextStatistics;
                        _documentStatistics.FilteredWordCountChanged += UpdateWordList;

                        // The new model has to take over the filter already set on the user interface.
                        UpdateWordFilter(this, EventArgs.Empty);

                        _documentStatistics.Load();
                    }
                    catch (FileManagerException ex)
                    {
                        MessageBox.Show("File reading is unsuccessful!\n" + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Passes the filter set on the user interface to the model.
        /// </summary>
        /// <remarks>
        /// All three properties are assigned, but the model raises an event only for the
        /// values that actually changed, so the list is not rebuilt needlessly.
        /// </remarks>
        private void UpdateWordFilter(object? sender, EventArgs e)
        {
            if (_documentStatistics == null)
                return; // no document has been loaded yet

            _documentStatistics.MinWordLength = Convert.ToInt32(spinBoxMinLength.Value);
            _documentStatistics.MinWordOccurrence = Convert.ToInt32(spinBoxMinOccurrence.Value);
            _documentStatistics.IgnoredWords = textBoxIgnoredWords.Text.Split(',');
        }

        private void ResetFilter(object? sender, EventArgs e)
        {
            if (_documentStatistics == null)
            {
                MessageBox.Show("No file is loaded!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            spinBoxMinLength.Value = spinBoxMinLength.Minimum;
            spinBoxMinOccurrence.Value = spinBoxMinOccurrence.Minimum;
            textBoxIgnoredWords.Text = string.Empty;
        }

        private void UpdateFileContent(object? sender, EventArgs e)
        {
            if (_documentStatistics?.FileContent == textBox.Text)
                return; // the content has not changed

            textBox.Text = _documentStatistics?.FileContent;
            listBoxCounter.Items.Clear();
        }

        private void UpdateTextStatistics(object? sender, TextStatisticsReadyEventArgs e)
        {
            labelCharacters.Text = $"{_prefixCharacters} {e.CharacterCount}";
            labelNonWhitespaceCharacters.Text = $"{_prefixNonWhitespaceCharacters} {e.NonWhiteSpaceCharacterCount}";
            labelSentences.Text = $"{_prefixSentences} {e.SentenceCount}";
            labelProperNouns.Text = $"{_prefixProperNouns} {e.ProperNounCount}";
            labelColemanLiauIndex.Text = $"{_prefixColemanLiauIndex} {e.ColemanLiauIndex:F2}";
            labelFleschReadingEase.Text = $"{_prefixFleschReadingEase} {e.FleschReadingEase:F2}";
        }

        private void UpdateWordList(object? sender, EventArgs e)
        {
            if (_documentStatistics == null)
                return;

            listBoxCounter.Items.Clear();
            listBoxCounter.BeginUpdate();
            foreach (var pair in _documentStatistics.FilteredWordCount)
            {
                listBoxCounter.Items.Add(pair.Key + ": " + pair.Value);
            }
            listBoxCounter.EndUpdate();
        }
    }
}
