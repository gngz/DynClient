using DnsClient; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DynClient.Retriever
{
    class DnsAddressRetriever : IAddressRetriever
    {
        private LookupClient _client;

        private const String openDnsNs1 = "208.67.222.222";
        private const String openDnsNs2 = "208.67.220.220";

        public DnsAddressRetriever()
        {
            _client = new LookupClient(IPAddress.Parse(openDnsNs1),IPAddress.Parse(openDnsNs2));
        }

        public IPAddress GetAddress()
        {
            var result = _client.Query("myip.opendns.com", QueryType.A);
            var record = result.Answers.ARecords().FirstOrDefault();
           
            return record?.Address;
        }
    }
}
