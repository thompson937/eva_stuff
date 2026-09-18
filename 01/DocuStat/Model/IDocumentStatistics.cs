using System;
using System.Collections.Generic;
using System.Text;

namespace DocuStat.Model
{
    public interface IDocumentStatistics
    {
        string FileContent { get; }
        IReadOnlyDictionary<string, int> DistinctWordCount { get; }
        public int MinWordLength { get; set; }
        int MinWordOccurence { get; set; }
        IReadOnlyCollection<string> IgnoredWords { get; set; }

        // output
        IEnumerable<KeyValuePair<string,int>> FilteredWordCount { get; }

        void Load();
    }
}
