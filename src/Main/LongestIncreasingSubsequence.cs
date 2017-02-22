using System;
using System.Collections.Generic;
using System.Linq;

namespace Stepic.Algorithms
{
    public static class LongestIncreasingSubsequence
    {
        public static int GetLongestIncreasingSubsequence(int[] a)
        {
            var d = new int[a.Length];

            for (int i = 0; i < a.Length; i++)
            {
                d[i] = 1;
                var maxLength = 0;
                //var maxLengthIndex = 0;
                for (int j = 0; j < i; j++)
                {
                    if (a[j] < a[i] && d[j] > maxLength)
                    {
                        //maxLengthIndex = j;
                        maxLength = d[j];
                    }
                }
                d[i] = maxLength + 1;
            }

            return d.Max();
        }
    }

    public static class Task825
    {
        public static void Run()
        {
            var n = int.Parse(Console.ReadLine());
            var nums = Console.ReadLine().Split().ToArray();
        }
    }
}