using Rates.Process;
using System;
using System.Threading.Tasks;

namespace Rates
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IRateProcessor processor = new RateProcessor();

            var results = await processor.Run();

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(results);

            Console.WriteLine(json);

            Console.WriteLine("---");
        }
    }
}
