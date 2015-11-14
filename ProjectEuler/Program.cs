using System;
using System.Diagnostics;
using System.Numerics;
using System.Linq;

namespace ProjectEuler
{
	class Program
	{
		static void Main(string[] args)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();

			var count = 0;
			for (int i = 1; i <= 1000; ++i)
			{
				var strNum = BasicMath.ToSpokenEnglish(i);
				count += strNum.Count(c => Char.IsLetter(c));
			}

			Console.WriteLine("Count: "+count);

			sw.Stop();

			Console.WriteLine("Elapsed={0}", sw.Elapsed);
		}
	}
}
