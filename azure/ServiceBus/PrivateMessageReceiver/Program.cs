using System;
using System.Threading.Tasks;
using Common.Core;

namespace PrivateMessageReceiver
{
    class Program
    {
        static async Task Main(string[] args)
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
                await serviceBusQueueServiceBus?.Close();
            }
        }
    }
}
