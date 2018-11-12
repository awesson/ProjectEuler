using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ProjectEuler
{
	public static class Fibonacci
	{
		/// <summary>
		///     Calculates and returns the sum of all even Fibonacci numbers less than or equal to the given number.
		/// </summary>
		/// <param name="maxN">The value of the Fibonacci number at which to stop summing.</param>
		/// <param name="numPrevValues">
		///     Optional parameter which allows for a generalization of the Fibonacci numbers.
		///     The given number is the number of previous terms to use in the sum which defines the next term.
		///     2 is the default where f(n) = f(n-1) + f(n-2). If for example, 5 was given instead,
		///     then the new definition would be, f(n) = f(n-1) + f(n-2) + f(n-3) + f(n-4) + f(n-5).
		///     If <paramref name="numPrevValues" /> is less than 2, it ends iteration right away.
		/// </param>
		/// <exception cref="OverflowException">
		///     If the nth Fibonacci number or the even sum is greater than long.MaxValue.
		/// </exception>
		public static BigInteger EvenSum(BigInteger maxN, int numPrevValues = 2)
		{
			BigInteger evenSum = 0;
			foreach (var fib in FibonacciNums(numPrevValues))
			{
				if (fib > maxN)
				{
					return evenSum;
				}

				if (fib.IsEven)
				{
					evenSum += fib;
				}
			}

			throw new OverflowException("Could not calculate a Fibonacci number greater than " + maxN);
		}

		/// <summary>
		///     Returns the Nth Fibonacci number.
		/// </summary>
		/// <param name="n">Which fibonacci number to return.</param>
		/// <param name="numPrevValues">
		///     Optional parameter which allows for a generalization of the Fibonacci numbers.
		///     The given number is the number of previous terms to use in the sum which defines the next term.
		///     2 is the default where f(n) = f(n-1) + f(n-2). If for example, 5 was given instead,
		///     then the new definition would be, f(n) = f(n-1) + f(n-2) + f(n-3) + f(n-4) + f(n-5).
		///     If <paramref name="numPrevValues" /> is less than 2, it ends iteration right away.
		/// </param>
		/// <returns>The Nth Fibonacci number.</returns>
		public static BigInteger NthFibonacci(int n, int numPrevValues = 2)
		{
			if (n <= 0 || numPrevValues < 2)
			{
				return 0L;
			}
			
			return FibonacciNums(numPrevValues).ElementAt(n);
		}

		/// <summary>
		///     Returns an enumeration over the Fibonacci numbers.
		/// </summary>
		/// <param name="numPrevValues">
		///     Optional parameter which allows for a generalization of the Fibonacci numbers.
		///     The given number is the number of previous terms to use in the sum which defines the next term.
		///     2 is the default where f(n) = f(n-1) + f(n-2). If for example, 5 was given instead,
		///     then the new definition would be, f(n) = f(n-1) + f(n-2) + f(n-3) + f(n-4) + f(n-5)
		///     If <paramref name="numPrevValues" /> is less than 2, it ends iteration right away.
		/// </param>
		/// <returns>The next generalized Fibonacci number.</returns>
		public static IEnumerable<BigInteger> FibonacciNums(int numPrevValues = 2)
		{
			if (numPrevValues < 2)
			{
				yield break;
			}

			yield return 0;

			yield return 1;

			var prev = new BigInteger[numPrevValues];
			prev[1] = 1;

			var n = 2;
			while (n < numPrevValues)
			{
				prev[n] =  prev.Aggregate(BigInteger.Add);
				yield return prev[n];
				++n;
			}

			while (true)
			{
				var next = prev.Aggregate(BigInteger.Add);

				yield return next;

				for (var i = 1; i < numPrevValues; ++i)
				{
					prev[i - 1] = prev[i];
				}
				prev[numPrevValues - 1] = next;
			}
		}
	}
}