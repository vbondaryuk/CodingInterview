using System;
using System.Collections.Generic;
using System.Linq;
using CodingInterview.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Graph
{
    [TestClass]
    public class BridgeTest
    {
        [TestMethod]
        public void Test()
        {
            List<IList<int>> expected = new List<IList<int>>{ new List<int>{1,2}, new List<int>{ 4, 5 } };
            int[,] grid = { { 1, 2 }, { 1, 3 }, { 3, 4 }, { 1, 4 }, { 4, 5 } };
            //int[,] grid = { { 1, 2 }, { 1, 3 }, { 2, 3 }, { 2, 4 }, { 2, 5 }, { 4, 6 }, { 5, 6 } };

            var adjacencyList = grid.CreateHashedAdjacencyList();
            var bridges = new Bridge().Get(adjacencyList, 5);

            for (int i = 0; i < expected.Count; i++)
            {
                CollectionAssert.AreEqual(expected[i].ToArray(), bridges[i].ToArray());
            }
            
        }
    }

    //https://leetcode.com/discuss/interview-question/372581
    public class Bridge
    {
        public IList<IList<int>> Get(Dictionary<int, HashSet<int>> adjacencyList, int numNodes)
        {
            HashSet<int> visited = new HashSet<int>();
            Dictionary<int, int> disc = new Dictionary<int, int>();
            Dictionary<int, int> low = new Dictionary<int, int>();
            IList<IList<int>> bridges = new List<IList<int>>();

            foreach (var adjacencyListKey in adjacencyList.Keys)
            {
                disc[adjacencyListKey] = -1;
                low[adjacencyListKey] = -1;
            }

            int time = 0;
            foreach (var adjacencyListKey in adjacencyList.Keys)
            {
                if (!visited.Contains(adjacencyListKey))
                    FillBridges(adjacencyListKey, visited, disc, low, -1, adjacencyList, bridges, ref time);
            }

            return bridges;
        }

        /// <param name="disc">Entry time of vertices</param>
        /// <param name="low">Minimum time</param>
        private void FillBridges(
            int v,
            HashSet<int> visited,
            Dictionary<int, int> disc,
            Dictionary<int, int> low,
            int parent,
            Dictionary<int, HashSet<int>> adjacencyList,
            IList<IList<int>> bridges,
            ref int time)
        {
            visited.Add(v);
            disc[v] = low[v] = ++time;

            foreach (var vTo in adjacencyList[v])
            {
                if (vTo == parent)
                    continue;

                if (visited.Contains(vTo))
                {
                    low[v] = Math.Min(low[v], disc[vTo]);
                }
                else
                {
                    FillBridges(vTo, visited, disc, low, v, adjacencyList, bridges, ref time);
                    low[v] = Math.Min(low[vTo], low[v]);

                    if (disc[v] < low[vTo])
                        bridges.Add(new int[] { v, vTo });
                }
            }
        }
    }
}