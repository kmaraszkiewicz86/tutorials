using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace Common.Core
{
    public class ServiceBusQueueService
    {
        private string ServiceBusConnectionString =>
            "Endpoint=sb://servicebus209.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=JYYFh+3b/MrREmitX18qy8VecUyafoH1vULoUkdfI0I=";

        private QueueClient _queueClient;

        public ServiceBusQueueService(string queueName)
        {
            _queueClient = new QueueClient(ServiceBusConnectionString, queueName);
        }

        public async Task Send(string messageString)
        {
            var message = new Message(Encoding.UTF8.GetBytes(messageString));
            try
            {
                await _queueClient.SendAsync(message);
            }
            catch
            {
                throw;
            }
        }

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

            _queueClient.RegisterMessageHandler(ProcessMessageAsync, messageHandlerOptions);
        }

        private async Task ProcessMessageAsync(Message message, CancellationToken cancellationToken)
        {
            Console.WriteLine(
                $"Received message: SequenceNumber: {message.SystemProperties.SequenceNumber} Body: {Encoding.UTF8.GetString(message.Body)}");

            await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
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

        public async Task Close()
        {
            await _queueClient.CloseAsync();
        }
    }
}
