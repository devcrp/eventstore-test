using AtmMachine.Domain;
using AtmMachine.Domain.ValueObjects;
using Microsoft.Extensions.Options;
using Serializedio.Client;
using Serializedio.Client.Aggregates;
using System;
using System.Collections.Generic;
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
                                                                  options.BaseUri = connectionOptions.Value.BaseUri;
                                                              });

            _aggregatesClient = serializedClientFactory.CreateAggregatesClient();
        }

        public Task<List<Event<T>>> GetEventsAsync<T>(string streamName, Guid streamId)
        {
            throw new NotImplementedException();
        }

        public async Task AddEventAsync<T>(string streamName, Guid streamId, string @event, T entity) where T : class, IEntity
        {
            HttpResponseMessage response = await _aggregatesClient.AddEventAsync(streamName, 
                                                                                streamId, 
                                                                                new Serializedio.Client.Types.Event<T>()
                                                                                {
                                                                                    Data = entity, 
                                                                                    EventId = entity.Id,
                                                                                    EventType = @event
                                                                                });

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Response for {nameof(AddEventAsync)} was not OK.");
        }
    }
}
