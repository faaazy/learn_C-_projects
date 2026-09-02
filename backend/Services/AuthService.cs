using System.Security.Claims;
using plzwork.Models;
using plzwork.Models.Dtos;
using plzwork.Repositories;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace plzwork.Services;

public class AuthService(IUsersRepository usersRepository, IConfiguration configuration) : IAuthService
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

    public async Task<LoginUserResult> LoginUserAsync(LoginDto loginDto)
    {
        var user = await usersRepository.GetUserAsync(loginDto.Username);

        if(user is null)
        {
            return new LoginUserResult(LoginUserStatus.InvalidCredentials, null);
        }

        var isVerifiedPassword = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);

        if(!isVerifiedPassword) {
            return new LoginUserResult(LoginUserStatus.InvalidCredentials, null);
        }

        List<Claim> claims = [
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        ];

        var jwtSecret = configuration["Jwt:Secret"];
        var jwtIssuer = configuration["Jwt:Issuer"];
        var jwtAudience = configuration["Jwt:Audience"];

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret!));

        var signingCredentials = new SigningCredentials(
            key, SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: signingCredentials
        );

        var JWT = new JwtSecurityTokenHandler().WriteToken(token);

        return new LoginUserResult(LoginUserStatus.Success, JWT);

    }
}