using System.ComponentModel.DataAnnotations;

namespace Taller.DTOs
{
    public class UpdateTaskDTO
    {
        [Required]
        public Models.TaskStatus Status { get; set; }
    }
}
