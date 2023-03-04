using System.ComponentModel.DataAnnotations;

namespace SuggestionPanel.Domain.Models
{
    public class ValueStreamResponsibility
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        [Required]
        public string Surname { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string Salt { get; set; }

        public virtual ValueStream ValueStream { get; set; }
        [Required]
        public int ValueStreamId { get; set; }
    }
}
