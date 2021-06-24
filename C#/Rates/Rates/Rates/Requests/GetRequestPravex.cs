using Rates.Models;
using Rates.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Rates.Requests
{
    public class GetRequestPravex
    {
        Uri Uri { get; set; }
        string Currency { get; set; }

        public GetRequestPravex(string url)
        {
            this.Uri = new Uri(url);
        }

        public PravexHttpResultModel Execute()
        {
            var request = WebRequest.Create(Uri.ToString());
            request.Method = "GET";

            using var webResponse = request.GetResponse();
            using var webStream = webResponse.GetResponseStream();

            using var reader = new StreamReader(webStream);
            var htmlPage = reader.ReadToEnd();

            //copy
            char[] htmlPageCopyChars = new char[htmlPage.Length];            
            htmlPage.CopyTo(0, htmlPageCopyChars, 0, htmlPage.Length);
            var htmlPageCopy = new string(htmlPageCopyChars);

            //patterns
            var patternStart = "id=\"base_pravex\"";
            var patternStartIndex = htmlPageCopy.IndexOf(patternStart);

            var paternEnd = "</span></div> </div> </div> </div> </div>";
            var patternEndIndex = htmlPageCopy.IndexOf(paternEnd);

            //found block
            var blockHtml = htmlPage.Substring
                (patternStartIndex,
                (htmlPage.Length - patternStartIndex - patternEndIndex + paternEnd.Length)
                );

            return (new PravexHtmlParser(blockHtml)).GetRates();
        }
    }
}
