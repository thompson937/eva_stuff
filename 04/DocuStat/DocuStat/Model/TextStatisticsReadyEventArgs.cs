using System;

namespace ELTE.DocuStat.Model
{
    /// <summary>
    /// Holds the computed text metrics of a document.
    /// </summary>
    public class TextStatisticsReadyEventArgs : EventArgs
    {
        #region Properties

        public int CharacterCount { get; }
        public int NonWhiteSpaceCharacterCount { get; }
        public int SentenceCount { get; }
        public int ProperNounCount { get; }
        public double ColemanLiauIndex { get; }
        public double FleschReadingEase { get; }

        #endregion

        #region Constructors

        public TextStatisticsReadyEventArgs(int characterCount,
                                            int nonWhiteSpaceCharacterCount,
                                            int sentenceCount,
                                            int properNounCount,
                                            double colemanLiauIndex,
                                            double fleschReadingEase)
        {
            CharacterCount = characterCount;
            NonWhiteSpaceCharacterCount = nonWhiteSpaceCharacterCount;
            SentenceCount = sentenceCount;
            ProperNounCount = properNounCount;
            ColemanLiauIndex = colemanLiauIndex;
            FleschReadingEase = fleschReadingEase;
        }

        #endregion
    }
}
