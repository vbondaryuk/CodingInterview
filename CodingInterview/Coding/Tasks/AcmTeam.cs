using System;
using System.Linq;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class AcmTeamTest
    {
        [TestMethod]
        public void Test()
        {
        }

        //https://www.hackerrank.com/challenges/acm-icpc-team/problem
        static int[] acmTeam(string[] topics)
        {
            int maxCount = 0;
            int teamCount = 0;
            var bigIntegers = new BigInteger[topics.Length];
            int i = 0;
            foreach (var topic in topics)
            {
                var bytes = new Byte[topic.Length];
                int j = 0;
                foreach (char c in topic)
                {
                    bytes[j++] = (byte)(c == '0' ? 0 : 1);
                }

                bigIntegers[i++] = new BigInteger(bytes);
            }

            for (i = 0; i < topics.Length; i++)
            {
                for (int j = i + 1; j < topics.Length; j++)
                {
                    BigInteger bigInteger = bigIntegers[i] | bigIntegers[j];
                    var count = bigInteger.ToByteArray().Where(x => x == 1).Sum(x => x);
                    if (maxCount < count)
                    {
                        maxCount = count;
                        teamCount = 1;
                        continue;
                    }

                    if (maxCount == count)
                    {
                        teamCount++;
                    }
                }
            }

            return new[] { maxCount, teamCount };
        }
    }
}
