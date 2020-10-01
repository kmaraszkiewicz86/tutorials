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

            using (var client = appDbContextDesign.CreateDbContext(null))
            {
                var dogBreeder = new DogBreeder
                {
                    Name = "poppy"
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
                    Name = "Rurwiej",
                    DogBreeder = dogBreeder
                };

                client.AddRange(dog, dogBreeder);

                client.SaveChanges();

                var a = "Rur%";
                var count = client.Dogs.Where(d => EF.Functions.Like(d.Name, a)).Count();

                count = client.Dogs.Where(d => EF.Functions.Like(d.Name, a)).Count();

                Console.WriteLine($"Count => {count}");

                foreach (var dog8 in client.Dogs)
                {
                    Console.WriteLine(dog8.Name);
                }
            }


            Console.ReadKey();
        }
    }
}
