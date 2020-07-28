using System.Collections.Generic;
using CodingInterview.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class JourneyToMoon
    {
        [TestMethod]
        public void Test()
        {
            var astronaut = new int[7][];
            astronaut[0] = new[] { 0, 2 };
            astronaut[1] = new[] { 1, 8 };
            astronaut[2] = new[] { 1, 4 };
            astronaut[3] = new[] { 2, 8 };
            astronaut[4] = new[] { 2, 6 };
            astronaut[5] = new[] { 3, 5 };
            astronaut[6] = new[] { 6, 9 };
            var result = journeyToMoon(10, astronaut);

            astronaut = new int[2][];
            astronaut[0] = new[] { 1, 2 };
            astronaut[1] = new[] { 3, 4 };

            result = journeyToMoon(100000, astronaut);
            Assert.AreEqual(4999949998, result);
        }

        [TestMethod]
        [DataRow(@"Coding\Tasks\TestData\journey-to-the-moon.txt")]
        [DataRow(@"Coding\Tasks\TestData\journey-to-the-moon_2.txt")]
        public void TestFromFile(string fileName)
        {
            var data = TestDataParser.Parse<AstoInfo>((option)=>
            {
                option.ParseByLine = true;
                option.FileName = fileName;
                option.Initialize = () => new AstoInfo();
                option.ByLineParse = (astoInfo, line, lineNumber) =>
                {
                    var datas = line.Split(" ");
                    if (lineNumber == 0)
                    {
                        var pairs = int.Parse(datas[1]);
                        astoInfo.AstroCount = int.Parse(datas[0]);
                        astoInfo.ExpectedResult = int.Parse(datas[2]);
                        astoInfo.Input = new int[pairs][];
                    }
                    else
                    {
                        var astro1 = int.Parse(datas[0]);
                        var astro2 = int.Parse(datas[1]);
                        astoInfo.Input[lineNumber - 1] = new[] {astro1, astro2};
                    }
                };
            });

            var result = journeyToMoon(data.AstroCount, data.Input);

            Assert.AreEqual(data.ExpectedResult, result);
        }

        //https://www.hackerrank.com/challenges/journey-to-the-moon/problem
        static int journeyToMoon(int n, int[][] astronaut)
        {
            Dictionary<int, HashSet<int>> countryToAstroMap = AstroByCountries(n, astronaut);

            int result = 0;
            int astroNumber = n;
            foreach (var countrySet in countryToAstroMap)
            {
                var astroInCountry = countrySet.Value.Count;
                astroNumber = astroNumber - astroInCountry;
                result += astroNumber * astroInCountry;
            }
            return result;
        }

        private static Dictionary<int, HashSet<int>> AstroByCountries(int astroCount, int[][] astronaut)
        {
            Dictionary<int, HashSet<int>> countryToAstroMap = new Dictionary<int, HashSet<int>>();
            Dictionary<int, int> astroToCountry = new Dictionary<int, int>();

            for (int i = 0; i < astroCount; i++)
            {
                astroToCountry[i] = -1;
            }

            int currentCountry = 0;
            for (int i = 0; i < astronaut.Length; i++)
            {
                int astronaut1 = astronaut[i][0];
                int astronaut2 = astronaut[i][1];

                int country1 = astroToCountry[astronaut1];
                int country2 = astroToCountry[astronaut2];

                if (country1 == -1 && country2 == -1)
                {
                    countryToAstroMap[currentCountry] = new HashSet<int> {astronaut1, astronaut2};
                    astroToCountry[astronaut1] = currentCountry;
                    astroToCountry[astronaut2] = currentCountry;
                }
                else if (country1 == -1)
                {
                    countryToAstroMap[country2].Add(astronaut1);
                    astroToCountry[astronaut1] = country2;
                }
                else if(country2 == -1)
                {
                    countryToAstroMap[country1].Add(astronaut2);
                    astroToCountry[astronaut2] = country1;
                }
                else
                {
                    if (country2 == country1)
                        continue;

                    foreach (var astro in countryToAstroMap[country2])
                    {
                        astroToCountry[astro] = country1;
                    }
                    countryToAstroMap[country1].UnionWith(countryToAstroMap[country2]);
                    countryToAstroMap.Remove(country2);
                }

                currentCountry++;
            }

            foreach (var astroSet in astroToCountry)
            {
                if (astroSet.Value == -1)
                {

                    countryToAstroMap[currentCountry++] = new HashSet<int>(new[] { 1 });
                }
            }

            return countryToAstroMap;
        }

        private class AstoInfo : TestData<int[][], int>
        {
            public int AstroCount { get; set; }
        }
    }
}
