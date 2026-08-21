namespace plzwork.Repositories;

using plzwork.Models;

public interface ITasksRepository
{
    Task<List<Todo>> GetTasksAsync();

    Task<Todo> AddTaskAsync(Todo task);

    Task<Todo?> GetTaskByIdAsync(int id);

    Task DeleteTaskByIdAsync(int id);

    Task UpdateTaskAsync(Todo task);
};