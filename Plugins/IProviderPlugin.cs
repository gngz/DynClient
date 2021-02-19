using DynClient.Logger;
using System;
using System.Collections.Generic;
using System.Net;


namespace DynClient.Plugins
{
    public interface IProviderPlugin
    {
        String Name { get; }
        String Author { get; }
        IDynLogger Logger { get; set; }
        List<String> OptionRequirements { get; set; }
        void Update(IPAddress address, IDictionary<String,Object> config);

    }
}
