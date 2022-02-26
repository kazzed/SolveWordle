using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SolveWordle
{
    public class WordleDictionary
    {
        IEnumerable<WordleWord> WordleWords { get; set; }

        public WordleDictionary()
        {
            string projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string dictionaryPath = Path.Combine(projectFolder, @"Data\dictionary.csv");

            WordleWords = File.ReadAllLines(dictionaryPath)
                .Skip(1)
                .Select(x => x.Split(','))
                .Select(x => new WordleWord
                {
                    Word = x[0],
                    UniqueLetters = int.Parse(x[1]),
                    Score = int.Parse(x[2])
                });

            WordleWords = WordleWords.OrderByDescending(w => w.Score);
        }

        public string GetNextGuessWord(string regexLocation, string charInclude, string charExclude, string guessedWord)
        {
            
            WordleWords = from w in WordleWords where Regex.IsMatch(w.Word, regexLocation) select w;
            WordleWords = WordleWords.Where(w => !w.Word.Contains(guessedWord));

            if (!string.IsNullOrEmpty(charInclude))
            {
                char[] includeChars = charInclude.ToCharArray();

                foreach (char charToInclude in includeChars)
                {
                    WordleWords = WordleWords.Where(w => w.Word.Contains(charToInclude));
                }
            }

            if (!string.IsNullOrEmpty(charExclude))
            {
                char[] excludeChars = charExclude.ToCharArray();

                foreach (char charToExclude in excludeChars)
                {
                    WordleWords = WordleWords.Where(w => !w.Word.Contains(charToExclude));
                }
            }

            return WordleWords.FirstOrDefault().Word;
        }
    }
}
