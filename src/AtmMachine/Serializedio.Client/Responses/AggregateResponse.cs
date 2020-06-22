using Serializedio.Client.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serializedio.Client.Responses
{
    public class AggregateResponse<T>
    {
        public Guid AggregateId { get; set; }
        public string AggregateType { get; set; }
        public int AggregateVersion { get; set; }
        public List<Event<T>> Events { get; set; }
    }
}
