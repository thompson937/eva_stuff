using ELTE.DocuStat.Model;
using ELTE.DocuStat.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace ELTE.DocuStat.Test
{
    [TestClass]
    public class DocumentStatisticsTest
    {
        #region Fields

        private Mock<IFileManager> _mock = null!;
        private DocumentStatistics _docStats = null!;

        #endregion

        #region Init and Utility methods

        [TestInitialize]
        public void InitDocumentStatisticsTest()
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

        [TestMethod]
        public void TestFileLoading()
        {
            bool fileContentReadyRaised = false;
            TextStatisticsReadyEventArgs? statisticsArgs = null;
            _docStats.FileContentReady += (sender, args) => fileContentReadyRaised = true;
            _docStats.TextStatisticsReady += (sender, args) => statisticsArgs = args;

            MockAndLoadText("Hello World");

            Assert.AreEqual("Hello World", _docStats.FileContent);
            Assert.AreNotEqual(0, _docStats.DistinctWordCount.Count);
            Assert.IsTrue(fileContentReadyRaised);

            // The TextStatisticsReady event carries the metrics itself.
            Assert.IsNotNull(statisticsArgs);
            Assert.AreEqual(_docStats.CharacterCount, statisticsArgs.CharacterCount);
            Assert.AreEqual(_docStats.NonWhiteSpaceCharacterCount, statisticsArgs.NonWhiteSpaceCharacterCount);
            Assert.AreEqual(_docStats.SentenceCount, statisticsArgs.SentenceCount);
            Assert.AreEqual(_docStats.ProperNounCount, statisticsArgs.ProperNounCount);
            Assert.AreEqual(_docStats.ColemanLiauIndex, statisticsArgs.ColemanLiauIndex, 0.001);
            Assert.AreEqual(_docStats.FleschReadingEase, statisticsArgs.FleschReadingEase, 0.001);
        }

        [TestMethod]
        public void TestFileLoadingWithException()
        {
            _mock.Setup(fileManager => fileManager.Load()).Returns(() => throw new Exception());
            Assert.ThrowsExactly<Exception>(() => _docStats.Load());
        }

        #endregion

        #region DistinctWordCount

        [TestMethod]
        public void TestDistinctWordCountForEmptyText()
        {
            MockAndLoadText("");
            Assert.AreEqual(0, _docStats.DistinctWordCount.Count);
        }

        [TestMethod]
        public void TestDistinctWordCountForSpecialCharacterText()
        {
            MockAndLoadText(" *?.!  ");
            Assert.AreEqual(0, _docStats.DistinctWordCount.Count);
        }

        [TestMethod]
        public void TestDistinctWordCountSameWordRepeating()
        {
            MockAndLoadText("hello hello hello");
            Assert.AreEqual(1, _docStats.DistinctWordCount.Count);
            Assert.AreEqual(3, _docStats.DistinctWordCount["hello"]);
        }

        [TestMethod]
        public void TestDistinctWordCountSameWordRepeatingWithSpecialChars()
        {
            MockAndLoadText("**hello hello1123 35345hello345345 hello");
            Assert.AreEqual(1, _docStats.DistinctWordCount.Count);
            Assert.AreEqual(4, _docStats.DistinctWordCount["hello"]);
        }

        [TestMethod]
        public void TestDistinctWordCountSameWordWithSpecialCharsInTheMiddle()
        {
            MockAndLoadText("hello hell'o");
            Assert.AreEqual(2, _docStats.DistinctWordCount.Count);
            Assert.AreEqual(1, _docStats.DistinctWordCount["hello"]);
            Assert.AreEqual(1, _docStats.DistinctWordCount["hell'o"]);
        }

        [TestMethod]
        public void TestDistinctWordCountMultipleWords()
        {
            MockAndLoadText("hello world! hello world!");
            Assert.AreEqual(2, _docStats.DistinctWordCount.Count);
            Assert.AreEqual(2, _docStats.DistinctWordCount["hello"]);
            Assert.AreEqual(2, _docStats.DistinctWordCount["world"]);
        }

        #endregion

        #region WordFilter

        [TestMethod]
        public void TestFilteredWordCountChangedIsRaisedOnLoad()
        {
            bool raised = false;
            _docStats.FilteredWordCountChanged += (sender, args) => raised = true;

            // The filter itself did not change, but the word statistics behind it did.
            MockAndLoadText("alpha beta alpha");

            Assert.IsTrue(raised);
            Assert.AreEqual(2, _docStats.FilteredWordCount.Count());
        }

        [TestMethod]
        public void TestFilteredWordCountChangedIsRaisedOnChange()
        {
            int raised = 0;
            _docStats.FilteredWordCountChanged += (sender, args) => ++raised;

            _docStats.MinWordLength = 4;
            _docStats.MinWordOccurrence = 2;
            _docStats.IgnoredWords = new[] { "hello" };

            Assert.AreEqual(3, raised);
        }

        [TestMethod]
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

            Assert.AreEqual(0, raised);
        }

        [TestMethod]
        public void TestIgnoredWordsAreNormalized()
        {
            _docStats.IgnoredWords = new[] { "  Hello ", "WORLD" };

            CollectionAssert.AreEqual(new[] { "hello", "world" }, _docStats.IgnoredWords.ToArray());
        }

        [TestMethod]
        public void TestFilteredWordCountIsOrderedByOccurrence()
        {
            MockAndLoadText("apple pear apple plum apple pear");

            var filtered = _docStats.FilteredWordCount.ToList();

            Assert.AreEqual(3, filtered.Count);
            Assert.AreEqual("apple", filtered[0].Key);
            Assert.AreEqual(3, filtered[0].Value);
            Assert.AreEqual("pear", filtered[1].Key);
            Assert.AreEqual("plum", filtered[2].Key);
        }

        [TestMethod]
        public void TestFilteredWordCountByMinWordLength()
        {
            MockAndLoadText("apple pear plum fig fig fig");
            _docStats.MinWordLength = 4;

            CollectionAssert.AreEquivalent(new[] { "apple", "pear", "plum" },
                _docStats.FilteredWordCount.Select(p => p.Key).ToArray());
        }

        [TestMethod]
        public void TestFilteredWordCountByMinWordOccurrence()
        {
            MockAndLoadText("apple pear apple plum apple pear");
            _docStats.MinWordOccurrence = 2;

            CollectionAssert.AreEquivalent(new[] { "apple", "pear" },
                _docStats.FilteredWordCount.Select(p => p.Key).ToArray());
        }

        [TestMethod]
        public void TestFilteredWordCountByIgnoredWords()
        {
            MockAndLoadText("apple pear plum");
            _docStats.IgnoredWords = new[] { " Apple ", "PLUM" };

            CollectionAssert.AreEqual(new[] { "pear" },
                _docStats.FilteredWordCount.Select(p => p.Key).ToArray());
        }

        #endregion

        #region CharacterCount

        [TestMethod]
        public void TestCharacterCount()
        {
            MockAndLoadText("test character count");
            Assert.AreEqual(20, _docStats.CharacterCount);
        }

        [TestMethod]
        public void TestCharacterCountForEmptyFile()
        {
            MockAndLoadText("");
            Assert.AreEqual(0, _docStats.CharacterCount);
        }

        #endregion

        #region NonWhiteSpaceCharacterCount

        [TestMethod]
        public void TestNonWhitespaceCount()
        {
            MockAndLoadText("test character count");
            Assert.AreEqual(18, _docStats.NonWhiteSpaceCharacterCount);
        }

        [TestMethod]
        public void TestNonWhitespaceCountOnlyWhitespace()
        {
            MockAndLoadText("           ");
            Assert.AreEqual(0, _docStats.NonWhiteSpaceCharacterCount);
        }

        #endregion

        #region SentenceCount

        [TestMethod]
        public void TestSentenceCount()
        {
            MockAndLoadText(
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.");
            Assert.AreEqual(2, _docStats.SentenceCount);
        }

        [TestMethod]
        public void TestSentenceCountZeroSentences()
        {
            MockAndLoadText("");
            Assert.AreEqual(0, _docStats.SentenceCount);
        }

        #endregion

        #region ProperNounCount

        [TestMethod]
        public void TestProperNounCount()
        {
            MockAndLoadText("thunderbolts and lightning, very, very frightening me Galileo, Galileo");
            Assert.AreEqual(2, _docStats.ProperNounCount);
        }

        [TestMethod]
        public void TestProperNounCountMultipleSentences()
        {
            MockAndLoadText(
                "Lorem Ipsum dolor sit amet, Consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.");
            Assert.AreEqual(2, _docStats.ProperNounCount);
        }

        #endregion

        #region ColemanLiauIndex

        [TestMethod]
        public void TestColemanLiauIndex()
        {
            MockAndLoadText(
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.");
            Assert.AreEqual(15.14, _docStats.ColemanLiauIndex, 0.01);
        }

        [TestMethod]
        public void TestColemanLiauIndexOneSentence()
        {
            MockAndLoadText("Oh hi Mark");
            Assert.AreEqual(-0.12, _docStats.ColemanLiauIndex, 0.01);
        }

        #endregion

        #region FleschReadingEase

        [TestMethod]
        public void TestFleschReadingEase()
        {
            MockAndLoadText(
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.");
            Assert.AreEqual(4.99, _docStats.FleschReadingEase, 0.01);
        }

        [TestMethod]
        public void TestFleschReadingEaseNoSyllables()
        {
            MockAndLoadText("sdfsdf dfgdfg fghfgh");
            Assert.AreEqual(19.48, _docStats.ColemanLiauIndex);
        }

        #endregion
    }
}