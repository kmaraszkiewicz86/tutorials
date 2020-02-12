using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyFirstEFApp.Models;

namespace MyFirstEFApp.Core
{
    public class Service
    {
        public static void ShowAllBooks()
        {
            using (var db = new AppDbContext())
            {
                foreach (var book in
                    db.Books.AsNoTracking()
                        .Include(a => a.Author)) 
                {
                    var webUrl = book.Author.WebUrl == null
                        ? "- no web URL given -"
                        : book.Author.WebUrl;
                    Console.WriteLine(
                        $"{book.Title} by {book.Author.Name}");
                    Console.WriteLine("     " +
                                      "Published on " +
                                      $"{book.PublishedOn:dd-MMM-yyyy}" +
                                      $". {webUrl}");
                }
            }
        }

        public static void ChangeWebURL()
        {
            Console.Write("New Quantum Networking WebUrl > ");
            var newWebUrl = Console.ReadLine();

            using (var db = new AppDbContext())
            {
                var book = db.Books
                    .Include(a => a.Author)   
                    .Single(b => b.Title == "O tym jednym co sie zesral");
                
                book.Author.WebUrl = newWebUrl;
                db.SaveChanges();
                Console.WriteLine("... SavedChanges called.");
            }
            
            ShowAllBooks();
        }
    }
}