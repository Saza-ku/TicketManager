using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TicketManager.Models
{
    public class Drama
    {
        [Key]
        [Required]
        [DisplayName("公演名")]
        public string Name { get; set; }
        [Required]
        [DisplayName("新歓公演")]
        public bool IsShinkan { get; set; }
        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }
    }
}
