using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Helpers;

namespace AdventOfCode.Days.Nineteen
{
    public class Day5
    {
        private int[] sourceCode;
        public Day5(string path)
        {
            sourceCode = Array.ConvertAll(File.ReadAllText(path).Split(','), s => int.Parse(s));
        }

        public int PartOne()
        {
            List<int> res = IntComputer.Run(null, null, sourceCode, new int[] { 1 });
            return res.Last();
        }
    }
}