using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    public class Day2 : AdventOfCodeChallenge
    {
        private const char LEFT = 'L';
        private const char RIGHT = 'R';
        private const char UP = 'U';
        private const char DOWN = 'D';

        private const char BLANK = ' ';

        public Day2() : base(2, true, true) { }

        public override string FirstPuzzle()
        {
            //  The keypad       
            //  | 1 | 2 | 3 |   |0,0|0,1|0,2| 
            //  | 4 | 5 | 6 |   |1,0|1,1|1,2|
            //  | 7 | 8 | 9 |   |2,0|2,1|2,2|
            //
            // To go horizontal we need to increase the second value in the 2d array
            // Increase go right, decrease goes left
            // To go vertical we need to increase the first valeu in the 2d array
            // Increase goes down, decrease goes up
            int[,] keypad = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            string keyCode = String.Empty; // Will hold the code to use
            // Splitting up instructions - each line is an instruction
            string[] instructions = Input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            // Starting position number 5
            int upDown = 1;
            int leftRight = 1;

            // Run through the instructions for each number to the door
            foreach (var instructionLine in instructions)
            {
                // We find out where we need to go on the keypad
                // We also need to stay inbound if we are max to
                // the right we can't go further.
                foreach (var instruction in instructionLine)
                {
                    if (instruction == RIGHT && leftRight < 2)
                        leftRight++;

                    else if (instruction == LEFT && leftRight > 0)
                        leftRight--;

                    else if (instruction == UP && upDown > 0)
                        upDown--;

                    else if (instruction == DOWN && upDown < 2)
                        upDown++;
                }

                keyCode += keypad[upDown, leftRight];
            }

            return keyCode;
        }

        public override string SecondPuzzle()
        {
            // The changed key pad
            // |   |   | 1 |   |   |   |0,0|0,1|0,2|0,3|0,4|
            // |   | 2 | 3 | 4 |   |   |1,0|1,1|1,2|1,3|1,4|
            // | 5 | 6 | 7 | 8 | 9 |   |2,0|2,1|2,2|2,3|2,4|
            // |   | A | B | C |   |   |3,0|3,1|3,2|3,3|3,4|
            // |   |   | D |   |   |   |4,0|4,1|4,2|4,3|4,4|
            //
            // To go horizontal we need to increase the second value in the 2d array
            // Increase go right, decrease goes left
            // To go vertical we need to increase the first valeu in the 2d array
            // Increase goes down, decrease goes up
            // 
            // The keypad uses characters instead of just numbers 
            char[,] keypad = { 
                { BLANK, BLANK, '1', BLANK, BLANK },
                { BLANK, '2', '3', '4', BLANK },
                { '5', '6', '7', '8', '9' },
                { BLANK, 'A', 'B', 'C', BLANK },
                { BLANK, BLANK, 'D', BLANK, BLANK }
            };

            string keyCode = String.Empty; // Will hold the code to use
            // Splitting up instructions - each line is an instruction
            string[] instructions = Input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            // Starting position number 5 has changed
            int upDown = 2;
            int leftRight = 0;

            // Run through the instructions for each number to the door
            foreach (var instructionLine in instructions)
            {
                // This time we need to also check if we hit a blank because that illegal
                // and the size has changed
                foreach (var instruction in instructionLine)
                {
                    if (instruction == RIGHT && leftRight < 4 && keypad[upDown, leftRight+1] != BLANK)
                        leftRight++;

                    else if (instruction == LEFT && leftRight > 0 && keypad[upDown, leftRight-1] != BLANK)
                        leftRight--;

                    else if (instruction == UP && upDown > 0 && keypad[upDown-1, leftRight] != BLANK)
                        upDown--;

                    else if (instruction == DOWN && upDown < 4 && keypad[upDown+1, leftRight] != BLANK)
                        upDown++;
                }

                keyCode += keypad[upDown, leftRight];
            }

            return keyCode;
        }
    }
}
