namespace hanjie_prototype
{
    public class Nonogram
    {
        private readonly int _imageSize;

        private readonly bool[] _originalImage =
        {
            false, false, true, false, false,
            false, true, false, true, false,
            true, false, false, false, true,
            true, true, false, true, true,
            false, false, true, false, false
        };

        private bool[] _guesses;

        public Nonogram()
        {
            _imageSize = 5;
            _guesses = new bool[_imageSize*_imageSize];

            System.Console.WriteLine("The board: ");

            for (int i = 0; i < _originalImage.Length; i++)
            {
                _guesses[i] = false;
            }

            _PrintBoard(_originalImage);
        }

        public void SubmitGuess(int x, int y)
        {
            if (x > _imageSize -1 || x < 0 || y > _imageSize - 1 || y < 0) throw new ArgumentException($"Supplied guess location {x}:{y} not on image area");

            int n = y * _imageSize + x;

            _guesses[n] = true;

            //System.Console.WriteLine($"New guess: {x}:{y}");

            if (CheckValidity())
            {
                System.Console.WriteLine("Correct:");
                _PrintBoard(_guesses);
            }
            else
            {
                System.Console.WriteLine("Incorrect:");
                _PrintBoard(_guesses);
            }

            if (ExactMatch())
            {
                System.Console.WriteLine("YOU'VE WON");
            }

        }

        private string GetRow(int y)
        {
            string output = "";
            int continuity = 0;
            for (int i = y * _imageSize; i < y * _imageSize + _imageSize; i++)
            {
                if (_originalImage[i])
                {
                    continuity++;
                }
                else
                {
                    if (continuity > 0) output += continuity.ToString() + " ";
                    continuity = 0;
                }
            }

            if (continuity > 0) output += continuity.ToString() + " ";

            return output;
        }

        private string GetColumn(int x)
        {
            string output = "";
            int continuity = 0;

            for (int i = x; i < _imageSize * _imageSize + x; i += _imageSize)
            {
                if (_originalImage[i])
                {
                    continuity++;
                }
                else
                {
                    if (continuity > 0) output += continuity.ToString() + " ";
                    continuity = 0;
                }
            }

            if (continuity > 0) output += continuity.ToString() + " ";

            return output;
        }


        private bool CheckValidity()
        {
            for (int i = 0; i < _originalImage.Length; i++)
            {
                if (_guesses[i] && !_originalImage[i])
                {
                    _guesses[i] = false;
                    return false;
                }
            }

            return true;
        }

        private bool ExactMatch()
        {
            for (int i = 0; i < _originalImage.Length; i++)
            {
                if (_guesses[i] != _originalImage[i])
                {
                    return false;
                }
            }

            return true;
        }


        private void _PrintBoard(bool[] board)
        {
            for (int i = 0; i < board.Length; i++)
            {
                Console.Write((board[i] == true) ? "#X" : ". ");
                if (i % _imageSize == _imageSize - 1)
                {
                    System.Console.WriteLine(" " + GetRow(i / _imageSize));
                }
            }

            for (int i = 0; i < _imageSize; i++)
            {
                Console.Write(GetColumn(i) + ",");
            }
            System.Console.WriteLine();
        }


    }
}