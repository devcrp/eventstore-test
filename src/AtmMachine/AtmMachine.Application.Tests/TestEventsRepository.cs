using AtmMachine.Domain;
using AtmMachine.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine.Application.Tests
{
    public class TestEventsRepository : IEventRepository
    {
        Dictionary<string, List<IEntity>> _events;

        public TestEventsRepository()
        {
            _events = new Dictionary<string, List<IEntity>>();
        }

        private string GetDictionaryKey(string name, Guid id) => $"{name.ToLower()}-{id}";

        public async Task<List<Event<T>>> GetEventsAsync<T>(string streamName, Guid streamId)
        {
            string key = GetDictionaryKey(streamName, streamId);

            if (!_events.ContainsKey(key))
                return new List<Event<T>>();

            return _events[key]
                .Select(ev => ev as Event<T>)
                .ToList();
        }

        public async Task AddEventAsync<T>(string streamName, Guid streamId, string @event, T entity) where T : class, IEntity
        {
            string key = GetDictionaryKey(streamName, streamId);

            if (!_events.ContainsKey(key))
                _events.Add(key, new List<IEntity>());

            _events[key].Add(entity);
        }
    }
}
