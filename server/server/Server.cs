using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace server
{
    class Server
    {
        String Url;
        HttpListener httpListener;
        Serializer serializer;

        public Server(String port)
        {
            serializer = new JSONSerializer();
            Url = "http://127.0.0.1:" + port;
            httpListener.Prefixes.Add(Url + "/Ping");
            httpListener.Prefixes.Add(Url + "/PostInputData");
            httpListener.Prefixes.Add(Url + "/GetAnswer");
            httpListener.Prefixes.Add(Url + "/Stop");
        }

        public void Start()
        {
            Output output = new Output();
            Input input = new Input();

            httpListener.Start();
            while(httpListener.IsListening) {
                var context = httpListener.GetContext();
                String path = context.Request.Url.ToString();
                if (path.Equals(Url + "/Ping")) {
                    Ping(context);
                } else if (path.Equals(Url + "/PostInputData")) {
                    input = PostInputData(context);
                } else if (path.Equals(Url + "/GetAnswer")) {
                    output = mathOperations(input);
                    GetAnswer(context, output);
                    output = new Output();
                } else if (path.Equals(Url + "/Stop")) {
                    Stop();
                }
            }
        }

        public void Ping(HttpListenerContext context)
        {
            using (StreamWriter streamWriter = new StreamWriter(context.Response.OutputStream)) {
                streamWriter.Write("");
            }
            context.Response.StatusCode = 200;
        }

        public void GetAnswer(HttpListenerContext context, Output output)
        {
            string outputDataStr = serializer.serialize<Output>(output);

            using (StreamWriter streamWriter = new StreamWriter(context.Response.OutputStream)) {
                streamWriter.Write(outputDataStr);
            }
        }

        public Input PostInputData(HttpListenerContext context)
        {
            using (StreamReader streamReader = new StreamReader(context.Request.InputStream)) {
                String strReader = streamReader.ReadToEnd();
                Input input = serializer.deserialize<Input>(strReader);
                return input;
            }
        }

        public void Stop()
        {
            httpListener.Stop();
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
