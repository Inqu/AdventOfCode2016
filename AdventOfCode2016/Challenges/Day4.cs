using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    public class Day4 : AdventOfCodeChallenge
    {
        private const int A_ASCII_VALUE = 97; // lowercase a ascii value
        private const int Z_ASCII_VALUE = 122; // lowercase z ascii value
        // How many chars in alphabet
        private const int CHARS_IN_APLHABET = Z_ASCII_VALUE - A_ASCII_VALUE + 1;

        public Day4() : base(4, true, true) { }

        public override string FirstPuzzle()
        {
            // Split the input by lines
            string[] lines = Input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            int totalSum = lines.Sum(GetSum); // Get total sum for each valid room

            return totalSum.ToString();
        }

        public static int GetSum(string inputLine)
        {
            // Regex we use to split up the input like below in parantheses
            // (gbc-frperg-pubpbyngr-znantrzrag)-(377)[(rgbnp)]
            Regex regEx = new Regex("(.*)-(\\d*)\\[(.*)\\]");
            var match = regEx.Match(inputLine);

            // Group 1 now contains the encryptet name
            // E.G. gbc-frperg-pubpbyngr-znantrzrag
            string encryptetName = match.Groups[1].Value;
            // Group 2 contains the sector id
            // E.G. 377
            int sectorId = Int32.Parse(match.Groups[2].Value);
            // Group 3 contains the checksum
            // E.G. rgbnp
            string checksum = match.Groups[3].Value;


            // We remove the dashes from the encryptet name
            encryptetName = encryptetName.Replace("-", "");

            // We find the most used letters and sort them
            var calculatedChecksum =
                encryptetName.ToCharArray()
                    .OrderBy(c => c) // Order alphabetical
                    .GroupBy(c => c) // Group out duplicates
                    .OrderByDescending(c => c.Count()) // Order by number of chars
                    .Take(5) // take 5
                    .Aggregate(String.Empty, (current, topFiveLetter) => current + topFiveLetter.Key); // Make into a new string

            // If the calculated checksum and the checksum match
            // the room is valid and we return the sector id value
            if (calculatedChecksum == checksum)
                return sectorId;

            return 0; // If not valid we return 0
        }

        public override string SecondPuzzle()
        {
            // Split input by newline
            string[] lines = Input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            
            // This time i didnt care about if the room was valid or not
            foreach (var line in lines) // running through the lines
            {
                // Regex we use to split up the input like below in parantheses
                // (gbc-frperg-pubpbyngr-znantrzrag)-(377)[(rgbnp)]
                // We dont need the checksum in this though
                Regex regEx = new Regex("(.*)-(\\d*)\\[(.*)\\]");
                var match = regEx.Match(line);

                // Group 1 now contains the encryptet name
                // E.G. gbc-frperg-pubpbyngr-znantrzrag
                string encryptetName = match.Groups[1].Value;
                // Group 2 contains the sector id
                // E.G. 377
                int sectorId = Int32.Parse(match.Groups[2].Value);
                // We remove the dashes from the encryptet name
                encryptetName = encryptetName.Replace("-", "");
                // Container for the decryptet name
                string decryptetName = String.Empty;

                // Run through the chars in encryption name
                foreach (var character in encryptetName)
                {
                    // We find the decimal value of the character
                    int characterDecimalValue = (int) character;
                    // Finding out how many chars to move to decrypt it
                    int charsToMove = sectorId % CHARS_IN_APLHABET;

                    // Finding the new position of the character
                    char decryptedCharValue = (char) (characterDecimalValue + charsToMove);

                    // If it passes the z character we substract the number of chars in alphabet
                    // E.G. z (122) + 1 = 123 => 123 - 26 = 97 (a)
                    if ((int) decryptedCharValue > Z_ASCII_VALUE)
                        decryptedCharValue = (char) (decryptedCharValue - CHARS_IN_APLHABET);

                    // And add this to our decryptet string
                    decryptetName += decryptedCharValue;
                }

                // First i thought we were looking for the room named: northpoleobjects
                // so my if statement was decryptetValue == "northpoleobjects"
                // this didnt return anything so i did a contains and found a northpoleobjectstorage name
                // so i changed it to a contains
                if (decryptetName.Contains("northpoleobjects"))
                    return sectorId.ToString();
            }

            throw new CouldntFindAnswerException();
        }
    }
}
