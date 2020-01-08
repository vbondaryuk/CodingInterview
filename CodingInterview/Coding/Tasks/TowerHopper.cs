using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class TowerHopperTest
    {
        [TestMethod]
        [DataRow(new int[] { 1, 3, 6, 1, 0, 9 }, true)]
        [DataRow(new int[] { 1, 0, 6, 1, 0, 9 }, false)]
        public void TestCanJump(int[] arr, bool expected)
        {
            var tower = new TowerHopper();
            var result = tower.CanJump(arr);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow(new int[] { 1, 3, 6, 1, 0, 9 }, 3)]
        [DataRow(new int[] { 1, 3, 5, 8, 9, 2, 6, 7, 6, 8, 9 }, 3)]
        [DataRow(new int[] { 1, 1, 1, 1, 1 }, 4)]
        public void TestMinJumpOn2(int[] arr, int expected)
        {
            var tower = new TowerHopper();
            var result = tower.JumpOn2(arr);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        // [DataRow(new int[] { 1, 3, 6, 1, 0, 9 }, 3)]
        // [DataRow(new int[] { 1, 3, 5, 8, 9, 2, 6, 7, 6, 8, 9 }, 3)]
        [DataRow(new int[] { 1, 1, 1, 1, 1 }, 4)]

        public void TestMinJumpOn(int[] arr, int expected)
        {
            var tower = new TowerHopper();
            var result = tower.JumpOn(arr);

            Assert.AreEqual(expected, result);
        }
    }
    public class TowerHopper
    {
        //https://leetcode.com/problems/jump-game/solution/
        public bool CanJump(int[] nums)
        {
            //take the last index of array and set to lastGoodIndex.
            //move from right to left. if the current index + arr[index] more or equals the last good index,
            //therefore we can jump to it. do it while the index is not zero. then return value last good index it should be zero.
            var lastGoodIndex = nums.Length - 1;
            for (int i = nums.Length - 1; i >= 0; i--)
            {
                if (nums[i] + i >= lastGoodIndex)
                    lastGoodIndex = i;
            }

            return lastGoodIndex == 0;
        }

        //https://leetcode.com/problems/jump-game-ii/
        //O(n^2)
        public int JumpOn2(int[] arr)
        {
            if (arr == null || arr.Length == 0 || arr[0] == 0)
                return int.MaxValue;

            //store the number of jumps to the current position.
            int[] jumps = new int[arr.Length];

            //start from 1 to validate if we can jump to that.
            for (int i = 1; i < arr.Length; i++)
            {
                //set maxvalue to verify that we have lowest number of jumps
                jumps[i] = int.MaxValue;
                for (int j = 0; j < i; j++)
                {
                    //starting from the first index check
                    //if arr[j] + j >= i than we can jump to that so jumps[i] = jumps[j] + 1
                    //if jumps[j] == intMax than we cannot jump to that.
                    if (i <= arr[j] + j && jumps[j] != int.MaxValue)
                    {
                        jumps[i] = jumps[j] + 1;
                        break;
                    }
                }
            }

            return jumps[arr.Length - 1];
        }

        public int JumpOn(int[] arr)
        {
            if (arr == null || arr.Length == 0 || arr[0] == 0)
                return int.MaxValue;

            int maxReach = arr[0];
            int step = arr[0];
            int jump = 1;

            for (int i = 1; i < arr.Length; i++)
            {
                if (i == arr.Length - 1)
                    return jump;

                maxReach = Math.Max(maxReach, i + arr[i]);
                step--;

                if (step == 0)
                {
                    jump++;
                    if (i >= maxReach)
                        return int.MaxValue;

                    step = maxReach - i;
                }
            }

            return jump;
        }
    }
}