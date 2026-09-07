namespace plzwork.Repositories;

using plzwork.Models;

public interface ITasksRepository
{
    Task<List<Todo>> GetTasksAsync(int userId);

    Task<List<Todo>> GetAllTasksAsync();

    Task<Todo> AddTaskAsync(Todo task);

    Task<Todo?> GetTaskByIdAsync(int id, int userId);

    Task<bool> DeleteTaskByIdAsync(int id, int userId);

    Task SaveChangesAsync();
};