using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace WcfClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var service0 = new ServiceReference1.PersonServiceContractClient();

            var personModels = service0.GetAll();

            foreach (var personModel in personModels)
            {
                Console.WriteLine($"Id => {personModel.Id}; " +
                              $"Name => {personModel.Name}; " +
                              $"Surname => {personModel.Surname}" +
                              $"Gender => {personModel.GenderType}");
            }


            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    var service = new ServiceReference1.PersonServiceContractClient();
                    

                    service.Insert(new ServiceReference1.PersonModel
                    {
                        Name = "test1",
                        Surname = "test1",
                        GenderType = ServiceReference1.GenderType.Male
                    });

                    throw new Exception();

                    var service1 = new ServiceReference2.PersonServiceContractClient();

                    service1.Insert(new ServiceReference2.PersonModel
                    {
                        Name = "test2",
                        Surname = "test2",
                        GenderType = ServiceReference2.GenderType.Male
                    });

                    scope.Complete();

                }
                catch
                {
                    // ignored
                }
            }

            var service3 = new ServiceReference1.PersonServiceContractClient();

            personModels = service3.GetAll();

            foreach (var personModel in personModels)
            {
                Console.WriteLine($"Id => {personModel.Id}; " +
                                  $"Name => {personModel.Name}; " +
                                  $"Surname => {personModel.Surname}" +
                                  $"Gender => {personModel.GenderType}");
            }

            Console.ReadKey();
        }
    }
}
