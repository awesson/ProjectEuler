﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;

namespace ProjectEuler
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var sw = new Stopwatch();
			sw.Start();

			var ans = Problem31();

			sw.Stop();

			Print(ans);

			Console.WriteLine("Elapsed={0}", sw.Elapsed);
		}

		private static void Print<T>(T value)
		{
			Console.WriteLine(value);
		}

		/// <summary>
		/// 
		/// "Euler discovered the remarkable quadratic formula:
		///
		/// n^2 + n + 41
		///
		/// It turns out that the formula will produce 40 primes for the consecutive integer values 0 ≤ n ≤ 39.
		/// However, when n = 40, 40^2 + 40 + 41 = 40*(40 + 1) + 41 is divisible by 41, and certainly when n=41,
		/// 41^2 + 41 + 41 is clearly divisible by 41.
		///
		/// The incredible formula n^2 − 79n + 1601 was discovered, which produces 80 primes for the consecutive
		/// values 0 ≤ n ≤ 79. The product of the coefficients, −79 and 1601, is −126479.
		///
		/// Considering quadratics of the form:
		///
		/// n^2 + an + b, where |a| &lt; 1000 and |b| ≤ 1000 >
		///
		/// where |n| is the modulus/absolute value of n
		/// e.g. |11| = 11 and |−4| = 4
		///
		/// Find the product of the coefficients, a and b, for the quadratic expression that produces the maximum
		/// number of primes for consecutive values of n, starting with n = 0."
		/// 
		/// </summary>
		/// <returns></returns>
		private static int Problem27()
		{
			var longestPrimeSeqLength = 0;
			var longestPrimeSeqProduct = 0;
			for (var a = -999; a < 1000; ++a)
			{
				for (var b = -999; b <= 1000; ++b)
				{
					var quadratic = new Polynomials.Polynomial<int>();
					quadratic.AddTerm(0, b);
					quadratic.AddTerm(1, a);
					quadratic.AddTerm(2, 1);

					var len = 0;
					for (var x = 0; Primes.IsPrime(quadratic.EvaluateForX(x)); ++x, ++len)
					{
					}

					if (len > longestPrimeSeqLength)
					{
						longestPrimeSeqLength = len;
						longestPrimeSeqProduct = a * b;
					}
				}
			}

			return longestPrimeSeqProduct;
		}

		/// <summary>
		/// 
		/// "Starting with the number 1 and moving to the right in a clockwise direction a 5 by 5 spiral is formed as follows:
		///
		/// 21 22 23 24 25
		/// 20  7  8  9 10
		/// 19  6  1  2 11
		/// 18  5  4  3 12
		/// 17 16 15 14 13
		///
		///	It can be verified that the sum of the numbers on the diagonals is 101.
		///
		///	What is the sum of the numbers on the diagonals in a 1001 by 1001 spiral formed in the same way?"
		///
		/// </summary>
		/// <returns></returns>
		private static int Problem28()
		{
			var runningSum = 1;
			var cur = 1;
			// The numbers on the diagonal, in increasing order, increase by the length of the spiral at that point minus 1
			for (var len = 2; len < 1001; len += 2)
			{
				for (var corner = 0; corner < 4; ++corner)
				{
					cur += len;
					runningSum += cur;
				}
			}
			return runningSum;
		}

		/// <summary>
		///
		/// "Consider all integer combinations of a^b for 2 ≤ a ≤ 5 and 2 ≤ b ≤ 5:
		///
		/// 2^2=4, 2^3=8, 2^4=16, 2^5=32
		/// 3^2=9, 3^3=27, 3^4=81, 3^5=243
		/// 4^2=16, 4^3=64, 4^4=256, 4^5=1024
		/// 5^2=25, 5^3=125, 5^4=625, 5^5=3125
		///
		/// If they are then placed in numerical order, with any repeats removed, we get the following sequence of 15 distinct terms:
		///
		/// 4, 8, 9, 16, 25, 27, 32, 64, 81, 125, 243, 256, 625, 1024, 3125
		///
		/// How many distinct terms are in the sequence generated by a^b for 2 ≤ a ≤ 100 and 2 ≤ b ≤ 100?"
		/// 
		/// </summary>
		/// <returns></returns>
		private static int Problem29()
		{
			var pows = new HashSet<BigInteger>();
			for (BigInteger i = 2; i <= 100; ++i)
			{
				for (int j = 2; j <= 100; ++j)
				{
					pows.Add(BasicMath.Pow(i, j));
				}
			}
			return pows.Count;
		}

		/// <summary>
		///
		/// "Surprisingly there are only three numbers that can be written as the sum of fourth powers of their digits:
		///
		/// 1634 = 1^4 + 6^4 + 3^4 + 4^4
		/// 8208 = 8^4 + 2^4 + 0^4 + 8^4
		/// 9474 = 9^4 + 4^4 + 7^4 + 4^4
		///
		///	As 1 = 1^4 is not a sum it is not included.
		///
		///	The sum of these numbers is 1634 + 8208 + 9474 = 19316.
		///
		///	Find the sum of all the numbers that can be written as the sum of fifth powers of their digits."
		/// 
		/// </summary>
		/// <returns></returns>
		private static int Problem30()
		{
			var sum = 0;
			// Max digit is 9^5. With 6 digits their sum is at most 6*9^5 = 354294. With 7, 7*9^5 is only 413343,
			// so no number greater than 6*9^5 could ever be the sum of 5th powers of their digits.
			for (var num = 2; num < 354294; ++num)
			{
				if (BasicMath.SumOfDigits(num, 5) == num)
				{
					sum += num;
				}
			}
			return sum;
		}

		private static int Problem31()
		{
			return Combinatorics.AdditionCombinations(new[] { 1, 2, 5, 10, 20, 50, 100, 200 }, 200);
		}
	}
}
