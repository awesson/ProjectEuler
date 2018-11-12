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

			var denomWithLongestRepeatLength = -1;
			var longestRepeatLength = -1;
			for (var denom = 2; denom < 1000; ++denom)
			{
				var len = BasicMath.GetUnitFractionRecurringDecimal(denom).Length;
				if (len > longestRepeatLength)
				{
					denomWithLongestRepeatLength = denom;
					longestRepeatLength = len;
				}
			}

			sw.Stop();

			Print(denomWithLongestRepeatLength);

			Console.WriteLine("Elapsed={0}", sw.Elapsed);
		}

		private static void Print<T>(T value)
		{
			Console.WriteLine(value);
		}
	}
}