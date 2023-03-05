using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SuggestionPanel.Domain.DTOs
{
    public class SuggestionReviewRequest
    {
        public required int Id { get; set; }
        public required string Problem { get; set; }
        public required string? Solution { get; set; }

        [Display(Name = "Station Number")]
        public required string StationNumber { get; set; }

        [Display(Name = "Submission Date")]
        public required DateTime DateOfSubmission { get; set; }

        [Display(Name = "Anomaly Card?")]
        public required bool IsCardAnomaly { get; set; }

        [Display(Name = "Implementation Description")]
        public string? ImplementationDesc { get; set; }

        [Display(Name = "Proposition Date")]
        public DateTime? PropositionDate { get; set; }
        [Display(Name = "Implementation Date")]
        public DateTime? ImplementationDate { get; set; }
        [Display(Name = "Delete?")]
        public bool? Delete { get; set; }
    }
}
