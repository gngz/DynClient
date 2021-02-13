using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DynClient
{
    class HttpAddressRetriever : IAddressRetriever
    {
        private List<String> endpoints = new List<String>
        {
            "https://checkip.amazonaws.com/",
            "https://ipinfo.io/ip",
            "https://icanhazip.com/",
            "https://ipecho.net/plain",
        };

        private String GetEndpoint()
        {
            var random = new Random();
            var index = random.Next(endpoints.Count);
            return endpoints[index];
        }

        private void RemoveEndpoint(String endpoint) => endpoints.Remove(endpoint);

        public IPAddress GetAddress()
        {
            HttpClient client = new HttpClient();
            var endpoint = GetEndpoint();

            var response = client.GetAsync(endpoint).Result;

            if(response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                content = content.Replace("\n", "");
                return IPAddress.Parse(content);
            }

            RemoveEndpoint(endpoint);
            return null;
        }
    }
}
