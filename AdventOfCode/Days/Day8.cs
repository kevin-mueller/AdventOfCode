using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    public class Day8
    {
        private List<Node> nodes;

        int[] rawNodes;
        public Day8(string inputPath)
        {
            nodes = new List<Node>();
            string raw = File.ReadAllText(inputPath);
            rawNodes = Array.ConvertAll(raw.Split(' '), int.Parse);
            for (int i = 0; i < rawNodes.Length; i++)
            {
                if (i + 1 == rawNodes.Length)
                    break;
                //Kei ahnig..
                int metaDataScore = 0;
                for (int j = 0; j < numberOfMetaData; j++)
                {
                    metaDataScore += rawNodes[(i + 1) + j];
                }

            }
        }

        private int GetLastIndexOfNode()
        {
            
        }

        public string Part1()
        {
            int res = 0;
            foreach (var node in nodes)
            {
                res += node.MetadataEntries;
            }
            return res.ToString();
        }
    }

    class Node
    {
        public int ChildNodes { get; set; }
        public int MetadataEntries { get; set; }
        public int Index { get; set; }
    }
}
