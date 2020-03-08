using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace Common.Core
{
    public class ServiceBusTopicSubscriptionService : ServiceBusBaseService
    {
        private SubscriptionClient _subscriptionClient;

        public ServiceBusTopicSubscriptionService(string topicName, string subscriptionName)
        {
            _subscriptionClient = new SubscriptionClient(ServiceBusConnectionString, topicName, subscriptionName);
        }


        #region Received data from queue

        public async Task ReciveMessages()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("Press Enter key to exit after receiving all messages");
            Console.WriteLine("========================================");

            RegisterMessageHandler();

            Console.Read();
        }

        public void RegisterMessageHandler()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceiverHandler)
            {
                AutoComplete = false,
                MaxConcurrentCalls = 1,
            };

            _subscriptionClient.RegisterMessageHandler(ProcessMessageAsync, messageHandlerOptions);
        }

        private async Task ProcessMessageAsync(Message message, CancellationToken cancellationToken)
        {
            Console.WriteLine(
                $"Received message: SequenceNumber: {message.SystemProperties.SequenceNumber} Body: {Encoding.UTF8.GetString(message.Body)}");

            await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private Task ExceptionReceiverHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;

            Console.WriteLine($"Message handler encountered and exception {exceptionReceivedEventArgs.Exception}");
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Execuiting Action: {context.Action}");

            return Task.CompletedTask;
        }

        #endregion

        public async Task Close()
        {
            await _subscriptionClient.CloseAsync();
        }
    }
}
