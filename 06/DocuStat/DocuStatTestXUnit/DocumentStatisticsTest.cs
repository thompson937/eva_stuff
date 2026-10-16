using ELTE.DocuStat.Model;
using ELTE.DocuStat.Persistence;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace ELTE.DocuStat.XTest
{
    public class DocumentStatisticsTest
    {
        #region Fields

        private readonly Mock<IFileManager> _mock;
        private readonly DocumentStatistics _docStats;

        #endregion

        #region Constructor and Utility

        public DocumentStatisticsTest()
        {
            _mock = new Mock<IFileManager>();
            _docStats = new DocumentStatistics(_mock.Object);
        }

        private void MockAndLoadText(string text)
        {
            _mock.Setup(fileManager => fileManager.Load()).Returns(text);
            _docStats.Load();
        }

        #endregion

        #region File Loading

        [Fact]
        public void TestFileLoading()
        {
            bool fileContentReadyRaised = false;
            TextStatisticsReadyEventArgs? statisticsArgs = null;
            _docStats.FileContentReady += (sender, args) => fileContentReadyRaised = true;
            _docStats.TextStatisticsReady += (sender, args) => statisticsArgs = args;

            MockAndLoadText("Hello World");

            Assert.Equal("Hello World", _docStats.FileContent);
            Assert.NotEmpty(_docStats.DistinctWordCount);
            Assert.True(fileContentReadyRaised);

            // The TextStatisticsReady event carries the metrics itself.
            Assert.NotNull(statisticsArgs);
            Assert.Equal(_docStats.CharacterCount, statisticsArgs.CharacterCount);
            Assert.Equal(_docStats.NonWhiteSpaceCharacterCount, statisticsArgs.NonWhiteSpaceCharacterCount);
            Assert.Equal(_docStats.SentenceCount, statisticsArgs.SentenceCount);
            Assert.Equal(_docStats.ProperNounCount, statisticsArgs.ProperNounCount);
            Assert.Equal(_docStats.ColemanLiauIndex, statisticsArgs.ColemanLiauIndex, 3);
            Assert.Equal(_docStats.FleschReadingEase, statisticsArgs.FleschReadingEase, 3);
        }

        [Fact]
        public void TestFileLoadingWithException()
        {
            _mock.Setup(fileManager => fileManager.Load()).Returns(() => throw new Exception());
            Assert.Throws<Exception>(() => _docStats.Load());
        }

        #endregion

        #region DistinctWordCount

        [Fact]
        public void TestDistinctWordCountForEmptyText()
        {
            MockAndLoadText("");
            Assert.Equal(0, _docStats.DistinctWordCount.Count);
        }

        [Fact]
        public void TestDistinctWordCountForSpecialCharacterText()
        {
            MockAndLoadText(" *?.!  ");
            Assert.Equal(0, _docStats.DistinctWordCount.Count);
        }

        [Fact]
        public void TestDistinctWordCountSameWordRepeating()
        {
            MockAndLoadText("hello hello hello");
            Assert.Equal(1, _docStats.DistinctWordCount.Count);
            Assert.Equal(3, _docStats.DistinctWordCount["hello"]);
        }

        [Fact]
        public void TestDistinctWordCountSameWordRepeatingWithSpecialChars()
        {
            MockAndLoadText("**hello hello1123 35345hello345345 hello");
            Assert.Equal(1, _docStats.DistinctWordCount.Count);
            Assert.Equal(4, _docStats.DistinctWordCount["hello"]);
        }

        [Fact]
        public void TestDistinctWordCountSameWordWithSpecialCharsInTheMiddle()
        {
            MockAndLoadText("hello hell'o");
            Assert.Equal(2, _docStats.DistinctWordCount.Count);
            Assert.Equal(1, _docStats.DistinctWordCount["hello"]);
            Assert.Equal(1, _docStats.DistinctWordCount["hell'o"]);
        }

        [Fact]
        public void TestDistinctWordCountMultipleWords()
        {
            MockAndLoadText("hello world! hello world!");
            Assert.Equal(2, _docStats.DistinctWordCount.Count);
            Assert.Equal(2, _docStats.DistinctWordCount["hello"]);
            Assert.Equal(2, _docStats.DistinctWordCount["world"]);
        }

        #endregion

        #region WordFilter

        [Fact]
        public void TestFilteredWordCountChangedIsRaisedOnLoad()
        {
            bool raised = false;
            _docStats.FilteredWordCountChanged += (sender, args) => raised = true;

            // The filter itself did not change, but the word statistics behind it did.
            MockAndLoadText("alpha beta alpha");

            Assert.True(raised);
            Assert.Equal(2, _docStats.FilteredWordCount.Count());
        }

        [Fact]
        public void TestFilteredWordCountChangedIsRaisedOnChange()
        {
            int raised = 0;
            _docStats.FilteredWordCountChanged += (sender, args) => ++raised;

            _docStats.MinWordLength = 4;
            _docStats.MinWordOccurrence = 2;
            _docStats.IgnoredWords = new[] { "hello" };

            Assert.Equal(3, raised);
        }

        [Fact]
        public void TestFilteredWordCountChangedIsNotRaisedWithoutChange()
        {
            _docStats.MinWordLength = 4;
            _docStats.IgnoredWords = new[] { "hello" };

            int raised = 0;
            _docStats.FilteredWordCountChanged += (sender, args) => ++raised;

            // Assigning the very same values must not notify the view again.
            _docStats.MinWordLength = 4;
            _docStats.MinWordOccurrence = _docStats.MinWordOccurrence;
            _docStats.IgnoredWords = new[] { " HELLO " };

            Assert.Equal(0, raised);
        }

        [Fact]
        public void TestIgnoredWordsAreNormalized()
        {
            _docStats.IgnoredWords = new[] { "  Hello ", "WORLD" };

            Assert.Equal(new[] { "hello", "world" }, _docStats.IgnoredWords);
        }

        [Fact]
        public void TestFilteredWordCountIsOrderedByOccurrence()
        {
            MockAndLoadText("apple pear apple plum apple pear");

            var filtered = _docStats.FilteredWordCount.ToList();

            Assert.Equal(3, filtered.Count);
            Assert.Equal("apple", filtered[0].Key);
            Assert.Equal(3, filtered[0].Value);
            Assert.Equal("pear", filtered[1].Key);
            Assert.Equal("plum", filtered[2].Key);
        }

        [Fact]
        public void TestFilteredWordCountByMinWordLength()
        {
            MockAndLoadText("apple pear plum fig fig fig");
            _docStats.MinWordLength = 4;

            Assert.Equal(new[] { "apple", "pear", "plum" },
                _docStats.FilteredWordCount.Select(p => p.Key).OrderBy(w => w));
        }

        [Fact]
        public void TestFilteredWordCountByMinWordOccurrence()
        {
            MockAndLoadText("apple pear apple plum apple pear");
            _docStats.MinWordOccurrence = 2;

            Assert.Equal(new[] { "apple", "pear" },
                _docStats.FilteredWordCount.Select(p => p.Key).OrderBy(w => w));
        }

        [Fact]
        public void TestFilteredWordCountByIgnoredWords()
        {
            MockAndLoadText("apple pear plum");
            _docStats.IgnoredWords = new[] { " Apple ", "PLUM" };

            Assert.Equal(new[] { "pear" }, _docStats.FilteredWordCount.Select(p => p.Key));
        }

        #endregion

        #region CharacterCount

        [Fact]
        public void TestCharacterCount()
        {
            MockAndLoadText("test character count");
            Assert.Equal(20, _docStats.CharacterCount);
        }

        [Fact]
        public void TestCharacterCountForEmptyFile()
        {
            MockAndLoadText("");
            Assert.Equal(0, _docStats.CharacterCount);
        }

        #endregion

        #region NonWhiteSpaceCharacterCount

        [Fact]
        public void TestNonWhitespaceCount()
        {
            MockAndLoadText("test character count");
            Assert.Equal(18, _docStats.NonWhiteSpaceCharacterCount);
        }

        [Fact]
        public void TestNonWhitespaceCountOnlyWhitespace()
        {
            MockAndLoadText("           ");
            Assert.Equal(0, _docStats.NonWhiteSpaceCharacterCount);
        }

        #endregion

        #region SentenceCount

        [Fact]
        public void TestSentenceCount()
        {
            MockAndLoadText("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.");
            Assert.Equal(2, _docStats.SentenceCount);
        }

        [Fact]
        public void TestSentenceCountZeroSentences()
        {
            MockAndLoadText("");
            Assert.Equal(0, _docStats.SentenceCount);
        }

        #endregion

        #region ProperNounCount

        [Fact]
        public void TestProperNounCount()
        {
            MockAndLoadText("thunderbolts and lightning, very, very frightening me Galileo, Galileo");
            Assert.Equal(2, _docStats.ProperNounCount);
        }

        [Fact]
        public void TestProperNounCountMultipleSentences()
        {
            MockAndLoadText("Lorem Ipsum dolor sit amet, Consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.");
            Assert.Equal(2, _docStats.ProperNounCount);
        }

        #endregion

        #region ColemanLiauIndex

        [Fact]
        public void TestColemanLiauIndex()
        {
            MockAndLoadText("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.");
            Assert.Equal(15.14, _docStats.ColemanLiauIndex, 2);
        }

        [Fact]
        public void TestColemanLiauIndexOneSentence()
        {
            MockAndLoadText("Oh hi Mark");
            Assert.Equal(-0.12, _docStats.ColemanLiauIndex, 2);
        }

        #endregion

        #region FleschReadingEase

        [Fact]
        public void TestFleschReadingEase()
        {
            MockAndLoadText("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.");
            Assert.Equal(4.99, _docStats.FleschReadingEase, 2);
        }

        [Fact]
        public void TestFleschReadingEaseNoSyllables()
        {
            MockAndLoadText("sdfsdf dfgdfg fghfgh");
            Assert.Equal(19.48, _docStats.ColemanLiauIndex);
        }

        #endregion
    }
}