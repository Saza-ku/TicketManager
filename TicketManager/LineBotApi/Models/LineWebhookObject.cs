using System.Collections.Generic;

namespace TicketManager.LineBotApi.Models
{
    public class LineWebhookObject
    {
        public string destination;
        public List<Event> events;
    }
}
