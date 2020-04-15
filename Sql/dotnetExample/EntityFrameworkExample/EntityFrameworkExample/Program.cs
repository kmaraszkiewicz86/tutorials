using System;
using System.Collections.Generic;
using EntityFrameworkExample.Core;
using EntityFrameworkExample.Factories;
using EntityFrameworkExample.Services;

namespace EntityFrameworkExample
{
    class Program
    {
        static IEnumerable<IJoinService> Services;

        static void GenerateListOfClasses(AppDbContext db)
        {
            Services = new List<IJoinService>
            {
                new InnerJoinServiceFactory().CreateBaseJoinService(db)
            };
        }

        static void Main(string[] args)
        {
            using (var db = new AppDbContextFactory().CreateDbContext(new string[] { }))
            {
                GenerateListOfClasses(db);

                foreach(var service in Services)
                {
                    Console.WriteLine("=====================================");
                    Console.WriteLine($"Running {service.GetType().FullName}");
                    Console.WriteLine("=====================================");

                    service.Query();
                    service.Print();

                    Console.WriteLine("=====================================");
                }
            }
        }

        
    }
}
