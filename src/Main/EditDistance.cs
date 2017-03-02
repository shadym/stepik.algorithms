using System;

namespace Stepic.Algorithms
{
    public class EditDistance
    {
        public static int Calculate(string a, string b)
        {
            var d = new int[b.Length + 1, a.Length + 1];

            for (int j = 0; j < a.Length + 1; j++)
            {
                d[0, j] = j;
            }
            for (int i = 0; i < b.Length + 1; i++)
            {
                d[i, 0] = i;
            }

            for (int i = 1; i < b.Length + 1; i++)
            {
                for (int j = 1; j < a.Length + 1; j++)
                {
                    d[i, j] = Min(
                        d[i, j - 1] + 1,
                        d[i - 1, j] + 1,
                        d[i - 1, j - 1] + Diff(a[j - 1], b[i - 1])
                    );
                }
            }

            return d[b.Length, a.Length];
        }

        private static int Min(int a, int b, int c)
        {
            return Math.Min(a, Math.Min(b, c));
        }

        private static int Diff(char a, char b)
        {
            return a == b ? 0 : 1;
        }
    }

    public static class Task838
    {
        public static void Run()
        {
            var first = Console.ReadLine();
            var second = Console.ReadLine();
            Console.WriteLine(EditDistance.Calculate(first, second));
        }
    }
}