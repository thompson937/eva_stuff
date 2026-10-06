namespace NonogramModel.Model
{
    public class CellChangeEventArgs : EventArgs
    {
        public int x;
        public int y;
        public CellCategory category;

        public CellChangeEventArgs(int _x, int _y, CellCategory _category)
        {
            x = _x;
            y = _y;
            category = _category;
        }
    }
}