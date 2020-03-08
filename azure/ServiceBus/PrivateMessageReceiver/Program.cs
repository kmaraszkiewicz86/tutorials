using System;
using Common.Core;

namespace PrivateMessageReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceBusQueueService serviceBusQueueServiceBus = null;

            try
            {
                serviceBusQueueServiceBus = new ServiceBusQueueService("servicebus_queue209");
                serviceBusQueueServiceBus.ReciveMessages().GetAwaiter().GetResult();
            }
            catch (Exception exception)
            {
                Console.Write(exception.Message);
            }
            finally
            {
                serviceBusQueueServiceBus?.Close().GetAwaiter().GetResult();
            }
        }
    }
}
