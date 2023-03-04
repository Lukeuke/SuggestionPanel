using System.ComponentModel.DataAnnotations;

namespace SuggestionPanel.Domain.DTOs
{
    public class ValueStreamResponsibilityRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }
        public int ValueStreamId { get; set; }
    }
}
