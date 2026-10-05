using System;
using System.Collections.Generic;
using System.Text;

namespace NonogramModel.GameStates
{
    public class GameState : IGameState
    {
        public int LivesCounter { get; set; }
        public int TimeCounter { get; set; }

        public int boardSize { get; }

        public event EventHandler? BoardChanged;

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

        public void SetBoard(int x, int y, UserInputType type)
        {
            int i = y * boardSize + x;
            bool originalBitFlag = (board[i] & 0b001) != 0;

            if (type == UserInputType.EXCLUSION)
            {
                if (board[i] != 0b011) board[i] = board[i] | 0b100;
            }
            else
            {
                if (originalBitFlag)
                {
                    board[i] = 0b011;
                }
                else
                {
                    board[i] = board[i] | 0b100;
                }
            }

            BoardChanged?.Invoke(this, EventArgs.Empty);
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
    }
}
