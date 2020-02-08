using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    /*
     * Given some integer, find the maximal number you can obtain by deleting exactly one digit of the given number.

        Example

            For n = 152, the output should be
            deleteDigit(n) = 52;
            For n = 1001, the output should be
            deleteDigit(n) = 101.
     */
    [TestClass]
    public class DeleteDigitTest
    {
        [TestMethod]
        [DataRow(152, 52)]
        [DataRow(1001, 101)]
        [DataRow(100, 10)]
        public void Test(int number, int expected)
        {
            var result = DeleteDigit2(number);
            Assert.AreEqual(expected, result);
        }

        private static int DeleteDigit(int n)
        {
            int count = 1;
            int max = int.MinValue;
            while (count <= n)
            {
                var mod = n % count;
                //n=152 count = 1 => mod = 0, number = 15  => 15
                //n=152 count = 10 => mod = 2, number = 10 => 12
                //n=152 count = 100 => mod = 52 number = 0 => 52
                int number = n / count / 10 * count;
                var tempMax = mod + number;

                //var temp = n;
                //var tempMax = 0;
                //var pow = 1;
                //for (int i = 1; temp!= 0; i++)
                //{
                //    int digit = temp % 10;
                //    temp = temp / 10;
                //    if (i == count)
                //        continue;
                //    tempMax = digit * pow + tempMax;
                //    pow *= 10;
                //}

                if (tempMax > max)
                    max = tempMax;
                count *= 10;
            }

            return max;
        }

        //n=1001
        //d=100
        //
        private static int DeleteDigit2(int n)
        {
            int ans = 0;
            for (int d = 1; d <= n; d *= 10)
            {
                int tmp = n % d + ((n / d) / 10) * d;
                ans = Math.Max(ans, tmp);
            }

            return ans;
        }
    }

}
