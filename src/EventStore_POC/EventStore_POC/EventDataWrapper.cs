using EventStore.ClientAPI;
using Newtonsoft.Json;
using System;
using System.Buffers.Text;
using System.Text;

namespace EventStore_POC
{
    public class EventDataWrapper
    {
        public Guid EventId { get; set; }   
        public string EventType { get; set; }
        public object Data { get; set; }
        public object Metadata { get; set; }

        public EventData ToEventData()
        {
            byte[] data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Data));
            byte[] metadata = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Metadata));

            return new EventData(EventId, EventType, true, data, metadata);
        }
    }
}
