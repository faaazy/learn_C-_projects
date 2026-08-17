namespace plzwork.Controllers;

using Microsoft.AspNetCore.Mvc;
using plzwork.Models;
using plzwork.Models.Dtos;
using plzwork.Services;

[ApiController]
[Route("tasks")]
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
        await _tasksService.DeleteTaskByIdAsync(id);
        return NoContent();
    }

}