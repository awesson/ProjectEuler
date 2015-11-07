using System;
using System.Diagnostics;
using System.Numerics;

namespace ProjectEuler
{
	class Program
	{
		static void Main(string[] args)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();

			var numArr = BasicMath.ParseFileIntoBigInts(@"..\..\100-50_digit_nums.txt");

			BigInteger sum = 0;
			foreach (var bigNum in numArr)
			{
				sum += bigNum;
			}

			Console.WriteLine(sum);

			sw.Stop();

			Console.WriteLine("Elapsed={0}", sw.Elapsed);
		}
	}
}
