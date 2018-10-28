using System;
using System.Collections.Generic;
using System.Linq;
using NUnit;
using NUnit.Framework;
using FluentAssertions;

namespace third
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void METHOD1()
        {
            var maze = new bool[3, 3]
            {
                {true, true, true},
                {true, false, true},
                {true, true, true},
            };
            var current = new Vector(0, 0);
            Program.GetAllNeighbours(maze, current).ToList().Should()
                .BeEquivalentTo(new List<Vector> {new Vector(1, 0), new Vector(0, 1)});
        }

        [Test]
        public void METHOD2()
        {
            var maze = new bool[3, 3]
            {
                {true, true, true},
                {true, false, true},
                {true, true, true},
            };
            var current = new Vector(0, 1);
            Program.GetAllNeighbours(maze, current).ToList().Should()
                .BeEquivalentTo(new List<Vector> {new Vector(0, 0), new Vector(0, 2)});
        }

        [Test]
        public void METHOD3()
        {
            var maze = new bool[3, 3]
            {
                {true, true, true},
                {true, false, true},
                {true, true, true},
            };
            var current = new Vector(0, 2);
            Program.GetDegree(maze, current).Should().Be(2);
        }

        [Test]
        public void METHOD4()
        {
            var maze = new bool[3, 3]
            {
                {true, true, true},
                {true, false, true},
                {true, true, true},
            };
            var current = new Vector(0, 1);
            Program.GetDegree(maze, current).Should().Be(2);
        }

        [Test]
        public void METHOD5()
        {
            var maze = new bool[5, 5]
            {
                {true, true, true, true, true},
                {true, true, true, false, false},
                {true, true, false, true, true},
                {true, true, false, false, false},
                {true, true, true, true, true},
            };
            Program.GetVisibleWallCount(maze).Should().Be(26);//Не вычитаю пока на входе 2 и на выходе 2
        }
    }
}