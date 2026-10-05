using NonogramModel.GameStates;
using System;
using System.Collections.Generic;
using System.Text;

namespace NonogramModel
{
    public interface INonogram
    {
        IGameState? GameState { get; set; }

        void LoadGame();
        void SaveGame();
        void NewGame();
    }
}
