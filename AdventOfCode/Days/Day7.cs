using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day7
    {
        private List<char[]> steps = new List<char[]>();
        private List<char> artifacts = new List<char>();

        public void Part1(string inputPath)
        {
            string raw = File.ReadAllText(inputPath);

            foreach (var line in raw.Split(new[] { "\n" }, StringSplitOptions.None))
            {
                char s1 = line.Split(new[] { "Step " }, StringSplitOptions.None)[1].First();
                char s2 = line.Split(new[] { "step " }, StringSplitOptions.None)[1].First();

                steps.Add(new char[] { s1, s2 });
            }

            //determine the first step:
            List<char> cStep = new List<char>();
            List<char> nStep = new List<char>();
            foreach (var item in steps)
            {
                cStep.Add(item[0]);
                nStep.Add(item[1]);
            }
            char firstStep = cStep.Except(nStep).ToList().First();

            List<char> finalOrder = new List<char>();
            finalOrder.Add(firstStep);

            foreach (var s in steps)
            {
                finalOrder.Add(GetNextStep(finalOrder.Last()));
            }

            foreach (var item in finalOrder)
            {
                Console.Write(item);
            }
        }

        private char GetNextStep(char c)
        {
            char nextStep = '_';
            List<char> nextStepsPossible = new List<char>();

            foreach (var item in artifacts)
            {
                nextStepsPossible.Add(item);
            }

            artifacts.Clear();

            foreach (var item in steps)
            {
                if (item[0] == c)
                    nextStepsPossible.Add(item[1]);
            }

            if (nextStepsPossible.Count >= 2)
            {
                //use alphabetical order
                List<char> SortedList = nextStepsPossible.OrderBy(o => o).ToList();
                nextStep = SortedList.First();
                SortedList.Remove(nextStep);
                foreach (var item in SortedList)
                {
                    artifacts.Add(item);
                }
            }
            else
            {
                try
                {
                    nextStep = nextStepsPossible.First();
                }
                catch
                {
                    //ignore
                }
            }

            return nextStep;
        }
    }

    public class StepModel
    {
        public char ToBeFinished { get; set; }
        public char NextStep { get; set; }
    }
}