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

			Pythagorean.Triple ans = new Pythagorean.Triple();
			uint sum = (uint)((1L << 32) - 2u);
			while(!Pythagorean.PythagoreanUtils.TripleSum(sum, ref ans))
			{
				sum -= 2;
			}
			Console.WriteLine(ans);
			Console.WriteLine(String.Format("Sum = {0}", ans.Sum()));

			sw.Stop();

			Console.WriteLine("Elapsed={0}", sw.Elapsed);
		}
	}
}
