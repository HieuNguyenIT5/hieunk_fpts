using System.Collections.Concurrent;
using System.Diagnostics;


namespace SequentialQueueTest
{
    public sealed class ValueEntry
    {
        public long Value { get; set; }

        public ValueEntry()
        {
            Console.WriteLine("New ValueEntry created");
        }
    }
    class Program
    {
        private static readonly Random _random = new Random();

        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<ValueEntry> arr = new List<ValueEntry>();
            try
            {
                for (int i = 0;i < 10000; i++)
                {
                    var value = _random.Next() + _random.Next();
                    var entry = new ValueEntry() { Value = value };
                    Console.WriteLine("Published entry, value {0}", entry.Value);
                    arr.Add(entry);
                    Console.WriteLine("Event handled: Value = {0} (processed event {1})", entry.Value, i);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            stopwatch.Stop();
            Console.WriteLine("Thoi gian thuc hien: {0}ms", stopwatch.ElapsedMilliseconds);
        }
    }
}