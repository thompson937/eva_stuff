namespace NonogramModel.Model
{
    public class CellChangeEventArgs : EventArgs
    {
        public readonly int x;
        public readonly int y;
        public readonly CellCategory category;

        public CellChangeEventArgs(int _x, int _y, CellCategory _category)
        {
            x = _x;
            y = _y;
            category = _category;
        }
    }
}