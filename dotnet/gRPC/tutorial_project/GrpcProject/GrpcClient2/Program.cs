using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Messages;
using static Messages.SayHelloService;

namespace GrpcClient2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var channel = GrpcChannel.ForAddress("https://localhost:50000"))
            {
                var client = new SayHelloServiceClient(channel);
                var reply = await client.SayHelloAsync(new HelloRequest() { Name = "Name" });

                Console.WriteLine("Press any key to finish...");
                Console.ReadKey();
            }
        }
    }
}
