﻿using System.Diagnostics;

namespace ProjectEuler.Pythagorean
{
	/// <summary>
	///     A class solving things related to the Pythagorean theorem: a^2 + b^2 = c^2
	/// </summary>
	public static class PythagoreanUtils
	{
		/// <summary>
		///     Finds the Pythagorean triple with the given sum.
		/// </summary>
		/// <param name="sum">The required sum of the triple (a + b + c).</param>
		/// <param name="triple">The triple with the given sum.</param>
		/// <returns>True if a triple was found, false otherwise.</returns>
		public static bool TripleSum(int sum, ref Triple triple)
		{
			if (sum <= 0)
			{
				// a, b, and c are strictly positive, so their sum must also be positive.
				return false;
			}

			// See triple.cs for info about Pythagorean Triples used here.

			// a + b + c is always even.
			if (!BasicMath.IsEven(sum))
			{
				return false;
			}

			/**
			 * S = a + b + c = k(m^2 - n^2) + (2kmn) + k(m^2 + n^2) = k(2m^2 + 2mn)
			 * S = 2km(m + n)
			 * 
			 * S/(m + n) = 2km
			 * b = 2kmn = Sn/(m + n) = Sn(m + n)/(m + n)^2
			 *
			 * a = k(m^2 - n^2) = k(m - n)(m + n)
			 * 
			 * S - a = 2km(m + n) - k(m - n)(m + n) = k(2m - (m - n))(m + n) = k(m + n)^2
			 * (S - a)/k = (m + n)^2
			 * 
			 * S - 2a = 2km(m + n) - 2k(m - n)(m + n) = k(2m - (2m - 2n))(m + n) = 2kn(m + n)
			 * (S - 2a)/(2k) = n(m + n)
			 * 
			 * b = Sn(m + n)/(m + n)^2 = S((S - 2a)/(2k))/((S - a)/k) = S(S/2 - a)/(S - a)
			 * 
			 * We now know b in terms of the sum and a.
			 * We also know that b must be a positive integer, which implies that (S/2 - a) must be positive,
			 * or in other words, S/2 > a.
			 * Similarly the numerator must be evenly divisible by the denominator,
			 * or in other words, S(S/2 - a) % (S - a) = 0.
			 * (Since S is even S/2 will always be an integer.)
			 **/
			var halfSum = (sum >> 1);
			for (var a = 1; a < halfSum; ++a)
			{
				var numerator = sum * (halfSum - a);
				var denominator = sum - a;
				if ((numerator % denominator) == 0)
				{
					triple.A = a;
					triple.B = numerator / denominator;
					Debug.Assert(triple.B >= 0);
					triple.C = sum - a - triple.B;
					Debug.Assert(triple.C > 0);
					return true;
				}
			}

			return false;
		}
	}
}
