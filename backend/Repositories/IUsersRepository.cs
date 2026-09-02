using plzwork.Models;

namespace plzwork.Repositories;

public interface IUsersRepository
{
    Task<bool> UserExistsAsync(string username);

    Task AddUserAsync(User user);

    Task<User?> GetUserAsync(string username);
}