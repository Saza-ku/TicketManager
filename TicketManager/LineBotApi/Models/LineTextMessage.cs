using System;
namespace TicketManager.LineBotApi.Models
{
    public class LineTextMessage
    {
        public string[] to;
        public Message[] messages;
        public bool notificationDisabled;
    }
}
