using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
	public class Series
	{
		/// <summary>
		/// Calculates the sum of all multiples of <paramref name="mult"/>
		/// less than or equal to <paramref name="value"/>
		/// </summary>
		public static ulong SumUpToVMultM(ulong value, ulong mult)
		{
			// We know 1 + 2 + 3 + ... + N = (1 + N)N/2
			// If instead of 1s we want to go by multiples of mult,
			// mult + 2mult + 3mult + ... + Nmult = mult(1 + N)N/2
			// where N in this case is the number of times mult goes into value.
			var N = value / mult;
			return (mult*(N + 1uL)*N)/2uL;
		}

		/// <summary>
		/// Returns the largest product of N consecutive digits in the given number, represented as a string.
		/// </summary>
		/// <param name="num">The string formatted number containing the digits to search.</param>
		/// <returns>The largest product.</returns>
		public static long LargestNProduct(int N, string num)
		{
			Func<char, int, long> charToLong = (c, _) => (long)Char.GetNumericValue(c);
			var arrNum = num.Select<char, long>(charToLong);
			return LargestNProduct(N, arrNum);
		}

		/// <summary>
		/// Returns the largest product of N consecutive digits in the given enumerable.
		/// </summary>
		/// <param name="num">The string formatted number containing the digits to search.</param>
		/// <returns>The largest product.</returns>
		public static long LargestNProduct(int N, IEnumerable<long> digits)
		{
			long maxProd = -1;
			for (int i = 0; i < digits.Count(); ++i)
			{
				var prod = Product(digits.Skip(i).Take(N));
				if (prod > maxProd)
				{
					maxProd = prod;
				}
			}

			return maxProd;
		}

		public static long Product(IEnumerable<long> nums)
		{
			return nums.Aggregate((prod, num) => prod * num);
		}
	}
}
