using System;
using System.Collections.Generic;
using System.Threading;
using Confluent.Kafka;

namespace KafkaConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConsumerConfig
            {
                GroupId = "test-consumers",
                BootstrapServers = "localhost:9092",
                EnableAutoCommit = false
            };

            using (var consumer = new ConsumerBuilder<Null, string>(config).Build())
            {
                consumer.Subscribe("test-topic");

                while(true)
                {
                    try
                    {
                        var result = consumer.Consume();

                        Console.WriteLine($"Message => {result.Message.Value}; at: {result.TopicPartitionOffset}");

                        Thread.Sleep(2000);
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Error occoured => {e.Error.Reason}");
                    }                    
                }
            }
        }
    }
}
