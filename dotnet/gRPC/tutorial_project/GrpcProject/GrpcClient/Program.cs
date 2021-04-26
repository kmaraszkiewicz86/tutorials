using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Messages;
using static Messages.SayHelloService;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var port = 50000;

            var channel = new Channel($"127.0.0.1:{port}", ChannelCredentials.Insecure);
            var client = new SayHelloServiceClient(channel);

            var reply = await client.SayHelloAsync(new HelloRequest() { Name = "Name" });

            Console.WriteLine("Press any key to finish...");
            Console.ReadKey();
        }
    }
}
