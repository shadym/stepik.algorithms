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

    public class ListPriorityQueue<T>
    {
        private List<Tuple<int, T>> items;

        public ListPriorityQueue()
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

    public class BinaryHeap<T> where T: IComparable
    {
        private List<T> data;
        private int heapSize = 0;

        public BinaryHeap()
        {
            data = new List<T>();
            data.Add(default(T)); // add zero-indexed item to make indexes start from one
        }

        public void Insert(T item)
        {
            heapSize++;
            data.Insert(heapSize, item);
            SiftUp(heapSize);
        }

        public T ExtractMin()
        {
            if (heapSize < 1)
            {
                return default(T);
            }
            var min = data[1];
            Swap(1, heapSize);
            heapSize--;
            SiftDown(1);
            return min;
        }


        private void Swap(int indexA, int indexB)
        {
            var temp = data[indexA];
            data[indexA] = data[indexB];
            data[indexB] = temp;
        }

        private int Parent(int index)
        {
            return index/2;
        }

        private int LeftChild(int index)
        {
            return index * 2;
        }

        private int RightChild(int index)
        {
            return index * 2 + 1;
        }

        private void SiftUp(int index)
        {
            while (data[Parent(index)].CompareTo(data[index]) > 0 && index > 1)
            {
                Swap(index, Parent(index));
                index = Parent(index);
            }
        }

        private void SiftDown(int index)
        {
            while (RightChild(index) <= heapSize)
            {
                var left = data[LeftChild(index)];
                var right = data[RightChild(index)];
                var item = data[index];
                var minimumIndex = left.CompareTo(right) < 0 && left.CompareTo(item) < 0 ? LeftChild(index) :
                    right.CompareTo(left) < 0 && right.CompareTo(item) < 0 ? RightChild(index) : index;

                if (minimumIndex == index)
                {
                    break;
                }

                Swap(minimumIndex, index);
                index = minimumIndex;
            }
        }

    }

    public class Huffman
    {
        public Node CreateTree(IEnumerable<Node> nodes)
        {
            var queue = new ListPriorityQueue<Node>();
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

    public class Task4
    {
        public static void RunHuffmanEncode()
        {
            var input = Console.ReadLine();
            var nodes = input.GroupBy(l=>l).Select(g=> new Node(g.Count(), g.Key));
            var huffman = new Huffman();
            var root = huffman.CreateTree(nodes);
            var codes = huffman.CalculateCodes(root);

            var encoded = string.Join("", input.Select(c=>codes[c]));

            Console.WriteLine($"{codes.Keys.Count} {encoded.Length}");
            foreach (var code in codes)
            {
                Console.WriteLine($"{code.Key}: {code.Value}");
            }
            Console.WriteLine(encoded);
        }

        public static void RunHuffmanDecode()
        {
            var encoded = string.Empty;
            var decoded = string.Empty;
            var codes = new Dictionary<string, char>();
            Console.ReadLine(); // we don't need 1st line
            string line;
            while ((line = Console.ReadLine()) != null && line != string.Empty)
            {
                var data = line.Split(':');
                if (data.Count() == 2)
                {
                    codes.Add(data[1].Trim(), data[0][0]);
                
                }
                else
                {
                    encoded = line;    
                }
            }

            var accumulator = string.Empty;
            foreach (var c in encoded)
            {
                accumulator += c;
                if (codes.ContainsKey(accumulator))
                {
                    decoded += codes[accumulator];
                    accumulator = string.Empty;
                }
            }

            Console.WriteLine(decoded);
        }

        public static void RunPriorityQueue()
        {
            var queue = new BinaryHeap<int>();
            Console.ReadLine();
            string line;
            while ((line = Console.ReadLine()) != null && line != string.Empty)
            {
                if (line[0] == 'I')
                {
                    var value = int.Parse(line.Substring(7));
                    queue.Insert(-value);
                }
                else
                {
                    Console.WriteLine(-queue.ExtractMin());
                }
            }
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