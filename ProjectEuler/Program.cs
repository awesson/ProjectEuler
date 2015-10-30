using System;
using System.Diagnostics;

namespace ProjectEuler
{
	class Program
	{
		static void Main(string[] args)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();

			Console.WriteLine(Primes.LargestPrimeFactor(600851475143));

			sw.Stop();

			Console.WriteLine("Elapsed={0}", sw.Elapsed);
		}
	}
}
