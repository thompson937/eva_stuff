using System;
using System.Collections.Generic;
using System.Text;

namespace NonogramModel.Persistence
{
    public class NonogramFileData : IDisposable
    {
        public int[] Board { get; }
        public int Lives { get; }
        public int ElapsedTime { get; }
        public int BoardSize { get; }

        public NonogramFileData(int[] board, int lives, int elapsedTime, int boardSize)
        {
            Board = board;
            Lives = lives;
            ElapsedTime = elapsedTime;
            BoardSize = boardSize;
        }

        public void Dispose() { }
    }
}
