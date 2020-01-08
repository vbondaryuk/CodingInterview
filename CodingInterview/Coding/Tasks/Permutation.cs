using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class PermutationTest
    {
        [TestMethod]
        [DataRow(3, 4, "231")]
        public void NextPermutation(int n, int k, string expected)
        {
            var permutation = new Permutation();
            var result = permutation.GetPermutation(n, k);

            Assert.AreEqual(expected, result);
        }
    }

    public class Permutation
    {
        //https://leetcode.com/problems/permutation-sequence/discuss/22507/%22Explain-like-I'm-five%22-Java-Solution-in-O(n)
        public string GetPermutation(int n, int k)
        {
            var factorial = new int[n+1];
            var nums = new List<int>();
            
            factorial[0] = 1;
            // fill factorial for (n)!
            // fill numbers
            for (int i = 1; i <= n; i++)
            {
                factorial[i] = factorial[i-1] * i;
                nums.Add(i);
            }

            StringBuilder builder = new StringBuilder(nums.Count);

            k--;

            // n = 3, k = 4 => array {1, 2, 3}
            // k-- since we work with i = 1; 
            // for a first step k/(n)! = 3/(2)! = 3/2 = 1.
            // The array {1, 2, 3} has a value of 2 at index 1. So the first number is a 1.
            // than remove index => arr = {1 , 3}
            for (int i = n-1; i >= 0; i--)
            {
                var fact = factorial[i];
                int index = k / fact;
                builder.Append(nums[index]);
                nums.RemoveAt(index);
                k -= index * fact;
            }

            return builder.ToString();
        }

        //https://leetcode.com/problems/permutations/
        public IList<IList<int>> Permute(int[] nums)
        {
            var permute = new List<IList<int>>();

            Permute(permute, nums, 0);

            return permute;
        }

        private static void Permute(IList<IList<int>> permute, int[] nums, int index)
        {
            if (index == nums.Length)
            {
                permute.Add(nums.ToList());
            }

            for (int i = index; i < nums.Length; i++)
            {
                (nums[index], nums[i]) = (nums[i], nums[index]);
                Permute(permute, nums, index + 1);
                (nums[index], nums[i]) = (nums[i], nums[index]);
            }
        }
    }
}
