using Microsoft.AspNetCore.Mvc;
using Taller.DTOs;
using Taller.Services.Interfaces;

namespace Taller.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateTask([FromBody] CreateTaskDTO createTaskDTO)
        {
            if (createTaskDTO == null)
            {
                return BadRequest("Task data is required.");
            }

            var createdTask = await _taskService.CreateTaskAsync(createTaskDTO);

            return CreatedAtAction(nameof(CreateTask), new { id = createdTask.Title }, createdTask);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            var success = await _taskService.DeleteTaskAsync(id);

            if (!success)
            {
                return NotFound($"Task with ID {id} not found.");
            }
            else
            {
                return Ok($"Task with ID {id} deleted successfully.");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskResponseDTO>>> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();

            return Ok(tasks);
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult> UpdateTask(int id, UpdateTaskDTO updateTaskDTO)
        {
            var updatedTask = await _taskService.UpdateTaskAsync(id, updateTaskDTO);

            return CreatedAtAction(nameof(CreateTask), new { id = updatedTask.Title }, updatedTask);
        }
    }
}
