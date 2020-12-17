﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SwapNodesAlgo
{
	//https://www.hackerrank.com/challenges/swap-nodes-algo/problem
	class Solution
    {
        class Node
		{
            public int Index { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Depth { get; set; }

            public override string ToString()
			{
                return $"Index: {Index} - Left: {Left?.Index} - Right: {Right?.Index}";
			}

            public Node(int index, int depth)
			{
                Index = index;
                Depth = depth;
			}
        }

        static void BuildTreeNodes(Node currentNode, int[][] indexes)
        {
            var childrenIndexes = indexes[currentNode.Index - 1];

            if (childrenIndexes[0] != -1)
            {
                var newNode = new Node(childrenIndexes[0], currentNode.Depth + 1);
                currentNode.Left = newNode;
                BuildTreeNodes(newNode, indexes);
            }

            if (childrenIndexes[1] != -1)
            {
                var newNode = new Node(childrenIndexes[1], currentNode.Depth + 1);
                currentNode.Right = newNode;
                BuildTreeNodes(newNode, indexes);
            }
        }

        static int TraverseSwapping(Node node, int[] result, int swappingK, int resultPosition)
        {
            if (node.Depth % swappingK == 0)
            {
                var auxNode = node.Left;
                node.Left = node.Right;
                node.Right = auxNode;
            }

            if (node.Left != null)
            {
                resultPosition = TraverseSwapping(node.Left, result, swappingK, resultPosition);
            }

            result[resultPosition] = node.Index;
            resultPosition++;

            if (node.Right != null)
            {
                resultPosition = TraverseSwapping(node.Right, result, swappingK, resultPosition);
            }

            return resultPosition;
        }

        /*
         * Complete the swapNodes function below.
         */
        static int[][] swapNodes(int[][] indexes, int[] queries)
        {
            //insert root node
            var tree = new Node(1, 1);

            BuildTreeNodes(tree, indexes);

            int[][] result = new int[queries.Length][];

			for (int i = 0; i < queries.Length; i++)
			{
                result[i] = new int[indexes.Length];

                TraverseSwapping(tree, result[i], queries[i], 0);
			}

            return result;

            /*
11
2 3
4 -1
5 -1
6 -1
7 8
-1 9
-1 -1
10 11
-1 -1
-1 -1
-1 -1
2
2
4

             */
            //return new int[][]{ new int[] { 2, 9, 6, 4, 1, 3, 7, 5, 11, 8, 10 }, new int[] { 2, 6, 9, 4, 1, 3, 7, 5, 10, 8, 11 } };
        }

        static void Main(string[] args)
        {
            TextWriter textWriter = new StreamWriter(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\result", false);

            int n = Convert.ToInt32(Console.ReadLine());

            int[][] indexes = new int[n][];

            for (int indexesRowItr = 0; indexesRowItr < n; indexesRowItr++)
            {
                indexes[indexesRowItr] = Array.ConvertAll(Console.ReadLine().Split(' '), indexesTemp => Convert.ToInt32(indexesTemp));
            }

            int queriesCount = Convert.ToInt32(Console.ReadLine());

            int[] queries = new int[queriesCount];

            for (int queriesItr = 0; queriesItr < queriesCount; queriesItr++)
            {
                int queriesItem = Convert.ToInt32(Console.ReadLine());
                queries[queriesItr] = queriesItem;
            }

            int[][] result = swapNodes(indexes, queries);

            textWriter.WriteLine(String.Join("\n", result.Select(x => String.Join(" ", x))));

            textWriter.Flush();
            textWriter.Close();
        }
    }
}
