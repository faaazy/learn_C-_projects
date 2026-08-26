using plzwork.Models.Dtos;

namespace plzwork.Services;


public record UpdateTaskResult(
    UpdateTaskStatus Status,
    TodoDto? Todo
);