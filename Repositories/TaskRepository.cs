using Microsoft.Extensions.Caching.Memory;
using Taller.DTOs;
using Taller.Models;
using Taller.Repositories.Interfaces;

namespace Taller.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IMemoryCache _cache;
        private const string TASKS_CACHE_KEY = "all_tasks";

        public TaskRepository(IMemoryCache cache)
        {
            _cache = cache;

            if (!_cache.TryGetValue(TASKS_CACHE_KEY, out _))
            {
                _cache.Set(TASKS_CACHE_KEY, new List<TaskItem>(), TimeSpan.FromHours(24));
            }
        }

        public async Task<TaskItem> CreateTaskAsync(TaskItem taskItem)
        {
            var tasks = _cache.Get<List<TaskItem>>(TASKS_CACHE_KEY) ?? new List<TaskItem>();

            taskItem.Id = await GetNextIdAsync();

            tasks.Add(taskItem);
            _cache.Set(TASKS_CACHE_KEY, tasks, TimeSpan.FromHours(24));

            return await Task.FromResult(taskItem);
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var tasks = _cache.Get<List<TaskItem>>(TASKS_CACHE_KEY) ?? new List<TaskItem>();
            var taskToRemove = tasks.FirstOrDefault(t => t.Id == id);

            if (taskToRemove != null)
            {
                tasks.Remove(taskToRemove);

                _cache.Set(TASKS_CACHE_KEY, tasks, TimeSpan.FromHours(24));

                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
        {
            var tasks = _cache.Get<List<TaskItem>>(TASKS_CACHE_KEY) ?? new List<TaskItem>();

            return await Task.FromResult(tasks.AsEnumerable());
        }

        private async Task<int> GetNextIdAsync()
        {
            var tasks = _cache.Get<List<TaskItem>>(TASKS_CACHE_KEY) ?? new List<TaskItem>();

            return await Task.FromResult(tasks.Count + 1);
        }

        public async Task<TaskItem> UpdateTaskAsync(int taskId, UpdateTaskDTO updateTaskDTO)
        {
            var tasks = _cache.Get<List<TaskItem>>(TASKS_CACHE_KEY) ?? new List<TaskItem>();
            var existingTask = tasks.FirstOrDefault(t => t.Id == taskId);

            if (existingTask != null)
            {
                existingTask.Status = updateTaskDTO.Status;
                _cache.Set(TASKS_CACHE_KEY, tasks, TimeSpan.FromHours(24));
            }
            else
            {
                throw new KeyNotFoundException($"Task with Id {taskId} not found.");
            }

            return await Task.FromResult(existingTask);
        }
    }
}
