using System;
using System.Collections.Generic;
using System.Linq;

namespace Stepic.Algorithms
{
    public static class Sort
    {
        public static long MergeInversions(LinkedList<int[]> a, long count)
        {
            long inversions = 0;

            while (count > 1)
            {
                var first = a.First();
                a.RemoveFirst();
                var second = a.First();
                a.RemoveFirst();
                var merged = MergeArrays(first, second, ref inversions);
                a.AddLast(merged);
                count--;
            }
            
            return inversions;
        }

        private static int[] MergeArrays(int[] first, int[] second, ref long inversions)
        {
            var n = first.Length;
            var m = second.Length;
            var merged = new int[n + m];
            var i = 0;
            var j = 0;
            var k = 0;
            while (i < n || j < m)
            {
                if (i < n && j < m)
                {
                    if (first[i] <= second[j])
                    {
                        merged[k++] = first[i++];
                    }
                    else
                    {
                        merged[k++] = second[j++];
                        inversions += n - i;
                    }
                }
                else if (i < n)
                {
                    merged[k++] = first[i++];
                }
                else
                {
                    merged[k++] = second[j++];
                }
            }
            return merged;
        }


        public static void Swap(int[] a, int i, int j)
        {
            var temp = a[i];
            a[i] = a[j];
            a[j] = temp;
        }

        public static void QuickSort(int[] a, int l, int r)
        {
            if (l >= r)
            {
                return;
            }

            // get some element
            var m = l + (r - l) / 2;
            Swap(a, l, m);

            var j = l; // first witch higher than pivot
            for (int i = l + 1; i <= r; i++)
            {
                // move all elements < m to left.
                if (a[i] < a[l])
                {
                    Swap(a, j + 1, i);
                    j++;
                }
            }
            Swap(a, j, l);

            QuickSort(a, l, m - 1);
            QuickSort(a, m + 1, r);
        }
    }

    public static class Task64
    {
        public static void Run()
        {
            var n = int.Parse(Console.ReadLine());
            var fullN = GetNextPowerOfTwo(n);
            var items = Console.ReadLine().Split().Select(i => new int[] {int.Parse(i)} );
            var a = new LinkedList<int[]>(items);
            while (n < fullN)
            {
                a.AddLast(new int[] {int.MaxValue});
                n++;
            }

            var count = Sort.MergeInversions(a, n);
            Console.WriteLine(count);
        }

        public static int GetNextPowerOfTwo(int num)
        {
            return (int)Math.Pow(2, Math.Ceiling(Math.Log(num)/Math.Log(2)) );
        }
    }
}