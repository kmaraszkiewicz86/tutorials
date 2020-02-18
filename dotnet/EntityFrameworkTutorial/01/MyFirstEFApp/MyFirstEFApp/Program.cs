using System;
using MyFirstEFApp.Core;
using MyFirstEFApp.Extensions;

namespace MyFirstEFApp
{
    class Program
    {
        static void Main(string[] args)
        {
            QueryExamples.Execute();

            using (var client = new AppDbContext())
            {
                var books = client.Books.MapBookToDto();

                foreach (var book in books)
                {
                    Console.Write(book.Title);
                }
            }
            
            Console.ReadKey();
        }
    }
}