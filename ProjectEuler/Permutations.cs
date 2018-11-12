using System;
using System.Collections.Generic;
using System.Numerics;

namespace ProjectEuler
{
	public static class Permutations
	{
		/// <summary>
		///		Returns the nth permutation of the given characters if listing them lexographically.
		/// </summary>
		/// <param name="chars">The set of characters creating the permutations.</param>
		/// <param name="perm">The nth permutation we are looking for.</param>
		/// <returns>The nth permutation in lexographic order.</returns>
		public static string NthLexographicPermutation(char[] chars, BigInteger perm)
		{
			var numChars = chars.Length;

			if (perm > BasicMath.Factorial(numChars))
			{
				throw new ArgumentException("There are not " + perm + " permutations of " + chars);
			}

			var availableChars = new List<char>(chars);
			availableChars.Sort();

			var resultPerm = "";
			BigInteger currentN = 0;
			// There are n! permutations, and we are going lexographically, so starting with the 'lowest' character at the front,
			// there are n-1! permutations of all the other numbers. If that number is less than the one being asked for,
			// then it must start with a 'higher' character.
			// In other words, the number of permutations going from "0..." to "1..." is n! where n is the number of characters in the rest of
			// the string (the ... part) and so we 'count' the number of permutations this way instead of actually creating each permutation.
			for (var indexToTest = 0; indexToTest < numChars; ++indexToTest)
			{
				var index = 0;
				var permutationsOfFollowingChars = BasicMath.Factorial(numChars - (indexToTest + 1));
				while (currentN + permutationsOfFollowingChars < perm)
				{
					currentN += permutationsOfFollowingChars;
					index++;
				}

				resultPerm += availableChars[index];
				availableChars.RemoveAt(index);
			}

			return resultPerm;
		}
	}
}