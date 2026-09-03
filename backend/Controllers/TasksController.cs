namespace plzwork.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using plzwork.Models.Dtos;
using plzwork.Services;

[ApiController]
[Route("tasks")]
[Authorize]
public class TasksController(ITasksService tasksService) : ControllerBase
{
    private readonly ITasksService _tasksService = tasksService;

    [HttpGet]
    public async Task<ActionResult<List<TodoDto>>> GetAll()
    {
        var tasks = await _tasksService.GetTasksAsync();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoDto>> GetById(int id)
    {
        var task = await _tasksService.GetTaskByIdAsync(id);
        return task is null ? NotFound() : Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<TodoDto>> Create(CreateTodoDto task)
    {
        var createdTask = await _tasksService.AddTaskAsync(task);
        return Created($"/tasks/{createdTask.Id}", createdTask);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var isDeleted = await _tasksService.DeleteTaskByIdAsync(id);

        return isDeleted ? NoContent() : NotFound();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TodoDto>> Update(int id, UpdateTodoDto taskDto)
    {
        var task = await _tasksService.UpdateTaskAsync(id, taskDto);

        switch (task.Status)
        {
            case UpdateTaskStatus.Success: 
                return Ok(task.Todo);
            
            case UpdateTaskStatus.AlreadyCompleted:
                return Conflict("This task is already completed");

            case UpdateTaskStatus.NotFound:
                return NotFound();

            default: 
                return BadRequest();
        };
    }

    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<TodoDto>>> GetAllForAdmin()
    {
        var tasks = await _tasksService.GetTasksAsync();
        return Ok(tasks);
    }
}