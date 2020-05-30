using System;
using Confluent.Kafka;

namespace KafkaProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

            Action<DeliveryReport<Null, string>> handler = r =>
                Console.WriteLine(!r.Error.IsError
                ? $"Delivered messege to {r.TopicPartitionOffset}"
                : $"Delivered Error: {r.Error.Reason}");


            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                var stringValue = string.Empty;

                for (int i = 0; i < 20; i++)
                {
                    stringValue += i.ToString() + "";

                    producer.ProduceAsync("test-topic", new Message<Null, string> { Value = stringValue });

                    producer.Flush(TimeSpan.FromSeconds(2));
                }
            }


        }
    }
}
