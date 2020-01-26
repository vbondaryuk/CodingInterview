using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class QueensAttackTest
    {
        [TestMethod]
        public void Test()
        {
            var obs = new int[][]
            {
                new int[] {5, 5},
                new int[] {4, 2},
                new int[] {2, 3}
            };

            var attack = queensAttack(5, 5, 4, 3, obs);

            Assert.AreEqual(10, attack);
        }

        //https://www.hackerrank.com/challenges/queens-attack-2/problem
        static int queensAttack(
            int n,
            int k,
            int rQ,
            int cQ,
            int[][] obstacles)
        {
            var set = new HashSet<Tuple<int, int>>();
            for (int i = 0; i < obstacles.Length; i++)
                set.Add(Tuple.Create(obstacles[i][1], obstacles[i][0]));

            var count = 0;
            count += GetPossibleSteps(n, k, rQ, cQ, set, (Tuple<int, int> point) => Tuple.Create(point.Item1, point.Item2 - 1));
            count += GetPossibleSteps(n, k, rQ, cQ, set, (Tuple<int, int> point) => Tuple.Create(point.Item1, point.Item2 + 1));
            count += GetPossibleSteps(n, k, rQ, cQ, set, (Tuple<int, int> point) => Tuple.Create(point.Item1 - 1, point.Item2));
            count += GetPossibleSteps(n, k, rQ, cQ, set, (Tuple<int, int> point) => Tuple.Create(point.Item1 + 1, point.Item2));
            count += GetPossibleSteps(n, k, rQ, cQ, set, (Tuple<int, int> point) => Tuple.Create(point.Item1 - 1, point.Item2 - 1));
            count += GetPossibleSteps(n, k, rQ, cQ, set, (Tuple<int, int> point) => Tuple.Create(point.Item1 - 1, point.Item2 + 1));
            count += GetPossibleSteps(n, k, rQ, cQ, set, (Tuple<int, int> point) => Tuple.Create(point.Item1 + 1, point.Item2 + 1));
            count += GetPossibleSteps(n, k, rQ, cQ, set, (Tuple<int, int> point) => Tuple.Create(point.Item1 + 1, point.Item2 - 1));

            return count;
        }

        private static int GetPossibleSteps(
            int n,
            int k,
            int rQ,
            int cQ,
            HashSet<Tuple<int, int>> obstacles,
            Func<Tuple<int, int>, Tuple<int, int>> func)
        {
            var steps = 0;
            Tuple<int, int> point = func(Tuple.Create(cQ, rQ));
            while (point.Item1 > 0 && point.Item1 <= n && point.Item2 > 0 && point.Item2 <= k)
            {
                if (obstacles.Contains(point))
                    break;
                steps++;
                point = func(point);
            }

            return steps;
        }
    }
}
