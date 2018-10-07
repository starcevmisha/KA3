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
        public Node next;

        public Node(int x, int y, int treeId)
        {
            X = x;
            Y = y;
            this.treeId = treeId;
            next = this;
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
        public static int[] size;

        public static int Weight(Node a, Node b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        public static void Main(string[] args)
        {
            ///Читаем
            var nodes = new List<Node>();
            var filename = "in.txt";
            var lines = File.ReadAllLines(filename);
            var n = int.Parse(lines[0]);
            size = new int[n+1];
            for (int i = 1; i < lines.Length; i++)
            {
                var coords = lines[i].Split(' ');
                nodes.Add(new Node(int.Parse(coords[0]), int.Parse(coords[1]), i));
                size[i] = 1;
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

            //Сортируем ребра по весу 
            edgeList = edgeList.OrderBy(x => x.weight).ToList();

            var res = new List<Edge>();

            foreach (var edge in edgeList)
            {
                var v = nodes[edge.startPointIndex];
                var w = nodes[edge.endPointIndex];
                var p = v.treeId;
                var q = w.treeId;
                if (p != q)
                {
                    if (size[p] > size[q])
                        merge(v, w, size);
                    else
                        merge(w, v, size);

                    res.Add(edge);
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


            var resLine = "";
            foreach (var line in matrix)
            {
                resLine += string.Join(" ", line);
                resLine += "\r\n";
            }

            File.WriteAllText("out.txt", resLine);
        }

        public static void merge(Node v, Node w, int[] size)
        {
            var p = v.treeId;
            var q = w.treeId;

            w.treeId = p;
            var u = w.next;
            while (u.treeId != p)
            {
                u.treeId = p;
                u = u.next;
            }

            size[p] = size[p] + size[q];
            var x = v.next;
            var y = w.next;
            v.next = y;
            v.next = x;
        }
    }
}