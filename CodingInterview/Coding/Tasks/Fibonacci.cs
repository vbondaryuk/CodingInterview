using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class FibonacciTest
    {
        [TestMethod]
        public void Test()
        {
            var fib = new Fibonacci();
            var result = fib.Get(10);

            Assert.AreEqual(55, result);
        }

        [TestMethod]
        public void TestRecursively()
        {
            var fib = new Fibonacci();
            var result = fib.GetRec(10);

            Assert.AreEqual(55, result);
        }
    }
    public class Fibonacci
    {
        public int Get(int n)
        {
            int sum = 0;
            int f0 = 0;
            int f1 = 1;

            if (n == 0)
                return f0;
            if (n == 1)
                return f1;

            for (int i = 2; i <= n; i++)
            {
                sum = f0 + f1;
                f0 = f1;
                f1 = sum;
            }

            return sum;
        }

        private Dictionary<int, int> dict = new Dictionary<int, int>
        {
            { 0, 0 },
            { 1, 1 }
        };
        public int GetRec(int i)
        {
            if (dict.ContainsKey(i))
                return dict[i];

            dict[i] = GetRec(i - 1) + GetRec(i - 2);

            return dict[i];
        }
    }
}