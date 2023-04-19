using NetMQ;
using NetMQ.Sockets;

namespace ZeroMQChat
{
    public class ChatServer
    {
        private readonly string _address = "tcp://10.26.4.171:5556";

        public void Start()
        {
            using (var publisher = new PublisherSocket())
            {
                publisher.Bind(_address);

                Console.WriteLine("Server started!");

                while (true)
                {
                    Console.Write("> ");
                    var message = Console.ReadLine();

                    var timestamp = DateTime.Now.ToString("HH:mm:ss");
                    var finalMessage = $"[{timestamp}] Server: {message}";

                    publisher.SendFrame(finalMessage);

                    Thread.Sleep(1000);
                }
            }
        }
    }
}
