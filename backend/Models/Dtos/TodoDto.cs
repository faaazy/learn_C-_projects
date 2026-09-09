namespace DotnetTodoApp.Models.Dtos;

public record TodoDto(int Id, string Name, DateOnly? DueDate, bool IsCompleted);