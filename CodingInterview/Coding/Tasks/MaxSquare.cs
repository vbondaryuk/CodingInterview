using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class MaxSquareTest
    {
        [TestMethod]
        public void Test()
        {
            var input = new char[][]
            {
                new []{'1', '0', '1', '0', '0'},
                new []{'1', '0', '1', '1', '1'},
                new []{'1', '1', '1', '1', '1'},
                new []{'1', '0', '0', '1', '0'}
            };

            var expected = 4;

            var maxSq = new MaxSquare();
            var result = maxSq.MaximalSquare(input);

            Assert.AreEqual(expected, result);
        }
    }
    public class MaxSquare
    {
        public int MaximalSquare(char[][] matrix)
        {
            if (matrix == null || matrix.Length == 0)
                return 0;

            var max = int.MinValue;
            var sumMatrix = new int[matrix.Length][];
            sumMatrix[0] = new int[matrix[0].Length];
            for (int i = 0; i < matrix.Length; i++)
            {
                sumMatrix[i] = new int[matrix[i].Length];
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (AsInt(matrix[i][j]) == 1)
                    {
                        if (i != 0 && j != 0)
                        {
                            sumMatrix[i][j] = Math.Min(sumMatrix[i - 1][j], Math.Min(sumMatrix[i - 1][j - 1], sumMatrix[i][j - 1])) + 1;
                        }
                        else
                        {
                            sumMatrix[i][j] = 1;
                        }

                        if (sumMatrix[i][j] > max)
                        {
                            max = sumMatrix[i][j];
                        }
                    }
                }
            }

            return max == int.MinValue ? 0 : max * max;
        }

        private static int AsInt(char c) => (int)char.GetNumericValue(c);
    }
}
