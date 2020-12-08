using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adventOfCode.days
{
    class Day3 : Code
    {

        public int Part1(int right, int down, bool print = false)
        {
            var map = read("day3").Split("\r\n");

            var x = 0;
            var y = 0;

            var trees = 0;

            while (y < map.Length)
            {
                var arr = map[y].ToCharArray();
                trees += arr[x] == '#' ? 1 : 0;

                arr[x] = arr[x] == '#' ? 'X' : '0';
                if (print) Console.WriteLine(new string(arr));

                x = (x + right) % map[y].Length;
                y += down;
            }

            Console.WriteLine($"Encountered {trees} trees");

            return trees;
        }

        public void Part2()
        {
            long product = Part1(right: 1, down: 1);
            product *= Part1(right: 3, down: 1);
            product *= Part1(right: 5, down: 1);
            product *= Part1(right: 7, down: 1);
            product *= Part1(right: 1, down: 2);

            Console.WriteLine($"Product: {product}");
        }

    }
}
