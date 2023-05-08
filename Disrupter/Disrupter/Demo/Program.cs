using Disruptor;
using Disruptor.Dsl;
using System.Diagnostics;

namespace DisruptorTest
{
    public sealed class ValueEntry
    {
        public long Value { get; set; }

        public ValueEntry()
        {
            Console.WriteLine("New ValueEntry created");
        }
    }

    public class ValueAdditionHandler : IEventHandler<ValueEntry>
    {
        public void OnEvent(ValueEntry data, long sequence, bool endOfBatch)
        {
            Console.WriteLine("Event handled: Value = {0} (processed event {1})", data.Value, sequence);
        }
    }

    class Program
    {
        private static readonly Random _random = new Random();
        private static readonly int _ringSize = 16;  // Must be multiple of 2

        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var disruptor = new Disruptor<ValueEntry>(() => new ValueEntry(), _ringSize, TaskScheduler.Default);

                disruptor.HandleEventsWith(new ValueAdditionHandler());

                var ringBuffer = disruptor.Start();

                for (int i = 0; i< 10000; i++)
                {
                    long sequenceNo = ringBuffer.Next();

                    ValueEntry entry = ringBuffer[sequenceNo];
                    var value = _random.Next() * _random.Next();

                    entry.Value = value;

                    ringBuffer.Publish(sequenceNo);

                    Console.WriteLine("Published entry {0}, value {1}", sequenceNo, entry.Value);
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