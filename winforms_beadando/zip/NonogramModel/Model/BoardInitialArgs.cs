using NonogramModel.Persistence;

namespace NonogramModel.Model
{
    public class BoardInitialArgs : EventArgs
    {
        public readonly NonogramFileData data;
        public string[] rowStrings { get; }
        public string[] columnStrings { get; }

        public BoardInitialArgs(NonogramFileData _data, string[] _row, string[] _column)
        {
            data = _data;
            rowStrings = _row;
            columnStrings = _column;
        }
    }
}