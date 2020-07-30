using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using CodingInterview.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class Candies
    {
        [TestMethod]
        [DataRow(3, new[] { 1, 2, 2 }, 4)]
        [DataRow(8, new[] { 2, 4, 3, 5, 2, 6, 4, 5 }, 12)]
        [DynamicData(nameof(Data), DynamicDataSourceType.Method)]
        public void Test(int n, int[] arr, long expected)
        {
            var result = candies_v2(n, arr);

            Assert.AreEqual(expected, result);
        }

        //https://www.hackerrank.com/challenges/candies/problem
        static long candies_v2(int n, int[] arr)
        {
            if (n == 1)
                return 1;

            var result = new List<int>();
            bool? toUpper = null;


            int count = 1;
            if (arr[0] > arr[1])
            {
                count = 1;
                toUpper = false;
            }
            else
            {
                result.Add(1);
            }

            int needToAdd = 0;
            for (int i = 1; i < n; i++)
            {
                var cur = arr[i];
                var prev = arr[i - 1];
                if (cur > prev)
                {
                    if (toUpper == false)
                    {
                        AddToDecrease(count, result, ref needToAdd);
                        toUpper = null;
                    }

                    bool nextLowest = false;
                    if (i + 1 < n)
                    {
                        nextLowest = cur > arr[i + 1];
                    }

                    if (nextLowest)
                    {
                        needToAdd = result[result.Count - 1] + 1;
                        toUpper = false;
                        count = 0;
                    }
                    else
                    {
                        result.Add(result[result.Count - 1] + 1);
                    }
                }
                else if (cur < prev)
                {
                    if (toUpper == false)
                    {
                        count++;
                    }
                    else
                    {
                        count = 1;
                    }
                    toUpper = false;
                }
                else
                {
                    if (toUpper == false)
                    {
                        AddToDecrease(count, result, ref needToAdd);
                        toUpper = null;
                    }

                    bool nextLowest = false;
                    if (i + 1 < n)
                    {
                        nextLowest = cur > arr[i + 1];
                    }

                    if (nextLowest)
                    {
                        needToAdd = 1;
                        toUpper = false;
                        count = 0;
                    }
                    else
                    {
                        result.Add(1);
                    }
                }
            }
            if (toUpper == false)
            {
                AddToDecrease(count, result, ref needToAdd);
            }

            return result.Sum(x => (long)x);
        }

        private static void AddToDecrease(int count, List<int> candies, ref int add)
        {

            if (add != 0)
            {
                if (add <= count)
                {
                    candies.Add(count + 1);
                }
                else
                {
                    candies.Add(add);
                }
                add = 0;
            }

            for (int i = count; i > 0; i--)
            {
                candies.Add(i);
            }
        }

        static long candies(int n, int[] arr)
        {
            var candyGraph = new CandyGraph();

            for (int i = 0; i < n; i++)
            {
                var cur = arr[i];
                candyGraph.Add(cur);
            }
            var result = new List<long>();
            var current = candyGraph.Root;
            long next = 1;
            for (int i = 0; i < n; i++)
            {
                if (current.NextHighestNode > 0)
                {
                    result.Add(next);
                    next++;
                }
                else if (current.NextLowestNode > 0)
                {
                    if (next <= current.NextLowestNode)
                        next = current.NextLowestNode + 1;
                    result.Add(next);
                    next = current.NextLowestNode;
                }
                else
                {
                    if (current.Previous.NextLowestNode > 0)
                    {
                        next = 1;
                    }
                    result.Add(next);
                }

                current = current.Next;
            }

            return result.Sum(x => x);
        }

        public class CandyGraph
        {
            public CandyNode Root;
            private CandyNode Current;

            public void Add(int i)
            {
                if (Current == null)
                {
                    Current = new CandyNode { Value = i };
                    Root = Current;
                }
                else
                {

                    Current.Next = new CandyNode { Value = i, Previous = Current };
                    Current = Current.Next;

                    bool? toHighest = null;
                    CandyNode curr = Current;
                    while ((curr = curr.Previous) != null)
                    {
                        bool isNextHighest = curr.Value < i;
                        if (toHighest == null)
                        {
                            toHighest = isNextHighest;
                        }

                        if (isNextHighest == toHighest)
                        {
                            if (isNextHighest)
                            {
                                curr.NextHighestNode++;
                            }
                            else
                            {
                                curr.NextLowestNode++;
                            }

                            i = curr.Value;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            public class CandyNode
            {
                public long NextHighestNode = 0;
                public long NextLowestNode = 0;
                public CandyNode Next = null;
                public CandyNode Previous = null;
                public int Value = 0;
            }

        }

        private static IEnumerable<object[]> Data()
        {
            Func<string, IEnumerable<CandiesTestData>> parser = (fileName) => TestDataParser.MultiCaseParse<CandiesTestData>((option) =>
            {
                option.CaseSeparator = "#";
                option.ParseByLine = true;
                option.FileName = fileName;
                option.Initialize = () => new CandiesTestData();
                option.ByLineParse = (data, line, lineNumber) =>
                {
                    var datas = line.Split(" ");
                    if (lineNumber == 0)
                    {
                        data.ChildrenCount = int.Parse(datas[0]);
                        data.ExpectedResult = long.Parse(datas[1]);
                        data.Input = new int[data.ChildrenCount];
                    }
                    else
                    {
                        var l = int.Parse(datas[0]);
                        data.Input[lineNumber - 1] = l;
                    }
                };
            });

            return new[] { @"Coding\Tasks\TestData\candies.txt" }
                .SelectMany(fileName => parser(fileName))
                .Select(data => new object[] { data.ChildrenCount, data.Input, data.ExpectedResult });
        }

        public class CandiesTestData : TestData<int[], long>
        {
            public int ChildrenCount { get; set; }
        }
    }
}
