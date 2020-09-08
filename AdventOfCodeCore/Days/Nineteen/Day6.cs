using AdventOfCode.Helpers;
using Newtonsoft.Json;
using NGenerics.DataStructures.Trees;
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
        private string[] orbitsRaw;
        public Day6(string path)
        {
            orbitsRaw = File.ReadAllLines(path);
        }
        public int PartOne()
        {
            //use tree implementation?
            var com = new BinaryTree<Planet>(new Planet("COM"));

            foreach (var item in orbitsRaw)
            {
                var planet = item.Split(")", StringSplitOptions.RemoveEmptyEntries)[0];
                var planetChild = item.Split(")", StringSplitOptions.RemoveEmptyEntries)[1];

                if (planet.Equals("COM"))
                {
                    com.Add(new Planet(planetChild));
                }
                else
                {
                    var planetNode = com.FindNode(x => x.Name.Equals(planet));
                    planetNode.Add(new Planet(planetChild));
                }
            }

            foreach (var item in com)
            {
                Console.WriteLine(item.Name);
            }

            return 0;
        }

    }

    public class Planet
    {
        public string Name { get; set; }
        public Planet(string name)
        {
            Name = name;
        }
    }
}