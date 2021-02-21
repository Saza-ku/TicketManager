using System;
using System.Collections.Generic;

namespace TicketManager.LineBotApi.Models
{
    public class LineTextReply
    {
        public string replyToken;
        public List<Message> messages;
        public bool notificationDisabled;
    }
}
