using System;
using System.Collections.Generic;
using System.Text;

namespace NonogramModel.Persistence
{
    public class NonogramFileManager : INonogramFileManager
    {
        readonly string _path;

        public NonogramFileManager(string path)
        {
            _path = path;
        }

        public NonogramFileData Load()
        {
            using (StreamReader sr = new StreamReader(_path))
            {
                try
                {
                    int lives, elapsedTime;
                    int.TryParse(sr.ReadLine(), out lives);
                    int.TryParse(sr.ReadLine(), out elapsedTime);

                    string[] stringBoardValues = sr.ReadToEnd().Split(' ');

                    int boardSize = (stringBoardValues.Length) switch
                    {
                        25 => 5,
                        100 => 10,
                        225 => 15,
                        _ => throw new NonogramFileException($"Invalid board size: {stringBoardValues.Length}")
                    };

                    int[] boardValues = new int[stringBoardValues.Length];

                    for (int i = 0; i < boardValues.Length; i++)
                    {
                        int.TryParse(stringBoardValues[i], out boardValues[i]);
                    }

                    return new NonogramFileData(boardValues, lives, elapsedTime, boardSize);
                }
                catch (Exception ex)
                {
                    throw new NonogramFileException("Loading error: " + ex.Message);
                }
            }
        }

        public void Save(NonogramFileData data)
        {
            using (StreamWriter sw = new StreamWriter(_path))
            {
                try
                {
                    sw.WriteLine(data.Lives);
                    sw.WriteLine(data.ElapsedTime);

                    sw.Write(string.Join(" ", data.Board));
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public void Dispose() { }
    }
}
