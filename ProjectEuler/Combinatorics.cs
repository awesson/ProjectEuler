using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ProjectEuler
{
	public static class Combinatorics
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

		/// <summary>
		///		Determins the number of ways to uniquely combine the given set of values so that they sum to the given total.
		///		Eg. given the valid values [1, 2, 3] and the target total of 4, there are three ways: [1, 1, 2], [1, 3], and [2, 2].
		/// </summary>
		/// <param name="validValues"> An enumerable list of values to use in the sum. </param>
		/// <param name="targetTotal"> The target total which the combinations of valid values should sum to. </param>
		/// <returns> The number of unique combinations. </returns>
		public static int AdditionCombinations(IEnumerable<int> validValues, int targetTotal)
		{
			var sortedValues = (from value in validValues orderby value select value).ToArray();
			return AdditionCombinationsHelper(sortedValues, 0, targetTotal);
		}

		/// <summary>
		///		Helper method called by AdditionCombinations to recursively determine the number of combinations.
		/// </summary>
		/// <param name="sortedValidValues"> The valid values to use in the sum, sorted in ascending order. </param>
		/// <param name="validIndex">
		///		The first index into <paramref name="sortedValidValues"/> which can be used.
		///		Any previous values are restricted from use in this call.
		/// </param>
		/// <param name="targetTotal"> The target total which the combinations of valid values should sum to. </param>
		/// <returns> The number of unique combinations. </returns>
		private static int AdditionCombinationsHelper(int[] sortedValidValues, int validIndex, int targetTotal)
		{
			var combinations = 0;
			for (var index = validIndex; index < sortedValidValues.Length; ++index)
			{
				var value = sortedValidValues[index];
				// "Use" this value to sum toward the total. The new total will be what's left over.
				var newTotal = targetTotal - value;
				// If the newTotal is greater than 0, we need to add more to reach the target - recurse
				while (newTotal > 0)
				{
					// Move the valid index forward by one so that we don't reuse a value we already have
					// (which would count duplicate patterns as separate combinations eg. [1 + 2] and [2 + 1])
					combinations += AdditionCombinationsHelper(sortedValidValues, index + 1, newTotal);
					// Instead of allowing the recursive call to re-use the current value in the wrong order,
					// loop here to try combinations with varying multiples of the current value.
					newTotal -= value;
				}

				// If we reached the total exactly (and didn't overshoot) we have chosen a set of values which sum to the target.
				if (newTotal == 0)
				{
					++combinations;
				}
			}
			return combinations;
		}
	}
}
