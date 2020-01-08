using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CodingInterview.Utils
{
    public static class Extensions
    {
        public static void Swap<T>(this IList<T> list, int from, int to)
        {
            var temp = list[to];
            list[to] = list[from];
            list[from] = temp;
        }

        public static Dictionary<T, HashSet<T>> CreateHashedAdjacencyList<T>(this T[,] grid)
        {
            var adjacencyList = new Dictionary<T, HashSet<T>>();
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                if (!adjacencyList.TryGetValue(grid[i, 0], out var set))
                {
                    set = new HashSet<T>();
                    adjacencyList[grid[i, 0]] = set;
                }

                for (int j = 1; j < grid.GetLength(1); j++)
                {
                    set.Add(grid[i, j]);

                    if (!adjacencyList.TryGetValue(grid[i, j], out var connectedSet))
                    {
                        connectedSet = new HashSet<T>();
                        adjacencyList[grid[i, j]] = connectedSet;
                    }

                    connectedSet.Add(grid[i, 0]);
                }
            }

            return adjacencyList;
        }

        public static IList<(int, IList<int>)> CreateAdjacencyList(this int[,] grid, int skip = -1)
        {
            var adjacencyList = new List<(int, IList<int>)>();
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                var list = new List<int>();
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] != skip)
                        list.Add(j);
                }

                adjacencyList.Add((i, list));
            }

            return adjacencyList;
        }

        public static T[] SplitAndCast<T>(this string str, char delimetr = ',', char[] trim = null)
        {
            if (trim == null)
            {
                trim = new[] { '[', ']' };
            }

            var arr = str.Trim(trim).Split(delimetr);
            var type = typeof(T);
            var isNullable = Nullable.GetUnderlyingType(type) != null;
            return arr.Select(x =>
            {
                T value;
                try
                {
                    Type t = Nullable.GetUnderlyingType(type) ?? type;
                    value = (T)(x == null ? null : Convert.ChangeType(x, t));
                }
                catch (Exception e)
                {
                    if (isNullable)
                        value = default(T);
                    else 
                        throw;
                }

                return value;
            }).ToArray();
        }
    }
}