using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days.Nineteen
{
    public static class Day1
    {
        public static string Solve(string inputPath)
        {
            string input = File.ReadAllText(inputPath);
            int res = 0;
            using (StringReader reader = new StringReader(input))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    res += (Convert.ToInt32(line) / 3) - 2;
                }
            }
            return res.ToString();
        }

        public static string Solve2(string inputPath)
        {
            string input = File.ReadAllText(inputPath);
            int res = 0;
            using (StringReader reader = new StringReader(input))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    res += GetFuel(Convert.ToInt32(line), 0);
                }
            }
            return res.ToString();
        }

        private static int GetFuel(int mass, int totalFuel)
        {
            int fuel = (mass / 3) - 2;
            totalFuel += fuel;
            if (((fuel / 3) - 2) <= 0)
                return totalFuel;
            else return GetFuel(fuel, totalFuel);
        }
    }
}
