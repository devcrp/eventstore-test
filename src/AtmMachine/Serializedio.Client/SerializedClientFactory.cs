using Serializedio.Client.Aggregates;
using Serializedio.Client.Projections;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Serializedio.Client
{
    public class SerializedClientFactory
    {
        private static HttpClient _httpClient = new HttpClient();
        private static SerializedClientFactoryOptions _serializedClientFactoryOptions;

        public static SerializedClientFactory Create(Action<SerializedClientFactoryOptions> options) => new SerializedClientFactory(options);

        private SerializedClientFactory(Action<SerializedClientFactoryOptions> optionsAction)
        {
            var options = new SerializedClientFactoryOptions();
            optionsAction.Invoke(options);
            _serializedClientFactoryOptions = options;

            _httpClient.BaseAddress = new Uri(options.BaseUrl);
        }

        public AggregatesClient CreateAggregatesClient() => new AggregatesClient(_httpClient, _serializedClientFactoryOptions);
        public ProjectionsClient CreateProjectionsClient() => new ProjectionsClient(_httpClient, _serializedClientFactoryOptions);
    }
}
