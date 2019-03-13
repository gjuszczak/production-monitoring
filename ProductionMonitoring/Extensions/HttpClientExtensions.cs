using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProductionMonitoring.Extensions
{
    public static class HttpClientExtensions
    {
        public static HttpClient BasicAuth(this HttpClient client, string username, string password)
        {
            var auth = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);
            return client;
        }

        public static HttpClient BaseUri(this HttpClient client, Uri baseUri)
        {
            client.BaseAddress = baseUri;
            return client;
        }

        public static async Task<T> ReadJsonContentAsync<T>(this Task<HttpResponseMessage> response)
        {
            return await ReadJsonContentAsync<T>(await response);
        }

        public static async Task<T> ReadJsonContentAsync<T>(this HttpResponseMessage response)
        {
            var stream = await response.Content.ReadAsStreamAsync();
            var serializer = new JsonSerializer();
            using (var reader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(reader))
                return serializer.Deserialize<T>(jsonReader);
        }
    }
}
