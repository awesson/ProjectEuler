using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
	public class Factors
	{
		public static ulong SumOfMultiplesLessThan(ulong value, ulong multiple1, ulong multiple2)
		{
			if (value == 0 || value == 1)
			{
				return 0;
			}

			// We don't include the value as a multiple
			value -= 1;

			var firstSum = Series.SumUpToVMultM(value, multiple1);
			var secondSum = Series.SumUpToVMultM(value, multiple2);
			var overlapOfSums = Series.SumUpToVMultM(value, multiple1 * multiple2);
			return firstSum + secondSum - overlapOfSums;
		}

		public static bool IsFactor(long potentialFactor, long num)
		{
			return (num % potentialFactor) == 0;
		}

		public static bool IsFactor(ulong potentialFactor, ulong num)
		{
			return (num % potentialFactor) == 0;
		}

		public static List<ulong> FindFactors(ulong num)
		{
			var factors = new List<ulong>();

			// We don't need to check for any factors larger than the square root.
			var sqrtFloor = (ulong)Math.Floor(Math.Sqrt((double)num));
			for (ulong potentialFactor = 1; potentialFactor < sqrtFloor; ++potentialFactor)
			{
				if (IsFactor(potentialFactor, num))
				{
					// Keep track of both the factor we just found and it's compliment factor.
					// (eg. 3 is a factor of 21, and so is 21/3 = 7)
					factors.Add(potentialFactor);
					factors.Add(num/potentialFactor);
				}
			}

			// Special case to check if the sqrt is an integer factor.
			if (sqrtFloor * sqrtFloor == num)
			{
				factors.Add(sqrtFloor);
			}

			return factors;
		}

		/// <summary>
		///		Triangle numbers are the sum of integers 1 through N.
		///		This function finds and returns the smallest triangle number with the given number of factors.
		/// </summary>
		public static ulong FirstTriangleNumberWithAtLeastNFactors(ulong num)
		{
			var triangleNumIndex = 1uL;
			var triangleNum = Series.SumUpToVMultM(triangleNumIndex, 1uL);
			while ((ulong)FindFactors(triangleNum).Count < num)
			{
				++triangleNumIndex;
				triangleNum = Series.SumUpToVMultM(triangleNumIndex, 1uL);
			}

			return triangleNum;
		}

		public static void ReduceToOddFactor(ref long num)
		{
			if (num == 0)
			{
				return;
			}

			while (BasicMath.IsEven(num))
			{
				num >>= 1;
			}
		}

		/// <summary>
		///		Enumerates a list of the products of two numbers less than or equal to the given number,
		///		in decreasing order.
		///		(eg. For a maxMultiplier of 3, it would be 3*3, 3*2, 2*2, 3*1, 2*1, 1*1) 
		///	</summary>
		public static IEnumerable<long> ProductsInDecreasingOrder(long maxMultiplier)
		{
			var diffFromMax = 0;
			var incr = true;

			var mult1 = maxMultiplier;
			var mult2 = maxMultiplier;
			while (mult1 > 1 || mult2 > 1)
			{
				yield return (mult1 * mult2);
				if (mult1 == maxMultiplier)
				{
					mult1 -= diffFromMax;
					mult2 += diffFromMax - 1;
					if (incr)
					{
						++diffFromMax;
					}
					incr = !incr;
				}
				else
				{
					++mult1;
					--mult2;
				}
			}
		}

		/// <summary>
		///		Returns the smallest common multiple of all the numbers 1 through n.
		///		(eg. for n = 4, this is 12 since 12 is divisible by 2, 3, and 4
		///		and no other number smaller than 12 has this property.)
		/// </summary>
		public static long SmallestCommonMultipleOf1ThroughN(int n)
		{
			if (n <= 0)
			{
				throw new ArgumentException("SmallestCommonMultipleOf1ThroughN(n): n must be positive.");
			}

			var commonMultiple = 1L;
			// Increase the current common multiple to include each number starting at 2
			for (long i = 2; i <= n; ++i)
			{
				var remainder = commonMultiple % i;
				// If it doesn't divide equally, then we need a bigger number.
				if (remainder != 0)
				{
					/** We need to find integer M such that (M*commonMultiple) % i == 0
					 * (M*commonMultiple) % i = (M(commonMultiple - remainder + remainder)) % i
					 * = (M(commonMultiple - remainder) + M*remainder) % i
					 * = ((M(commonMultiple - remainder)) % i) + ((M*remainder) % i)
					 * i divides evenly into (commonMultiple - remainder) by the definition of modulus, so
					 * ((M(commonMultiple - remainder)) % i) + ((M*remainder) % i) = 0 + ((M*remainder) % i)
					 * So we need an integer M such that (M*remainder) % i == 0
					 * If we make M, i, then we can easily see that (i*remainder) % i == 0,
					 * since i*remainder/i exactly equals remainder. But can we do better than i?
					 * More generally, what we need is for the shared factors of M*remainder to contain i.
					 * Lets say the prime factors of M are Mp => (Mp1, Mp2, ... Mpn)
					 * the prime factors of remainder are Rp => (Rp1, Rp2, ... Rpn)
					 * and the prime factors of i are Ip => (Ip1, Ip2, ... Ipn)
					 * We want the intersection of Mp and Rp to equal Ip.
					 * (M*remainder = Mp1*Mp2*...*Mpn*Rp1*Rp2*...*Rpn
					 * = Ip1*Ip2*...*Ipn*(some other primes)
					 * = i*primes which % i is 0.)
					 * So first we need to find what prime factors i and remainder share
					 * and then make M the product of the missing factors.
					 * The prime factors they have in common, multiplied together,
					 * is what's known as the greatest common divisor.
					 * We can easily get the missing factors by dividing out the GCD from i.
					 **/
					commonMultiple *= i / GCD(i, remainder);
				}
			}

			return commonMultiple;
		}

		/// <summary>
		///		Returns the greatest common divisor of the given two numbers.
		/// </summary>
		public static long GCD(long num1, long num2)
		{
			if (num1 == long.MinValue || num2 == long.MinValue)
			{
				throw new ArgumentException("Can't find the greatest common divisor of " + long.MinValue);
			}

			int powersOf2InCommon;

			// GCD(0, num) => num
			// GCD(0, 0) => 0
			if (num1 == 0) return num2;
			if (num2 == 0) return num1;

			// Reduce the numbers by common powers of 2.
			for (powersOf2InCommon = 0; BasicMath.IsEven(num1) && BasicMath.IsEven(num2); ++powersOf2InCommon)
			{
				num1 >>= 1;
				num2 >>= 1;
			}

			ReduceToOddFactor(ref num1);

			// From here on, num1 is always odd.
			do
			{
				// remove all factors of 2 in num2 since they are not common
				ReduceToOddFactor(ref num2);

				// Now num1 and num2 are both odd. Swap if necessary so num1 <= num2,
				// then set num2 = num2 - num1 (which will be even).
				if (num1 > num2)
				{
					long t = num1; num1 = num2; num2 = t;
				}
				num2 = num2 - num1;
			} while (num2 != 0);

			// Restore the common factors of powers of 2
			return num1 << powersOf2InCommon;
		}
	}
}
