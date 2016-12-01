using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    public class First : AdventOfCodeChallenge
    {
        private char right = 'R';
        private char left = 'L';

        private string memoryFormat = "x{0} y{1}";
        
        public First() : base(1, true, false) {
            Input = "R4, R4, L1, R3, L5, R2, R5, R1, L4, R3, L5, R2, L3, L4, L3, R1, R5, R1, L3, L1, R3, L1, R2, R2, L2, R5, L3, L4, R4, R4, R2, L4, L1, R5, L1, L4, R4, L1, R1, L2, R5, L2, L3, R2, R1, L194, R2, L4, R49, R1, R3, L5, L4, L1, R4, R2, R1, L5, R3, L5, L4, R4, R4, L2, L3, R78, L5, R4, R191, R4, R3, R1, L2, R1, R3, L1, R3, R4, R2, L2, R1, R4, L5, R2, L2, L4, L2, R1, R2, L3, R5, R2, L3, L3, R3, L1, L1, R5, L4, L4, L2, R5, R1, R4, L3, L5, L4, R5, L4, R5, R4, L3, L2, L5, R4, R3, L3, R1, L5, R5, R1, L3, R2, L5, R5, L3, R1, R4, L5, R4, R2, R3, L4, L5, R3, R4, L5, L5, R4, L4, L4, R1, R5, R3, L1, L4, L3, L4, R1, L5, L1, R2, R2, R4, R4, L5, R4, R1, L1, L1, L3, L5, L2, R4, L3, L5, L4, L1, R3";
        }

        public override string FirstPuzzle()
        {
            // Koordinater til hvor vi er henne af
            int x = 0;
            int y = 0;

            var headingDirection = Direction.North; // Vi starter mod nord
            string[] instructions = Input.Split(','); // Deler kommandoer op

            foreach(var i in instructions) // Løber instrukserne igennem
            {
                var instruction = i.Trim(); // Gør den nem og læselig
                var turnInstruction = instruction[0]; // Finder frem til højre eller venstre
                var stepsInstruction = Int32.Parse(instruction.Substring(1)); // hvor mange skridt
                headingDirection = FindNewDirection(headingDirection, turnInstruction); // hvor skal vi hen af i forhold til venstre højre

                // Udfylder koordinater
                if (headingDirection == Direction.North)
                    x += stepsInstruction;
                else if (headingDirection == Direction.South)
                    x -= stepsInstruction;
                else if (headingDirection == Direction.West)
                    y -= stepsInstruction;
                else if (headingDirection == Direction.East)
                    y += stepsInstruction;
            }

            return (ConvertToPositive(x) + ConvertToPositive(y)).ToString();
        }

        public override string SecondPuzzle()
        {
            // Minder meget om den anden, men vi indfører en hukommelse så vi ved hvor vi har været
            // Dette bliver bare en liste af koordinater
            List<string> memory = new List<string>();
            AddToMemory(memory, 0, 0); // Tilføjer start koordinat

            int x = 0;
            int y = 0;

            var headingDirection = Direction.North;
            string[] instructions = Input.Split(','); // Deler kommandoer op

            foreach (var ins in instructions) // Løber dem igennem
            {
                var instruction = ins.Trim(); // Gør den nem og læselig
                var turnInstruction = instruction[0]; // Finder frem til højre eller venstre
                var stepsInstruction = Int32.Parse(instruction.Substring(1)); // hvor mange skridt
                headingDirection = FindNewDirection(headingDirection, turnInstruction); // hvor skal vi hen af

                // Vi kører igennem hvert eneste step nu og gemmer det i hukommelsen
                // Vi tæller bare x og y op i forhold til hvor vi skal hen
                for(int i = 0; i < stepsInstruction; i++)
                {
                    if (headingDirection == Direction.North)
                        x++;
                    else if (headingDirection == Direction.South)
                        x--;
                    else if (headingDirection == Direction.West)
                        y--;
                    else if (headingDirection == Direction.East)
                        y++;


                    // Hvis koordinatet findes i hukommelsen har vi fundet ud af hvor vi skal være
                    if (IsInMemory(memory, x, y))
                    {
                        return (ConvertToPositive(x) + ConvertToPositive(y)).ToString();
                        
                    }

                    AddToMemory(memory, x, y); // ellers tilføjer til hukkomelse
                }
            }

            throw new CouldntFindAnswerException();
        }

        /// <summary>
        /// Tilføjer koordinat til hukommelsen
        /// </summary>
        /// <param name="memory"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void AddToMemory(List<string> memory, int x, int y)
        {
            string memoryKey = String.Format(memoryFormat, x, y);
            memory.Add(memoryKey);
        }

        /// <summary>
        /// Finder frem til om vi allerede har været på koordinatet
        /// </summary>
        /// <param name="memory"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool IsInMemory(List<string> memory, int x, int y)
        {
            string memoryKey = String.Format(memoryFormat, x, y);
            return memory.Contains(memoryKey);
        }

        /// <summary>
        /// Finder frem til hvor vi skal hen udfra hvor vi peger hen
        /// </summary>
        /// <param name="currentDirection"></param>
        /// <param name="turnInstruction"></param>
        /// <returns></returns>
        private Direction FindNewDirection(Direction currentDirection, char turnInstruction)
        {
            if(currentDirection == Direction.North)
            {
                if (turnInstruction == right)
                    return Direction.East;
                if (turnInstruction == left)
                    return Direction.West;
            }
            else if(currentDirection == Direction.South)
            {
                if (turnInstruction == right)
                    return Direction.West;
                if (turnInstruction == left)
                    return Direction.East;
            }
            else if (currentDirection == Direction.West)
            {
                if (turnInstruction == right)
                    return Direction.North;
                if (turnInstruction == left)
                    return Direction.South;
            }
            else if (currentDirection == Direction.East)
            {
                if (turnInstruction == right)
                    return Direction.South;
                if (turnInstruction == left)
                    return Direction.North;
            }

            throw new ArgumentException("Couldnt find direction from given intel");
        }

        /// <summary>
        /// Hurtig hjælper til at finde frem til de forskellige verdenshjørner
        /// </summary>
        private enum Direction
        {
            North, South, East, West
        }

        /// <summary>
        /// Laver et negativt tal om til et positivt
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
