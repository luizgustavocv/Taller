using Taller.DTOs;
using Taller.Models;
using Taller.Repositories.Interfaces;
using Taller.Services.Interfaces;

namespace Taller.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<TaskResponseDTO> CreateTaskAsync(CreateTaskDTO createTaskDTO)
        {
            var taskItem = new TaskItem
            {
                Title = createTaskDTO.Title,
                Description = createTaskDTO.Description,
                DueDate = createTaskDTO.DueDate,
                Status = Models.TaskStatus.Pending
            };

            _ = await _repository.CreateTaskAsync(taskItem);

            return await Task.FromResult(MapToResponseDTO(taskItem));
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            return await _repository.DeleteTaskAsync(id);
        }

        private static TaskResponseDTO MapToResponseDTO(TaskItem taskItem)
        {
            return new TaskResponseDTO
            {
                Id = taskItem.Id,
                Title = taskItem.Title,
                Description = taskItem.Description,
                DueDate = taskItem.DueDate,
                Status = taskItem.Status.ToString()
            };
        }

        public async Task<IEnumerable<TaskResponseDTO>> GetAllTasksAsync()
        {
            var tasks = _repository.GetAllTasksAsync().Result;

            return await Task.FromResult(tasks.Select(MapToResponseDTO));
        }

        public async Task<TaskResponseDTO> UpdateTaskAsync(int taskId, UpdateTaskDTO updateTaskDTO)
        {
            var updatedTask = await _repository.UpdateTaskAsync(taskId, updateTaskDTO);

            return await Task.FromResult(MapToResponseDTO(updatedTask));
        }
    }
}
