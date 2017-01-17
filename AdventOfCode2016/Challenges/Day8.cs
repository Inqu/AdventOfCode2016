using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    public class Day8 : AdventOfCodeChallenge
    {
        public Day8() : base(8, false, false) { }

        public override string FirstPuzzle()
        {
            string[] lines = Input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            bool[,] screen = new bool[6, 50];

            foreach(var line in lines)
            {
                if(line.StartsWith("rect "))
                {
                    var grid = line.Replace("rect ", String.Empty);
                    var values = grid.Split('x')

                }
            }


            throw new NotImplementedException();
        }

        public override string SecondPuzzle()
        {
            throw new NotImplementedException();
        }
    }
}
