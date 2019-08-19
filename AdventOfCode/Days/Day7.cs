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
        private List<char> finalOrder = new List<char>();

        public string Part1(string inputPath)
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
            var x = cStep.Except(nStep).ToList();

            x.Sort();

            char firstStep = x.First();
            x.Remove(firstStep);
            foreach (var item in x)
            {
                artifacts.Add(item);
            }


            finalOrder.Add(firstStep);

            foreach (var s in steps)
            {
                if (!finalOrder.Contains(s[1]))
                    finalOrder.Add(GetNextStep(finalOrder.Last()));
            }

            string res = "";
            foreach (var item in finalOrder)
            {
                res += item;
            }
            return res;
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
                List<char> hasToBeFinishedFirst = new List<char>();
                char tmp = default;
                if (item[0] == c)
                {
                    tmp = item[1];
                }
                else continue;
                foreach (var t in steps)
                {
                    if (t[1] == tmp)
                    {
                        hasToBeFinishedFirst.Add(t[0]);
                    }
                }

                //final order needs to contain ALL items in hasToBeFinished
                var allOfList1IsInList2 = !hasToBeFinishedFirst.Except(finalOrder).Any();

                if (allOfList1IsInList2)
                {
                    nextStepsPossible.Add(tmp);
                }


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