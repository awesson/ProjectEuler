using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.IO;

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

		/// <summary>
		///		Returns the number of significant digits in the given number.
		/// </summary>
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

		/// <summary>
		///		Finds and returns the largest product of two numbers with the given number of digits.
		///		(eg. If the number of digits is 2, then the largest product is 9009 = 99 x 91)
		/// </summary>
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

		/// <summary>
		///		Returns the largest number with the given number of digits.
		///		(In other words, 9 repeated the given number of times).
		/// </summary>
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

		/// <summary>
		///		Returns the nth significant digit in base 10 of the given number.
		/// </summary>
		/// <param name="num">The number to find the digit of.</param>
		/// <param name="digit">The "place" of the digit (eg. 3 would be the thousands place digit).</param>
		public static int GetDecimalDigit(long num, int digit)
		{
			var pow10 = NthPower10(digit);
			var nextPow10 = NthPower10(digit + 1);
			return (int)((num % nextPow10) / pow10);
		}

		/// <summary>
		///		Returns 10^<paramref name="pow"/>.
		/// </summary>
		public static long NthPower10(int pow)
		{
			return s_PowersOfTen[pow];
		}

		/// <summary>
		///		Returns a list of BigIntegers read from the given file which is assumed to
		///		have each number on it's own line with no punctuation.
		/// </summary>
		public static List<BigInteger> ParseFileIntoBigInts(string file)
		{
			var result = new List<BigInteger>();
			using (var nums = new StreamReader(file))
			{
				string line;
				while ((line = nums.ReadLine()) != null)
				{
					BigInteger val;
					if (BigInteger.TryParse(line, out val))
					{
						result.Add(val);
					}
				}
			}

			return result;
		}
	}
}
