using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManager.Models
{
    public class OutsideReservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [DisplayName("氏名")]
        public string GuestName { get; set; }
        [DisplayName("フリガナ")]
        public string Furigana { get; set; }
        [DisplayName("人数")]
        public int NumOfGuests { get; set; }
        [DisplayName("新入生")]
        public int NumOfFreshmen { get; set; }
        [DisplayName("新入生以外")]
        public int NumOfOthers { get; set; }
        [Required]
        public string DramaName { get; set; }
        [Required]
        [DisplayName("ステージ")]
        public int StageNum { get; set; }
        [DisplayName("メールアドレス")]
        public string Email { get; set; }
        [DisplayName("電話番号")]
        public string PhoneNumber { get; set; }
        [DisplayName("備考")]
        public string Remarks { get; set; }
        public string Time { get; set; }

        [ForeignKey("DramaName,StageNum")]
        public Stage Stage { get; set; }

        public OutsideReservation()
        {
            NumOfFreshmen = 0;
        }
    }
}
