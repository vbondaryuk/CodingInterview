using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class NonDivisibleSubsetTest
    {
        [TestMethod]
        public void Test()
        {
            var result = nonDivisibleSubset(4, new List<int>{ 19, 10, 12, 10, 24, 25, 22 });

            Assert.AreEqual(3, result);
        }
        /*
         https://www.hackerrank.com/challenges/non-divisible-subset/problem
		 s=[19,10,12,10,24,25,22]
		 k=4
         
		 1) find remainder for every item in array
		 0 = 2
		 1 = 1
		 2 = 3
		 3 = 1
         
		 2) there are a few points
		    2.1 we cannot add 2 elements from index 0 since the sum of elements which divided to K also divided to K
		    2.2 we cannot add 2 element remainders sum of whose equals to K. i.e. k = 4 and two remainder 3 and 1 sum equal of 4
				so we choose max between them
		 */
        private static int nonDivisibleSubset(int k, List<int> s)
        {
            var remainders = new int[k];
            for (int i = 0; i < s.Count; i++)
                remainders[s[i] % k]++;
            int numbers = 0;
            for (int i = 1; i <= k / 2; i++)
            {
                if (i == k - i)
                {
                    numbers++;
                    continue;
                }
                numbers += Math.Max(remainders[i], remainders[k - i]);
            }
            if (remainders[0] > 0)
                numbers++;

            return numbers;
        }
    }
}
