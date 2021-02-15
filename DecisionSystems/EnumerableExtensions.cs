using System;
using System.Collections.Generic;
using System.Linq;

namespace DecisionSystems
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TOut> Pairwise<TIn, TOut>(this IEnumerable<TIn> items, Func<TIn, TIn, TOut> merge)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }
            return items.Zip(items.Skip(1), merge);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> numbers)
        {
            //var generator = new Random();
            //return numbers.OrderBy(_ => generator.Next());

            var generator = new Random();
            var data = numbers.ToArray();
            //ToDo Fisher-Yates-Shuffle
            return data;
        }

        public static TItem BestBy<TItem, TValue>(this IEnumerable<TItem> items, Func<TItem, TValue> selector, Func<TValue, TValue, bool> isFistBetter) //Rückgabewert von Func = bool
        {
            TValue bestValue = default;
            TItem bestItem = default;
            var hasBestItem = false;

            foreach (var item in items)
            {
                var value = selector(item);
                if (!hasBestItem || isFistBetter(value, bestValue))
                {
                    bestValue = value;
                    bestItem = item;
                }
                hasBestItem = true;
            }

            if (hasBestItem) return bestItem;
            throw new ArgumentException("Can not calculate the minimum item from an empty list.");
        }

        public static void Swap<T>(this IList<T> items, int index1, int index2)
        {
            //T item1 = items[index1];
            //items[index1] = items[index2];
            //items[index2] = item1;

            //oder so:

            (items[index1], items[index2]) = (items[index2], items[index1]);
        }
    }
}
