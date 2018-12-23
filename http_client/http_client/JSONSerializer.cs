using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace http_client
{
    class JSONSerializer : Serializer
    {
        public string serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public T deserialize<T>(string serializedObj)
        {
            return JsonConvert.DeserializeObject<T>(serializedObj);
        }
    }
}
