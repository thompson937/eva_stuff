using System.Collections.Generic;

namespace ELTE.DocuStat.Model
{
    public interface IDocumentStatistics
    {
        string FileContent { get; }
        IReadOnlyDictionary<string, int> DistinctWordCount { get; }

        int MinWordLength { get; set; }
        int MinWordOccurrence { get; set; }
        IReadOnlyCollection<string> IgnoredWords { get; set; }

        IEnumerable<KeyValuePair<string, int>> FilteredWordCount { get; }

        void Load();
    }
}
