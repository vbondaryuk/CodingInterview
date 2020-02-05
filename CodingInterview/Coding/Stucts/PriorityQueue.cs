using System.Collections.Generic;
using System.Linq;

namespace CodingInterview.Coding.Stucts
{
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

        public (TKey, TKeyCost) DequeueMax()
        {
            var key = _dict.Keys.Last();
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
