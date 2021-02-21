using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CsvHelper.Configuration.Attributes;
using Microsoft.AspNetCore.Identity;

namespace TicketManager.Models
{
    public class MemberReservation
    {
        [Ignore]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [DisplayName("氏名")]
        public string GuestName { get; set; }
        [DisplayName("フリガナ")]
        public string Furigana { get; set; }
        [DisplayName("人数")]
        public int NumOfGuests { get; set; } = 0;
        [DisplayName("新入生")]
        public int NumOfFreshmen { get; set; } = 0;
        [DisplayName("新入生以外")]
        public int NumOfOthers { get; set; } = 0;
        [Ignore]
        [Required]
        public string DramaName { get; set; }
        [Required]
        [DisplayName("ステージ")]
        public int StageNum { get; set; }
        [DisplayName("団員名")]
        public string MemberName { get; set; } = "";
        [Ignore]
        public string MemberId { get; set; } = "";

        [Ignore]
        [ForeignKey("DramaName,StageNum")]
        public Stage Stage { get; set;}
    }
}