using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class LogFilesTest
    {
        [TestMethod]
        public void Test()
        {
            var expected = new[] {"let1 art can", "let3 art zero", "let2 own kit dig", "dig1 8 1 5 1", "dig2 3 6"};

            var reorderLogFiles = LogFiles.ReorderLogFiles(new[] {"dig1 8 1 5 1", "let1 art can", "dig2 3 6", "let2 own kit dig", "let3 art zero"});

            CollectionAssert.AreEqual(expected, reorderLogFiles);
        }
    }

    //https://leetcode.com/problems/reorder-data-in-log-files/
    public class LogFiles
    {
        // Reorder Data in Log Files
        public static string[] ReorderLogFiles(string[] logs)
        {
            if (logs == null || logs.Length == 0)
                return logs;

            Array.Sort(logs, (s, s1) =>
            {
                var firstArr = s.Split(new[] { ' ' }, 2);
                var secondArr = s1.Split(new[] { ' ' }, 2);

                var isDigitLogFirst = Char.IsDigit(firstArr[1][0]);
                var isDigitLogSecond = Char.IsDigit(secondArr[1][0]);

                if (isDigitLogFirst && isDigitLogSecond)
                    return 0;
                if (isDigitLogFirst)
                    return 1;
                if (isDigitLogSecond)
                    return -1;

                return string.Compare(firstArr[1], secondArr[1]);
            });

            return logs;
        }
    }
}