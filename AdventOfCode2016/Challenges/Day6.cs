using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    public class Day6 : AdventOfCodeChallenge
    {
        public Day6(): base(6, true, true) { }

        public override string FirstPuzzle()
        {            
            // Split the input by lines
            string[] lines = Input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            string message = String.Empty; // container for the message

            // We start by running through the 0 index of the string (first letter)
            // Then second so on
            for(int i = 0; i < 8; i++) 
            {
                // We create a dictionary to keep count of how many times the different
                // chars are being used for the char position
                Dictionary<char, int> dic = new Dictionary<char, int>();

                foreach (var line in lines) // We run through the lines
                {
                    char c = line[i]; // The char at the position

                    // If we haven't met the char yet we add it with a 1 counter
                    if (!dic.ContainsKey(c))
                        dic.Add(c, 1);
                    // If we have met it we just count up
                    else
                        dic[c]++;
                }

                // We find the most used char in the dictionary
                var mostUsedChar = dic.OrderByDescending(d => d.Value).First().Key;
                // And add it to the message
                message += mostUsedChar;
            }

            return message;
        }

        // This puzzle is just a copy of puzzle one allmost.
        // Te only thing changed is when we order the dictionary
        // var mostUsedChar = dic.OrderBy(d => d.Value).First().Key;
        // This orders it by least used instead of most used we use in Puzzle1
        public override string SecondPuzzle()
        {
            // Split the input by lines
            string[] lines = Input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            string message = String.Empty; // container for the message

            // We start by running through the 0 index of the string (first letter)
            // Then second so on
            for (int i = 0; i < 8; i++)
            {
                // We create a dictionary to keep count of how many times the different
                // chars are being used for the char position
                Dictionary<char, int> dic = new Dictionary<char, int>();

                foreach (var line in lines) // We run through the lines
                {
                    char c = line[i]; // The char at the position

                    // If we haven't met the char yet we add it with a 1 counter
                    if (!dic.ContainsKey(c))
                        dic.Add(c, 1);
                    // If we have met it we just count up
                    else
                        dic[c]++;
                }

                // We find the least used char in the dictionary
                // THe only difference from Puzzle1
                var leastUsedChar = dic.OrderBy(d => d.Value).First().Key;
                // And add it to the message
                message += leastUsedChar;
            }

            return message;
        }
    }
}
