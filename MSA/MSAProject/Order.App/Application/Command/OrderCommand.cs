using Confluent.Kafka;
using MediatR;
using Order.Domain.AggregateModels;

namespace Order.App.Application.Command
{
    public class OrderCommand : IRequest
    {
        public ConsumeResult<string, string> consumer { get; set; }
        public OrderCommand(ConsumeResult<string, string> consumer)
        {
            this.consumer = consumer;
        }
    }
}
