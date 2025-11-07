using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Utility
{
    public class ApiClient
    {
        private readonly HttpClient client;
        HttpResponseMessage response;
        public ApiClient(string Url, string ApiKey)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(Url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("ApiKey", ApiKey);
            response = new HttpResponseMessage();
        }

        public async Task<HttpResponseMessage> Get(string Api)
        {
            response = await client.GetAsync(Api);
            return response;
        }

        public async Task<HttpResponseMessage> Post(string Api, object payload)
        {
            var jsonPayload = JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            response = await client.PostAsync(Api, content);
            return response;
        }
    }
}
