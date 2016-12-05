using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Reflection;

namespace AdventOfCode2016
{
    /// <summary>
    /// Abstract class for a new challenge.
    /// </summary>
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

            // This is where my input is
            var inputPath = Path.GetFullPath(String.Format(@"C:\Users\nis\Documents\visual studio 2017\Projects\AdventOfCode2016\AdventOfCode2016\Input\{0}.txt", Day));
            FileInfo fNfo = new FileInfo(inputPath); // Creating FileInfo for it

            if (fNfo.Exists) // If file exists
            {
                var stream = fNfo.OpenRead(); // Create stream
                using (TextReader reader = new StreamReader(stream))
                {
                    // Set input to the file content
                    Input = reader.ReadToEnd();
                }
            }
                
        }

        public abstract string FirstPuzzle();
        public abstract string SecondPuzzle();
    }
}
