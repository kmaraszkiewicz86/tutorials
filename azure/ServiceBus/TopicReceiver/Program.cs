using System;
using Common.Core;

namespace TopicReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceBusTopicSubscriptionService serviceBusTopicSubscriptionService = null;

            try
            {
                serviceBusTopicSubscriptionService = new ServiceBusTopicSubscriptionService("servicebusmessages209", "Americas");
                serviceBusTopicSubscriptionService.ReciveMessages().GetAwaiter().GetResult();
            }
            catch (Exception exception)
            {
                Console.Write(exception.Message);
            }
            finally
            {
                serviceBusTopicSubscriptionService?.Close().GetAwaiter().GetResult();
            }
        }
    }
}
