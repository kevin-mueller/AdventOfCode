using System;
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

            List<Wire> wires = new List<Wire>();
            var matrix = new Matrix();
            foreach (var item in wiresRaw)
            {
                wires.Add(new Wire(item, matrix));
                matrix.ResetCurrentPosition();
            }

            var allPointsVisited1 = wires[0]._allPointsVisited;
            var allPointsVisited2 = wires[1]._allPointsVisited;

            //var intersectingPoints = allPointsVisited1.Where(o => allPointsVisited2.Any(w => w.X == o.X && w.Y == o.Y));
            var intersectingPoints = allPointsVisited2.Where(o => o.Crossed);
            List<int> distances = new List<int>();
            foreach (var item in intersectingPoints)
            {
                distances.Add(item.CalculateDistanceToCentralPort());
            }
            distances.Sort();
            //The first one is the central port
            return distances[0];
        }
    }

    internal class Wire
    {
        private readonly List<string> _instructions = new List<string>();

        public List<Point> _allPointsVisited { get; set; }

        public Wire(string inputRaw, Matrix matrix)
        {
            _instructions = BuildInstructionTable(inputRaw);
            _allPointsVisited = ComputeAllPointsVisited(_instructions, matrix);
        }

        private List<string> BuildInstructionTable(string wireData)
        {
            foreach (var item in wireData.Split(','))
            {
                _instructions.Add(item);
            }
            return _instructions;
        }

        private List<Point> ComputeAllPointsVisited(List<string> instructionTable, Matrix matrix)
        {
            //TOOD Check for index out of bounds exception.
            //TODO Idealy, scan the input an calculate the size of the matrix.

            //TODO loop through the matrix and set the points visited to true.
            foreach (var item in instructionTable)
            {
                char direction = item[0];
                int travelDistance = Convert.ToInt32(item.Remove(0, 1));
                Console.WriteLine($"Traveling {direction} ({travelDistance})");
                switch (direction)
                {
                    case 'R':
                        matrix.TravelRight(travelDistance);
                        break;

                    case 'U':
                        matrix.TravelUp(travelDistance);
                        break;

                    case 'L':
                        matrix.TravelLeft(travelDistance);
                        break;

                    case 'D':
                        matrix.TravelDown(travelDistance);
                        break;
                }
            }

            //List<Point> pointsVisited = new List<Point>();
            //for (int i = 0; i < matrix.SizeX; i++)
            //{
            //    for (int j = 0; j < matrix.SizeY; j++)
            //    {
            //        if (matrix.GetValueAt(i, j))
            //        {
            //            pointsVisited.Add(new Point(i, j));
            //        }
            //    }
            //}
            return matrix.GetMatrix();
        }
    }

    internal class Point
    {
        public Point(int x, int y, bool visited = false)
        {
            X = x;
            Y = y;
            Visited = visited;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public bool Visited { get; set; }
        public bool IsCentralPort { get; set; }
        public bool Crossed { get; set; }

        public int CalculateDistanceToCentralPort()
        {
            return X + Y;
        }
    }

    internal class Matrix
    {
        private const int MatrixSize = 9999999;

        //Bigger Matrix => throws out of memory exception..
        //use jagged array
        private readonly List<Point> matrix = new List<Point>();

        private readonly Point currentMatrixPosition = new Point(0, 0);

        public readonly int SizeX;
        public readonly int SizeY;

        public Matrix()
        {
            //Central Port
            matrix.Add(new Point(0, 0, true)
            {
                IsCentralPort = true
            });
        }

        public void ResetCurrentPosition()
        {
            currentMatrixPosition.X = 0;
            currentMatrixPosition.Y = 0;
        }

        public List<Point> GetMatrix()
        {
            return matrix;
        }

        public bool GetValueAt(int x, int y)
        {
            return matrix.First(z => z.X == x && z.Y == y).Visited;
        }

        internal Point TravelRight(int travelDistance)
        {
            for (int i = 0; i < travelDistance; i++)
            {
                currentMatrixPosition.X += 1;
                try
                {
                    //Found
                    var element = matrix.First(z => z.X == currentMatrixPosition.X && z.Y == currentMatrixPosition.Y);
                    element.Crossed = true;
                }
                catch
                {
                    //Not found
                    matrix.Add(new Point(currentMatrixPosition.X, currentMatrixPosition.Y, true));
                }
            }
            return currentMatrixPosition;
        }

        internal Point TravelLeft(int travelDistance)
        {
            for (int i = 0; i < travelDistance; i++)
            {
                currentMatrixPosition.X -= 1;
                try
                {
                    //Found
                    var element = matrix.First(z => z.X == currentMatrixPosition.X && z.Y == currentMatrixPosition.Y);
                    element.Crossed = true;
                }
                catch
                {
                    //Not found
                    matrix.Add(new Point(currentMatrixPosition.X, currentMatrixPosition.Y, true));
                }
            }
            return currentMatrixPosition;
        }

        internal Point TravelUp(int travelDistance)
        {
            for (int i = 0; i < travelDistance; i++)
            {
                currentMatrixPosition.Y += 1;
                try
                {
                    //Found
                    var element = matrix.First(z => z.X == currentMatrixPosition.X && z.Y == currentMatrixPosition.Y);
                    element.Crossed = true;
                }
                catch
                {
                    //Not found
                    matrix.Add(new Point(currentMatrixPosition.X, currentMatrixPosition.Y, true));
                }
            }
            return currentMatrixPosition;
        }

        internal Point TravelDown(int travelDistance)
        {
            for (int i = 0; i < travelDistance; i++)
            {
                currentMatrixPosition.Y -= 1;
                try
                {
                    //Found
                    var element = matrix.First(z => z.X == currentMatrixPosition.X && z.Y == currentMatrixPosition.Y);
                    element.Crossed = true;
                }
                catch
                {
                    //Not found
                    matrix.Add(new Point(currentMatrixPosition.X, currentMatrixPosition.Y, true));
                }
            }
            return currentMatrixPosition;
        }
    }
}