using AtmMachine.Domain;
using AtmMachine.Domain.ValueObjects;
using Microsoft.Extensions.Options;
using Serializedio.Client;
using Serializedio.Client.Aggregates;
using Serializedio.Client.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine.Infrastructure.Repositories
{
    public class SerializedioRepository : IEventRepository
    {
        private readonly AggregatesClient _aggregatesClient;

        public SerializedioRepository(IOptions<SerializedClientFactoryOptions> connectionOptions)
        {
            SerializedClientFactory serializedClientFactory = SerializedClientFactory.Create(options =>
                                                              {
                                                                  options.AccessKey = connectionOptions.Value.AccessKey;
                                                                  options.SecretAccessKey = connectionOptions.Value.SecretAccessKey;
                                                                  options.BaseUrl = connectionOptions.Value.BaseUrl;
                                                              });

            _aggregatesClient = serializedClientFactory.CreateAggregatesClient();
        }

        public async Task<List<Event<T>>> GetEventsAsync<T>(string streamName, Guid streamId)
        {
            streamName = streamName.ToLower();
            AggregateResponse<T> events = await _aggregatesClient.GetAsync<T>(streamName, streamId);
            return events.Events.Select(x => new Event<T>
            {
                Data = x.Data
            }).ToList();
        }

        public async Task AddEventAsync<T>(string streamName, Guid streamId, string @event, T entity) where T : class, IEntity
        {
            streamName = streamName.ToLower();
            HttpResponseMessage response = await _aggregatesClient.AddEventAsync(streamName, 
                                                                                streamId, 
                                                                                new Serializedio.Client.Types.Event<T>()
                                                                                {
                                                                                    Data = entity, 
                                                                                    EventId = entity.Id,
                                                                                    EventType = @event
                                                                                });

            if (!response.IsSuccessStatusCode)
            {
#if DEBUG
                string t = await response.Content.ReadAsStringAsync();
#endif

                throw new Exception($"Response for {nameof(AddEventAsync)} was not OK.");
            }
        }
    }
}
