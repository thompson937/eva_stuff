using System;
using System.Collections.Generic;
using System.Text;

namespace NonogramModel.GameState
{
    public class GameState : IGameState
    {
        public int LivesCounter { get; set; }

        public int boardSize { get; }

        readonly bool[] board;
        bool[] guesses;

        public GameState(int _boardSize, bool[] image)
        {
            LivesCounter = 3;
            boardSize = _boardSize;
            board = image;
            guesses = new bool[boardSize];

            for (int i = 0; i < guesses.Length; i++)
            {
                guesses[i] = false;
            }
        }

        public bool GetBoard(int x, int y)
        {
            int i = y % boardSize + x;
            return board[i];
        }

        public bool GetGuess(int x, int y)
        {
            int i = y % boardSize + x;
            return guesses[i];
        }

        public bool SetGuess(int x, int y)
        {
            int i = y % boardSize + x;

            if (!board[i])
            {
                return false;
            }
            else
            {
                guesses[i] = true;
                return true;
            }
        }
    }
}
