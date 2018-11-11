using System;
using System.Collections.Generic;
using System.Numerics;
using ProjectEuler.Extensions;

namespace ProjectEuler
{
	public static class Permutations
	{
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
			var currentN = new BigInteger(0);
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