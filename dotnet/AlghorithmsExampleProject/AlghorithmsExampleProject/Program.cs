using System;
using System.Collections.Generic;
using System.Linq;
using AlghorithmsExampleProject.AlghoritmsTypes.TreeImpl;
using static AlghorithmsExampleProject.AlghoritmsTypes.OtherType;
using static AlghorithmsExampleProject.AlghoritmsTypes.SearchType;
using static AlghorithmsExampleProject.AlghoritmsTypes.SortType;

namespace AlghorithmsExampleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine();
            ShowResult($"SmallestSharedDivider -> {SmallestSharedDivider(28, 24)}");
            ShowResult($"FibonacciAlorithmWithForLoopReturnArray -> {String.Join(',', FibonacciAlorithmWithForLoopReturnArray(5))}");
            ShowResult($"FibonacciAlorithmWithRecursionReturnArray -> {String.Join(',', FibonacciAlorithmWithRecursionReturnArray(5))}");

            ShowResult($"FibonacciAlorithm -> {String.Join(',', FibonacciAlorithm(5))}");
            ShowResult($"FibonacciAlorithmRecursion -> {String.Join(',', FibonacciAlorithmRecursion(5))}");


            ShowResult($"LinearSearch -> {String.Join(',', LinearSearch(20))}");
            ShowResult($"BinarySearch -> {String.Join(',', BinarySearch(20))}");
            ShowResult($"InsertionSort -> {String.Join(',', InsertionSort(20))}");
            ShowResult($"BubbleSort -> {String.Join(',', BubbleSort(20))}");
            ShowResult($"QuickSort -> {String.Join(',', QuickSort(20))}");

            Console.WriteLine("---------------------Tree impl-------------------------------");


            var tree = new Tree<int>
            {
                Root = new TreeNode<int>
                {
                    Data = 10
                }
            };

            tree.Root.AddChildren(new List<TreeNode<int>>(new[] {
                new TreeNode<int>
                {
                    Data = 23
                },
                new TreeNode<int>
                {
                    Data = 230
                },
                new TreeNode<int>
                {
                    Data = 11
                },
                new TreeNode<int>
                {
                    Data = 2
                }
            }));

            tree.Root.Children[2].AddChildren(new[]
            {
                new TreeNode<int>
                {
                    Data = 123
                },
                new TreeNode<int>
                {
                    Data = 345
                }
            });
            


            Console.WriteLine("---------------------Tree impl-------------------------------");
        }

        static void ShowResult(string resultString)
        {
            Console.WriteLine(resultString);
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine();
        }
    }
}