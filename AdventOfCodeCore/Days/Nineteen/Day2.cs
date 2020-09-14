using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Helpers;

namespace AdventOfCode.Days.Nineteen
{
    public class Day2
    {
        public static string Solve(string input)
        {
            string[] stringArray = File.ReadAllText(input).Split(',');
            int[] sourceCode = Array.ConvertAll(stringArray, s => int.Parse(s));
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"Noun: {i}");
                for (int j = 0; j < 100; j++)
                {
                    List<int> res = IntComputer.Run(i, j, sourceCode);
                    if (res.Last() == 19690720)
                        return ((i * 100) + j).ToString();
                    sourceCode = Array.ConvertAll(stringArray, s => int.Parse(s));
                }
                sourceCode = Array.ConvertAll(stringArray, s => int.Parse(s));
            }
            return "Error";
        }

        
    }
}