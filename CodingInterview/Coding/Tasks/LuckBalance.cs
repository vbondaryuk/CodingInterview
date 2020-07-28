using System;
using CodingInterview.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class LuckBalance
    {
        [TestMethod]
        [DataRow(@"Coding\Tasks\TestData\luck-balance.txt")]
        [DataRow(@"Coding\Tasks\TestData\luck-balance_2.txt")]
        [DataRow(@"Coding\Tasks\TestData\luck-balance_3.txt")]
        public void TestFromFile(string fileName)
        {
            var luckBalanceData = TestDataParser.Parse<LuckBalanceData>((option) =>
            {
                option.ParseByLine = true;
                option.FileName = fileName;
                option.Initialize = () => new LuckBalanceData();
                option.ByLineParse = (data, line, lineNumber) =>
                {
                    var datas = line.Split(" ");
                    if (lineNumber == 0)
                    {
                        var pairs = int.Parse(datas[0]);
                        data.LuckBalance = int.Parse(datas[1]);
                        data.ExpectedResult = int.Parse(datas[2]);
                        data.Input = new int[pairs][];
                    }
                    else
                    {
                        var l = int.Parse(datas[0]);
                        var t = int.Parse(datas[1]);
                        data.Input[lineNumber - 1] = new[] { l, t };
                    }
                };
            });

            var result = luckBalance(luckBalanceData.LuckBalance, luckBalanceData.Input);

            Assert.AreEqual(luckBalanceData.ExpectedResult, result);
        }

        //https://www.hackerrank.com/challenges/luck-balance/
        static int luckBalance(int k, int[][] contests)
        {
            Array.Sort(contests, (r, r1) =>
            {
                if (r[1] == r1[1])
                {
                    return r[0].CompareTo(r1[0]);
                }

                if (r[1] == 1)
                {
                    return -1;
                }

                if (r1[1] == 1)
                {
                    return 1;
                }

                return r[0].CompareTo(r1[0]);
            });

            int importantContests = 0;
            for (int i = 0; i < contests.Length; i++)
            {
                if (contests[i][1] == 1)
                    importantContests++;
            }

            k = k > importantContests ? 0: importantContests - k;
            int sum = 0;
            for (int i = 0; i < contests.Length; i++)
            {
                if (k > 0)
                {
                    sum -= contests[i][0];
                    k--;
                }
                else
                {
                    sum += contests[i][0];
                }
            }

            return sum;
        }

        public class LuckBalanceData : TestData<int[][], int>
        {
            public int LuckBalance { get; set; }
        }
    }
}
