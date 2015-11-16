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

			sw.Stop();

			Console.WriteLine("Elapsed={0}", sw.Elapsed);
		}
	}
}
