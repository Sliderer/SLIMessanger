using System;
using System.Collections.Generic;
using System.Text;

namespace SLIMessanger
{
    class Message
    {
        public string sender;
        public string recipient;
        public string text;
        public DateTime date;

        public Message(string sender, string recipient, string text, DateTime date)
        {
            this.sender = sender;
            this.recipient = recipient;
            this.text = text;
            this.date = date;
        }
    }
}
