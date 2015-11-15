using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ProjectEuler.Extensions
{
	public static class StringExtensions
	{
		/// <summary>
		///		Attempts to convert the string to the given value type.
		///		Returns true if it succeeds and false otherwise.
		/// </summary>
		public static bool TryConvert<T>(this string input, ref T value)
		{
			var converter = TypeDescriptor.GetConverter(typeof(T));
			if (converter != null)
			{
				try
				{
					value = (T)converter.ConvertFromString(input);
					return true;
				}
				catch
				{
					return false;
				}
			}

			return false;
		}

		/// <summary>
		///		Converts the string to the given value type.
		///		Will throw an exception if the string cannot be converted.
		///		Use TryConvert() if you do not want the possibility of exceptions.
		/// </summary>
		public static T Convert<T>(this string input)
		{
			var converter = TypeDescriptor.GetConverter(typeof(T));
			if (converter != null)
			{
				return (T)converter.ConvertFromString(input);
			}

			return default(T);
		}
	}
}
