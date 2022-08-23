using Rates.Models;
using Rates.Requests;
using Rates.Transformers;
using System.Threading.Tasks;

namespace Rates.Banks
{
    /// <summary>
    /// Retrieves currency Rates from Privat Bank
    /// </summary>
    public class PrivatRatesRetriever : BaseBankRetriever
    {
        public PrivatRatesRetriever()
        {
            BankName = "PrivatBank";
        }

        /// <summary>
        /// PRIVAT: 
        /// //https://www.liqpay.ua/documentation/en/api/public/exchange
        /// </summary>
        public async override Task<BankRates> GetRates()
        {
            var url = @"https://api.privatbank.ua/p24api/pubinfo?json&exchange&coursid=5";

            var rateHttpResult = (new GetRequestPrivat(url)).Execute();

            var privatTransformer = new PrivatBankRatesTransformer();

            var rates =  await privatTransformer
                .TransformBankRates(rateHttpResult);

            rates.BankName = BankName;

            return rates;
        }
    }
}
