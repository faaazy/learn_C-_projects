using DotnetTodoApp.Models.Dtos;

namespace DotnetTodoApp.Services;


public record UpdateTaskResult(
    UpdateTaskStatus Status,
    TodoDto? Todo
);