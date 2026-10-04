using NonogramModel.GameState;
using System;
using System.Collections.Generic;
using System.Text;

namespace NonogramModel
{
    public interface INonogramModel
    {
        IGameState? gameState { get; }

        void Load();
        void Save();
        void New();
    }
}
