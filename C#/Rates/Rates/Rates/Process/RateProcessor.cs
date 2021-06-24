using Rates.Banks;
using Rates.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rates.Process
{
    public sealed class RateProcessor : IRateProcessor
    {
        public async Task<IEnumerable<BankRates>> Run()
        {
            var rates = new List<BankRates>();

            var retrievers = new BaseBankRetriever[]
             {
                 new PravexRatesRetriever(),
                 new PrivatRatesRetriever()
             };

            foreach (var bank in retrievers)
            {
                var rate = await bank.GetRates().ConfigureAwait(false);

                rates.Add(rate);
            }

            return rates;
        }
    }
}
