using System;
using NUnit;
using NUnit.Framework;
using FluentAssertions;

namespace second
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void METHOD1()
        {
            var actual = Program.FindMaxThread(4, 0, 3,
                new int[4, 4] {{0, 4, 2, 0}, {0, 0, 2, 1}, {0, 0, 0, 6}, {0, 0, 0, 0}}, out var actualFvalue);

            var expectedFValue = 5;
            var expected = new int[4, 4] {{0, 3, 2, 0}, {0, 0, 2, 1}, {0, 0, 0, 4}, {0, 0, 0, 0}};

            actualFvalue.Should().Be(expectedFValue);
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void METHOD2()
        {
            var actual = Program.FindMaxThread(4, 0, 3,
                new int[4, 4] {{0, 1000, 1000, 0}, {0, 0, 5, 1000}, {0, 0, 0, 1000}, {0, 0, 0, 0}},
                out var actualFvalue);

            var expectedFValue = 2000;
            var expected = new int[4, 4] {{0, 1000, 1000, 0}, {0, 0, 0, 1000}, {0, 0, 0, 1000}, {0, 0, 0, 0}};

            actualFvalue.Should().Be(expectedFValue);
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void METHOD3()
        {
            var actual = Program.FindMaxThread(3, 0, 2,
                new int[3, 3] {{0, 7, 0}, {0, 0, 2}, {0, 0, 0}}, out var actualFvalue);

            var expectedFValue = 2;
            var expected = new int[3, 3] {{0, 2, 0}, {0, 0, 2}, {0, 0, 0}};

            actualFvalue.Should().Be(expectedFValue);
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void METHOD4()
        {
            var n = 9;
            var s = 0;
            var t = 8;
            var f = new int[9, 9]
            {
                {0, 2, 0, 0, 2, 0, 2, 0, 0},
                {0, 0, 1, 0, 0, 2, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 1},
                {0, 0, 1, 0, 0, 0, 0, 1, 0},
                {0, 0, 2, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 2},
                {0, 0, 1, 1, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 1},
                {0, 0, 0, 0, 0, 0, 0, 0, 0}
            };
            var actual = Program.FindMaxThread(n, s, t, f, out var actualFvalue);
        }
    }
}