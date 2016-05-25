using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ULinq
{
	public static class UnityLinqExtensions 
	{
		public static void Foreach<T>(this IList<T> items, System.Action<T> action)
		{
			for (int i = 0; i < items.Count; i++)
			{
				action(items[i]);
			}
		}

		public static IList<T> Where<T>(this IList<T> items, System.Func<T, bool> func)
		{
			List<T> results = new List<T>();
			for (int i = 0; i < items.Count; i++)
			{
				T item = items[i];
				if (func(item))
				{
					results.Add(item);
				}
			}
			return results;
		}

		public static T Aggregate<T>(this IList<T> items, T seed, System.Func<T, T, T> accumulator)
		{
			T value = seed;
			for (int i = 0; i < items.Count; i++)
			{
				value = accumulator(value, items[i]);
			}
			return value;
		}

		public static bool All<T>(this IList<T> items, System.Func<T, bool> func)
		{
			for (int i = 0; i < items.Count; i++)
			{
				if (!func(items[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static bool Any<T> (this IList<T> items, System.Func<T, bool> func)
		{
			for (int i = 0; i < items.Count; i++)
			{
				if (func(items[i]))
				{
					return true;
				}
			}
			return false;
		}

		public static IList<T> Distinct<T>(this IList<T> items)
		{
			List<T> result = new List<T>();

			for (int i = 0; i < items.Count; i++)
			{
				T item = items[i];
				if (! result.Contains(item))
				{
					result.Add (item);
				}
			}
			return result;
		}

		public static IList<T> Except<T>(this IList<T> items, IList<T> second)
		{
			List<T> results = new List<T>();
			HashSet<T> hashSet = second.ToHashSet();

			for (int i = 0; i < items.Count; i++)
			{
				T item = items[i];
				if (!hashSet.Contains(item))
				{
					results.Add(item);
				}
			}
			return results;
		}

		public static T FirstOrDefault<T>(this IList<T> items, System.Func<T, bool> func)
		{
			for (int i = 0; i < items.Count; i++)
			{
				if (func(items[i]))
				{
					return items[i];
				}
			}
			return default(T);
		}

		public static int FindIndex<T>(this IList<T> items, System.Func<T, bool> func)
		{
			for (int i = 0; i < items.Count; i++)
			{
				if (func(items[i]))
				{
					return i;
				}
			}
			return -1;
		}

		public static T Find<T>(this IList<T> items, System.Func<T, bool> func)
		{
			for (int i = 0; i < items.Count; i++)
			{
				if (func(items[i]))
				{
					return items[i];
				}
			}
			return default(T);
		}

		public static IList<Result> Select<T, Result>(this IList<T> items, System.Func<T, Result> func)
		{
			List<Result> results = new List<Result>(items.Count);

			for (int i = 0; i < items.Count; i++)
			{
				results.Add(func(items[i]));
			}
			return results;
		}

		public static IList<IGrouping<Key, T>> GroupBy<T, Key>(this IList<T> items, System.Func<T, Key> keySelector)
		{
			//Go through all items, Create a group for each unique key. 
			var groupings = new Dictionary<Key, IList<T>>();

			for (int i = 0; i < items.Count; i++)
			{
				T item = items[i]; 
				Key key = keySelector(item);
				IList<T> list;

				if (! groupings.TryGetValue(key, out list))
				{
					//Create a new group entry.
					list = new List<T>();
					groupings.Add(key, list);

				}
				//add the item to the existing group.
				list.Add (item);
			}
			var result = groupings.Select(x => new Grouping<Key, T>(x.Key, x.Value) as IGrouping<Key, T>).ToList ();
			return result;
		}

		public static HashSet<T> ToHashSet<T>(this IList<T> items)
		{
			HashSet<T> hashSet = new HashSet<T>();

			for (int i = 0; i < items.Count; i++)
			{
				hashSet.Add(items[i]);
			}
			return hashSet;
		}

		public static Dictionary<Key, T> ToDictionary<T, Key>(this IList<T> items, System.Func<T, Key> keySelector)
		{
			Dictionary<Key, T> dictionary = new Dictionary<Key, T>(items.Count);

			for (int i = 0; i < items.Count; i++)
			{
				T item = items[i];
				dictionary.Add (keySelector(item), item);
			}
			return dictionary;
		}
	}
}