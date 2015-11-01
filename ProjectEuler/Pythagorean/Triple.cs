using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler.Pythagorean
{
	/// <summary>
	///		Represents an integer set of triangle lengths satisfying the pythagorean formula.
	/// 
	///		Things to note about pythagorean triples:
	///		* c > a and c > b (we don't assume here that b > a)
	/// 
	///		* Euclid's Formula:
	///			Given m and n where m > n, either m or n is odd, but not both, and m and n are coprime,
	///			a = m^2 - n^2
	///			b = 2mn
	///			c = m^2 + n^2
	///			defines all primitive pythagorean Triples.
	///			(Primative triples are ones where a, b, and c are coprime.)
	///			
	///			We can multiply by a factor of k to get all pythagorean triples:
	///			a = k(m^2 - n^2)
	///			b = 2kmn
	///			c = k(m^2 + n^2)
	///			
	///		* a and c are always odd in primative triples:
	///			m ± n is odd if one of n or m is odd. (even ± odd = odd, odd ± even = odd)
	///			Since an even number squared is still even and an odd number squared is still odd,
	///			m^2 ± n^2 must also be odd.
	/// 
	///		* b is always even in primative triples:
	///			Since b = 2*m*n and an odd times and even is always even, b is even.
	///			
	///		* a + b + c is even:
	///			Regardless of whether it is a primitive triple or not.
	///			If it's not, then a + b + c = k(a' + b' + c') for some primitive triple, (a', b', c').
	///			The sum a' + b' + c' must be even since it's odd + even + odd.
	///			The even sum times any k will still be even.
	///	</summary>
	public struct Triple
	{
		public int A { get; set; }
		public int B { get; set; }
		public int C { get; set; }

		public long Sum()
		{
			return (long)A + (long)B + (long)C;
		}

		public long Product()
		{
			return (long)A * (long)B * (long)C;
		}

		public override string ToString()
		{
			return string.Format("({0}, {1}, {2})", A, B, C);
		}
	}
}
