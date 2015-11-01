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
		public static int SumUpToVMultM(int value, int mult)
		{
			var quotient = value / mult;
			var n = quotient * mult;
			return ((n + mult) * quotient) / 2;
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
