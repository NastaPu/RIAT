using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newsoft.Json;
using System.Xml.Serialization;
using System.IO;

namespace Serialization
{
    interface Serializer
    {
        String serialize<T>(T obj);
        T deserialize<T>(String serializedObj);
    }

    public class Output
    {
        public decimal SumResult { get; set; }
        public int MulResult { get; set; }
        public decimal[] SortedInputs { get; set; }
    }

    public class Input
    {
        public int K { get; set; }
        public decimal[] Sums { get; set; }
        public int[] Muls { get; set; }
    }

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

    class XMLSerializer : Serializer
    {
        public string serialize<T>(T obj)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (MemoryStream stream = new MemoryStream())
            {
                xmlSerializer.Serialize(stream, obj);
                return deleteGarbage(Encoding.UTF8.GetString(stream.ToArray()));
            }
        }

        public string deleteGarbage(string str)
        {
            str = str.Remove(0, 23);
            var attributesStart = str.IndexOf(" xmlns", 0);
            str = str.Remove(attributesStart, attributesStart + 92);
            return str = str.Replace("/n", "");
        }

        public T deserialize<T>(string serializedObj)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            StringReader stringReader = new StringReader(serializedObj);
            return (T)xmlSerializer.Deserialize(stringReader);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            String serializationType = Console.ReadLine();
            Serializer serializer;
            switch (serializationType)
            {
                case "Json":
                    serializer = new JSONSerializer();
                    break;
                case "Xml":
                    serializer = new XMLSerializer();
                    break;
                default:
                    return;
            }
            Input input = serializer.deserialize<Input>(Console.ReadLine());
            Output output = mathOperations(input);
            Console.WriteLine(serializer.serialize<Output>(output));
        }

        static Output mathOperations(Input input)
        {
            Output output = new Output();
            output.SumResult = 0;
            List<decimal> sortedInputs = new List<decimal>();
            for (int i = 0; i < input.Sums.Length; i++)
            {
                output.SumResult += input.Sums[i];
                sortedInputs.Add(input.Sums[i]);
            }
            output.SumResult *= input.K;
            output.MulResult = 1;
            for (int i = 0; i < input.Muls.Length; i++)
            {
                output.MulResult *= input.Muls[i];
                sortedInputs.Add(input.Muls[i]);
            }
            sortedInputs.Sort();
            output.SortedInputs = sortedInputs.ToArray();
            return output;

        }
    }
}
