using System;
using System.Diagnostics;
using System.Numerics;
using System.Linq;
using ProjectEuler.Trees;

namespace ProjectEuler
{
	class Program
	{
		static void Main(string[] args)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();

			Console.WriteLine(Series.LargestSumTopToBottomInBinaryTree(@"D:\Development\ProjectEuler\ProjectEuler\p067_triangle.txt"));

			Print(BasicMath.IsPalindrome(long.MinValue));

			sw.Stop();

			Console.WriteLine("Elapsed={0}", sw.Elapsed);
		}

		static void Print<T>(T value)
		{
			Console.WriteLine(value);
		}
	}
}
