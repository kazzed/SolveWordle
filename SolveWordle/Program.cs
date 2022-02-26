using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SolveWordle
{
    public class Program
    {
        static void Main(string[] args)
        {
            Prompt();
        }

        private static void Prompt()
        {
            Console.WriteLine("Enter the file path for the file containing words to solve.");
            Console.WriteLine(@"Example: C:\TopSecretFiles\WordsToSolve.txt");
            string inputFilePath = Console.ReadLine();
            GameOverSummary gameOverSummary = InputFile(inputFilePath);

            Console.WriteLine("Summary:");
            Console.WriteLine($"{gameOverSummary.NumberOfInputWords} words to solve.");
            Console.WriteLine($"{gameOverSummary.NumberOfSolvedWords} words solved.");

            CreateOutputTextFile(gameOverSummary.WordsGuessedForAllInputWords);
            Console.ReadLine();
        }

        private static GameOverSummary InputFile(string inputFilePath)
        {
            IEnumerable<WordToSolve> wordsFromFile = Enumerable.Empty<WordToSolve>();
            
            try
            {
                wordsFromFile = File.ReadAllLines(inputFilePath)
                .Select(x => new WordToSolve
                {
                    Word = x
                });
            }
            catch
            {
                Console.WriteLine("There was a problem reading the file. Please try again.");
                Console.WriteLine();

                Prompt();
            }           

            if (wordsFromFile.Count() == 0)
            {
                Console.WriteLine("The input file was empty or there was a problem reading the file. Please try again.");
                Console.WriteLine("");
                Prompt();
            }

            List<WordToSolve> wordsToWordle = wordsFromFile.ToList();

            foreach (WordToSolve wordToSolve in wordsToWordle)
            {
                wordToSolve.Word = wordToSolve.Word.ToUpper();
            }

            Console.WriteLine("Imported words successfully");

            WordleSolver solver = new WordleSolver(wordsToWordle);

            Console.WriteLine("Playing game...");

            GameOverSummary gameOverSummary = solver.SolveWordle();

            Console.WriteLine("Game Over!");

            return gameOverSummary;
        }

        private static void CreateOutputTextFile(string attemptedwords)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            filePath = filePath + @"\WordleResults_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
            File.WriteAllText(filePath, attemptedwords);

            Console.WriteLine($"Wordle Results saved to: {filePath}");
        }
    }
}
