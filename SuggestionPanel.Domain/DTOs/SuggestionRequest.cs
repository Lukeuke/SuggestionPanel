using SuggestionPanel.Domain.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace SuggestionPanel.Domain.DTOs
{
    public class SuggestionRequest
    {
        [Required]
        public string Problem { get; set; }
        public string? Solution { get; set; }

        [Display(Name = "Station Number")]
        public string StationNumber { get; set; }

        [Display(Name = "Card Anomaly?")]
        public bool IsCardAnomaly { get; set; }

        [Display(Name = "Card Number")]
        public int OwnerCardNumber { get; set; }
        
        [Display(Name = "Cost")]
        public virtual Cost? Cost { get; set; }
        [Display(Name = "Cost")]
        public int CostId { get; set; }

        [Display(Name = "Value Stream")]
        public int ValueStreamId { get; set; }
    }
}
