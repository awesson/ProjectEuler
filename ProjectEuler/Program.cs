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

			var n = 0;
			Fibonacci.FibonacciNums().First(x => { ++n; return BasicMath.NumDigits(x) >= 1000; });

			sw.Stop();

			Print(n);

			Console.WriteLine("Elapsed={0}", sw.Elapsed);
		}

		private static void Print<T>(T value)
		{
			Console.WriteLine(value);
		}
	}
}