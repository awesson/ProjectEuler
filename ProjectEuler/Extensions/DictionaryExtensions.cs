using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Geometry;

namespace ProjectEuler.Extensions
{
	public static class DictionaryExtensions
	{
		public static void AddOrIncrement<TKey>(this Dictionary<TKey, long> countMap,
														TKey toAdd)
		{
			countMap.AddOrIncrement<TKey>(toAdd, 1, 1);
		}

		public static void AddOrIncrement<TKey>(this Dictionary<TKey, long> countMap,
													  TKey toAdd,
													  long defaultCount)
		{
			countMap.AddOrIncrement<TKey>(toAdd, 1, defaultCount);
		}

		public static void AddOrIncrement<TKey>(this Dictionary<TKey, long> countMap,
													  TKey toAdd,
													  long incrementAmount,
													  long defaultCount)
		{
			if (countMap.ContainsKey(toAdd))
			{
				countMap[toAdd] += incrementAmount;
			}
			else
			{
				countMap.Add(toAdd, defaultCount);
			}
		}
	}
}
