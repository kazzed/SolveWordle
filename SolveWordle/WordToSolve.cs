using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWordle
{
    public class WordToSolve
    {
        public string Word { get; set; }       

        public AttemptResults CheckIfGuessedCorrectly(string guessWord)
        {
            AttemptResults results = new AttemptResults();
            results.GuessedCorrectly = false;

            char[] answer = Word.ToCharArray();
            char[] guess = guessWord.ToCharArray();
            StringBuilder regexLocation = new StringBuilder();
            StringBuilder charInclude = new StringBuilder();
            StringBuilder charExclude = new StringBuilder();

            if (guessWord == Word)
            {
                results.GuessedCorrectly = true;
                return results;
            }

            for (int i = 0; i < 5; i++)
            {
                if (guess[i] == answer[i])
                {
                    regexLocation.Append(guess[i]);
                }
                else
                {
                    regexLocation.Append('.');
                }
            }

            for (int i = 0; i < 5; i++)
            {
                if (Word.Contains(guess[i]))
                {
                    charInclude.Append(guess[i]);
                }
                else
                {
                    charExclude.Append(guess[i]);
                }
            }

            results.GuessedWord = guessWord;
            results.RegexLocation = regexLocation.ToString();
            results.CharInclude = charInclude.ToString();
            results.CharExclude = charExclude.ToString();

            return results;
        }
    }
}
