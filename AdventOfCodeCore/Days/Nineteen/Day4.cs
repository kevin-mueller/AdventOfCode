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
            List<int> validNumbers = new List<int>();
            while (range[0] <= range[1])
            {
                range[0]++;
                if (isValidNumber(range[0]))
                    validNumbers.Add(range[0]);
            }
            return validNumbers.Count;
        }

        private bool isValidNumber(int number)
        {
            string startString = number.ToString();

            for (int i = 1; i < startString.Length; i++)
            {
                if (Convert.ToInt16(startString[i].ToString()) < Convert.ToInt16(startString[i - 1].ToString()))
                    return false;
            }


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

    }
}