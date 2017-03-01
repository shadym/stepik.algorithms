using System;
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

        public static int GetLongestIncreasingSubsequenceNLogN(int[] a)
        {
            if (a.Length < 2)
            {
                return a.Length;
            }

            var t = new int[a.Length];
            t[0] = 0;
            var len = 1;

            for (int i = 1; i < a.Length; i++)
            {
                if (a[i] > a[t[len - 1]])
                {
                    t[len] = i;
                    len++;
                }
                else if (a[i] < a[t[0]])
                {
                    t[0] = i;
                }
                else
                {
                    // find new place for element
                    for (int j = 0; j < len; j++)
                    {
                        if (a[i] == a[t[j]])
                        {
                            break;
                        }
                        else if (a[i] < a[t[j]])
                        {
                            t[j] = i;
                            break;
                        }
                    }
                }
            }

            return len;
        }
    }

    public static class Task825
    {
        public static void Run()
        {
            var rand = new Random();
            for (int i = 0; i < 100; i++)
            {
                var nums = Enumerable.Range(0, 10).Select(n => rand.Next(0, 10)).ToArray();
                var nums1 = new [] {7, 0, 5, 0, 1};
                var res1 = LongestIncreasingSubsequence.GetLongestIncreasingSubsequence(nums);
                var res2 = LongestIncreasingSubsequence.GetLongestIncreasingSubsequenceNLogN(nums);
                if (res1 != res2)
                {
                    Console.WriteLine("Methods are not equal:");
                    Console.WriteLine($"res1: {res1}, res2: {res2}\n{string.Join(" ", nums)}");
                }
            }
        }
    }
}