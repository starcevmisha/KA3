using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace first
{
    public class Node
    {
        public int X;
        public int Y;
        public int treeId;

        public Node(int x, int y, int treeId)
        {
            X = x;
            Y = y;
            this.treeId = treeId;
        }
    }

    public class Edge
    {
        public int weight;
        public int startPointIndex;
        public int endPointIndex;

        public Edge(int weight, int startPointIndex, int endPointIndex)
        {
            this.weight = weight;
            this.startPointIndex = startPointIndex;
            this.endPointIndex = endPointIndex;
        }
    }

    internal class Program
    {
        public static int Weight(Node a, Node b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        public static void Main(string[] args)
        {
            var nodes = new List<Node>();
            var filename = "input.txt";
            var lines = File.ReadAllLines(filename);
            var n = int.Parse(lines[0]);
            for (int i = 1; i < lines.Length; i++)
            {
                var coords = lines[i].Split(' ');
                nodes.Add(new Node(int.Parse(coords[0]), int.Parse(coords[1]), i));
            }

            var edgeList = new List<Edge>();
            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = 0; j < nodes.Count; j++)
                {
                    if (i != j)
                    {
                        edgeList.Add(new Edge(Weight(nodes[i], nodes[j]), i, j));
                    }
                }
            }

            edgeList = edgeList.OrderBy(x => x.weight).ToList();

            var res = new List<Edge>();

            foreach (var edge in edgeList)
            {
                if (nodes[edge.startPointIndex].treeId != nodes[edge.endPointIndex].treeId)
                {
                    res.Add(edge);
                    var oldId = nodes[edge.endPointIndex].treeId;
                    var newId = nodes[edge.startPointIndex].treeId;
                    foreach (var node in nodes)
                        if (node.treeId == oldId)
                            node.treeId = newId;
                }
            }

            var matrix = new List<List<int>>();
            for (int i = 0; i < n; i++)
                matrix.Add(new List<int>());

            foreach (var edge in res)
            {
                matrix[edge.startPointIndex].Add(edge.endPointIndex + 1);
                matrix[edge.endPointIndex].Add(edge.startPointIndex + 1);
            }

            foreach (var line in matrix)
            {
                line.Sort();
                line.Add(0);
            }

            foreach (var line in matrix)
            {
                Console.WriteLine(string.Join(" ", line));
            }

        }
    }
}