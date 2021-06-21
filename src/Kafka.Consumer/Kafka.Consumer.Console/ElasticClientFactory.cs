using System;
using Nest;

namespace Kafka.Consumer.Console
{
    public static class ElasticClientFactory
    {
        private static IElasticClient _elasticClient;

        public static IElasticClient ElasticClient
        {
            get
            {
                return _elasticClient ??=
                    new ElasticClient(new ConnectionSettings(new Uri("http://192.168.24.78:9200/")));
            }
        }
    }
}