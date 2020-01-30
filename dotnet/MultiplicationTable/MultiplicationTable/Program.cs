using System;

namespace MultiplicationTable
{
	class Program
	{
		static void Main(string[] args)
		{
			int[,] table = new int[10, 10];

			for (var index = 0; index < 10; index++)
			{
				for (int i = 0; i < 10; i++)
				{
					table[index,i] = (index + 1) * (i + 1);
				}
			}

			for (var index = 0; index < 10; index++)
			{
				for (int i = 0; i < 10; i++)
				{
					Console.Write("{0,4}", table[index,i]);
				}
				Console.WriteLine();
			}

			Console.ReadKey();
		}
	}
}