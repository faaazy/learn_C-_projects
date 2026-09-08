namespace DotnetTodoApp.Services;

public record LoginUserResult(
    LoginUserStatus Status,
    string? JWT
);