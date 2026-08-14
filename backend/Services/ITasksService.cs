namespace plzwork.Services;

using plzwork.Models;

public interface ITasksService
{
    Task<List<Todo>> GetTasksAsync();

    Task<Todo> AddTaskAsync(Todo Task);

    Task<Todo?> GetTaskByIdAsync(int id);

    Task DeleteTaskByIdAsync(int id);
};