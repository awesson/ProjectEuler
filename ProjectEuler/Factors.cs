using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
	class Factors
	{
		public static int SumOfMultiplesLessThan(int value, int multiple1, int multiple2)
		{
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

		public static void ReduceToOddFactor(ref long n)
		{
			while (BasicMath.IsEven(n))
			{
				n >>= 1;
			}
		}

		public static IEnumerable<long> ProductsInIncreasingOrder(long maxMultiplier)
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
	}
}
