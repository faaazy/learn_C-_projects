namespace plzwork.Models;

public class Todo
{
    public int Id {get; set;}

    public required string Name {get; set;}

    public DateTime DueDate {get; set;}

    public bool IsCompleted {get; set;}
}