using System.ComponentModel.DataAnnotations;

namespace SuggestionPanel.Domain.Models
{
    public class Cost
    {
        [Key]
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
