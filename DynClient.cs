using DynClient.Logger;
using DynClient.Plugins;
using DynClient.Retriever;
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
       public static void UpdateDns(IProviderPlugin updateStrategy, IDictionary<String,Object> config)
       {
           IPAddress ipAddress = default;

           IDynLogger logger = new DynConsoleLogger();

           IAddressRetriever retriever = RetrieverFactory.GetRetriever(RetrieverType.Http);

            while(ipAddress == default)
            {
                ipAddress = retriever.GetAddress();
            }

            updateStrategy.Logger = logger;
            updateStrategy.Update(ipAddress, config);
       }
    }
}
