using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddSingleton<ITasksService>(new TasksService());

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/tasks", (ITasksService service) => service.GetTasks());

app.MapPost("/tasks", (Todo task, ITasksService service) =>
{
    service.AddTask(task);
    return TypedResults.Created($"/tasks/{task.Id}", task);
});

app.MapGet("/tasks/{id}", Results<Ok<Todo>, NotFound> (int id, ITasksService service) =>
{
    var selectedTodo = service.GetTaskById(id);
    return selectedTodo is null ? TypedResults.NotFound() : TypedResults.Ok(selectedTodo);
});

app.MapDelete("/tasks/{id}", (int id, ITasksService service) =>
{
    service.DeleteTaskById(id);
    return TypedResults.NoContent();
});

app.Run();

interface ITasksService
{
    List<Todo> GetTasks();

    Todo AddTask(Todo Task);

    Todo? GetTaskById(int id);

    void DeleteTaskById(int id);
};

public class TasksService : ITasksService
{
  private readonly List<Todo> _tasks = [];

  public List<Todo> GetTasks()
    {
        return _tasks;
    }

    public Todo AddTask(Todo task)
    {
        _tasks.Add(task);
        return task;
    }

    public Todo? GetTaskById(int id)
    {
        return _tasks.SingleOrDefault(task => task.Id == id);
    }

    public void DeleteTaskById(int id)
    {
        _tasks.RemoveAll(task => task.Id == id);
    }
};


public record Todo(int Id, string Name, DateTime DueDate, bool IsCompleted);
