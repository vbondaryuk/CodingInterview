using System;
using System.Collections.Generic;
using System.Linq;
using CodingInterview.Coding.Stucts.Nodes;
using CodingInterview.Coding.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Graph
{
    [TestClass]
    public class DijkstrasTest
    {
        [TestMethod]
        public void Test_Adjacency_matrix()
        {
            //      0
            //   1 / \ 20
            //    1 - 2
            //      3
            int[,] graph =
            {
                {0, 1, 20},
                {1, 0, 3},
                {20, 3, 0}
            };
            var expected = new int[] { 0, 1, 4 };
            var dijkstras = new Dijkstras();
            var shortestPaths = dijkstras.FindMinPath(graph, 0);

            CollectionAssert.AreEqual(expected, shortestPaths);
        }

        [TestMethod]
        public void Test_Adjacency_list()
        {
            //      0
            //   1 / \ 20
            //    1 - 2
            //      3
            List<IList<WeightedNode>> adjacencyList = new List<IList<WeightedNode>>
            {
                new List<WeightedNode> {new WeightedNode(1, 1), new WeightedNode(2, 20)},
                new List<WeightedNode> {new WeightedNode(0, 1), new WeightedNode(2, 3)},
                new List<WeightedNode> {new WeightedNode(0, 20), new WeightedNode(1, 3)}
            };
            //https://www.geeksforgeeks.org/dijkstras-algorithm-for-adjacency-list-representation-greedy-algo-8/
            //List<IList<WeightedNode>> adjacencyList = new List<IList<WeightedNode>>
            //{
            //    new List<WeightedNode> {new WeightedNode(1, 4), new WeightedNode(7, 8)},
            //    new List<WeightedNode> {new WeightedNode(0, 4), new WeightedNode(7, 11), new WeightedNode(2, 8)},
            //    new List<WeightedNode> {new WeightedNode(1, 8), new WeightedNode(8, 2), new WeightedNode(5, 4), new WeightedNode(3, 7)},
            //    new List<WeightedNode> {new WeightedNode(2,7), new WeightedNode(5, 14), new WeightedNode(4, 9)},
            //    new List<WeightedNode> {new WeightedNode(3,9), new WeightedNode(5,10)},
            //    new List<WeightedNode> {new WeightedNode(4,10), new WeightedNode(3,14), new WeightedNode(2,4), new WeightedNode(6,2)},
            //    new List<WeightedNode> {new WeightedNode(5,2), new WeightedNode(8,6), new WeightedNode(7,1)},
            //    new List<WeightedNode> {new WeightedNode(6,1), new WeightedNode(8,7), new WeightedNode(1,11), new WeightedNode(0,8)},
            //    new List<WeightedNode> {new WeightedNode(7,7), new WeightedNode(6,6), new WeightedNode(2,2)}
            //};
            var expected = new int[] { 0, 1, 4 };
            var dijkstras = new Dijkstras();
            var shortestPaths = dijkstras.FindMinPath(adjacencyList, 0);

            List<int> way = new List<int>();
            for (var at = 2; at != 0; at = shortestPaths.path[at])
                way.Add(shortestPaths.path[at]);
            way.Reverse();

            CollectionAssert.AreEqual(expected, shortestPaths.dist);
            CollectionAssert.AreEqual(new[] { 0, 1 }, way);
        }
    }

    public class Dijkstras
    {
        #region adjacency matrix
        public int[] FindMinPath(int[,] graph, int startPoint)
        {
            var vertexes = graph.GetLength(0);
            //keep the shortest distance from start to i
            int[] distance = new int[vertexes];
            bool[] visited = new bool[vertexes];
            int[] path = new int[vertexes];

            for (int i = 0; i < vertexes; i++)
            {
                distance[i] = int.MaxValue;
            }

            distance[startPoint] = 0;

            for (int i = 0; i < vertexes - 1; i++)
            {
                //get the minimum index and set us visited
                var minIndex = FindIndexOfMinDistance(vertexes, visited, distance);
                visited[minIndex] = true;

                //look through the line(minimum index) of graph and update the distance if other one has the lowest value
                for (int j = 0; j < vertexes; j++)
                {
                    var distanceTo = graph[minIndex, j];
                    if (!visited[j]
                        && distance[minIndex] != int.MaxValue
                        && distanceTo != 0
                        && distanceTo + distance[minIndex] < distance[j])
                    {
                        distance[j] = distanceTo + distance[minIndex];
                        path[j] = minIndex;
                    }
                }
            }

            return distance;
        }

        /// <summary>
        /// Find the minimum unvisited value in distances array and return index of it
        /// </summary>
        public int FindIndexOfMinDistance(int vertexes, bool[] visited, int[] distance)
        {
            int minValue = int.MaxValue;
            int index = 0;

            for (int i = 0; i < vertexes; i++)
            {
                if (!visited[i] && distance[i] < minValue)
                {
                    index = i;
                    minValue = distance[i];
                }
            }

            return index;
        }
        #endregion

        #region adjacency list

        public (int[] dist, int[] path) FindMinPath<T>(IList<IList<T>> adjacencyList, int startPoint)
            where T : WeightedNode
        {
            var vertexes = adjacencyList.Count;
            bool[] visited = new bool[vertexes];
            int[] path = new int[vertexes];
            int[] dist = Enumerable.Range(0, vertexes).Select(x => int.MaxValue).ToArray();
            dist[startPoint] = 0;

            //instead of priority queue
            PriorityQueue<int, int> priorityQueue = new PriorityQueue<int, int>();
            priorityQueue.Enqueue(startPoint, 0);
            while (priorityQueue.Count() > 0)
            {
                var (index, distance) = priorityQueue.Dequeue();
                visited[index] = true;
                if (dist[index] < distance)
                    continue;

                foreach (var weightedNode in adjacencyList[index])
                {
                    if (visited[weightedNode.To])
                        continue;

                    var newDistance = dist[index] + weightedNode.Cost;
                    if (dist[weightedNode.To] > newDistance)
                    {
                        dist[weightedNode.To] = newDistance;
                        path[weightedNode.To] = index;
                        priorityQueue.Enqueue(weightedNode.To, newDistance);
                    }
                }
            }

            return (dist, path);

        }
        #endregion
    }

    //https://gist.github.com/irsal/5589011
    public class PriorityQueue<TKey, TKeyCost>
    {
        private readonly SortedDictionary<TKeyCost, Queue<TKey>> _dict = new SortedDictionary<TKeyCost, Queue<TKey>>();

        public int Count() => _dict.Count;

        public (TKey, TKeyCost) Dequeue()
        {
            var key = _dict.Keys.First();
            var keysQueue = _dict[key];
            if (keysQueue.Count == 1)
            {
                _dict.Remove(key);
            }

            return (keysQueue.Dequeue(), key);
        }

        public void Enqueue(TKey key, TKeyCost cost)
        {
            if (!_dict.TryGetValue(cost, out var keyQueue))
            {
                keyQueue = new Queue<TKey>();
                _dict.Add(cost, keyQueue);
            }
            keyQueue.Enqueue(key);
        }
    }
}
