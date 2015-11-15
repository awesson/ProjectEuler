using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Extensions;

namespace ProjectEuler.Geometry
{
	public class Paths
	{
		public class Grid2D
		{
			public int Width { get; set; }
			public int Height { get; set; }

			public override bool Equals(System.Object obj)
			{
				var other = obj as Grid2D;
				return Equals(other);
			}

			public bool Equals(Grid2D other)
			{
				// If parameter is null return false:
				if ((object)other == null)
				{
					return false;
				}

				return ((Width == other.Width) && (Height == other.Height))
					|| ((Width == other.Height) && (Height == other.Width));
			}

			public override int GetHashCode()
			{
				return (Width + Height) ^ (Width * Height);
			}
		}

		/// <summary>
		///		Returns the number of unique paths along the lattice lines of a 2d grid,
		///		being able to only move down or to the right at each point.
		/// </summary>
		/// <param name="width">Width of the 2d grid.</param>
		/// <param name="height">Height of the 2d grid.</param>
		/// <returns>The number of paths through the lattice points of the grid.</returns>
		public static long NumLatticePaths2DDownAndRight(int width, int height)
		{
			Dictionary<Grid2D, long> toCheck = new Dictionary<Grid2D, long>();
			toCheck.Add(new Grid2D { Width = width, Height = height }, 1);

			var pathLength = 0L;
			while (toCheck.Count != 0)
			{
				var gridInfo = toCheck.First();
				var grid = gridInfo.Key;
				var count = gridInfo.Value;

				toCheck.Remove(grid);

				if (grid.Width == 1)
				{
					pathLength += count*(grid.Height + 1);
				}
				else if (grid.Height == 1)
				{
					pathLength += count*(grid.Width + 1);
				}
				else
				{
					var subGrid1 = new Grid2D { Width = grid.Width, Height = grid.Height - 1 };
					var subGrid2 = new Grid2D { Width = grid.Width - 1, Height = grid.Height };

					toCheck.AddOrIncrement<Grid2D>(subGrid1, count, count);
					toCheck.AddOrIncrement<Grid2D>(subGrid2, count, count);
				}
			}

			return pathLength;
		}
	}
}
