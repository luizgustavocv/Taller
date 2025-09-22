using System.ComponentModel.DataAnnotations;

namespace Taller.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        public DateTime? DueDate { get; set; } = DateTime.UtcNow;

        public TaskStatus Status { get; set; } = TaskStatus.Pending;
    }

    public enum TaskStatus
    {
        Pending,
        InProgress,
        Completed
    }
}
