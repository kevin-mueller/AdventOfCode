using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static AdventOfCode.Days.Day7_Part2;

namespace AdventOfCode.Days
{
    public class Day7
    {
        private List<char[]> steps = new List<char[]>();
        private List<char> artifacts = new List<char>();
        private List<char> finalOrder = new List<char>();

        public Day7(string inputPath)
        {
            string raw = File.ReadAllText(inputPath);

            foreach (var line in raw.Split(new[] { "\n" }, StringSplitOptions.None))
            {
                char s1 = line.Split(new[] { "Step " }, StringSplitOptions.None)[1].First();
                char s2 = line.Split(new[] { "step " }, StringSplitOptions.None)[1].First();

                steps.Add(new char[] { s1, s2 });
            }
        }

        public char GetFirstStep()
        {
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
            return firstStep;
        }

        public string Part1()
        {
            foreach (var s in steps)
            {
                if (!finalOrder.Contains(s[1]))
                    finalOrder.Add(GetNextStepAlphabetical(finalOrder.Last()));
            }

            string res = "";
            foreach (var item in finalOrder)
            {
                res += item;
            }
            return res;
        }

        public List<Task> GetAllNextPossible(char c)
        {
            List<Task> nextStepsPossible = new List<Task>();

            foreach (var item in artifacts)
            {
                nextStepsPossible.Add(new Task(item));
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
                    nextStepsPossible.Add(new Task(tmp));
                }
            }
            return nextStepsPossible.OrderBy(o => o.Label).ToList();
        }

        private char GetNextStepAlphabetical(char c)
        {
            char nextStep = '_';
            List<Task> nextStepsPossible = GetAllNextPossible(c);

            if (nextStepsPossible.Count >= 2)
            {
                //list already sorted alphabetical
                nextStep = nextStepsPossible.First().Label;
                nextStepsPossible.Remove(nextStepsPossible.First());
                foreach (var item in nextStepsPossible)
                {
                    artifacts.Add(item.Label);
                }
            }
            else
            {
                try
                {
                    nextStep = nextStepsPossible.First().Label;
                }
                catch
                {
                    //ignore
                }
            }

            return nextStep;
        }

        public char GetNextStepForWorker(Worker worker, List<Task> allPossibleNext)
        {

            if (worker.CurrentTask == null)
            {
                //Worker wants work

                //artifacts have priority 1
                if (artifacts.Count > 0)
                {
                    artifacts.Sort();
                    worker.CurrentTask = new Task(artifacts.First());
                    artifacts.Remove(artifacts.First());
                }
                else
                {
                    worker.CurrentTask = allPossibleNext.First();
                    allPossibleNext.Remove(worker.CurrentTask);
                }
            }

            return default;
        }
    }

    public class StepModel
    {
        public char ToBeFinished { get; set; }
        public char NextStep { get; set; }
    }
}