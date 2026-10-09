using NonogramModel.Persistence;

namespace NonogramTest;

[TestClass]
public class NonogramPersistenceTest
{
    NonogramFileManager? fm = null;

    [TestMethod]
    public void ValidFileLoadingTest()
    {
        fm = new NonogramFileManager("Input\\valid_file.ngm");

        NonogramFileData data = fm.Load();

        Assert.AreEqual(3, data.Lives);
        Assert.AreEqual(27, data.ElapsedTime);
        Assert.IsNotEmpty(data.Board);
        Assert.HasCount(data.BoardSize*data.BoardSize, data.Board);
    }

    [TestMethod]
    public void InvalidFileTest()
    {
        fm = new NonogramFileManager("Input\\random_text.ngm");

        Assert.ThrowsExactly<NonogramFileException>(() => fm.Load());
    }

    [TestMethod]
    public void InvalidBoardSizeOnLoadTest()
    {
        fm = new NonogramFileManager("Input\\bad_board_size.ngm");

        Assert.ThrowsExactly<NonogramFileException>(() => fm.Load());
    }
}
