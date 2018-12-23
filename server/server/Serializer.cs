using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace server
{
    interface Serializer
    {
        String serialize<T>(T obj);
        T deserialize<T>(String serializedObj);
    }
}
