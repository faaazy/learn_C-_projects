namespace DotnetTodoApp.Services;

using DotnetTodoApp.Models.Dtos;

public interface ITasksService
{
    Task<List<TodoDto>> GetTasksAsync(int userId);

    Task<List<TodoDto>> GetAllTasksAsync();

    Task<TodoDto> AddTaskAsync(CreateTodoDto task, int userId);

    Task<TodoDto?> GetTaskByIdAsync(int id, int userId);

    Task<bool> DeleteTaskByIdAsync(int id, int userId);

    Task<TodoDto?> UpdateTaskAsync(int id, UpdateTodoDto taskDto, int userId);

    Task<TodoDto?> UpdateTodoStatusAsync(int id, bool isCompleted, int userId);
};