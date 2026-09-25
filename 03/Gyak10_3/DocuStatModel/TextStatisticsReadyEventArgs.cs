namespace ELTE.DocuStat.Model
{
    public class TextStatisticsReadyEventArgs : EventArgs
    {
        public int CharacterCount { get; }
        public int NonWhitespaceCharacterCount { get; }
        public int SentenceCount { get; }
        public int ProperNounCount { get; }
        public double ColemanLieauIndex { get; }
        public double FleschReadingEase { get; }

        public TextStatisticsReadyEventArgs(int characterCount, int nonWhite, int sentence, int properNoun, double coleman, double flesch)
        {
            CharacterCount = characterCount;
            NonWhitespaceCharacterCount = nonWhite;
            SentenceCount = sentence;
            ProperNounCount = properNoun;
            ColemanLieauIndex = coleman;
            FleschReadingEase = flesch;
        }
    }
}