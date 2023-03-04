using SuggestionPanel.Domain.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace SuggestionPanel.Domain.DTOs
{
    public class SuggestionRequest
    {
        public string Problem { get; set; }
        public string Solution { get; set; }

        public string StationNumber { get; set; }
        public bool IsCardAnomaly { get; set; }

        [Display(Name = "Card Number")]
        public int OwnerCardNumber { get; set; }
        
        [Display(Name = "Cost")]
        public virtual Cost? Cost { get; set; }
        [Display(Name = "Cost")]
        public int CostId { get; set; }

        public int ValueStreamId { get; set; }
    }
}
