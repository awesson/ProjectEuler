using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
	public class BasicMath
	{
		private static long[] s_PowersOfTen = {1,
											   10,
											   100,
											   1000,
											   10000,
											   100000,
											   1000000,
											   10000000,
											   100000000,
											   1000000000,
											   10000000000,
											   100000000000,
											   1000000000000,
											   10000000000000,
											   100000000000000,
											   1000000000000000,
											   10000000000000000,
											   100000000000000000,
											   1000000000000000000};

		public static bool IsEven(long num)
		{
			return (num & 1) == 0;
		}

		public static bool IsPalindrome(long num)
		{
			num = Math.Abs(num);
			var numDigits = NumDigits((ulong)num);

			for (int digit = 0; digit < (numDigits / 2); ++digit)
			{
				var oppositeDigit = numDigits - digit - 1;
				if (GetDecimalDigit(num, digit) != GetDecimalDigit(num, oppositeDigit))
				{
					return false;
				}
			}

			return true;
		}

		public static int NumDigits(ulong num)
		{
			var numDigits = 0;
			while (num > 0)
			{
				++numDigits;
				num /= 10;
			}
			return numDigits;
		}

		public static long LargestPalindromeProduct(int multiplierDigits)
		{
			var maxMultiplier = MaxNumberOfNDigits(multiplierDigits);
			foreach (var prod in Factors.ProductsInDecreasingOrder(maxMultiplier))
			{
				if (IsPalindrome(prod))
				{
					return prod;
				}
			}

			return 1;
		}

		public static long MaxNumberOfNDigits(int digits)
		{
			var maxNum = 0;
			var place = 1;
			while (digits > 0)
			{
				maxNum += 9 * place;
				place *= 10;
				--digits;
			}
			return maxNum;
		}

		public static int GetDecimalDigit(long num, int digit)
		{
			var pow10 = NthPower10(digit);
			var nextPow10 = NthPower10(digit + 1);
			return (int)((num % nextPow10) / pow10);
		}

		public static long NthPower10(int pow)
		{
			return s_PowersOfTen[pow];
		}
	}
}
