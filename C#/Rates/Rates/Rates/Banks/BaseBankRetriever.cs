using Rates.Models;
using System.Threading.Tasks;

namespace Rates.Banks
{
    /// <summary>
    /// Represent defaul Bank
    /// </summary>
    public abstract class BaseBankRetriever
    {
        public string BankName { get; set; }

        public abstract Task<BankRates> GetRates();
    }
}
