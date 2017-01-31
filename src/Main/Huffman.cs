using System;
using System.Collections.Generic;
using System.Linq;

namespace Stepic.Algorythms
{
    public class Node
    {
        public char Letter {get; private set;}
        public int Value {get; private set;}
        public Node Left {get; private set;}
        public Node Right {get; private set;}
        public bool IsLeaf {
            get 
            {
                return Left == null && Right == null;
            }
        }

        public Node(int value, char letter)
        {
            Value = value;
            Letter = letter;
        }

        public Node(Node left, Node right)
        {
            Left = left;
            Right = right;
            Value = Left.Value + Right.Value;
        }
    }

    public class PriorityQueue<T>
    {
        private List<Tuple<int, T>> items;

        public PriorityQueue()
        {
            items = new List<Tuple<int, T>>();
        }

        public void Insert(int priority, T item)
        {
            items.Add(new Tuple<int, T>(priority, item));
        }

        public T ExtractMin()
        {
            var item = items.MinBy( t=> t.Item1);
            items.Remove(item);
            return item.Item2;
        }
    }

    public class Haffman
    {
        public Node CreateTree(IEnumerable<Node> nodes)
        {
            var queue = new PriorityQueue<Node>();
            foreach (var node in nodes)
            {
                queue.Insert(node.Value, node);
            }

            var n = nodes.Count();
            for (var k = 0; k < n - 1; k++)
            {
                var left = queue.ExtractMin();
                var right = queue.ExtractMin();
                var newNode = new Node(left, right);
                queue.Insert(newNode.Value, newNode);
            }

            var root = queue.ExtractMin();
            return root;
            
        }

        public Dictionary<char, string> CalculateCodes(Node node, string prefix = "")
        {
            if (!node.IsLeaf)
            {
                var leftCodes = CalculateCodes(node.Left, prefix + "0");
                var rightCodes = CalculateCodes(node.Right, prefix + "1");
                return leftCodes.Union(rightCodes).ToDictionary(k=>k.Key, v=>v.Value);
            }
            else
            {
                return new Dictionary<char, string> {
                    {node.Letter, prefix == string.Empty ? "0" : prefix}
                };
            }
        }
    }

    public class Task425
    {
        public static void Run()
        {
            var input = "a"; //Console.ReadLine();
            var nodes = input.GroupBy(l=>l).Select(g=> new Node(g.Count(), g.Key));
            var haffman = new Haffman();
            var root = haffman.CreateTree(nodes);
            var codes = haffman.CalculateCodes(root);

            var encoded = string.Join("", input.Select(c=>codes[c]));

            Console.WriteLine($"{codes.Keys.Count} {encoded.Length}");
            foreach (var code in codes)
            {
                Console.WriteLine($"{code.Key}: {code.Value}");
            }
            Console.WriteLine(encoded);
        }
    }

    public static class Extensions
    {
        public static T1 MinBy<T1, R>(this IEnumerable<T1> en, Func<T1, R> evaluate) where R : IComparable<R> {
            return en.Select(t => new Tuple<T1, R>(t, evaluate(t)))
                .Aggregate((max, next) => next.Item2.CompareTo(max.Item2) < 0 ? next : max).Item1;
        }
    }
}