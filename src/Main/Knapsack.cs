using System;

namespace Stepic.Algorithms
{
    public class Item
    {
        public int Weight { get; set; }
        public int Value { get; set; }

        public Item(int weight, int value)
        {
            Weight = weight;
            Value = value;
        }
    }

    public static class Knapsack
    {
        public static int KnapSackWithoutReps(Item[] items, int weight, bool print = false)
        {
            var d = new int[weight + 1, items.Length + 1];
            // 0 col and row already have 0s

            for (int Wi = 1; Wi < weight + 1; Wi++)
            {
                if (print) Console.Write($"\nWi:{Wi}\t");
                for (int i = 1; i < items.Length + 1; i++)
                {
                    var item = items[i - 1];

                    d[Wi, i] = d[Wi, i - 1];

                    if (item.Weight <= Wi)
                    {
                        var possibleValueWithCurrent = d[Wi - item.Weight, i - 1] + item.Value;
                        d[Wi, i] = Math.Max(d[Wi, i], possibleValueWithCurrent);
                    }
                    if (print) Console.Write(d[Wi, i] + "\t");
                }
            }
            if (print) Console.WriteLine();
            
            return d[weight, items.Length];
        }
    }

    public static class Task845
    {
        public static void Run()
        {
            var items = new Item[] {
                new Item(6, 30),
                new Item(3, 14),
                new Item(4, 16),
                new Item(2, 9)
            };
            var weight = 10;
            var res = Knapsack.KnapSackWithoutReps(items, weight, true);
            Console.WriteLine(res);
        }
    }
}