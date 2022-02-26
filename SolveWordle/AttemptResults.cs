namespace SolveWordle
{
    public class AttemptResults
    {
        public string GuessedWord { get; set; }
        
        public string RegexLocation { get; set; }

        public string CharInclude { get; set; }

        public string CharExclude { get; set; }

        public bool GuessedCorrectly { get; set; }
    }
}
