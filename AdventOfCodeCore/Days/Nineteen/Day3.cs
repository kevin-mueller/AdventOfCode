using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Days.Nineteen
{
    //https://adventofcode.com/2019/day/3
    public class Day3
    {
        public static int Solve(string inputPath)
        {
            string[] wiresRaw = File.ReadAllLines(inputPath);

            List<Wire> wires = new List<Wire>();

            foreach (var item in wiresRaw)
            {
                var wire = new Wire();
                wire.BuildInstructionTable(item);
                wires.Add(wire);
            }

            List<Task> pendingTasks = new List<Task>();
            foreach (var item in wires)
            {
                //Run parallell. (It takes long enough)
                Task t = new Task(delegate
                {
                    item.ComputeAllPointsVisited(item.Instructions);
                });
                pendingTasks.Add(t);
                t.Start();
            }

            Console.WriteLine($"Waiting for {pendingTasks.Count} tasks to complete.");
            foreach (var task in pendingTasks)
            {
                task.Wait();
            }

            Console.WriteLine("All points computed");

            //File.WriteAllText("wires.json", JsonConvert.SerializeObject(wires));
            Console.WriteLine("Saved wires.json");


            //Calculate all intersecting points (should work for x wires)
            List<Point> intersectingPoints = new List<Point>();
            foreach (var baseList in wires)
            {
                foreach (var otherList in wires)
                {
                    if (baseList.Equals(otherList))
                        continue;

                    Console.WriteLine("Calculating intersecting points..");
                    var tmp = baseList.AllPointsVisited.FindAll(o => otherList.AllPointsVisited.Any(w => w.X == o.X && w.Y == o.Y));
                    intersectingPoints.AddRange(tmp);
                    Console.WriteLine($"Found {intersectingPoints.Count} intersecting points.");
                }
                //Since there are only two wires for now, we can break here.
                break;
            }


            var distances = new List<int>();
            foreach (var item in intersectingPoints)
            {
                //distances.Add(item.CalculateDistanceToCentralPort());
                int stepsOfWire1 = item.TranceBackToRoot();
                item.Wire = item.Wire.Equals(wires[0]) ? wires[1] : wires[0];
                int stepsOfWires2 = item.TranceBackToRoot();
                distances.Add(stepsOfWire1 + stepsOfWires2);
            }
            distances.Sort();

            //distances = distances.Distinct().ToList();

            foreach (var item in distances)
            {
                Console.WriteLine(item);
            }

            //The first one is the central port
            return distances[1];
        }
    }

    internal class Wire
    {
        private readonly Matrix matrix;

        public List<Point> AllPointsVisited;
        public List<string> Instructions = new List<string>();

        public Wire()
        {
            matrix = new Matrix();
        }

        public List<string> BuildInstructionTable(string wireData)
        {
            foreach (var item in wireData.Split(','))
            {
                Instructions.Add(item);
            }
            return Instructions;
        }

        public void ComputeAllPointsVisited(List<string> instructionTable)
        {
            //TODO loop through the matrix and set the points visited to true.
            for (int i = 0; i < instructionTable.Count; i++)
            {
                Console.WriteLine($"Calculating.. ({i + 1} / {instructionTable.Count})");
                char direction = instructionTable[i][0];
                int travelDistance = Convert.ToInt32(instructionTable[i].Remove(0, 1));
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
            AllPointsVisited = matrix.GetMatrix();
            foreach (var item in AllPointsVisited)
            {
                item.Wire = this;
            }
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

        public Wire Wire;

        public int X { get; set; }
        public int Y { get; set; }
        public bool Visited { get; set; }
        public bool IsCentralPort { get; set; }
        public bool Crossed { get; set; }
        public int CalculateDistanceToCentralPort()
        {
            return Math.Abs(X) + Math.Abs(Y);
        }

        public int TranceBackToRoot()
        {
            int steps = 0;
            var ancestor = GetAncestor(this);
            while (ancestor != null)
            {
                steps++;
                ancestor = GetAncestor(ancestor);
            }
            return steps;
        }

        private Point GetAncestor(Point p)
        {
            var ancestor = p.Wire.AllPointsVisited.Find(x => x.X - 1 == p.X && x.Y == p.Y && x.X < p.X - 1);
            if (ancestor == null)
            {
                ancestor = p.Wire.AllPointsVisited.Find(x => x.X + 1 == p.X && x.Y == p.Y && x.X < p.X + 1);
                if (ancestor == null)
                {
                    ancestor = p.Wire.AllPointsVisited.Find(x => x.Y + 1 == p.Y && x.X == p.X && x.Y < p.Y + 1);
                    if (ancestor == null)
                    {
                        ancestor = p.Wire.AllPointsVisited.Find(x => x.Y - 1 == p.Y && x.X == p.X && x.Y < p.Y - 1);
                    }
                }
            }
            return ancestor;
        }
    }

    internal class Matrix
    {
        private readonly List<Point> matrix = new List<Point>();

        private readonly Point currentMatrixPosition = new Point(0, 0);

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
                    element.Visited = true;
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