namespace DotnetTodoApp.Services;

using DotnetTodoApp.Models.Dtos;

public interface IAuthService
{
    Task<RegisterUserResult> RegisterUserAsync(RegisterDto registerDto);

    Task<LoginUserResult> LoginUserAsync(LoginDto loginDto);
};