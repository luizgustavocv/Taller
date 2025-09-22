﻿namespace Taller.DTOs
{
    public class TaskResponseDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime? DueDate { get; set; }
        
        public string Status { get; set; } = string.Empty;
    }
}
