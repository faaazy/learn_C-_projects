namespace DotnetTodoApp.Services;

using DotnetTodoApp.Models;
using DotnetTodoApp.Models.Dtos;
using DotnetTodoApp.Repositories;

public class TasksService(ITasksRepository repository) : ITasksService
{
    public async Task<List<TodoDto>> GetTasksAsync(int userId)
    {
        var tasks = await repository.GetTasksAsync(userId);

        return tasks.Select(task => ToDto(task)).ToList();
    }

    public async Task<List<TodoDto>> GetAllTasksAsync()
    {
        var tasks = await repository.GetAllTasksAsync();

        return tasks.Select(task => ToDto(task)).ToList();
    }

    public async Task<TodoDto> AddTaskAsync(CreateTodoDto taskDto, int userId)
    {
        var task = new Todo{
            UserId = userId,
            Name = taskDto.Name, 
            DueDate = taskDto.DueDate, 
            IsCompleted = false
        };

        var created = await repository.AddTaskAsync(task);
        return ToDto(created);
    }

    public async Task<TodoDto?> GetTaskByIdAsync(int id, int userId)
    {
        var task = await repository.GetTaskByIdAsync(id, userId);

        return task is null ? null : ToDto(task);
    }

    public async Task<bool> DeleteTaskByIdAsync(int id, int userId)
    {
        var isDeleted = await repository.DeleteTaskByIdAsync(id, userId);
        
        return isDeleted;
    }

    public async Task<TodoDto?> UpdateTaskAsync(int id, UpdateTodoDto taskDto, int userId)
    {
        var task = await repository.GetTaskByIdAsync(id, userId);

        if (task is null) return null;

        task.Name = taskDto.Name;
        task.DueDate = taskDto.DueDate;

        await repository.SaveChangesAsync();

        return ToDto(task);
    }

    public async Task<TodoDto?> UpdateTodoStatusAsync(int id, bool isCompleted, int userId)
    {
        var task = await repository.GetTaskByIdAsync(id, userId);

        if(task is null) return null;

        task.IsCompleted = isCompleted;

        await repository.SaveChangesAsync();

        return ToDto(task);
    }

    private static TodoDto ToDto(Todo task) => 
    new(task.Id, task.Name, task.DueDate, task.IsCompleted);
};