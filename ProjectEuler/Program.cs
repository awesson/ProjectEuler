using System;
using System.Diagnostics;
using System.Numerics;
using System.Linq;
using ProjectEuler.Trees;
using ProjectEuler.Extensions;

namespace ProjectEuler
{
	class Program
	{
		static void Main(string[] args)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();

			Print(DateTimeExtensions.NumOccurancesOfDayOnMonthDate(1901, 2000, 1, DayOfWeek.Sunday));

			Print(BasicMath.SumOfDigits(BasicMath.Factorial(100)));

			sw.Stop();

			Console.WriteLine("Elapsed={0}", sw.Elapsed);
		}

		static void Print<T>(T value)
		{
			Console.WriteLine(value);
		}
	}
}
