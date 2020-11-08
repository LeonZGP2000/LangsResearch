using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Models
{
    public class User
    {
        public int id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool isBloked { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
