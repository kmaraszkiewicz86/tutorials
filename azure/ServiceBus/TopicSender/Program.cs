using System;
using Common.Core;

namespace TopicSender
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceBusTopicService serviceBusTopicService = null;

            try
            {
                serviceBusTopicService = new ServiceBusTopicService("servicebusmessages209");
                serviceBusTopicService.Send("To jest testowa wiadomosc").GetAwaiter().GetResult();
                Console.WriteLine("Message was sent successfuly");
            }
            catch (Exception exception)
            {
                Console.Write(exception.Message);
            }
            finally
            {
                serviceBusTopicService?.Close().GetAwaiter().GetResult();
            }
        }
    }
}
