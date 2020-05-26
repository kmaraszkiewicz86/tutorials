using System;
using Grpc.Net.Client;
using gRPCService;

namespace gRpcClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var channel = GrpcChannel.ForAddress("https://localhost:5001"))
            {
                var client = new Greeter.GreeterBase(channel);
            }

        }
    }
}
