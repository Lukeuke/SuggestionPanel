using System.ComponentModel.DataAnnotations;

namespace SuggestionPanel.Domain.DTOs
{
    public class AdminRequest
    {
        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
