namespace plzwork.Services;

using plzwork.Models;
using plzwork.Models.Dtos;
using plzwork.Repositories;

public class TasksService(ITasksRepository repository) : ITasksService
{
    public async Task<List<TodoDto>> GetTasksAsync()
    {
        var tasks = await repository.GetTasksAsync();

        return tasks.Select(task => ToDto(task)).ToList();
    }

    public async Task<TodoDto> AddTaskAsync(CreateTodoDto taskDto)
    {
        var task = new Todo(0, taskDto.Name, taskDto.DueDate, taskDto.IsCompleted);
        var created = await repository.AddTaskAsync(task);
        return ToDto(created);
    }

    public async Task<TodoDto?> GetTaskByIdAsync(int id)
    {
        var task = await repository.GetTaskByIdAsync(id);

        return task is null ? null : ToDto(task);

    }

    public async Task DeleteTaskByIdAsync(int id)
    {
        await repository.DeleteTaskByIdAsync(id);
    }

    private static TodoDto ToDto(Todo task) => 
    new(task.Id, task.Name, task.DueDate, task.IsCompleted);
};