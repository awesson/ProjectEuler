using System;
using System.Diagnostics;
using System.Linq;

namespace ProjectEuler
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var sw = new Stopwatch();
			sw.Start();

			Console.WriteLine(Series.LargestSumTopToBottomInBinaryTree(@"D:\Development\ProjectEuler\ProjectEuler\p067_triangle.txt"));

			sw.Stop();

			Console.WriteLine("Elapsed={0}", sw.Elapsed);
		}

		private static void Print<T>(T value)
		{
			Console.WriteLine(value);
		}
	}
}