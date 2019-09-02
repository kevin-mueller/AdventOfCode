using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day8
    {
        private List<Node> nodes;

        private int[] rawNodes;

        //Childnodes of childnodes metadata is not recognized correctly. Debug.
        public Day8(string inputPath)
        {
            nodes = new List<Node>();
            string raw = File.ReadAllText(inputPath);
            rawNodes = Array.ConvertAll(raw.Split(' '), int.Parse);

            Node n_start = new Node()
            {
                Index = 0
            };

            ProcessNode(n_start);

            nodes = ListNodes(n_start);
            foreach (var node in nodes)
            {
                ProcessNode(node);
            }
        }

        List<Node> ListNodes(Node initialNode)
        {
            var node = new List<Node>();
            node.Add(initialNode);
            foreach (var n in initialNode.ChildNodes)
            {
                node.AddRange(ListNodes(n));
            }
            return node;
        }

        private Node ProcessNode(Node n)
        {
            n.NumberOfChildNodes = rawNodes[n.Index];
            n.NumberOfMetadataEntries = rawNodes[n.Index + 1];
            if (n.NumberOfChildNodes == 0)
            {
                n.BranchLength = rawNodes[n.Index + 1] + 2;
                n.MetadataList.Clear();
                for (int j = 0; j < n.NumberOfMetadataEntries; j++)
                {
                    n.MetadataList.Add(rawNodes[n.BranchLength + j -1]);
                }
            }
            else
            {
                //Calculate ALL child nodes
                List<Node> children = GetAllChildNodes(n);
                int totalLength = 0;
                foreach (var item in children)
                {
                    totalLength += item.BranchLength + 2;
                    foreach (var cChild in item.ChildNodes)
                    {
                        totalLength += cChild.BranchLength + 2;
                    }
                }
                n.BranchLength = totalLength;
                n.MetadataList.Clear();
                for (int j = 0; j < n.NumberOfMetadataEntries; j++)
                {
                    n.MetadataList.Add(rawNodes[n.BranchLength + j -1]);
                }
                n.ChildNodes.AddRange(children);
            }
            return n;
        }

        private List<Node> GetAllChildNodes(Node n)
        {
            List<Node> childNodes = new List<Node>();
            for (int i = 0; i < n.NumberOfChildNodes; i++)
            {
                Node lastChild = childNodes.LastOrDefault();
                if (lastChild == null) lastChild = new Node() { BranchLength = 0 };
                Node child = new Node()
                {
                    Index = n.Index + lastChild.BranchLength + 2,
                    NumberOfChildNodes = rawNodes[n.Index + lastChild.BranchLength + 2],
                };
                if (child.NumberOfChildNodes == 0)
                {
                    child.BranchLength = rawNodes[child.Index + 1] + 2;
                }
                else
                {
                    //Calculate ALL child nodes
                    child.ChildNodes.AddRange(GetAllChildNodes(child));
                }

                childNodes.Add(child);

            }
            return childNodes;
        }

        public string Part1()
        {
            return "";
        }
    }

    internal class Node
    {
        public int BranchLength { get; set; }
        public int NumberOfChildNodes { get; set; }
        public List<Node> ChildNodes = new List<Node>();
        public int NumberOfMetadataEntries { get; set; }
        public int Index { get; set; }
        public List<int> MetadataList = new List<int>();
    }
}