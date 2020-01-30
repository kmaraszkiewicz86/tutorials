using System;
using System.Linq;
using SortTutorial.Enums;

namespace SortTutorial
{
	class Program
	{
		static void Main(string[] args)
		{
			var random = new Random();
			var tableLength = random.Next(5, 20);

			Console.WriteLine($"Initialization table with {tableLength} items");

			int[] table = new int[tableLength];
			Console.WriteLine("Randomize value of elements in table");

			for (int i = 0; i < tableLength; i++)
			{
				table[i] = random.Next(-20, 20);
			}

			DoWork(table);

			Console.WriteLine("Sorting strings");

			var tableStrings = new string[]
			{
				"albinos",
				"start",
				"work",
				"job",
				"entity",
				"shop",
				"fish",
				"horse",
				"man",
				"money",
				"war",
				"peace"
			};

			DoWork(tableStrings);

			Console.Write("Click any button to continue");
			Console.ReadKey();
		}

		private static void DoWork<TType>(TType[] table) where TType : IComparable
		{
			Console.WriteLine();
			Console.WriteLine("Showing table with random values");
			Console.WriteLine();

			Console.Write(string.Join(" | ", table));

			foreach (SortFactoryType sortFactoryType in Enum.GetValues(typeof(SortFactoryType)))
			{
				var tmpTable = (TType[])table.Clone();

				if (sortFactoryType == SortFactoryType.None)
					continue;

				tmpTable.CreateBaseSortManager(sortFactoryType).Sort(true);

				Console.WriteLine();
				Console.WriteLine("Sorted table");
				Console.WriteLine("===============================");

				Console.Write(string.Join(" | ", tmpTable));
				Console.WriteLine();
			}
		}
	}
}