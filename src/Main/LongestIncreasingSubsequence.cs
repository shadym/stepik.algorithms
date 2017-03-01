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
                else if (a[i] != a[t[0]] && a[i] != a[t[len - 1]])
                {
                    // use binary search
                    var pos = -1;
                    var l = 0;
                    var r = len - 1;
                    while (l <= r)
                    {
                        var m = l + (r - l) / 2;
                        var mid = a[t[m]];
                        if (mid == a[i])
                        {
                            break;
                        }
                        else if (mid < a[i])
                        {
                            if (m < r && a[t[m + 1]] > a[i])
                            {
                                pos = m + 1;
                                break;
                            }
                            else
                            {
                                l = m + 1;
                            }
                        }
                        else if (mid > a[i])
                        {
                            if (m > l && a[t[m - 1]] < a[i])
                            {
                                pos = m;
                                break;
                            }
                            else
                            {
                                r = m - 1;
                            }
                        }
                    }
                    if (pos != -1)
                    {
                        t[pos] = i;
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
            for (int i = 0; i < 1000000; i++)
            {
                var nums = Enumerable.Range(0, 20).Select(n => rand.Next(0, 10)).ToArray();
                var nums1 = new[] {1, 6, 7, 2, 6, 7};
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