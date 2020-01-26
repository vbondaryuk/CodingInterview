using System;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class ShortestCommonSupersequenceTest
    {
        [TestMethod]
        [DataRow("bcd", "abc", "abcd")]
        public void Test(String word1, string word2, string expected)
        {
            var scs = new ShortestCommonSupersequence();
            var result = scs.SCS(word1, word2);

            Assert.AreEqual(expected, result);
        }


    }

    // str1 = bcd
    // str2 = abc
    //
    //      a    b    c
    // b         a    ab   abc
    // c    b    ba   ab   abc
    // d    bc   bca  abc  abc
    // bcd  bcda abcd abcd abcd



    //https://leetcode.com/problems/shortest-common-supersequence/
    public class ShortestCommonSupersequence
    {
        public string SCS(string str1, string str2)
        {
            int m = str1.Length, n = str2.Length;
            StringBuilder[] buffer = new StringBuilder[n + 1];
            StringBuilder[] current = new StringBuilder[n + 1];
            for (int i = 0; i < n + 1; i++)
            {
                buffer[i] = new StringBuilder();
                current[i] = new StringBuilder();
            }
            for (int i = 0; i <= m; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    current[j].Clear();
                    if (i == 0)
                    {
                        current[j].Append(str2.Substring(0, j));
                    }
                    else if (j == 0)
                    {
                        current[j].Append(str1.Substring(0, i));
                    }
                    else if (str1[i - 1] == str2[j - 1])
                    {
                        current[j].Append(buffer[j - 1]).Append(str1[i - 1]);
                    }
                    else
                    {
                        if (buffer[j].Length < current[j - 1].Length)
                        {
                            current[j].Append(buffer[j]).Append(str1[i - 1]);
                        }
                        else
                        {
                            current[j].Append(current[j - 1]).Append(str2[j - 1]);
                        }
                    }
                }

                var temp = buffer;
                buffer = current;
                current = temp;
            }
            return buffer[buffer.Length - 1].ToString();
        }
    }
}
