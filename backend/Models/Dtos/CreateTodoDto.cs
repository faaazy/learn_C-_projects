namespace plzwork.Models.Dtos;

public record CreateTodoDto(string Name, DateTime DueDate, bool IsCompleted);