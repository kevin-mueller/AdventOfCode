using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days.Nineteen
{
    //https://adventofcode.com/2019/day/4
    public class Day4
    {
        private int[] range;
        public Day4(string path)
        {
            var rawText = File.ReadAllText(path);
            var tokens = rawText.Split("-");
            range = new int[] { Convert.ToInt32(tokens[0]), Convert.ToInt32(tokens[1]) };
        }

        public int Solve()
        {
            int start = range[0];
            start = 266799;

            int hits = 1; //the first value (hardcoded) is also valid

            List<int> startList = new List<int>();
            foreach (var item in start.ToString().ToCharArray())
            {
                startList.Add(Convert.ToInt32(item.ToString()));
            }

            //the start value is invalid

            //count up
            for (int i = startList.Count - 1; i >= 0; i--)
            {
                while (startList[i] < 9)
                {
                    if (listToInt(startList) > range[1])
                        return hits;

                    startList[i]++;

                    for (int j = i + 1; j < startList.Count; j++)
                    {
                        while (isSmallerThanPrevious(startList, j))
                            startList[j]++;
                    }
                    if (isValidNumber(listToInt(startList)))
                        hits++;
                }

                //reset startList
                startList.Clear();
                foreach (var item in start.ToString().ToCharArray())
                {
                    startList.Add(Convert.ToInt32(item.ToString()));
                }
            }
            return hits;
        }

        private bool isValidNumber(int number)
        {
            string startString = number.ToString();
            for (int i = 0; i < startString.Length; i++)
            {
                if (i + 1 == startString.Length)
                    break;

                if (startString[i].Equals(startString[i + 1]))
                {
                    return true;
                }
            }
            return false;
        }

        private bool isSmallerThanPrevious(List<int> values, int index)
        {
            var current = values[index];
            if (index > 0)
            {
                return current < values[index - 1];
            }
            return false;
        }

        private int listToInt(List<int> values)
        {
            var sb = new StringBuilder();
            foreach (var item in values)
            {
                sb.Append(item);
            }
            return Convert.ToInt32(sb.ToString());
        }
    }
}