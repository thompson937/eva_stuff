using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ELTE.DocuStat.Model
{
    public interface IDocumentStatistics
    {
        string FileContent { get; }
        IReadOnlyDictionary<string, int> DistinctWordCount { get; }

        int CharacterCount { get; }
        int NonWhiteSpaceCharacterCount { get; }
        int SentenceCount { get; }
        int ProperNounCount { get; }
        double ColemanLiauIndex { get; }
        double FleschReadingEase { get; }

        int MinWordLength { get; set; }
        int MinWordOccurrence { get; set; }
        IReadOnlyCollection<string> IgnoredWords { get; set; }

        IEnumerable<KeyValuePair<string, int>> FilteredWordCount { get; }

        event EventHandler? FileContentReady;
        event EventHandler<TextStatisticsReadyEventArgs>? TextStatisticsReady;
        event EventHandler? FilteredWordCountChanged;

        void Load();
        Task LoadAsync();
    }
}