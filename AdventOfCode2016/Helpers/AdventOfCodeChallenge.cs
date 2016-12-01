using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace AdventOfCode2016
{
    public abstract class AdventOfCodeChallenge
    {
        public int Day { get; set; }
        public bool FirstCompleted { get; set; }
        public bool SecondCompleted { get; set; }

        public string Input { get; set; }

        public AdventOfCodeChallenge(int day, bool firstCompleted = false, bool secondCompleted = false)
        {
            Day = day;
            FirstCompleted = firstCompleted;
            SecondCompleted = secondCompleted;
        }

        public abstract string FirstPuzzle();
        public abstract string SecondPuzzle();
    }
}
