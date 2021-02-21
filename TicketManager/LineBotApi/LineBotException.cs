using System;
namespace TicketManager.LineBotApi
{
    public class LineBotException : Exception
    {
        public LineBotException(string message)
        : base(message)
        {
        }
    }
}
