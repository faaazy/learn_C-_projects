namespace plzwork.Services;

using plzwork.Models.Dtos;

public interface ITasksService
{
    Task<List<TodoDto>> GetTasksAsync();

    Task<TodoDto> AddTaskAsync(CreateTodoDto task);

    Task<TodoDto?> GetTaskByIdAsync(int id);

    Task DeleteTaskByIdAsync(int id);

    Task<TodoDto?> UpdateTaskAsync(int id, UpdateTodoDto taskDto);
};