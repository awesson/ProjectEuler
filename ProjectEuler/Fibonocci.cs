using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
	class Fibonocci
	{
		public static long EvenSum(int maxN)
		{
			var prev = 1;
			var cur = 2;
			long evenSum = 0;
			while (cur <= maxN)
			{
				if ((cur & 1) == 0)
				{
					evenSum += cur;
				}

				var next = prev + cur;
				prev = cur;
				cur = next;
			}
			return evenSum;
		}
	}
}
