using System;
using System.Collections.Generic;
using System.Text;

namespace NonogramModel.Persistence
{
    public class NonogramFileData : IDisposable
    {
        public int[] Board { get; set; }
        public int Lives { get; set; }
        public int ElapsedTime { get; set; }
        public int BoardSize { get; set; }

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
