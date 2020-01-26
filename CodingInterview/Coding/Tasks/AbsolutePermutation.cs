using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class AbsolutePermutationTest
    {
        [TestMethod]
        public void Test()
        {
            // n=6; k=3
            // 1 2 3  4 5 6
            // -----
            // 4 5 6 \ 1 2 3
            //        ------

            var expected = new[] {4, 5, 6, 1, 2, 3};
            var result = AbsolutePermutation(6, 3);

            CollectionAssert.AreEqual(expected, result);
        }

        //https://www.hackerrank.com/challenges/absolute-permutation/problem
        static int[] AbsolutePermutation(int n, int k)
        {
            if (k > 0 && (n / k) % 2 != 0)
                return new[] { -1 };

            int[] arr = new int[n];
            int m = 1;
            for (int i = 0, j = 0; i < n; i++, j++)
            {
                if (j == k)
                {
                    j = 0;
                    m *= -1;
                }

                int value = i + 1 + k * m;
                if (value < 1 || value > n)
                    return new[] { -1 };
                arr[i] = value;
            }

            return arr;
        }
    }
}
