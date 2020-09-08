using System;
using System.IO;

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
                    var res = Run(i, j, sourceCode);
                    if (res == 19690720)
                        return ((i * 100) + j).ToString();
                    sourceCode = Array.ConvertAll(stringArray, s => int.Parse(s));
                }
                sourceCode = Array.ConvertAll(stringArray, s => int.Parse(s));
            }
            return "Error";
        }

        private static int Run(int noun, int verb, int[] sourceCode)
        {
            sourceCode[1] = noun;
            sourceCode[2] = verb;
            try
            {
                for (int i = 0; i < sourceCode.Length; i += 4)
                {
                    switch (sourceCode[i])
                    {
                        case 1:
                            sourceCode[sourceCode[i + 3]] = sourceCode[sourceCode[i + 1]] + sourceCode[sourceCode[i + 2]];
                            break;

                        case 2:
                            sourceCode[sourceCode[i + 3]] = sourceCode[sourceCode[i + 1]] * sourceCode[sourceCode[i + 2]];
                            break;

                        case 99:
                            throw new Exception();
                    }
                }
            }
            catch (Exception)
            {
                return sourceCode[0];
            }
            return -1;
        }
    }
}