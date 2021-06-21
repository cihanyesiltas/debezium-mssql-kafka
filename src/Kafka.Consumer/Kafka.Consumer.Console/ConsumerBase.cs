using System;
using System.Diagnostics;
using Confluent.Kafka;

namespace Kafka
{
    public abstract class ConsumerBase
    {
        private readonly string _topic;
 
        protected ConsumerBase(string topic)
        {
            this._topic = topic;
        }
 
        public void StartConsuming()
        {
            var conf = new ConsumerConfig
            {
                GroupId = Guid.NewGuid().ToString(),
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
 
            using (var consumer = new ConsumerBuilder<string, string>(conf).Build())
            {
                consumer.Subscribe(_topic);

                while (true)
                {
                    try
                    {
                        var consumedTextMessage = consumer.Consume();
                        //Console.WriteLine($"Key: {consumedTextMessage.Message.Key} Consumed message '{consumedTextMessage.Message.Value}' Topic: {consumedTextMessage.Topic} Partition: {consumedTextMessage.Partition}. Time:{sw.Elapsed}");

                        OnMessageDelivered(consumedTextMessage.Message.Value);
                    }
                    catch (ConsumeException e)
                    {
                        OnErrorOccured(e.Error);
                    }
                }
            }
        }
 
        public abstract void OnMessageDelivered(string message);
 
        public abstract void OnErrorOccured(Error error);
    }
}