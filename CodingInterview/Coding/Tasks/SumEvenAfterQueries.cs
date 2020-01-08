using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class SumEvenAfterQueriesTest
    {
        [TestMethod]
        public void Test()
        {
            int[] expected = { 8, 6, 2, 4 };

            int[] a = { 1, 2, 3, 4 };
            int[][] queries =
            {
                new[] {1, 0},
                new[]{-3, 1},
                new[] {-4, 0},
                new[] {2, 3}
            };
            var sum = new Sum();
            var result = sum.SumEvenAfterQueries(a, queries);

            CollectionAssert.AreEqual(expected, result);
        }
    }

    public class Sum
    {
        public int[] SumEvenAfterQueries(int[] A, int[][] queries)
        {
            var sums = new int[queries.Length];
            var sum = 0;
            for (int i = 0; i < A.Length; i++)
            {
                if (A[i] % 2 == 0)
                {
                    sum += A[i];
                }
            }

            for (int i = 0; i < queries.Length; i++)
            {
                var index = queries[i][1];
                var value = queries[i][0];

                if (A[index] % 2 == 0)
                    sum -= A[index];
                A[index] += value;
                if (A[index] % 2 == 0)
                    sum += A[index];

                sums[i] = sum;
            }

            return sums;
        }
    }
}