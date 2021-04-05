using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Messages;
using static Messages.SayHelloService;

namespace GrpcServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var port = 50000;

            Server server = new Server
            {
                Ports = { new ServerPort("localhost", port, ServerCredentials.Insecure) },
                Services = { BindService(new SayHelloService()) }
            };

            server.Start();

            Console.WriteLine($"Starting server on port {port}");
            Console.WriteLine("Press any key to stop...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }

        public class SayHelloService : SayHelloServiceBase
        {
            public override async Task<HelloResponse> SayHello(HelloRequest request, ServerCallContext context)
            {
                Metadata md = context.RequestHeaders;

                foreach (var entry in md)
                {
                    Console.WriteLine($"{entry.Key}: {entry.Value}");
                }

                return new HelloResponse();
            }
        }

    }
}
