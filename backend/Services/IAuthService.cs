namespace plzwork.Services;

using plzwork.Models.Dtos;

public interface IAuthService
{
    Task<RegisterUserResult> RegisterUserAsync(RegisterDto registerDto);

    Task<LoginUserResult> LoginUserAsync(LoginDto loginDto);
};