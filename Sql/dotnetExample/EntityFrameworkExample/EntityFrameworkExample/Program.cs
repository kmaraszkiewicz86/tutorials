using System;
using System.Collections.Generic;
using EntityFrameworkExample.Core;
using EntityFrameworkExample.Factories;

namespace EntityFrameworkExample
{
    class Program
    {
        static IEnumerable<IJoinServiceFactory> Services;

        static void GenerateListOfClasses()
        {
            Services = new List<IJoinServiceFactory>
            {
                new InnerJoinServiceFactory(),
                new LeftJoinServiceFactory(),
                new RightJoinServiceFactory(),
                new FullJoinServiceFactory(),
                new GroupByServiceFactory()
            };
        }

        static void Main(string[] args)
        {
            using (var db = new AppDbContextFactory().CreateDbContext(new string[] { }))
            {
                GenerateListOfClasses();

                foreach(var service in Services)
                {
                    var client = service.CreateBaseJoinService(db);

                    Console.WriteLine("=====================================");
                    Console.WriteLine($"Running {client.GetType().FullName}");
                    Console.WriteLine("=====================================");

                    client.Query();
                    client.Print();

                    Console.WriteLine("=====================================");
                }
            }
        }

        
    }
}
