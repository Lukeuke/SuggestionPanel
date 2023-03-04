using System.ComponentModel.DataAnnotations;

namespace SuggestionPanel.Domain.Models
{
    public class ValueStream
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string AreaName { get; set; }
    }
}
