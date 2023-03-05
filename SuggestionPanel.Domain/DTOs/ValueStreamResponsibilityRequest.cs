using System.ComponentModel.DataAnnotations;

namespace SuggestionPanel.Domain.DTOs
{
    public class ValueStreamResponsibilityRequest
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Surname { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public required string PasswordHash { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public required string EmailAddr { get; set; }

        [Required]
        public required string Number { get; set; }

        [Display(Name = "Value Stream")]
        public int ValueStreamId { get; set; }
    }
}
