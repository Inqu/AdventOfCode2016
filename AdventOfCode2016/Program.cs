﻿using System;
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
            List<AdventOfCodeChallenge> challenges = new List<AdventOfCodeChallenge>() { new First(), new BaseToCopy() };

            Console.Out.WriteLine("---------------------------------------------------");
            foreach (var challenge in challenges)
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

        private static void GreenRedSwitch(bool b)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            if (b)
                Console.BackgroundColor = ConsoleColor.DarkGreen;
        }
    }
}