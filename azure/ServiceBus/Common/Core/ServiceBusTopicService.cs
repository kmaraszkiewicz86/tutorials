using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace Common.Core
{
    public class ServiceBusTopicService: ServiceBusBaseService
    {
        private TopicClient _topicClient;

        public ServiceBusTopicService(string topicName)
        {
            _topicClient = new TopicClient(ServiceBusConnectionString, topicName);
        }

        #region Send message to topic

        public async Task Send(string messageText)
        {
            Console.WriteLine($"Sending message: {messageText}");
            var message = new Message(Encoding.UTF8.GetBytes(messageText));
            await _topicClient.SendAsync(message);
        }

        #endregion

        public async Task Close()
        {
            await _topicClient.CloseAsync();
        }
    }
}
