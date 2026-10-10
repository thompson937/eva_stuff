using NonogramModel.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace NonogramModel.Model
{
    public class Nonogram : INonogram
    {
        public NonogramFileData Data { get; set; }

        #region Events
        public event EventHandler<int>? LivesSet;
        public event EventHandler<CellChangeEventArgs>? CellChanged;
        public event EventHandler? WinCondition;
        public event EventHandler? LoseCondition;
        public event EventHandler<int>? RowCleared;
        public event EventHandler<int>? ColumnCleared;
        public event EventHandler<BoardInitialArgs>? ModelReady;
        #endregion

        #region Constructor

        public Nonogram(NonogramFileData data)
        {
            if (data.Board.Length != data.BoardSize*data.BoardSize)
            {
                throw new ArgumentOutOfRangeException("Supplied board size and actual board size do not match");
            }

            Data = data;
        }

        #endregion

        #region Public methods

        public void Initialise()
        {
            ModelReady?.Invoke(this, new BoardInitialArgs(Data, _getRowHints(), _getColumnHints()));
        }
        public void Dispose() { }

        public void SetBoard(int x, int y, CellCategory type)
        {
            int i = y * Data.BoardSize + x;

            switch (type)
            {
                case CellCategory.EXCLUSION:
                    Data.Board[i] = Data.Board[i] ^ 0b0100;

                    if ((Data.Board[i] & 0b0100) == 0) CellChanged?.Invoke(this, new CellChangeEventArgs(x, y, CellCategory.EMPTY));
                    else CellChanged?.Invoke(this, new CellChangeEventArgs(x, y, CellCategory.EXCLUSION));
                    _checkCompletion();


                    break;
                case CellCategory.SELECTION:
                    if ((Data.Board[i] & 0b0001) != 0)
                    {
                        // Selection correct
                        Data.Board[i] = 0b011;
                        CellChanged?.Invoke(this, new CellChangeEventArgs(x, y, CellCategory.SELECTION));
                        _checkCompletion();
                    }
                    else
                    {
                        // Selection incorrect, permanently excluded
                        Data.Board[i] = 0b1000;
                        CellChanged?.Invoke(this, new CellChangeEventArgs(x, y, CellCategory.PERMANENT_EXCLUSION));
                        _decreaseLives();
                    }
                    break;
                case CellCategory.PERMANENT_EXCLUSION:
                    Data.Board[i] = 0b1000;
                    CellChanged?.Invoke(this, new CellChangeEventArgs(x, y, CellCategory.PERMANENT_EXCLUSION));
                    break;
                default:
                    break;
            }
        }

        public void SaveGame(INonogramFileManager fileManager)
        {
            fileManager.Save(Data);

            fileManager.Dispose();
        }

        #endregion

        #region Private Methods

        int _getBoard(int x, int y)
        {
            int i = y * Data.BoardSize + x;
            return Data.Board[i];
        }  

        private string[] _getRowHints()
        {
            string[] data = new string[Data.BoardSize];

            for (int y = 0; y < Data.BoardSize; y++)
            {
                StringBuilder sb = new StringBuilder();
                int continuity = 0;

                for (int x = 0; x < Data.BoardSize; x++)
                {
                    if ((_getBoard(x, y) & 0b001) != 0)
                    {
                        continuity++;
                    }
                    else
                    {
                        if (continuity > 0) sb.Append(continuity.ToString() + " ");
                        continuity = 0;
                    }
                }

                if (continuity > 0) sb.Append(continuity);

                data[y] = sb.ToString();
            }

            return data;
        }

        private string[] _getColumnHints()
        {
            string[] data = new string[Data.BoardSize];

            for (int x = 0; x < Data.BoardSize; x++)
            {
                StringBuilder sb = new StringBuilder();
                int continuity = 0;

                for (int y = 0; y < Data.BoardSize; y++)
                {
                    if ((_getBoard(x, y) & 0b001) != 0)
                    {
                        continuity++;
                    }
                    else
                    {
                        if (continuity > 0) sb.Append(continuity.ToString() + "\n");
                        continuity = 0;
                    }
                }

                if (continuity > 0) sb.Append(continuity);

                data[x] = sb.ToString();
            }

            return data;
        }

        private void _decreaseLives()
        {

            Data.Lives--;
            LivesSet?.Invoke(this, Data.Lives);

            if (Data.Lives == 0)
            {
                LoseCondition?.Invoke(this, EventArgs.Empty);
            }
        }

        private void _checkCompletion()
        {
            // C# alapból 0-ra inicializálja ilyenkor a tömb elemeit, tehát nem kell külön
            // lenullázni ezt a kettő tömböt
            int[] completedRows = new int[Data.BoardSize];
            int[] completedColumns = new int[Data.BoardSize];
            int entireBoardCompletion = 0;

            for (int x = 0; x < Data.BoardSize; x++)
            {
                for (int y = 0; y < Data.BoardSize; y++)
                {
                    int boardValue = _getBoard(x, y);
                    if ( ((boardValue & 0b010) != 0 && (boardValue & 0b001) != 0) || (boardValue & 0b001) == 0 )
                    {
                        completedRows[y]++;
                        completedColumns[x]++;
                        entireBoardCompletion++;
                    }
                }
            }

            for (int i = 0; i < completedRows.Length; i++)
            {
                if (completedRows[i] == Data.BoardSize)
                {
                    RowCleared?.Invoke(this, i);

                    for (int x = 0; x < Data.BoardSize; x++)
                    {
                        if ((_getBoard(x, i) & 0b1000) == 0 && (_getBoard(x, i) & 0b0001) == 0)
                        {
                            SetBoard(x, i, CellCategory.PERMANENT_EXCLUSION);
                        }
                    }
                }
            }

            for (int i = 0; i < completedColumns.Length; i++)
            {
                if (completedColumns[i] == Data.BoardSize)
                {
                    ColumnCleared?.Invoke(this, i);

                    for (int y = 0; y < Data.BoardSize; y++)
                    {
                        if ((_getBoard(i, y) & 0b1000) == 0 && (_getBoard(i, y) & 0b0001) == 0)
                        {
                            SetBoard(i, y, CellCategory.PERMANENT_EXCLUSION);
                        }
                    }
                }
            }

            if (entireBoardCompletion == Data.BoardSize*Data.BoardSize)
            {
                WinCondition?.Invoke(this, EventArgs.Empty);
            }
        }

        #endregion
    }
}
