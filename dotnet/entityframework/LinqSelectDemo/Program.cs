using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        using var db = new AppDbContext();

        // Dodaj dane testowe
        if (!db.Customers.Any())
        {
            db.Customers.AddRange(
                new Customer
                {
                    Name = "Anna",
                    Orders = new List<Order>
                    {
                        new Order { Product = "Book" },
                        new Order { Product = "Pen" }
                    }
                },
                new Customer
                {
                    Name = "Tom",
                    Orders = new List<Order>
                    {
                        new Order { Product = "Notebook" }
                    }
                }
            );
            db.SaveChanges();
        }

        Console.WriteLine("SELECT (IEnumerable<List<Order>>):");
        var selectResult = db.Customers
            .Select(c => c.Orders)
            .ToList();

        foreach (var orderList in selectResult)
        {
            Console.WriteLine($"  Kolekcja: {string.Join(", ", orderList.Select(o => o.Product))}");
        }

        Console.WriteLine("\nSELECTMANY (IEnumerable<Order>):");
        var selectManyResult = db.Customers
            .SelectMany(c => c.Orders)
            .ToList();

        foreach (var order in selectManyResult)
        {
            Console.WriteLine($"  Produkt: {order.Product}");
        }
    }
}
