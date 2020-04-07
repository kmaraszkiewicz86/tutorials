using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;

namespace SenderApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            using (var connection = factory.CreateConnection())
            {
                var index = 1;

                while (true)
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue: "hello",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

                        while (true)
                        {
                            string message = $"Hello World! #{index++}";
                            var body = Encoding.UTF8.GetBytes(message);

                            Console.WriteLine($"Sending {index} message...");

                            channel.BasicPublish(exchange: "",
                            routingKey: "hello",
                            basicProperties: null,
                            body: body);

                            Console.WriteLine($" [x] Sent {message}", message);

                            Thread.Sleep(3000);
                        }
                    }
                }

                
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadKey();
        }
    }
}
