namespace plzwork.Repositories;

using Microsoft.EntityFrameworkCore;
using plzwork.Data;
using plzwork.Models;

public class TasksRepository(AppDbContext context) : ITasksRepository
{
    public async Task<List<Todo>> GetTasksAsync()
    {
        return await context.Todos.ToListAsync();
    }

    public async Task<Todo> AddTaskAsync(Todo task)
    {
        context.Todos.Add(task);
        await context.SaveChangesAsync();
        return task;
    }

    public async Task<Todo?> GetTaskByIdAsync(int id)
    {
        return await context.Todos.SingleOrDefaultAsync(task => task.Id == id);
    }

    public async Task DeleteTaskByIdAsync(int id)
    {
        var taskById = await context.Todos.SingleOrDefaultAsync(task => task.Id == id);
        
        if(taskById is not null)
        {
            context.Todos.Remove(taskById);
            await context.SaveChangesAsync();
        }
    }

    public async Task UpdateTaskAsync(Todo task)
    {
        context.Todos.Update(task);
        await context.SaveChangesAsync();
    }

}