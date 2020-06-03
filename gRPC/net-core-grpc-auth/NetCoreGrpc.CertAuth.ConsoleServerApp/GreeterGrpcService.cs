﻿using System.Threading.Tasks;
using Grpc.Core;
using NetCoreGrpc.CertAuth.Proto;

namespace NetCoreGrpc.CertAuth.ConsoleServerApp
{
    internal class GreeterService : Greeter.GreeterBase
    {
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply { Message = "Hello " + request.Name });
        }
    }
}
