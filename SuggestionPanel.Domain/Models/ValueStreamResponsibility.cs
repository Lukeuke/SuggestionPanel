using System.ComponentModel.DataAnnotations;

namespace SuggestionPanel.Domain.Models
{
    public class ValueStreamResponsibility
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Surname { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public virtual ValueStream ValueStream { get; set; }
        public int ValueStreamId { get; set; }
    }
}
