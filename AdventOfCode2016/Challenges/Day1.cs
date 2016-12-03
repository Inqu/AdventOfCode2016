using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    public class Day1 : AdventOfCodeChallenge
    {
        private const char RIGHT = 'R';
        private const char LEFT = 'L';

        private const string MEMORY_FORMAT = "x{0} y{1}"; // Used for keys in memory

        public Day1() : base(1, true, true) { }

        public override string FirstPuzzle()
        {
            // Coordinates used to find out where we are going
            int x = 0;
            int y = 0;

            var headingDirection = Direction.North; // We are starting heading North
            string[] instructions = Input.Split(','); // Splitting up instructions

            foreach(var i in instructions) // Running through the instructions
            {
                // Trim it for spaces
                var instruction = i.Trim();
                // Containing char to go left or right E.G [R]4 [R] is right
                var turnInstruction = instruction[0];
                // How many steps E.G R[4] [4] is steps
                var stepsInstruction = Int32.Parse(instruction.Substring(1));
                // Find directions according to previous
                headingDirection = FindNewDirection(headingDirection, turnInstruction);

                // Filling the coordinates depending on direction and steps
                switch (headingDirection)
                {
                    case Direction.North: // North is up the x-axis
                        x += stepsInstruction;
                        break;
                    case Direction.South: // South is down the x-axis
                        x -= stepsInstruction;
                        break;
                    case Direction.East: // East is right on y-axis
                        y += stepsInstruction;
                        break;
                    case Direction.West: // West is left on y-axis
                        y -= stepsInstruction;
                        break;
                    default:
                        throw new InvalidOperationException("Couldn't find direction");
                }
            }

            return (ConvertToPositive(x) + ConvertToPositive(y)).ToString(); // The result
        }

        // The second puzzle is much like the first one but we need to
        // A. know where we are going (memory)
        // B Instead of summin up we need to take each step
        public override string SecondPuzzle()
        {
            // Coordinates used to find out where we are going
            int x = 0;
            int y = 0;

            // Memory of were we have gone
            List<string> memory = new List<string>();
            
            // Adding initial position to memory
            AddToMemory(memory, x, y);

            var headingDirection = Direction.North; // We are starting heading North
            string[] instructions = Input.Split(','); // Splitting up instructions

            foreach (var ins in instructions) // Running through the instructions
            {
                var instruction = ins.Trim(); // Trim it for spaces
                // Containing char to go left or right E.G [R]4 [R] is right
                var turnInstruction = instruction[0];
                // How many steps E.G R[4] [4] is steps
                var stepsInstruction = Int32.Parse(instruction.Substring(1));
                // Find directions according to previous
                headingDirection = FindNewDirection(headingDirection, turnInstruction);

                // We are counting steps this time, we cannot do a full add because
                // we need to find the place where we first crossed paths
                for (int i = 0; i < stepsInstruction; i++)
                {
                    // Filling the coordinates depending on direction and steps
                    switch (headingDirection)
                    {
                        case Direction.North: // North is up the x-axis
                            x++;
                            break;
                        case Direction.South: // South is down the x-axis
                            x--;
                            break;
                        case Direction.East: // East is right on y-axis
                            y++;
                            break;
                        case Direction.West: // West is left on y-axis
                            y--;
                            break;
                        default:
                            throw new InvalidOperationException("Couldn't find direction");
                    }

                    // If the coordinate is in memory we have found the path where we crossed
                    if (IsInMemory(memory, x, y))
                        return (ConvertToPositive(x) + ConvertToPositive(y)).ToString();

                    AddToMemory(memory, x, y); // Else we add it to memory
                }
            }

            throw new CouldntFindAnswerException(); // We didnt find an answer
        }

        /// <summary>
        /// Adding coordinate to memory
        /// </summary>
        /// <param name="memory"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private static void AddToMemory(List<string> memory, int x, int y)
        {
            string memoryKey = String.Format(MEMORY_FORMAT, x, y);
            memory.Add(memoryKey);
        }

        /// <summary>
        /// Find out if coordinate is in memory
        /// </summary>
        /// <param name="memory"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool IsInMemory(List<string> memory, int x, int y)
        {
            string memoryKey = String.Format(MEMORY_FORMAT, x, y);
            return memory.Contains(memoryKey);
        }

        /// <summary>
        /// Helper enum to find out where we are heading
        /// </summary>
        private enum Direction
        {
            North, South, East, West
        }

        /// <summary>
        /// FInd out witch direction to go E.G. if looking west and we
        /// need to go right the direction we are heading north
        /// </summary>
        /// <param name="currentDirection"></param>
        /// <param name="turnInstruction"></param>
        /// <returns></returns>
        private Direction FindNewDirection(Direction currentDirection, char turnInstruction)
        {
            if(currentDirection == Direction.North)
            {
                if (turnInstruction == RIGHT)
                    return Direction.East;
                if (turnInstruction == LEFT)
                    return Direction.West;
            }
            else if(currentDirection == Direction.South)
            {
                if (turnInstruction == RIGHT)
                    return Direction.West;
                if (turnInstruction == LEFT)
                    return Direction.East;
            }
            else if (currentDirection == Direction.West)
            {
                if (turnInstruction == RIGHT)
                    return Direction.North;
                if (turnInstruction == LEFT)
                    return Direction.South;
            }
            else if (currentDirection == Direction.East)
            {
                if (turnInstruction == RIGHT)
                    return Direction.South;
                if (turnInstruction == LEFT)
                    return Direction.North;
            }

            throw new ArgumentException("Couldnt find direction");
        }

        /// <summary>
        /// Turn a negative number into a positive number
        /// E.G. -100 becomes 100, 100 should still be 100
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private int ConvertToPositive(int i)
        {
            if (i > 0)
                return i;

            return i * -1;
        }
    }
}
