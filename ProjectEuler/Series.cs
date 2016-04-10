using System;
using System.Collections.Generic;
using System.Linq;
using ProjectEuler.Trees;

namespace ProjectEuler
{
	public static class Series
	{
		/// <summary>
		///     Calculates the sum of all multiples of <paramref name="mult" />
		///     less than or equal to <paramref name="value" />.
		///     The sign of <paramref name="value" /> determines the sign of the return value.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">
		///     If <paramref name="mult" /> is less than 0
		///     or value is equal to long.MinValue.
		/// </exception>
		public static long SumUpToVMultM(long value, long mult)
		{
			if (mult <= 0)
			{
				throw new ArgumentOutOfRangeException("mult must be strictly positive.");
			}

			if (value == long.MinValue)
			{
				throw new ArgumentOutOfRangeException("value cannot be long.MinValue");
			}

			var sign = 1L;
			if (value < 0)
			{
				sign = -1L;
				value = -value;
			}

			// We know 1 + 2 + 3 + ... + N = (1 + N)N/2
			// If instead of 1s we want to go by multiples of mult,
			// mult + 2mult + 3mult + ... + Nmult = mult(1 + N)N/2
			// where N in this case is the number of times mult goes into value.
			var N = value / mult;
			return sign * (mult * (N + 1L) * N) / 2L;
		}

		/// <summary>
		///     Calculates the sum of all numbers less than and NOT including value which are
		///     multiples of either <paramref name="multiple1" /> or <paramref name="multiple2" />.
		/// </summary>
		public static long SumOfMultiplesLessThan(long value, long multiple1, long multiple2)
		{
			if (value == 0)
			{
				return 0;
			}

			// We don't include the value as a multiple
			value = BasicMath.ChangeAbsoluteValue(value, -1);

			var firstSum = SumUpToVMultM(value, multiple1);
			var secondSum = SumUpToVMultM(value, multiple2);
			var overlapOfSums = SumUpToVMultM(value, multiple1 * multiple2);
			return firstSum + secondSum - overlapOfSums;
		}

		/// <summary>
		///     Returns the largest product of N consecutive digits in the given number, represented as a string.
		/// </summary>
		/// <param name="num">The string formatted number containing the digits to search.</param>
		/// <returns>The largest product.</returns>
		public static long LargestNProduct(int N, string num)
		{
			Func<char, int, long> charToLong = (c, _) => (long) char.GetNumericValue(c);
			var arrNum = num.Select(charToLong);
			return LargestNProduct(N, arrNum);
		}

		/// <summary>
		///     Returns the largest product of N consecutive digits in the given enumerable.
		/// </summary>
		/// <param name="num">The string formatted number containing the digits to search.</param>
		/// <returns>The largest product.</returns>
		public static long LargestNProduct(int N, IEnumerable<long> digits)
		{
			long maxProd = -1;
			for (var i = 0; i < digits.Count(); ++i)
			{
				var prod = Product(digits.Skip(i).Take(N));
				if (prod > maxProd)
				{
					maxProd = prod;
				}
			}

			return maxProd;
		}

		public static long Product(IEnumerable<long> nums)
		{
			return nums.Aggregate((prod, num) => prod * num);
		}

		public static long SumModN(IEnumerable<long> nums, long mod)
		{
			return nums.Aggregate((sum, num) => (sum + (num % mod)) % mod);
		}

		/// <summary>
		///     Returns the numbers in a Collatz Sequence, where the next number in the sequence is
		///     n/2 if it is currently even, or 3n + 1 if it is currently odd.
		/// </summary>
		/// <param name="num">The starting number in the sequence.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		///     If <paramref name="num" /> is 0 or negative.
		///     The Collatz Sequence is only defined for positive integers.
		/// </exception>
		public static IEnumerable<long> CollatzSequence(long num)
		{
			if (num <= 0)
			{
				throw new ArgumentOutOfRangeException("num must be greater than 0");
			}

			while (num != 1)
			{
				yield return num;

				if (BasicMath.IsEven(num))
				{
					num >>= 1;
				}
				else
				{
					num = 3 * num + 1;
				}
			}

			yield return num;
		}

		/// <summary>
		///     Finds and returns the longest Collatz Sequence that starts with
		///     any number less than the given number.
		/// </summary>
		public static long LongestCollatzSequence(long maxStartNum)
		{
			var longestLen = -1;
			var longestStartingNum = 0L;
			// Since Len(sequence(n)) = Len(sequence(2n)) - 1, we can double every starting number less than half of the max
			// and get a new starting number that would have a longer sequence. If after doubling,
			// it is still less than half, it can be doubled again and the new number would have a
			// longer sequence and is guaranteed to be less than maxStartingNum (since it was less than half).
			// Therefore all numbers less than half of the maxStartNum necessarily have shorter sequences than
			// all even numbers greater than half maxStartNum.
			// N => even
			// N - 1 !/ 3
			// 2*N >? max
			// (2*max - 1) / 3 = 2*max/3 - 1/3 = M < max
			for (var i = maxStartNum / 2; i < maxStartNum; ++i)
			{
				var len = CollatzSequence(i).Count();
				if (len > longestLen)
				{
					longestLen = len;
					longestStartingNum = i;
				}
			}

			return longestStartingNum;
		}

		/// <summary>
		///     Calculates and returns the maximum sum of all nodes,
		///     starting from the root and going through only 1 child.
		///     For example, the max sum in the following tree is 23:
		///     3
		///     7 4
		///     2 4 6
		///     8 5 9 3
		///     By going through the nodes, 3, 7, 4, 9.
		/// </summary>
		/// <param name="treeFile">The file to load the interlaced binary tree from.</param>
		/// <returns>The largest top to bottom sum in the tree.</returns>
		public static long LargestSumTopToBottomInBinaryTree(string treeFile)
		{
			var tree = BinaryTree<long>.LoadInterlacedBinaryTree(treeFile, ' ');
			// Recursion depth is the height of the tree.
			// Since arguments are passes by reference, each stack is 128 bits.
			// Assuming only 1GB of RAM, this can solve for a tree a height of 62,500,000.
			return LargestSumTopToBottomInBinaryTreeHelper(tree, new Dictionary<BinaryTree<long>, long>());
		}

		private static long LargestSumTopToBottomInBinaryTreeHelper(BinaryTree<long> tree,
		                                                            Dictionary<BinaryTree<long>, long> cachedMaxSums)
		{
			// Memoize the results for sub-trees.
			if (cachedMaxSums.ContainsKey(tree))
			{
				return cachedMaxSums[tree];
			}

			// The max sum of this tree is it's value plus the max, max sum of it's children sub-trees.
			var maxSum = tree.Value;
			if (!tree.IsLeaf)
			{
				maxSum += Math.Max(LargestSumTopToBottomInBinaryTreeHelper(tree.Left, cachedMaxSums),
				                   LargestSumTopToBottomInBinaryTreeHelper(tree.Right, cachedMaxSums));
			}

			// Memoize the results for sub-trees.
			cachedMaxSums[tree] = maxSum;

			return maxSum;
		}
	}
}