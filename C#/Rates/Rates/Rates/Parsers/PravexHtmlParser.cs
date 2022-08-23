using Rates.Models;
using System;
using System.Collections.Generic;

namespace Rates.Parsers
{
    /// <summary>
    /// Retrieves and pars bank rates from Pravex Bank's URL
    /// </summary>
    public class PravexHtmlParser
    {
        const string stringUSD = "Долар США";
        const string stringEURO = "Євро";

        string html { get; set; }

        public PravexHtmlParser(string html)
        {
            this.html = html;
        }

        public PravexHttpResultModel GetRates()
        {
            var pravexHttpResultModel = new PravexHttpResultModel 
            { 
                PravexRates = new BankRates { DT = DateTime.Today} 
            };

            var patternsToRemove = new List<string>
            {
                "</div> ",
                "<div class=\"th-cell\">",
                "<div class=\"td-cell\">",
                "<div class=\"row\">",
                "id=\"base_pravex\" role=\"tabpanel\" aria-labelledby=\"\">",
                "<div class=\"table\" style=\"width: 50 %; \">",
                "<div class=\"title\">",
                "<div class=\"value\" style=\"text-align: right; \">",
                "<div class=\"table\" style=\"width: 50 %;\">",
                "<div class=\"value\" style=\"text-align: right;\">",
                "<div class=\"tab-pane\" id=\"base_nbu\" role=\"tabpanel\" aria-labelledby=\"\">",
                "<span class=\" increase \">",
                "<span class=\" \">",
                "<span class=\" decrease \">"
            };

            foreach (var pattern in patternsToRemove)
            {
                html = html.Replace(pattern, "");
            }

            //trims redundant info
            var stopPattern = "<div class=\"payment-systems\">";
            var index = html.IndexOf(stopPattern);

            html = html.Substring(0, index).Replace("</span>", "|");

            stopPattern = "Валюта Купівля Продаж";
            index = html.IndexOf(stopPattern);

            html = html.Substring(index + stopPattern.Length);

            stopPattern = "<div class=\"table\"";
            index = html.IndexOf(stopPattern);

            html = html.Substring(0, index);
            
            var empty = "  ";
            html = html.Replace(empty, "@");
            html = html.Replace("|", " ").Substring(1);

            //convert to model
            var array = html.Split('@');

            foreach (var item in array)
            {
                //USD

                if (item.Contains(stringUSD))
                {
                    var subItem = item.Replace(stringUSD, "").Trim().Split(" ");

                    pravexHttpResultModel.PravexRates.buy_USD = Convert.ToDecimal(subItem[0]);
                    pravexHttpResultModel.PravexRates.sell_USD = Convert.ToDecimal(subItem[2]);
                }

                //EURO

                else if (item.Contains(stringEURO))
                {
                    var subItem = item.Replace(stringEURO, "").Trim().Split(" ");

                    pravexHttpResultModel.PravexRates.buy_EURO = Convert.ToDecimal(subItem[0]);
                    pravexHttpResultModel.PravexRates.sell_EURO = Convert.ToDecimal(subItem[2]);
                }
            }

            return pravexHttpResultModel;
        }
    }
}
