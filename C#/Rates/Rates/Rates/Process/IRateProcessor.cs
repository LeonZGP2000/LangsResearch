using Rates.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rates.Process
{
    /// <summary>
    /// Object Retrieves and modifies Rates,  which wereretrieved from URLs
    /// </summary>
    public interface IRateProcessor
    {
        Task<IEnumerable<BankRates>> Run();
    }
}
