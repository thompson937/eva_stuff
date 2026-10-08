using NonogramModel.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace NonogramModel.Model
{
    public interface IGameState
    {
        int ElapsedTime { get; set; }

        void SetBoard(int x, int y, CellCategory type);

        void SaveGame(INonogramFileManager fileManager);

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
