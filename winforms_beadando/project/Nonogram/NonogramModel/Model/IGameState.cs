using System;
using System.Collections.Generic;
using System.Text;

namespace NonogramModel.Model
{
    public interface IGameState
    {
        int LivesCounter { get; set; }
        int TimeCounter { get; set; }

        int boardSize { get; }

        int GetBoard(int x, int y);
        void SetBoard(int x, int y, CellCategory type);

        string GetRowNumbers(int y);
        string GetColumnNumbers(int x);


        event EventHandler? WinCondition;
        event EventHandler? LoseCondition;
        event EventHandler<int>? RowCleared;
        event EventHandler<int>? ColumnCleared;
        event EventHandler<int>? LivesSet;
        event EventHandler<CellChangeEventArgs>? CellChanged;
    }
}
