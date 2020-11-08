using System;
using System.Collections.Generic;

namespace BL.Models
{
    public class ChatView
    {
        public IList<MessageContent> Content;

        public ChatView()
        {
            Content = new List<MessageContent>();
        }
    }

    public class MessageContent
    {
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public string UserFrom { get; set; }

    }
}
