using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
	public class Primes
	{
		private static HashSet<long> s_Primes = new HashSet<long>();
		private static List<long> s_SortedPrimes = new List<long>();
		private static long s_LargestCheckedPrime;

		static Primes()
		{
			// 2 is a special case since it is the only even prime.
			s_Primes.Add(2);
			s_SortedPrimes.Add(2);
			// and why not...(actually it's because it's useful to always have the largest known prime be odd.)
			s_Primes.Add(3);
			s_SortedPrimes.Add(3);
			s_LargestCheckedPrime = 3;
		}

		/// <summary>
		///		Returns a sorted list of all the prime factors of the given number.
		/// </summary>
		/// <param name="num">The number to find the prime factors of.</param>
		/// <returns>A sorted list of all the prime factors of the given number.</returns>
		public static List<long> FindPrimeFactors(long num)
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
			
			// We are looking for prime factors, so start checking all the primes we know about.
			// Since a factor of num can't be more than it's square root,
			// we can stop the loop once the prime we are checking is larger than num's sqrt.
			// (We could generate all primes up to the sqrt before we do this
			// so that we only have to check primes, rather than go through the second for loop below,
			// but that overhead makes the whole function take more time, potentially a lot more.)
			int i = 0;
			while (i < s_SortedPrimes.Count && s_SortedPrimes[i]*s_SortedPrimes[i] <= num)
			{
				// If we find a factor, reduce the number as many times as that factor goes into it
				// before moving on to the next potential factor.
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

			// At this point we have checked all primes up to LargestSortedCachedPrime() as potential factors.
			// From now on we check numbers which we are not sure are prime (only odd numbers though).
			// Since we reduce the number as much as possible every time we find a factor,
			// the next factor we find must be the smallest number which is a factor of the current number.
			// If the factor we find was not prime, then it's prime factors would also be factors of
			// the current number. This contradicts that it's the smallest factor of the current number.
			// Therefore, if we do find a factor, that factor is necessarily prime.
			var potentialFactor = LargestSortedCachedPrime() + 2;
			while (potentialFactor*potentialFactor <= num)
			{
				if (Factors.IsFactor(potentialFactor, num))
				{
					num /= potentialFactor;
					primeFactors.Add(potentialFactor);

					// Since we're here and we know that potentialFactor is a prime,
					// we might as well take the chance to cache it.
					s_Primes.Add(potentialFactor);
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

		/// <summary>
		///		Finds and returns the largest prime factor of the given number.
		/// </summary>
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

			// Check if we have it cached
			if (s_Primes.Contains(num))
			{
				return true;
			}

			// We don't need to check for any factors larger than the square root.
			var sqrtFloor = (long)Math.Floor(Math.Sqrt((double)num));

			// First check if any known primes are factors.
			foreach(var prime in s_SortedPrimes)
			{
				if (prime > sqrtFloor)
				{
					s_Primes.Add(num);
					return true;
				}

				if (Factors.IsFactor(prime, num))
				{
					return false;
				}
			}

			// At this point we have checked all primes up to LargestSortedCachedPrime() as potential factors.
			// There is no need to check any non-prime numbers less than LargestSortedCachedPrime()
			// since those numbers must have some prime factors which we already checked for
			// (and if all prime factors of a number are not factors of some other number,
			// than that number cannot be a factor of the other number).
			for (var potentialFactor = LargestSortedCachedPrime() + 2;
				potentialFactor <= sqrtFloor;
				potentialFactor += 2)
			{
				if (Factors.IsFactor(potentialFactor, num))
				{
					return false;
				}
			}

			s_Primes.Add(num);
			return true;
		}

		private static void GeneratePrimesUpToN(long num)
		{
			// We can assume s_LargestCheckedPrime is odd
			// and we already know about the only even prime
			// because of the static constructor.
			while (s_LargestCheckedPrime + 2 <= num)
			{
				s_LargestCheckedPrime += 2;
				if (IsPrime(s_LargestCheckedPrime))
				{
					s_SortedPrimes.Add(s_LargestCheckedPrime);
				}
			}
		}

		private static long LargestSortedCachedPrime()
		{
			return s_SortedPrimes[s_SortedPrimes.Count - 1];
		}

		/// <summary>
		///		Finds and returns all primes less than or equal to the given number,
		///		in order of smallest to largest.
		/// </summary>
		public static IEnumerable<long> PrimesUptoN(long num)
		{
			GeneratePrimesUpToN(num);
			return s_SortedPrimes.TakeWhile(prime => prime <= num);
		}

		/// <summary>
		///		Finds the Nth prime number (where 2 is the 1st).
		/// </summary>
		/// <param name="n">The prime number to find.</param>
		/// <returns>The Nth prime number.</returns>
		public static long NthPrime(int num)
		{
			if (num <= 0)
			{
				throw new ArgumentOutOfRangeException("NthPrime(): n must be strictly positive.");
			}

			// Find primes up to twice the value of the prime
			// we currently know about until we know about num primes.
			while (s_SortedPrimes.Count < num)
			{
				GeneratePrimesUpToN(2*LargestSortedCachedPrime());
			}

			return s_SortedPrimes[num - 1];
		}

		/// <summary>
		///		Finds and returns the sum of all primes less than or equal to the given number.
		/// </summary>
		public static long SumOfPrimesUpToN(int num)
		{
			return PrimesUptoN(num).Sum();
		}
	}
}
