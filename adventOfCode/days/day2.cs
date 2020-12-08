using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adventOfCode.days
{
    class Day2 : Code
    {
        public void Part1()
        {
            var lines = read("day2").Split("\r\n");

            var validCount = 0;

            bool isValid(int min, int max, char letter, string password)
            {
                var count = password.Where(c => c == letter).Count();
                return count >= min && count <= max;
            }

            foreach (var line in lines)
            {
                var parts = line.Split(':', ' ');
                var counts = parts[0].Split('-');
                var letter = parts[1];
                var password = parts[3];

                var min = counts[0];
                var max = counts[1];

                if (isValid(min: int.Parse(min), max: int.Parse(max), letter: letter[0], password: password))
                {
                    validCount++;
                    Console.WriteLine($"Valid Password: {password}");
                }
                else
                {

                }
            }

            Console.WriteLine($"Valid passwords: {validCount}");
        }

        public void Part2()
        {
            var lines = read("day2").Split("\r\n");

            var validCount = 0;

            bool isValid(int min, int max, char letter, string password)
            {
                try
                {
                    // passwords are 1-indexed, so we subtract 1
                    return (password[min-1] == letter) != (password[max-1] == letter);
                }
                catch
                {
                    return false;
                }
            }

            foreach (var line in lines)
            {
                var parts = line.Split(':', ' ');
                var counts = parts[0].Split('-');
                var letter = parts[1];
                var password = parts[3];

                var min = counts[0];
                var max = counts[1];

                if (isValid(min: int.Parse(min), max: int.Parse(max), letter: letter[0], password: password))
                {
                    validCount++;
                    Console.WriteLine($"Valid Password: {password}");
                }
                else
                {

                }
            }

            Console.WriteLine($"Valid passwords: {validCount}");
        }
    }
}
