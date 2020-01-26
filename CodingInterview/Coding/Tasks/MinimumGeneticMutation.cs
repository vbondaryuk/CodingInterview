using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class MinimumGeneticMutationTest
    {
        [TestMethod]
        
        public void Test()
        {
            var start = "AACCGGTT";
            var end = "AAACGGTA";
            var bank = new string[] {"AACCGGTA", "AACCGCTA", "AAACGGTA"};

            var minimumGeneticMutation = new MinimumGeneticMutation();
            var result = minimumGeneticMutation.MinMutation(start, end, bank);

            Assert.AreEqual(2, result);
        }
    }

    //https://leetcode.com/problems/minimum-genetic-mutation/
    public class MinimumGeneticMutation
    {
        public int MinMutation(string start, string end, string[] bank)
        {
            if (start != end && bank?.Length == 0)
                return -1;

            var result = MinMutation(start, end, bank, int.MaxValue, 0, new HashSet<string>());
            return result == int.MaxValue ? -1 : result;

        }

        private static int MinMutation(
            string start,
            string end,
            string[] bank,
            int minStep,
            int step,
            HashSet<string> visited)
        {
            if (start == end)
            {
                return Math.Min(minStep, step);
            }

            for (int j = 0; j < bank.Length; j++)
            {
                if(visited.Contains(bank[j]))
                    continue;
                
                visited.Add(bank[j]);
                if (IsAllowedStep(start, bank[j]))
                {
                    step++;
                    minStep = MinMutation(bank[j], end, bank, minStep, step, visited);
                    step--;
                }
                visited.Remove(bank[j]);
            }

            return minStep;
        }

        private static bool IsAllowedStep(string start, string to)
        {
            int num = 0;
            for (int i = 0; i < start.Length; i++)
            {
                if (start[i] != to[i])
                    num++;
            }

            return num <= 1;
        }
    }
}
