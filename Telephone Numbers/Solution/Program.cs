using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
namespace Solution
{
    class Program
    {
        public class Node
        {
            private Dictionary<byte, Node> _dictionary;

            public Node()
            {
                _dictionary = new Dictionary<byte, Node>();
            }

            public Node(byte root)
            {
                _dictionary = _dictionary ?? new Dictionary<byte, Node>();
                if (!_dictionary.Keys.Contains(root))
                {
                    _dictionary.Add(root, new Node());
                }
            }

            public Node(params byte[] roots)
            {
                _dictionary = _dictionary ?? new Dictionary<byte, Node>();
                if (roots.Length > 0)
                    foreach (byte root in roots)
                    {
                        if (!_dictionary.Keys.Contains(root))
                        {
                            _dictionary.Add(root, new Node());
                        }
                    }
                else
                { throw new Exception(); }
            }

            public Node this[byte index]
            {
                get
                {
                    if (_dictionary.Keys.Contains(index))
                    {
                        return _dictionary[index];
                    }
                    else
                    {
                        throw new IndexOutOfRangeException();
                    }
                }
            }

            public Node Add(byte value)
            {
                if (!_dictionary.Keys.Contains(value))
                {
                    _dictionary.Add(value, new Node());
                }
                return _dictionary[value];
            }

            public int Count
            {
                get
                {
                    int count = _dictionary.Keys.Count;
                    if (_dictionary.Values.Count > 0)
                    {
                        foreach (Node n in _dictionary.Values)
                        {
                            count += n.Count;
                        }
                    }
                    return count;
                }
            }
        }

        static void Main(string[] args)
        {
            int N = int.Parse(Console.ReadLine());
            Node root = new Node();
            for (int i = 0; i < N; i++)
            {
                var curNode = root;
                string telephone = Console.ReadLine();
                byte[] bt = telephone.Select((e) => byte.Parse(e.ToString())).ToArray();
                for (int j = 0; j < bt.Length; j++)
                {
                    curNode = curNode.Add(bt[j]);
                }
            }

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");


            // The number of elements (referencing a number) stored in the structure.
            Console.WriteLine(root.Count);
        }
    }
}