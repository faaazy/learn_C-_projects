namespace DotnetTodoApp.Controllers;

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DotnetTodoApp.Models.Dtos;
using DotnetTodoApp.Services;

[ApiController]
[Route("tasks")]
[Authorize]
public class TasksController(ITasksService tasksService) : ControllerBase
{
    private readonly ITasksService _tasksService = tasksService;

    [HttpGet]
    public async Task<ActionResult<List<TodoDto>>> GetAll()
    {
        var userId = GetCurrentUserId();

        if(userId is null) return Unauthorized();

        var tasks = await _tasksService.GetTasksAsync(userId.Value);
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoDto>> GetById(int id)
    {
        var userId = GetCurrentUserId();

        if(userId is null) return Unauthorized();

        var task = await _tasksService.GetTaskByIdAsync(id, userId.Value);
        return task is null ? NotFound() : Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<TodoDto>> Create(CreateTodoDto task)
    {
        var userId = GetCurrentUserId();

        if(userId is null) return Unauthorized();

        var createdTask = await _tasksService.AddTaskAsync(task, userId.Value);
        return Created($"/tasks/{createdTask.Id}", createdTask);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = GetCurrentUserId();

        if(userId is null) return Unauthorized();

        var isDeleted = await _tasksService.DeleteTaskByIdAsync(id, userId.Value);

        return isDeleted ? NoContent() : NotFound();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TodoDto>> Update(int id, UpdateTodoDto taskDto)
    {
        var userId = GetCurrentUserId();

        if(userId is null) return Unauthorized();

        var task = await _tasksService.UpdateTaskAsync(id, taskDto, userId.Value);

        return task is null ? NotFound() : Ok(task) ;
    }

    [HttpPatch("{id}/status")]
    public async Task<ActionResult<TodoDto>> UpdateStatus(int id, UpdateTodoStatusDto updateTodoStatus)
    {
        var userId = GetCurrentUserId();

        if(userId is null) return Unauthorized();

        var task = await _tasksService.UpdateTodoStatusAsync(id, updateTodoStatus.IsCompleted, userId.Value);

        return task is null ? NotFound() : Ok(task) ;
    }


    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<TodoDto>>> GetAllForAdmin()
    {
        var tasks = await _tasksService.GetAllTasksAsync();
        return Ok(tasks);
    }

    private int? GetCurrentUserId()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier);

        return int.Parse(userId.Value);
    }
}