using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.AggregateModels
{
    public class KafkaSettings
    {
        public string HostPort { get; set; }
        public string Group { get; set; }
        public string Topic { get; set; }
    }
}
