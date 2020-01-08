using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class BiggerIsGreaterTest
    {
        [TestMethod]
        [DataRow("ab", "ba", DisplayName = "ab")]
        [DataRow("lmno", "lmon", DisplayName = "lmno")]
        [DataRow("dcba", "no answer", DisplayName = "dcba")]
        [DataRow("dcbb", "no answer", DisplayName = "dcbb")]
        [DataRow("abdc", "acbd", DisplayName = "abdc")]
        public void Test(string input, string expected)
        {
            var bigger = new BiggerGreater();
            var result = bigger.BiggerIsGreater(input);

            Assert.AreEqual(expected, result);
        }
    }

    public class BiggerGreater
    {
        public string BiggerIsGreater(string w)
        {
            int maxIndex;
            // find character which lower than previous from right to left
            // fe: dkhc => d
            for (maxIndex = w.Length - 1; maxIndex > 0; maxIndex--)
            {
                if (w[maxIndex] > w[maxIndex - 1])
                    break;
            }

            if (maxIndex == 0)
                return "no answer";//than it ordered in by desc and it is not possible to made it greater

            maxIndex--;//move to that character 

            var subtraction = int.MinValue;
            int minIndex = 0;
            //find the smallest character which greater that w[maxIndex]
            //fe dkhc, maxIndex = 0 => minIndex = 3 => 'c'
            for (var i = maxIndex + 1; i < w.Length; i++)
            {
                if (w[maxIndex] < w[i] && w[maxIndex] - w[i] > subtraction)
                {
                    subtraction = w[maxIndex] - w[i];
                    minIndex = i;
                }
            }

            //swap and sort after that character
            var chars = w.ToCharArray();
            var temp = chars[maxIndex];
            chars[maxIndex] = chars[minIndex];
            chars[minIndex] = temp;

            var lengthToSort = chars.Length - (maxIndex + 1);
            if (lengthToSort > 1)
                Array.Sort(chars, maxIndex + 1, lengthToSort);

            return new string(chars);
        }
    }
}