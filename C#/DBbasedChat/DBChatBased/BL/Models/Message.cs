using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Models
{
    public class Message
    {
        public int id { get; set; }
        public int ChatId { get; set; }
        public int AuthorUserId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
