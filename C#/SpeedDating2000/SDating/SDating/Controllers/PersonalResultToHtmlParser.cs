using SDating.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDating.Controllers
{
    public interface IPersonalResultToHtmlParsert
    {
        string ToHTML(PersonalResult p);
    }

    public class PersonalResultToHtmlParser : IPersonalResultToHtmlParsert
    {
        public string ToHTML(PersonalResult p)
        {
            return "";
        }
    }
}
