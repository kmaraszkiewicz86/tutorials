using System;
using BookReader.JsonReader;

namespace BookReader
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKey keyPressed;
            var pageReader = new PagesReader();
            pageReader.Read();
            var pageNumber = pageReader.GetNextPageNumber();

            do
            {
                Console.Clear();

                var pageModel = pageReader.GetPage(pageNumber);

                Console.Write("{0,50}", "-");
                Console.Write(pageModel.PageNumber);
                Console.Write("-");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine(pageModel.Content);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("P - Poprzedni");
                Console.Write("{0,100}", "N - Nastepny");

                Console.WriteLine();
                keyPressed = Console.ReadKey().Key;

                switch (keyPressed)
                {
                    case ConsoleKey.N:
                        pageNumber = pageReader.GetNextPageNumber();
                        break;

                    case ConsoleKey.P:
                        pageNumber = pageReader.GetPrevPageNumber();
                        break;
                }


            } while (keyPressed != ConsoleKey.Enter);

            Console.ReadKey();

        }
    }
}