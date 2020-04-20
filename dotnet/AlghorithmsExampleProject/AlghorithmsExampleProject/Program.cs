using System;
using System.Collections.Generic;
using AlghorithmsExampleProject.AlghoritmsTypes;
using AlghorithmsExampleProject.AlghoritmsTypes.TreeImpl;
using AlghorithmsExampleProject.Helpers;
using static AlghorithmsExampleProject.AlghoritmsTypes.OtherType;
using static AlghorithmsExampleProject.AlghoritmsTypes.SearchType;
using static AlghorithmsExampleProject.AlghoritmsTypes.SortType;

namespace AlghorithmsExampleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            SearchType.ArrayHelper = new ArrayHelper();
            SortType.ArrayHelper = new ArrayHelper();

            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine();
            ShowResult($"SmallestSharedDivider -> {SmallestSharedDivider(28, 24)}");
            ShowResult($"FibonacciAlorithmWithForLoopReturnArray -> {String.Join(',', FibonacciAlorithmWithForLoopReturnArray(5))}");
            ShowResult($"FibonacciAlorithmWithRecursionReturnArray -> {String.Join(',', FibonacciAlorithmWithRecursionReturnArray(5))}");

            ShowResult($"FibonacciAlorithm -> {String.Join(',', FibonacciAlorithm(5))}");
            ShowResult($"FibonacciAlorithmRecursion -> {String.Join(',', FibonacciAlorithmRecursion(5))}");


            ShowResult($"HashSet Test -> {String.Join(',', HastableTest())}");
            ShowResult($"Stack Test -> {String.Join(' ', StackTest("Dragon ball"))}");
            ShowResult($"Stack Test -> {String.Join(' ', QueueTest("Dragon ball"))}");


            ShowResult($"LinearSearch -> {String.Join(',', LinearSearch(20))}");
            ShowResult($"BinarySearch -> {String.Join(',', BinarySearchRecursion(20))}");
            ShowResult($"BinarySearchStandard -> {String.Join(',', BinarySearchStandard(20))}");
            ShowResult($"InsertionSort -> {String.Join(',', InsertionSort(20))}");
            ShowResult($"BubbleSort -> {String.Join(',', BubbleSort(20))}");
            ShowResult($"QuickSort -> {String.Join(',', QuickSort(20))}");

            TreeNodeTes();
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