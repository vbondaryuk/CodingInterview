using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class ZigZagTest
    {
        [TestMethod]
        [DataRow("PAYPALISHIRING", 3, "PAHNAPLSIIGYIR")]
        public void Test(string input, int numRows, string expected)
        {
            var zigZag = new ZigZag();
            var result = zigZag.Convert(input, numRows);

            Assert.AreEqual(expected, result);
        }
    }

    //https://leetcode.com/problems/zigzag-conversion/
    public class ZigZag
    {
        public string Convert(string s, int numRows)
        {
            if (numRows == 1)
                return s;

            List<StringBuilder> builders = Enumerable.Range(0, numRows).Select(_ => new StringBuilder()).ToList();
            int row = 0;
            bool downDirection = true;
            for (int i = 0; i < s.Length; i++)
            {
                builders[row].Append(s[i]);
                if (downDirection)
                    row++;
                else
                    row--;
                if (row == 0 || row == numRows - 1)
                    downDirection = !downDirection;
            }

            for (int i = 1; i < numRows; i++)
            {
                builders[0].Append(builders[i]);
            }

            return builders[0].ToString();
        }
    }
}