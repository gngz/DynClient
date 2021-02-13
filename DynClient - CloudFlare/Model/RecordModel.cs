using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynClient.Updater.Cloudflare.Model
{
    class RecordModel
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public String Type { get; set; }
        public String Content { get; set; }
        public Boolean Proxied { get; set; }
    }
}
