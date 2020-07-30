using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using CodingInterview.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class BeautifulPairs
    {
        [TestMethod]
        [DynamicData(nameof(Data), DynamicDataSourceType.Method)]
        public void Test(int[] a, int[] b, long expected)
        {
            var result = beautifulPairs(a, b);

            Assert.AreEqual(expected, result);
        }

        //https://www.hackerrank.com/challenges/beautiful-pairs/problem
        static int beautifulPairs(int[] A, int[] B)
        {
            Dictionary<int, int> setA = new Dictionary<int, int>();
            for (int i = 0; i < A.Length; i++)
            {
                if (!setA.ContainsKey(A[i]))
                    setA[A[i]] = 0;
                setA[A[i]]++;
            }

            var result = 0;
            for (int i = 0; i < B.Length; i++)
            {
                int b = B[i];
                if (setA.ContainsKey(b) && setA[b] > 0)
                {
                    setA[b]--;
                    result++;
                }
            }

            if (result < A.Length)
                result++;
            else
                result--;

            return result;
        }

        private static IEnumerable<object[]> Data()
        {
            Func<string, PairsTestData> parser = 
                (fileName) => TestDataParser.Parse<PairsTestData>((option) =>
            {
                option.ParseByLine = true;
                option.FileName = fileName;
                option.Initialize = () => new PairsTestData();
                option.ByLineParse = (data, line, lineNumber) =>
                {
                    var datas = line.Split(" ");
                    if (lineNumber == 0)
                    {
                        data.ExpectedResult = int.Parse(datas[1]);
                    }else if(lineNumber == 1)
                    {
                        data.A = line.Split(" ").Select(x => int.Parse(x)).ToArray();
                    }
                    else
                    {
                        data.B = line.Split(" ").Select(x => int.Parse(x)).ToArray();
                    }
                };
            });

            return new[] { @"Coding\Tasks\TestData\beautiful-pairs.txt" }
                .Select(x=>parser(x))
                .Select(data => new object[] { data.A, data.B, data.ExpectedResult });
        }

        public class PairsTestData
        {
            public int[] A { get; set; }
            public int[] B { get; set; }
            public int ExpectedResult { get; set; }
        }
    }
}
