using System;
namespace TicketManager.Models
{
    public class StageViewModel
    {
        public Stage Stage { get; set; }
        public MemberResercationModel[] MemberReservations { get; set; }
        public OutsideReservation[] OutsideReservations { get; set; }
    }
}
