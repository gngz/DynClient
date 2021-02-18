using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynClient.Retriever
{

    public enum RetrieverType { Http, Dns};
    class RetrieverFactory
    {
        public static IAddressRetriever GetRetriever(RetrieverType type)
        {
            if (type.Equals(RetrieverType.Dns)) return new DnsAddressRetriever();

            return new HttpAddressRetriever();

        }
    }
}
