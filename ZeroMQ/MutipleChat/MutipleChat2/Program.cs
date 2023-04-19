namespace ZeroMQChat;
class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter your name: ");
        var name = Console.ReadLine();

        var client = new ChatClient(name);
        var server = new ChatClient(name);

        var clientThread = new Thread(client.Start);
        var serverThread = new Thread(server.Start);

        serverThread.Start();
        clientThread.Start();

        serverThread.Join();
        clientThread.Join();
    }
}