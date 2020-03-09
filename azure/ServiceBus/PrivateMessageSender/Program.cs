using System;
using System.Threading.Tasks;
using Common.Core;

namespace PrivateMessageSender
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ServiceBusQueueService serviceBusQueueServiceBus = null;

            try
            {
                serviceBusQueueServiceBus = new ServiceBusQueueService("servicebus_queue209");
                await serviceBusQueueServiceBus.Send("To jest testowa waidomosc");
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
