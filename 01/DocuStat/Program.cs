using DocuStat.Model;

namespace DocuStat
{
    class Program
    {
        static void Main(string[] args)
        {
            string path;
            do
            {
                Console.Write("Please enter a valid file path: ");
                path = Console.ReadLine() ?? "";
            }
            while (Path.GetExtension(path) != ".txt" || !File.Exists(path));

            IDocumentStatistics stat = new DocumentStatistics(path);
            stat.MinWordLength = 6;
            stat.MinWordOccurence = 6;

            try
            {
                stat.Load();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            foreach (var item in stat.FilteredWordCount)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
        }
    }
}
