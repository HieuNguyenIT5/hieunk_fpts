using NetMQ;
using NetMQ.Sockets;

using (var publisher = new PublisherSocket())
{
    publisher.Bind("tcp://*:5556");

    while (true)
    {
        Console.WriteLine("You: ");
        string message = Console.ReadLine();
        Thread.Sleep(100);
        publisher
            .SendMoreFrame("A")
            .SendFrame(message); // Message
    }
}