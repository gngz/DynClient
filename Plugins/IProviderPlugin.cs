using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DynClient.Plugins
{
    public interface IProviderPlugin
    {
        String Name { get; }
        List<String> OptionRequirements { get; set; }
        void Update(IPAddress address, IDictionary<String,Object> config);

    }
}
