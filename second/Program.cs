using System;
using System.Collections.Generic;
using System.IO;

namespace second
{
    internal class Program
    {
        const int INFINITY = 10000;

        private static int[] h;//массив в котором будем хранить дельты
        private static int[] choice; //Массив в котором будем хранить тип дуги(прямая/обратная)

        public static int[] FindPath(int s, int t, int n, int[,] c, int[,] f)
        {
            for (int i = 0; i < n; i++) 
                h[i] = INFINITY;

            var previous = new int[n];
            previous[s] = -1;

            var queue = new Queue<int>();
            queue.Enqueue(s);

            while (queue.Count > 0 && h[t] == INFINITY)
            {
                var w = queue.Dequeue();
                for (int v = 0; v < n; v++)
                {
                    if (h[v] == INFINITY && c[w, v] - f[w, v] > 0) //Ищем прямые ребра
                    {
                        h[v] = Math.Min(h[w], c[w, v] - f[w, v]);
                        previous[v] = w;
                        queue.Enqueue(v);
                        choice[v] = 1;
                    }
                }
                for (int v = 0; v < n; v++)
                {
                    if (w != s && h[v] == INFINITY && f[v, w] > 0) //Ищем обратные ребра
                    {
                        h[v] = Math.Min(h[w], f[v, w]);
                        previous[v] = w;
                        queue.Enqueue(v);
                        choice[v] = -1;
                    }
                }
            }

            return previous;
        }

        public static int[,] FindMaxThread(int n, int s, int t, int[,] c, out int fValue)
        {
            h = new int[n];
            choice = new int[n];
            choice[s] = 1;

            var f = new int[n, n];
            fValue = 0;

            for (var v = 0; v < n; v++)
            for (var w = 0; w < n; w++)
                f[v, w] = 0;


            do
            {
                var previous = FindPath(s, t, n, c, f);

                if (h[t] < INFINITY)
                {
                    fValue += h[t];
                    var v = t;
                    while (v != s)
                    {
                        var w = previous[v];
                        if (choice[v] == 1)
                            f[w, v] += h[t];
                        else
                            f[v, w] -= h[t];
                        v = w;
                    }
                }
            } while (h[t] != INFINITY);

            return f;
        }

        public static void Main(string[] args)
        {
            ///Читаем
            var filename = "in.txt";
            var lines = File.ReadAllLines(filename);
            var n = int.Parse(lines[0]);
            var c = new int[n, n];
            for (var i = 0; i < n; i++)
            {
                var line = lines[i + 1].Split(' ');
                for (var j = 0; j < n; j++)
                {
                    c[i, j] = int.Parse(line[j]);
                }
            }

            var s = int.Parse(lines[n + 1])-1;
            var t = int.Parse(lines[n + 2])-1;

            var f = FindMaxThread(n, s, t, c, out var fValue);


            var resLine = "";
            resLine += $"{fValue}\r\n";
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    resLine += f[i, j];
                    resLine += " ";
                }
                resLine += "\r\n";
            }

            
            
            File.WriteAllText("out.txt", resLine);
            
        }
    }
}