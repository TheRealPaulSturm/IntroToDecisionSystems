using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DecisionSystems
{
    public static class enumerableExtansions
    {
        public static IEnumerable<TOut> Pairwise<TIn, TOut>(
           this IEnumerable<TIn> items,
           Func<TIn, TIn, TOut> Merge)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            return items.Zip(items.Skip(1), Merge);   //"verschieben" und wieder zusammenhänger der Liste
        }
    }
}
