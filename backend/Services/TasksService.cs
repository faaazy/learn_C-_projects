namespace plzwork.Services;

using plzwork.Models;
using plzwork.Repositories;

public class TasksService(ITasksRepository repository) : ITasksService
{
    public async Task<List<Todo>> GetTasksAsync()
    {
        return await repository.GetTasksAsync();
    }

    public async Task<Todo> AddTaskAsync(Todo task)
    {
        return await repository.AddTaskAsync(task);
    }

    public async Task<Todo?> GetTaskByIdAsync(int id)
    {
        return await repository.GetTaskByIdAsync(id);
    }

    public async Task DeleteTaskByIdAsync(int id)
    {
        await repository.DeleteTaskByIdAsync(id);
    }
};