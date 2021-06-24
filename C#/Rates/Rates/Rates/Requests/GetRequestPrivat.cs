using Rates.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Rates.Requests
{
    public class GetRequestPrivat
    {
        Uri Uri { get; set; }
        string Currency { get; set; }

        public GetRequestPrivat(string url)
        {
            this.Uri = new Uri(url);
        }

        public PrivatHttpResultModel Execute()
        {
            var request = WebRequest.Create(Uri.ToString());
            request.Method = "GET";

            using var webResponse = request.GetResponse();
            using var webStream = webResponse.GetResponseStream();

            using var reader = new StreamReader(webStream);
            var data = reader.ReadToEnd();

            var m = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PrivatRate>>(data);

            return new PrivatHttpResultModel {Rates = m.ToArray() };
        }
    }
}
