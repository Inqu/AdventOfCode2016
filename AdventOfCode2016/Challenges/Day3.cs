using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    public class Day3 : AdventOfCodeChallenge
    {
        public Day3() : base(3, true, true) { }

        public override string FirstPuzzle()
        {
            int validTriangles = 0; // keeps number of valid triangles

            // Getting the triangles from input new line split
            string[] triangles = Input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (var triangle in triangles) // running through the triangles
            {
                int[] sides = GetSides(triangle); // getting sides
                if (IsValidTriangle(sides)) // if valid
                    validTriangles++; // count up valid triangles
            }

            return validTriangles.ToString();
        }

        public override string SecondPuzzle()
        {
            int validTriangles = 0; // keeps number of valid triangles

            // Getting the triangles from input new line split
            string[] triangles = Input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            for (int i = 0; i < triangles.Length; i += 3) // We now take three triangles at a time
            {
                // Getting the sides for the three triangles
                var fakeFirstTriangle = GetSides(triangles[i]);
                var fakeSecondTriangle = GetSides(triangles[i + 1]);
                var fakeThirdTriangle = GetSides(triangles[i + 2]);

                // Now switching up the triangles so it uses the others columns
                var realFirstTriangle = new int[] {fakeFirstTriangle[0], fakeSecondTriangle[0], fakeThirdTriangle[0]};
                var realSecondTriangle = new int[] { fakeFirstTriangle[1], fakeSecondTriangle[1], fakeThirdTriangle[1] };
                var realThirdTriangle = new int[] { fakeFirstTriangle[2], fakeSecondTriangle[2], fakeThirdTriangle[2] };

                // We check each of the three triangles
                if (IsValidTriangle(realFirstTriangle)) // if valid
                    validTriangles++; // count up valid triangles

                if (IsValidTriangle(realSecondTriangle)) // if valid
                    validTriangles++; // count up valid triangles

                if (IsValidTriangle(realThirdTriangle)) // if valid
                    validTriangles++; // count up valid triangles
            }

            return validTriangles.ToString();
        }

        /// <summary>
        /// Gets the sides from a input line
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int[] GetSides(string input)
        {
            List<int> result = new List<int>(); // Containr for sides

            string[] sides = input.Split(' '); // Splitting up on empty spaces
            foreach (var side in sides) // Running through sides
            {
                int holder; // Holder to see if what we have is a valid number
                bool success = Int32.TryParse(side, out holder); // Try to parse
                if (success) result.Add(holder); // IF succesfull add to the result
            }

            return result.ToArray();
        }

        /// <summary>
        /// Find out if 
        /// </summary>
        /// <param name="sides"></param>
        /// <returns></returns>
        public static bool IsValidTriangle(params int[] sides)
        {
            // A triangle must have 3 sides
            if (sides.Length != 3) throw new InvalidOperationException("Not a triangle");

            Array.Sort(sides); // Sort the array so smallest sides first
            // The two smallest sides must combined be bigger then the biggest
            return sides[0] + sides[1] > sides[2];
        }
    }
}
