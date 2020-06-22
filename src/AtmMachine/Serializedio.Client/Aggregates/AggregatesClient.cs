using Serializedio.Client.Responses;
using Serializedio.Client.Types;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Serializedio.Client.Aggregates
{
    public class AggregatesClient : BaseSerializedClient
    {
        internal AggregatesClient(HttpClient httpClient, SerializedClientFactoryOptions options) : base (httpClient, options)
        {
        }

        public Task<HttpResponseMessage> AddEventAsync<T>(string feed, Guid id, Event<T> @event) => AddEventsAsync<T>(feed, id, new List<Event<T>> { @event });
        public Task<HttpResponseMessage> AddEventsAsync<T>(string feed, Guid id, IEnumerable<Event<T>> events)
        {
            return PostAsync($"aggregates/{feed}/{id}/events", new { Events = events });
        }

        public Task<HttpResponseMessage> GetAsync(string feed, Guid id)
        {
            return GetAsync($"aggregates/{feed}/{id}");
        }

        public async Task<AggregateResponse<TEventData>> GetAsync<TEventData>(string feed, Guid id)
        {
            HttpResponseMessage response = await GetAsync(feed, id);
            if (!response.IsSuccessStatusCode)
                return null;

            return JsonSerializer.Deserialize<AggregateResponse<TEventData>>(await response.Content.ReadAsStringAsync());
        }
    }
}
