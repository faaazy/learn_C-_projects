using plzwork.Models;
using plzwork.Models.Dtos;
using plzwork.Repositories;

namespace plzwork.Services;

public class AuthService(IUsersRepository usersRepository) : IAuthService
{
    public async Task<RegisterUserResult> RegisterUserAsync(RegisterDto registerDto)
    {
        var userExists = await usersRepository.UserExistsAsync(registerDto.Username);

        if(userExists)
        {
            return new RegisterUserResult(RegisterUserStatus.UsernameAlreadyExists);
        }

        var newUser = new User
        {
            Username = registerDto.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
            Role = UserRole.User
        };
        
        await usersRepository.AddUserAsync(newUser);

        return new RegisterUserResult(RegisterUserStatus.Success);
    }
}