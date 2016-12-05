using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace AdventOfCode2016
{
    public class Day5 : AdventOfCodeChallenge
    {
        public Day5() : base(5, false, false) { }

        // Takes a bit time to run (bruteforce)
        public override string FirstPuzzle()
        {
            string pwd = String.Empty; // Container for password
            int i = 0; // Starting index

            while(pwd.Length < 8) // While password not full length
            {
                // Creates input + index string
                string combinedString = String.Format("{0}{1}", Input, i);
                // Gets hash
                string hashedCombined = ComputeMD5Hash(combinedString);
                // if the hash starts with 5 0's
                if (hashedCombined.StartsWith("00000"))
                    pwd += hashedCombined[5]; // add the 6th letter to it

                i++; // Count up
            }

            return pwd;
        }

        public override string SecondPuzzle()
        {
            // Since i need positions pwd is now a char array filled with blank spaces
            char[] pwd = new char[] { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '};
            int i = 0;// Starting index

            // While pwd still have blank spaces
            while (pwd.Contains(' '))
            {
                // Creates input + index string
                string combinedString = String.Format("{0}{1}", Input, i);
                // Gets hash
                string hashedCombined = ComputeMD5Hash(combinedString);
                // if the hash starts with 5 0's
                if (hashedCombined.StartsWith("00000"))
                {
                    int holder; // holds the parsed position
                    var position = hashedCombined[5]; // Get the position value (the 6th letter)
                    // Try to parse it
                    bool succes = Int32.TryParse(position.ToString(), out holder);
                    // If its parseable (not a b c d e f) and under the 9th letter and its
                    // position hasnt been filled
                    if (succes && holder < 8 && pwd[holder] == ' ')
                        pwd[holder] = hashedCombined[6]; // Fill in the 7th letter

                }

                i++; // Count up
            }

            return new string(pwd); // return the password
        }

        /// <summary>
        /// Creates a hash for a string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ComputeMD5Hash(string input)
        {
            MD5 md5 = MD5.Create(); // Makes the md5 object to use

            // Convert input to bytes
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            // Hash the bytes            
            byte[] hash = md5.ComputeHash(inputBytes);

            // Convert back to string
            StringBuilder stb = new StringBuilder();
            foreach (byte b in hash)
            {
                // x2 is hexidecimal in lowercase
                stb.Append(b.ToString("x2"));
            }

            return stb.ToString();
        }
    }
}
