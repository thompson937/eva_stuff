using NonogramModel.Model;
using NonogramModel.Persistence;
using Moq;

namespace NonogramTest
{
    [TestClass]
    public sealed class NonogramModelTest
    {
        Nonogram? _model = null;

        [TestInitialize]
        public void ModelTestSetup()
        {
            _model = new Nonogram(_getTestData());
        }

        NonogramFileData _getTestData()
        {
            return new NonogramFileData(new int[]{
                0, 0, 1, 0, 0,
                0, 1, 0, 1, 0,
                1, 0, 0, 0, 1,
                1, 1, 0, 1, 1,
                0, 0, 1, 0, 0,
            }, 3, 0, 5);
        }

        [TestMethod]
        public void ModelInitTest()
        {
            BoardInitialArgs? loadedArgs = null;

            _model!.ModelReady += (_, args) => loadedArgs = args;

            _model!.Initialise();

            Assert.IsNotNull(loadedArgs);
            Assert.IsNotEmpty(loadedArgs.rowStrings);
            Assert.IsNotEmpty(loadedArgs.columnStrings);
        }

        [TestMethod]
        public void SetExclusionTest()
        {
            CellChangeEventArgs? cellChangeArgs = null;

            _model!.CellChanged += (_, args) => cellChangeArgs = args;

            _model!.SetBoard(0, 0, CellCategory.EXCLUSION);

            Assert.IsNotNull(cellChangeArgs);
            Assert.AreEqual(0, cellChangeArgs.x);
            Assert.AreEqual(0, cellChangeArgs.y);
            Assert.AreEqual(CellCategory.EXCLUSION, cellChangeArgs.category);

            _model!.SetBoard(0, 0, CellCategory.EXCLUSION);
            Assert.AreEqual(CellCategory.EMPTY, cellChangeArgs.category);
        }

        [TestMethod]
        public void SetCorrectSelectionTest()
        {
            CellChangeEventArgs? cellChangeArgs = null;

            _model!.CellChanged += (_, args) => cellChangeArgs = args;

            _model!.SetBoard(1, 1, CellCategory.SELECTION);

            Assert.IsNotNull(cellChangeArgs);
            Assert.AreEqual(1, cellChangeArgs.x);
            Assert.AreEqual(1, cellChangeArgs.y);
            Assert.AreEqual(CellCategory.SELECTION, cellChangeArgs.category);
        }

        [TestMethod]
        public void SetIncorrectSelectionTest()
        {
            CellChangeEventArgs? cellChangeArgs = null;
            int remainingLives = -1;

            _model!.CellChanged += (_, args) => cellChangeArgs = args;
            _model!.LivesSet += (_, args) => remainingLives = args;

            _model!.SetBoard(2, 1, CellCategory.SELECTION);

            Assert.IsNotNull(cellChangeArgs);
            Assert.AreEqual(2, cellChangeArgs.x);
            Assert.AreEqual(1, cellChangeArgs.y);
            Assert.AreEqual(CellCategory.PERMANENT_EXCLUSION, cellChangeArgs.category);
            Assert.AreEqual(2, remainingLives);
        }

        [TestMethod]
        public void SetPermanentExclusionTest()
        {
            CellChangeEventArgs? cellChangeArgs = null;

            _model!.CellChanged += (_, args) => cellChangeArgs = args;

            _model!.SetBoard(3, 0, CellCategory.PERMANENT_EXCLUSION);

            Assert.IsNotNull(cellChangeArgs);
            Assert.AreEqual(3, cellChangeArgs.x);
            Assert.AreEqual(0, cellChangeArgs.y);
            Assert.AreEqual(CellCategory.PERMANENT_EXCLUSION, cellChangeArgs.category);
        }

        [TestMethod]
        public void LoseConditionTest()
        {
            bool loseEventTriggered = false;
            int remainingLives = -1;

            _model!.LoseCondition += (_, _) => loseEventTriggered = true;
            _model!.LivesSet += (_, args) => remainingLives = args;

            _model!.SetBoard(2, 1, CellCategory.SELECTION);
            _model!.SetBoard(2, 2, CellCategory.SELECTION);
            _model!.SetBoard(2, 3, CellCategory.SELECTION);

            Assert.IsTrue(loseEventTriggered);
            Assert.AreEqual(0, remainingLives);
        }

        [TestMethod]
        public void CompletedRowAutoExclusionTest()
        {
            var cellChanges = new List<CellChangeEventArgs>();
            int rowCleared = -1;

            _model!.CellChanged += (_, args) => cellChanges.Add(args);
            _model!.RowCleared += (_, args) => rowCleared = args;

            _model!.SetBoard(2, 0, CellCategory.SELECTION);

            Assert.AreEqual(0, rowCleared);
            Assert.HasCount(5, cellChanges);

            Assert.AreEqual(CellCategory.SELECTION, cellChanges[0].category);

            int[] expectedX = { 3, 0, 1, 3, 4 };

            for (int i = 1; i < cellChanges.Count; i++)
            {
                Assert.AreEqual(CellCategory.PERMANENT_EXCLUSION, cellChanges[i].category);
                Assert.AreEqual(expectedX[i], cellChanges[i].x);
                Assert.AreEqual(0, cellChanges[i].y);
            }
        }

        [TestMethod]
        public void CompletedColumnAutoExclusionTest()
        {
            var cellChanges = new List<CellChangeEventArgs>();
            int columnCleared = -1;

            _model!.CellChanged += (_, args) => cellChanges.Add(args);
            _model!.ColumnCleared += (_, args) => columnCleared = args;

            _model!.SetBoard(0, 2, CellCategory.SELECTION);
            _model!.SetBoard(0, 3, CellCategory.SELECTION);

            Assert.AreEqual(0, columnCleared);
            Assert.HasCount(5, cellChanges);

            Assert.AreEqual(CellCategory.SELECTION, cellChanges[0].category);
            Assert.AreEqual(CellCategory.SELECTION, cellChanges[1].category);

            int[] expectedY = { 2, 3, 0, 1, 4 };

            for (int i = 2; i < cellChanges.Count; i++)
            {
                Assert.AreEqual(CellCategory.PERMANENT_EXCLUSION, cellChanges[i].category);
                Assert.AreEqual(expectedY[i], cellChanges[i].y);
                Assert.AreEqual(0, cellChanges[i].x);
            }
        }

        [TestMethod]
        public void WinConditionTest()
        {
            _model = new Nonogram(
                new NonogramFileData(new int[]{
                    1, 0, 0, 0, 0,
                    0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0
                }, 3, 0, 5)
            );

            bool winEventTriggered = false;
            _model.WinCondition += (_, _) => winEventTriggered = true;

            _model.SetBoard(0, 0, CellCategory.SELECTION);

            Assert.IsTrue(winEventTriggered);
        }

        [TestMethod]
        public void SaveTest()
        {
            Mock<INonogramFileManager> saveMock = new Mock<INonogramFileManager>();

            _model!.SaveGame(saveMock.Object);

            saveMock.Verify(m => m.Save(It.IsAny<NonogramFileData>()), Times.Once);
            saveMock.Verify(m => m.Dispose(), Times.Once);
        }

        [TestMethod]
        public void LoadingErrorTest()
        {
            NonogramFileData faultyData = new NonogramFileData(new int[]{ 1 }, 3, 0, 5);

            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => new Nonogram(faultyData));
        }
    }
}
