using System;
using System.Diagnostics;

namespace ProjectEuler
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var sw = new Stopwatch();
			sw.Start();

			var perm = Permutations.NthLexographicPermutation(new char[10] {'4', '1', '2', '3', '0', '5', '6', '7', '8', '9'}, 1000000);

			sw.Stop();

			Print(perm);

			Console.WriteLine("Elapsed={0}", sw.Elapsed);
		}

		private static void Print<T>(T value)
		{
			Console.WriteLine(value);
		}
	}
}