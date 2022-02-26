using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolveWordle
{
    public class WordleSolver
    {
        public WordleDictionary WordleDictionary;
        public List<WordToSolve> WordsToSolve;
        public List<AttemptResults> WordToSolveAttemptResults;
        public StringBuilder WordsGuessed;
        public StringBuilder WordsGuessedForAllInputWords;
        public int WordAttemptCounter = 0;
        public int SolvedCounter = 0;
        public int WordToSolveCounter = 0;
        private const string FirstGuess = "AROSE";
        private const string SecondGuess = "UNLIT";

        public WordleSolver(List<WordToSolve> wordsToSolve)
        {
            WordsToSolve = wordsToSolve;            
        }

        public GameOverSummary SolveWordle()
        {
            WordsGuessedForAllInputWords = new StringBuilder();
            
            foreach (WordToSolve wordToSolve in WordsToSolve)
            {
                WordToSolveCounter++;
                WordleDictionary = new WordleDictionary();
                WordsGuessed = new StringBuilder();
                WordToSolveAttemptResults = new List<AttemptResults>();
                WordAttemptCounter = 0;

                AttemptResults firstAttemptResults = FirstAttemptToSolve(wordToSolve);

                if (firstAttemptResults.GuessedCorrectly == true)
                {
                    SolvedCounter++;
                    continue;
                }
                
                AttemptResults secondAttemptResults = SecondAttemptToSolve(wordToSolve, firstAttemptResults); 
                
                if (secondAttemptResults.GuessedCorrectly == true)
                {
                    SolvedCounter++;
                    continue;
                }

                AttemptResults attemptResults = secondAttemptResults;

                while (WordAttemptCounter < 6)
                {
                    attemptResults = AttemptToSolve(wordToSolve, attemptResults);

                    if (attemptResults.GuessedCorrectly)
                    {
                        SolvedCounter++;
                        break;
                    }
                }

                WordsGuessedForAllInputWords.AppendLine(WordsGuessed.ToString());
            }

            return new GameOverSummary 
            {
                NumberOfSolvedWords = SolvedCounter,
                WordsGuessedForAllInputWords = WordsGuessedForAllInputWords.ToString(),
                NumberOfInputWords = WordsToSolve.Count()
            };
        }

        private AttemptResults FirstAttemptToSolve(WordToSolve wordToSolve)
        {
            WordAttemptCounter++;
            string guessWord = FirstGuess;
            AttemptResults firstAttemptResults = wordToSolve.CheckIfGuessedCorrectly(guessWord);
            WordToSolveAttemptResults.Add(firstAttemptResults);
            WordsGuessed.Append(firstAttemptResults.GuessedWord);
            
            return firstAttemptResults;
        }

        private AttemptResults SecondAttemptToSolve(WordToSolve wordToSolve, AttemptResults firstAttemptResults)
        {
            WordAttemptCounter++;
            string guessWord = SecondGuess;

            AttemptResults secondAttemptResults = wordToSolve.CheckIfGuessedCorrectly(guessWord);
            WordToSolveAttemptResults.Add(secondAttemptResults);

            WordsGuessed.Append(", ");
            WordsGuessed.Append(SecondGuess);

            if (secondAttemptResults.GuessedCorrectly)
            {
                return secondAttemptResults;
            }           

            char[] firstLocationRegex = firstAttemptResults.RegexLocation.ToCharArray();
            char[] secondLocationRegex = secondAttemptResults.RegexLocation.ToCharArray();

            for (int i = 0; i < 5; i++)
            {
                if (firstLocationRegex[i] != '.')
                {
                    secondLocationRegex[i] = firstLocationRegex[i];
                }
            }

            secondAttemptResults.RegexLocation = new string(secondLocationRegex);
            secondAttemptResults.CharInclude += firstAttemptResults.CharInclude;

            return secondAttemptResults;
        }

        private AttemptResults AttemptToSolve(WordToSolve wordToSolve, AttemptResults previousAttemptResults)
        {
            WordAttemptCounter++;
            string guessWord = WordleDictionary.GetNextGuessWord(previousAttemptResults.RegexLocation, previousAttemptResults.CharInclude, previousAttemptResults.CharExclude, previousAttemptResults.GuessedWord);
            AttemptResults newAttemptResults = wordToSolve.CheckIfGuessedCorrectly(guessWord);
            WordToSolveAttemptResults.Add(newAttemptResults);

            WordsGuessed.Append(", ");
            WordsGuessed.Append(guessWord);

            newAttemptResults.CharInclude += previousAttemptResults.CharInclude;

            return newAttemptResults;
        }
    }
}
