using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TicketManager.Data;

namespace TicketManager.Models
{
    public class Stage
    {
        [Required]
        [Key]
        public int Num { get; set; }
        [Required]
        public string Time { get; set; }
        [Required]
        public int Max { get; set; }
        [Required]
        [ForeignKey("Drama")]
        public string DramaName { get; set; }

        [NotMapped]
        public int CountOfGuests { get; set; }

        public Drama Drama { get; set; }

        public void CountGuests(TicketContext context)
        {
            var drama = context.Dramas
                .AsNoTracking().
                FirstOrDefault(d => d.Name == DramaName);
            var memberReservations = context.MemberReservations
                    .Where(r => r.DramaName == DramaName && r.StageNum == Num)
                    .ToArray();
            var outsideReservations = context.OutsideReservations
                .Where(r => r.DramaName == DramaName && r.StageNum == Num)
                .ToArray();

            int count = 0;
            if (drama.IsShinkan)
            {
                foreach (MemberReservation r in memberReservations)
                {
                    count += r.NumOfFreshmen + r.NumOfOthers;
                }
                foreach (OutsideReservation r in outsideReservations)
                {
                    count += r.NumOfFreshmen + r.NumOfOthers;
                }
            }
            else
            {
                count += memberReservations.Select(r => r.NumOfGuests).Sum();
                count += outsideReservations.Select(r => r.NumOfGuests).Sum();
            }

            CountOfGuests = count;
        }
    }
}
