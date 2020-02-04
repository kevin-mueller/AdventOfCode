using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days.Nineteen
{
    //https://adventofcode.com/2019/day/3
    public class Day3
    {
        public static int Solve(string inputPath)
        {
            string[] wiresRaw = File.ReadAllLines(inputPath);
            var wire1 = new Wire(wiresRaw[0]);
            var wire2 = new Wire(wiresRaw[1]);

            //Find the same visitedPoints
            //Calculate the closest distance to the start.
        }
    }

    internal class Wire
    {
        private readonly List<string> _instructions = new List<string>();

        private List<int> _allPointsVisited = new List<int>();


        public Wire(string inputRaw)
        {
            _instructions = BuildInstructionTable(inputRaw);
            ComputeAllPointsVisited(_instructions);
        }

        private List<string> BuildInstructionTable(string wireData)
        {
            foreach (var item in wireData.Split(','))
            {
                _instructions.Add(item);
            }
            return _instructions;
        }

        private List<int> ComputeAllPointsVisited(List<string> instructionTable)
        {
            //TOOD Check for nullpointer exception.
            //TODO Idealy, scan the input an calculate the size of the matrix.
            bool[,] matrix = new bool[9999, 9999];
            
            //TODO loop through the matrix and set the points visited to true.
            foreach (var item in instructionTable)
            {
                char direction = item[0];
                switch (direction)
                {
                    case 'R':

                        break;
                    case 'U':

                        break;

                    case 'L':

                        break;
                    case 'D':

                        break;
                }
            }
        }
    }
}