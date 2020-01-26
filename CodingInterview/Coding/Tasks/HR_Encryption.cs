using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class HR_EncryptionTest
    {
        [TestMethod]
        [DataRow("haveaniceday", "hae and via ecy")]
        [DataRow("feedthedog", "fto ehg ee dd")]
        public void Test(string str, string expected)
        {
            var result = Encryption(str);

            Assert.AreEqual(expected, result);
        }
        /*
         * feed
         * thed
         * og
         */

        //https://www.hackerrank.com/challenges/encryption/problem
        private static string Encryption(string s)
        {
            var str = s.Replace(" ", "");
            int row = 0, column = 0;
            SetRowAndColumn(str, ref row, ref column);
            char[] result = new char[row * column + column - 1];

            int index = 0;
            for (int i = 0; i < column; i++)
            {
                int j = i;
                while (j < str.Length)
                {
                    result[index++] = str[j];
                    j += column;
                }
                if (i + 1 < column)
                    result[index++] = ' ';
            }

            return new string(result,0, index);
        }

        private static void SetRowAndColumn(string str, ref int row, ref int column)
        {
            var sqr = (int)Math.Sqrt(str.Length);
            if (sqr * sqr == str.Length)
            {
                row = column = sqr;
            }
            else
            {
                if (sqr * (sqr + 1) >= str.Length)
                {
                    row = sqr;
                    column = sqr + 1;
                }
                else
                {
                    row = column = sqr + 1;
                }
            }
        }
    }
}
