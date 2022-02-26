using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWordle
{
    public class WordleWord
    {
        public string Word { get; set; }
        public int UniqueLetters { get; set; }
        public int Score { get; set; }
    }
}
