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
        var task = new Todo{
            Name = taskDto.Name, 
            DueDate = taskDto.DueDate.Value, 
            IsCompleted = false
        };

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

    public async Task<TodoDto?> UpdateTaskAsync(int id, UpdateTodoDto taskDto)
    {
        var task = await repository.GetTaskByIdAsync(id);
        if(task is null)
        {
            return null;
        } 

        task.Name = taskDto.Name;
        task.DueDate = taskDto.DueDate.Value;
        task.IsCompleted = taskDto.IsCompleted;

        await repository.UpdateTaskAsync(task);

        return ToDto(task);
    }

    private static TodoDto ToDto(Todo task) => 
    new(task.Id, task.Name, task.DueDate, task.IsCompleted);
};