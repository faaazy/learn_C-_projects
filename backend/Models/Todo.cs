namespace DotnetTodoApp.Models;

public class Todo
{
    public int Id {get; set;}

    public int UserId {get; set;}
    public User? User {get; set;}

    public required string Name {get; set;}

    public DateOnly? DueDate {get; set;}

    public bool IsCompleted {get; set;}
}