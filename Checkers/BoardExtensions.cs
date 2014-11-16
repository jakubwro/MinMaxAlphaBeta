using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public static class BoardExtensions
    {
        public static T Second<T>(this IEnumerable<T> sequence)
        {
            return sequence.ElementAt(1);
        }

        public static T SecondOrDefault<T>(this IEnumerable<T> sequence)
        {
            return sequence.Skip(1).FirstOrDefault();
        }

        public static T Third<T>(this IEnumerable<T> sequence)
        {
            return sequence.ElementAt(2);
        }

        public static T ThirdOrDefault<T>(this IEnumerable<T> sequence)
        {
            return sequence.Skip(2).FirstOrDefault();
        }

        public static T Random<T>(this IEnumerable<T> sequence)
        {
            int count = sequence.Count();
            int index = new Random(DateTime.Now.Millisecond).Next(0, count - 1);
            return sequence.ElementAt(index);
        }

        //public static IImmutableSet<Square> OfColor(this IImmutableDictionary<Square, Checker> layout, ColorEnum color)
        //{
        //    return ImmutableSortedSet.CreateRange<Square>(layout.Where(kv => kv.Value.Color == color).Select(kv => kv.Key));
        //}

        public static Checker GetChecker(this IMove move)
        {
            var square = move.LayoutAfter.Keys.Except(move.LayoutBefore.Keys).Single();

            return move.LayoutAfter[square];
        }
    }
}
