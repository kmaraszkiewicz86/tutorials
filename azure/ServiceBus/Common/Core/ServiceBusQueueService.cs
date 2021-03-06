﻿using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace Common.Core
{
    public class ServiceBusQueueService: ServiceBusBaseService
    {
        private QueueClient _queueClient;

        public ServiceBusQueueService(string queueName)
        {
            _queueClient = new QueueClient(ServiceBusConnectionString, queueName);
        }

        #region Send data into queue

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

        #endregion

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

        #endregion

        public async Task Close()
        {
            await _queueClient.CloseAsync();
        }
    }
}
