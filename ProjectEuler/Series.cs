using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
	class Series
	{
		public static int SumUpToVMultM(int value, int mult)
		{
			var quotient = value / mult;
			var n = quotient * mult;
			return ((n + mult) * quotient) / 2;
		}
	}
}
