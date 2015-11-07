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

		public static long SumModN(IEnumerable<long> nums, long mod)
		{
			return nums.Aggregate((sum, num) => (sum + (num % mod)) % mod);
		}

		/// <summary>
		///		Returns the numbers in a Collatz Sequence, where the next number in the sequence is
		///		n/2 if it is currently even, or 3n + 1 if it is currently odd.
		/// </summary>
		/// <param name="num">The starting number in the sequence.</param>
		public static IEnumerable<long> CollatzSequence(long num)
		{
			if(num <= 0)
			{
				throw new ArgumentException("num must be greater than 0");
			}

			while (num != 1)
			{
				yield return num;

				if (BasicMath.IsEven(num))
				{
					num >>= 1;
				}
				else
				{
					num = 3*num + 1;
				}
			}

			yield return num;
		}

		/// <summary>
		///		Finds and returns the longest Collatz Sequence that starts with
		///		any number less than the given number.
		/// </summary>
		public static long LongestCollatzSequence(long maxStartNum)
		{
			var longestLen = -1;
			long longestStartingNum = 0L;
			// Since Len(seq(n)) = Len(seq(2n)) - 1, we can double every starting number less than half of the max
			// and get a new starting number that would have a longer sequence. If after doubling,
			// it is still less than half, it can be doubled again and the new number would have a
			// longer sequence and is guaranteed to be less than maxStartingNum (since it was less than half).
			// Therefore all numbers less than half of the maxStartNum necessarily have shorter sequences than
			// all even numbers greater than half maxStartNum.
			for (long i = maxStartNum/2; i < maxStartNum; ++i)
			{
				var len = CollatzSequence(i).Count();
				if (len > longestLen)
				{
					longestLen = len;
					longestStartingNum = i;
				}
			}

			return longestStartingNum;
		}
	}
}
