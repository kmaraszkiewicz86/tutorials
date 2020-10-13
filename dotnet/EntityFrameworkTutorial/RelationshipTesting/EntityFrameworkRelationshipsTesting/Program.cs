using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EntityFrameworkRelationshipsTesting.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkRelationshipsTesting
{
    class Program
    {
        static void Main(string[] args)
        {

            var appDbContextDesign = new AppDbContextDesign();

            var startDateTime = DateTime.Now;

            using (var client = appDbContextDesign.CreateDbContext(null))
            {
                //var dogBreederName = "12, 'b'); drop database EntityFrameworkRelationshipsTesting;--";
                //var dogName = "Lilo";

                //var dogBreeder = new DogBreeder
                //{
                //    Name = dogBreederName
                //};

                //var dogs = new List<Dog>
                //{
                //    new Dog
                //    {
                //        Name = "Mailo",
                //        DogBreeder = poppy
                //    },
                //    new Dog
                //    {
                //        Name = "Lilo",
                //        DogBreeder = poppy
                //    },
                //    new Dog
                //    {
                //        Name = "Krowiej",
                //        DogBreeder = poppy
                //    },
                //    new Dog
                //    {
                //        Name = "Kurwiej",
                //        DogBreeder = poppy
                //    },
                //};

                //var dog = new Dog
                //{
                //    Name = dogName,
                //    DogBreeder = dogBreeder,
                //    Puppies = new List<Puppy>
                //    {
                //        new Puppy { Name = "puppy1" },
                //        new Puppy { Name = "puppy2" },
                //        new Puppy { Name = "puppy3" },
                //        new Puppy { Name = "puppy4" },
                //        new Puppy { Name = "puppy5" },
                //    }
                //};

                //client.Dogs.Add(dog);

                //client.SaveChanges();

                //var a = "Rur%";
                //var count = client.Dogs.Where(d => EF.Functions.Like(d.Name, a)).Count();

                //count = client.Dogs.Where(d => EF.Functions.Like(d.Name, a)).Count();

                //Console.WriteLine($"Count => {count}");

                //foreach (var dog8 in client.Dogs.Include(d => d.DogBreeder))
                //{
                //    Console.WriteLine($"{dog8.Name}; {dog8.DogBreeder?.Name ?? "Empty"}");

                //    if (dog8.Puppies != null)
                //    {
                //        Console.WriteLine("List of puppies:");

                //        foreach (var puppy in dog8.Puppies)
                //        {
                //            Console.WriteLine(puppy.Name);
                //        }
                //    }

                //    Console.WriteLine("==================================");
                //}

                //var dogData = client.Dogs.Select(d => new TestModel
                //{
                //    DogName = d.Name,
                //    FirstPuppyName = d.Puppies.First().Name,
                //    Puppies = d.Puppies,
                //    PuppiesWithWhere = d.Puppies.Where(p => p.Name.EndsWith("1")),
                //    PuppyCount = d.Puppies.Count
                //}).ToList();

                //dogData[0].DogName += " testing";

                //Console.WriteLine(dogData.First().DogName);

                

                try
                {
                    client.Database.GetDbConnection().Open();

                    using (var command = client.Database.GetDbConnection().CreateCommand())
                    {
                        command.CommandText = "[dbo].[GetFirstDogPuppy]";
                        command.CommandType = CommandType.StoredProcedure;

                        var dogNameParameter = command.CreateParameter();
                        dogNameParameter.ParameterName = "@DogId";
                        dogNameParameter.DbType = DbType.Int32;
                        dogNameParameter.Value = 1;

                        command.Parameters.Add(dogNameParameter);

                        var puppyName = (string)command.ExecuteScalar();

                        Console.WriteLine(puppyName);
                    }
                }
                finally
                {
                    client.Database.GetDbConnection().Close();
                }
                

                foreach (var item in client.GetPupiesWithParentDogs.ToList())
                {
                    Console.WriteLine($"ParentDogName: {item.ParentDogName}; PuppyName: {item.PuppyName}");
                }
            }

            var estimateTime = DateTime.Now - startDateTime;
            Console.WriteLine($"Finished with {estimateTime}");
            Console.ReadKey();
        }
    }

    class TestModel
    {
        public string DogName { get; set; }

        public string FirstPuppyName { get; set; }

        public IEnumerable<Puppy> Puppies { get; set; }

        public IEnumerable<Puppy> PuppiesWithWhere { get; set; }

        public int PuppyCount { get; set; }
    }
}
