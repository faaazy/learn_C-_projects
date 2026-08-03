namespace plzwork.Controllers;

using Microsoft.AspNetCore.Mvc;
using plzwork.Models;
using plzwork.Services;

[ApiController]
[Route("tasks")]
public class TasksController : ControllerBase
{
    private readonly ITasksService _tasksService;

    public TasksController(ITasksService tasksService)
    {
        _tasksService = tasksService;
    }

    [HttpGet]
    public ActionResult<List<Todo>> GetAll()
    {
        return Ok(_tasksService.GetTasks());
    }

    [HttpGet("{id}")]
    public ActionResult<Todo> GetById(int id)
    {
        var task = _tasksService.GetTaskById(id);
        return task is null ? NotFound() : Ok(task);
    }

    [HttpPost]
    public ActionResult<Todo> Create(Todo task)
    {
        var createdTask = _tasksService.AddTask(task);
        return Created($"/tasks/{createdTask.Id}", createdTask);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _tasksService.DeleteTaskById(id);
        return NoContent();
    }

}