using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class FindItineraryTest
    {
        [TestMethod]
        public void Test()
        {
            var input = new List<IList<string>>
            {
                new List<string> {"JFK", "KUL"},
                new List<string> {"JFK", "NRT"},
                new List<string> {"NRT", "JFK"}
            };

            var expected = new[] {"JFK", "NRT", "JFK", "KUL"};
            var result = FindItinerary(input);

            CollectionAssert.AreEqual(expected, result.ToArray());
        }

        //https://leetcode.com/problems/reconstruct-itinerary/
        private static IList<string> FindItinerary(IList<IList<string>> tickets)
        {
            const string origin = "JFK";

            var solution = new List<string>();
            var set = new SortedDictionary<string, List<string>>();
            for (int i = 0; i < tickets.Count; i++)
            {
                string from = tickets[i][0];
                string to = tickets[i][1];
                if (set.TryGetValue(from, out var destinations))
                {
                    destinations.Add(to);
                }
                else
                {
                    destinations = new List<string>();
                    destinations.Add(to);
                    set.Add(from, destinations);
                }
            }

            foreach (var value in set.Values)
            {
                value.Sort((x, y) => y.CompareTo(x));
            }

            solution.Add(origin);
            FindItinerary(origin, set, solution);
            return solution;
        }

        private static bool FindItinerary(
            string to,
            SortedDictionary<string, List<string>> set,
            List<string> solution)
        {
            if (set.Count == 0)
                return true;

            if (!set.TryGetValue(to, out List<string> directions))
                return false;

            for (int i = directions.Count - 1; i >= 0; i--)
            {
                var direction = directions[i];
                directions.Remove(direction);

                if (directions.Count == 0)
                    set.Remove(to);
                solution.Add(direction);

                if (FindItinerary(direction, set, solution))
                    return true;

                solution.RemoveAt(solution.Count - 1);
                directions.Insert(i, direction);
                if (!set.ContainsKey(to))
                    set[to] = directions;
            }

            return false;
        }
    }
}
