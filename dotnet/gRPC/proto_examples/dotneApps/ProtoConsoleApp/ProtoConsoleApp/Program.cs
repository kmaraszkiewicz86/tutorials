using System;
using System.Collections.Generic;
using Dogdata;
using Google.Protobuf.Collections;

namespace ProtoConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var customer = new Customer()
            {
                Username = "test",
                Type = CustomType.Dog,
                Name = "Name"
            };

            customer.EmailAddresses.AddRange(new List<Address>
            {
                new Address
                {
                    Street = "Street 1",
                    City = "Poznan"
                }
            });

            customer.AnimalFood.Add(0, "Szarlotka");
            customer.AnimalFood.Add(1, "Chrupki");
            customer.AnimalFood.Add(2, "Chrupki");

            Console.WriteLine(customer.ToString());
        }
    }
}
