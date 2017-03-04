using System;
using System.Collections.Generic;
using System.Linq;

namespace Stepic.Algorithms
{
    public static class Calculator
    {
        public static int GetOperations(int number, out List<int> intermediates)
        {
            
            intermediates = new List<int>
            {
                number
            };
            var d = new int[number];
            d[0] = 0;

            if (number == 1)
            {
                return 0;
            }

            var pretendents = new List<int>(3);

            int i;
            for (i = 1; i < number; i++)
            {
                var n = i + 1;

                if (n % 3 == 0)
                {
                    pretendents.Add(d[n / 3 - 1] + 1);
                }
                if (n % 2 == 0)
                {
                    pretendents.Add(d[n / 2 - 1] + 1);
                }
                pretendents.Add(d[n - 1 - 1] + 1);
                d[i] = pretendents.Min();
                pretendents.Clear();
            }

            // Restore sequence

            i = number - 1;
            while (i > 0)
            {
                var n = i + 1;
                var previous = d[i] - 1;
                if (n % 3 == 0 && d[n / 3 - 1] == previous)
                {
                    i = n / 3 - 1;
                }
                else if (n % 2 == 0 && d[n / 2 - 1] == previous)
                {
                    i = n / 2 - 1;
                }
                else
                {
                    i = n - 1 - 1;
                }

                intermediates.Add(i + 1);
            }

            intermediates.Reverse();
            
            return d[number - 1];
        }

        public static int Min(int a, int b, int c)
        {
            return Math.Min(a, Math.Min(b, c));
        }
    }

    public static class Task875
    {
        public static void Run()
        {
            var number = int.Parse(Console.ReadLine());
            List<int> intermediates;
            Console.WriteLine(Calculator.GetOperations(number, out intermediates));
            Console.WriteLine(string.Join(" ", intermediates));
        }
    }
}