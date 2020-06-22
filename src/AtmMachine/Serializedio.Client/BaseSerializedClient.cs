using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Serializedio.Client
{
    public class BaseSerializedClient
    {
        private readonly SerializedClientFactoryOptions _options;

        internal BaseSerializedClient(HttpClient httpClient, SerializedClientFactoryOptions options)
        {
            HttpClient = httpClient;
            this._options = options;
            HttpClient.DefaultRequestHeaders.Clear();
            HttpClient.DefaultRequestHeaders.Add("Serialized-Access-Key", _options.AccessKey);
            HttpClient.DefaultRequestHeaders.Add("Serialized-Secret-Access-Key", _options.SecretAccessKey);
        }

        protected HttpClient HttpClient { get; }

        protected Task<HttpResponseMessage> PostAsync(string endpoint, object data)
        {
            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            
            StringContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return HttpClient.PostAsync(endpoint, content);
        }

        protected Task<HttpResponseMessage> GetAsync(string endpoint)
        {
            return HttpClient.GetAsync(endpoint);
        }
    }
}
