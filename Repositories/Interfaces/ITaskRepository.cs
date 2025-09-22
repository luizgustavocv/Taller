using Taller.DTOs;
using Taller.Models;

namespace Taller.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskItem> CreateTaskAsync(TaskItem taskItem);

        Task<bool> DeleteTaskAsync(int id);

        Task<IEnumerable<TaskItem>> GetAllTasksAsync();

        Task<TaskItem> UpdateTaskAsync(int taskId, UpdateTaskDTO updateTaskDTO);
    }
}
