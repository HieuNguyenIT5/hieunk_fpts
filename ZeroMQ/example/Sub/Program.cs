using NetMQ;
using NetMQ.Sockets;

using (var subscriber = new SubscriberSocket())
{
    subscriber.Connect("tcp://127.0.0.1:5556");
    subscriber.Subscribe("A");

    while (true)
    {
        var topic = subscriber.ReceiveFrameString();
        var msg = subscriber.ReceiveFrameString();
        Console.WriteLine("Your friend: {0} {1}", topic, msg);
        Thread.Sleep(100);
    }
}