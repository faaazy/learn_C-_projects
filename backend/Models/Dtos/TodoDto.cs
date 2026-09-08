namespace DotnetTodoApp.Models.Dtos;

public record TodoDto(int Id, string Name, DateTime DueDate, bool IsCompleted);