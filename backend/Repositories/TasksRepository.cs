namespace plzwork.Repositories;

using Microsoft.EntityFrameworkCore;
using plzwork.Data;
using plzwork.Models;

public class TasksRepository(AppDbContext context) : ITasksRepository
{
    public async Task<List<Todo>> GetTasksAsync(int userId)
    {
        return await context.Todos
            .Where(todo => todo.UserId == userId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Todo>> GetAllTasksAsync()
    {
        return await context.Todos
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Todo> AddTaskAsync(Todo task)
    {
        context.Todos.Add(task);
        await context.SaveChangesAsync();
        return task;
    }

    public async Task<Todo?> GetTaskByIdAsync(int id, int userId)
    {
        return await context.Todos
            .SingleOrDefaultAsync(task => task.Id == id && task.UserId == userId);
    }

    public async Task<bool> DeleteTaskByIdAsync(int id, int userId)
    {
        var taskById = await context.Todos
            .SingleOrDefaultAsync(task => task.Id == id && task.UserId == userId);
        
        if(taskById is not null)
        {
            context.Todos.Remove(taskById);
            await context.SaveChangesAsync();
            return true;
        } else
        {
            return false;
        }
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

}