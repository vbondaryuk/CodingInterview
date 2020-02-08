using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class DigitDegreeTest
    {
        [TestMethod]
        [DataRow(100,1)]
        [DataRow(54, 1)]
        [DataRow(91, 2)]
        [DataRow(5, 0)]
        public void Test(int number, int expected)
        {
            var result = DigitDegree(number);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow(100, 1)]
        [DataRow(54, 1)]
        [DataRow(91, 2)]
        [DataRow(5, 0)]
        public void Test2(int number, int expected)
        {
            var result = DigitDegree2(number);
            Assert.AreEqual(expected, result);
        }
        /*
         * Let's define digit degree of some positive integer as the number of times we need to replace this number with the sum of its digits until we get to a one digit number.

        Given an integer, find its digit degree.

        Example

            For n = 5, the output should be
            digitDegree(n) = 0;
            For n = 100, the output should be
            digitDegree(n) = 1.
            1 + 0 + 0 = 1.
            For n = 91, the output should be
            digitDegree(n) = 2.
            9 + 1 = 10 -> 1 + 0 = 1.
         */
        private int DigitDegree(int n)
        {
            int count = 0;
            while (n >= 10)
            {
                int result = 0;
                int pos = (int)Math.Log10(n);
                int digits = (int)Math.Pow(10, pos);
                while (n > 0)
                {
                    var temp = n / digits;
                    result += temp;
                    n -= temp * digits;
                    digits /= 10;
                }
                count++;
                n = result;
            }

            return count;
        }

        private int DigitDegree2(int n)
        {
            int count = 0;
            while (n >= 10)
            {
                int buf = 0;
                while (n > 0)
                {
                    buf += n % 10;
                    n /= 10;
                }

                n = buf;
                ++count;
            }

            return count;
        }
    }
}
