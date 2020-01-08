using System;
using System.Collections.Generic;

namespace CodingInterview.Coding.Sort
{
    public class OrderSettings<TEntity, TValue>
    {
        public OrderSettings(OrderDirection direction, Func<TEntity, TValue> extractValue)
            : this(direction, extractValue, Comparer<TValue>.Default)
        {
        }

        public OrderSettings(Func<TEntity, TValue> func)
            : this(OrderDirection.Asc, func, Comparer<TValue>.Default)
        {
        }

        public OrderSettings(OrderDirection direction, Func<TEntity, TValue> extractValue, IComparer<TValue> comparer)
        {
            Direction = direction;
            ExtractValue = extractValue;
            Comparer = comparer;
        }

        public IComparer<TValue> Comparer { get; }
        public OrderDirection Direction { get; }
        public Func<TEntity, TValue> ExtractValue { get; }

        public int Compare(TEntity first, TEntity second)
        {
            return Comparer.Compare(ExtractValue(first), ExtractValue(second));
        }
    }


    public enum OrderDirection
    {
        Asc,
        Desc
    }
}