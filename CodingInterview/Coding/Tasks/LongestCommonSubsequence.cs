using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class LongestCommonSubsequenceTest
    {
        [TestMethod]
        [DataRow("sea", "eat", 2)]
        public void Test(string word1, string word2, int expected)
        {
            var lcs = new LongestCommonSubsequence();
            var result = lcs.MinDistance(word1, word2);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow("sea", "eat", 2)]
        public void Test2(string word1, string word2, int expected)
        {
            var lcs = new LongestCommonSubsequence();
            var result = lcs.MinDistance2(word1, word2);

            Assert.AreEqual(expected, result);
        }

        //[TestMethod]
        [DataRow(new []{ 1,2,3,4,1 }, new[] { 3,4,1,2,1,3 }, new[] { 1, 2, 3})]
        public void Test3(int[] a, int[] b, int[] expected)
        {
            var lcs = new LongestCommonSubsequence();
            var result = lcs.LongestCommonSubsequence3(a, b);

            CollectionAssert.AreEqual(expected, result);
        }
    }

    public class LongestCommonSubsequence
    {
        //https://leetcode.com/problems/delete-operation-for-two-strings/solution/
        public int MinDistance(string word1, string word2)
        {
            var buffer = new int[word1.Length, word2.Length];
            var longestCommonSubsequence = MinDistance(word1, word2, word1.Length - 1, word2.Length - 1, buffer);
            var result = word1.Length + word2.Length - 2 * longestCommonSubsequence;
            return result;
        }

        private int MinDistance(string word1, string word2, int index1, int index2, int[,] buffer)
        {
            if (index1 < 0 || index2 < 0)
                return 0;
            if (buffer[index1, index2] != 0)
                return buffer[index1, index2];

            var result = 0;
            if (word1[index1] == word2[index2])
            {
                result = 1 + MinDistance(word1, word2, index1 - 1, index2 - 1, buffer);
            }
            else
            {
                result = Math.Max(MinDistance(word1, word2, index1, index2 - 1, buffer),
                    MinDistance(word1, word2, index1 - 1, index2, buffer));
            }

            buffer[index1, index2] = result;

            return result;
        }


        public int MinDistance2(string word1, string word2)
        {
            var buffer = new int[word1.Length + 1, word2.Length + 1];
            for (int i = 1; i <= word1.Length; i++)
            {
                for (int j = 1; j <= word2.Length; j++)
                {
                    if (word1[i - 1] == word2[j - 1])
                    {
                        buffer[i, j] = buffer[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        buffer[i, j] = Math.Max(buffer[i - 1, j], buffer[i, j - 1]);
                    }
                }
            }

            var longestCommonSubsequence = buffer[word1.Length, word2.Length];
            var result = word1.Length + word2.Length - 2 * longestCommonSubsequence;

            return result;
        }

        public int[] LongestCommonSubsequence3(int[] a, int[] b)
        {
            var buffer = new List<List<int>>(b.Length + 1);
            var current = new List<List<int>>(b.Length + 1);

            for (int i = 0; i <= b.Length; i++)
            {
                buffer.Add(new List<int>());
                current.Add(new List<int>());
            }

            for (int i = 1; i <= a.Length; i++)
            {
                for (int j = 1; j <= b.Length; j++)
                {
                    current[j].Clear();
                    if (a[i - 1] == b[j - 1])
                    {
                        current[j].Add(a[i - 1]);
                        current[j].AddRange(buffer[i - 1]);
                    }
                    else
                    {
                        if (current[j - 1].Count > buffer[j].Count)
                        {
                            current[j].AddRange(current[j - 1]);
                        }
                        else
                        {
                            current[j].AddRange(buffer[j - 1]);
                        }
                    }
                }

                var temp = buffer;
                buffer = current;
                current = temp;
            }

            buffer[buffer.Count-1].Reverse();
            return buffer[buffer.Count-1].ToArray();
        }
    }
}
