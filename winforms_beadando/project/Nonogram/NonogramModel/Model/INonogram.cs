using NonogramModel.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace NonogramModel.Model
{
    public interface INonogram : IDisposable
    {
        int ElapsedTime { get; set; }

        void SetBoard(int x, int y, CellCategory type);

        void SaveGame(INonogramFileManager fileManager);

        void Initialise(NonogramFileData data);

        event EventHandler? WinCondition;
        event EventHandler? LoseCondition;
        event EventHandler<int>? RowCleared;
        event EventHandler<int>? ColumnCleared;
        event EventHandler<int>? LivesSet;
        event EventHandler<CellChangeEventArgs>? CellChanged;
        event EventHandler<BoardInitialArgs>? ModelReady;
    }
}
