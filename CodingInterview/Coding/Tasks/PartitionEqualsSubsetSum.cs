using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class PartitionEqualsSubsetSumTest
    {
        [TestMethod]
        [DataRow(new int[] { 1, 2, 5 }, false)]
        public void CanPartition(int[] nums, bool expected)
        {
            var subsetSum = new PartitionEqualsSubsetSum();
            var result = subsetSum.CanPartition(nums);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow(5, new int[] { 1, 2, 5 }, 4)]
        public void ChangeTest(int amount, int[] coins, int expected)
        {
            var subsetSum = new PartitionEqualsSubsetSum();
            var result = subsetSum.Change(amount, coins);

            Assert.AreEqual(expected, result);
        }
    }

    public class PartitionEqualsSubsetSum
    {
        //https://leetcode.com/problems/partition-equal-subset-sum/
        public bool CanPartition(int[] nums)
        {
            var sum = 0;
            for (int i = 0; i < nums.Length; i++)
                sum += nums[i];

            //if we need to find subset, sum SHOULD definitely divided to 2, otherwise return false
            //therefore the find sum is the MIDDLE
            var middle = Math.DivRem(sum, 2, out int remainder);
            if (remainder != 0)
                return false;

            var dp = new bool?[nums.Length + 1, sum + 1];

            bool Find(int index, int target)
            {
                if (target == 0)
                    return true;

                if (index < 0 || target < 0)
                    return false;

                if (dp[index, target] != null)
                    return dp[index, target].Value;
                bool result = Find(index - 1, target) || Find(index - 1, target - nums[index]);
                dp[index, target] = result;

                return result;
            }

            return Find(nums.Length - 1, middle);
        }

        //https://leetcode.com/problems/coin-change-2/
        public int Change(int amount, int[] coins)
        {
            int[,] dp = new int[coins.Length + 1, amount + 1];
            int Find(int sum, int index)
            {
                if (sum == amount)
                    return 1;
                if (sum > amount)
                    return 0;
                int count = 0;
                for (int i = index; i < coins.Length; i++)
                    count += Find(sum + coins[i], i);

                return count;
            }

            return Find(0, 0);
        }
    }
}
