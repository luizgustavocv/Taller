using System.ComponentModel.DataAnnotations;

namespace Taller.DTOs
{
    public class CreateTaskDTO
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        public DateTime? DueDate { get; set; }
    }
}
