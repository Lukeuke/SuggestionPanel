using System.ComponentModel.DataAnnotations;

namespace SuggestionPanel.Domain.Models
{
    public class HumanResources
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Surname { get; set; }
        public int CardNumber { get; set; }
    }
}
