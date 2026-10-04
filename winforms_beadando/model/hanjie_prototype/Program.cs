namespace hanjie_prototype
{
    class Program
    {
        static void Main(string[] args)
        {
            Nonogram n = new Nonogram();

            while (true)
            {
                string input = Console.ReadLine();

                int x, y;

                int.TryParse(input.Split(' ')[0], out x);
                int.TryParse(input.Split(' ')[1], out y);

                n.SubmitGuess(x, y);
            }
        }
    }  
}

