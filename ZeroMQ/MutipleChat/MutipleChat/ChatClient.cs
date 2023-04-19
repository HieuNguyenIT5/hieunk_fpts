using NetMQ;
using NetMQ.Sockets;

namespace ZeroMQChat;
public class ChatClient
{
    private readonly string _address = "tcp://10.26.4.171:5555";
    private readonly string _name;

    public ChatClient(string name)
    {
        _name = name;
    }

    public void Start()
    {
        using (var subscriber = new SubscriberSocket())
        {
            subscriber.Connect(_address);
            subscriber.Subscribe("");

            Console.WriteLine($"Connected to server with id {_name}");

            while (true)
            {
                var message = subscriber.ReceiveFrameString();

                if (!string.IsNullOrWhiteSpace(message))
                {
                    Console.WriteLine(message);
                }
            }
        }
    }
}