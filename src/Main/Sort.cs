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

            var m = Partition(a, l, r);

            QuickSort(a, l, m - 1);
            QuickSort(a, m + 1, r);
        }

        public static void QuickSort2(int[] a, int l, int r)
        {
            if (l >= r)
            {
                return;
            }
            
            var p = Partition3(a, l, r);

            QuickSort(a, l, p.Item1 - 1);
            QuickSort(a, p.Item2, r);
        }

        public static int Partition(int[] a, int l, int r)
        {
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

            return j;
        }

        public static Tuple<int, int> Partition3(int[] a, int l, int r)
        {
            if (l >= r)
            {
                throw new ArgumentException("l >= r");
            }

            var j = l + 1; // > pivot
            var k = l + 1; // == pivot
            for (int i = l + 1; i <= r; i++)
            {
                // move all elements < m to left.
                if (a[i] < a[l])
                {
                    Swap(a, i, j);
                    j++;
                    k++;
                    if (k > j)
                    {
                        Swap(a, i, k - 1);
                    }
                }
                else if (a[i] == a[l])
                {
                    Swap(a, i, k);
                    k++;
                }
            }

            Swap(a, l, j - 1);
            j--;

            return new Tuple<int, int>(j, k);
        }

        public static int[] CountSort(int[] A, int maxValue)
        {   
            var sorted = new int[A.Length];
            var B = new int[maxValue];
            for (int i = 0; i < A.Length; i++)
            {
                B[A[i] - 1]++;
            }
            for (int i = 1; i < maxValue; i++)
            {
                B[i] += B[i - 1];
            }
            for (int i = A.Length - 1; i > -1; i--)
            {
                sorted[B[A[i] - 1] - 1] = A[i];
                B[A[i] - 1]--;
            }
            return sorted;
        }
    }

    public static class Task64
    {
        public static void Run()
        {
            var n = int.Parse(Console.ReadLine());
            var fullN = GetNextPowerOfTwo(n);
            var items = Console.ReadLine().Split().Select(i => new int[] { int.Parse(i) });
            var a = new LinkedList<int[]>(items);
            while (n < fullN)
            {
                a.AddLast(new int[] { int.MaxValue });
                n++;
            }

            var count = Sort.MergeInversions(a, n);
            Console.WriteLine(count);
        }

        public static int GetNextPowerOfTwo(int num)
        {
            return (int)Math.Pow(2, Math.Ceiling(Math.Log(num) / Math.Log(2)));
        }
    }

    public static class LinesAndPoints
    {
        internal static int GetIntersectionNumber(int[] lineStarts, int[] lineEnds, int point)
        {
            var startLessOrEqual = Search.LessCount(lineStarts, point, true);
            var endLess = Search.LessCount(lineEnds, point, false);

            var result = startLessOrEqual - endLess;
            return result;
        }

        internal static int GetIntersectionNumberNaive(int[] lineStarts, int[] lineEnds, int point)
        {
            var lineCount = lineStarts.Length;
            var startLess = 0;
            var startEqual = 0;
            var endLess = 0;
            var endEqual = 0;

            for (int i = 0; i < lineCount; i++)
            {
                if (lineStarts[i] < point)
                {
                    startLess++;
                }
                else if (lineStarts[i] == point)
                {
                    startEqual++;
                }
                else
                {
                    break;
                }
            }

            for (int i = 0; i < lineCount; i++)
            {
                if (lineEnds[i] < point)
                {
                    endLess++;
                }
                else if (lineEnds[i] == point)
                {
                    endEqual++;
                }
                else
                {
                    break;
                }
            }

            var result = startLess - endLess + startEqual;
            return result;
        }
    }

    public static class Task65
    {
        public static void Run()
        {
            var data = Console.ReadLine().Split();
            var n = int.Parse(data[0]);
            var m = int.Parse(data[1]);

            var lineStarts = new int[n];
            var lineEnds = new int[n];

            for (int i = 0; i < n; i++)
            {
                data = Console.ReadLine().Split();
                lineStarts[i] = int.Parse(data[0]);
                lineEnds[i] = int.Parse(data[1]);
            }

            Sort.QuickSort(lineStarts, 0, n - 1);
            Sort.QuickSort(lineEnds, 0, n - 1);

            var points = Console.ReadLine().Split();
            for (var i = 0; i < points.Length; i++)
            {
                var num = LinesAndPoints.GetIntersectionNumber(lineStarts, lineEnds, int.Parse(points[i]));
                Console.Write($"{num} ");
            }

        }

        public static void ForEach<T>(this IEnumerable<T> @this, Action<T> action)
        {
            foreach (var x in @this)
                action(x);
        }

        public static void Test(int n = 50000, int m = 50000, int maxCoordinate = 100000000)
        {
            var rand = new Random();

            var lineStarts = new int[n];
            var lineEnds = new int[n];

            for (int i = 0; i < n; i++)
            {
                lineStarts[i] = rand.Next(-maxCoordinate, maxCoordinate);
                lineEnds[i] = rand.Next(-maxCoordinate, maxCoordinate);
            }

            Sort.QuickSort2(lineStarts, 0, n - 1);
            Sort.QuickSort2(lineEnds, 0, n - 1);

            var cache = new Dictionary<int, int>(m);
            var getFromCache = 0;
            var algorithmFails = 0;

            var counts = Enumerable
                .Range(0, m)
                .Select(x => rand.Next(-maxCoordinate, maxCoordinate))
                .Select(point =>
                {
                    var count = 0;
                    var countNaive = 0;
                    if (!cache.ContainsKey(point))
                    {
                        count = LinesAndPoints.GetIntersectionNumber(lineStarts, lineEnds, point);
                        //countNaive = LinesAndPoints.GetIntersectionNumberNaive(lineStarts, lineEnds, point);
                        if (count == countNaive || true)
                        {
                            cache.Add(point, count);
                        }
                        else
                        {
                            algorithmFails++;
                            //Console.WriteLine($"Error! {count} != {countNaive}");
                        }
                    }
                    else
                    {
                        count = cache[point];
                        getFromCache++;
                    }
                    return count;

                })
                .ToList();
                //.ForEach(num => Console.Write($"{num} "));

                Console.WriteLine($"{cache.Count} elements in cache. {getFromCache} times get from cache");
                Console.WriteLine($"{counts.Count} points processed");
                Console.WriteLine($"{algorithmFails} times algo failed");
                Console.WriteLine("\n------------------------\n\n");
        }

        public static void RunTest()
        {
            var runCount = 10;

            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            for (int i = 0; i < runCount; i++)
            {
                Test(50000, 50000, 10);
            }
            sw.Stop();
            System.Console.WriteLine($"\n\nAvg time elapsed: {sw.ElapsedMilliseconds/runCount} ({runCount} runs)");
        }
    }

    public static class Task68
    {
        public static void Run()
        {
            Console.ReadLine();
            var data = Console.ReadLine().Split().Select(int.Parse).ToArray();
            Console.WriteLine(string.Join(" ", Sort.CountSort(data, 10)));
        }
    }
}