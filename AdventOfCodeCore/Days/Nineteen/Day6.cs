using AdventOfCode.Helpers;
using Newtonsoft.Json;
using NGenerics.DataStructures.Trees;
using NGenerics.Patterns.Visitor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days.Nineteen
{
    //https://adventofcode.com/2019/day/6
    public class Day6
    {
        private readonly string[] orbitsRaw;
        public Day6(string path)
        {
            orbitsRaw = File.ReadAllLines(path);
        }
        public int PartOne()
        {
            List<SimplePlanet> parsed = new List<SimplePlanet>();
            foreach (var p in orbitsRaw)
            {
                parsed.Add(new SimplePlanet(p.Split(")", StringSplitOptions.RemoveEmptyEntries)[0], p.Split(")", StringSplitOptions.RemoveEmptyEntries)[1]));
            }

            var com = new GeneralTree<Planet>(new Planet("COM"));

            string next = "COM";

            com = Do(parsed, com, next);

            int counter = 0;
            com.BreadthFirstTraversal(new ActionVisitor<Planet>(delegate (Planet p)
            {
                var node = com.FindNode(x => x.Name.Equals(p.Name));
                counter += node.Ancestors.Count;
            }));

            return counter;
        }

        public int PartTwo()
        {
            List<SimplePlanet> parsed = new List<SimplePlanet>();
            foreach (var p in orbitsRaw)
            {
                parsed.Add(new SimplePlanet(p.Split(")", StringSplitOptions.RemoveEmptyEntries)[0], p.Split(")", StringSplitOptions.RemoveEmptyEntries)[1]));
            }

            var com = new GeneralTree<Planet>(new Planet("COM"));

            string next = "COM";

            com = Do(parsed, com, next);

            
            var node1 = com.FindNode(x => x.Name.Equals("YOU"));
            var node1Path = node1.GetPath().ToList();

            var node2 = com.FindNode(x => x.Name.Equals("SAN"));
            var node2Path = node2.GetPath().ToList();

            var intersection = node1Path.Intersect(node2Path).ToList();
            intersection.RemoveAt(intersection.Count -1);

            foreach (var item in intersection)
            {
                node1Path.Remove(item);
                node2Path.Remove(item);
            }

            var path = new List<GeneralTree<Planet>>();
            path.AddRange(node1Path);
            path.AddRange(node2Path);

            var finalPath = path.DistinctBy(x => x.Data.Name);

            return finalPath.Count() - 1;
        }

        private GeneralTree<Planet> Do(List<SimplePlanet> parsed, GeneralTree<Planet> com, string next)
        {
            var lefts = parsed.Where(x => x.Left.Equals(next));

            var node = com.FindNode(x => x.Name.Equals(next));
            if (node == null)
                return com;

            foreach (var left in lefts)
            {
                node.Add(new Planet(left.Right));
                com = Do(parsed, com, left.Right);
            }
            return com;
        }

    }

    public class Planet
    {
        public string Name { get; set; }
        public int Key { get; set; }
        public Planet(string name)
        {
            Name = name;
            Key = int.MaxValue;
        }
    }

    public class SimplePlanet
    {
        public string Left { get; set; }
        public string Right { get; set; }
        public SimplePlanet(string left, string right)
        {
            Left = left;
            Right = right;
        }
    }
}