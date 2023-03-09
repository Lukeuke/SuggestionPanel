using System.ComponentModel.DataAnnotations;

namespace SuggestionPanel.Domain.Models
{
    public class Suggestion
    {
        [Key]
        public int Id { get; set; }
        public required string Problem { get; set; }
        public string? Solution { get; set; }

        [MaxLength(100)]
        public required string StationNumber { get; set; }
        [Display(Name = "Submission Date")]
        public required DateTime DateOfSubmission { get; set; }
        [Display(Name = "Anomaly Card?")]
        public required bool IsCardAnomaly { get; set; }
        [Display(Name = "Implementation Desc.")]
        public string? ImplementationDesc { get; set; }
        public DateTime? PropositionDate { get; set; }
        public DateTime? ImplementationDate { get; set; }
        public bool? Delete { get; set; }
        public bool? ToCommittee { get; set; } = false;

        public int? Points { get; set; }
        public decimal? Money { get; set; }
        public DateTime? ReviewDate { get; set; }

        public bool? Accepted { get; set; }
        public bool? Archive { get; set; }

        public virtual HumanResources SubmissionOwner { get; set; }
        public int SubmissionOwnerId { get; set; }
        public virtual ValueStreamResponsibility SignedTo { get; set; }
        public int SignedToId { get; set; }
        public virtual Cost Cost { get; set; }
        public int CostId { get; set; }
    }
}
