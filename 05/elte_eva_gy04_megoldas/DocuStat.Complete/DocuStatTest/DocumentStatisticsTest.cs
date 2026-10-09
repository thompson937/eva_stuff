using ELTE.DocuStat.Model;
using ELTE.DocuStat.Persistence;
using Moq;

namespace DocuStatTest
{
    [TestClass]
    public sealed class DocumentStatisticsTest
    {
        Mock<IFileManager>? _mock = null;
        DocumentStatistics? _docStat = null;

        [TestInitialize]
        public void InitDocumentStatisticsTest()
        {
            _mock = new Mock<IFileManager>();
            _docStat = new DocumentStatistics( _mock.Object );
        }

        void MockAndLoadText(string text)
        {
            _mock!.Setup(fm => fm.Load()).Returns(text);
            _docStat!.Load();
        }

        [TestMethod]
        public void TestFileLoading()
        {
            // Előkészítés
            bool fileContentReadyRaised = false;
            TextStatisticsReadyEventArgs? statArgs = null;

            _docStat!.FileContentReady += (sender, args) => fileContentReadyRaised = true;
            _docStat!.TextStatisticsReady += (sender, args) => statArgs = args;

            // Teszt
            MockAndLoadText("Hello World");

            Assert.IsTrue( fileContentReadyRaised );
            Assert.AreEqual("Hello World", _docStat.FileContent);
            Assert.IsNotNull(statArgs);
            Assert.AreNotEqual(0, _docStat.DistinctWordCount.Count);
        }

        [TestMethod]
        public void TestFileLoadingWithException()
        {
            _mock!.Setup(fm => fm.Load()).Returns(() => throw new Exception());

            Assert.ThrowsExactly<Exception>( () => _docStat!.Load());
        }

        [TestMethod]
        public void TestForEmptyText()
        {
            MockAndLoadText("");
            Assert.IsEmpty(_docStat!.DistinctWordCount);
        }

        [TestMethod]
        public void SameWordRepeatTest()
        {
            MockAndLoadText("alma alma");

            Assert.HasCount(1, _docStat!.DistinctWordCount);
            Assert.AreEqual(2, _docStat!.DistinctWordCount["alma"]);
        }

    }
}
