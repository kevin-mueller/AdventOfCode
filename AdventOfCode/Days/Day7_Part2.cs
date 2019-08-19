using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    public class Day7_Part2
    {
        private List<char[]> rawSteps = new List<char[]>();
        private List<Task> artifacts = new List<Task>();
        private List<Task> steps = new List<Task>();
        private List<Task> finalOrder = new List<Task>();

        private Day7 day7;

        public Day7_Part2(string inputPath)
        {
            day7 = new Day7(inputPath);
        }

        public void Solve()
        {
            List<Worker> workers = new List<Worker>
            {
                new Worker(),
                new Worker(),
            };
            List<Task> allNextPossible = new List<Task>();

            
            allNextPossible = day7.GetAllNextPossible(day7.GetFirstStep());

            foreach (var worker in workers)
            {
                day7.GetNextStepForWorker(worker, allNextPossible);
            }

        }


        public class Worker
        {
            public Task CurrentTask { get; set; }
        }

        public class Task
        {
            public Task(char c)
            {
                Label = c;
                Duration = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(c) + 1;
                //Duration += 60;
            }
            public char Label { get; set; }
            public int Duration { get; set; }
            public Task NextTask { get; set; }

            public override string ToString()
            {
                string n = "";
                try
                {
                    n = NextTask.Label.ToString();
                }
                catch { }
                return Label.ToString() + " -> " + n;
            }
        }
    }
}
