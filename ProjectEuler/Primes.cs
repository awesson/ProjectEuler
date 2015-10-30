using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
	class Primes
	{
		private static HashSet<long> s_Primes = new HashSet<long>();
		private static List<long> s_SortedPrimes = new List<long>();
		private static long s_LargestCheckedPrime = 3;

		static Primes()
		{
			// 2 is a special case since it is the only even prime.
			CacheAsPrime(2);
			// and why not...
			CacheAsPrime(3);
		}

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
			
			// To weed out unnecessary checks (eg. checking if 12 is a factor after already checking 2 and 3),
			// only check for primes up until we run out of known primes.
			int sqrtFloor = (int)Math.Floor(Math.Sqrt((double)num));
			for (int i = 0; i < s_SortedPrimes.Count && s_SortedPrimes[i] < sqrtFloor; )
			{
				if (Factors.IsFactor(s_SortedPrimes[i], num))
				{
					num /= s_SortedPrimes[i];
					primeFactors.Add(s_SortedPrimes[i]);
				}
				else
				{
					++i;
				}
			}

			var potentialFactor = LargestCachedPrime() + 2;
			while (potentialFactor < sqrtFloor)
			{
				if (Factors.IsFactor(potentialFactor, num))
				{
					num /= potentialFactor;
					primeFactors.Add(potentialFactor);
				}
				else
				{
					potentialFactor += 2;
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

			if (num == 2)
			{
				return true;
			}

			// Even numbers, other than 2, are not prime.
			// This will let us only check for odd factors later.
			if (BasicMath.IsEven(num))
			{
				return false;
			}

			// If we have it cached
			if (s_Primes.Contains(num))
			{
				return true;
			}

			// To make future calls to IsPrime faster, and to always know all primes up to some N,
			// find all primes between the old N and the given number.
			// (Since the given number wasn't in the cache, we haven't already done this.)
			for (var largestCheckedPrime = s_LargestCheckedPrime; largestCheckedPrime + 2 < num; largestCheckedPrime += 2)
			{
				IsPrime(largestCheckedPrime + 2);
			}

			s_LargestCheckedPrime = num;

			// At this point, we know all prime numbers less than the given number.
			// Check to see if each prime number less than the sqrt of the given number is a factor.
			// (We don't care abount non-primes, since they have smaller prime factors which we already checked.)
			int sqrtFloor = (int)Math.Floor(Math.Sqrt((double)num));
			foreach(var prime in s_SortedPrimes)
			{
				if (prime > sqrtFloor)
				{
					break;
				}

				if (Factors.IsFactor(prime, num))
				{
					return false;
				}
			}

			CacheAsPrime(num);
			return true;
		}

		private static void CacheAsPrime(long prime)
		{
			if (s_Primes.Add(prime))
			{
				s_SortedPrimes.Add(prime);
			}
		}

		private static long LargestCachedPrime()
		{
			return s_SortedPrimes[s_SortedPrimes.Count - 1];
		}

		/// <summary>
		///		Finds the Nth prime number (where 2 is the 1st).
		/// </summary>
		/// <param name="n">The prime number to find.</param>
		/// <returns>The Nth prime number.</returns>
		public static long NthPrime(int n)
		{
			if (n <= 0)
			{
				throw new ArgumentOutOfRangeException("NthPrime(): n must be strictly positive.");
			}

			if (n == 1)
			{
				return 2;
			}

			while (s_SortedPrimes.Count < n)
			{
				var largestKnownPrime = LargestCachedPrime();
				IsPrime(3*largestKnownPrime);
			}

			return s_SortedPrimes[n];
		}
	}
}
