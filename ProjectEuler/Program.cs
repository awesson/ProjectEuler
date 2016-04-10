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

			Primes.NthPrime(1000001);

			sw.Stop();

			Console.WriteLine("Elapsed={0}", sw.Elapsed);
		}

		private static void Print<T>(T value)
		{
			Console.WriteLine(value);
		}
	}
}