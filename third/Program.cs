using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace third
{
    public class Vector
    {
        public int X;
        public int Y;

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }

        protected bool Equals(Vector other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Vector) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }
    }

    internal class Program
    {
        private const int WallSize = 3;

        public static void Main(string[] args)
        {
            var filename = "in.txt";
            var lines = File.ReadAllLines(filename);
            var n = int.Parse(lines[0]);
            var maze = new bool[n, n];
            for (int i = 0; i < n; i++)
            {
                var row = lines[i + 1];
                for (int j = 0; j < n; j++)
                {
                    maze[i, j] = row[j] == '.';
                }
            }

            var result = GetVisibleWallCount(maze);

            var square = (result - 4) * WallSize * WallSize;
            
            File.WriteAllText("out.txt", square.ToString());
        }

        public static int GetVisibleWallCount(bool[,] maze)
        {
            var queue = new Queue<Vector>();
            var visited = new HashSet<Vector>();
            queue.Enqueue(new Vector(0, 0));
            while (queue.Count != 0)
            {
                var current = queue.Dequeue();
                foreach (var neighbour in GetAllNeighbours(maze, current))
                {
                    if (!visited.Contains(neighbour))
                        queue.Enqueue(neighbour);
                }

                visited.Add(current);
            }

            var result = 0;
            // В Visited будут лежать все клетки, в которые смогут дойти посетители
            foreach (var vector in visited)
            {
                result += GetDegree(maze, vector);
            }

            return result;
        }

        public static IEnumerable<Vector> GetAllNeighbours(bool[,] maze, Vector current)
        {
            var deltas = new List<Vector>
            {
                new Vector(-1, 0),
                new Vector(0, -1),
                new Vector(0, 1),
                new Vector(1, 0)
            };
            foreach (var delta in deltas)
            {
                var neighbour = current + delta;
                if (neighbour.X >= 0 && neighbour.X < maze.GetLength(0) &&
                    neighbour.Y >= 0 && neighbour.Y < maze.GetLength(1) &&
                    maze[neighbour.X, neighbour.Y])
                {
                    yield return neighbour;
                }
            }
        }

        public static int GetDegree(bool[,] maze, Vector current)
        {
            var deltas = new List<Vector>
            {
                new Vector(-1, 0),
                new Vector(0, -1),
                new Vector(0, 1),
                new Vector(1, 0)
            };
            var count = 0;
            foreach (var delta in deltas)
            {
                var neighbour = current + delta;
                if (!(neighbour.X >= 0 && neighbour.X < maze.GetLength(0) &&
                    neighbour.Y >= 0 && neighbour.Y < maze.GetLength(1) &&
                    maze[neighbour.X, neighbour.Y]))
                {
                    count += 1;
                }
            }

            return count;
        }
    }
}