using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class CircularArrayLoopTest
    {
        [TestMethod]
        [DataRow(new[] { 2, -1, 1, 2, 2 }, true)]
        [DataRow(new[] { -2, 1, -1, -2, -2 }, false)]
        [DataRow(new[] { -2, -3,-9 }, false)]
        public void Test(int[] nums, bool expected)
        {
            var cal = new CircularArray();
            var result = cal.CircularArrayLoop(nums);

            Assert.AreEqual(expected, result);
        }
    }

    public class CircularArray
    {
        public bool CircularArrayLoop(int[] nums)
        {
            if (nums == null || nums.Length == 1)
                return false;

            for (int i = 0; i < nums.Length; i++)
            {
                //we can set elements to 0 if we look through the direction
                //and didn't find the way
                if (nums[i] == 0)
                    continue;

                int slow = i;
                int fast = GetIndex(nums, i);

                //here we multiply the current index and next and next.next so if sign is different we exit from traverse
                while (nums[i] * nums[fast] > 0
                       && nums[i] * nums[GetIndex(nums, fast)] > 0)
                {
                    if (slow == fast)
                    {
                        if (slow == GetIndex(nums, slow))
                            break; // loop contains only one element
                        return true;
                    }

                    slow = GetIndex(nums, slow);
                    fast = GetIndex(nums, GetIndex(nums, fast));
                }

                slow = i;
                int sign = nums[i];
                while (sign * nums[slow] > 0)
                {
                    int temp = GetIndex(nums, slow);
                    nums[slow] = 0;
                    slow = temp;
                }
            }

            return false;
        }

        private static int GetIndex(int[] nums, int index)
        {
            var length = nums.Length;
            index += nums[index];
            if (index >= 0)
            {
                //index = 5 length=3 => index=1
                //index =1 length=3 => index = 1
                index %= length;
            }
            else
            {   //in case length = 3 index = -9
                //index = 3
                index = (index % length) + length;
            }

            return index;
        }
    }
}