using DynClient.Updater.Cloudflare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using DynClient.Plugins;

namespace DynClient.Updater.Cloudflare
{
    public class CloudflareUpdater : IProviderPlugin
    {
        const String ENDPOINT = "https://api.cloudflare.com/client/v4";

        public string Name { get => "CloudFlare"; }
        public List<string> OptionRequirements { get; set; } = new List<string>();

        public void Update(IPAddress address, IDictionary<string, object> config)
        {
            String apiKey = config.ContainsKey("api-key") ? config["api-key"].ToString() : default;
            String zoneName = config.ContainsKey("api-key") ? config["api-key"].ToString() : default;
            String domain = config.ContainsKey("api-key") ? config["api-key"].ToString() : default;

            ZoneModel zone = GetZone(apiKey, zoneName);
            RecordModel record = GetRecord(apiKey, zone, domain);
            UpdateRecord(apiKey, zone, record, address.ToString());
        }

        private ZoneModel GetZone(String apiKey, String zoneName)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var uri = $"{ENDPOINT}/zones?name={zoneName}&status=active";
            var response = client.GetFromJsonAsync<ResultModel<ZoneModel>>(uri).Result;

            var zone = response.Result.FirstOrDefault();
            return zone;
        }
        private RecordModel GetRecord(String apiKey, ZoneModel zone, String domain)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var uri = $"{ENDPOINT}/zones/{zone.Id}/dns_records?name={domain}";
            var response = client.GetFromJsonAsync<ResultModel<RecordModel>>(uri).Result;

            var rr = response.Result.FirstOrDefault();
            return rr;
        }

        private void UpdateRecord(String apiKey, ZoneModel zone, RecordModel record, String address)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var uri = $"{ENDPOINT}/zones/{zone.Id}/dns_records/{record.Id}";

            var resp = client.PutAsJsonAsync<RecordModel>(uri, new RecordModel()
            {
                Name = record.Name,
                Type = "A",
                Content = address,
                Proxied = false,
            }).Result;


            Console.WriteLine(resp.ToString());


        }
    }
}
