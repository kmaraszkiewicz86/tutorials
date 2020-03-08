using System;
using System.Threading.Tasks;
using AzureStorageTutorial.Core;

namespace AzureStorageTutorial
{
    class Program
    {
        static async Task Main(string[] args)
        {
            AzureStorageService client = null;

            try
            {
                client = new AzureStorageService();

                client.Open();
                await client.CreateBlobContainer();

                Console.WriteLine(client.IsBlobContainerCreated ? "Created with success" : "Created process failure");
            }
            catch (Exception exception)
            {
                Console.Write(exception.Message);
            }
            finally
            {
                await client?.Delete();
            }

        }
    }
}
