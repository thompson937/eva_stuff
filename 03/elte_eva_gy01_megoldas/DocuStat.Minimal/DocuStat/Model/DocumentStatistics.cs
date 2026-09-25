using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ELTE.DocuStat.Model
{
    public class DocumentStatistics : IDocumentStatistics
    {
        #region Fields

        private readonly string _filePath;

        private readonly Dictionary<string, int> _distinctWordCount = new Dictionary<string, int>();

        private IReadOnlyCollection<string> _ignoredWords = new List<string>();

        #endregion

        #region Properties

        public string FileContent { get; private set; }

        /// <summary>
        /// The number of occurrences of each distinct word in the whole document.
        /// </summary>
        /// <remarks>
        /// The filtered view of it is the FilteredWordCount property.
        /// </remarks>
        public IReadOnlyDictionary<string, int> DistinctWordCount => _distinctWordCount;

        /// <summary>
        /// Minimum length of a word to be included in the filtered statistics.
        /// </summary>
        public int MinWordLength { get; set; }

        /// <summary>
        /// Minimum occurrence of a word to be included in the filtered statistics.
        /// </summary>
        public int MinWordOccurrence { get; set; }

        /// <summary>
        /// The words excluded from the filtered statistics.
        /// </summary>
        /// <remarks>
        /// The words are normalized (trimmed and lowercased) upon assignment,
        /// so the caller does not have to prepare them.
        /// </remarks>
        public IReadOnlyCollection<string> IgnoredWords
        {
            get => _ignoredWords;
            set => _ignoredWords = value.Select(w => w.Trim().ToLower())
                                        .Where(w => w.Length > 0)
                                        .ToList();
        }

        /// <summary>
        /// The word occurrences matching the current filter, ordered by occurrence.
        /// </summary>
        public IEnumerable<KeyValuePair<string, int>> FilteredWordCount =>
            DistinctWordCount
                .Where(p => p.Value >= MinWordOccurrence)
                .Where(p => p.Key.Length >= MinWordLength)
                .Where(p => !IgnoredWords.Contains(p.Key))
                .OrderByDescending(p => p.Value);

        #endregion

        #region Constructors

        public DocumentStatistics(string filePath)
        {
            _filePath = filePath;
            FileContent = string.Empty;
            MinWordLength = 1;
            MinWordOccurrence = 1;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Loads and processes the file.
        /// </summary>
        public void Load()
        {
            FileContent = File.ReadAllText(_filePath);

            ComputeDistinctWords();
        }

        #endregion

        #region Private methods

        private void ComputeDistinctWords()
        {
            _distinctWordCount.Clear();
            string[] words = FileContent
                .Split()
                .Where(s => s.Length > 0)
                .ToArray();

            for (int i = 0; i < words.Length; ++i)
            {
                // Remove leading and trailing non letter characters
                words[i] = string.Concat(
                    words[i]
                        .SkipWhile(c => !char.IsLetter(c))
                        .Reverse()
                        .SkipWhile(c => !char.IsLetter(c))
                        .Reverse()
                );

                // Removing all characters from an all non-letter word results in an empty string.
                if (string.IsNullOrEmpty(words[i]))
                {
                    continue;
                }

                words[i] = words[i].ToLower();

                // Add word to dictionary or increment its value.
                if (_distinctWordCount.ContainsKey(words[i]))
                {
                    ++_distinctWordCount[words[i]];
                }
                else
                {
                    _distinctWordCount.Add(words[i], 1);
                }
            }
        }

        #endregion
    }
}
