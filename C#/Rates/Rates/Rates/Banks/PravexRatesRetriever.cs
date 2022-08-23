using Rates.Models;
using Rates.Requests;
using System.Threading.Tasks;

namespace Rates.Banks
{
    /// <summary>
    /// Retrieves currency Rates from PRAVEX Bank
    /// </summary>
    public class PravexRatesRetriever : BaseBankRetriever
    {
        public PravexRatesRetriever()
        {
            BankName = "Pravex";
        }

        /// <summary>
        /// PRAVEX
        /// https://www.pravex.com.ua/kursy-valyut
        /// </summary>        
        public override async Task<BankRates> GetRates()
        {
            var url = @"https://www.pravex.com.ua/kursy-valyut";

            var rate = (new GetRequestPravex(url)).Execute();

            rate.PravexRates.BankName = BankName;

            return rate.PravexRates;
        }
    }
}
