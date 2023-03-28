using Confluent.Kafka;
using System.Text.Json;

namespace WorkerService1;

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
            var product = new Product();
            product.MaSP = 1;
            product.TenSP = "May tinh";
            product.GiaBan = 2000;
            product.SoLuongCon = 100;
            if (topic != "")
            {
                string jsonProduct = JsonSerializer.Serialize(product);
                await ProduceAsync(topic, jsonProduct);
            }
            else
            {
                Console.WriteLine("Ten topic khong duoc bo trong!");
            }
        } while (topic == "");
    }
}
