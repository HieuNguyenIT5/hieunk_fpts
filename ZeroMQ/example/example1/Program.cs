using System;
using System.Threading;
using NetMQ;
using NetMQ.Sockets;

static class Program
{
    public static void Main()
    {
        using (var server = new ResponseSocket())
        {
            server.Bind("tcp://*:5555");

            while (true)
            {
                string str = server.ReceiveFrameString();
                Console.WriteLine("You: {0}",str);
                Console.Write("Me: ");
                string ms = Console.ReadLine();
                Thread.Sleep(100);
                server.SendFrame(ms);
            }
        }
    }
}