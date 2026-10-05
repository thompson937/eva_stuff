using System;
using System.Collections.Generic;
using System.Text;

namespace NonogramModel.GameStates
{
    public interface IGameState
    {
        int LivesCounter { get; set; }
        int TimeCounter { get; set; }

        int boardSize { get; }

        int GetBoard(int x, int y);
        void SetBoard(int x, int y, UserInputType type);

        string GetRowNumbers(int y);
        string GetColumnNumbers(int x);

        event EventHandler? BoardChanged;
    }
}
