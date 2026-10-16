using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ELTE.DocuStat.Model;
using ELTE.DocuStat.Persistence;

namespace ELTE.DocuStat.View
{
    public partial class DocuStatControl : UserControl
    {
        private IDocumentStatistics? _documentStatistics;

        private readonly string _prefixCharacters;
        private readonly string _prefixNonWhitespaceCharacters;
        private readonly string _prefixSentences;
        private readonly string _prefixProperNouns;
        private readonly string _prefixColemanLiauIndex;
        private readonly string _prefixFleschReadingEase;

        public DocuStatControl()
        {
            InitializeComponent();

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

        public async Task LoadFileAsync(IFileManager fileManager)
        {
            try
            {
                _documentStatistics = new DocumentStatistics(fileManager);
                _documentStatistics.FileContentReady += UpdateFileContent;
                _documentStatistics.TextStatisticsReady += UpdateTextStatistics;
                _documentStatistics.FilteredWordCountChanged += UpdateWordList;

                // The new model has to take over the filter already set on the user interface.
                UpdateWordFilter(this, EventArgs.Empty);

                progressBar.Visible = true;
                await _documentStatistics.LoadAsync();
                progressBar.Visible = false;
            }
            catch (FileManagerException ex)
            {
                MessageBox.Show("File reading is unsuccessful!\n" + ex.Message,
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void UpdateWordFilter(object? sender, EventArgs e)
        {
            if (_documentStatistics == null)
                return; // no document has been loaded yet

            _documentStatistics.MinWordLength = Convert.ToInt32(spinBoxMinLength.Value);
            _documentStatistics.MinWordOccurrence = Convert.ToInt32(spinBoxMinOccurrence.Value);
            _documentStatistics.IgnoredWords = textBoxIgnoredWords.Text.Split(',');
        }

        public void ResetFilter()
        {
            spinBoxMinLength.Value = spinBoxMinLength.Minimum;
            spinBoxMinOccurrence.Value = spinBoxMinOccurrence.Minimum;
            textBoxIgnoredWords.Text = string.Empty;
        }

        private void UpdateFileContent(object? sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke( () => UpdateFileContent(sender, e) );
                return;
            }

            if (_documentStatistics?.FileContent == textBox.Text)
                return; // the content has not changed

            textBox.Text = _documentStatistics?.FileContent;
            listBoxCounter.Items.Clear();
        }

        private void UpdateTextStatistics(object? sender, TextStatisticsReadyEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(() => UpdateTextStatistics(sender, e));
                return;
            }

            labelCharacters.Text = $"{_prefixCharacters} {e.CharacterCount}";
            labelNonWhitespaceCharacters.Text = $"{_prefixNonWhitespaceCharacters} {e.NonWhiteSpaceCharacterCount}";
            labelSentences.Text = $"{_prefixSentences} {e.SentenceCount}";
            labelProperNouns.Text = $"{_prefixProperNouns} {e.ProperNounCount}";
            labelColemanLiauIndex.Text = $"{_prefixColemanLiauIndex} {e.ColemanLiauIndex:F2}";
            labelFleschReadingEase.Text = $"{_prefixFleschReadingEase} {e.FleschReadingEase:F2}";
        }

        private void UpdateWordList(object? sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(() => UpdateWordList(sender, e));
                return;
            }

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
