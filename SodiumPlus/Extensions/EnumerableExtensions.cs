using System.Linq;

namespace System.Collections.Generic
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Allows two sets to be enumerated in parallel. The second set cannot be larger than the first
        /// </summary>
        public static void EnumerateWithAdjacent<T1, T2>(this IEnumerable<T1> set1, IEnumerable<T2> set2, Action<T1, T2> action)
        {
            EnumerateWithAdjacent(set1, set2, (x, y, z) => action(x, y));
        }

        /// <summary>
        /// Allows two sets to be enumerated in parallel. The second set cannot be larger than the first
        /// </summary>
        public static void EnumerateWithAdjacent<T1, T2>(this IEnumerable<T1> set1, IEnumerable<T2> set2, Action<T1, T2, int> action)
        {
            EnumerateWithAdjacentImpl(set1, set2, action);
        }

        /// <summary>
        /// Allows a "Select" transformation to be applied to each adjacent element in two sets. The second set cannot be larger than the first
        /// </summary>
        public static IEnumerable<TResult> SelectWithAdjacent<T1, T2, TResult>(this IEnumerable<T1> set1, IEnumerable<T2> set2, Func<T1, T2, TResult> func)
        {
            return SelectWithAdjacent(set1, set2, (x, y, z) => func(x, y));
        }

        /// <summary>
        /// Allows a "Select" transformation to be applied to each adjacent element in two sets. The second set cannot be larger than the first
        /// </summary>
        public static IEnumerable<TResult> SelectWithAdjacent<T1, T2, TResult>(this IEnumerable<T1> set1, IEnumerable<T2> set2, Func<T1, T2, int, TResult> func)
        {
            return SelectWithAdjacentImpl(set1, set2, func);
        }

        /// <summary>
        /// Syntactic sugar around a foreach loop
        /// </summary>
        public static void Enumerate<T>(this IEnumerable<T> t, Action<T> action)
        {
            Enumerate(t, (x, i) => action(x));
        }

        /// <summary>
        /// Syntactic sugar around a foreach loop
        /// </summary>
        public static void Enumerate<T>(this IEnumerable<T> t, Action<T, int> action)
        {
            EnumerateEachImpl(t, action);
        }

        /// <summary>
        /// Run some Action for the cartesian product of two sets
        /// </summary>
        public static void EnumerateCartesian<T1, T2>(this IEnumerable<T1> set1, IEnumerable<T2> set2, Action<T1, T2> action)
        {
            EnumerateCartesian(set1, set2, (t1, t2, x, y) => action(t1, t2));
        }

        /// <summary>
        /// Run some Action for the cartesian product of two sets
        /// </summary>
        public static void EnumerateCartesian<T1, T2>(this IEnumerable<T1> set1, IEnumerable<T2> set2, Action<T1, T2, int, int> action)
        {
            EnumerateCartesianImpl(set1, set2, action);
        }

        /// <summary>
        /// Select the cartesian product of two sets
        /// </summary>
        public static IEnumerable<object> Cartesian<T1, T2, TResult>(IEnumerable<T1> set1, IEnumerable<T2> set2, Func<T1, T2, TResult> func)
        {
            return Cartesian(set1, set2, (t1, t2, x, y) => func);
        }

        /// <summary>
        /// Select the cartesian product of two sets
        /// </summary>
        public static IEnumerable<object> Cartesian<T1, T2, TResult>(IEnumerable<T1> set1, IEnumerable<T2> set2, Func<T1, T2, int, int, TResult> func)
        {
            return CartesianImpl(set1, set2, func);
        }

        private static IEnumerable<object> CartesianImpl<T1, T2, TResult>(IEnumerable<T1> set1, IEnumerable<T2> set2, Func<T1, T2, int, int, TResult> func)
        {
            var set1Enumerator = set1.GetEnumerator();
            var set2Enumerator = set2.GetEnumerator();
            var x = 0;
            while (set1Enumerator.MoveNext())
            {
                var y = 0;
                while (set2Enumerator.MoveNext())
                {
                    yield return func(set1Enumerator.Current, set2Enumerator.Current, x, y++);
                }
                set2Enumerator.Reset();
                x++;
            }
        }

        private static void EnumerateCartesianImpl<T1, T2>(IEnumerable<T1> set1, IEnumerable<T2> set2, Action<T1, T2, int, int> action)
        {
            var set1Enumerator = set1.GetEnumerator();
            var set2Enumerator = set2.GetEnumerator();
            var x = 0;
            while (set1Enumerator.MoveNext())
            {
                var y = 0;
                while (set2Enumerator.MoveNext())
                {
                    action(set1Enumerator.Current, set2Enumerator.Current, x, y++);
                }
                try
                {
                    set2Enumerator.Reset();
                }
                catch (NotImplementedException)
                {
                }
                x++;
            }
        }

        private static void EnumerateWithAdjacentImpl<T1, T2>(IEnumerable<T1> set1, IEnumerable<T2> set2, Action<T1, T2, int> action)
        {
            var set1Enumerator = set1.GetEnumerator();
            var set2Enumerator = set2.GetEnumerator();
            var index = 0;
            while (set1Enumerator.MoveNext() & set2Enumerator.MoveNext())
            {
                action(set1Enumerator.Current, set2Enumerator.Current, index++);
            }
        }

        private static IEnumerable<TResult> SelectWithAdjacentImpl<T1, T2, TResult>(IEnumerable<T1> set1, IEnumerable<T2> set2, Func<T1, T2, int, TResult> func)
        {
            var set1Enumerator = set1.GetEnumerator();
            var set2Enumerator = set2.GetEnumerator();
            var index = 0;
            while (set1Enumerator.MoveNext() & set2Enumerator.MoveNext())
            {
                yield return func(set1Enumerator.Current, set2Enumerator.Current, index++);
            }
        }

        private static void EnumerateEachImpl<T>(this IEnumerable<T> t, Action<T, int> action)
        {
            var set1Enumerator = t.GetEnumerator();
            var index = 0;
            while (set1Enumerator.MoveNext())
            {
                action(set1Enumerator.Current, index++);
            }
        }

        public static double Sum<T1>(this IEnumerable<T1> set1, Func<T1, int, double> func)
        {
            var total = 0d;
            set1.Enumerate((item, index) =>
            {
                total += func(item, index);
            });
            return total;
        }

        public static double Product<T1>(this IEnumerable<T1> set1, Func<T1, double> func)
        {
            return Product(set1, (item, i) => func(item));
        }

        public static double Product<T1>(this IEnumerable<T1> set1, Func<T1, int, double> func)
        {
            var total = 0d;
            set1.Enumerate((item, index) =>
            {
                total *= func(item, index);
            });
            return total;
        }

        public static ICollection<T> ToCollection<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.ToList();
        }
    }
}
