using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
	public class Fibonacci
	{
		/// <summary>
		///		Calculates and returns the sum of all even Fibonacci numbers less than or equal to the given number.
		/// </summary>
		public static long EvenSum(long maxN)
		{
			var evenSum = 0L;
			foreach (var fib in FibonacciNums())
			{
				if (fib > maxN)
				{
					return evenSum;
				}

				if (BasicMath.IsEven(fib))
				{
					var prevSum = evenSum;
					evenSum += fib;
					if (evenSum < prevSum)
					{
						throw new OverflowException("The sum is greater than a max long");
					}
				}
			}

			throw new OverflowException("Could not calculate a Fibonacci number greater than " + maxN);
		}

		public static long Fibonacci(int n, uint numPrevValues = 2)
		{
			if (n <= 0)
			{
				return 0;
			}

			var i = 0;
			foreach (var fib in FibonacciNums(numPrevValues))
			{
				if (i == n)
				{
					return fib;
				}

				++i;
			}

			throw new OverflowException("Could not calculate the " + n
									  + " Fibonacci number because it is greater than " + long.MaxValue);
		}

		/// <summary>
		///		Returns an enumeration over the Fibonacci numbers.
		/// </summary>
		/// <param name="numPrevValues">
		///		Optional parameter which allows for a generalization of the Fibonacci numbers.
		///		The given number is the number of previous terms to use in the sum which defines the next term.
		///		2 is the default where f(n) = f(n-1) + f(n-2). If for example, 5 was given instead,
		///		then the new definition would be, f(n) = f(n-1) + f(n-2) + f(n-3) + f(n-4) + f(n-5)
		///		If <paramref name="numPrevValues"/> is less than 2, it ends iteration right away.
		/// </param>
		/// <returns>The next generalized Fibonacci number.</returns>
		public static IEnumerable<long> FibonacciNums(uint numPrevValues = 2)
		{
			if(numPrevValues < 2)
			{
				yield break;
			}

			yield return 0;

			yield return 1;

			var prev = new long[numPrevValues];
			prev[1] = 1L;

			var N = 2;
			while(N < numPrevValues)
			{
				prev[N] = prev.Sum();
				yield return prev[N];
			}

			while (true)
			{
				var next = prev.Sum();
				if (next < prev[numPrevValues - 1])
				{
					// It overflowed, stop returning new values.
					yield break;
				}

				yield return next;

				for (int i = 1; i < numPrevValues; ++i)
				{
					prev[i - 1] = prev[i];
				}
				prev[numPrevValues - 1] = next;
			}
		}
	}
}
