using Taller.DTOs;

namespace Taller.Services.Interfaces
{
    public interface ITaskService
    {
        Task<TaskResponseDTO> CreateTaskAsync(CreateTaskDTO createTaskDTO);

        Task<bool> DeleteTaskAsync(int id);

        Task<IEnumerable<TaskResponseDTO>> GetAllTasksAsync();

        Task<TaskResponseDTO> UpdateTaskAsync(int taskId, UpdateTaskDTO updateTaskDTO);
    }
}
