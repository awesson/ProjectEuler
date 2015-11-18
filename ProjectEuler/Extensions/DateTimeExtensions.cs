using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler.Extensions
{
	public static class DateTimeExtensions
	{
		/// <summary>
		///		Returns the number of times the given day of the week occurred
		///		on a specific month day during the years given. For example:
		///		The number of times Sunday was the 1st of the month between 1901 and 2000.
		/// </summary>
		public static int NumOccurancesOfDayOnMonthDate(int yearFrom, int yearTo, int date, DayOfWeek day)
		{
			int days = 0;
			for (int year = yearFrom; year <= yearTo; ++year)
			{
				for (int month = 1; month <= 12; ++month)
				{
					if ((new DateTime(year, month, date)).DayOfWeek == day)
					{
						days++;
					}
				}
			}

			return days;
		}
	}
}
