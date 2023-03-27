using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService1
{
    public class Main
    {
        private readonly IProducer<Null, string> _producer;

        public Main(IProducer<Null, string> producer)
        {
            _producer = producer;
        }
        private async Task ProduceAsync(string topic, string message)
        {
            var msg = new Message<Null, string> { Value = message };
            await _producer.ProduceAsync(topic, msg);
        }

        public async Task RunAsync()
        {
            var topic = "";
            do
            {
                Console.WriteLine("Nhap topic: ");
                topic = Console.ReadLine();
                if (topic != "")
                {
                    while (true)
                    {
                        Console.WriteLine("Nhap message");
                        var message = Console.ReadLine();
                        await ProduceAsync(topic, message);
                    }
                }
                else
                {
                    Console.WriteLine("Ten topic khong duoc bo trong!");
                }
            } while (topic == "");
        }
    }
}
