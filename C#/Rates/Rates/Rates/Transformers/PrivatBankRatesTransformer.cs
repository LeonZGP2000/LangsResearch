using Rates.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Rates.Transformers
{
    public class PrivatBankRatesTransformer
    {
        /// <summary>
        /// Transforms currency rates form Privat Api to BankRates model
        /// </summary>
        public async Task<BankRates> TransformBankRates(PrivatHttpResultModel privatSiteRanks)
        {
            var usdData = privatSiteRanks.Rates.FirstOrDefault(r => r.ccy == "USD");
            var euroData = privatSiteRanks.Rates.FirstOrDefault(r => r.ccy == "EUR");

            return new BankRates
            {
                buy_EURO = Convert.ToDecimal(euroData.buy),
                buy_USD = Convert.ToDecimal(usdData.buy),
                sell_EURO = Convert.ToDecimal(euroData.sale),
                sell_USD = Convert.ToDecimal(usdData.sale),
                DT = DateTime.Today
            };
        }
    }
}
