using System;
using System.ComponentModel.DataAnnotations;

namespace TicketManager.Models
{
    public class IdInputModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
