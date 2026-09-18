using System;
using System.Collections.Generic;
using System.Text;

namespace DocuStat.Model
{
    public class DocumentStatistics : IDocumentStatistics
    {
        readonly string _filePath;
        readonly Dictionary<string, int> _distinctWordCount = new Dictionary<string, int>();
        IReadOnlyCollection<string> _ignoredWords = new List<string>();
        public string FileContent { get; set; }
        public int MinWordLength { get; set; }
        public int MinWordOccurence { get; set; }
        public IReadOnlyCollection<string> IgnoredWords
        {
            get => _ignoredWords;
            set => _ignoredWords = value.Select(w => w.Trim().ToLower()).Where(w => w.Length > 0).ToList();
        }
        public IReadOnlyDictionary<string, int> DistinctWordCount => _distinctWordCount;

        public IEnumerable<KeyValuePair<string,int>> FilteredWordCount => 
            DistinctWordCount.Where(p => p.Value > MinWordOccurence)
            .Where(p => p.Key.Length > MinWordLength)
            .Where(p => !IgnoredWords
            .Contains(p.Key))
            .OrderByDescending(p=>p.Value);

        public DocumentStatistics(string filePath)
        {
            _filePath = filePath;
            FileContent = string.Empty;
            MinWordLength = 1;
            MinWordOccurence = 1;
        }

        public void Load()
        {
            FileContent = File.ReadAllText(_filePath);
            ComputeDistinctWords();
        }

        private void ComputeDistinctWords()
        {
            _distinctWordCount.Clear();
            var words = FileContent.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();

            for (int i = 0; i < words.Length; i++)
            {
                words[i] = string.Concat(
                        words[i].TakeWhile(c => !char.IsLetter(c))
                        .TakeWhile(c => char.IsLetter(c))
                    );

                if (string.IsNullOrEmpty(words[i])) continue;

                words[i] = words[i].ToLower();

                if (_distinctWordCount.ContainsKey(words[i]))
                {
                    _distinctWordCount[words[i]]++;
                }
                else
                {
                    _distinctWordCount.Add(words[i], 1);
                }
            }
        }
    }
}
