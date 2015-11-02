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
			for (int i = 2; i <= n; ++i)
			{
				var remainder = commonMultiple % i;
				// If it doesn't divide equally, then we need a bigger number.
				if (remainder != 0)
				{
					// Obviously we could multiply the current common multiple by i,
					// and then the new value would be divisible by i. If however,
					// the remainder divides equally into i, then we can actually multiply
					// by the quotient and the result will still be a multiple of i.
					// (eg. If the common multiple is currently 42 and i = 8, then the remainder is 2.
					// 8/2 = 4. 42*4 = (40+2)*4 = (8*5 + 2)*4 = 8*20 + 2*4 = 8*20 + 8 = 8*21 = 168)
					if ((i % remainder) == 0)
					{
						commonMultiple *= i / remainder;
					}
					else
					{
						commonMultiple *= i;
					}
				}
			}

			return commonMultiple;
		}
	}
}
