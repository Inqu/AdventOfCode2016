using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AdventOfCode2016
{
    /// <summary>
    /// Not the cleanest of code, could use some major refac
    /// Could have used alot more of regex to find what needed
    /// E.G. (.)(.)\1\2 for abba (.)(.)\1 For ABA and so on
    /// </summary>
    public class Day7 : AdventOfCodeChallenge
    {
        public Day7() : base(7, true, true) { }

        public override string FirstPuzzle()
        {
            int tlsSupported = 0; // Holder for lines with tls support
            // Split the input by lines
            string[] lines = Input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            foreach(var line in lines) // Running through the lines
            {
                // Regex to find square brackets
                Regex squareBreacketsRegEx = new Regex(@"\[\w*\]");
                // We find the square brackets
                var hypernets = squareBreacketsRegEx.Matches(line);
                // We replace the brackets with a pipe so we can split it
                var supernetsString = squareBreacketsRegEx.Replace(line, "|");
                // We split it to find the segments without hypernets
                var supernets = supernetsString.Split('|');

                bool hypernetContainsAbba = false; // Holder for if hypernets contains abba
                bool supernetContainsAbba = false; // Holder for if supernets contains abba

                // We run through the hypernets to see if they contain abba
                foreach(Match hypernet in hypernets)
                {
                    // If they do we set hypernetContainsAbba to true
                    // and we dont have to run through the rest
                    if (ContainsAbba(hypernet.Value))
                    {
                        hypernetContainsAbba = true;
                        break;
                    }
                }

                // We run through the supernets to see if they contain abba
                // if they do we set splittedContainsAbba to true and dont have to run through the rest
                foreach (var supernet in supernets)
                {
                    if (ContainsAbba(supernet))
                    {
                        supernetContainsAbba = true;
                        break;
                    }

                }

                // We count tls supported ip up if hypernets doesnt contains abba
                // and supernets does
                if (!hypernetContainsAbba && supernetContainsAbba)
                    tlsSupported++;

            }

            return tlsSupported.ToString();
        }

        public override string SecondPuzzle()
        {
            int sslSupported = 0; // Holder for lines with ssl support
            // Split the input by lines
            string[] lines = Input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (var line in lines)
            {
                // Regex to find square brackets
                Regex squareBreacketsRegEx = new Regex(@"\[\w*\]");
                var hypernets = squareBreacketsRegEx.Matches(line);
                // We replace the brackets with a pipe so we can split it
                var supernetsString = squareBreacketsRegEx.Replace(line, "|");
                // We split it to find the segments without hypernets
                var supernets = supernetsString.Split('|');
                
                if (SupportsSSL(hypernets, supernets))
                    sslSupported++;
            }

            return sslSupported.ToString();
        }

        /// <summary>
        /// Takes the hypernet and supernet portion is input
        /// and checks if it supports SSL according to ABA BAB rule
        /// </summary>
        /// <param name="hypernets"></param>
        /// <param name="supernets"></param>
        /// <returns></returns>
        public static bool SupportsSSL(MatchCollection hypernets, params string[] supernets)
        {
            List<string> abas = GetAbas(supernets); // Get abas from supernets

            List<string> babs = new List<string>(); // Holder fra babs
            foreach (var aba in abas) // We run through the abas
            {
                // Convert it to bab
                var bab = new string(new char[] { aba[1], aba[0], aba[1] });
                babs.Add(bab);
            }

            foreach (Match hypernet in hypernets) // we rung through the matches
            {
                foreach (string bab in babs) // We run through the babs
                {
                    if (hypernet.Value.Contains(bab)) // if a match value contains bab
                        return true; // It supprts SSL
                }
            }

            return false;
        }

        /// <summary>
        /// Finds ABAs in multiple strings
        /// </summary>
        /// <param name="supernets"></param>
        /// <returns></returns>
        public static List<string> GetAbas(params string[] supernets)
        {
            // Container for aba's found
            List<string> abas = new List<string>();

            foreach(var supernet in supernets) // running through the strings to find abas
            {
                for (int i = 0; i + 2 < supernet.Length; i++) // 3 Chars at a time
                {
                    char firstPosition = supernet[i]; // First char in sequence
                    char secondPosition = supernet[i + 1]; // Second char in sequence

                    // If they equal each other continue this in invalid
                    if (firstPosition == secondPosition)
                        continue;

                    if(supernet[i +2] == firstPosition) // If 3 position is equal first
                        abas.Add(supernet.Substring(i, 3)); // We have an aba
                }
            }

            return abas;            
        }

        /// <summary>
        /// Finds out if a string contains ABBA
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool ContainsAbba(string s)
        {
            // We run through each char of the string we start at index 0
            // snd use 4 chars of the string to find the ABBA
            for(int i = 0; i + 3 < s.Length; i++)
            {
                char firstPosition = s[i]; // First char in sequence
                char secondPosition = s[i + 1]; // Second char in sequence

                // If they equal each other continue this in invalid
                if (firstPosition == secondPosition)
                    continue;

                // If third position eqals second position its an abba
                // a(0) b(0+1) b(0+2) a(0+3)
                if (s[i + 2] == secondPosition && s[i + 3] == firstPosition)
                    return true;
            }

            return false;
        }
    }
}
