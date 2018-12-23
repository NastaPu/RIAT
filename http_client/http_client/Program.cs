using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace http_client
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client(Console.ReadLine());
            while (client.Ping() != true) ;

            Input input = client.getInputData();
            Output output = mathOperations(input);

            client.writeAnswer(output);
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
