using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalInfoClient.PersonalInforamtionServiceReference;

namespace PersonalInfoClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new PersonalInforamtionServiceClient())
            {
                client.GetCompleted += Client_GetCompleted;

                client.GetAsync(new PersonModelRequest
                {
                    PersonId = 23
                });

                Console.WriteLine("Starting....");
                var result = client.GetAll();

                Console.WriteLine("Get response from normal operation contract");
                foreach (var item in client.GetAll().People)
                {
                    Console.WriteLine($"Id => {item.Id}; Name => {item.Name}; Gender => {item.GenderType}");
                }
            }

            Console.ReadKey();
        }

        private static void Client_GetCompleted(object sender, GetCompletedEventArgs e)
        {
            Console.WriteLine("Get response from async operation contract");
            Console.WriteLine(e.Result.Name);
        }
    }
}
