using AtmMachine.Domain;
using AtmMachine.Domain.ValueObjects;
using AtmMachine.Infrastructure.Contexts;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine.Infrastructure.Repositories
{
    public class EsRepository : IEventRepository
    {
        private readonly EsConnection _esConnection;
        private IEventStoreConnection Connection => _esConnection.Connection;

        public EsRepository(EsConnection esConnection)
        {
            this._esConnection = esConnection;
        }

        public Task AddEventAsync<T>(string stream, string @event, T entity) where T : class, IEntity
        {
            string data = JsonConvert.SerializeObject(entity);
            string metadata = JsonConvert.SerializeObject(new { TimestampUtc = DateTime.UtcNow });

            EventData eventPayload = new EventData(Guid.NewGuid(),
                                                   @event,
                                                   true,
                                                   Encoding.UTF8.GetBytes(data),
                                                   Encoding.UTF8.GetBytes(metadata));

            return Connection.AppendToStreamAsync(stream, ExpectedVersion.Any, eventPayload);
        }

        public async Task<List<Event<T>>> GetEventsAsync<T>(string stream)
        {
            StreamEventsSlice result = await Connection.ReadStreamEventsBackwardAsync(stream, StreamPosition.End, 10, true);
            IEnumerable<Event<T>> events = result.Events.Select(x => new Event<T>
                                            {
                                                Data = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(x.Event.Data))
                                            });

            return events.ToList();
        }
    }
}
