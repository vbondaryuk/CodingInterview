using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class ReverseIntegerTest
    {
        [TestMethod]
        [DataRow(-2147483412, -2143847412)]
        [DataRow(-2147483648, 0)]
        [DataRow(-123, -321)]
        [DataRow(25, 52)]
        public void Test(int x, int expected)
        {
            var ri = new ReverseInteger();
            var result = ri.Reverse(x);

            Assert.AreEqual(expected, result);
        }
    }

    //https://leetcode.com/problems/reverse-integer/
    public class ReverseInteger
    {
        public int Reverse(int x)
        {
            if (x == 0)
                return x;

            int i = 0;
            int max = int.MaxValue / 10;
            int min = int.MinValue / 10;

            while (x != 0)
            {
                var div = x % 10;
                x /= 10;

                if (max < i || min > i)
                    return 0;

                i = i * 10 + div;
            }

            return i;
        }
    }
}