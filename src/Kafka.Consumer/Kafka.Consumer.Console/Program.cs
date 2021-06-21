using System;

namespace Kafka
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandMessageConsumer consumer = new CommandMessageConsumer();
            
            consumer.StartConsuming();

            Console.ReadLine();
        }
    }
}