using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TicketManager.Models
{
    public class CreateDramaModel
    {
        [DisplayName("公演名")]
        public string DramaName { get; set; }
        [DisplayName("新歓公演の場合チェックをつけてください")]
        public bool IsShinkan { get; set; }
        [DisplayName("ステージ数")]
        [Range(0, 366)]
        public int NumOfStage { get; set; }
    }
}
