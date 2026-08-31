using Microsoft.EntityFrameworkCore;
using plzwork.Data;
using plzwork.Models;

namespace plzwork.Repositories;

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

}