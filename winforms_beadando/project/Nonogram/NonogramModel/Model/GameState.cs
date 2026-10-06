using System;
using System.Collections.Generic;
using System.Text;

namespace NonogramModel.Model
{
    public class GameState : IGameState, IDisposable
    {
        public int LivesCounter { get; set; }
        public int TimeCounter { get; set; }

        public int boardSize { get; }

        public event EventHandler<int>? LivesSet;
        public event EventHandler<CellChangeEventArgs>? CellChanged;
        public event EventHandler? WinCondition;
        public event EventHandler? LoseCondition;
        public event EventHandler<int>? RowCleared;
        public event EventHandler<int>? ColumnCleared;

        /* Board bitformat
        0b000
        0b|exclusion|selection|original|
        */

        int[] board;

        public GameState(int _boardSize, bool[] image)
        {
            LivesCounter = 3;
            TimeCounter = 0;
            boardSize = _boardSize;
            board = new int[boardSize*boardSize];

            for (int i = 0; i < image.Length; i++)
            {
                board[i] = (image[i]) ? 0b001 : 0b000;
            }
        }

        public int GetBoard(int x, int y)
        {
            int i = y * boardSize + x;
            return board[i];
        }

        public void SetBoard(int x, int y, CellCategory type)
        {
            int i = y * boardSize + x;
            bool originalBitFlag = (board[i] & 0b001) != 0;

            if (type == CellCategory.EXCLUSION)
            {
                // Kizárás (jobb klikk)
                if (board[i] != 0b011)
                {
                    board[i] = board[i] | 0b100;
                    CellChanged?.Invoke(this, new CellChangeEventArgs(x, y, CellCategory.EXCLUSION));
                }
            }
            else if (type == CellCategory.SELECTION)
            {
                // Jelölés (bal klikk)
                if (originalBitFlag)
                {
                    // Jelölés helyes
                    board[i] = 0b011;
                    CellChanged?.Invoke(this, new CellChangeEventArgs(x, y, CellCategory.SELECTION));
                    CheckCompletion();
                }
                else
                {
                    // Jelölés helytelen
                    board[i] = board[i] | 0b100;
                    CellChanged?.Invoke(this, new CellChangeEventArgs(x, y, CellCategory.INCORRECT));
                    DecreaseLives();
                }
            }

        }

        public string GetRowNumbers(int y)
        {
            StringBuilder sb = new StringBuilder();
            int continuity = 0;

            for (int x = 0; x < boardSize; x++)
            {
                if ( (GetBoard(x,y) & 0b001) != 0)
                {
                    continuity++;
                }
                else
                {
                    if (continuity > 0) sb.Append(continuity.ToString() + " ");
                    continuity = 0;
                }
            }

            if (continuity > 0) sb.Append(continuity.ToString());

            return sb.ToString();
        }

        public string GetColumnNumbers(int x)
        {
            StringBuilder sb = new StringBuilder();
            int continuity = 0;

            for (int y = 0; y < boardSize; y++)
            {
                if ((GetBoard(x, y) & 0b001) != 0)
                {
                    continuity++;
                }
                else
                {
                    if (continuity > 0) sb.Append(continuity.ToString() + "\n");
                    continuity = 0;
                }
            }

            if (continuity > 0) sb.Append(continuity.ToString());

            return sb.ToString();
        }

        private void DecreaseLives()
        {

            LivesCounter--;
            LivesSet?.Invoke(this, LivesCounter);

            if (LivesCounter == 0)
            {
                LoseCondition?.Invoke(this, EventArgs.Empty);
            }
        }

        private void CheckCompletion()
        {
            // C# alapból 0-ra inicializálja ilyenkor a tömb elemeit, tehát ez egy legális manőver
            int[] completedRows = new int[boardSize];
            int[] completedColumns = new int[boardSize];
            int entireBoardCompletion = 0;

            for (int x = 0; x < boardSize; x++)
            {
                for (int y = 0; y < boardSize; y++)
                {
                    int boardValue = GetBoard(x, y);
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
                if (completedRows[i] == boardSize)
                {
                    RowCleared?.Invoke(this, i);
                }
            }

            for (int i = 0; i < completedColumns.Length; i++)
            {
                if (completedColumns[i] == boardSize)
                {
                    ColumnCleared?.Invoke(this, i);
                }
            }

            if (entireBoardCompletion == boardSize*boardSize)
            {
                WinCondition?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Dispose()
        {
            
        }
    }
}
