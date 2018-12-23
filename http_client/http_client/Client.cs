using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace http_client
{
	class Client
	{
        WebClient webClient;
        String Url;

        public Client(String port)
        {
            webClient = new WebClient();
            Url = "http://127.0.0.1:" + port;
        }

        public bool Ping()
        {
            try {
                webClient.DownloadData(Url + "/Ping");
                return true;
            } catch (WebException) {
                return false;
            }
        }

        public Input getInputData()
        {
            byte[] inputData;
            string inputDataStr;
            inputData = webClient.DownloadData(Url + "/GetInputData");
            inputDataStr = Encoding.UTF8.GetString(inputData);
            Serializer serializer = new JSONSerializer();
            Input input = serializer.deserialize<Input>(inputDataStr);
            return input;
        } 

        public void writeAnswer(Output output)
        {
            byte[] outputData;
            Serializer serializer = new JSONSerializer();
            string outputDataStr = serializer.serialize<Output>(output);
            outputData = Encoding.UTF8.GetBytes(outputDataStr);
            webClient.UploadData(Url + "/WriteAnswer", outputData);
        }
    }
}
