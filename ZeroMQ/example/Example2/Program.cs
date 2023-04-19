using System;
using NetMQ;
using NetMQ.Sockets;

static class Program
{
    public static void Main()
    {
        Console.WriteLine("Connecting to server…");
        using (var client = new RequestSocket())
        {
            client.Connect("tcp://localhost:5555");

            while (true)
            {
                Console.Write("Me: ");
                string ms = Console.ReadLine();
                Thread.Sleep(100);  //  Do some 'work'
                client.SendFrame(ms);
                string str = client.ReceiveFrameString();
                Console.WriteLine("You: {0}", str);
                
            }
        }
    }
}