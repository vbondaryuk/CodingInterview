using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class CombinationSumTest
    {
        [TestMethod]
        [DataRow(new int[]{ 2, 3, 6, 7 }, 7, 2)]
        public void CombinationSumI(int[] candidates, int target, int expectedCount)
        {
            var sums = new CombinationSum();
            var result = sums.CombinationSumI(candidates, target);

            Assert.AreEqual(expectedCount, result.Count);
        }
        
        [TestMethod]
        [DataRow(new int[]{10, 1, 2, 7, 6, 1, 5}, 8, 4)]
        public void CombinationSumII(int[] candidates, int target, int expectedCount)
        {
            var sums = new CombinationSum();
            var result = sums.CombinationSumII(candidates, target);

            Assert.AreEqual(expectedCount, result.Count);
        }
    }

    public class CombinationSum
    {
        //https://leetcode.com/problems/combination-sum/submissions/
        public IList<IList<int>> CombinationSumI(int[] candidates, int target)
        {
            var result = new List<IList<int>>();
            Array.Sort(candidates);

            void Collect(int index, int sum, List<int> items)
            {
                if (sum == target)
                {
                    result.Add(items.ToArray());
                    return;
                }

                if (sum > target)
                    return;

                for (int i = index; i < candidates.Length; i++)
                {
                    items.Add(candidates[i]);
                    Collect(i, candidates[i] + sum, items);//i because we can use the same element multiple times
                    items.RemoveAt(items.Count - 1);
                }
            }

            var list = new List<int>();
            Collect(0, 0, list);

            return result;
        }

        //https://leetcode.com/problems/combination-sum-ii/
        public IList<IList<int>> CombinationSumII(int[] candidates, int target)
        {
            var result = new List<IList<int>>();
            Array.Sort(candidates);

            void Collect(int index, int sum, List<int> items)
            {
                if (sum == target)
                {
                    result.Add(items.ToArray());
                    return;
                }

                if (sum > target)
                    return;

                for (int i = index; i < candidates.Length; i++)
                {
                    //for duplicate skipping
                    if (i > index && candidates[i] == candidates[i - 1])
                        continue;

                    items.Add(candidates[i]);
                    Collect(i + 1, candidates[i] + sum, items);
                    items.RemoveAt(items.Count - 1);
                }
            }

            var list = new List<int>();
            Collect(0, 0, list);

            return result;
        }
    }
}