using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using rvezy.Extensions;

namespace rvezy.Data.Extensions
{
    public static class EnumerableExtensions
    {
        public static T GetKeyOrDefault<T>(this IDictionary<string, T> source, string key, T value)
        {
            return source.TryGetValue(key, out var result) ? result : value;
        }

        public static T GetKeyOrDefault<T>(this IDictionary<string, T> source, Func<KeyValuePair<string, T>, bool> predicate, T value)
        {
            var entry = source.Where(predicate)
                .Select(e => (KeyValuePair<string, T>?) e)
                .FirstOrDefault();

            return entry.HasValue ? entry.Value.Value : value;
        }

        public static bool AreEquals<T>(this T list1, T list2) where T : IList
        {
            if (ReferenceEquals(list1, list2))
            {
                return true;
            }

            var inList1 = (from object x in list1 where !list2.Contains(x) select x).ToList();
            var inList2 = (from object x in list2 where !list1.Contains(x) select x).ToList();

            return !inList1.Any() && !inList2.Any();
        }

        public static void AddIfNotExists<T>(this IDictionary<string, T> source, string key, T value)
        {
            if (source != null && !source.ContainsKey(key))
            {
                source.Add(new KeyValuePair<string, T>(key, value));
            }
        }

        public static void AddOrUpdate<T>(this IDictionary<string, T> source, string key, T value)
        {
            if (source != null)
            {
                if (!source.ContainsKey(key))
                {
                    source.Add(new KeyValuePair<string, T>(key, value));
                }
                else
                {
                    source[key] = value;
                }
            }
        }

        public static string AsString<T>(this IEnumerable<T> source)
        {
            return !source.IsNullOrEmpty() ? string.Join(", ", source) : string.Empty;
        }

        public static string AsString(this IDictionary<string, string> source)
        {
            return !source.IsNullOrEmpty() ? string.Join(";", source.Select(x => x.Key + "=" + x.Value)) : string.Empty;
        }

        public static T MergeLeft<T, K, V>(this T me, params IDictionary<K, V>[] others) where T : IDictionary<K, V>, new()
        {
            if (others == null)
            {
                return me;
            }

            T newMap = new T();
            foreach (IDictionary<K, V> src in new List<IDictionary<K, V>> {me}.Concat(others))
            {
                foreach (KeyValuePair<K, V> p in src)
                {
                    newMap[p.Key] = p.Value;
                }
            }

            return newMap;
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static Dictionary<string, string> HideSensitive(this Dictionary<string, string> source)
        {
            if (source.IsNullOrEmpty())
            {
                return null;
            }

            var result = new Dictionary<string, string>();
            foreach (var kv in source)
            {
                var value = kv.Value;
                if (kv.Key.Contains("pass", StringComparison.InvariantCultureIgnoreCase) || 
                    kv.Key.Contains("key", StringComparison.InvariantCultureIgnoreCase))
                {
                    value = "******";
                }
                result.TryAdd(kv.Key, value);
            }

            return result;
        }
    }
}