using Microsoft.EntityFrameworkCore;
using DotnetTodoApp.Data;
using DotnetTodoApp.Models;

namespace DotnetTodoApp.Repositories;

public class UsersRepository(AppDbContext context) : IUsersRepository
{
    public async Task<bool> UserExistsAsync(string username)
    {
        return await context.Users.AnyAsync(user => user.Username == username);
    }

    public async Task AddUserAsync(User user)
    {
        context.Users.Add(user);

        await context.SaveChangesAsync();
    }

    public async Task<User?> GetUserAsync(string username)
    {
        return await context.Users.SingleOrDefaultAsync(u => u.Username == username);
    }
}