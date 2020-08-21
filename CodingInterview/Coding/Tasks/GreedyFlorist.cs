using System;
using System.Collections.Generic;
using System.Linq;
using CodingInterview.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class GreedyFloristTest
    {
        [TestMethod]
        [DynamicData(nameof(Data), DynamicDataSourceType.Method)]
        public void Test(int k, int[] c, long expected)
        {
            var result = getMinimumCost(k, c);

            Assert.AreEqual(expected, result);
        }

        //https://www.hackerrank.com/challenges/greedy-florist/
        private static long getMinimumCost(int k, int[] c)
        {
            Array.Sort(c);

            long sum = 0;
            var peoples = new List<int>(k);
            int x = 0;
            for (; x < k; x++)
            {
                var index = c.Length - x - 1;
                sum += c[index];
                peoples.Add(1);
            }

            var kIndex = 0;
            for (int i = 0; i < c.Length - x;)
            {
                int price = (1 + peoples[kIndex]) * c[c.Length - x - 1];
                x++;

                sum += price;
                peoples[kIndex]++;

                kIndex++;
                if (kIndex >= peoples.Count)
                    kIndex = 0;
            }

            return sum;
        }


        private static IEnumerable<object[]> Data()
        {
            Func<string, IEnumerable<GreedyFloristData>> parser = (fileName) => TestDataParser.MultiCaseParse<GreedyFloristData>((option) =>
            {
                option.CaseSeparator = "#";
                option.ParseByLine = true;
                option.FileName = fileName;
                option.Initialize = () => new GreedyFloristData();
                option.ByLineParse = (data, line, lineNumber) =>
                {
                    var datas = line.Split(" ");
                    if (lineNumber == 0)
                    {
                        var count = int.Parse(datas[0]);
                        data.FriendsCount = int.Parse(datas[1]);
                        data.ExpectedResult = long.Parse(datas[2]);
                        data.Input = new int[count];
                    }
                    else
                    {
                        var l = line.Split(" ").Select(x => int.Parse(x)).ToArray();
                        data.Input = l;
                    }
                };
            });

            return new[] { @"Coding\Tasks\TestData\greedy-florist.txt" }
                .SelectMany(fileName => parser(fileName))
                .Select(data => new object[] { data.FriendsCount, data.Input, data.ExpectedResult });
        }

        private class GreedyFloristData : TestData<int[], long>
        {
            public int FriendsCount { get; set; }
        }
    }
}
