using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace ProjectEuler
{
	public static class BasicMath
	{
		private static Dictionary<long, string> s_BasicNumToSpokenString = new Dictionary<long, string>();

		private static long[] s_PowersOfTen =
		{
			1,
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
			1000000000000000000
		};

		static BasicMath()
		{
			// Add the base cases.
			s_BasicNumToSpokenString[0] = "zero";
			s_BasicNumToSpokenString[1] = "one";
			s_BasicNumToSpokenString[2] = "two";
			s_BasicNumToSpokenString[3] = "three";
			s_BasicNumToSpokenString[4] = "four";
			s_BasicNumToSpokenString[5] = "five";
			s_BasicNumToSpokenString[6] = "six";
			s_BasicNumToSpokenString[7] = "seven";
			s_BasicNumToSpokenString[8] = "eight";
			s_BasicNumToSpokenString[9] = "nine";
			s_BasicNumToSpokenString[10] = "ten";
			s_BasicNumToSpokenString[11] = "eleven";
			s_BasicNumToSpokenString[12] = "twelve";
			s_BasicNumToSpokenString[13] = "thirteen";
			s_BasicNumToSpokenString[14] = "fourteen";
			s_BasicNumToSpokenString[15] = "fifteen";
			s_BasicNumToSpokenString[16] = "sixteen";
			s_BasicNumToSpokenString[17] = "seventeen";
			s_BasicNumToSpokenString[18] = "eighteen";
			s_BasicNumToSpokenString[19] = "nineteen";
			s_BasicNumToSpokenString[20] = "twenty";
			s_BasicNumToSpokenString[30] = "thirty";
			s_BasicNumToSpokenString[40] = "forty";
			s_BasicNumToSpokenString[50] = "fifty";
			s_BasicNumToSpokenString[60] = "sixty";
			s_BasicNumToSpokenString[70] = "seventy";
			s_BasicNumToSpokenString[80] = "eighty";
			s_BasicNumToSpokenString[90] = "ninety";
			s_BasicNumToSpokenString[100] = "hundred";
			s_BasicNumToSpokenString[1000] = "thousand";
			s_BasicNumToSpokenString[1000000] = "million";
			s_BasicNumToSpokenString[1000000000] = "billion";
			s_BasicNumToSpokenString[1000000000000] = "trillion";
			s_BasicNumToSpokenString[1000000000000000] = "quadrillion";
			s_BasicNumToSpokenString[1000000000000000000] = "quintillion";
		}

		public static bool IsEven(long num)
		{
			return (num & 1) == 0;
		}

		/// <summary>
		///     Returns whether or not the given number is a palindrome.
		///     (eg. 12321)
		/// </summary>
		/// <exception cref="OverflowException">If <paramref name="num" /> is long.MinValue.</exception>
		public static bool IsPalindrome(long num)
		{
			var numDigits = NumDigits(num);

			for (var digit = 0; digit < (numDigits / 2); ++digit)
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
		///     Returns the number of significant digits in the given number.
		/// </summary>
		public static int NumDigits(int num)
		{
			return NumDigits((BigInteger) num);
		}

		/// <summary>
		///     Returns the number of significant digits in the given number.
		/// </summary>
		public static int NumDigits(long num)
		{
			return NumDigits((BigInteger) num);
		}

		/// <summary>
		///     Returns the number of significant digits in the given number.
		/// </summary>
		/// <exception cref="OverflowException">If <paramref name="num" /> is long.MinValue.</exception>
		public static int NumDigits(BigInteger num)
		{
			return num.ToString().Length;
		}

		/// <summary>
		///     Finds and returns the largest product of two numbers with the given number of digits.
		///     (eg. If the number of digits is 2, then the largest product is 9009 = 99 x 91)
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
		///     Returns the largest number with the given number of digits.
		///     (In other words, 9 repeated the given number of times).
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
		///     Returns the nth significant digit in base 10 of the given number.
		/// </summary>
		/// <param name="num">The number to find the digit of.</param>
		/// <param name="digit">The "place" of the digit (eg. 3 would be the thousands place digit).</param>
		public static int GetDecimalDigit(long num, int digit)
		{
			long pow10;
			long nextPow10;

			try
			{
				pow10 = NthPower10(digit);
			}
			catch (ArgumentOutOfRangeException)
			{
				// If the digit is out of range of a long, num can't have a digit there.
				return 0;
			}

			try
			{
				nextPow10 = NthPower10(digit + 1);
			}
			catch (ArgumentOutOfRangeException)
			{
				// This means only the next power of ten is out of the range of a long,
				// so even if we theoretically could represent the next power of ten and
				// then take the mod, since num is within the range of a long and
				// therefore less than the next power of ten, moding wont change the value of num.
				return (int) (num / pow10);
			}

			return (int) Math.Abs((num % nextPow10) / pow10);
		}

		/// <summary>
		///     Returns the 3 digit number in the given section. (eg. 1,347,328 in the 1 section is 347)
		/// </summary>
		/// <param name="num">The number to find the 3-digit number of.</param>
		/// <param name="section">The "section" of the 3-digit number (eg. 1 would be the thousands section).</param>
		public static int GetDecimalSection(long num, int section)
		{
			long minPow10;
			long maxPow10;

			try
			{
				minPow10 = NthPower10(3 * section);
			}
			catch (ArgumentOutOfRangeException)
			{
				// If the section's next digit is out of range of a long, num can't have a set of digits there.
				return 0;
			}

			try
			{
				maxPow10 = NthPower10(3 * (section + 1));
			}
			catch (ArgumentOutOfRangeException)
			{
				// This means only the power of ten in the next section is out of the range of a long,
				// so even if we theoretically could represent the power of ten in the next section and
				// then take the mod, since num is within the range of a long and
				// therefore less than the power of ten in the next section,
				// moding wont change the value of num.
				return (int) (num / minPow10);
			}

			return (int) ((num % maxPow10) / minPow10);
		}

		public static int SumOfDigits<T>(T num)
		{
			return SumOfDigits(num.ToString());
		}

		public static int SumOfDigits(string num)
		{
			return num.Select(c => (int) char.GetNumericValue(c)).Sum();
		}

		/// <summary>
		///     Returns <paramref name="num" /> moved <paramref name="delta" /> away/toward 0.
		///     (Note: If delta is negative and delta > abs(num), then the return value will change sign.)
		/// </summary>
		public static long ChangeAbsoluteValue(long num, long delta)
		{
			var sign = num < 0 ? -1L : 1L;
			return num + sign * delta;
		}

		/// <summary>
		///     Returns N! for the given number.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">If <paramref name="num" /> is negative.</exception>
		public static BigInteger Factorial(int num)
		{
			if (num < 0)
			{
				throw new ArgumentOutOfRangeException(@"Factorial does not have a standard definition
														for negative integers.");
			}

			// 0! == 1 by definition of an empty product.
			if (num == 0)
			{
				return 1;
			}

			var factorial = new BigInteger(num);
			for (var i = num - 1; i > 1; --i)
			{
				factorial *= i;
			}
			return factorial;
		}

		/// <summary>
		///     Returns 10^<paramref name="pow" />.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">
		///     If <paramref name="pow" /> is negative or greater than 18,
		///     since negative powers of ten are fractions which cannot be represented by a long
		///     and 10^19 or higher cannot be represented by a long.
		/// </exception>
		public static long NthPower10(int pow)
		{
			if (pow < 0)
			{
				throw new ArgumentOutOfRangeException(@"NthPowerOf10 can only find positive powers of 10");
			}

			if (pow >= s_PowersOfTen.Length)
			{
				throw new ArgumentOutOfRangeException("cannot represent 10^" + pow + " with an int64");
			}

			return s_PowersOfTen[pow];
		}

		/// <summary>
		///		Returns a string containing the part of the decimal representation of the fraction
		///		1/<paramref name="denominator"/> which repeats. If the decimal terminates, eg. 1/8 = 0.125,
		///		then the empty string is returned. It also only returns the recurring part,
		///		eg. 1/6 = 0.1(6) would return "6".
		/// </summary>
		/// <param name="denominator">The bottom part of the fraction (the top is assumed to be 1, making it a unit fraction).</param>
		/// <returns>The recurring part of the decimal representation or the empty string if the decimal terminates.</returns>
		public static string GetUnitFractionRecurringDecimal(int denominator)
		{
			string decimalDigits = "";
			var seenRemainders = new Dictionary<int, int>();
			
			var remainder = 10;
			// Do long division until there's a repeat or we've found the whole decimal.
			// We've found the whole decimal if the remainder is 0
			// (at this point we are just adding 0s to the end of a decimal which does nothing).
			// We are at the end of a recurring pattern if the remainer is the same as one that we
			// saw in a previous step (since the denominator isn't changing, if the remainder is the same,
			// we will get the same values for the digits too).
			while (remainder != 0 && !seenRemainders.ContainsKey(remainder))
			{
				seenRemainders.Add(remainder, decimalDigits.Length);
				while (remainder < denominator)
				{
					remainder *= 10;
				}
				var nextDigit = remainder / denominator;
				decimalDigits += nextDigit;
				remainder = remainder - nextDigit * denominator;
			}

			if (remainder == 0)
			{
				// It terminates, so no recurring part
				return "";
			}

			var beginingOfPattern = seenRemainders[remainder];
			return decimalDigits.Substring(beginingOfPattern);
		}

		/// <summary>
		///     Returns a list of BigIntegers read from the given file which is assumed to
		///     have each number on it's own line with no punctuation.
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

		/// <summary>
		///     Returns the spoken name of the given number in English.
		///     (eg. 1425 => "one thousand four hundred and twenty five")
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">If <paramref name="num" /> is int.MinValue.</exception>
		public static string ToSpokenEnglish(long num)
		{
			if (num == long.MinValue)
			{
				throw new ArgumentOutOfRangeException("num cannot be long.MinValue");
			}

			var sign = "";
			var result = "";

			if (num < 0)
			{
				sign = "negative ";
				num = -num;
			}

			// First set of base cases is anything 20 or less since those are all added in the static constructor.
			if (num <= 20)
			{
				return sign + s_BasicNumToSpokenString[num];
			}

			// Any other two digit number takes the number without the ones place
			// and the one place name (unless the ones place is 0).
			// (eg. 42 => 40" "2 => "forty two")
			if (num < 100)
			{
				result = sign + s_BasicNumToSpokenString[((num % 100) / 10) * 10];

				var onedigit = num % 10;
				if (onedigit > 0)
				{
					result += " " + s_BasicNumToSpokenString[num % 10];
				}

				return result;
			}

			// Three digit numbers are the hundreds digit number plus " hundred and "
			// plus the string version of the first two digits,
			// unless the two digit part is 0 in which case the " and " gets left off.
			if (num < 1000)
			{
				result = sign
				         + s_BasicNumToSpokenString[GetDecimalDigit(num, 2)]
				         + " "
				         + s_BasicNumToSpokenString[100];

				var twodigit = num % 100;
				if (twodigit != 0)
				{
					result += " and " + ToSpokenEnglish(twodigit);
				}

				return result;
			}

			// For numbers greater than 999, we take the next "section" of 3 digits,
			// convert that to a string, add the place name (eg. "thousand" or "million"),
			// and then add the previous numbers as a string.
			var place = 0;
			while (num > 0)
			{
				// The first 3 digits are the current section since we divide num by 1000 each iteration.
				var sectionNum = GetDecimalSection(num, 0);
				// If the section is 0, no text should be added.
				if (sectionNum != 0)
				{
					var sectionStr = ToSpokenEnglish(sectionNum);
					// If this is the first three digits of the original number,
					// then there is no place modifier or previous section values to append.
					if (place == 0)
					{
						result = sectionStr;
					}
					else
					{
						result = sectionStr
						         + " " + s_BasicNumToSpokenString[NthPower10(place)] + " "
						         + result;
					}
				}

				// Move to the next section
				num /= 1000;
				place += 3;
			}

			return sign + result;
		}
	}
}