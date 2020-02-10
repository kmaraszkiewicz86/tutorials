using System;
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
    }
}