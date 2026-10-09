using NonogramModel.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace NonogramModel.Model
{
    public class Nonogram : INonogram
    {
        #region Public properties
        public int ElapsedTime { get; set; }
        #endregion

        #region Fields
        private readonly int _boardSize;
        private int _livesCounter;
        private int[] _boardData;

        /* Board bitformat
        0b0000
        0b|permanent exclusion|exclusion|selection|original|
        */
        #endregion

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

            _livesCounter = data.Lives;
            _boardSize = data.BoardSize;
            _boardData = data.Board;
            ElapsedTime = data.ElapsedTime;
        }

        #endregion

        #region Public methods

        public void Initialise(NonogramFileData data)
        {
            ModelReady?.Invoke(this, new BoardInitialArgs(data, _getRowHints(), _getColumnHints()));
        }
        public void Dispose() { }

        public void SetBoard(int x, int y, CellCategory type)
        {
            int i = y * _boardSize + x;

            switch (type)
            {
                case CellCategory.EXCLUSION:
                    _boardData[i] = _boardData[i] ^ 0b0100;

                    if ((_boardData[i] & 0b0100) == 0) CellChanged?.Invoke(this, new CellChangeEventArgs(x, y, CellCategory.EMPTY));
                    else CellChanged?.Invoke(this, new CellChangeEventArgs(x, y, CellCategory.EXCLUSION));
                    _checkCompletion();


                    break;
                case CellCategory.SELECTION:
                    if ((_boardData[i] & 0b0001) != 0)
                    {
                        // Selection correct
                        _boardData[i] = 0b011;
                        CellChanged?.Invoke(this, new CellChangeEventArgs(x, y, CellCategory.SELECTION));
                        _checkCompletion();
                    }
                    else
                    {
                        // Selection incorrect, permanently excluded
                        _boardData[i] = 0b1000;
                        CellChanged?.Invoke(this, new CellChangeEventArgs(x, y, CellCategory.PERMANENT_EXCLUSION));
                        _decreaseLives();
                    }
                    break;
                case CellCategory.PERMANENT_EXCLUSION:
                    _boardData[i] = 0b1000;
                    CellChanged?.Invoke(this, new CellChangeEventArgs(x, y, CellCategory.PERMANENT_EXCLUSION));
                    break;
                default:
                    break;
            }
        }

        public void SaveGame(INonogramFileManager fileManager)
        {
            fileManager.Save(new NonogramFileData(
                _boardData, _livesCounter, ElapsedTime, _boardSize
            ));

            fileManager.Dispose();
        }

        #endregion

        #region Private Methods

        int _getBoard(int x, int y)
        {
            int i = y * _boardSize + x;
            return _boardData[i];
        }  

        private string[] _getRowHints()
        {
            string[] data = new string[_boardSize];

            for (int y = 0; y < _boardSize; y++)
            {
                StringBuilder sb = new StringBuilder();
                int continuity = 0;

                for (int x = 0; x < _boardSize; x++)
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
            string[] data = new string[_boardSize];

            for (int x = 0; x < _boardSize; x++)
            {
                StringBuilder sb = new StringBuilder();
                int continuity = 0;

                for (int y = 0; y < _boardSize; y++)
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

            _livesCounter--;
            LivesSet?.Invoke(this, _livesCounter);

            if (_livesCounter == 0)
            {
                LoseCondition?.Invoke(this, EventArgs.Empty);
            }
        }

        private void _checkCompletion()
        {
            // C# alapból 0-ra inicializálja ilyenkor a tömb elemeit, tehát nem kell külön
            // lenullázni ezt a kettő tömböt
            int[] completedRows = new int[_boardSize];
            int[] completedColumns = new int[_boardSize];
            int entireBoardCompletion = 0;

            for (int x = 0; x < _boardSize; x++)
            {
                for (int y = 0; y < _boardSize; y++)
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
                if (completedRows[i] == _boardSize)
                {
                    RowCleared?.Invoke(this, i);

                    for (int x = 0; x < _boardSize; x++)
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
                if (completedColumns[i] == _boardSize)
                {
                    ColumnCleared?.Invoke(this, i);

                    for (int y = 0; y < _boardSize; y++)
                    {
                        if ((_getBoard(i, y) & 0b1000) == 0 && (_getBoard(i, y) & 0b0001) == 0)
                        {
                            SetBoard(i, y, CellCategory.PERMANENT_EXCLUSION);
                        }
                    }
                }
            }

            if (entireBoardCompletion == _boardSize*_boardSize)
            {
                WinCondition?.Invoke(this, EventArgs.Empty);
            }
        }

        #endregion
    }
}
