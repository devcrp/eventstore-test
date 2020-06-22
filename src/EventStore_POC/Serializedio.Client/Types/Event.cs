using System;
using System.Collections.Generic;
using System.Text;

namespace Serializedio.Client.Types
{
    public class Event<T>
    {
        public Guid EventId { get; set; }
        public string EventType { get; set; }
        public T Data { get; set; }
    }
}
