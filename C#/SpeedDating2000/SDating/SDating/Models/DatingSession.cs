using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SDating.Models
{
    public class DatingSession
    {
        public int SessionID { get; set; }
        [DataType(DataType.Date)]
        public DateTime Dt { get; set; }        
        public int MansCount { get; set; }//TODO
        public int GirlsCount { get; set; }//TODO
        public List<PersonalBlanc> PersonalBlancs { get; set; }
    }
}
