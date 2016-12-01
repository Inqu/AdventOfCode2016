using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    public class CouldntFindAnswerException : Exception
    {
        public CouldntFindAnswerException() : base("Couldn't find answer to this")
        {
        }
    }
}
