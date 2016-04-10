using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ProjectEuler.Extensions;

namespace ProjectEuler.Trees
{
	public class BinaryTree<T>
	{
		public T Value { get; set; }

		public BinaryTree<T> Parent { get; set; }

		public bool IsRoot
		{
			get { return Parent == null; }
		}

		public BinaryTree<T> Left { get; set; }

		public bool HasLeft
		{
			get { return Left != null; }
		}

		public BinaryTree<T> Right { get; set; }

		public bool HasRight
		{
			get { return Right != null; }
		}

		public bool IsLeaf
		{
			get { return !HasLeft && !HasRight; }
		}

		public BinaryTree()
		{
		}

		public BinaryTree(T value)
		{
			Value = value;
		}

		/// <summary>
		///     Creates a binary tree as defined by the given file.
		///     Expects the nodes to be laid out with each line being a level of the tree,
		///     and each node separated by the given delimiter.
		///     In the interlaced binary tree, each interior node has two parents
		///     (and therefore two adjacent nodes share a child).
		/// </summary>
		/// <param name="filename"></param>
		public static BinaryTree<T> LoadInterlacedBinaryTree(string filename, char delimiter = ' ')
		{
			BinaryTree<T> tree = null;

			using (var nodes = new StreamReader(filename))
			{
				string line;
				// Keeps track of the nodes made in the previous level
				// of the tree so that their children can be added.
				List<BinaryTree<T>> prevLevelNodes = null;
				while ((line = nodes.ReadLine()) != null)
				{
					// Parse the line into an IEnumerable of type T
					var levelNodesAsStrings = line.Split(delimiter);
					var levelNodeValues = levelNodesAsStrings.Select(valStr => valStr.Convert<T>());

					// Keep track of the nodes created for the current level of the tree.
					var levelNodes = new List<BinaryTree<T>>();
					var index = 0;
					foreach (var nodeValue in levelNodeValues)
					{
						if (prevLevelNodes == null)
						{
							// This is the first level and root node
							Debug.Assert(levelNodeValues.Count() == 1);
							tree = new BinaryTree<T>(nodeValue);
							levelNodes.Add(tree);
							break;
						}
						// Make a new node for the current value.
						var node = new BinaryTree<T>(nodeValue);
						// Build of the list of nodes being created for this level in the tree.
						levelNodes.Add(node);

						// Set the left and right nodes of the previous
						// level nodes, which should point to this new node.
						if (index < prevLevelNodes.Count)
						{
							prevLevelNodes[index].Left = node;
						}
						if (index > 0)
						{
							prevLevelNodes[index - 1].Right = node;
						}

						++index;
					}

					// Save off the nodes made this iteration for the next iteration.
					prevLevelNodes = levelNodes;
				}
			}

			return tree;
		}

		public BinaryTree<T> SetLeftByValue(T value)
		{
			var node = new BinaryTree<T>(value);
			node.Parent = this;
			Left = new BinaryTree<T>(value);
			return node;
		}

		public BinaryTree<T> SetRightByValue(T value)
		{
			var node = new BinaryTree<T>(value);
			node.Parent = this;
			Right = new BinaryTree<T>(value);
			return node;
		}

		public BinaryTree<T> RemoveLeft()
		{
			var left = Left;
			Left = null;
			return left;
		}

		public BinaryTree<T> RemoveRight()
		{
			var right = Right;
			Right = null;
			return right;
		}
	}
}