using System;
using System.Collections.Generic;
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
                var dogBreederName = "12, 'b'); drop database EntityFrameworkRelationshipsTesting;--";
                var dogName = "Lilo";

                var dogBreeder = new DogBreeder
                {
                    Name = dogBreederName
                };

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

                var dog = new Dog
                {
                    Name = dogName,
                    DogBreeder = dogBreeder,
                    Puppies = new List<Puppy>
                    {
                        new Puppy { Name = "puppy1" },
                        new Puppy { Name = "puppy2" },
                        new Puppy { Name = "puppy3" },
                        new Puppy { Name = "puppy4" },
                        new Puppy { Name = "puppy5" },
                    }
                };

                client.Dogs.Add(dog);

                client.SaveChanges();

                var a = "Rur%";
                var count = client.Dogs.Where(d => EF.Functions.Like(d.Name, a)).Count();

                count = client.Dogs.Where(d => EF.Functions.Like(d.Name, a)).Count();

                Console.WriteLine($"Count => {count}");

                foreach (var dog8 in client.Dogs.Include(d => d.DogBreeder))
                {
                    Console.WriteLine($"{dog8.Name}; {dog8.DogBreeder?.Name ?? "Empty"}");

                    if (dog8.Puppies != null)
                    {
                        Console.WriteLine("List of puppies:");

                        foreach (var puppy in dog8.Puppies)
                        {
                            Console.WriteLine(puppy.Name);
                        }
                    }

                    Console.WriteLine("==================================");
                }
            }

            var estimateTime = DateTime.Now - startDateTime;
            Console.WriteLine($"Finished with {estimateTime}");
            Console.ReadKey();
        }
    }
}
