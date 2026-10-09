using ELTE.DocuStat.Model;
using ELTE.DocuStat.Persistence;
using Moq;

namespace DocuStatTestXUnit
{
    public class DocuStatTesting
    {
        Mock<IFileManager> _mock;
        DocumentStatistics _docStat;


        void MockAndLoadText(string text)
        {
            _mock.Setup(fm => fm.Load()).Returns(text);
            _docStat.Load();
        }

        public DocuStatTesting()
        {
            _mock = new Mock<IFileManager>();
            _docStat = new DocumentStatistics( _mock.Object );
        }

        [Fact]
        public void TestFileLoading()
        {
            // Előkészítés
            bool fileContentReadyRaised = false;
            TextStatisticsReadyEventArgs? statArgs = null;

            _docStat!.FileContentReady += (sender, args) => fileContentReadyRaised = true;
            _docStat!.TextStatisticsReady += (sender, args) => statArgs = args;

            // Teszt
            MockAndLoadText("Hello World");

            Assert.True(fileContentReadyRaised);
            Assert.Equal("Hello World", _docStat.FileContent);
            Assert.NotNull(statArgs);
            Assert.NotEmpty(_docStat.DistinctWordCount);
        }
    }
}
