using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Helpers;

namespace AdventOfCode.Days
{
    public class Day7
    {
        private int[] sourceCode;
        public Day7(string path)
        {
            sourceCode = Array.ConvertAll(File.ReadAllText(path).Split(','), s => int.Parse(s));
        }

        public int PartOne()
        {

            var ampA = new IntComputer(sourceCode);
            var ampB = new IntComputer(sourceCode);
            var ampC = new IntComputer(sourceCode);
            var ampD = new IntComputer(sourceCode);
            var ampE = new IntComputer(sourceCode);

            List<int> results = new List<int>();
            List<int> usedPhases = new List<int>();

            for (int i = 0; i <= 44444; i++)
            {
                ampA.Reset();
                ampB.Reset();
                ampC.Reset();
                ampD.Reset();
                ampE.Reset();

                string formatted = i.ToString("00000");
                int[] sequence = new int[] 
                {
                    Convert.ToInt32(formatted[0].ToString()),
                    Convert.ToInt32(formatted[1].ToString()),
                    Convert.ToInt32(formatted[2].ToString()),
                    Convert.ToInt32(formatted[3].ToString()),
                    Convert.ToInt32(formatted[4].ToString())
                };
                
                if (sequence.Length != sequence.Distinct().Count() || sequence.Any(x => x > 4))
                    continue;

                Console.WriteLine($"Testing with: {formatted}");

                var resA = ampA.Run(parameters: new int[] { sequence[0], 0 }).Last();
                var resB = ampB.Run(parameters: new int[] { sequence[1], resA }).Last();
                var resC = ampC.Run(parameters: new int[] { sequence[2], resB }).Last();
                var resD = ampD.Run(parameters: new int[] { sequence[3], resC }).Last();
                var resE = ampE.Run(parameters: new int[] { sequence[4], resD }).Last();

                results.Add(resE);
            }
            results.Sort();
            return results.Last();
        }
    }
}