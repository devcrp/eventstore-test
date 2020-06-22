using Serializedio.Client.Responses;
using Serializedio.Client.Types;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Serializedio.Client.Projections
{
    public class ProjectionsClient : BaseSerializedClient
    {
        internal ProjectionsClient(HttpClient httpClient, SerializedClientFactoryOptions options) : base(httpClient, options)
        {
        }

        public Task<HttpResponseMessage> CreateAsync(string projectionName, string feed, ProjectionDefinition projectionDefinition) => CreateAsync(projectionName, feed, new List<ProjectionDefinition> { projectionDefinition });
        public Task<HttpResponseMessage> CreateAsync(string projectionName, string feed, IEnumerable<ProjectionDefinition> projectionDefinitions)
        {
            return PostAsync("projections/definitions", new
            {
                ProjectionName = projectionName,
                FeedName = feed,
                Handlers = projectionDefinitions
            });
        }

        public Task<HttpResponseMessage> QuerySingleAsync(string projectionName, Guid aggregateId)
        {
            return GetAsync($"projections/single/{projectionName}/{aggregateId}");
        }

        public async Task<ProjectionResponse<TData>> QuerySingleAsync<TData>(string projectionName, Guid aggregateId)
        {
            HttpResponseMessage response = await GetAsync($"projections/single/{projectionName}/{aggregateId}");
            if (!response.IsSuccessStatusCode)
                return null;

            return JsonSerializer.Deserialize<ProjectionResponse<TData>>(await response.Content.ReadAsStringAsync());
        }
    }
}
