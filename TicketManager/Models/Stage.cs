﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    }
}
