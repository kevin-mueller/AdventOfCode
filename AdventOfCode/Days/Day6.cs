using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days
{
    public static class Day6
    {
        public static void Solve(string inputPath)
        {
            string input = File.ReadAllText(inputPath);

            List<Coordinate> coords = new List<Coordinate>();
            foreach (var item in input.Split(new[] { "\n" }, StringSplitOptions.None))
            {
                string[] tmp = item.Split(new[] { ", " }, StringSplitOptions.None);
                coords.Add(new Coordinate() { x = Convert.ToInt32(tmp[0]), y = Convert.ToInt32(tmp[1]) });
            }
            int maxX = 0;
            int maxY = 0;
            foreach (var coord in coords)
            {
                if (coord.x > maxX)
                    maxX = coord.x;
                if (coord.y > maxY)
                    maxY = coord.y;
            }

            var grid = new string[maxX + 1, maxY + 1];

            foreach (var item in coords)
            {
                grid[item.x, item.y] = item.ToString();
            }

            for (int i = 0; i <= maxX; i++)
            {
                for (int j = 0; j <= maxY; j++)
                {
                    List<Coordinate> closestCoords = new List<Coordinate>();
                    foreach (var coord in coords)
                    {
                        coord.distanceFromScanner = (i - coord.x).MakePositive() + (j - coord.y).MakePositive();
                        closestCoords.Add(coord);
                    }

                    //Set the current value to the closest coord
                    List<Coordinate> SortedList = closestCoords.OrderBy(o => o.distanceFromScanner).ToList();
                    if (SortedList.First().distanceFromScanner == SortedList[1].distanceFromScanner)
                    {
                        //There are more than 1 coordinate that have the same distance to the scanner.
                        grid[i, j] = ".";
                    }
                    else
                    {
                        if (i == 0 || j == 0)
                        {
                            SortedList.First().isInfinite = true;
                        }
                        else if (i == maxX || j == maxY)
                        {
                            SortedList.First().isInfinite = true;

                        }
                        grid[i, j] = SortedList.First().ToString();
                    }
                }
            }

            List<string> occurences = new List<string>();
            for (int i = 0; i < maxX; i++)
            {
                for (int j = 0; j < maxY; j++)
                {
                    occurences.Add(grid[i, j]);
                }
            }

            foreach (var item in occurences.ToList())
            {
                foreach (var coord in coords)
                {
                    if (coord.ToString().Equals(item))
                        if (coord.isInfinite)
                            occurences.Remove(item);
                }
            }

            var q = occurences.GroupBy(x => x)
            .Select(g => new { Value = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count);
            foreach (var item in q)
            {
                Console.WriteLine(item.Value + " : " + item.Count);
            }
        }

        public static int MakePositive(this int x) => x.ToString().StartsWith("-") ? x * -1 : x;
    }

    public class Coordinate
    {
        public int x { get; set; }
        public int y { get; set; }
        public int distanceFromScanner { get; set; }
        public bool isInfinite { get; set; }

        public override string ToString()
        {
            return x.ToString() + "__" + y.ToString();
        }
    }
}