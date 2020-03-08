using System;
using System.IO;
using System.Threading;
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

                Console.WriteLine(client.IsBlobContainerCreated ? "Created with success" : "Container already exists");

                foreach (var blob in await client.GetBlobs())
                {
                    Console.WriteLine(blob.Name);
                }

                var file = File.OpenRead("./test.txt");

                var name = $"test123{Guid.NewGuid()}";

                await client.Save(file, name);

                Thread.Sleep(2000);

                var stream = await client.Load(name);

                Console.WriteLine(new StreamReader(stream).ReadToEnd());

            }
            catch (Exception exception)
            {
                Console.Write(exception.Message);
            }
            finally
            {
                //await client?.Delete();
            }

        }
    }
}
