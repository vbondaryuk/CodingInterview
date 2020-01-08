using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class SubArraySumTest
    {
        [TestMethod]
        public void Test()
        {
            int[] arr = new[] { 1, 2, 2 };

            var subArr = new SubArraySum();
            var count = subArr.SubarraySum(arr, 4);

            Assert.AreEqual(1, count);
        }
    }

    public class SubArraySum
    {
        //https://leetcode.com/problems/subarray-sum-equals-k/
        public int SubarraySum(int[] nums, int k)
        {
            var count = 0;
            int sum = 0;
            var dict = new Dictionary<int, int>
            {
                {0,1}
            };
            foreach (var num in nums)
            {
                sum += num;
                if (dict.ContainsKey(sum - k))
                    count += dict[sum - k];

                if (dict.ContainsKey(sum))
                {
                    dict[sum]++;
                }
                else
                {
                    dict.Add(sum, 1);
                }
            }


            return count;
        }
    }
}