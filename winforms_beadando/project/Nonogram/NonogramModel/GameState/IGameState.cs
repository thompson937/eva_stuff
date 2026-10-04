using System;
using System.Collections.Generic;
using System.Text;

namespace NonogramModel.GameState
{
    public interface IGameState
    {
        int LivesCounter { get; set; }

        int boardSize { get; }

        bool GetBoard(int x, int y);

        bool GetGuess(int x, int y);
        bool SetGuess(int x, int y);
    }
}
