namespace Rates.Models
{
    public class PrivatHttpResultModel
    {
        public PrivatRate[] Rates { get; set; }
    }

    public class PrivatRate
    {
        public string ccy { get; set; }
        public string base_ccy { get; set; }
        public string buy { get; set; }
        public string sale { get; set; }
    }

}
