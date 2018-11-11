using System;

namespace ProjectEuler.Extensions
{
	public static class ArrayExtentions
	{
		public static void Swap<T>(this T[] arr, int index1, int index2)
		{
			var temp = arr[index1];
			arr[index1] = arr[index2];
			arr[index2] = arr[index1];
		}
	}
}