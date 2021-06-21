using System;
using Kafka.Consumer.Console;
using Newtonsoft.Json;
using Error = Confluent.Kafka.Error;

namespace Kafka
{
    public class CommandMessageConsumer : ConsumerBase
    {
        public CommandMessageConsumer() : base("debezium.dbo.jobs") { }
 
        public override void OnMessageDelivered(string message)
        {
            var rootModel = JsonConvert.DeserializeObject<Root>(message);
            if (rootModel != null)
            {
               var indexReponse = ElasticClientFactory.ElasticClient.Index(new 
                {
                    Id = rootModel.after.id,
                    Description = rootModel.after.description,
                    Ip = rootModel.after.ip,
                    Title = rootModel.after.title,
                    CreationDate = DateTime.Now
                }, i=> i.Index("test-job"));
            }
        }
 
        public override void OnErrorOccured(Error error)
        {
            Console.WriteLine($"Error: {error}");
        }
    }
}