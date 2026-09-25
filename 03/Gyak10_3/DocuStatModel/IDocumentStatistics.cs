using System.Collections.Generic;

namespace ELTE.DocuStat.Model
{
    public interface IDocumentStatistics
    {
        string FileContent { get; }
        IReadOnlyDictionary<string, int> DistinctWordCount { get; }
		
        int CharacterCount { get; }
        int NonWhiteSpaceCharacterCount { get;}
        int SentenceCount { get; }
        int ProperNounCount { get; }
        double ColemanLiauIndex { get; }
        double FleschReadingEase { get; }

        int MinWordLength { get; set; }
        int MinWordOccurrence { get; set; }
        IReadOnlyCollection<string> IgnoredWords { get; set; }

        IEnumerable<KeyValuePair<string, int>> FilteredWordCount { get; }

        public event EventHandler? FileContentReady;
        public event EventHandler<TextStatisticsReadyEventArgs>? TextStatisticsReady;
        public event EventHandler? FilteredWordCountChanged;

        void Load();
    }
}
