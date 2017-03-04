using System;
using System.Linq;

namespace Stepic.Algorithms
{
    public static class Stairs
    {
        public static int CalculateSumBU(int[] steps)
        {
            if (steps.Length == 1)
            {
                return steps[0];
            }
            
            var sums = new int[steps.Length];
            sums[0] = steps[0];
            sums[1] = Math.Max(sums[0] + steps[1], steps[1]);

            if (steps.Length == 2)
            {
                return sums[1];
            }

            for (int i = 2; i < steps.Length; i++)
            {
                sums[i] = Math.Max(sums[i - 1] + steps[i], sums[i - 2] + steps[i]);
            }

            return sums[steps.Length - 1];
        }
    }

    public static class Task874
    {
        public static void Run()
        {
            Console.ReadLine();
            var steps = Console.ReadLine().Split()
                .Select(int.Parse)
                .ToArray();
            Console.WriteLine(Stairs.CalculateSumBU(steps));
        }
    }
}