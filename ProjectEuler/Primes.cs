using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
	class Primes
	{
		private static SortedSet<long> s_Primes = new SortedSet<long>();

		/// <summary>
		///		Returns a sorted list of all the prime factors of the given number.
		/// </summary>
		/// <param name="num">The number to find the prime factors of.</param>
		/// <returns>A sorted list of all the prime factors of the given number.</returns>
		static List<long> FindPrimeFactors(long num)
		{
			num = Math.Abs(num);

			var primeFactors = new List<long>();

			// 0 and 1 have no prime factors
			if (num < 2)
			{
				return primeFactors;
			}

			if (BasicMath.IsEven(num))
			{
				primeFactors.Add(2);
				Factors.ReduceToOddFactor(ref num);
			}
			
			for (int i = 3; i * i < num; )
			{
				if (Factors.IsFactor(i, num))
				{
					num /= i;
					primeFactors.Add(i);
				}
				else
				{
					i += 2;
				}
			}

			if (num > 1)
			{
				primeFactors.Add(num);
			}

			return primeFactors;
		}

		public static long LargestPrimeFactor(long num)
		{
			var primeFactors = FindPrimeFactors(num);
			return primeFactors[primeFactors.Count - 1];
		}

		public static bool IsPrime(long num)
		{
			// 1, 0, and negative numbers are not prime.
			if (num < 2)
			{
				return false;
			}

			// If we have it cached
			if (s_Primes.Contains(num))
			{
				return true;
			}

			// 2 is the first prime and also the only even prime, so it's a special case
			if (num == 2)
			{
				s_Primes.Add(num);
				return true;
			}

			// Even numbers, other than 2, are not prime.
			// This will let us only check for odd factors later.
			if (BasicMath.IsEven(num))
			{
				return false;
			}

			// Check for any odd factors less than the square root of the number.
			int sqrtFloor = (int)Math.Floor(Math.Sqrt((double)num));
			for (int i = 3; i <= sqrtFloor; i += 2)
			{
				if (Factors.IsFactor(i, num))
				{
					return false;
				}
			}

			s_Primes.Add(num);
			return true;
		}
	}
}
