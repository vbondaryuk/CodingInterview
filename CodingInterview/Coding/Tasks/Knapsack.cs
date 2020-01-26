using System;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class KnapsackTest
    {
        [TestMethod]
        [DataRow(
            new int[] { 1, 2, 4, 2, 5 },
            new int[] { 5, 3, 5, 3, 2 },
            10,
            11
            )]
        public void Test(int[] weights, int[] values, int total, int expected)
        {
            var knapsack = new Knapsack();
            var result = knapsack.FindKnapsack(weights, values, total);

            Assert.AreEqual(expected, result);
        }
    }
    public class Knapsack
    {
        public int FindKnapsack(int[] weights, int[] values, int total)
        {
            var temp = new int[weights.Length + 1, total + 1];
            int FindKnapsack(int index, int rest)
            {
                if (index == 0 || rest == 0)
                    return 0;
                if (temp[index, rest] != 0)
                    return temp[index, rest];

                var result = FindKnapsack(index - 1, rest);
                if (weights[index] > rest)
                {
                    //return FindKnapsack(index - 1, rest);
                }
                else
                {
                    var second = values[index] + FindKnapsack(index - 1, rest - values[index]);
                    result = Math.Max(result, second);
                }

                temp[index, rest] = result;

                return result;
            }

            return FindKnapsack(weights.Length - 1, total);
        }
    }
}
