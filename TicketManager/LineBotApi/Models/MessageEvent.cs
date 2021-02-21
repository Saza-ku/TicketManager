namespace TicketManager.LineBotApi.Models
{
    public class Event
    {
        public string replyToken;
        public string type;
        public object timestamp;
        public Source source;
        public Message message;
    }
}
