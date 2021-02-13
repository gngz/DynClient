using DynClient.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DynClient
{
    class DynClient
    {
       public static void UpdateDns(IAddressRetriever retriever, IProviderPlugin updateStrategy, IDictionary<String,Object> config)
       {
            IPAddress ipAddress = default;

            while(ipAddress == default)
            {
                ipAddress = retriever.GetAddress();
            }
            
            updateStrategy.Update(ipAddress, config);
       }
    }
}
