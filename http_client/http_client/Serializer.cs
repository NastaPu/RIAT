using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace http_client
{
    interface Serializer
    {
        String serialize<T>(T obj);
        T deserialize<T>(String serializedObj);
    }
}
