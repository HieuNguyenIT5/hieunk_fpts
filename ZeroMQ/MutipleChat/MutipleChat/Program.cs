namespace ZeroMQChat;
class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter your name: ");
        var name = Console.ReadLine();

        var server = new ChatServer();
        var client = new ChatClient(name);

        var serverThread = new Thread(server.Start);
        var clientThread = new Thread(client.Start);

        serverThread.Start();
        clientThread.Start();

        serverThread.Join();
        clientThread.Join();
    }
}