using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace QueueApp.Core
{
    public class CloudQueueService
    {
        private const string QueueName = "newsqueue";

        private const string ConnectionString =
            "DefaultEndpointsProtocol=https;EndpointSuffix=core.windows.net;AccountName=articlesubmission;AccountKey=AnGdUqQguM072jrVtJb5tNygXoU+c9dAJPywJ3czvfx+qFhKdCao9zuaBEd/1MTWSLharK6I/LqOXGSrsMrekQ==";

        private CloudQueue _queue;

        public CloudQueueService()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            _queue = queueClient.GetQueueReference(QueueName);
        }

        public async Task CreateNewQueueIfNotExistsAsync()
        {
            bool createdQueue = await _queue.CreateIfNotExistsAsync();
            if (createdQueue)
            {
                Console.WriteLine("The queue of news articles was created.");
            }
        }

        public async Task SendArticleAsync(string newsMessage)
        {
            CloudQueueMessage articleMassage = new CloudQueueMessage(newsMessage);
            await _queue.AddMessageAsync(articleMassage);
        }

        public async Task<string> ReceiveArticleAsync()
        {
            bool exists = await _queue.ExistsAsync();
            if (!exists)
                throw new Exception($"Queue with name {QueueName} not exists");

            CloudQueueMessage message = await _queue.GetMessageAsync();
            if (message != null)
            {
                string messageText = message.AsString;
                await _queue.DeleteMessageAsync(message);
                return messageText;
            }

            return string.Empty;
        }
    }
}
