using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    class Program
    {
        static void Main(string[] args)
        {
            // List of challenges to run
            List<AdventOfCodeChallenge> challengesToRun = new List<AdventOfCodeChallenge>() { new Day1(), new Day2(), new Day3(), new Day4(), /* new Day5() this one is slow*/ new Day6()  };
            
            // Some simple console UI
            Console.Out.WriteLine("---------------------------------------------------");
            foreach (var challenge in challengesToRun)
            {
                Console.Out.WriteLine("\t Challenge day {0}", challenge.Day);
                Console.Out.Write("\t\t Answer one is: ");
                GreenRedSwitch(challenge.FirstCompleted);

                try
                {
                    Console.Write(challenge.FirstPuzzle());
                }
                catch (Exception e)
                {
                    Console.Write("Exception: {0}", e.Message);
                }

                Console.ResetColor();
                Console.WriteLine("");
                
                
                Console.Out.Write("\t\t Answer two is: ");
                GreenRedSwitch(challenge.SecondCompleted);

                try {
                    Console.Write(challenge.SecondPuzzle());
                }
                catch(Exception e)
                {
                    Console.Write("Exception: {0}", e.Message);
                }
                
                Console.ResetColor();
                Console.WriteLine("");
                Console.Out.WriteLine("---------------------------------------------------");
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Switches between green (if completed) red (if not)
        /// </summary>
        /// <param name="b"></param>
        private static void GreenRedSwitch(bool b)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            if (b)
                Console.BackgroundColor = ConsoleColor.DarkGreen;
        }
    }
}