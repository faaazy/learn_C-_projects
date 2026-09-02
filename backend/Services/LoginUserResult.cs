namespace plzwork.Services;

public record LoginUserResult(
    LoginUserStatus Status,
    string? JWT
);