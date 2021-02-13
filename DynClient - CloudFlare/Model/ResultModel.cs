using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DynClient.Updater.Cloudflare.Model
{
    class ResultModel<T>
    {
        [JsonPropertyName("result")]
        public IList<T> Result { get; set; }
    }
}
