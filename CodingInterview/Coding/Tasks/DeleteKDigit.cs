using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class DeleteKDigit
    {
        [TestMethod]
        [DataRow(152, 1, 52)]
        [DataRow(1001, 2, 101)]
        [DataRow(152, 3, 15)]
        public void DeleteKDigitFromStartTest(int number, int k, int expected)
        {
            var result = DeleteKDigitFromStart(number, k);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow(152, 1, 15)]
        [DataRow(101234, 2, 10124)]
        [DataRow(152, 3, 52)]
        public void DeleteKDigitFromEndTest(int number, int k, int expected)
        {
            var result = DeleteKDigitFromEnd(number, k);
            Assert.AreEqual(expected, result);
        }

        private int DeleteKDigitFromStart(int number, int k)
        {
            int digitCounts = (int)Math.Floor(Math.Log10(number)) + 1;// if K starts from 0 => + 0 if from 1 => + 1
            digitCounts -= k;
            int mark = (int)Math.Pow(10, digitCounts);
            var result = number % mark + number / mark / 10 * mark;

            return result;

        }
        private int DeleteKDigitFromEnd(int number, int k)
        {
            int mark = k == 1 ? 1 : (int)Math.Pow(10, k - 1);
            var result = number % mark + number / mark / 10 * mark;

            return result;
        }
    }
}
