using System.ComponentModel.DataAnnotations;

namespace SuggestionPanel.Domain.DTOs
{
    public class SuggestionCommitteeReviewRequest : SuggestionReviewRequest
    {
        [Required]
        public required int? Points { get; set; }
        [Required]
        public required decimal? Money { get; set; }
    }
}
