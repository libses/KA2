using System;
using System.Collections.Generic;
using System.Linq;

namespace KA2
{
    public enum NodeType
    {
        Unspecified,
        Even,
        Odd
    }
    public class Node
    {
        public NodeType NodeType = NodeType.Unspecified;
        public List<Node> Nodes = new List<Node>();
        public bool visited = false;
        public void Add(Node node)
        {
            Nodes.Add(node);
        }

        public void Add(IEnumerable<Node> nodes)
        {
            Nodes = Nodes.Concat(nodes).ToList();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());

            var array = Enumerable.Range(0, n).Select(x => new Node()).ToArray();
            for (int i = 0; i < n; i++)
            {
                var inp = Console.ReadLine().Split().Select(x => int.Parse(x)).Where(x => x != 0).Select(x => array[x-1]);
                array[i].Add(inp);
            }

            if (n < 2)
            {
                Console.WriteLine("N");
                return;
            }

            var queue = new Queue<Node>();
            queue.Enqueue(array[0]);
            array[0].visited = true;
            array[0].NodeType = NodeType.Even;
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                var nextNodeType = node.NodeType == NodeType.Odd ? NodeType.Even : NodeType.Odd;
                foreach (var v in node.Nodes)
                {
                    if (v.NodeType == node.NodeType)
                    {
                        Console.WriteLine("N");
                        return;
                    }

                    if (!v.visited)
                    {
                        queue.Enqueue(v);
                        v.NodeType = nextNodeType;
                        v.visited = true;
                    }
                }
            }

            Console.WriteLine("Y");
            var firstType = array[0].NodeType;
            Console.WriteLine(string.Join(' ', array.Select((x,i) => (x,i)).Where(x => x.x.NodeType == firstType).Select(x => x.i + 1)));
            Console.WriteLine("0");
            Console.WriteLine(string.Join(' ', array.Select((x, i) => (x, i)).Where(x => x.x.NodeType != firstType).Select(x => x.i + 1)));
        }
    }
}
