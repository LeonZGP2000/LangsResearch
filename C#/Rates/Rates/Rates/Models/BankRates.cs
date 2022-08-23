namespace Rates.Models
{
    public class BankRates
    {
        public System.DateTime DT { get; set; }
        public string BankName { get; set; }

        public decimal sell_USD { get; set; }
        public decimal sell_EURO { get; set; }

        public decimal buy_USD { get; set; }
        public decimal buy_EURO { get; set; }
    }
}
