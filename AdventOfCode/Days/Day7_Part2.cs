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

        private Day7 day7 = new Day7();
        public void Solve(string inputPath)
        {
            string Q = day7.Part1(inputPath);


        }


        class Worker
        {
            public char CurrentTask { get; set; }
        }

        class Task
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
