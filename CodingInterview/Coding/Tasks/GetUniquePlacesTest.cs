using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class GetUniquePlacesTest
    {
        [TestMethod]
        [DataRow(new string[]{"2","10 22","E 2", "N 1"}, 4)]
        [DataRow(new string[]{"5","10 22","E 2", "N 1","W 1", "S 2"}, 6)]
        public void Test(string[] instructions, int expected)
        {
            var result = GetUniquePlaces(instructions);

            Assert.AreEqual(expected, result);
        }

        private static readonly Dictionary<char, (int column, int row)> Direction = new Dictionary<char, (int, int)>
        {
            ['E'] = (1,0),
            ['W'] = (-1, 0),
            ['S'] = (0,-1),
            ['N'] = (0,1)
        };

        public int GetUniquePlaces(string[] instructions)
        {
            string[] splittedPoints = instructions[1].Split(' ');
            (int column, int row) robotLocation = (int.Parse(splittedPoints[0]), int.Parse(splittedPoints[1]));
            List<(char, int)> robotInstructions = new List<(char, int)>(instructions.Length - 2);
            for (int i = 2; i < instructions.Length; i++)
            {
                var instruction = instructions[i];
                (char, int) direction = (instruction[0], int.Parse(instruction.Substring(2)));
                robotInstructions.Add(direction);
            }

            int cleanedPlaces = 1;
            HashSet<(int, int)> visited = new HashSet<(int, int)>();
            visited.Add(robotLocation);

            foreach ((char direction, int steps) robotInstruction in robotInstructions)
            {
                (int column, int row) direction = Direction[robotInstruction.direction];
                for (int i = 0; i < robotInstruction.steps; i++)
                {
                    robotLocation.column += direction.column;
                    robotLocation.row += direction.row;
                    if (!visited.Contains(robotLocation))
                    {
                        visited.Add(robotLocation);
                        cleanedPlaces++;
                    }
                }
            }

            return cleanedPlaces;
        }
    }
}
